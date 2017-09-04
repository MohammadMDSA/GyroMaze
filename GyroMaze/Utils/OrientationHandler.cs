using System;
using Windows.Devices.Sensors;
using Windows.Foundation;

namespace GyroMaze.Utils
{
	class OrientationHandler
	{
		private OrientationSensor _sensor;
		public event TypedEventHandler<OrientationSensor, OrientationSensorReadingChangedEventArgs> ReadingChanged;

		private bool _IsRunning;
		public bool IsRunning
		{
			get { return _IsRunning; }
		}

		public OrientationHandler()
		{
			_IsRunning = false;
		}

		public bool Initialize()
		{
			_sensor = OrientationSensor.GetDefault();
			if (_sensor != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Start()
		{
			_sensor.ReportInterval = Math.Max(_sensor.MinimumReportInterval, 16);


			if (ReadingChanged.GetInvocationList().Length < 1)
				return false;
			_sensor.ReadingChanged += ReadingChanged;
			_IsRunning = true;
			return true;
		}

		public void Stop()
		{
			_sensor.ReadingChanged -= ReadingChanged;
			_sensor.ReportInterval = 0;
			_IsRunning = false;
		}
	}
}
