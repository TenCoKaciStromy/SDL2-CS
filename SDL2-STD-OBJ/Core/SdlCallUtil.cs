using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ObjectiveSdl2.Core {
	public static class SdlCallUtil {
		public static void ThrowIfSdlActionFails(Action tryAction, [CallerMemberName]string methodName = null) {
			try {
				tryAction();
			}
			catch (Exception exc) {
				var prefix = methodName + ": ";
				throw SdlNativeException.CreateFromLastSdlError(prefix, exc);
			}
		}
		public static void ThrowIfSdlActionFails<TArg0>(Action<TArg0> tryAction, TArg0 arg0, [CallerMemberName]string methodName = null) {
			try {
				tryAction(arg0);
			}
			catch (Exception exc) {
				var prefix = methodName + ": ";
				throw SdlNativeException.CreateFromLastSdlError(prefix, exc);
			}
		}
		public static void ThrowIfSdlActionFails<TArg0, TArg1>(Action<TArg0, TArg1> tryAction, TArg0 arg0, TArg1 arg1, [CallerMemberName]string methodName = null) {
			try {
				tryAction(arg0, arg1);
			}
			catch (Exception exc) {
				var prefix = methodName + ": ";
				throw SdlNativeException.CreateFromLastSdlError(prefix, exc);
			}
		}
		public static void ThrowIfSdlActionFails<TArg0, TArg1, TArg2>(Action<TArg0, TArg1, TArg2> tryAction, TArg0 arg0, TArg1 arg1, TArg2 arg2, [CallerMemberName]string methodName = null) {
			try {
				tryAction(arg0, arg1, arg2);
			}
			catch (Exception exc) {
				var prefix = methodName + ": ";
				throw SdlNativeException.CreateFromLastSdlError(prefix, exc);
			}
		}
		public static void ThrowIfSdlActionFails<TArg0, TArg1, TArg2, TArg3>(Action<TArg0, TArg1, TArg2, TArg3> tryAction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, [CallerMemberName]string methodName = null) {
			try {
				tryAction(arg0, arg1, arg2, arg3);
			}
			catch (Exception exc) {
				var prefix = methodName + ": ";
				throw SdlNativeException.CreateFromLastSdlError(prefix, exc);
			}
		}

		public static void ThrowIfSdlCallFails(Func<int> tryFunction, [CallerMemberName]string methodName = null) {
			var resultCode = tryFunction();
			if (0 == resultCode) { return; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static void ThrowIfSdlCallFails<TArg0>(Func<TArg0, int> tryFunction, TArg0 arg0, [CallerMemberName]string methodName = null) {
			var resultCode = tryFunction(arg0);
			if (0 == resultCode) { return; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static void ThrowIfSdlCallFails<TArg0, TArg1>(Func<TArg0, TArg1, int> tryFunction, TArg0 arg0, TArg1 arg1, [CallerMemberName]string methodName = null) {
			var resultCode = tryFunction(arg0, arg1);
			if (0 == resultCode) { return; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static void ThrowIfSdlCallFails<TArg0, TArg1, TArg2>(Func<TArg0, TArg1, TArg2, int> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, [CallerMemberName]string methodName = null) {
			var resultCode = tryFunction(arg0, arg1, arg2);
			if (0 == resultCode) { return; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static void ThrowIfSdlCallFails<TArg0, TArg1, TArg2, TArg3>(Func<TArg0, TArg1, TArg2, TArg3, int> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, [CallerMemberName]string methodName = null) {
			var resultCode = tryFunction(arg0, arg1, arg2, arg3);
			if (0 == resultCode) { return; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}

		public static TResult ThrowIfSdlFuncFails<TResult>(Func<TResult> tryFunction, [CallerMemberName]string methodName = null) {
			var result = tryFunction();
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static TResult ThrowIfSdlFuncFails<TArg0, TResult>(Func<TArg0, TResult> tryFunction, TArg0 arg0, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static TResult ThrowIfSdlFuncFails<TArg0, TArg1, TResult>(Func<TArg0, TArg1, TResult> tryFunction, TArg0 arg0, TArg1 arg1, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TResult>(Func<TArg0, TArg1, TArg2, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1, arg2);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1, arg2, arg3);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1, arg2, arg3, arg4);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1, arg2, arg3, arg4, arg5);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
		public static TResult ThrowIfSdlFuncFails<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> tryFunction, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, [CallerMemberName]string methodName = null) {
			var result = tryFunction(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			if (null != result) { return result; }

			var prefix = methodName + ": ";
			throw SdlNativeException.CreateFromLastSdlError(prefix);
		}
	}
}
