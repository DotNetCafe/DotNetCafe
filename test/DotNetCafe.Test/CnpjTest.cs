using System;
using System.Collections.Generic;
using Xunit;
using DotNetCafe.Globalization;

namespace DotNetCafe.Test
{
    public class CnpjTest
    {
        #region Constants

        const long A_NUMBER = 191L;
        const long B_NUMBER = 272L;
        const long C_NUMBER = 353L;
        const long D_NUMBER = 434L;

        const string A_STRING = "00.000.000/0001-91";
        const string B_STRING = "00000000000272";
        const string C_STRING = "000.000.000/0003-53";
        const string D_STRING = "000000000000434";
        
        #endregion

        #region Helpers

        public static IEnumerable<object[]> ParseData => new List<object[]>
        {
            new object[] { A_STRING, new Cnpj(A_NUMBER) },
            new object[] { B_STRING, new Cnpj(B_NUMBER) },
            new object[] { C_STRING, new Cnpj(C_NUMBER) },
            new object[] { D_STRING, new Cnpj(D_NUMBER) }
        };

        public static IEnumerable<object[]> ParseThrowsExceptionData => new List<object[]>
        {
            new object[] { "", typeof(FormatException), SR.FormatException_InvalidCnpjFormat },
            new object[] { "00?000.000/0000-00", typeof(FormatException), SR.FormatException_InvalidCnpjFormat },
            new object[] { "00.000.000/0000-00", typeof(ArgumentException), SR.ArgumentException_InvalidCnpjNumber },
            new object[] { "00.000.000/0001-00", typeof(ArgumentException), SR.ArgumentException_InvalidCnpjNumber },
            new object[] { "00.000.000/0001-90", typeof(ArgumentException), SR.ArgumentException_InvalidCnpjNumber }
        };

        public static IEnumerable<object[]> InvalidTryParseData => new List<object[]>
        {
            new object[] { "" },
            new object[] { "00?000.000/0000-00" },
            new object[] { "00.000.000/0000-00" },
            new object[] { "00.000.000/0001-00" },
            new object[] { "00.000.000/0001-90" },
        };

        #endregion

        #region IsEmpty

        [Fact]
        public void TestIsEmpty()
        {
            Assert.Equal(Cnpj.Empty, new Cnpj());
        }

        #endregion

        #region Constructor

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestConstructor(string s, Cnpj expected)
        {
            var actual = new Cnpj(s);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ParseThrowsExceptionData))]
        public void TestConstructorThrowsException(string s, Type type, string message)
        {
            var actual = Assert.Throws(type, () => new Cnpj(s));

            Assert.StartsWith(message, actual.Message);
        }

        #endregion

        #region Parse & TryParse

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestParse(string s, Cnpj expected)
        {
            var actual = Cnpj.Parse(s);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ParseThrowsExceptionData))]
        public void TestParseThrowsException(string s, Type type, string message)
        {
            var actual = Assert.Throws(type, () => Cnpj.Parse(s));

            Assert.StartsWith(message, actual.Message);
        }

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestTryParse(string s, Cnpj expected)
        {
            bool canParse = Cnpj.TryParse(s, out Cnpj actual);

            Assert.True(canParse);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(InvalidTryParseData))]
        public void TestInvalidTryParse(string s)
        {
            bool canParse = Cnpj.TryParse(s, out Cnpj actual);

            Assert.False(canParse);
            Assert.Equal(Cnpj.Empty, actual);
        }

        #endregion

        #region ToString

        [Fact]
        public void TestToString()
        {
            var a = new Cnpj(A_NUMBER);

            Assert.Equal(A_STRING, a.ToString());
        }

        [Fact]
        public void TestToStringNumericFormat()
        {
            var b = new Cnpj(B_NUMBER);

            Assert.Equal(B_STRING, b.ToString("N"));
        }

        #endregion

        #region Comparison

        [Fact]
        public void TestCompareToNull()
        {
            var a = new Cnpj(A_NUMBER);

            Assert.True(a.CompareTo(null!) > 0);
        }

        [Fact]
        public void TestCompareToObject()
        {
            var a = new Cnpj(A_NUMBER);

            Assert.True(a.CompareTo(new object()) > 0);
        }

        [Fact]
        public void TestLesserCompareToGreater()
        {
            var a = new Cnpj(A_NUMBER);
            var b = new Cnpj(B_NUMBER);

            Assert.True(a.CompareTo(b) < 0);
        }

        [Fact]
        public void TestGreaterCompareToLesser()
        {
            var c = new Cnpj(C_NUMBER);
            var d = new Cnpj(D_NUMBER);

            Assert.True(d.CompareTo(c) > 0);
        }

        [Fact]
        public void TestCompareToSame()
        {
            var a1 = new Cnpj(A_NUMBER);
            var a2 = new Cnpj(A_NUMBER);

            Assert.True(a1.CompareTo(a2) == 0);
        }

        #endregion

        #region Equality

        [Fact]
        public void TestEquals()
        {
            var a1 = new Cnpj(A_NUMBER);
            var a2 = new Cnpj(A_NUMBER);

            Assert.True(a1.Equals(a2));
        }

        [Fact]
        public void TestGetHashCode()
        {
            var a = new Cnpj(A_NUMBER);
            var b = new Cnpj(A_NUMBER);

            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        #endregion

        #region Comparison Operators

        [Fact]
        public void TestOpGreaterThan()
        {
            var d = new Cnpj(D_NUMBER);
            var c = new Cnpj(C_NUMBER);

            Assert.True(d > c);
        }

        [Fact]
        public void TestOpLesserThan()
        {
            var a = new Cnpj(A_NUMBER);
            var b = new Cnpj(B_NUMBER);

            Assert.True(a < b);
        }

        [Fact]
        public void TestOpGreaterOrEqualTo()
        {
            var a = new Cnpj(C_NUMBER);
            var b = new Cnpj(C_NUMBER);
            var c = new Cnpj(B_NUMBER);

            Assert.True(a >= b && a >= c);
        }

        [Fact]
        public void TestOpLesserOrEqualTo()
        {
            var a = new Cnpj(A_NUMBER);
            var b = new Cnpj(A_NUMBER);
            var c = new Cnpj(B_NUMBER);

            Assert.True(a <= b && a <= c);
        }

        #endregion

        #region Equality Operators

        [Fact]
        public void TestOpEqualTo()
        {
            var a1 = new Cnpj(A_NUMBER);
            var a2 = new Cnpj(A_NUMBER);

            Assert.True(a1 == a2);
        }

        [Fact]
        public void TestOpNotEqualTo()
        {
            var a = new Cnpj(A_NUMBER);
            var b = new Cnpj(B_NUMBER);

            Assert.True(a != b);
        }

        #endregion
    }
}
