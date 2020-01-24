using System;
using System.Collections.Generic;
using Xunit;

namespace DotNetCafe.Test
{
    public class CnpjTest
    {
        #region Constants

        const long A_NUMBER = 191L;
        const long B_NUMBER = 272L;
        const long C_NUMBER = 353L;
        const string GENERAL_FORMAT = "G";
        const string NUMERIC_FORMAT = "N";
        const string A_GENERAL_FORMAT = "00.000.000/0001-91";
        const string A_NUMERIC_FORMAT = "00000000000191";
        const string B_GENERAL_FORMAT = "00.000.000/0002-72";
        const string B_NUMERIC_FORMAT = "00000000000272";
        const string C_GENERAL_FORMAT = "00.000.000/0003-53";
        const string C_NUMERIC_FORMAT = "00000000000353";

        #endregion

        #region Helpers

        public static readonly Cnpj A = new Cnpj(A_NUMBER);
        public static readonly Cnpj B = new Cnpj(B_NUMBER);
        public static readonly Cnpj C = new Cnpj(C_NUMBER);

        public static IEnumerable<object[]> ParseData => new List<object[]>
            {
                new object[] { "000.000.000/0001-91", A },
                new object[] { "000.000.000/0002-72", B },
                new object[] { "000.000.000/0003-53", C },
                new object[] { "00.000.000/0001-91",  A },
                new object[] { "00.000.000/0002-72",  B },
                new object[] { "00.000.000/0003-53",  C },
                new object[] { "000000000000191", A },
                new object[] { "000000000000272", B },
                new object[] { "000000000000353", C },
                new object[] { "00000000000191", A },
                new object[] { "00000000000272", B },
                new object[] { "00000000000353", C }        
            };

        public static IEnumerable<object[]> InvalidParseData => new List<object[]>
            {
                new object[] { "000.000.000/0000-000", typeof(FormatException), "O formato do CNPJ é inválido" },
                new object[] { "00.000.000/0000?00", typeof(FormatException), "O formato do CNPJ é inválido" },
                new object[] { "00.000.000?0000-00", typeof(FormatException), "O formato do CNPJ é inválido" },
                new object[] { "00.000?000/0000-00", typeof(FormatException), "O formato do CNPJ é inválido" },
                new object[] { "00?000.000/0000-00", typeof(FormatException), "O formato do CNPJ é inválido" },
                new object[] { "00.000.000/0000-00", typeof(ArgumentException), "O número do CNPJ é inválido" },
                new object[] { "00.000.000/0001-00", typeof(ArgumentException), "O número do CNPJ é inválido" },
                new object[] { "00.000.000/0001-90", typeof(ArgumentException), "O número do CNPJ é inválido" }
            };

        public static IEnumerable<object[]> InvalidTryParseData => new List<object[]>
            {
                new object[] { "000.000.000/0000-000" },
                new object[] { "00.000.000/0000?00" },
                new object[] { "00.000.000?0000-00" },
                new object[] { "00.000?000/0000-00" },
                new object[] { "00?000.000/0000-00" },
                new object[] { "00.000.000/0001-00" },
                new object[] { "00.000.000/0001-90" }
            };

        public static IEnumerable<object[]> ToStringData => new List<object[]>
            {
                new object[] { A_GENERAL_FORMAT, A },
                new object[] { B_GENERAL_FORMAT, B },
                new object[] { C_GENERAL_FORMAT, C }
            };

        public static IEnumerable<object[]> ToStringWithFormatArgData => new List<object[]>
            {
                new object[] { A_GENERAL_FORMAT, A, null! },
                new object[] { A_GENERAL_FORMAT, A, GENERAL_FORMAT },
                new object[] { A_NUMERIC_FORMAT, A, NUMERIC_FORMAT },
                new object[] { B_GENERAL_FORMAT, B, null! },
                new object[] { B_GENERAL_FORMAT, B, GENERAL_FORMAT },
                new object[] { B_NUMERIC_FORMAT, B, NUMERIC_FORMAT },
                new object[] { C_GENERAL_FORMAT, C, null! },
                new object[] { C_GENERAL_FORMAT, C, GENERAL_FORMAT },
                new object[] { C_NUMERIC_FORMAT, C, NUMERIC_FORMAT },
            };
        
        public static IEnumerable<object[]> EqualToData => new List<object[]>
            {   
                new object[] { new Cnpj(A_NUMBER), A },
                new object[] { new Cnpj(B_NUMBER), B },
                new object[] { new Cnpj(C_NUMBER), C }
            };
        
        public static IEnumerable<object[]> NotEqualToData => new List<object[]>
            {
                new object[] { A, B },
                new object[] { B, C },
                new object[] { C, A }
            };

        #endregion

        #region IsEmpty

        [Fact]
        public void TestIsEmpty()
        {
            Assert.True(Cnpj.IsEmpty(Cnpj.Empty));
        }

