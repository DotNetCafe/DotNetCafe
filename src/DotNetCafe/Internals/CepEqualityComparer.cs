namespace DotNetCafe.Internals
{
    internal static class CepEqualityComparer
    {
        public static bool Equal(Cep lhs, object rhs)
        {
            return rhs is Cep ? lhs.Equals(rhs) : false;
        }

        public static bool Equal(Cep lhs, Cep rhs)
        {
            return lhs.number == rhs.number;
        }

        public static int GetHashCode(Cep obj)
        {
            return (typeof(Cep), obj.number).GetHashCode();
        }
    }
}