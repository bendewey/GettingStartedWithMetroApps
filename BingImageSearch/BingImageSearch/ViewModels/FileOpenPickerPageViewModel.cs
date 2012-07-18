using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BingImageSearch.Adapters;
using BingImageSearch.Common;
using BingImageSearch.Common;
using BingImageSearch.Services;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml.Input;

namespace BingImageSearch.Model
{
    public class FileOpenPickerPageViewModel : BindableBase
    {
        private readonly ApplicationSettings _settings;
        private readonly IImageSearchService _imageSearchService;
        private readonly IFileOpenPickerUiManager _fileOpenPicker;

        public FileOpenPickerPageViewModel(ApplicationSettings settings, IImageSearchService imageSearchService, IFileOpenPickerUiManager fileOpenPicker)
        {
            _settings = settings;
            _imageSearchService = imageSearchService;
            _fileOpenPicker = fileOpenPicker;

            SearchCommand = new AsyncDelegateCommand(Search);
        }

        public ICommand SearchCommand { get; set; }

        private string _searchQuery;
        public string SearchQuery
        {
            get { return _searchQuery; }
            set { SetProperty(ref _searchQuery, value); }
        }

        private IList<ImageResult> _images;
        public IList<ImageResult> Images
        {
            get { return _images; }
            set { SetProperty(ref _images, value); }
        }

        public async Task Search()
        {
            Images = await _imageSearchService.Search(SearchQuery);
        }

        public async Task AddImage(object item)
        {
            var image = item as ImageResult;
            if (image == null) return;
            
            if (_fileOpenPicker.AllowedFileTypes.Any(ext => ext == "*" || image.MediaUrl.EndsWith(ext)))
            {
                var file = await _settings.GetTempFileAsync(image.MediaUrl);
                var result = _fileOpenPicker.AddFile(image.MediaUrl, file);
                Debug.Assert(result != AddFileResult.Added, "Image not added to FilePicker.  This sould probably be an inline message somewhere.");
            }
        }

        public void RemoveImage(object item)
        {
            var image = item as ImageResult;
            if (image == null) return;

            _fileOpenPicker.RemoveFile(image.MediaUrl);
        }
    }
}