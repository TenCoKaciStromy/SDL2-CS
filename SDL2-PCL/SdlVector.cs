using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allodium.SDL2 {
	public struct SdlVector {
		public int X;
		public int Y;

		public SdlVector(int x, int y) {
			this.X = x;
			this.Y = y;
		}
	}
}
