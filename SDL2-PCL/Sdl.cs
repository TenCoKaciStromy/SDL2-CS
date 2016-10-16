using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}
