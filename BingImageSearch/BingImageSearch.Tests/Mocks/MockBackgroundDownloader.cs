using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using Windows.Foundation;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Uri = System.Uri;

namespace BingImageSearch.Tests.Mocks
{
    public class MockBackgroundDownloader : IBackgroundDownloader
    {
        public string StartedDownloadingWithUrl { get; set; }

        public IAsyncOperationWithProgress<DownloadOperation, DownloadOperation> StartDownloadAsync(Uri uri, IStorageFile storageFile)
        {
            StartedDownloadingWithUrl = uri.ToString();
            return AsyncInfo.Run<DownloadOperation, DownloadOperation>((ctx, progress) => Task.Run<DownloadOperation>(() => (DownloadOperation)null));
        }
    }
}