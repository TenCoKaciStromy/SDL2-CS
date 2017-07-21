using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveSdl2 {
	internal static class NativeExtensions {
		public static bool IsZero(this IntPtr self) => IntPtr.Zero == self;

		public static SDL2.SDL.SDL_Point[] ToNativePoints(this SdlVector[] points) {
			var result = new SDL2.SDL.SDL_Point[points.Length];
			for (var i = 0; i < result.Length; i++) {
				result[i] = (SDL2.SDL.SDL_Point)points[i];
			}

			return result;
		}

		public static SDL2.SDL.SDL_Point[] ToNativePoints(this IList<SdlVector> points) {
			var result = new SDL2.SDL.SDL_Point[points.Count];
			for (var i = 0; i < result.Length; i++) {
				result[i] = (SDL2.SDL.SDL_Point)points[i];
			}

			return result;
		}

		public static SDL2.SDL.SDL_Rect[] ToNativeRects(this SdlRect[] rects) {
			var result = new SDL2.SDL.SDL_Rect[rects.Length];
			for (var i = 0; i < result.Length; i++) {
				result[i] = (SDL2.SDL.SDL_Rect)rects[i];
			}

			return result;
		}

		public static SDL2.SDL.SDL_Rect[] ToNativeRects(this IList<SdlRect> rects) {
			var result = new SDL2.SDL.SDL_Rect[rects.Count];
			for (var i = 0; i < result.Length; i++) {
				result[i] = (SDL2.SDL.SDL_Rect)rects[i];
			}

			return result;
		}
	}
}
