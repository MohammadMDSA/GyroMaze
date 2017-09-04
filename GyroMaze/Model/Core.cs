using GyroMaze.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.UI.Core;

namespace GyroMaze.Model
{
	class Core
	{
		public static Core MainCore;
		private OrientationHandler _orientation;
		private MainPage _RootPage;
		private Ball _Ball;
		private bool _Running;
		
		public Ball Ball { get { return _Ball; } }
		public bool Running { get { return _Running; } }
		public MainPage MainPage { get { return _RootPage; } }

		public Core()
		{
			_RootPage = MainPage.RootPage;
			_Ball = new Ball(this, new Vector2(200, 200));
			//_Ball = new Ball(RootPage.CanvasSize / 2);
			MainCore = this;
			Initialize();
			_Running = true;
			Task updateThead = new Task(Update);
			updateThead.Start();
		}

		public void Initialize()
		{
			_orientation = new OrientationHandler();
			if (_orientation.Initialize()) { }

			_orientation.ReadingChanged += OnReadingChange;

			_orientation.Start();

		}

		private void OnReadingChange(OrientationSensor sender, OrientationSensorReadingChangedEventArgs args)
		{
			SensorQuaternion quaternion = args.Reading.Quaternion;
			Ball.SetAccelaration(quaternion.X, quaternion.Y);
		}

		public async void Update()
		{
			while(Running)
			{
				Ball.Update();
				await Task.Delay(10);
			}
		}
	}
}
