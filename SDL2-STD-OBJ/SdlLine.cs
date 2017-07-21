using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2 {
	public struct SdlLine {
		public SdlVector Start;
		public SdlVector End;

		public SdlLine(SdlVector start, SdlVector end) {
			this.Start = start;
			this.End = end;
		}
		public SdlLine(int x1, int y1, int x2, int y2) {
			this.Start = new SdlVector(x1, y1);
			this.End = new SdlVector(x2, y2);
		}

		public override string ToString() => $"[{{{Start}}}:{{{End}}}]";
	}
}
