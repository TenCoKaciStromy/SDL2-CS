using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick.Drawing {
	public interface IRenderable {
		void Render(IRenderContext context);
	}
}
