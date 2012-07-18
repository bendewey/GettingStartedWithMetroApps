using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Uri = System.Uri;

namespace BingImageSearch.Adapters
{
    public class BackgroundDownloaderAdapter : IBackgroundDownloader
    {
        public IAsyncOperationWithProgress<DownloadOperation, DownloadOperation> StartDownloadAsync(Uri uri, IStorageFile storageFile)
        {
            return new BackgroundDownloader().CreateDownload(uri, storageFile).StartAsync();
        }
    }

    public class NullBackgroundDownloader : IBackgroundDownloader
    {
        public IAsyncOperationWithProgress<DownloadOperation, DownloadOperation> StartDownloadAsync(Uri uri, IStorageFile storageFile)
        {
            return AsyncInfo.Run<DownloadOperation, DownloadOperation>((token, progress) =>
              Task.Run<DownloadOperation>(() =>
              {
                 return (DownloadOperation)null;
              }, token));
        }
    }

    public interface IBackgroundDownloader
    {
        IAsyncOperationWithProgress<DownloadOperation, DownloadOperation>  StartDownloadAsync(Uri uri, IStorageFile storageFile);
    }
}
