using System;
using System.Collections.Generic;
using System.Text;

namespace UiQuick {
	public class Control : IControl, IChildControl {
		#region IControl
		public virtual void Render(IRenderContext context) {
			if (context is null) { throw new ArgumentNullException(nameof(context)); }

			var beforeEvent = this.beforeRender;
			var afterEvent = this.afterRender;

			if (beforeRender is null && afterRender is null) {
				this.RenderInternal(context);
			}
			else {
				var args = new RenderEventArgs(context);
				beforeEvent?.Invoke(this, args);
				this.RenderInternal(context);
				afterEvent?.Invoke(this, args);
			}
		}
		protected virtual void RenderInternal(IRenderContext context) {
			// do nothing ... good to override
		}

		protected event EventHandler<RenderEventArgs> beforeRender;
		public event EventHandler<RenderEventArgs> BeforeRender {
			add { this.beforeRender += value; }
			remove { this.beforeRender -= value; }
		}

		protected event EventHandler<RenderEventArgs> afterRender;
		public event EventHandler<RenderEventArgs> AfterRender {
			add { this.afterRender += value; }
			remove { this.afterRender -= value; }
		}
		#endregion IControl

		#region IChild
		private IChildControlCollection parentCollection;
		public IChildControlCollection ParentCollection {
			get => this.parentCollection;
			private set {
				this.RemoveFromParent(this.parentCollection);
				this.parentCollection = value;
				value?.Add(this);
			}
		}

		public bool RemoveFromParent() {
			return this.RemoveFromParent(this.parentCollection);
		}
		public bool RemoveFromParent(IChildControlCollection parentCollection) {
			var currParentCollection = this.parentCollection;
			if (!object.ReferenceEquals(currParentCollection, parentCollection)) {
				return false;
			}

			parentCollection.Remove(this);
			this.parentCollection = null;
			return true;
		}
		#endregion IChild
	}
}
