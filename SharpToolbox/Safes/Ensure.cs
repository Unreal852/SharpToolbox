namespace SharpToolbox.Safes
{
    public static class Ensure
    {
        public static T    NotNull<T>(T value, T ifNull)                 => value ?? ifNull;
        public static void NotNull<T>(ref T value, T ifNull)             => value = NotNull(value, ifNull);
        public static void GreaterThan(ref int value, int minimum)       => value = value < minimum ? minimum : value;
        public static void GreaterThan(ref double value, double minimum) => value = value < minimum ? minimum : value;
        public static void GreaterThan(ref float value, float minimum)   => value = value < minimum ? minimum : value;
        public static void GreaterThan(ref long value, long minimum)     => value = value < minimum ? minimum : value;
        public static void LowerThan(ref int value, int maximum)         => value = value > maximum ? maximum : value;
        public static void LowerThan(ref double value, double maximum)   => value = value > maximum ? maximum : value;
        public static void LowerThan(ref float value, float maximum)     => value = value > maximum ? maximum : value;
        public static void LowerThan(ref long value, long maximum)       => value = value > maximum ? maximum : value;

        public static void BetweenThan(ref int value, int minimum, int maximum)
        {
            GreaterThan(ref value, minimum);
            LowerThan(ref value, maximum);
        }

        public static void BetweenThan(ref double value, double minimum, double maximum)
        {
            GreaterThan(ref value, minimum);
            LowerThan(ref value, maximum);
        }

        public static void BetweenThan(ref float value, float minimum, float maximum)
        {
            GreaterThan(ref value, minimum);
            LowerThan(ref value, maximum);
        }

        public static void BetweenThan(ref long value, long minimum, long maximum)
        {
            GreaterThan(ref value, minimum);
            LowerThan(ref value, maximum);
        }
    }
}