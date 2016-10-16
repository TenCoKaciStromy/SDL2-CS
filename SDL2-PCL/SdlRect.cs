using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Core;
using Allodium.SDL2.Native;
using static Allodium.SDL2.Native.SDL;

namespace Allodium.SDL2 {
	public struct SdlRect {
		public int X;
		public int Y;
		public int W;
		public int H;

		public SdlRect(int x, int y, int w, int h) {
			this.X = x;
			this.Y = y;
			this.W = w;
			this.H = h;
		}

		public static explicit operator SDL_Rect(SdlRect rect) {
			return new SDL_Rect {
				x = rect.X,
				y = rect.Y,
				w = rect.W,
				h = rect.H
			};
		}
		public static explicit operator SdlRect(SDL_Rect rect) {
			return new SdlRect(rect.x, rect.y, rect.w, rect.h);
		}
	}
}
