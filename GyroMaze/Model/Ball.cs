using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GyroMaze.Model
{
	class Ball
	{
		private Vector2 _Position;
		private Vector2 _Speed;
		public Vector2 Acceleration;
		private Core Core;
		public Vector2 Position
		{
			get { return _Position; }
		}
		public Vector2 Speed
		{
			get { return _Speed; }
		}

		public Ball(Core core, Vector2 Position)
		{
			Acceleration = new Vector2(0, 0);
			_Position = new Vector2(Position.X, Position.Y);
			_Speed = new Vector2(0, 0);
			this.Core = core;
		}

		public void Update()
		{
			_Speed += Acceleration;
			Vector2 size = this.Core.MainPage.CanvasSize;

			Vector2 temp = new Vector2(_Speed.Y, _Speed.X);

			//temp = _Speed;
			

			if (_Position.X + _Speed.X > size.X || _Position.X + _Speed.X < 0)
			{
				temp.X = 0;
				_Speed.X = 0;
			}
			if (_Position.Y + _Speed.Y > size.Y || _Position.Y + _Speed.Y < 0)
			{
				temp.Y = 0;
				_Speed.Y = 0;
			}

			_Position += temp;
		}

		public void SetAccelaration(float x, float y)
		{
			int xTemp = (int)(x * 100);
			int yTemp = (int)(y * 100);
			Vector2 temp = new Vector2(xTemp, yTemp);

			Acceleration = temp * 1.0f / 100f;
			//Acceleration.X *= Math.Abs(x);
			//Acceleration.Y *= Math.Abs(y);
		}
	}
}
