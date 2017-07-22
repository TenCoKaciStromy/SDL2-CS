using System;
using System.Collections.Generic;
using System.Text;
using UiQuick.Drawing;

namespace UiQuick.Common {
	public interface IHasForeground {
		IRenderable Foreground { get; set; }

		event RenderDelegate BeforeForegroundRender;
		event RenderDelegate AfterForegroundRender;
	}
}
