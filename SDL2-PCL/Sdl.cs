using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Core;
using Allodium.SDL2.Native;
using static Allodium.SDL2.Native.SDL;

namespace Allodium.SDL2 {
	/// <summary>
	/// Ensures access to SDL functionality.
	/// </summary>
	public sealed partial class Sdl : IDisposable {
		#region Singleton
		public static Sdl Singleton { get; private set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Private constructor.
		/// Use factory methods instead of constructors!
		/// </summary>
		private Sdl() {
			var current = Singleton;
			if (!object.ReferenceEquals(current, null) && !object.ReferenceEquals(current, this)) {
				throw new InvalidOperationException("The SdlEnvironment is already created. Use the static property 'Singleton'.");
			}
		}
		#endregion Constructors

		#region Initialization
		public static void Initialize(SdlInitializationFlags initFlags) {
			var current = Singleton;
			if (!object.ReferenceEquals(current, null)) {
				throw new InvalidOperationException("The SdlEnvironment is already created. Use the static property 'Singleton'.");
			}

			lock (typeof(Sdl)) {
				current = Singleton;
				if (!object.ReferenceEquals(current, null)) {
					throw new InvalidOperationException("The SdlEnvironment is already created. Use the static property 'Singleton'.");
				}

				var rslt = SDL.SDL_Init((uint)initFlags);
				if (0 != rslt) {
					throw SdlNativeException.CreateFromLastSdlError("SDL_Init: ");
				}

				var obj = new Sdl();
				Singleton = obj;
			}
		}
		#endregion Initialization

		#region Disposing
		private bool isDisposed;
		public void Dispose() {
			if (this.isDisposed) { return; }

			lock (this) {
				if (this.isDisposed) { return; }

				SDL.SDL_Quit();
				this.isDisposed = true;
			}
		}
		#endregion Disposing
	}

	partial class Sdl {
		#region --- native call helpers ---
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

