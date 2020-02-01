using System;
using System.Diagnostics;
using DotNetCafe.Globalization;
using static DotNetCafe.Internals.CnpjFormatInfo;

namespace DotNetCafe.Internals
{
    internal static class CnpjParser
    {
        private const int FirstCheckDigitPosition = 12;
        private const int SecondCheckDigitPosition = 13;

        private static readonly int[] FirstCheckDigitWeights =
            new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        private static readonly int[] SecondCheckDigitWeights =
            new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        
        private enum ParseExceptionKind 
        {
            None,
            Argument,
            Format
        }

        private struct ParseResult
        {
            internal long result;
            internal string paramName;
            internal ParseExceptionKind exceptionKind;

            internal bool Succeed => exceptionKind == ParseExceptionKind.None;

            internal Exception GetException()
            {
                switch (exceptionKind)
                {
                    case ParseExceptionKind.Argument:
                        return new ArgumentException(SR.ArgumentException_InvalidCnpjNumber,
                            paramName);

                    case ParseExceptionKind.Format:
                        return new FormatException(SR.FormatException_InvalidCnpjFormat);

                    case ParseExceptionKind.None:
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        internal static Cnpj Parse(string s)
        {
            var pr = new ParseResult();
            pr.paramName = nameof(s);

            TryParseCnpj(ref pr, s);

            if (pr.Succeed)
            {
                return new Cnpj(pr.result);
            }

            throw pr.GetException();
        }

        internal static bool TryParse(string s, out Cnpj result)
        {
            var pr = new ParseResult();
            pr.paramName = nameof(s);

            TryParseCnpj(ref pr, s);

            result = new Cnpj(pr.result);
            return pr.Succeed;
        }

        private static void TryParseCnpj(ref ParseResult pr, ReadOnlySpan<char> source)
        {
            if (source.Length == 0 ||
                source.Length != GeneralFormatLength &&
                source.Length != GeneralFormat2Length &&
                source.Length != NumericFormatLength &&
                source.Length != NumericFormat2Length)
            {
                Debug.WriteLine("FAIL: invalid length.");
                pr.exceptionKind = ParseExceptionKind.Format;
                return;
            }

            if (!MaskParser.TryParse(source, GetFormat(source.Length), 14, out Span<char> numeric))
            {
                Debug.WriteLine("FAIL: invalid format.");
                pr.exceptionKind = ParseExceptionKind.Format;
                return;
            }

            if (numeric[8..12].SequenceEqual("0000"))
            {
                Debug.WriteLine("FAIL: zeroed order number.");
                pr.exceptionKind = ParseExceptionKind.Argument;
                return;
            }

            if (!long.TryParse(numeric, out long number))
            {
                Debug.WriteLine("FAIL: isn't numeric");
                pr.exceptionKind = ParseExceptionKind.Format;
                return;
            }

            if (Char.GetNumericValue(numeric[FirstCheckDigitPosition]) != 
                CheckDigit.Calculate(numeric, FirstCheckDigitWeights))
            {
                Debug.WriteLine("FAIL: DC1 isn't {0}", CheckDigit.Calculate(numeric, FirstCheckDigitWeights));
                pr.exceptionKind = ParseExceptionKind.Argument;
                return;
            }

            if (Char.GetNumericValue(numeric[SecondCheckDigitPosition]) != 
                CheckDigit.Calculate(numeric, SecondCheckDigitWeights))
            {
                Debug.WriteLine("FAIL: DC2 isn't {0}", CheckDigit.Calculate(numeric, FirstCheckDigitWeights));
                pr.exceptionKind = ParseExceptionKind.Argument;
                return;
            }

            pr.result = number;
        }

        private static ReadOnlySpan<char> GetFormat(int length)
        {
            return length switch
            {
                GeneralFormatLength => GeneralFormatMask,
                GeneralFormat2Length => GeneralFormat2Mask,
                NumericFormatLength => NumericFormatMask,
                NumericFormat2Length => NumericFormat2Mask,
                _ => throw new InvalidOperationException()
            };
        }
    }
}
