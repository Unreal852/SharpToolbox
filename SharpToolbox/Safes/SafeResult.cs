using System;

namespace SharpToolbox.Safes;

public class SafeResult
{
    public static SafeResult    Success()                      => new(true);
    public static SafeResult<T> Success<T>(T resultValue)      => new(resultValue, true);
    public static SafeResult    Failed(Exception ex = null)    => new(false, ex);
    public static SafeResult<T> Failed<T>(Exception ex = null) => new(default, false, ex);


    public SafeResult(bool success, Exception ex = null)
    {
        IsSuccess = success;
        Exception = ex;
    }

    /// <summary>
    /// Result, true if successful, false otherwise
    /// </summary>
    public bool IsSuccess { get; }

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