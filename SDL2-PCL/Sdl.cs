using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Allodium.SDL2.Core;
using Allodium.SDL2.Native;

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
			if (!object.ReferenceEquals(current, null) || !object.ReferenceEquals(current, this)) {
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

		private TResult ThrowIfSdlCallFails<TResult>(Func<TResult> tryFunction, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction();
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlCallFails<TArg0, TResult>(Func<TArg0, TResult> tryFunction, TArg0 arg0, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction(arg0);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		private TResult ThrowIfSdlCallFails<TArg0, TArg1, TResult>(Func<TArg0, TArg1, TResult> tryFunction, TArg0 arg0, TArg1 arg1, [CallerMemberName]string methodName = null) where TResult : class {
			var result = tryFunction(arg0, arg1);
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
			var result = this.TryCreateWindow(title, x, y, width, height, creationFlags);
			if (null != result) { return result; }

			throw SdlNativeException.CreateFromLastSdlError("CreateWindow: ");
		}
		#endregion CreateWindow

		#region CreateRenderer
		private const int DEFAULT_RENDERING_DRIVER_INDEX = -1;
		public SdlRenderer TryCreateRenderer(SdlWindow window, SdlRenderingFlags renderingFlags) => this.TryCreateRenderer(window, DEFAULT_RENDERING_DRIVER_INDEX, renderingFlags);
		public SdlRenderer TryCreateRenderer(SdlWindow window, int renderingDriverIndex, SdlRenderingFlags renderingFlags) {
			if (null == window) { throw new ArgumentNullException(nameof(window)); }

			var ptrWindow = window.GetValidHandle();
			var ptr = SDL.SDL_CreateRenderer(ptrWindow, renderingDriverIndex, (SDL.SDL_RendererFlags)(uint)renderingFlags);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlRenderer(ptr, true);
		}

		public SdlRenderer CreateRenderer(SdlWindow window, SdlRenderingFlags renderingFlags) => CreateRenderer(window, DEFAULT_RENDERING_DRIVER_INDEX, renderingFlags);
		public SdlRenderer CreateRenderer(SdlWindow window, int renderingDriverIndex, SdlRenderingFlags renderingFlags) {
			var result = TryCreateRenderer(window, renderingDriverIndex, renderingFlags);
			if (null != result) { return result; }

			throw SdlNativeException.CreateFromLastSdlError("CreateRenderer: ");
		}
		#endregion CreateRenderer

		#region LoadBMP
		public SdlSurface TryLoadBMP(string filePath) {
			var ptr = SDL.SDL_LoadBMP(filePath);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlSurface(ptr, true);
		}

		public SdlSurface LoadBMP(string filePath) {
			var result = this.TryLoadBMP(filePath);
			if (null != result) { return result; }

			throw SdlNativeException.CreateFromLastSdlError("LoadBMP: ");
		}
		#endregion LoadBMP

		#region CreateTextureFromSurface
		public SdlTexture TryCreateTextureFromSurface(SdlRenderer renderer, SdlSurface surface) {
			if (null == renderer) { throw new ArgumentNullException(nameof(renderer)); }
			if (null == surface) { throw new ArgumentNullException(nameof(surface)); }

			var ptrRenderer = renderer.GetValidHandle();
			var ptrSurface = surface.GetValidHandle();

			var ptr = SDL.SDL_CreateTextureFromSurface(ptrRenderer, ptrSurface);
			if (IntPtr.Zero == ptr) { return null; }

			return new SdlTexture(ptr, true);
		}

		public SdlTexture CreateTextureFromSurface(SdlRenderer renderer, SdlSurface surface) => ThrowIfSdlCallFails(this.TryCreateTextureFromSurface, renderer, surface);
		#endregion CreateTextureFromSurface

		#region EXT - CreateTextureFromBMP
		public SdlTexture TryCreateTextureFromBMP(SdlRenderer renderer, string filePath) {
			var bmp = TryLoadBMP(filePath);
			if (null == bmp) { return null; }

			using (bmp) {
				return TryCreateTextureFromSurface(renderer, bmp);
			}
		}
		public SdlTexture CreateTextureFromBMP(SdlRenderer renderer, string filePath) => ThrowIfSdlCallFails(this.TryCreateTextureFromBMP, renderer, filePath);
		#endregion EXT - CreateTextureFromBMP

		#region RenderClear
		public int TryRenderClear(SdlRenderer renderer) {
			if (null == renderer) { throw new ArgumentNullException(nameof(renderer)); }

			var ptr = renderer.GetValidHandle();
			var result = SDL.SDL_RenderClear(ptr);
			return result;
		}
		public void RenderClear(SdlRenderer renderer) => this.ThrowIfSdlCallFails(TryRenderClear, renderer);
		#endregion RenderClear
	}
}
