using BingImageSearch.Adapters;
using Windows.Devices.Sensors;
using Windows.Foundation;

namespace BingImageSearch.Tests.Mocks
{
    class MockAccelerometer : IAccelerometer
    {
        public event TypedEventHandler<IAccelerometer, AccelerometerShakenEventArgs> Shaken;
    }
}
