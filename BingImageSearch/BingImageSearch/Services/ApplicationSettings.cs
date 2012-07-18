using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using BingImageSearch.Model;
using Windows.ApplicationModel;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Streams;

namespace BingImageSearch.Services
{
    public class ApplicationSettings
    {
        private readonly ISuspensionManager _suspensionManager;
        private readonly IBackgroundDownloader _backgroundDownloader;

        public event EventHandler Saved = delegate { };

        public ApplicationSettings(IBackgroundDownloader backgroundDownloader)
            : this(new SuspensionManager(), backgroundDownloader)
        {
        }

        protected ApplicationSettings(ISuspensionManager suspensionManager, IBackgroundDownloader backgroundDownloader)
        {
            _suspensionManager = suspensionManager;
            _backgroundDownloader = backgroundDownloader;
        }

        public virtual List<SearchInstance> Searches
        {
            get { return GetOrNew<List<SearchInstance>>("Searches"); }
            set { SaveOrCreate("Searches", value); }
        }

        public string SearchResultsCurrentView
        {
            get { return GetOrDefault<string>("SearchResultsCurrentView", SearchResultsPageViewModes.SplitView); }
            set { SaveOrCreate("SearchResultsCurrentView", value); }
        }

        public string CurrentPageTitle
        {
            get { return GetOrDefault<string>("CurrentPageTitle"); }
            set { SaveOrCreate("CurrentPageTitle", value); }
        }

        public string CurrentPage
        {
            get { return GetOrDefault<string>("CurrentPage"); }
            set { SaveOrCreate("CurrentPage", value); }
        }

        public virtual SearchInstance SelectedInstance
        {
            get { return GetOrDefault<SearchInstance>("SelectedInstance"); }
            set { SaveOrCreate("SelectedInstance", value); }
        }

        public virtual ImageResult SelectedImage
        {
            get { return SelectedInstance.SelectedImage; }
            set { SelectedInstance.SelectedImage = value; }
        }

        public ResultSize ImageResultSize
        {
            get { return (ResultSize)GetOrDefault<int>("ImageResultSize", (int)ResultSize.Twenty); }
            set { SaveOrCreate("ImageResultSize", (int)value); }
        }

        public Rating Rating
        {
            get { return (Rating)GetOrDefault<int>("Rating", (int)Rating.Strict); }
            set { SaveOrCreate("Rating", (int)value); }
        }

        public async Task<StorageFile> GetLockScreenFileAsync(string uri)
        {
            return await CreateAndDownloadFile(uri, "LockScreen.jpg");
        }

        public async Task<StorageFile> GetShareFileAsync(string uri)
        {
            return await CreateAndDownloadFile(uri, "ShareImage.jpg");
        }

        public async Task<StorageFile> GetTempFileAsync(string uri)
        {
            return await CreateAndDownloadFile(uri);
        }

        private bool _isSaving;
        public async virtual Task SaveAsync()
        {
            if (_isSaving) return;
            _isSaving = true;
            await _suspensionManager.SaveAsync();
            _isSaving = false;
            Saved(this, EventArgs.Empty);
        }

        public async virtual Task RestoreAsync()
        {
            await _suspensionManager.RestoreAsync();
        }

        private T GetOrDefault<T>(string key, T @default = default(T))
        {
            if (_suspensionManager.SessionState.ContainsKey(key))
            {
                return (T)_suspensionManager.SessionState[key];
            }
            return @default;
        }

        private T GetOrNew<T>(string key) where T : class, new()
        {
            if (_suspensionManager.SessionState.ContainsKey(key))
            {
                return (T)_suspensionManager.SessionState[key];
            }
            return new T();
        }

        private void SaveOrCreate<T>(string key, T value)
        {
            if (_suspensionManager.SessionState.ContainsKey(key))
            {
                _suspensionManager.SessionState[key] = value;
            }
            else
            {
                _suspensionManager.SessionState.Add(key, value);
            }
        }

        private async Task<StorageFile> CreateAndDownloadFile(string uri, string filename = null)
        {
            filename = filename ?? Regex.Replace(uri, "https?://|[/?&#]", "");
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await _backgroundDownloader.StartDownloadAsync(new Uri(uri), file);
            return file;
        }

        #region Modified Version of Suspension Manager from WinRT Samples
        private class SuspensionManager : ISuspensionManager
        {
            private Dictionary<string, object> sessionState_ = new Dictionary<string, object>();
            private List<Type> knownTypes_ = new List<Type>() 
            { 
                typeof(List<SearchInstance>),
                typeof(string),
                typeof(SearchInstance),
                typeof(ResultSize),
                typeof(Rating)
            };
            private const string filename = "_sessionState.xml";

            public Dictionary<string, object> SessionState
            {
                get { return sessionState_; }
            }

            public List<Type> KnownTypes
            {
                get { return knownTypes_; }
            }

            async public Task SaveAsync()
            {
                // Get the output stream for the SessionState file.
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                using (IRandomAccessStream raStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
                    {
                        // Serialize the Session State.
                        DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, object>), knownTypes_);
                        serializer.WriteObject(outStream.AsStreamForWrite(), sessionState_);
                        await outStream.FlushAsync();
                    }
                }
            }

            // Restore the saved sesison state
            async public Task RestoreAsync()
            {
                // Get the input stream for the SessionState file.
                try
                {
                    StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                    if (file == null) return;
                    IInputStream inStream = await file.OpenSequentialReadAsync();

                    // Deserialize the Session State.
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, object>), knownTypes_);
                    sessionState_ = (Dictionary<string, object>)serializer.ReadObject(inStream.AsStreamForRead());
                }
                catch (Exception)
                {
                    // Restoring state is best-effort.  If it fails, the app will just come up with a new session.
                }
            }
        }
        #endregion
    }
}
