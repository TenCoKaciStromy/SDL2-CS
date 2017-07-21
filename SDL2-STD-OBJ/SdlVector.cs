using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2 {
	public partial struct SdlVector {
		public int X;
		public int Y;

		public SdlVector(int x, int y) {
			this.X = x;
			this.Y = y;
		}

		public override string ToString() => $"({X};{Y})";
	}

	partial struct SdlVector {
		public static SdlVector operator +(SdlVector a, SdlVector b) => new SdlVector(a.X + b.X, a.Y + b.Y);
		public static SdlVector operator -(SdlVector a, SdlVector b) => new SdlVector(a.X - b.X, a.Y - b.Y);

		public static SdlVector operator *(int b, SdlVector a) => new SdlVector(a.X * b, a.X * b);
		public static SdlVector operator *(SdlVector a, int b) => new SdlVector(a.X * b, a.X * b);
		public static SdlVector operator /(SdlVector a, int b) => new SdlVector(a.X / b, a.X / b);
		public static SdlVector operator %(SdlVector a, int b) => new SdlVector(a.X % b, a.X % b);
	}

	partial struct SdlVector {
		public static explicit operator SDL2.SDL.SDL_Point(SdlVector self) {
			return new SDL2.SDL.SDL_Point {
				x = self.X,
				y = self.Y
			};
		}
		public static explicit operator SdlVector(SDL2.SDL.SDL_Point self) {
			return new SdlVector(self.x, self.y);
		}
	}
}
