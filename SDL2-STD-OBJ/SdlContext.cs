﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ObjectiveSdl2.Core;
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
}
