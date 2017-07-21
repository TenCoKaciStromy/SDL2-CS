using System;
using ObjectiveSdl2;
using ObjectiveSdl2.Core;
using ObjectiveSdl2.Drawing;

namespace SDL2_STD_OBJ.CmdLab
{
    class Program
    {
        static void Main(string[] args)
        {
			Main_C(args);
		}

		static void Main_C(string[] args) {
			SdlContext.Default.Initialize(SdlInitializationFlags.Video);

			var window = SdlWindow.Create("My window", new SdlVector(10, 20), new SdlVector(1280, 800));
			var renderer = window.Renderer;

			var bmp = SdlSurface.LoadFromBmpFile(@"C:\Users\TcKs\Pictures\screenshot.bmp");
			var tex = renderer.CreateTexture(bmp);

			renderer.DrawColor = new SdlRgba(0, 255, 0);
			renderer.Clear();
			renderer.CopyFrom(tex);
			renderer.DrawColor = new SdlRgba(255, 0, 0);
			renderer.DrawRect(new SdlRect(20, 30, 50, 75));
			renderer.FillRect(new SdlRect(10, 20, 100, 200));
			renderer.Present();

			window.Show();
			window.Raise();

			var loop = new SdlEventLoop();
			loop.EventHandlers += (_event) => {
				Console.WriteLine(_event.type);

				if (_event.type == SDL2.SDL.SDL_EventType.SDL_WINDOWEVENT) {
					Console.WriteLine("WIN: " + _event.window.windowEvent);
				}
				else if (_event.type == SDL2.SDL.SDL_EventType.SDL_MOUSEMOTION) {
					renderer.DrawPoint(_event.motion.x, _event.motion.y);
					renderer.Present();
				}

				return SdlEventHandlerResult.None;
			};
			loop.Run(SdlContext.Default);
		}

		static void Main_B(string[] args) {
			var window = SdlWindow.Create("My window", new SdlVector(10, 20), new SdlVector(640, 480));
			// var renderer = SdlContext.Default.CreateRenderer(((ISdlObject)window).GetValidHandle(), SdlRenderingFlags.Software);
			var renderer = window.Renderer;


			//var ptrBmp = SDL2.SDL.SDL_LoadBMP(@"C:\Users\TcKs\Pictures\screenshot.bmp");
			//var tex = SdlContext.Default.CreateTextureFromSufrace(((ISdlObject)renderer).GetValidHandle(), ptrBmp);
			var bmp = SdlSurface.LoadFromBmpFile(@"C:\Users\TcKs\Pictures\screenshot.bmp");
			var tex = renderer.CreateTexture(bmp);

			renderer.Clear();
			renderer.CopyFrom(tex);
			renderer.Present();

			window.Show();
			window.Raise();

			var loop = new SdlEventLoop();
			loop.EventHandlers += (_event) => {
				Console.WriteLine(_event.type);

				if (_event.type == SDL2.SDL.SDL_EventType.SDL_WINDOWEVENT) {
					Console.WriteLine(_event.window.windowEvent);
				}

				return SdlEventHandlerResult.None;
			};
			loop.Run(SdlContext.Default);
		}

		static void Main_A(string[] args) {
			SdlMessageLoopAction(() => {
				var window = SdlWindow.Create("My window", new SdlVector(10, 20), new SdlVector(640, 480));
				//var renderer = SdlContext.Default.CreateRenderer(((ISdlObject)window).GetValidHandle(), SdlRenderingFlags.Software);
				var renderer = window.Renderer;


				//var ptrBmp = SDL2.SDL.SDL_LoadBMP(@"C:\Users\TcKs\Pictures\screenshot.bmp");
				//var tex = SdlContext.Default.CreateTextureFromSufrace(((ISdlObject)renderer).GetValidHandle(), ptrBmp);
				var bmp = SdlSurface.LoadFromBmpFile(@"C:\Users\TcKs\Pictures\screenshot.bmp");
				var tex = renderer.CreateTexture(bmp);

				renderer.Clear();
				renderer.CopyFrom(tex);
				renderer.Present();

				window.Show();
				window.Raise();

				return;
			});
		}

		static void SdlAction(Action action) {
			try {
				SdlContext.Default.Initialize(SdlInitializationFlags.Video);

				action();
			}
			finally {
				SdlContext.Default.Quit();
			}
		}

		static void SdlMessageLoopAction(Action action) {
			SdlAction(() => {
				action();

				bool quit = false;
				while (!quit) {
					while (0 < SDL2.SDL.SDL_PollEvent(out var _event)) {
						Console.WriteLine(_event.type);

						if (_event.type == SDL2.SDL.SDL_EventType.SDL_QUIT) {
							quit = true;
							break;
						}
					}
				}
			});
		}
	}
}