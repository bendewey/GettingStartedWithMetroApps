using BingImageSearch.Messaging;

namespace BingImageSearch.Tests.Mocks
{
    public class MockShareDataRequestedPump : IShareDataRequestedPump
    {
        public object DataToShare { get; set; }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
