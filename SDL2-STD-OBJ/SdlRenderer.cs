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

	partial class SdlRenderer {
		#region DrawColor
		private SdlRgba drawColor;
		public SdlRgba DrawColor {
			get => this.drawColor;
			set {
				this.drawColor = value;
				SdlCallUtil.ThrowIfSdlCallFails(TrySetDrawColor, this.GetPointer(), value.R, value.G, value.B, value.A);
			}
		}
		public SdlRgba DrawColorRefresh() => this.drawColor = this.GetDrawColor();

		private static int TrySetDrawColor(IntPtr ptrRenderer, byte r, byte g, byte b, byte a) {
			return SDL2.SDL.SDL_SetRenderDrawColor(ptrRenderer, r, g, b, a);
		}

		private static SdlRgba? TryGetDrawColor(IntPtr ptrRenderer) {
			var rslt = SDL2.SDL.SDL_GetRenderDrawColor(ptrRenderer, out var r, out var g, out var b, out var a);
			if (0 == rslt) { return null; }

			return new SdlRgba(r, g, b, a);
		}

		private SdlRgba GetDrawColor() => SdlCallUtil.ThrowIfSdlFuncFails(TryGetDrawColor, this.GetPointer()).Value;
		#endregion DrawColor

		#region DrawLine
		private static int TryDrawLine(IntPtr ptrRenderer, int x1, int x2, int y1, int y2) {
			return SDL2.SDL.SDL_RenderDrawLine(ptrRenderer, x1, x2, y1, y2);
		}

		public void DrawLine(int x1, int y1, int x2, int y2) {
			SdlCallUtil.ThrowIfSdlCallFails(TryDrawLine, this.GetPointer(), x1, y1, x2, y2);
		}
		public void DrawLine(SdlVector start, SdlVector end) {
			this.DrawLine(start.X, start.Y, end.X, end.Y);
		}
		public void DrawLine(SdlLine line) {
			this.DrawLine(line.Start, line.End);
		}

		public void DrawLine(SdlRgba color, SdlVector start, SdlVector end) {
			this.DrawColor = color;
			this.DrawLine(start, end);
		}
		public void DrawLine(SdlRgba color, SdlLine line) {
			this.DrawColor = color;
			this.DrawLine(line);
		}
		#endregion DrawLine

		#region DrawLines
		private static int TryDrawLines(IntPtr ptrRenderer, SDL2.SDL.SDL_Point[] points) {
			return SDL2.SDL.SDL_RenderDrawLines(ptrRenderer, points, points.Length);
		}

		public void DrawLines(SdlVector[] points) {
			var nativePoints = points.ToNativePoints();

			SdlCallUtil.ThrowIfSdlCallFails(TryDrawLines, this.GetPointer(), nativePoints);
		}
		public void DrawLines(IList<SdlVector> points) {
			var nativePoints = points.ToNativePoints();

			SdlCallUtil.ThrowIfSdlCallFails(TryDrawLines, this.GetPointer(), nativePoints);
		}

		public void DrawLines(SdlRgba color, SdlVector[] points) {
			this.DrawColor = color;
			this.DrawLines(points);
		}
		public void DrawLines(SdlRgba color, IList<SdlVector> points) {
			this.DrawColor = color;
			this.DrawLines(points);
		}
		#endregion DrawLines

		#region DrawPoint
		private static int TryDrawPoint(IntPtr ptrRenderer, int x, int y) {
			return SDL2.SDL.SDL_RenderDrawPoint(ptrRenderer, x, y);
		}

		public void DrawPoint(int x, int y) {
			SdlCallUtil.ThrowIfSdlCallFails(TryDrawPoint, this.GetPointer(), x, y);
		}
		public void DrawPoint(SdlVector position) {
			this.DrawPoint(position.X, position.Y);
		}

		public void DrawPoint(SdlRgba color, int x, int y) {
			this.DrawColor = color;
			this.DrawPoint(x, y);
		}
		public void DrawPoint(SdlRgba color, SdlVector position) {
			this.DrawColor = color;
			this.DrawPoint(position);
		}
		#endregion DrawPoint

		#region DrawPoints
		private static int TryDrawPoints(IntPtr ptrRenderer, SDL2.SDL.SDL_Point[] points) {
			return SDL2.SDL.SDL_RenderDrawPoints(ptrRenderer, points, points.Length);
		}

		public void DrawPoints(SdlVector[] points) {
			var nativePoints = points.ToNativePoints();

			SdlCallUtil.ThrowIfSdlCallFails(TryDrawPoints, this.GetPointer(), nativePoints);
		}
		public void DrawPoints(IList<SdlVector> points) {
			var nativePoints = points.ToNativePoints();

			SdlCallUtil.ThrowIfSdlCallFails(TryDrawPoints, this.GetPointer(), nativePoints);
		}
		#endregion DrawPoints

		#region DrawRect
		private static int TryDrawRect(IntPtr ptrRenderer, SDL2.SDL.SDL_Rect rect) {
			return SDL2.SDL.SDL_RenderDrawRect(ptrRenderer, ref rect);
		}

		public void DrawRect(SdlRect rect) {
			SdlCallUtil.ThrowIfSdlCallFails(TryDrawRect, this.GetPointer(), (SDL2.SDL.SDL_Rect)rect);
		}
		public void DrawRect(SdlRgba color, SdlRect rect) {
			this.DrawColor = color;
			this.DrawRect(rect);
		}
		#endregion DrawRect

		#region DrawRects
		private static int TryDrawRects(IntPtr ptrRenderer, SDL2.SDL.SDL_Rect[] rects) {
			return SDL2.SDL.SDL_RenderDrawRects(ptrRenderer, rects, rects.Length);
		}

		public void DrawRects(SdlRect[] rects) {
			var nativeRects = rects.ToNativeRects();
			SdlCallUtil.ThrowIfSdlCallFails(TryDrawRects, this.GetPointer(), nativeRects);
		}
		public void DrawRects(IList<SdlRect> rects) {
			var nativeRects = rects.ToNativeRects();
			SdlCallUtil.ThrowIfSdlCallFails(TryDrawRects, this.GetPointer(), nativeRects);
		}

		public void DrawRects(SdlRgba color, SdlRect[] rects) {
			this.DrawColor = color;
			this.DrawRects(rects);
		}
		public void DrawRects(SdlRgba color, IList<SdlRect> rects) {
			this.DrawColor = color;
			this.DrawRects(rects);
		}
		#endregion DrawRects

		#region FillRect
		private static int TryFillRect(IntPtr ptrRenderer, SDL2.SDL.SDL_Rect rect) {
			return SDL2.SDL.SDL_FillRect(ptrRenderer, ref rect);
		}

		public void FillRect(SdlRect rect) {
			SdlCallUtil.ThrowIfSdlCallFails(TryFillRect, this.GetPointer(), (SDL2.SDL.SDL_Rect)rect);
		}
		#endregion FillRect
	}
}
