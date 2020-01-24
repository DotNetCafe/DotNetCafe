using System;

namespace DotNetCafe.Internals
{
    internal static class DigitChecker
    {
        public static int Get(ReadOnlySpan<char> value, int[] weights, int modulo = 11)
        {
            int sum = 0, rest = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * (int) Char.GetNumericValue(value[i]);
            }

            rest = sum % modulo;

            return rest < 2 ? 0 : modulo - rest;
        }
    }
}