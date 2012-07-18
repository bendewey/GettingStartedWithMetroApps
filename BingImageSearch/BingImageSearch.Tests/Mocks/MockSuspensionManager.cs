using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Services;

namespace BingImageSearch.Tests.Mocks
{
    class MockSuspensionManager : ISuspensionManager
    {
        private Dictionary<string, object> _sessionState;
        private List<Type> _knownTypes;
        
        public MockSuspensionManager()
        {
            _sessionState = new Dictionary<string, object>();
            _knownTypes = new List<Type>();
        }

        public Dictionary<string, object> SessionState
        {
            get { return _sessionState; }
        }

        public List<Type> KnownTypes
        {
            get { return _knownTypes; }
        }

        public Task SaveAsync()
        {
            return Task.Run(() => { });
        }

        public Task RestoreAsync()
        {
            return new Task(() => { });
        }
    }
}
