using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using Windows.Storage;
using Windows.Storage.Streams;

namespace BingImageSearch.Tests.Mocks
{
    class MockLockScreen : ILockScreen
    {
        public object CurrentLockScreen { get; set; }

        public Task SetImageFileAsync(IStorageFile file)
        {
            return Task.Run(() => CurrentLockScreen = file);
        }

        public Task SetImageStreamAsync(IRandomAccessStream stream)
        {
            return Task.Run(() => CurrentLockScreen = stream);
        }
    }
}
