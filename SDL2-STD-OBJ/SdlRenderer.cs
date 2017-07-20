using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ObjectiveSdl2.Core;
using ObjectiveSdl2.Core.SafeHandles;
using ObjectiveSdl2.Drawing;

namespace ObjectiveSdl2 {
	public sealed partial class SdlRenderer : SdlObject {
		public SdlRenderer(SafeHandle validHandle) : base(validHandle) { }
		public SdlRenderer(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle) => new SdlRendererSafeHandle(validHandle, ownsHandle);
	}

	partial class SdlRenderer {
		internal static SdlRenderer Create(IntPtr ptrWindow, SdlRenderingFlags renderingFlags) {
			return Create(ptrWindow, -1, renderingFlags);
		}
		internal static SdlRenderer Create(IntPtr ptrWindow, int index, SdlRenderingFlags renderingFlags) {
			var ptr = SDL2.SDL.SDL_CreateRenderer(ptrWindow, index, (SDL2.SDL.SDL_RendererFlags)(uint)renderingFlags);
			return new SdlRenderer(ptr, true);
		}
	}

	partial class SdlRenderer {
		internal static SdlRenderer Get(IntPtr ptrWindow) {
			var ptr = SDL2.SDL.SDL_GetRenderer(ptrWindow);
			if (ptr.IsZero()) {
				return null;
			}

			return new SdlRenderer(ptr, false);
		}
	}

	partial class SdlRenderer {
		#region Clear
		private static int TryClear(IntPtr ptrRenderer) {
			return SDL2.SDL.SDL_RenderClear(ptrRenderer);
		}
		public void Clear() {
			SdlCallUtil.ThrowIfSdlCallFails(TryClear, this.GetPointer());
		}
		#endregion Clear

		#region CopyFrom
		private static int TryCopyFrom(IntPtr ptrRenderer, IntPtr ptrTexture) {
			return SDL2.SDL.SDL_RenderCopy(ptrRenderer, ptrTexture, IntPtr.Zero, IntPtr.Zero);
		}
		public void CopyFrom(SdlTexture texture) {
			var ptrTexture = ((ISdlObject)texture).GetValidHandle();
			SdlCallUtil.ThrowIfSdlCallFails(TryCopyFrom, this.GetPointer(), ptrTexture);
		}

		private static int TryCopyFrom(IntPtr ptrRenderer, IntPtr ptrTexture, SdlRect sourceRect) {
			var srcRect = (SDL2.SDL.SDL_Rect)sourceRect;
			return SDL2.SDL.SDL_RenderCopy(ptrRenderer, ptrTexture, ref srcRect, IntPtr.Zero);
		}
		public void CopyFrom(SdlTexture texture, SdlRect sourceRect) {
			var ptrTexture = ((ISdlObject)texture).GetValidHandle();
			SdlCallUtil.ThrowIfSdlCallFails(TryCopyFrom, this.GetPointer(), ptrTexture, sourceRect);
		}

		private static int TryCopyFrom(IntPtr ptrRenderer, SdlRect destinationRect, IntPtr ptrTexture) {
			var dstRect = (SDL2.SDL.SDL_Rect)destinationRect;
			return SDL2.SDL.SDL_RenderCopy(ptrRenderer, ptrTexture, IntPtr.Zero, ref dstRect);
		}
		public void CopyFrom(SdlRect destinationRect, SdlTexture texture) {
			var ptrTexture = ((ISdlObject)texture).GetValidHandle();
			SdlCallUtil.ThrowIfSdlCallFails(TryCopyFrom, this.GetPointer(), destinationRect, ptrTexture);
		}

		private static int TryCopyFrom(IntPtr ptrRenderer, SdlRect destinationRect, IntPtr ptrTexture, SdlRect sourceRect) {
			var dstRect = (SDL2.SDL.SDL_Rect)destinationRect;
			var srcRect = (SDL2.SDL.SDL_Rect)sourceRect;
			return SDL2.SDL.SDL_RenderCopy(ptrRenderer, ptrTexture, ref srcRect, ref dstRect);
		}
		public void CopyFrom(SdlRect destinationRect, SdlTexture texture, SdlRect sourceRect) {
			var ptrTexture = ((ISdlObject)texture).GetValidHandle();
			SdlCallUtil.ThrowIfSdlCallFails(TryCopyFrom, this.GetPointer(), destinationRect, ptrTexture, sourceRect);
		}
		#endregion CopyFrom

		#region Present
		private static void TryPresent(IntPtr ptrRenderer) {
			SDL2.SDL.SDL_RenderPresent(ptrRenderer);
		}
		public void Present() {
			SdlCallUtil.ThrowIfSdlActionFails(TryPresent, this.GetPointer());
		}
		#endregion Present
	}

	partial class SdlRenderer {
		#region CreateTexture
		private static SdlTexture TryCreateTextureFromSurface(IntPtr ptrRenderer, IntPtr ptrSurface) {
			var ptr = SDL2.SDL.SDL_CreateTextureFromSurface(ptrRenderer, ptrSurface);
			if (ptr.IsZero()) { return null; }

			return new SdlTexture(ptr, true);
		}
		public SdlTexture CreateTexture(SdlSurface surface) {
			var ptrSurface = ((ISdlObject)surface).GetValidHandle();
			return SdlCallUtil.ThrowIfSdlFuncFails(TryCreateTextureFromSurface, this.GetPointer(), ptrSurface);
		}
		#endregion CreateTexture
	}
}
