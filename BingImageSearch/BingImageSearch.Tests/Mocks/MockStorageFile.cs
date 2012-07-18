using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;

namespace BingImageSearch.Tests.Mocks
{
    public class MockStorageFile : IStorageFile
    {
        public IAsyncAction RenameAsync(string desiredName)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncAction RenameAsync(string desiredName, NameCollisionOption option)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncAction DeleteAsync()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncAction DeleteAsync(StorageDeleteOption option)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncOperation<BasicProperties> GetBasicPropertiesAsync()
        {
            throw new System.NotImplementedException();
        }

        public bool IsOfType(StorageItemTypes type)
        {
            throw new System.NotImplementedException();
        }

        public FileAttributes Attributes { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public IAsyncOperation<IRandomAccessStreamWithContentType> OpenReadAsync()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncOperation<IInputStream> OpenSequentialReadAsync()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncOperation<IRandomAccessStream> OpenAsync(FileAccessMode accessMode)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncOperation<StorageFile> CopyAsync(IStorageFolder destinationFolder)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncAction CopyAndReplaceAsync(IStorageFile fileToReplace)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncAction MoveAsync(IStorageFolder destinationFolder)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncAction MoveAndReplaceAsync(IStorageFile fileToReplace)
        {
            throw new System.NotImplementedException();
        }

        public string ContentType { get; set; }
        public string FileType { get; set; }

        public IAsyncAction MoveAsync(IStorageFolder destinationFolder, string desiredNewName, NameCollisionOption option)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncAction MoveAsync(IStorageFolder destinationFolder, string desiredNewName)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncOperation<StorageFile> CopyAsync(IStorageFolder destinationFolder, string desiredNewName, NameCollisionOption option)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncOperation<StorageFile> CopyAsync(IStorageFolder destinationFolder, string desiredNewName)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncOperation<StorageStreamTransaction> OpenTransactedWriteAsync()
        {
            throw new NotImplementedException();
        }
    }
}