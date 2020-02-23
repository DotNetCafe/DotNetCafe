using System;
using System.Globalization;
using DotNetCafe.Globalization;
using static DotNetCafe.Internals.CepFormatInfo;

namespace DotNetCafe.Internals
{
    internal static class CepFormatter
    {
        public static string Format(Cep self, string format, IFormatProvider provider)
        {
            format ??= GeneralFormat;
            provider ??= CultureInfo.InvariantCulture;

            return format switch
            {
                GeneralFormat => 
                    self.number.ToString(GeneralFormatMask, provider),
                NumericFormat => 
                    self.number.ToString(NumericFormatMask, provider),
                _ => 
                    throw new FormatException(string.Format(SR.FormatException_InvalidFormat, format))
            };
        }
    }
}