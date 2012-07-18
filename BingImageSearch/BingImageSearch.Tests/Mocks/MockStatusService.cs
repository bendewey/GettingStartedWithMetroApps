using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Services;

namespace BingImageSearch.Tests.Mocks
{
    class MockStatusService : IStatusService
    {
        public bool WasToggled;

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading && !value)
                {
                    WasToggled = true;
                }
                _isLoading = value;
            }
        }

        public string Title { get; set; }

        public string Message { get; set; }        

        public string TemporaryMessage { get; set; }

        public bool IsNetworkUnavailable { get; private set; }
        public void SetNetworkUnavailable()
        {
            IsNetworkUnavailable = true;
        }

        public bool IsBingUnavailable { get; private set; }
        public void SetBingUnavailable()
        {
            IsBingUnavailable = true;
        }
    }
}
