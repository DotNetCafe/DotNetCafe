using System;
using System.Globalization;
using DotNetCafe.Globalization;
using static DotNetCafe.Internals.CpfFormatInfo;

namespace DotNetCafe.Internals
{
    internal static class CpfFormatter
    {
        public static string Format(Cpf self, string? format, IFormatProvider? provider)
        {
            format ??= GeneralFormat;
            provider ??= CultureInfo.InvariantCulture;

            return format.ToUpperInvariant() switch
            {
                GeneralFormat => 
                    self.number.ToString(GeneralFormatMask, provider),
                NumericFormat =>
                    self.number.ToString(NumericFormatMask, provider),
                NewFormat =>
                    self.number.ToString(NewFormatMask, provider),
                _ => 
                    throw new FormatException(string.Format(SR.FormatException_InvalidFormat, format))
            };
        }
    }
}
