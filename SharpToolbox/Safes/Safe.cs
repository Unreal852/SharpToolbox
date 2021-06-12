using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpToolbox.Safes
{
    /// <summary>
    /// Provide try-catch methods.
    /// </summary>
    public static class Safe
    {
        /// <summary>
        /// Tries to execute the specified action.
        /// </summary>
        /// <param name="action">Action</param>
        /// <returns>SafeResult</returns>
        public static SafeResult Try([NotNull] Action action)
        {
            try
            {
                action();
                return new SafeResult(true);
            }
            catch (Exception ex)
            {
                return new SafeResult(false, ex);
            }
        }

        /// <summary>
        /// Tries to execute the specified func
        /// </summary>
        /// <param name="action">Func</param>
        /// <typeparam name="T">Return Type</typeparam>
        /// <returns>SafeResult</returns>
        public static SafeResult<T> Try<T>([NotNull] Func<T> action)
        {
            try
            {
                return new SafeResult<T>(action(), true);
            }
            catch (Exception ex)
            {
                return new SafeResult<T>(default, false, ex);
            }
        }

        /// <summary>
        /// Tries to execute the specified func
        /// </summary>
        /// <param name="action">Func</param>
        /// <param name="result">Result</param>
        /// <typeparam name="T">Return Type</typeparam>
        /// <returns>SafeResult</returns>
        public static SafeResult Try<T>([NotNull] Func<T> action, out T result)
        {
            try
            {
                result = action();
                return new SafeResult(true);
            }
            catch (Exception ex)
            {
                result = default;
                return new SafeResult(false, ex);
            }
        }
    }
}