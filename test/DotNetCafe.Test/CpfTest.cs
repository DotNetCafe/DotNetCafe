using System;
using System.Collections.Generic;
using DotNetCafe.Globalization;
using Xunit;

namespace DotNetCafe.Test
{
    public class CpfTest
    {
        #region Constants

        const long A_NUMBER = 100_100_100_17L;
        const long B_NUMBER = 200_200_200_23L;
        const long C_NUMBER = 300_300_300_30L;

        const string A_STRING = "100.100.100-17";
        const string B_STRING = "200200200/23";
        const string C_STRING = "30030030030";

        #endregion

        #region Helpers
        
        public static IEnumerable<object[]> ParseData => new List<object[]>
        {
            new object[] { A_STRING, new Cpf(A_NUMBER) },
            new object[] { B_STRING, new Cpf(B_NUMBER) },
            new object[] { C_STRING, new Cpf(C_NUMBER) }
        };

        public static IEnumerable<object[]> InvalidParseData => new List<object[]>
        {
            new object[] { "INVALID", typeof(FormatException), SR.FormatException_InvalidCpfFormat },
            new object[] { "100.100?100-00", typeof(FormatException), SR.FormatException_InvalidCpfFormat },
            new object[] { "100.100.100-07", typeof(ArgumentException), SR.ArgumentException_InvalidCpfNumber },
            new object[] { "100.100.100-10", typeof(ArgumentException), SR.ArgumentException_InvalidCpfNumber }
        };

        public static IEnumerable<object[]> InvalidTryParseData => new List<object[]>
        {
            new object[] { "INVALID" },
            new object[] { "100.100?100-00" },
            new object[] { "100.100.100-07" },
            new object[] { "100.100.100-10" }
        };

        #endregion

        #region IsEmpty

        [Fact]
        public void TestIsEmpty()
        {
            Assert.Equal(Cpf.Empty, new Cpf());
        }

        #endregion

        #region Constructor

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestConstructor(string s, Cpf expected)
        {
            var actual = new Cpf(s);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(InvalidParseData))]
        public void TestConstructorThrowsException(string s, Type type, string message)
        {
            var actual = Assert.Throws(type, () => new Cpf(s));
            
            Assert.Equal(message, actual.Message);
        }

        #endregion

        #region Parse & TryParse

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestParse(string s, Cpf expected)
        {
            var actual = Cpf.Parse(s);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(InvalidParseData))]
        public void TestParseInvalidFormatThrowsException(string s, Type type, string message)
        {
            var actual = Assert.Throws(type, () => Cpf.Parse(s));

            Assert.Equal(message, actual.Message);
        }

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestTryParse(string s, Cpf expected)
        {
            bool canParse = Cpf.TryParse(s, out Cpf actual);

            Assert.True(canParse);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(InvalidTryParseData))]
        public void TestInvalidTryParse(string s)
        {
            bool canParse = Cpf.TryParse(s, out Cpf actual);
            
            Assert.False(canParse);
            Assert.Equal(Cpf.Empty, actual);
        }

        #endregion

        #region ToString

        [Fact]
        public void TestToString()
        {
            var a = new Cpf(A_NUMBER);

            Assert.Equal(A_STRING, a.ToString());
        }

        [Fact]
        public void TestToStringBarFormat()
        {
            var a = new Cpf(B_NUMBER);

            Assert.Equal(B_STRING, a.ToString("B"));
        }

        [Fact]
        public void TestToStringNumericFormat()
        {
            var a = new Cpf(C_NUMBER);

            Assert.Equal(C_STRING, a.ToString("N"));
        }

        [Fact]
        public void TestToStringThrowsException()
        {
            var exception = Assert.Throws<FormatException>(() => new Cpf(A_NUMBER).ToString("?"));
            string expected = string.Format(SR.FormatException_InvalidFormat, "?");

            Assert.Equal(expected, exception.Message);
        }

        #endregion

        #region Comparison
            
        [Fact]
        public void TestCompareToNull()
        {
            var a = new Cpf(A_NUMBER);

            Assert.True(a.CompareTo(null!) > 0);
        }

        [Fact]
        public void TestCompareToObject()
        {
            var a = new Cpf(A_NUMBER);

            Assert.True(a.CompareTo(new object()) > 0);
        }

        #endregion

        #region Equality

        [Fact]
        public void TestEquals()
        {
            var a = new Cpf(A_NUMBER);
            var b = new Cpf(A_NUMBER);

            Assert.True(a.Equals(b));
        }

        [Fact]
        public void TestGetHashCode()
        {
            var a = new Cpf(A_NUMBER);
            var b = new Cpf(A_NUMBER);

            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        #endregion

        #region Comparison Operators

        [Fact]
        public void TestOpGreaterThan()
        {
            var a = new Cpf(C_NUMBER);
            var b = new Cpf(B_NUMBER);

            Assert.True(a > b);
        }

        [Fact]
        public void TestOpLesserThan()
        {
            var a = new Cpf(A_NUMBER);
            var b = new Cpf(B_NUMBER);

            Assert.True(a < b);
        }

        [Fact]
        public void TestOpGreaterOrEqualTo()
        {
            var a = new Cpf(C_NUMBER);
            var b = new Cpf(C_NUMBER);
            var c = new Cpf(B_NUMBER);

            Assert.True(a >= b && a >= c);
        }

        [Fact]
        public void TestOpLesserOrEqualTo()
        {
            var a = new Cpf(A_NUMBER);
            var b = new Cpf(A_NUMBER);
            var c = new Cpf(B_NUMBER);

            Assert.True(a <= b && a <= c);
        }

        #endregion

        #region Equality Operators
        
        [Fact]
        public void TestOpEqualTo()
        {
            var a = new Cpf(A_NUMBER);
            var b = new Cpf(A_NUMBER);

            Assert.True(a == b);
        }

        [Fact]
        public void TestOpNotEqualTo()
        {
            var a = new Cpf(A_NUMBER);
            var b = new Cpf(B_NUMBER);

            Assert.True(a != b);
        }

        #endregion
    }
}