        #endregion

        #region Constructor 

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestConstructor(string s, Cnpj expected)
        {
            Assert.Equal(expected, new Cnpj(s));
        }

        [Theory]
        [MemberData(nameof(InvalidParseData))]
        public void TestConstructorThrowsException(string s, Type exceptionType, string message)
        {
            var exception = Assert.Throws(exceptionType, () => new Cnpj(s));
            Assert.StartsWith(message, exception.Message);
        }

        #endregion
        
        #region Parse & TryParse

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestParse(string s, Cnpj expected)
        {
            Assert.Equal(expected, Cnpj.Parse(s));
        }

        [Theory]
        [MemberData(nameof(InvalidParseData))]
        public void TestParseThrowsException(string s, Type exceptionType, string message)
        {
            var exception = Assert.Throws(exceptionType, () => Cnpj.Parse(s));
            Assert.StartsWith(message, exception.Message);
        }

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestTryParse(string s, Cnpj expected)
        {
            Assert.True(Cnpj.TryParse(s, out Cnpj result));
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(InvalidTryParseData))]
        public void TestTryParseReturnFalse(string s)
        {
            Assert.False(Cnpj.TryParse(s, out Cnpj result));
            Assert.Equal(Cnpj.Empty, result);
        }

        #endregion

        #region ToString

        [Theory]
        [MemberData(nameof(ToStringData))]
        public void TestToString(string expected, Cnpj obj)
        {
            Assert.Equal(expected, obj.ToString());
        }

        [Theory]
        [MemberData(nameof(ToStringWithFormatArgData))]
        public void TestToStringWithFormatArg(string expected, Cnpj obj, string format)
        {
            Assert.Equal(expected, obj.ToString(format));
        }

        [Fact]
        public void TestToStringWithInvalidFormatArg()
        {
            var exception = Assert.Throws<FormatException>(() => A.ToString("?"));
            Assert.Equal("The format '?' was not supported.", exception.Message);
        }

        #endregion

        #region Comparison

        [Fact]
        public void TestCompareToNull()
        {
            Assert.Equal(1, A.CompareTo(null!));
        }

        [Fact]
        public void TestCompareToObject()
        {
            Assert.Equal(1, A.CompareTo(new object()));
        }

        [Fact]
        public void TestCompareTo()
        {
            Assert.True(A.CompareTo(A) == 0);
            Assert.True(B.CompareTo(B) == 0);
            Assert.True(C.CompareTo(C) == 0);

            Assert.True(A.CompareTo(B) < 0);
            Assert.True(B.CompareTo(C) < 0);

            Assert.True(B.CompareTo(A) > 0);
            Assert.True(C.CompareTo(A) > 0);
        }

        #endregion

        #region Equality

        [Theory]
        [MemberData(nameof(EqualToData))]
        public void TestEquals(Cnpj lhs, Cnpj rhs)
        {
            Assert.True(lhs.Equals(rhs));
        }

        [Fact]
        public void TestGetHashCode()
        {
            int sameAsA = new Cnpj(A_NUMBER).GetHashCode();
            int sameAsB = new Cnpj(B_NUMBER).GetHashCode();
            int sameAsC = new Cnpj(C_NUMBER).GetHashCode();

            Assert.True(sameAsA.GetHashCode() == A.GetHashCode());
            Assert.True(sameAsB.GetHashCode() == B.GetHashCode());
            Assert.True(sameAsC.GetHashCode() == C.GetHashCode());

            Assert.False(A.GetHashCode() == B.GetHashCode());
            Assert.False(B.GetHashCode() == C.GetHashCode());
            Assert.False(C.GetHashCode() == A.GetHashCode());
        }

        #endregion

        #region Comparison Operators

        [Fact]
        public void TestOpGreaterThan()
        {
            Assert.True(B > A);
        }

        [Fact]
        public void TestOpLesserThan()
        {
            Assert.True(A < B);
        }

        [Fact]
        public void TestOpGreaterThanOrEqualTo()
        {
            Cnpj sameAsB = new Cnpj(B_NUMBER);

            Assert.True(B >= A);
            Assert.True(B >= sameAsB);
        }

        [Fact]
        public void TestOpLesserThanOrEqualTo()
        {
            Cnpj sameAsA = new Cnpj(A_NUMBER);

            Assert.True(A <= B);
            Assert.True(A <= sameAsA);
        }

        #endregion

        #region Equality Operators

        [Theory]
        [MemberData(nameof(EqualToData))]
        public void TestOpEqualTo(Cnpj lhs, Cnpj rhs)
        {
            Assert.True(lhs == rhs);
        }

        [Theory]
        [MemberData(nameof(NotEqualToData))]
        public void TestOpNotEqualTo(Cnpj lhs, Cnpj rhs)
        {
            Assert.True(lhs != rhs);
        }

        #endregion
    }
}
