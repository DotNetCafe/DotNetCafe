using System;
using Xunit;

namespace DotNetCafe.Test
{
    public class CpfTest
    {
        #region Constants

        const long A_NUMBER = 100_100_100_00L;
        const long B_NUMBER = 200_200_200_00L;
        const long C_NUMBER = 300_300_300_00L;

        #endregion

        #region IsEmpty

        [Fact]
        public void TestIsEmpty()
        {
            Assert.Equal(Cpf.Empty, new Cpf());
        }

        #endregion

        #region Constructor

        [Fact(Skip = "Not Implemented")]
        public void TestConstructor()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Not Implemented")]
        public void TestConstructorThrowsException()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Parse & TryParse

        [Fact(Skip = "Not Implemented")]
        public void TestParse()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Not Implemented")]
        public void TestParseThrowsException()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Not Implemented")]
        public void TestTryParse()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ToString

        [Fact(Skip = "Not Implemented")]
        public void TestToString()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Comparison
            
        [Fact(Skip = "Not Implemented")]
        public void TestCompareTo()
        {
            throw new NotImplementedException();
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

        [Fact(Skip = "Not Implemented")]
        public void TestOpGreaterThan()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Not Implemented")]
        public void TestOpLesserThan()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Not Implemented")]
        public void TestOpGreaterOrEqualTo()
        {
            throw new NotImplementedException();
        }

        [Fact(Skip = "Not Implemented")]
        public void TestOpLesserOrEqualTo()
        {
            throw new NotImplementedException();
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
