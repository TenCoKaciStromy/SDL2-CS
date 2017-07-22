using System;
using System.Collections.Generic;
using System.Text;
using ObjectiveSdl2;

namespace UiQuick.Drawing {
	public sealed class ColorFillPainter : IPainter {
		public SdlRgba Color { get; set; }

		public void Paint(SdlRect bounds, IRenderContext context) {
			// because the "FillRect" does not works, we will use painting by lines
			var x1 = bounds.X;
			var y1 = bounds.Y;
			var x2 = bounds.W + x1;
			var y2 = bounds.H + y1;

			var renderer = context.Renderer;
			renderer.DrawColor = this.Color;
			for (var i = y1; y1 <= y2; i++) {
				renderer.DrawLine(x1, i, x2, i);
			}
		}
	}
}
