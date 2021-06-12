using System;

namespace SharpToolbox.Safes
{
    public class SafeResult
    {
        public SafeResult(bool success, Exception ex = null)
        {
            Success = success;
            Exception = ex;
        }

        /// <summary>
        /// Result, true if successful, false otherwise
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Exception, this is null if the success is true
        /// </summary>
        public Exception Exception { get; }
    }

    public class SafeResult<T> : SafeResult
    {
        public SafeResult(T result, bool success, Exception ex = null) : base(success, ex)
        {
            Result = result;
        }

        /// <summary>
        /// Result object, this is null if there is no return type.
        /// </summary>
        public T Result { get; }
    }
}