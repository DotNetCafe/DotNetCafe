namespace DotNetCafe.Internals
{
    internal static class CpfEqualityComparer
    {
        public static bool Equals(Cpf lhs, object rhs)
        {
            return rhs is Cpf ? Equals(lhs, (Cpf) rhs) : false;
        }

        public static bool Equals(Cpf lhs, Cpf rhs)
        {
            return lhs.number == rhs.number;
        }

        public static int GetHashCode(Cpf obj)
        {
            return (typeof(Cpf), obj.number).GetHashCode();
        }
    }    
}
