using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Allodium.SDL2.Native.SDL;

namespace Allodium.SDL2 {
	public struct SdlDisplayMode {
		public uint Format;
		public int Width;
		public int Height;
		public int RefreshRate;
		public IntPtr DriverData; // void*

		public static explicit operator SDL_DisplayMode(SdlDisplayMode value) {
			return new SDL_DisplayMode {
				format = value.Format,
				w = value.Width,
				h = value.Height,
				refresh_rate = value.RefreshRate,
				driverdata = value.DriverData
			};
		}

		public static explicit operator SdlDisplayMode(SDL_DisplayMode value) {
			return new SdlDisplayMode {
				Format = value.format,
				Width = value.w,
				Height = value.h,
				RefreshRate = value.refresh_rate,
				DriverData = value.driverdata
			};
		}
	}
}
