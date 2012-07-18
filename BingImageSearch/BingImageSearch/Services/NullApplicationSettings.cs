using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BingImageSearch.Model;
using BingImageSearch.Adapters;

namespace BingImageSearch.Services
{
    class NullApplicationSettings : ApplicationSettings
    {
        public NullApplicationSettings() : base(new NullSuspensionManager(), new NullBackgroundDownloader())
        {
        }

        class NullSuspensionManager : ISuspensionManager
        {
            public NullSuspensionManager()
            {
                SessionState = new Dictionary<string, object>();
                KnownTypes = new List<Type>();
            }

            public Dictionary<string, object> SessionState { get; private set; }
            public List<Type> KnownTypes { get; private set; }

            public async Task SaveAsync()
            {
                await Task.Run(() => { });
            }

            public async Task RestoreAsync()
            {
                await Task.Run(() => { });
            }
        }
    }
}
