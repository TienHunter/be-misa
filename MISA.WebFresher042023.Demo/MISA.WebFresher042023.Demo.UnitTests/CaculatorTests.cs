using NUnit.Framework;

namespace MISA.WebFresher042023.Demo.UnitTests
{
    [TestFixture]
    public class CaculatorTests
    {

        [TestCase(4, 5, 9)]
        [TestCase(int.MaxValue, 1, (long)int.MaxValue + 1)]
        public void Add_ValidInput_Success(int a, int b, long expectedResult)
        {

            // Arrange


            // Act
            var actualResult = new Caculator().Add(a, b);

            // Assert

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }


        [TestCase(5, 3, 2)]
        [TestCase(int.MaxValue, int.MinValue, (long)2 * int.MaxValue + 1)]
        public void Sub_ValidInput_Success(int a, int b, long expectedResult)
        {

            // Arrange


            // Act
            var actualResult = new Caculator().Sub(a, b);

            // Assert

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }



        [TestCase(int.MaxValue, int.MinValue, (long)int.MaxValue * int.MinValue)]
        [TestCase(5, 2, (long)5 * 2)]

        public void Mul_ValidInput_Success(int a, int b, long expectedResult)
        {

            // Arrange


            // Act
            var actualResult = new Caculator().Mul(a, b);

            // Assert

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }



        [TestCase(5, 6, (double)5 / 6)]
        [TestCase(4, 5, 0.8)]
        [TestCase(5, 3, 1.6666666666666667)]

        public void Div_ValidInput_Success(int a, int b, double expectedResult)
        {

            // Arrange


            // Act
            var actualResult = new Caculator().Div(a, b);

            // Assert

            Assert.That(Math.Abs(actualResult - expectedResult), Is.LessThan(10e-6));
        }

        [Test]
        public void Div_InValidInput_Exception()
        {

            // Arrange
            var a = 5;
            var b = 0;
            var expectedMessage = "Không được chia cho 0";

            // Act
            var handler = () => new Caculator().Div(a, b);

            // Assert
            var exception = Assert.Throws<Exception>(() => handler());
            Assert.That(expectedMessage, Is.EqualTo(exception.Message));
        }
    }
}