namespace SharpToolbox.Safes
{
    public static class Ensure
    {
        public static T      NotNull<T>(T value, T ifNull)                         => value ?? ifNull;
        public static void   NotNull<T>(ref T value, T ifNull)                     => value = NotNull(value, ifNull);
        public static int    GreaterThan(int value, int minimum)                   => value < minimum ? minimum : value;
        public static double GreaterThan(double value, double minimum)             => value < minimum ? minimum : value;
        public static float  GreaterThan(float value, float minimum)               => value < minimum ? minimum : value;
        public static long   GreaterThan(long value, long minimum)                 => value < minimum ? minimum : value;
        public static void   GreaterThan(ref int value, int minimum)               => value = value < minimum ? minimum : value;
        public static void   GreaterThan(ref double value, double minimum)         => value = value < minimum ? minimum : value;
        public static void   GreaterThan(ref float value, float minimum)           => value = value < minimum ? minimum : value;
        public static void   GreaterThan(ref long value, long minimum)             => value = value < minimum ? minimum : value;
        public static int    LowerThan(int value, int maximum)                     => value > maximum ? maximum : value;
        public static double LowerThan(double value, double maximum)               => value > maximum ? maximum : value;
        public static float  LowerThan(float value, float maximum)                 => value > maximum ? maximum : value;
        public static long   LowerThan(long value, long maximum)                   => value > maximum ? maximum : value;
        public static void   LowerThan(ref int value, int maximum)                 => value = value > maximum ? maximum : value;
        public static void   LowerThan(ref double value, double maximum)           => value = value > maximum ? maximum : value;
        public static void   LowerThan(ref float value, float maximum)             => value = value > maximum ? maximum : value;
        public static void   LowerThan(ref long value, long maximum)               => value = value > maximum ? maximum : value;
        public static int    Positive(int value)                                   => System.Math.Abs(value);
        public static double Positive(double value)                                => System.Math.Abs(value);
        public static float  Positive(float value)                                 => System.Math.Abs(value);
        public static long   Positive(long value)                                  => System.Math.Abs(value);
        public static void   Positive(ref int value)                               => value = System.Math.Abs(value);
        public static void   Positive(ref double value)                            => value = System.Math.Abs(value);
        public static void   Positive(ref float value)                             => value = System.Math.Abs(value);
        public static void   Positive(ref long value)                              => value = System.Math.Abs(value);
        public static int    Negative(int value)                                   => value > 0 ? -value : value;
        public static double Negative(double value)                                => value > 0 ? -value : value;
        public static float  Negative(float value)                                 => value > 0 ? -value : value;
        public static long   Negative(long value)                                  => value > 0 ? -value : value;
        public static void   Negative(ref int value)                               => value = value > 0 ? -value : value;
        public static void   Negative(ref double value)                            => value = value > 0 ? -value : value;
        public static void   Negative(ref float value)                             => value = value > 0 ? -value : value;
        public static void   Negative(ref long value)                              => value = value > 0 ? -value : value;
        public static int    Between(int value, int minimum, int maximum)          => GreaterThan(LowerThan(value, maximum), minimum);
        public static double Between(double value, double minimum, double maximum) => GreaterThan(LowerThan(value, maximum), minimum);
        public static float  Between(float value, float minimum, float maximum)    => GreaterThan(LowerThan(value, maximum), minimum);
        public static long   Between(long value, long minimum, long maximum)       => GreaterThan(LowerThan(value, maximum), minimum);

        public static void Between(ref int value, int minimum, int maximum)
        {
            GreaterThan(ref value, minimum);
            LowerThan(ref value, maximum);
        }

        public static void Between(ref double value, double minimum, double maximum)
        {
            GreaterThan(ref value, minimum);
            LowerThan(ref value, maximum);
        }

        public static void Between(ref float value, float minimum, float maximum)
        {
            GreaterThan(ref value, minimum);
            LowerThan(ref value, maximum);
        }

        public static void Between(ref long value, long minimum, long maximum)
        {
            GreaterThan(ref value, minimum);
            LowerThan(ref value, maximum);
        }
    }
}