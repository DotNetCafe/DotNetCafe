namespace DotNetCafe.Internals
{
    internal static class CpfFormatInfo
    {
        public const string GeneralFormat = "G";
        public const string GeneralFormatMask = @"000\.000\.000\-00";
        public const int GeneralFormatLength = 14;

        public const string NewFormat = "B";
        public const string NewFormatMask = @"000000000\/00";
        public const int NewFormatLength = 12;

        public const string NumericFormat = "N";
        public const string NumericFormatMask = @"00000000000";
        public const int NumericFormatLength = 11;
    }
}