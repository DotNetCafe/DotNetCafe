using System.Runtime.CompilerServices;

namespace DotNetCafe.Internals
{
    internal static class CepComparer
    {
        public static int Compare(Cep lhs, object rhs)
        {
            return rhs is Cep ? Compare(lhs, (Cep) rhs) : 1;
        }

        public static int Compare(Cep lhs, Cep rhs)
        {
            if (lhs.number > rhs.number)
            {
                return 1;
            }

            if (lhs.number < rhs.number)
            {
                return -1;
            }

            return 0;
        }
    }
}