using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick {
	public class RenderEventArgs : EventArgs {
		public IRenderContext Context { get; }

		public RenderEventArgs(IRenderContext context) {
			this.Context = context;
		}
	}
}
