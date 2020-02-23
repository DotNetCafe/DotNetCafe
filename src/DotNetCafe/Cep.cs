using System;
using DotNetCafe.Internals;

namespace DotNetCafe
{
    public readonly struct Cep : IComparable, IComparable<Cep>, IEquatable<Cep>, IFormattable
    {
        public static readonly Cep Empty;

        internal readonly int number;

        internal Cep(int number)
        {
            this.number = number;
        }

        public Cep(string s)
        {
            this = CepParser.Parse(s);
        }

        public int CompareTo(object obj) =>
            CepComparer.Compare(this, obj);

        public int CompareTo(Cep other) =>
            CepComparer.Compare(this, other);

        public bool Equals(Cep value) =>
            CepEqualityComparer.Equal(this, value);
        
        public override bool Equals(object obj) =>
            CepEqualityComparer.Equal(this, obj);
        
        public override int GetHashCode() =>
            CepEqualityComparer.GetHashCode(this);
        
        public override string ToString() =>
            CepFormatter.Format(this, null!, null!);
        
        public string ToString(string format) =>
            CepFormatter.Format(this, format, null!);
        
        public string ToString(IFormatProvider formatProvider) =>
            CepFormatter.Format(this, null!, formatProvider);
        
        public string ToString(string format, IFormatProvider formatProvider) =>
            CepFormatter.Format(this, format, formatProvider);
        
        public static Cep Parse(string s) =>
            CepParser.Parse(s);
        
        public static bool TryParse(string s, out Cep result) =>
            CepParser.TryParse(s, out result);
        
        public static bool IsEmpty(Cep other) =>
            CepEqualityComparer.Equal(Empty, other);

        public static bool operator ==(Cep lhs, Cep rhs)
        {
            return CepEqualityComparer.Equal(lhs, rhs);
        }

        public static bool operator !=(Cep lhs, Cep rhs)
        {
            return !CepEqualityComparer.Equal(lhs, rhs);
        }

        public static bool operator >(Cep lhs, Cep rhs)
        {
            return CepComparer.Compare(lhs, rhs) > 0;
        }

        public static bool operator <(Cep lhs, Cep rhs)
        {
            return CepComparer.Compare(lhs, rhs) < 0;
        }

        public static bool operator >=(Cep lhs, Cep rhs)
        {
            return CepComparer.Compare(lhs, rhs) >= 0;
        }

        public static bool operator <=(Cep lhs, Cep rhs)
        {
            return CepComparer.Compare(lhs, rhs) <= 0;
        }
    }
}