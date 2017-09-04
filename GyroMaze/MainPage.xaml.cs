using GyroMaze.Model;
using GyroMaze.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GyroMaze
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public static MainPage RootPage;
		private CanvasState _State;
		public CanvasState State
		{
			get { return _State; }
		}
		public Vector2 CanvasSize
		{
			get { return new Vector2((float)DrawCanvas.Size.Width, (float)DrawCanvas.Size.Height); }
		}

		public MainPage()
		{
			_State = CanvasState.Unloaded;
			this.InitializeComponent();
			ShowStatusBar();
			RootPage = this;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			Core mainCore = new Core();
		}

		private async void ShowStatusBar()
		{
			var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
			await statusBar.ShowAsync();
			statusBar.BackgroundOpacity = 1;
			statusBar.BackgroundColor = (Color)Resources["SystemAccentColor"];
			statusBar.ForegroundColor = Colors.White;
		}

		private void DrawCanvas_Loaded(object sender, RoutedEventArgs e)
		{
			_State = CanvasState.Loaded;
		}

		private void DrawCanvas_Loading(FrameworkElement sender, object args)
		{
			_State = CanvasState.Loading;
		}

		private void DrawCanvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
		{

			Core mainCore = Core.MainCore;
			if (mainCore != null && !mainCore.Running)
				return;
			var session = args.DrawingSession;
			session.FillCircle(mainCore.Ball.Position, 20, Colors.Red);

			session.FillCircle((float)sender.Size.Width, (float)sender.Size.Height, 20, Colors.Red);
			session.DrawLine(new Vector2(200, 200), mainCore.Ball.Position / 10, Colors.Blue);
			session.DrawText(
				mainCore.Ball.Position.X.ToString() + "\n"
				+ mainCore.Ball.Position.Y.ToString() + "\n"
				+ mainCore.Ball.Acceleration.X.ToString() + "\n"
				+ mainCore.Ball.Acceleration.Y.ToString() + "\n"
				+ mainCore.Ball.Speed.X.ToString() + "\n"
				+ mainCore.Ball.Speed.Y.ToString() + "\n"
				+ sender.Size.Width + "\n"
				+ sender.Size.Width
				, 100, 100, Colors.Red);
		}
	}

	public enum CanvasState
	{
		Unloaded,
		Loading,
		Loaded
	}
}
