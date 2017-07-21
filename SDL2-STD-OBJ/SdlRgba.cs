using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2 {
	public partial struct SdlRgba {
		public const byte ALPHA_OPAQUE = 255;

		public byte R;
		public byte G;
		public byte B;
		public byte A;

		public SdlRgba(byte r, byte g, byte b) : this(r, g, b, ALPHA_OPAQUE) { }
		public SdlRgba(byte r, byte g, byte b, byte a) {
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

		public override string ToString() => $"({R};{G};{B};{A})";
	}
}
