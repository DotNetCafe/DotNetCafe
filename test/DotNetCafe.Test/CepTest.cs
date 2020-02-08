using System;
using System.Collections.Generic;
using DotNetCafe.Globalization;
using Xunit;

namespace DotNetCafe.Test
{
    public class CepTest
    {
        #region Constants

        const int A_NUMBER = 1310918;
        const int B_NUMBER = 22010000;

        const string A_STRING = "01310-918";
        const string B_STRING = "22010000";

        #endregion

        #region Helpers

        public static IEnumerable<object[]> ParseData =>
            new List<object[]>
            {
                new object[] { A_STRING, new Cep(A_NUMBER) },
                new object[] { B_STRING, new Cep(B_NUMBER) }
            };
        
        public static IEnumerable<object[]> InvalidParseData =>
            new List<object[]>
            {
                new object[] { "INVALID", typeof(FormatException), SR.FormatException_InvalidCepFormat },
                new object[] { "01310?918", typeof(FormatException), SR.FormatException_InvalidCepFormat }
            };

        public static IEnumerable<object[]> InvalidTryParseData =>
            new List<object[]>
            {
                new object[] { "INVALID" },
                new object[] { "01310?918" }
            };

        #endregion

        #region IsEmpty

        [Fact]
        public void TestIsEmpty()
        {
            Assert.Equal(Cep.Empty, new Cep());
        }
            
        #endregion

        #region Constructor
        
        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestConstructor(string s, Cep expected)
        {
            var actual = new Cep(s);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(InvalidParseData))]
        public void TestConstructorThrowsException(string s, Type type, string message)
        {
            var actual = Assert.Throws(type, () => new Cep(s));

            Assert.StartsWith(message, actual.Message);
        }

        #endregion

        #region Parse & TryParse

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestParse(string s, Cep expected)
        {
            var actual = Cep.Parse(s);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(InvalidParseData))]
        public void TestParseInvalidFormatThrowsException(string s, Type type, string message)
        {
            var actual = Assert.Throws(type, () => Cep.Parse(s));

            Assert.Equal(message, actual.Message);
        }

        [Theory]
        [MemberData(nameof(ParseData))]
        public void TestTryParse(string s, Cep expected)
        {
            bool canParse = Cep.TryParse(s, out Cep actual);

            Assert.True(canParse);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(InvalidTryParseData))]
        public void TestInvalidTryParse(string s)
        {
            bool canParse = Cep.TryParse(s, out Cep actual);
            
            Assert.False(canParse);
            Assert.Equal(Cep.Empty, actual);
        }
            
        #endregion

        #region ToString
        
        [Fact]
        public void TestToString()
        {
            var a = new Cep(A_NUMBER);

            Assert.Equal(A_STRING, a.ToString());
        }

        [Fact]
        public void TestToStringNumericFormat()
        {
            var b = new Cep(B_NUMBER);

            Assert.Equal(B_STRING, b.ToString("N"));
        }

        #endregion

        #region Comparison

        [Fact]
        public void TestCompareToNull()
        {
            var a = new Cep(A_NUMBER);

            Assert.Equal(1, a.CompareTo(null!));
        }

        [Fact]
        public void TestCompareToObject()
        {
            var a = new Cep(A_NUMBER);

            Assert.Equal(1, a.CompareTo(new object()));
        }

        [Fact]
        public void TestLesserCompareToGreater()
        {
            var a = new Cep(A_NUMBER);
            var b = new Cep(B_NUMBER);

            Assert.Equal(-1, a.CompareTo(b));
        }

        [Fact]
        public void TestGreaterCompareToLesser()
        {
            var a = new Cep(A_NUMBER);
            var b = new Cep(B_NUMBER);

            Assert.Equal(1, b.CompareTo(a));
        }

        [Fact]
        public void TestCompareToSame()
        {
            var a1 = new Cep(A_NUMBER);
            var a2 = new Cep(A_NUMBER);

            Assert.True(a1.CompareTo(a2) == 0);
        }

        #endregion

        #region Equality

        [Fact]
        public void TestEquals()
        {
            var a = new Cep(A_NUMBER);
            var b = new Cep(A_NUMBER);

            Assert.True(a.Equals(b));
        }

        [Fact]
        public void TestGetHashCode()
        {
            var a = new Cep(A_NUMBER);
            var b = new Cep(A_NUMBER);

            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        #endregion

        #region Comparison Operators

        [Fact]
        public void TestOpGreaterThan()
        {
            var a = new Cep(B_NUMBER);
            var b = new Cep(A_NUMBER);

            Assert.True(a > b);
        }

        [Fact]
        public void TestOpLesserThan()
        {
            var a = new Cep(A_NUMBER);
            var b = new Cep(B_NUMBER);

            Assert.True(a < b);
        }

        [Fact]
        public void TestOpGreaterOrEqualTo()
        {
            var a = new Cep(B_NUMBER);
            var b = new Cep(B_NUMBER);
            var c = new Cep(A_NUMBER);

            Assert.True(a >= b && a >= c);
        }

        [Fact]
        public void TestOpLesserOrEqualTo()
        {
            var a = new Cep(A_NUMBER);
            var b = new Cep(A_NUMBER);
            var c = new Cep(B_NUMBER);

            Assert.True(a <= b && a <= c);
        }

        #endregion

        #region Equality Operators

        [Fact]
        public void TestOpEqualTo()
        {
            var a = new Cep(A_NUMBER);
            var b = new Cep(A_NUMBER);

            Assert.True(a == b);
        }

        [Fact]
        public void TestOpNotEqualTo()
        {
            var a = new Cep(A_NUMBER);
            var b = new Cep(B_NUMBER);

            Assert.True(a != b);
        }

        #endregion
    }
}