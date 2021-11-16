using System;
using System.Data;

namespace SharpToolbox.Math;

public static class MathHelper
{
    /// <summary>
    /// Static Random
    /// </summary>
    public static Random Random { get; } = new();

    /// <summary>
    /// Returns percentage of the specified value
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="percentage">Percentage</param>
    /// <returns>Percentage</returns>
    public static int PercentageOf(int value, int percentage)
    {
        return value * percentage / 100;
    }

    /// <summary>
    /// Returns percentage of the specified value
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="percentage">Percentage</param>
    /// <returns>Percentage</returns>
    public static double PercentageOf(double value, double percentage)
    {
        return value * percentage / 100.0;
    }

    /// <summary>
    /// Returns the mathematical operation result of the specified string
    /// </summary>
    /// <param name="operation">String operation</param>
    /// <returns>Mathematical Result</returns>
    public static object CalculateStringOperation(string operation)
    {
        return new DataTable().Compute(operation, null);
    }

    /// <summary>
    /// Returns the mathematical operation result of the specified string
    /// </summary>
    /// <param name="operation">String operation</param>
    /// <typeparam name="T">Result Type</typeparam>
    /// <returns>Mathematical Result</returns>
    public static T CalculateStringOperation<T>(string operation) where T : struct, IConvertible, IComparable, IFormattable, IComparable<T>, IEquatable<T>
    {
        object calculated = CalculateStringOperation(operation);
        if (calculated == null)
            return default;
        return (T)calculated;
    }
}