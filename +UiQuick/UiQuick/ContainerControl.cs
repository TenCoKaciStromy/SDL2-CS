using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick {
	public class ContainerControl : Control, IContainerControl {
		#region IContainerControl
		private readonly IChildControlCollection children;
		public IChildControlCollection Children => this.children;
		#endregion IContainerControl

		public ContainerControl() {
			this.children = this.CreateChildrenOrThrow();
		}

		private IChildControlCollection CreateChildrenOrThrow() => this.CreateChildren() ?? throw new InvalidOperationException("The children collection was not created.");
		protected virtual IChildControlCollection CreateChildren() => new ChildControls();

		protected override void RenderInternal(IRenderContext context) {
			this.RenderChildrenInternal(context);
		}

		protected virtual void RenderChildrenInternal(IRenderContext context) {
			foreach (var child in this.Children) {
				child.Render(context);
			}
		}
	}
}
