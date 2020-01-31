using System;
using DotNetCafe.Internals;

namespace DotNetCafe
{
    public readonly struct Cpf : IComparable, IComparable<Cpf>, IEquatable<Cpf>, IFormattable
    {
        public static readonly Cpf Empty;

        internal readonly long number;

        internal Cpf(long number)
        {
            this.number = number;
        }

        public Cpf(string s)
        {
            this = CpfParser.Parse(s);
        }

        public int CompareTo(object obj) =>
            CpfComparer.Compare(this, obj);
        
        public int CompareTo(Cpf other) =>
            CpfComparer.Compare(this, other);
        
        public bool Equals(Cpf value) =>
            CpfEqualityComparer.Equal(this, value);
        
        public override bool Equals(object obj) =>
            CpfEqualityComparer.Equal(this, obj);
        
        public override int GetHashCode() =>
            CpfEqualityComparer.GetHashCode(this);
        
        public override string ToString() =>
            CpfFormatter.Format(this, null!, null!);

        public string ToString(string format) =>
            CpfFormatter.Format(this, format, null!);
        
        public string ToString(IFormatProvider formatProvider) =>
            CpfFormatter.Format(this, null!, formatProvider);
        
        public string ToString(string format, IFormatProvider formatProvider) =>
            CpfFormatter.Format(this, format, formatProvider);
        
        public static Cpf Parse(string s) =>
            CpfParser.Parse(s);
        
        public static bool TryParse(string s, out Cpf result) =>
            CpfParser.TryParse(s, out result);
        
        public static bool IsEmpty(Cpf other) =>
            CpfEqualityComparer.Equal(Empty, other);
        
        public static bool operator ==(Cpf lhs, Cpf rhs)
        {
            return CpfEqualityComparer.Equal(lhs, rhs);
        }

        public static bool operator !=(Cpf lhs, Cpf rhs)
        {
            return !CpfEqualityComparer.Equal(lhs, rhs);
        }

        public static bool operator >(Cpf lhs, Cpf rhs)
        {
            return CpfComparer.Compare(lhs, rhs) > 0;
        }

        public static bool operator <(Cpf lhs, Cpf rhs)
        {
            return CpfComparer.Compare(lhs, rhs) < 0;
        }

        public static bool operator >=(Cpf lhs, Cpf rhs)
        {
            return CpfComparer.Compare(lhs, rhs) >= 0;
        }

        public static bool operator <=(Cpf lhs, Cpf rhs)
        {
            return CpfComparer.Compare(lhs, rhs) <= 0;
        }
    }
}
