using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2.Core.Registers {
	public sealed class WindowsRegister {
		private readonly Dictionary<uint, SdlWindow> items = new Dictionary<uint, SdlWindow>();

		public void Remove(SdlWindow window) {
			var id = window.WindowID;
			lock (this.items) {
				this.items.Remove(id);
			}
		}

		public void Register(SdlWindow window) {
			var id = window.WindowID;
			lock (this.items) {
				items[id] = window;
			}
		}
		public SdlWindow Find(uint windowID) {
			lock (this.items) {
				this.items.TryGetValue(windowID, out var result);
				return result;
			}
		}
	}
}
