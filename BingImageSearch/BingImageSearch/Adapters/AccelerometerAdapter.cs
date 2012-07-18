using System;
using System.Linq;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.UI.Core;

namespace BingImageSearch.Adapters
{
    public class AccelerometerAdapter : IAccelerometer
    {
        private readonly Accelerometer _accelerometer;

        private TypedEventHandler<IAccelerometer, AccelerometerShakenEventArgs> _shaken;
        public event TypedEventHandler<IAccelerometer, AccelerometerShakenEventArgs> Shaken
        {
            add
            {
                if (_accelerometer != null)
                {
                    if (_shaken == null || !_shaken.GetInvocationList().Any())
                    {
                        _accelerometer.Shaken += Accelerometer_Shaken;
                    }
                }
                _shaken += value;
            }
            remove
            {
                _shaken -= value;
                if (_accelerometer != null)
                {
                    if (_shaken == null || !_shaken.GetInvocationList().Any())
                    {
                        _accelerometer.Shaken -= Accelerometer_Shaken;
                    }
                }
            }
        }

        public AccelerometerAdapter()
        {
            _accelerometer = Accelerometer.GetDefault();
        }

        async void Accelerometer_Shaken(Accelerometer sender, AccelerometerShakenEventArgs args)
        {
            var dispatcher = App.Dispatcher;
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, (DispatchedHandler)(() =>
                                                                      {
                                                                          _shaken(this, args);
                                                                      }));
        }
    }
}