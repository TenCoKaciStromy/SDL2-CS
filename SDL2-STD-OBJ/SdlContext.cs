using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ObjectiveSdl2.Core;
using ObjectiveSdl2.Drawing;
using static SDL2.SDL;

namespace ObjectiveSdl2 {
	public sealed partial class SdlContext { }

	#region Singleton
	partial class SdlContext {
		private readonly static SdlContext @default = new SdlContext();
		public static SdlContext Default => @default;
	}
	#endregion Singleton

	#region Init & Quit
	partial class SdlContext {
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
				else {
					this.isInitialized = true;
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
	#endregion Init & Quit
}
