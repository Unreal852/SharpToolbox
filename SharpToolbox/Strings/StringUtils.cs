using System;
using System.Text;
using SharpToolbox.Math;

namespace SharpToolbox.Strings
{
    public static class StringUtils
    {
        /// <summary>
        /// Generate a random string
        /// </summary>
        /// <param name="length">string Length</param>
        /// <param name="useLowerCase">Use Lower Case Characters</param>
        /// <param name="useUpperCase">Use Upper Case Characters</param>
        /// <param name="useNumbers">User Numbers</param>
        /// <param name="customChars">Use Custom Characters</param>
        /// <returns>Random String</returns>
        public static string RandomString(int length = 5, bool useLowerCase = true, bool useUpperCase = true,
                                          bool useNumbers = true, params string[] customChars)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(useLowerCase ? "abcdefghijklmnopqrstuvwxyz" : "")
                   .Append(useUpperCase ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ" : "")
                   .Append(useNumbers ? "0123456789" : "")
                   .Append(customChars is {Length: > 0} ? string.Join("", customChars) : "");
            string characters = builder.ToString();
            if (characters.Length == 0)
                throw new ArgumentException(
                    $"You must at least use one of these parameters: {nameof(useLowerCase)}, {nameof(useUpperCase)}, {nameof(useNumbers)}, {nameof(customChars)}.");
            builder.Clear();
            for (int count = 0; count < length; count++)
                builder.Append(characters[MathHelper.Random.Next(0, characters.Length)]);
            return builder.ToString();
        }
    }
}