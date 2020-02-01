using System;
using DotNetCafe.Globalization;

namespace DotNetCafe.Internals
{
    internal static class CnpjParser
    {
        private const string Format1 = @"099.999.999/9999-99";
        private const string Format2 = @"99.999.999/9999-99";
        private const string Format3 = @"099999999999999";
        private const string Format4 = @"99999999999999";

        private const int Format1Length = 19;
        private const int Format2Length = 18;
        private const int Format3Length = 15;
        private const int Format4Length = 14;

        private static readonly int[] FirstDigitCheckerWeights =
            new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        private static readonly int[] SecondDigitCheckerWeights =
            new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        
        private enum ParseExceptionKind 
        {
            Argument,
            Format
        }

        private struct ParseResult
        {
            internal Cnpj result;
            internal bool succeed;
            internal string paramName;
            private ParseExceptionKind exceptionKind;

            internal void SetException(ParseExceptionKind kind)
            {
                exceptionKind = kind;
            }

            internal Exception GetException()
            {
                return exceptionKind switch
                {
                    ParseExceptionKind.Argument => 
                        new ArgumentException(SR.ArgumentException_InvalidCnpjNumber, 
                            paramName),
                    
                    ParseExceptionKind.Format => 
                        new FormatException(SR.FormatException_InvalidCnpjFormat),

                    _ => 
                        new InvalidOperationException(),
                };
            }
        }

        internal static Cnpj Parse(string s)
        {
            var pr = new ParseResult();
            pr.paramName = nameof(s);

            TryParseCnpj(ref pr, s);

            if (pr.succeed)
            {
                return pr.result;
            }

            throw pr.GetException();
        }

        internal static bool TryParse(string s, out Cnpj result)
        {
            var pr = new ParseResult();
            pr.paramName = nameof(s);

            TryParseCnpj(ref pr, s);

            result = pr.result;
            return pr.succeed;
        }

        private static void TryParseCnpj(ref ParseResult pr, ReadOnlySpan<char> src)
        {
            if (src.Length > Format1Length)
            {
                pr.SetException(ParseExceptionKind.Format);
                return;
            }

            ReadOnlySpan<char> fmt = GetFormatFromLength(src.Length);            
            Span<char> dst = new char[] 
            { 
                '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' 
            };

            for (int i = src.Length - 1, j = Format4Length; i >= 0; i--)
            {
                if (fmt[i] == '9' && Char.IsDigit(src[i]))
                {
                    dst[--j] = src[i];
                    continue;
                }
                
                if (fmt[i] != src[i])
                {
                    pr.SetException(ParseExceptionKind.Format);
                    return;
                }
            }

            if (dst[8..12].SequenceEqual("0000"))
            {
                pr.SetException(ParseExceptionKind.Argument);
                return;
            }

            if (Char.GetNumericValue(dst[12]) != CheckDigit.Calculate(dst, FirstDigitCheckerWeights) ||
                Char.GetNumericValue(dst[13]) != CheckDigit.Calculate(dst, SecondDigitCheckerWeights))
            {
                pr.SetException(ParseExceptionKind.Argument);
                return;
            }

            pr.result = new Cnpj(Int64.Parse(dst));
            pr.succeed = true;
        }

        private static ReadOnlySpan<char> GetFormatFromLength(int length)
        {
            return length switch
            {
                Format1Length => Format1,
                Format2Length => Format2,
                Format3Length => Format3,
                _ => Format4
            };
        }
    }
}
