using System;
using System.Diagnostics;
using DotNetCafe.Globalization;
using static DotNetCafe.Internals.CpfFormatInfo;

namespace DotNetCafe.Internals
{
    internal static class CpfParser
    {
        private const int FirstCheckDigitPosition = 9;
        private const int SecondCheckDigitPosition = 10;

        private static int[] FirstCheckDigitWeights =
            new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        
        private static int[] SecondCheckDigitWeights =
            new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        
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
                        return new ArgumentException(string.Format(SR.ArgumentException_InvalidCpfNumber,
                            paramName));
                    
                    case ParseExceptionKind.Format:
                        return new FormatException(SR.FormatException_InvalidCpfFormat);

                    case ParseExceptionKind.None:   
                    default:
                        return new InvalidOperationException();
                };
            }
        }

        public static Cpf Parse(string s)
        {
            var pr = new ParseResult();
            pr.paramName = nameof(s);

            TryParseCpf(ref pr, s);

            if (pr.Succeed)
            {
                return new Cpf(pr.result);
            }

            throw pr.GetException();
        }

        public static bool TryParse(string s, out Cpf result)
        {
            var pr = new ParseResult();
            pr.paramName = nameof(s);

            TryParseCpf(ref pr, s);

            result = new Cpf(pr.result);
            return pr.Succeed;
        }

        private static void TryParseCpf(ref ParseResult pr, ReadOnlySpan<char> source)
        {
            if (source.Length == 0 || 
                source.Length != GeneralFormatLength &&
                source.Length != NewFormatLength && 
                source.Length != NumericFormatLength)
            {
                Debug.WriteLine("FAIL: invalid length.");
                pr.exceptionKind = ParseExceptionKind.Format;
                return;
            }

            if (!MaskParser.TryParse(source, GetFormat(source.Length), 11, out Span<char> numeric))
            {
                Debug.WriteLine("FAIL: invalid format.");
                pr.exceptionKind = ParseExceptionKind.Format;
                return;
            }

            if (!long.TryParse(numeric, out long number))
            {
                Debug.WriteLine("FAIL: not a number");
                pr.exceptionKind = ParseExceptionKind.Format;
                return;
            }

            if (Char.GetNumericValue(numeric[FirstCheckDigitPosition]) != 
                CheckDigit.Calculate(numeric, FirstCheckDigitWeights))
            {
                Debug.WriteLine($"FAIL: DC1 isn't {CheckDigit.Calculate(numeric, FirstCheckDigitWeights)}");
                pr.exceptionKind = ParseExceptionKind.Argument;
                return;
            }
                    
            if (Char.GetNumericValue(numeric[SecondCheckDigitPosition]) != 
                CheckDigit.Calculate(numeric, SecondCheckDigitWeights))
            {
                Debug.WriteLine($"FAIL: DC2 isn't {CheckDigit.Calculate(numeric, SecondCheckDigitWeights)}");
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
                NewFormatLength => NewFormatMask,
                _ => NumericFormatMask
            };
        }
    }
}