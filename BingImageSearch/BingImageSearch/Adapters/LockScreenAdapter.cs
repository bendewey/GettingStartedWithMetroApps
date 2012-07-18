using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.UserProfile;

namespace BingImageSearch.Adapters
{
    public class LockScreenAdapter : ILockScreen
    {
        public async Task SetImageFileAsync(IStorageFile file)
        {
            await LockScreen.SetImageFileAsync(file);
        }

        public async Task SetImageStreamAsync(IRandomAccessStream stream)
        {
            await LockScreen.SetImageStreamAsync(stream);
        }
    }

    public interface ILockScreen
    {
        Task SetImageFileAsync(IStorageFile file);
        Task SetImageStreamAsync(IRandomAccessStream stream);
    }
}
