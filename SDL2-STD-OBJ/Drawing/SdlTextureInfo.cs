using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2.Drawing {
	public struct SdlTextureInfo {
		public uint Format;
		public int Access;
		public int Width;
		public int Height;

		public SdlVector Size => new SdlVector(this.Width, this.Height);
	}
}
