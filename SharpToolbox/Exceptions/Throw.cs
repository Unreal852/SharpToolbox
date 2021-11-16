using System;
using System.Collections;
using System.Reflection;

namespace SharpToolbox.Exceptions;

/// <summary>
/// Provide methods to throw exceptions.
/// </summary>
public static class Throw
{
    public static void If<TEx>(bool condition, string paramName, string message = null) where TEx : Exception
    {
        if (!condition)
            return;
        TEx exception = null;
        Type exType = typeof(TEx);
        if (typeof(ArgumentException).IsAssignableFrom(exType))
        {
            ConstructorInfo ctor = exType.GetConstructor(new[] {typeof(string), typeof(string)});
            if (ctor != null)
            {
                if (ctor.GetParameters()[0].Name == "paramName")
                    exception = Activator.CreateInstance(exType, paramName, message ?? "") as TEx;
                else
                    exception = Activator.CreateInstance(exType, message ?? "", paramName) as TEx;
            }
        }
        else
            exception = Activator.CreateInstance(exType, message) as TEx;

        if (exception == null)
            throw new NullReferenceException(
                $"Couldn't find constructor in the specified exception type '{exType.Name}'");
        throw exception;
    }

    /// <summary>
    /// Throws a ArgumentNullException if the specified value is null.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Value Name</param>
    /// <param name="message">Message</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void IfNull(object value, string paramName, string message = null)
    {
        if (value == null)
            throw new ArgumentNullException(paramName, message ?? "The specified value is null.");
    }

    /// <summary>
    /// Throw a ArgumentException if the specified string value is null or empty.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Value Name</param>
    /// <param name="message">Message</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void IfNullOrEmpty(string value, string paramName, string message = null)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException(paramName, message ?? $"The specified string value '{paramName}' is null or empty.");
    }

    /// <summary>
    /// Throw a ArgumentNullException if the specified string value is null, empty, or whitespace.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <param name="paramName">Value Name</param>
    /// <param name="message">Param Name</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void IfNullOrWhiteSpace(string value, string paramName, string message = null)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException(paramName, message ?? $"The specified string value '{paramName}' is null, empty or only made of whitespaces.");
    }

    /// <summary>
    /// Throw a IndexOutOfRangeException of the specified index is out of range of the specified collection.
    /// </summary>
    /// <param name="collection">Collection</param>
    /// <param name="index">Index</param>
    /// <param name="paramName">Param Name</param>
    /// <param name="message">Message</param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public static void IfOutOfRange(IList collection, int index, string paramName, string message = null)
    {
        if (index < 0 || index > collection.Count - 1)
            throw new IndexOutOfRangeException(message ?? $"The specified index '{index}' is out of range. Minimum: '0' Maximum: '{collection.Count - 1}'");
    }
}