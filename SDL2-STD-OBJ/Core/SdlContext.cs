using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ObjectiveSdl2.Core;
using ObjectiveSdl2.Core.Registers;
using ObjectiveSdl2.Drawing;
using static SDL2.SDL;

namespace ObjectiveSdl2.Core {
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
					this.isInitialized = false;
					this.registers = null;
					SDL_Quit();
				}
			}
		}
	}
	#endregion Init & Quit

	#region Registers
	partial class SdlContext {
		private RegistersContainer registers;
		public RegistersContainer Registers {
			get {
				var result = this.registers;
				if (result is null) {
					lock (this.syncRoot) {
						result = this.registers;
						if (result is null) {
							if (this.isInitialized) {
								this.registers = result = new RegistersContainer();
							}
						}
					}
				}

				return result;
			}
		}

		public sealed class RegistersContainer {
			private readonly WindowsRegister windows = new WindowsRegister();
			public WindowsRegister Windows => this.windows;
		}
	}
	#endregion Registers
}
