using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ObjectiveSdl2.Core;
using ObjectiveSdl2.Drawing;
using static SDL2.SDL;

namespace ObjectiveSdl2 {
	public sealed partial class SdlContext { }

	#region Singleton
	partial class SdlContext {
		private readonly static SdlContext @default = new SdlContext();
		public static SdlContext Default => @default;
	}
	#endregion Singleton

	#region Init & Quit
	partial class SdlContext {
		private readonly object syncRoot = new object();
		public object SyncRoot => this.syncRoot;

		private bool isInitialized;
		public bool IsInitialized => this.isInitialized;

		public void Initialize(SdlInitializationFlags initFlags) {
			lock (this.syncRoot) {
				if (this.IsInitialized) {
					throw new InvalidOperationException("The SdlContext is already initalized.");
				}

				var rslt = SDL_Init((uint)initFlags);
				if (0 != rslt) {
					throw SdlNativeException.CreateFromLastSdlError();
				}
				else {
					this.isInitialized = true;
				}
			}
		}

		public void Quit() {
			lock (this.syncRoot) {
				if (this.IsInitialized) {
					SDL_Quit();
				}
			}
		}
	}
	#endregion Init & Quit

	#region Native call helpers
	partial class SdlContext {
		private void ThrowIfSdlCallFails(Func<int> tryFunction, [CallerMemberName]string methodName = null) {
			var resultCode = tryFunction();
			if (0 == resultCode) { return; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private void ThrowIfSdlCallFails<TArg0>(Func<TArg0, int> tryFunction, TArg0 arg0, [CallerMemberName]string methodName = null) {
			var resultCode = tryFunction(arg0);
			if (0 == resultCode) { return; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private void ThrowIfSdlCallFails<TArg0, TArg1>(Func<TArg0, TArg1, int> tryFunction, TArg0 arg0, TArg1 arg1, [CallerMemberName]string methodName = null) {
			var resultCode = tryFunction(arg0, arg1);
			if (0 == resultCode) { return; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private void ThrowIfSdlCallFails<TArg0, TArg1, TArg2>(Func<TArg0, TArg1, TArg2, int> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, [CallerMemberName]string methodName = null) {
			var resultCode = tryFunction(arg0, arg1, arg2);
			if (0 == resultCode) { return; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private void ThrowIfSdlCallFails<TArg0, TArg1, TArg2, TArg3>(Func<TArg0, TArg1, TArg2, TArg3, int> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, [CallerMemberName]string methodName = null) {
			var resultCode = tryFunction(arg0, arg1, arg2, arg3);
			if (0 == resultCode) { return; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}

		private TResult ThrowIfSdlFuncFails<TResult>(Func<TResult> tryFunction, [CallerMemberName]string methodName = null) {
			var result = tryFunction();
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TResult>(Func<TArg0, TResult> tryFunction, TArg0 arg0, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TResult>(Func<TArg0, TArg1, TResult> tryFunction, TArg0 arg0, TArg1 arg1, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TResult>(Func<TArg0, TArg1, TArg2, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1, arg2);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1, arg2, arg3);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1, arg2, arg3, arg4);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1, arg2, arg3, arg4, arg5);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
	}
	#endregion Native call helpers

	#region CreateWindow
	partial class SdlContext {
		public SdlWindow TryCreateWindow(string title, int x, int y, int width, int height, SdlWindowCreationFlags creationFlags) {
			var ptr = SDL_CreateWindow(title, x, y, width, height, (SDL_WindowFlags)(uint)creationFlags);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlWindow(ptr, true, title, new SdlVector(x, y), new SdlVector(width, height));
		}

		public SdlWindow CreateWindow(string title, int x, int y, int width, int height, SdlWindowCreationFlags creationFlags) {
			return this.ThrowIfSdlFuncFails(this.TryCreateWindow, title, x, y, width, height, creationFlags);
		}
	}
	#endregion CreateWindow

	#region GetWindowTitle
	partial class SdlContext {
		public string TryGetWindowTitle(SdlWindow window) {
			if (window is null) { throw new ArgumentNullException(nameof(window)); }

			var ptr = ((ISdlObject)window).GetValidHandle();
			var result = SDL_GetWindowTitle(ptr);
			return result;
		}
		public string GetWindowTitle(SdlWindow window) => this.ThrowIfSdlFuncFails(this.TryGetWindowTitle, window);
	}
	#endregion GetWindowTitle

	#region SetWindowTitle
	partial class SdlContext {
		public int TrySetWindowTitle(IntPtr ptrWindow, string title) {
			if (null == title) { throw new ArgumentNullException(nameof(title)); }

			SDL_SetWindowTitle(ptrWindow, title);
			return 0;
		}
		public void SetWindowTitle(IntPtr ptrWindow, string title) => this.ThrowIfSdlFuncFails(this.TrySetWindowTitle, ptrWindow, title);
	}
	#endregion SetWindowTitle

	#region WindowPosition
	partial class SdlContext {
		public SdlVector TryGetWindowPosition(IntPtr ptrWindow) {
			SDL_GetWindowPosition(ptrWindow, out var x, out var y);
			return new SdlVector(x, y);
		}

		public SdlVector GetWindowPosition(IntPtr ptrWindow) => this.ThrowIfSdlFuncFails(this.TryGetWindowPosition, ptrWindow);

		public int TrySetWindowPosition(IntPtr ptrWindow, SdlVector value) {
			SDL_SetWindowPosition(ptrWindow, value.X, value.Y);
			return 0;
		}

		public void SetWindowPosition(IntPtr ptrWindow, SdlVector value) => this.ThrowIfSdlCallFails(this.TrySetWindowPosition, ptrWindow, value);
	}
	#endregion WindowPosition

	#region WindowSize
	partial class SdlContext {
		public SdlVector TryGetWindowSize(IntPtr ptrWindow) {
			SDL_GetWindowSize(ptrWindow, out var w, out var h);
			return new SdlVector(w, h);
		}
		public SdlVector GetWindowSize(IntPtr ptrWindow) => this.ThrowIfSdlFuncFails(this.TryGetWindowSize, ptrWindow);

		public int TrySetWindowSize(IntPtr ptrWindow, SdlVector value) {
			SDL_SetWindowSize(ptrWindow, value.X, value.Y);
			return 0;
		}
		public void SetWindowSize(IntPtr ptrWindow, SdlVector value) => this.ThrowIfSdlCallFails(this.TrySetWindowSize, ptrWindow, value);
	}
	#endregion WindowSize

	#region ShowWindow
	partial class SdlContext {
		public int TryShowWindow(IntPtr ptrWindow) {
			SDL_ShowWindow(ptrWindow);
			return 0;
		}
		public void ShowWindow(IntPtr ptrWindow) {
			this.ThrowIfSdlCallFails(this.TryShowWindow, ptrWindow);
		}
	}
	#endregion ShowWindow

	#region RaiseWindow
	partial class SdlContext {
		public int TryRaiseWindow(IntPtr ptrWindow) {
			SDL_RaiseWindow(ptrWindow);
			return 0;
		}
		public void RaiseWindow(IntPtr ptrWindow) {
			this.ThrowIfSdlCallFails(this.TryRaiseWindow, ptrWindow);
		}
	}
	#endregion RaiseWindow


	#region CreateRenderer
	partial class SdlContext {
		private const int DEFAULT_RENDERING_DRIVER_INDEX = -1;
		public SdlRenderer TryCreateRenderer(IntPtr ptrWindow, SdlRenderingFlags renderingFlags) => this.TryCreateRenderer(ptrWindow, DEFAULT_RENDERING_DRIVER_INDEX, renderingFlags);
		public SdlRenderer TryCreateRenderer(IntPtr ptrWindow, int renderingDriverIndex, SdlRenderingFlags renderingFlags) {
			var ptr = SDL_CreateRenderer(ptrWindow, renderingDriverIndex, (SDL_RendererFlags)(uint)renderingFlags);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlRenderer(ptr, true);
		}

		public SdlRenderer CreateRenderer(IntPtr ptrWindow, SdlRenderingFlags renderingFlags) {
			return this.ThrowIfSdlFuncFails(this.TryCreateRenderer, ptrWindow, renderingFlags);
		}
		public SdlRenderer CreateRenderer(IntPtr ptrWindow, int renderingDriverIndex, SdlRenderingFlags renderingFlags) {
			return this.ThrowIfSdlFuncFails(this.TryCreateRenderer, ptrWindow, renderingDriverIndex, renderingFlags);
		}
	}
	#endregion CreateRenderer

	#region GetRenderer
	partial class SdlContext {
		public SdlRenderer TryGetRenderer(IntPtr ptrWindow) {
			var ptr = SDL_GetRenderer(ptrWindow);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlRenderer(ptr, false);
		}

		public SdlRenderer GetRenderer(IntPtr ptrWindow) {
			return this.ThrowIfSdlFuncFails(this.TryGetRenderer, ptrWindow);
		}
	}
	#endregion GetRenderer


	#region BlitSurface
	partial class SdlContext {
		public int TryBlitSurface(IntPtr ptrSource, IntPtr ptrDestination) {
			return SDL_BlitSurface(ptrSource, IntPtr.Zero, ptrDestination, IntPtr.Zero);
		}
		public void BlitSurface(IntPtr ptrSource, IntPtr ptrDestination) {
			this.ThrowIfSdlCallFails(this.TryBlitSurface, ptrSource, ptrDestination);
		}

		public int TryBlitSurface(IntPtr ptrSource, IntPtr ptrDestination, SdlRect destinationRect) {
			var sdlDstRect = (SDL_Rect)destinationRect;
			return SDL_BlitSurface(ptrSource, IntPtr.Zero, ptrDestination, ref sdlDstRect);
		}
		public void BlitSurface(IntPtr ptrSource, IntPtr ptrDestination, SdlRect destinationRect) {
			this.ThrowIfSdlCallFails(this.TryBlitSurface, ptrSource, ptrDestination, destinationRect);
		}

		public int TryBlitSurface(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination) {
			var sdlSrcRect = (SDL_Rect)sourceRect;
			return SDL_BlitSurface(ptrSource, ref sdlSrcRect, ptrDestination, IntPtr.Zero);
		}
		public void BlitSurface(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination) {
			this.ThrowIfSdlCallFails(this.TryBlitSurface, ptrSource, sourceRect, ptrDestination);
		}

		public int TryBlitSurface(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination, SdlRect destinationRect) {
			var sdlSrcRect = (SDL_Rect)sourceRect;
			var sdlDstRect = (SDL_Rect)destinationRect;
			return SDL_BlitSurface(ptrSource, ref sdlSrcRect, ptrDestination, ref sdlDstRect);
		}
		public void BlitSurface(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination, SdlRect destinationRect) {
			this.ThrowIfSdlCallFails(this.TryBlitSurface, ptrSource, sourceRect, ptrDestination, destinationRect);
		}
	}
	#endregion BlitSurface

	#region BlitScaled
	partial class SdlContext {
		public int TryBlitScaled(IntPtr ptrSource, IntPtr ptrDestination) {
			return SDL_BlitScaled(ptrSource, IntPtr.Zero, ptrDestination, IntPtr.Zero);
		}
		public int TryBlitScaled(IntPtr ptrSource, IntPtr ptrDestination, SdlRect destinationRect) {
			var sdlDstRect = (SDL_Rect)destinationRect;
			return SDL_BlitScaled(ptrSource, IntPtr.Zero, ptrDestination, ref sdlDstRect);
		}
		public int TryBlitScaled(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination) {
			var sdlSrcRect = (SDL_Rect)sourceRect;
			return SDL_BlitScaled(ptrSource, ref sdlSrcRect, ptrDestination, IntPtr.Zero);
		}
		public int TryBlitScaled(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination, SdlRect destinationRect) {
			var sdlSrcRect = (SDL_Rect)sourceRect;
			var sdlDstRect = (SDL_Rect)destinationRect;
			return SDL_BlitScaled(ptrSource, ref sdlSrcRect, ptrDestination, ref sdlDstRect);
		}

		public void BlitScaled(IntPtr ptrSource, IntPtr ptrDestination) => this.ThrowIfSdlCallFails(this.TryBlitScaled, ptrSource, ptrDestination);
		public void BlitScaled(IntPtr ptrSource, IntPtr ptrDestination, SdlRect destinationRect) => this.ThrowIfSdlCallFails(this.TryBlitScaled, ptrSource, ptrDestination, destinationRect);
		public void BlitScaled(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination) => this.ThrowIfSdlCallFails(this.TryBlitScaled, ptrSource, sourceRect, ptrDestination);
		public void BlitScaled(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination, SdlRect destinationRect) => this.ThrowIfSdlCallFails(this.TryBlitScaled, ptrSource, sourceRect, ptrDestination, destinationRect);
	}
	#endregion BlitScaled


	#region RenderClear
	partial class SdlContext {
		public int TryRenderClear(IntPtr ptrRenderer) {
			return SDL_RenderClear(ptrRenderer);
		}
		public void RenderClear(IntPtr ptrRenderer) => this.ThrowIfSdlCallFails(TryRenderClear, ptrRenderer);
	}
	#endregion RenderClear

	#region RenderCopy
	partial class SdlContext {
		public int TryRenderCopy(IntPtr ptrRenderer, IntPtr ptrTexture) => this.TryRenderCopy(ptrRenderer, ptrTexture, sourceRect: null, destinationRect: null);
		public int TryRenderCopy(IntPtr ptrRenderer, IntPtr ptrTexture, SdlRect? sourceRect, SdlRect? destinationRect) {
			int result;
			if (sourceRect.HasValue && destinationRect.HasValue) {
				var sdlSrcRect = (SDL_Rect)sourceRect.Value;
				var sdlDstRect = (SDL_Rect)destinationRect.Value;
				result = SDL_RenderCopy(ptrRenderer, ptrTexture, ref sdlSrcRect, ref sdlDstRect);
			}
			else if (sourceRect.HasValue && !destinationRect.HasValue) {
				var sdlSrcRect = (SDL_Rect)sourceRect.Value;
				result = SDL_RenderCopy(ptrRenderer, ptrTexture, ref sdlSrcRect, IntPtr.Zero);
			}
			else if (!sourceRect.HasValue && destinationRect.HasValue) {
				var sdlDstRect = (SDL_Rect)destinationRect.Value;
				result = SDL_RenderCopy(ptrRenderer, ptrTexture, IntPtr.Zero, ref sdlDstRect);
			}
			else if (!sourceRect.HasValue && !destinationRect.HasValue) {
				result = SDL_RenderCopy(ptrRenderer, ptrTexture, IntPtr.Zero, IntPtr.Zero);
			}
			else {
				throw new NotImplementedException("The combination of parameters is not implemented.");
			}

			return result;
		}

		public void RenderCopy(IntPtr ptrRenderer, IntPtr ptrTexture) {
			this.ThrowIfSdlCallFails(this.TryRenderCopy, ptrRenderer, ptrTexture);
		}
		public void RenderCopy(IntPtr ptrRenderer, IntPtr ptrTexture, SdlRect? sourceRect, SdlRect? destinationRect) {
			this.ThrowIfSdlCallFails(this.TryRenderCopy, ptrRenderer, ptrTexture, sourceRect, destinationRect);
		}
	}
	#endregion RenderCopy

	#region RenderPresent
	partial class SdlContext {
		public int TryRenderPresent(IntPtr ptrRenderer) {
			SDL_RenderPresent(ptrRenderer);
			return 0;
		}

		public void RenderPresent(IntPtr ptrRenderer) => this.ThrowIfSdlCallFails(this.TryRenderPresent, ptrRenderer);
	}
	#endregion RenderPresent


	#region CreateTextureFromSufrace
	partial class SdlContext {
		public SdlTexture TryCreateTextureFromSufrace(IntPtr ptrRenderer, IntPtr ptrSurface) {
			var ptr = SDL_CreateTextureFromSurface(ptrRenderer, ptrSurface);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlTexture(ptr, true);
		}

		public SdlTexture CreateTextureFromSufrace(IntPtr ptrRenderer, IntPtr ptrSurface) {
			return this.ThrowIfSdlFuncFails(this.TryCreateTextureFromSufrace, ptrRenderer, ptrSurface);
		}
	}
	#endregion CreateTextureFromSufrace

	#region QueryTexture
	partial class SdlContext {
		public SdlTextureInfo? TryQueryTexture(IntPtr ptrTexture) {
			uint format;
			int access, width, height;
			var result = SDL_QueryTexture(ptrTexture, out format, out access, out width, out height);
			if (0 != result) { return null; }

			return new SdlTextureInfo {
				Format = format,
				Access = access,
				Width = width,
				Height = height
			};
		}
		public SdlTextureInfo QueryTexture(IntPtr ptrTexture) {
			return this.ThrowIfSdlFuncFails(this.TryQueryTexture, ptrTexture).Value;
		}
	}
	#endregion QueryTexture
}
