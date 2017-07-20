using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ObjectiveSdl2.Core;

namespace ObjectiveSdl2.Drawing {
	public sealed partial class SdlSurface : SdlObject {
		public SdlSurface(SafeHandle validHandle) : base(validHandle) { }
		public SdlSurface(IntPtr validHandle, bool ownsHandle) : base(validHandle, ownsHandle) { }

		protected override SafeHandle CreateSdlSafeHandle(IntPtr validHandle, bool ownsHandle) => throw new NotImplementedException();
	}

	partial class SdlSurface {
		private static SdlSurface TryLoadFromBmp(string filePath) {
			var ptr = SDL2.SDL.SDL_LoadBMP(filePath);
			if (ptr.IsZero()) { return null; }

			return new SdlSurface(ptr, true);
		}

		public static SdlSurface LoadFromBmpFile(string filePath) {
			return SdlCallUtil.ThrowIfSdlFuncFails(TryLoadFromBmp, filePath);
		}
	}

	partial class SdlSurface {

	}

	partial class SdlSurface {
		#region BlitFrom
		private static int TryBlitFrom(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination, SdlRect destinationRect) {
			var srcRect = (SDL2.SDL.SDL_Rect)sourceRect;
			var dstRect = (SDL2.SDL.SDL_Rect)destinationRect;
			return SDL2.SDL.SDL_BlitSurface(ptrSource, ref srcRect, ptrDestination, ref dstRect);
		}
		public void BlitFrom(SdlRect destinationRect, SdlSurface source, SdlRect sourceRect) {
			var ptrSource = source.GetPointer();
			SdlCallUtil.ThrowIfSdlCallFails(TryBlitFrom, ptrSource, sourceRect, this.GetPointer(), destinationRect);
		}

		private static int TryBlitFrom(IntPtr ptrSource, IntPtr ptrDestination, SdlRect destinationRect) {
			var dstRect = (SDL2.SDL.SDL_Rect)destinationRect;
			return SDL2.SDL.SDL_BlitSurface(ptrSource, IntPtr.Zero, ptrDestination, ref dstRect);
		}
		public void BlitFrom(SdlRect destinationRect, SdlSurface source) {
			var ptrSource = source.GetPointer();
			SdlCallUtil.ThrowIfSdlCallFails(TryBlitFrom, ptrSource, this.GetPointer(), destinationRect);
		}

		private static int TryBlitFrom(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination) {
			var srcRect = (SDL2.SDL.SDL_Rect)sourceRect;
			return SDL2.SDL.SDL_BlitSurface(ptrSource, ref srcRect, ptrDestination, IntPtr.Zero);
		}
		public void BlitFrom(SdlSurface source, SdlRect sourceRect) {
			var ptrSource = source.GetPointer();
			SdlCallUtil.ThrowIfSdlCallFails(TryBlitFrom, ptrSource, sourceRect, this.GetPointer());
		}

		private static int TryBlitFrom(IntPtr ptrSource, IntPtr ptrDestination) {
			return SDL2.SDL.SDL_BlitSurface(ptrSource, IntPtr.Zero, ptrDestination, IntPtr.Zero);
		}
		public void BlitFrom(SdlSurface source) {
			var ptrSource = source.GetPointer();
			this.Sdl.BlitSurface(ptrSource, this.GetValidHandle());
		}
		#endregion BlitFrom

		#region BlitScaledFrom
		private static int TryBlitScaledFrom(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination, SdlRect destinationRect) {
			var srcRect = (SDL2.SDL.SDL_Rect)sourceRect;
			var dstRect = (SDL2.SDL.SDL_Rect)destinationRect;
			return SDL2.SDL.SDL_BlitScaled(ptrSource, ref srcRect, ptrDestination, ref dstRect);
		}
		public void BlitScaledFrom(SdlRect destinationRect, SdlSurface source, SdlRect sourceRect) {
			var ptrSource = source.GetPointer();
			SdlCallUtil.ThrowIfSdlCallFails(TryBlitScaledFrom, ptrSource, sourceRect, this.GetPointer(), destinationRect);
		}

		private static int TryBlitScaledFrom(IntPtr ptrSource, IntPtr ptrDestination, SdlRect destinationRect) {
			var dstRect = (SDL2.SDL.SDL_Rect)destinationRect;
			return SDL2.SDL.SDL_BlitScaled(ptrSource, IntPtr.Zero, ptrDestination, ref dstRect);
		}
		public void BlitScaledFrom(SdlRect destinationRect, SdlSurface source) {
			var ptrSource = source.GetPointer();
			SdlCallUtil.ThrowIfSdlCallFails(TryBlitScaledFrom, ptrSource, this.GetPointer(), destinationRect);
		}

		private static int TryBlitScaledFrom(IntPtr ptrSource, SdlRect sourceRect, IntPtr ptrDestination) {
			var srcRect = (SDL2.SDL.SDL_Rect)sourceRect;
			return SDL2.SDL.SDL_BlitScaled(ptrSource, ref srcRect, ptrDestination, IntPtr.Zero);
		}
		public void BlitScaledFrom(SdlSurface source, SdlRect sourceRect) {
			var ptrSource = source.GetPointer();
			SdlCallUtil.ThrowIfSdlCallFails(TryBlitScaledFrom, ptrSource, sourceRect, this.GetPointer());
		}

		private static int TryBlitScaledFrom(IntPtr ptrSource, IntPtr ptrDestination) {
			return SDL2.SDL.SDL_BlitScaled(ptrSource, IntPtr.Zero, ptrDestination, IntPtr.Zero);
		}
		public void BlitScaledFrom(SdlSurface source) {
			var ptrSource = source.GetPointer();
			SdlCallUtil.ThrowIfSdlCallFails(TryBlitScaledFrom, ptrSource, this.GetPointer());
		}
		#endregion BlitScaledFrom
	}
}
