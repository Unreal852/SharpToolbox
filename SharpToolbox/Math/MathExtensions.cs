using System;

namespace SharpToolbox.Math
{
    /// <summary>
    /// Provide mathematical operations extensions.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Returns a random floating-point between the specified min and max values.
        /// </summary>
        /// <param name="random">Random</param>
        /// <param name="min">Minimum</param>
        /// <param name="max">Maximum</param>
        /// <returns>Random Double</returns>
        public static double NextDouble(this Random random, double min, double max)
        {
            return min + (random.NextDouble() * (max - min));
        }
    }
}