using System;

namespace DotNetCafe.Internals
{
    internal static class MaskParser
    {
        public static bool TryParse(ReadOnlySpan<char> source, ReadOnlySpan<char> format, 
            int length, out Span<char> result)
        {
            result = new char[length];

            for (int f = 0, s = 0, r = 0; s < source.Length; f++)
            {
                switch (format[f])
                {
                    case '0':
                        if (Char.IsDigit(source[s]))
                        {
                            result[r++] = source[s++];
                            continue;
                        }
                        return false;
                    
                    case '\\':
                        if (format[++f] == source[s++])
                        {
                            continue;
                        }
                        return false;
                    
                    default:
                        throw new InvalidOperationException();
                }
            }
            
            return true;
        }
    }
}
