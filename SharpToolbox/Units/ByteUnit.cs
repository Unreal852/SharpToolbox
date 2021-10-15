using System;
using System.Collections.Generic;
using SharpToolbox.Exceptions;

namespace SharpToolbox.Units
{
    public readonly struct ByteUnit
    {
        private static Dictionary<EUnit, string[]> UnitValues { get; } = new()
        {
            { EUnit.Bit, new[] { "b", "Kb", "Mb", "Gb", "Tb", "Pb", "Eb", "Zb", "Yb" } },
            { EUnit.Byte, new[] { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" } },
            { EUnit.Octet, new[] { "o", "Ko", "Mo", "Go", "To", "Po", "Eo", "Zo", "Yo" } }
        };

        private static Dictionary<EUnit, int> UnitMultiplier { get; } = new()
        {
            { EUnit.Bit, 8 },
            { EUnit.Byte, 1 },
            { EUnit.Octet, 1 }
        };


        public ByteUnit(long value)
        {
            Value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public long Value { get; }

        /// <summary>
        /// Value to KILO unit
        /// </summary>
        /// <param name="unit">Unit to use</param>
        /// <returns>Value / 1024.0 * EUnit</returns>
        public double ToKilo(EUnit unit = EUnit.Bit)
        {
            return Value / 1024.0 * UnitMultiplier[unit];
        }

        /// <summary>
        /// Value to MEGA unit
        /// </summary>
        /// <param name="unit">Unit to use</param>
        /// <returns>Value / 1024.0 / 1024.0 * EUnit</returns>
        public double ToMega(EUnit unit = EUnit.Bit)
        {
            return Value / 1024.0 / 1024.0 * UnitMultiplier[unit];
        }

        /// <summary>
        /// Value to GIGA unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>Value / 1024.0 / 1024.0 / 1024.0 * EUnit</returns>
        public double ToGiga(EUnit unit = EUnit.Bit)
        {
            return Value / 1024.0 / 1024.0 / 1024.0 * UnitMultiplier[unit];
        }

        /// <summary>
        /// Value to TERA unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>Value / 1024.0 / 1024.0 / 1024.0 / 1024.0 * EUnit</returns>
        public double ToTera(EUnit unit = EUnit.Bit)
        {
            return Value / 1024.0 / 1024.0 / 1024.0 / 1024.0 * UnitMultiplier[unit];
        }

        /// <summary>
        /// Value to a human readable string.
        /// </summary>
        /// <param name="unit">Unit to use</param>
        /// <param name="decimalPlaces">Decimal places</param>
        /// <returns>Human readable value</returns>
        public string ToHumanReadable(EUnit unit = EUnit.Bit, int decimalPlaces = 1)
        {
            Throw.If<ArgumentOutOfRangeException>(decimalPlaces <= 0, nameof(decimalPlaces));
            if (Value <= 0)
                return $"0.00 {UnitValues[unit][0]}";
            int mag = (int)System.Math.Log(Value, 1024);                // Mag is 0 for (b, B, o), 1 for (Kb, KB, Ko) etc
            decimal adjustedSize = (decimal)Value / (1L << (mag * 10)); // 1L << (mag * 10) == 2 ^ (10 * mag)
            if (System.Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag++;
                adjustedSize /= 1024;
            }

            if (unit == EUnit.Bit)
                adjustedSize *= 8;
            return $"{adjustedSize:#.##} {UnitValues[unit][mag]}";
        }

        /// <summary>
        /// Returns the sum of A + B
        /// </summary>
        /// <param name="a">Value</param>
        /// <param name="b">Add amount</param>
        /// <returns>Sum</returns>
        public static ByteUnit operator +(ByteUnit a, ByteUnit b)
        {
            return new(a.Value + b.Value);
        }

        /// <summary>
        /// Returns the difference between A + B
        /// </summary>
        /// <param name="a">Value</param>
        /// <param name="b">Subtraction amount</param>
        /// <returns>Difference</returns>
        public static ByteUnit operator -(ByteUnit a, ByteUnit b)
        {
            return new(a.Value - b.Value);
        }

        /// <summary>
        /// Returns the product of A and B
        /// </summary>
        /// <param name="a">Value</param>
        /// <param name="b">Multiply by amount</param>
        /// <returns>Product</returns>
        public static ByteUnit operator *(ByteUnit a, ByteUnit b)
        {
            return new(a.Value * b.Value);
        }
    }
}