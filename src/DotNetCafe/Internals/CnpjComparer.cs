namespace DotNetCafe.Internals
{
    internal static class CnpjComparer
    {
        public static int Compare(Cnpj lhs, object rhs)
        {
            if (!(rhs is Cnpj))
            {
                return 1;
            }

            return Compare(lhs, (Cnpj) rhs);
        }

        public static int Compare(Cnpj lhs, Cnpj rhs)
        {
            long x = lhs.number;
            long y = rhs.number;

            if (x > y)
            {
                return 1;
            }

            if (x < y)
            {
                return -1;
            }

            return 0;
        }
    }
}