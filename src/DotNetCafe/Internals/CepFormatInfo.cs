namespace DotNetCafe.Internals
{
    internal static class CepFormatInfo
    {
        public const string GeneralFormat = "G";
        public const string GeneralFormatMask = @"00000\-000";
        public const int GeneralFormatLength = 9;

        public const string NumericFormat = "N";
        public const string NumericFormatMask = "00000000";
        public const int NumericFormatLength = 8;
    }
}