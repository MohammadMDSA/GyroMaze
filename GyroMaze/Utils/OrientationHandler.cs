using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace GyroMaze.Utils
{
	class OrientationHandler
	{
		private OrientationSensor _sensor;

		public bool Initialize()
		{
			_sensor = OrientationSensor.GetDefault(SensorReadingType.Absolute, SensorOptimizationGoal.Precision);

		}
	}
}
