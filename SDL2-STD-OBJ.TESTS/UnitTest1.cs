using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectiveSdl2;

namespace SDL2_STD_OBJ.TESTS {
	[TestClass]
	public class UnitTest1 {
		[TestMethod]
		public void CanInitializeVideo() {
			try {
				SdlContext.Default.Initialize(SdlInitializationFlags.Video);

				Assert.IsTrue(SdlContext.Default.IsInitialized);
			}
			finally {
				SdlContext.Default.Quit();
			}
		}

		private void SdlAction(Action action) {
			try {
				SdlContext.Default.Initialize(SdlInitializationFlags.Video);

				Assert.IsTrue(SdlContext.Default.IsInitialized);

				action();
			}
			finally {
				SdlContext.Default.Quit();
			}
		}

		private void SdlMessageLoopAction(Action action) {
			this.SdlAction(() => {
				action();

				bool quit = false;
				while (!quit) {
					while (0 < SDL2.SDL.SDL_PollEvent(out var _event)) {
						if (_event.type == SDL2.SDL.SDL_EventType.SDL_QUIT) {
							quit = true;
							break;
						}
					}
				}
			});
		}

		[TestMethod]
		public void CanCreateWindow() {
			SdlAction(() => {
				var window = SdlWindow.Create("My window", new SdlVector(10, 20), new SdlVector(30, 40));

				Assert.AreEqual("My window", window.TitleRefresh());
				Assert.AreEqual(new SdlVector(10, 20), window.PositionRefresh());
				Assert.AreEqual(new SdlVector(30, 40), window.SizeRefresh());
			});
		}

		[TestMethod]
		public void CanShowWindow() {
			SdlMessageLoopAction(() => {
				//var t = Task.Factory.StartNew(() => {
				//	var window = SdlWindow.Create("My window", new SdlVector(10, 20), new SdlVector(30, 40));

				//	Assert.AreEqual("My window", window.TitleRefresh());
				//	Assert.AreEqual(new SdlVector(10, 20), window.PositionRefresh());
				//	Assert.AreEqual(new SdlVector(30, 40), window.SizeRefresh());

				//	window.Show();
				//	window.Raise();
				//});

				//Task.Delay(10000).Wait();

				var window = SdlWindow.Create("My window", new SdlVector(10, 20), new SdlVector(30, 40));

				Assert.AreEqual("My window", window.TitleRefresh());
				Assert.AreEqual(new SdlVector(10, 20), window.PositionRefresh());
				Assert.AreEqual(new SdlVector(30, 40), window.SizeRefresh());

				window.Show();
				window.Raise();

				return;
			});
		}
	}
}
