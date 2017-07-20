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
			Main_B(args);
		}

		static void Main_B(string[] args) {
			var window = SdlWindow.Create("My window", new SdlVector(10, 20), new SdlVector(640, 480));
			var renderer = SdlContext.Default.CreateRenderer(((ISdlObject)window).GetValidHandle(), SdlRenderingFlags.Software);


			var ptrBmp = SDL2.SDL.SDL_LoadBMP(@"C:\Users\TcKs\Pictures\screenshot.bmp");
			var tex = SdlContext.Default.CreateTextureFromSufrace(((ISdlObject)renderer).GetValidHandle(), ptrBmp);

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

				return false;
			};
			loop.Run();
		}

		static void Main_A(string[] args) {
			SdlMessageLoopAction(() => {
				var window = SdlWindow.Create("My window", new SdlVector(10, 20), new SdlVector(640, 480));
				var renderer = SdlContext.Default.CreateRenderer(((ISdlObject)window).GetValidHandle(), SdlRenderingFlags.Software);


				var ptrBmp = SDL2.SDL.SDL_LoadBMP(@"C:\Users\TcKs\Pictures\screenshot.bmp");
				var tex = SdlContext.Default.CreateTextureFromSufrace(((ISdlObject)renderer).GetValidHandle(), ptrBmp);

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