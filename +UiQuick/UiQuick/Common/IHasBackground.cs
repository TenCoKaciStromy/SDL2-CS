using System;
using System.Collections.Generic;
using System.Text;
using UiQuick.Drawing;

namespace UiQuick.Common {
	public interface IHasBackground {
		IRenderable Background { get; set; }

		event RenderDelegate BeforeBackgroundRender;
		event RenderDelegate AfterBackgroundRender;
	}
}
