using Windows.Devices.Sensors;
using Windows.Foundation;

namespace BingImageSearch.Adapters
{
    public interface IAccelerometer
    {
        event TypedEventHandler<IAccelerometer, AccelerometerShakenEventArgs> Shaken;
    }
}
