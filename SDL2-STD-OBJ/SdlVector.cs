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
	}

	partial struct SdlVector {
		public static SdlVector operator +(SdlVector a, SdlVector b) => new SdlVector(a.X + b.X, a.Y + b.Y);
		public static SdlVector operator -(SdlVector a, SdlVector b) => new SdlVector(a.X - b.X, a.Y - b.Y);

		public static SdlVector operator *(int b, SdlVector a) => new SdlVector(a.X * b, a.X * b);
		public static SdlVector operator *(SdlVector a, int b) => new SdlVector(a.X * b, a.X * b);
		public static SdlVector operator /(SdlVector a, int b) => new SdlVector(a.X / b, a.X / b);
		public static SdlVector operator %(SdlVector a, int b) => new SdlVector(a.X % b, a.X % b);
	}
}
