using System;
using System.Collections.Generic;
using System.Text;
using static SDL2.SDL;

namespace ObjectiveSdl2 {
	public partial struct SdlRect {
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
	}

	partial struct SdlRect {
		public static explicit operator SDL_Rect(SdlRect rect) => new SDL_Rect { x = rect.X, y = rect.Y, w = rect.W, h = rect.H };
		public static explicit operator SdlRect(SDL_Rect rect) => new SdlRect(rect.x, rect.y, rect.w, rect.h);
	}
}
