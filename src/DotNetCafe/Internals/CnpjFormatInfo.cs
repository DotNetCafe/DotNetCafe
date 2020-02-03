namespace DotNetCafe.Internals
{
    internal static class CnpjFormatInfo
    {
        public const string GeneralFormat = "G";
        public const string GeneralFormatMask = @"00\.000\.000\/0000\-00";
        public const int GeneralFormatLength = 18;

        public const string NumericFormat = "N";
        public const string NumericFormatMask = @"00000000000000";
        public const int NumericFormatLength = 14;

        public const string GeneralFormat2 = "0G";
        public const string GeneralFormat2Mask = @"\000\.000\.000\/0000\-00";
        public const int GeneralFormat2Length = 19;

        public const string NumericFormat2 = "0N";
        public const string NumericFormat2Mask = @"\000000000000000";
        public const int NumericFormat2Length = 15;
    }
}