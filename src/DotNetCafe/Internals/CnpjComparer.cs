namespace DotNetCafe.Internals
{
    internal static class CnpjComparer
    {
        public static int Compare(object lhs, object rhs)
        {
            if (!(lhs is Cnpj))
            {
                return -1;
            }

            if (!(rhs is Cnpj))
            {
                return 1;
            }

            return Compare((Cnpj) lhs, (Cnpj) rhs);
        }

        public static int Compare(Cnpj lhs, Cnpj rhs)
        {
            long x = lhs.value;
            long y = rhs.value;

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