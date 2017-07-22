using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick.Common {
	public static class ComponentHelper {
		public static void SetProperty<T>(object sender, ref T field, T value, EventHandler beforeChangeEvent, EventHandler afterChangeEvent) {
			beforeChangeEvent?.Invoke(sender, EventArgs.Empty);
			field = value;
			afterChangeEvent?.Invoke(sender, EventArgs.Empty);
		}
	}
}
