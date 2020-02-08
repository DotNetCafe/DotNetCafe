using System;
using System.Diagnostics;
using DotNetCafe.Globalization;
using static DotNetCafe.Internals.CepFormatInfo;

namespace DotNetCafe.Internals
{
    internal static class CepParser
    {
        private enum ParseExceptionKind
        {
            None,
            Format
        }

        private struct ParseResult
        {
            internal int result;
            internal string paramName;
            internal ParseExceptionKind exceptionKind;

            internal bool Succeed => exceptionKind == ParseExceptionKind.None;

            internal Exception GetException()
            {
                switch (exceptionKind)
                {                   
                    case ParseExceptionKind.Format:
                        return new FormatException(SR.FormatException_InvalidCepFormat);
                    
                    case ParseExceptionKind.None:
                    default:
                        return new InvalidOperationException();
                }
            }
        }

        public static Cep Parse(string s)
        {
            ParseResult pr = new ParseResult();
            pr.paramName = nameof(s);

            TryParseCep(ref pr, s);

            if (pr.Succeed)
            {
                return new Cep(pr.result);
            }

            throw pr.GetException();
        }

        public static bool TryParse(string s, out Cep result)
        {
            ParseResult pr = new ParseResult();
            pr.paramName = nameof(s);

            TryParseCep(ref pr, s);

            result = new Cep(pr.result);
            return pr.Succeed;
        }

        private static void TryParseCep(ref ParseResult pr, ReadOnlySpan<char> source)
        {
            if (source.Length == 0 ||
                source.Length != GeneralFormatLength &&
                source.Length != NumericFormatLength)
            {
                Debug.WriteLine("FAIL: invalid length.");
                pr.exceptionKind = ParseExceptionKind.Format;
                return;
            }

            if (!MaskParser.TryParse(source, GetFormat(source.Length), NumericFormatLength, 
                out Span<char> numeric))
            {
                Debug.WriteLine("FAIL: invalid format.");
                pr.exceptionKind = ParseExceptionKind.Format;
                return;
            }

            pr.result = Int32.Parse(numeric);
        }

        private static string GetFormat(int length)
        {
            return length switch
            {
                GeneralFormatLength => GeneralFormatMask,
                NumericFormatLength => NumericFormatMask,
                _ => throw new InvalidOperationException()
            };
        }
    }
}