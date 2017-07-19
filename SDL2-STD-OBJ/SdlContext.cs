using System;
using System.Collections.Generic;
using System.Text;
using ObjectiveSdl2.Core;
using static SDL2.SDL;

namespace ObjectiveSdl2 {
	public sealed class SdlContext {
		#region Singleton
		private readonly static SdlContext @default = new SdlContext();
		public static SdlContext Default => @default;
		#endregion Singleton

		private readonly object syncRoot = new object();
		public object SyncRoot => this.syncRoot;

		private bool isInitialized;
		public bool IsInitialized => this.isInitialized;

		public void Initialize(SdlInitializationFlags initFlags) {
			lock (this.syncRoot) {
				if (this.IsInitialized) {
					throw new InvalidOperationException("The SdlContext is already initalized.");
				}

				var rslt = SDL_Init((uint)initFlags);
				if (0 != rslt) {
					throw SdlNativeException.CreateFromLastSdlError();
				}
			}
		}

		public void Quit() {
			lock (this.syncRoot) {
				if (this.IsInitialized) {
					SDL_Quit();
				}
			}
		}
	}
}
