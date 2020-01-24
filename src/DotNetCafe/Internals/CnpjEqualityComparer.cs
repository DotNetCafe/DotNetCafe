namespace DotNetCafe.Internals
{
    internal static class CnpjEqualityComparer
    {
        public static bool Equals(Cnpj lhs, object rhs)
        {
            return rhs is Cnpj ? Equals(lhs, (Cnpj) rhs) : false;
        }

        public static bool Equals(Cnpj lhs, Cnpj rhs)
        {
            return lhs.value == rhs.value;
        }

        public static int GetHashCode(Cnpj obj)
        {
            return (typeof(Cnpj), obj.value).GetHashCode();
        }
    }
}