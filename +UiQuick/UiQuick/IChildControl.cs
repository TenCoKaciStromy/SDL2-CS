using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick {
	public interface IChildControl : IControl {
		IChildControlCollection ParentCollection { get; }
		bool RemoveFromParent();
		bool RemoveFromParent(IChildControlCollection parentCollection);
	}
}
