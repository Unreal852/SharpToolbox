using System;

namespace SharpToolbox.Extensions
{
    /// <summary>
    /// Provide Extensions to the DateTime.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     Converts the specified DateTime into a TimeSpan
        /// </summary>
        /// <param name="time">DateTime</param>
        ///     <returns>TimeSpan</returns>
        public static TimeSpan ToTimeSpan(this DateTime time)
        {
            return TimeSpan.FromTicks(time.Ticks);
        }
    }
}