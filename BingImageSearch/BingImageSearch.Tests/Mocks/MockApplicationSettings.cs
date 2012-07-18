using BingImageSearch.Adapters;
using BingImageSearch.Services;

namespace BingImageSearch.Tests.Mocks
{
    public class MockApplicationSettings : ApplicationSettings
    {
        public MockApplicationSettings() : base(new MockSuspensionManager(), new NullBackgroundDownloader())
        {
        }
    }
}
