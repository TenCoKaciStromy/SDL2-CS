using System;
using System.Collections.Generic;
using System.Text;
using UiQuick.Common;
using UiQuick.Drawing;

namespace UiQuick {
	public class Control : IControl, IChildControl, IHasBackground, IHasForeground {
		#region IControl
		public virtual void Render(IRenderContext context) {
			if (context is null) { throw new ArgumentNullException(nameof(context)); }

			this.beforeRender?.Invoke(this, context);
			this.RenderInternal(context);
			this.afterRender?.Invoke(this, context);
		}
		protected virtual void RenderInternal(IRenderContext context) {
			this.RenderInternalBackground(context);
			this.RenderInternalContent(context);
			this.RenderInternalForeground(context);
		}
		protected virtual void RenderInternalBackground(IRenderContext context) {
			this.beforeBackgroundRender?.Invoke(this, context);
			this.background?.Render(context);
			this.afterBackgroundRender?.Invoke(this, context);
		}
		protected virtual void RenderInternalContent(IRenderContext context) {
			// do nothing ... good to override
		}
		protected virtual void RenderInternalForeground(IRenderContext context) {
			this.beforeForegroundRender?.Invoke(this, context);
			this.foreground?.Render(context);
			this.afterForegroundRender?.Invoke(this, context);
		}

		protected event RenderDelegate beforeRender;
		public event RenderDelegate BeforeRender {
			add { this.beforeRender += value; }
			remove { this.beforeRender -= value; }
		}

		protected event RenderDelegate afterRender;
		public event RenderDelegate AfterRender {
			add { this.afterRender += value; }
			remove { this.afterRender -= value; }
		}
		#endregion IControl

		#region IChildControl
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
		#endregion IChildControl

		#region IHasBackground
		private IRenderable background;
		public IRenderable Background {
			get => this.background;
			set => ComponentHelper.SetProperty(this, ref this.background, value, this.BeforeBackgroundChange, this.AfterBackgroundChange);
		}
		public event EventHandler BeforeBackgroundChange;
		public event EventHandler AfterBackgroundChange;

		protected event RenderDelegate beforeBackgroundRender;
		public event RenderDelegate BeforeBackgroundRender;

		protected event RenderDelegate afterBackgroundRender;
		public event RenderDelegate AfterBackgroundRender;
		#endregion IHasBackground

		#region IHasForeground
		private IRenderable foreground;
		public IRenderable Foreground {
			get => this.foreground;
			set => ComponentHelper.SetProperty(this, ref this.foreground, value, this.BeforeForegroundChange, this.AfterForegroundChange);
		}
		public event EventHandler BeforeForegroundChange;
		public event EventHandler AfterForegroundChange;

		protected event RenderDelegate beforeForegroundRender;
		public event RenderDelegate BeforeForegroundRender {
			add => this.beforeForegroundRender += value;
			remove => this.beforeForegroundRender -= value;
		}

		protected event RenderDelegate afterForegroundRender;
		public event RenderDelegate AfterForegroundRender {
			add => this.afterForegroundRender += value;
			remove => this.afterForegroundRender -= value;
		}
		#endregion IHasForeground
	}
}
