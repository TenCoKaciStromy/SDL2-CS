using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick {
	public interface IChild {
		IChildControls ParentCollection { get; }
		bool RemoveFromParent();
		bool RemoveFromParent(IChildControls parentCollection);
	}
}
