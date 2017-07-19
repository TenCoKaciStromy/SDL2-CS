using System;
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

		[TestMethod]
		public void CanCreateWindow() {
			SdlAction(() => {
				var window = SdlWindow.Create("My window", new SdlVector(10, 20), new SdlVector(30, 40));

				Assert.AreEqual("My window", window.TitleRefresh());
				Assert.AreEqual(new SdlVector(10, 20), window.PositionRefresh());
				Assert.AreEqual(new SdlVector(30, 40), window.SizeRefresh());
			});
		}
	}
}