		private TResult ThrowIfSdlFuncFails<TResult>(Func<TResult> tryFunction, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction();
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TResult>(Func<TArg0, TResult> tryFunction, TArg0 arg0, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction(arg0);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TResult>(Func<TArg0, TArg1, TResult> tryFunction, TArg0 arg0, TArg1 arg1, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction(arg0, arg1);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TResult>(Func<TArg0, TArg1, TArg2, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction(arg0, arg1, arg2);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction(arg0, arg1, arg2, arg3);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction(arg0, arg1, arg2, arg3, arg4);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction(arg0, arg1, arg2, arg3, arg4, arg5);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		#endregion --- native call helpers ---

		#region CreateWindow
		public SdlWindow TryCreateWindow(string title, int x, int y, int width, int height, SdlWindowCreationFlags creationFlags) {
			var ptr = SDL.SDL_CreateWindow(title, x, y, width, height, (SDL.SDL_WindowFlags)(uint)creationFlags);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlWindow(ptr, true);
		}

		public SdlWindow CreateWindow(string title, int x, int y, int width, int height, SdlWindowCreationFlags creationFlags) {
			return this.ThrowIfSdlFuncFails(this.TryCreateWindow, title, x, y, width, height, creationFlags);
		}
		#endregion CreateWindow

		#region CreateRenderer
		private const int DEFAULT_RENDERING_DRIVER_INDEX = -1;
		public SdlRenderer TryCreateRenderer(SdlWindow window, SdlRenderingFlags renderingFlags) => this.TryCreateRenderer(window, DEFAULT_RENDERING_DRIVER_INDEX, renderingFlags);
		public SdlRenderer TryCreateRenderer(SdlWindow window, int renderingDriverIndex, SdlRenderingFlags renderingFlags) {
			if (null == window) { throw new ArgumentNullException(nameof(window)); }

			var ptrWindow = window.GetValidPointer();
			var ptr = SDL.SDL_CreateRenderer(ptrWindow, renderingDriverIndex, (SDL.SDL_RendererFlags)(uint)renderingFlags);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlRenderer(ptr, true);
		}

		public SdlRenderer CreateRenderer(SdlWindow window, SdlRenderingFlags renderingFlags) {
			return this.ThrowIfSdlFuncFails(this.TryCreateRenderer, window, renderingFlags);
		}
		public SdlRenderer CreateRenderer(SdlWindow window, int renderingDriverIndex, SdlRenderingFlags renderingFlags) {
			return this.ThrowIfSdlFuncFails(this.TryCreateRenderer, window, renderingDriverIndex, renderingFlags);
		}
		#endregion CreateRenderer

		#region LoadBMP
		public SdlSurface TryLoadBMP(string filePath) {
			var ptr = SDL.SDL_LoadBMP(filePath);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlSurface(ptr, true);
		}

		public SdlSurface LoadBMP(string filePath) => ThrowIfSdlFuncFails(this.TryLoadBMP, filePath);
		#endregion LoadBMP

		#region CreateTextureFromSurface
		public SdlTexture TryCreateTextureFromSurface(SdlRenderer renderer, SdlSurface surface) {
			if (null == renderer) { throw new ArgumentNullException(nameof(renderer)); }
			if (null == surface) { throw new ArgumentNullException(nameof(surface)); }

			var ptrRenderer = renderer.GetValidPointer();
			var ptrSurface = surface.GetValidPointer();

			var ptr = SDL.SDL_CreateTextureFromSurface(ptrRenderer, ptrSurface);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlTexture(ptr, true);
		}

		public SdlTexture CreateTextureFromSurface(SdlRenderer renderer, SdlSurface surface) => ThrowIfSdlFuncFails(this.TryCreateTextureFromSurface, renderer, surface);
		#endregion CreateTextureFromSurface

		#region EXT - CreateTextureFromBMP
		public SdlTexture TryCreateTextureFromBMP(SdlRenderer renderer, string filePath) {
			var bmp = TryLoadBMP(filePath);
			if (null == bmp) { return null; }

			using (bmp) {
				return TryCreateTextureFromSurface(renderer, bmp);
			}
		}
		public SdlTexture CreateTextureFromBMP(SdlRenderer renderer, string filePath) => ThrowIfSdlFuncFails(this.TryCreateTextureFromBMP, renderer, filePath);
		#endregion EXT - CreateTextureFromBMP

		#region RenderClear
		public int TryRenderClear(SdlRenderer renderer) {
			if (null == renderer) { throw new ArgumentNullException(nameof(renderer)); }

			var ptr = renderer.GetValidPointer();
			var result = SDL.SDL_RenderClear(ptr);
			return result;
		}
		public void RenderClear(SdlRenderer renderer) => this.ThrowIfSdlCallFails(TryRenderClear, renderer);
		#endregion RenderClear

		#region RenderCopy
		public int TryRenderCopy(SdlRenderer renderer, SdlTexture texture) => this.TryRenderCopy(renderer, texture, sourceRect: null, destinationRect: null);
		public int TryRenderCopy(SdlRenderer renderer, SdlTexture texture, SdlRect? sourceRect, SdlRect? destinationRect) {
			if (object.ReferenceEquals(renderer, null)) { throw new ArgumentNullException(nameof(renderer)); }
			if (object.ReferenceEquals(texture, null)) { throw new ArgumentNullException(nameof(texture)); }

			var ptrRenderer = renderer.GetValidPointer();
			var ptrTexture = texture.GetValidPointer();

			int result;
			if (sourceRect.HasValue && destinationRect.HasValue) {
				var sdlSrcRect = (SDL_Rect)sourceRect.Value;
				var sdlDstRect = (SDL_Rect)destinationRect.Value;
				result = SDL.SDL_RenderCopy(ptrRenderer, ptrTexture, ref sdlSrcRect, ref sdlDstRect);
			}
			else if (sourceRect.HasValue && !destinationRect.HasValue) {
				var sdlSrcRect = (SDL_Rect)sourceRect.Value;
				result = SDL.SDL_RenderCopy(ptrRenderer, ptrTexture, ref sdlSrcRect, IntPtr.Zero);
			}
			else if (!sourceRect.HasValue && destinationRect.HasValue) {
				var sdlDstRect = (SDL_Rect)destinationRect.Value;
				result = SDL.SDL_RenderCopy(ptrRenderer, ptrTexture, IntPtr.Zero, ref sdlDstRect);
			}
			else if (!sourceRect.HasValue && !destinationRect.HasValue) {
				result = SDL.SDL_RenderCopy(ptrRenderer, ptrTexture, IntPtr.Zero, IntPtr.Zero);
			}
			else {
				throw new NotImplementedException("The combination of parameters is not implemented.");
			}

			return result;
		}

		public void RenderCopy(SdlRenderer renderer, SdlTexture texture) {
			this.ThrowIfSdlCallFails(this.TryRenderCopy, renderer, texture);
		}
		public void RenderCopy(SdlRenderer renderer, SdlTexture texture, SdlRect? sourceRect, SdlRect? destinationRect) {
			this.ThrowIfSdlCallFails(this.TryRenderCopy, renderer, texture, sourceRect, destinationRect);
		}
		#endregion RenderCopy

		#region RenderPresent
		public int TryRenderPresent(SdlRenderer renderer) {
			if (null == renderer) { throw new ArgumentNullException(nameof(renderer)); }

			var ptr = renderer.GetValidPointer();
			SDL.SDL_RenderPresent(ptr);
			return 0;
		}

		public void RenderPresent(SdlRenderer renderer) => this.ThrowIfSdlCallFails(this.TryRenderPresent, renderer);
		#endregion RenderPresent

		#region Delay
		public int TryDelay(TimeSpan timeToDelay) => this.TryDelay((uint)timeToDelay.TotalMilliseconds);
		public int TryDelay(uint milliseconds) {
			SDL.SDL_Delay(milliseconds);
			return 0;
		}

		public void Delay(TimeSpan timeToDelay) => this.ThrowIfSdlCallFails(this.TryDelay, timeToDelay);
		public void Delay(uint milliseconds) => this.ThrowIfSdlCallFails(this.TryDelay, milliseconds);
		#endregion Delay
	}

	partial class Sdl {
		#region BlitSurface
		public int TryBlitSurface(SdlSurface source, SdlSurface destination) {
			return this.TryBlitSurface(source: source, sourceRect: null, destination: destination, destinationRect: null);
		}
		public int TryBlitSurface(SdlSurface source, SdlSurface destination, SdlRect? destinationRect) {
			return this.TryBlitSurface(source: source, sourceRect: null, destination: destination, destinationRect: destinationRect);
		}
		public int TryBlitSurface(SdlSurface source, SdlRect? sourceRect, SdlSurface destination) {
			return this.TryBlitSurface(source: source, sourceRect: sourceRect, destination: destination, destinationRect: null);
		}
		public int TryBlitSurface(SdlSurface source, SdlRect? sourceRect, SdlSurface destination, SdlRect? destinationRect) {
			if (null == source) { throw new ArgumentNullException(nameof(source)); }
			if (null == destination) { throw new ArgumentNullException(nameof(destination)); }

			var ptrSource = source.GetValidPointer();
			var ptrDestination = source.GetValidPointer();

			int result;
			if (sourceRect.HasValue && destinationRect.HasValue) {
				var sdlSrcRect = (SDL_Rect)sourceRect.Value;
				var sdlDstRect = (SDL_Rect)destinationRect.Value;
				result = SDL.SDL_BlitSurface(ptrSource, ref sdlSrcRect, ptrDestination, ref sdlDstRect);
			}
			else if (sourceRect.HasValue && !destinationRect.HasValue) {
				var sdlSrcRect = (SDL_Rect)sourceRect.Value;
				result = SDL.SDL_BlitSurface(ptrSource, ref sdlSrcRect, ptrDestination, IntPtr.Zero);
			}
			else if (!sourceRect.HasValue && destinationRect.HasValue) {
				var sdlDstRect = (SDL_Rect)destinationRect.Value;
				result = SDL.SDL_BlitSurface(ptrSource, IntPtr.Zero, ptrDestination, ref sdlDstRect);
			}
			else if (!sourceRect.HasValue && !destinationRect.HasValue) {
				result = SDL.SDL_BlitSurface(ptrSource, IntPtr.Zero, ptrDestination, IntPtr.Zero);
			}
			else {
				throw new NotImplementedException("Processing the passed combination of parameters is not implemented.");
			}

			return result;
		}

		public void BlitSurface(SdlSurface source, SdlSurface destination) => this.ThrowIfSdlCallFails(this.TryBlitSurface, source, destination);
		public void BlitSurface(SdlSurface source, SdlSurface destination, SdlRect? destinationRect) => this.ThrowIfSdlCallFails(this.TryBlitSurface, source, destination, destinationRect);
		public void BlitSurface(SdlSurface source, SdlRect? sourceRect, SdlSurface destination) => this.ThrowIfSdlCallFails(this.TryBlitSurface, source, sourceRect, destination);
		public void BlitSurface(SdlSurface source, SdlRect? sourceRect, SdlSurface destination, SdlRect? destinationRect) => this.ThrowIfSdlCallFails(this.TryBlitSurface, source, sourceRect, destination, destinationRect);
		#endregion BlitSurface

		#region BlitScaled
		public int TryBlitScaled(SdlSurface source, SdlSurface destination) {
			return this.TryBlitScaled(source: source, sourceRect: null, destination: destination, destinationRect: null);
		}
		public int TryBlitScaled(SdlSurface source, SdlSurface destination, SdlRect? destinationRect) {
			return this.TryBlitScaled(source: source, sourceRect: null, destination: destination, destinationRect: destinationRect);
		}
		public int TryBlitScaled(SdlSurface source, SdlRect? sourceRect, SdlSurface destination) {
			return this.TryBlitScaled(source: source, sourceRect: sourceRect, destination: destination, destinationRect: null);
		}
		public int TryBlitScaled(SdlSurface source, SdlRect? sourceRect, SdlSurface destination, SdlRect? destinationRect) {
			if (null == source) { throw new ArgumentNullException(nameof(source)); }
			if (null == destination) { throw new ArgumentNullException(nameof(destination)); }

			var ptrSource = source.GetValidPointer();
			var ptrDestination = source.GetValidPointer();

			int result;
			if (sourceRect.HasValue && destinationRect.HasValue) {
				var sdlSrcRect = (SDL_Rect)sourceRect.Value;
				var sdlDstRect = (SDL_Rect)destinationRect.Value;
				result = SDL.SDL_BlitScaled(ptrSource, ref sdlSrcRect, ptrDestination, ref sdlDstRect);
			}
			else if (sourceRect.HasValue && !destinationRect.HasValue) {
				var sdlSrcRect = (SDL_Rect)sourceRect.Value;
				result = SDL.SDL_BlitScaled(ptrSource, ref sdlSrcRect, ptrDestination, IntPtr.Zero);
			}
			else if (!sourceRect.HasValue && destinationRect.HasValue) {
				var sdlDstRect = (SDL_Rect)destinationRect.Value;
				result = SDL.SDL_BlitScaled(ptrSource, IntPtr.Zero, ptrDestination, ref sdlDstRect);
			}
			else if (!sourceRect.HasValue && !destinationRect.HasValue) {
				result = SDL.SDL_BlitScaled(ptrSource, IntPtr.Zero, ptrDestination, IntPtr.Zero);
			}
			else {
				throw new NotImplementedException("Processing the passed combination of parameters is not implemented.");
			}

			return result;
		}

		public void BlitScaled(SdlSurface source, SdlSurface destination) => this.ThrowIfSdlCallFails(this.TryBlitScaled, source, destination);
		public void BlitScaled(SdlSurface source, SdlSurface destination, SdlRect? destinationRect) => this.ThrowIfSdlCallFails(this.TryBlitScaled, source, destination, destinationRect);
		public void BlitScaled(SdlSurface source, SdlRect? sourceRect, SdlSurface destination) => this.ThrowIfSdlCallFails(this.TryBlitScaled, source, sourceRect, destination);
		public void BlitScaled(SdlSurface source, SdlRect? sourceRect, SdlSurface destination, SdlRect? destinationRect) => this.ThrowIfSdlCallFails(this.TryBlitScaled, source, sourceRect, destination, destinationRect);
		#endregion BlitScaled

		#region ClearError
		public int TryClearError() {
			SDL.SDL_ClearError();
			return 0;
		}
		public void ClearError() => this.ThrowIfSdlCallFails(this.TryClearError);
		#endregion ClearError

		#region ClearHints
		public int TryClearHints() {
			SDL.SDL_ClearHints();
			return 0;
		}
		public void ClearHints() => this.ThrowIfSdlCallFails(this.TryClearHints);
		#endregion ClearHints

		#region CreateColorCursor
		public SdlCursor TryCreateColorCursor(SdlSurface surface, int hotX, int hotY) {
			if (null == surface) { throw new ArgumentNullException(nameof(surface)); }

			var ptrSurface = surface.GetValidPointer();
			var result = SDL.SDL_CreateColorCursor(ptrSurface, hotX, hotY);
			if (IntPtr.Zero == result) { return null; }

			return new SdlCursor(result, true);
		}

		public SdlCursor CreateColorCursor(SdlSurface surface, int hotX, int hotY) => this.ThrowIfSdlFuncFails(this.TryCreateColorCursor, surface, hotX, hotY);
		#endregion CreateColorCursor

		#region CreateRGBSurface
		public SdlSurface TryCreateRGBSurface(int width, int height, int depth, uint rMask, uint gMask, uint bMask, uint aMask) {
			var result = SDL.SDL_CreateRGBSurface(0, width, height, depth, rMask, gMask, bMask, aMask);
			if (IntPtr.Zero == result) { return null; }

			return new SdlSurface(result, true);
		}

		public SdlSurface CreateRGBSurface(int width, int height, int depth, uint rMask, uint gMask, uint bMask, uint aMask) {
			return this.ThrowIfSdlFuncFails(this.TryCreateRGBSurface, width, height, depth, rMask, gMask, bMask, aMask);
		}
		#endregion CreateRGBSurface

		#region CreateSystemCursor
		public SdlCursor TryCreateSystemCursor(SldSystemCursorKind cursorKind) {
			var result = SDL.SDL_CreateSystemCursor((SDL_SystemCursor)(int)cursorKind);
			if (IntPtr.Zero == result) { return null; }

			return new SdlCursor(result, true);
		}

		public SdlCursor CreateSystemCursor(SldSystemCursorKind cursorKind) {
			return this.ThrowIfSdlFuncFails(this.TryCreateSystemCursor, cursorKind);
		}
		#endregion CreateSystemCursor

		#region CreateWindowFrom
		public SdlWindow CreateWindowFrom(IntPtr windowPointer) {
			return this.CreateWindowFrom(windowPointer, false);
		}
		public SdlWindow CreateWindowFrom(IntPtr windowPointer, bool ownsHandle) {
			var result = SDL.SDL_CreateWindowFrom(windowPointer);
			if (IntPtr.Zero == result) { return null; }

			return new SdlWindow(result, ownsHandle);
		}
		#endregion CreateWindowFrom

		#region DisableScreenSaver
		public int TryDisableScreenSaver() {
			SDL.SDL_DisableScreenSaver();
			return 0;
		}
		public void DisableScreenSave() => this.ThrowIfSdlCallFails(this.TryDisableScreenSaver);
		#endregion DisableScreenSaver

		#region EnableScreenSaver
		public int TryEnableScreenSaver() {
			SDL.SDL_EnableScreenSaver();
			return 0;
		}
		public void EnableScreenSaver() => this.ThrowIfSdlCallFails(this.TryEnableScreenSaver);
		#endregion EnableScreenSaver

		#region FillRect
		public int TryFillRect(SdlSurface surface, SdlRect rect, uint color) {
			if (null == surface) { throw new ArgumentNullException(nameof(surface)); }

			var sdlRect = (SDL_Rect)rect;
			var ptrSurface = surface.GetValidPointer();
			var result = SDL.SDL_FillRect(ptrSurface, ref sdlRect, color);
			return result;
		}
		public void FillRect(SdlSurface surface, SdlRect rect, uint color) {
			this.ThrowIfSdlCallFails(this.TryFillRect, surface, rect, color);
		}
		#endregion FillRect

		//public object asdf() => SDL.SDL_FillRect
	}
}