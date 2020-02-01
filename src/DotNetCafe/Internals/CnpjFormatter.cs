using DotNetCafe.Globalization;
using System;
using System.Globalization;
using static DotNetCafe.Internals.CnpjFormatInfo;

namespace DotNetCafe
{
    internal static class CnpjFormatter
    {
        public static string Format(Cnpj self, string format, IFormatProvider formatProvider)
        {
            format ??= GeneralFormat;
            formatProvider ??= CultureInfo.InvariantCulture;

            switch (format.ToUpperInvariant())
            {
                case NumericFormat:
                    return self.number.ToString(NumericFormatMask, formatProvider);

                case GeneralFormat:
                    return self.number.ToString(GeneralFormatMask, formatProvider);

                default:
                    throw new FormatException(string.Format(SR.FormatException_InvalidFormat, format));                    
            }
        }
    }
}
