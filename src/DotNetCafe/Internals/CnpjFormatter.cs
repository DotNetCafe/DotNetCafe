using System;
using System.Globalization;

namespace DotNetCafe
{
    internal static class CnpjFormatter
    {
        private const string GENERAL_FORMAT = "G";
        private const string NUMERIC_FORMAT = "N";

        public static string Format(Cnpj self, string? format, IFormatProvider? formatProvider)
        {
            format ??= GENERAL_FORMAT;
            formatProvider ??= CultureInfo.InvariantCulture;

            switch (format.ToUpperInvariant())
            {
                case NUMERIC_FORMAT:
                    return self.value.ToString("D14", formatProvider);

                case GENERAL_FORMAT:
                    return self.value.ToString(@"00\.000\.000\/0000\-00", formatProvider);

                default:
                    throw new FormatException($"O formato '{format}' não é suportado.");                    
            }
        }
    }
}
