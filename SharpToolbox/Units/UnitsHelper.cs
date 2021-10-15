namespace SharpToolbox.Units
{
    public static class UnitsHelper
    {
        /// <summary>
        /// Returns the formatted string for the specified value
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="unit">Unit Conversion</param>
        /// <param name="decimalPlaces">Decimal Places</param>
        /// <returns>Formatted String</returns>
        public static string GetFormattedString(long value, EUnit unit = EUnit.Bit, int decimalPlaces = 1)
        {
            return new ByteUnit(value).ToHumanReadable(unit, decimalPlaces);
        }
    }
}