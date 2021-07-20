using Calculator;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace CalculatorTests
{
    public class LogicTests
    {
        private ICalculatorLogic _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator.Calculator();
        }

        [Test]
        public void TestSum()
        {
            Assert.AreEqual(7, _calculator.Sum(2, 5));
            Assert.AreEqual(7.1, _calculator.Sum(2.1, 5));
            Assert.AreEqual(-0, _calculator.Sum(-1, 1));

        }

        [Test]
        public void Substract()
        {
            Assert.AreEqual(7, _calculator.Substract(12, 5));
            Assert.AreEqual(7.4, _calculator.Substract(10, 2.6));
            Assert.AreEqual(-0, _calculator.Substract(1, 1));
            Assert.AreEqual(-2, _calculator.Substract(0, 2));
            Assert.AreEqual(-4.2, _calculator.Substract(0, 4.2));
            Assert.AreEqual(2, _calculator.Substract(0, -2));
        }

        [Test]
        public void Multiplicate()
        {
            Assert.AreEqual(4, _calculator.Multiplicate(2, 2));
            Assert.AreEqual(5, _calculator.Multiplicate(2, 2.5));
            Assert.AreEqual(-0, _calculator.Multiplicate(0, 1));
            Assert.AreEqual(-5, _calculator.Multiplicate(2, -2.5));
        }

        [Test]
        public void Divide()
        {
            Assert.AreEqual(1, _calculator.Divide(12, 12));
            Assert.AreEqual(2.5, _calculator.Divide(5, 2));
            Assert.IsNaN(_calculator.Divide(0, 0));
            Assert.IsTrue(double.IsInfinity(_calculator.Divide(5, 0)));
            Assert.AreEqual(-2.5, _calculator.Divide(5, -2));
        }

        [Test]
        public void Sqrt()
        {
            Assert.AreEqual(3, _calculator.Sqrt(9));
            Assert.IsNaN(_calculator.Sqrt(-25));
            Assert.AreEqual(0, _calculator.Sqrt(0));
            Assert.AreEqual(1, _calculator.Sqrt(1));
        }

        [Test]
        public void Cbrt()
        {
            Assert.AreEqual(5, _calculator.Cbrt(125));
            Assert.AreEqual(3, _calculator.Cbrt(27));
            Assert.AreEqual(0, _calculator.Cbrt(0));
            Assert.AreEqual(1, _calculator.Cbrt(1));
        }

        [Test]
        public void Exp()
        {
            Assert.AreEqual("1e-1", _calculator.Exp(0.1));
            Assert.AreEqual("1e+0", _calculator.Exp(1));
            Assert.AreEqual("-1e-1", _calculator.Exp(-0.1));
            Assert.AreEqual("-1e+0", _calculator.Exp(-1));

            Assert.AreEqual("-9.9999999567e+7", _calculator.Exp(-99999999.567));
        }

        [Test]
        public void Factorial()
        {
            Assert.AreEqual(720, _calculator.Fact(6));
            Assert.AreEqual(1, _calculator.Fact(0));
            Assert.Throws(Is.TypeOf<ArithmeticException>()
                 .And.Message.EqualTo("Число меньше нуля"), () => _calculator.Fact(-1));
        }

        [Test]
        public void ToHex()
        {
            Assert.Throws<ArgumentNullException>(() => _calculator.ToHex(null, 0x0));

            var expected = "00 01";

            var mockHexCalculator = new Mock<IHexCalculator>();

            mockHexCalculator.Setup(mock => mock.ToHex(It.IsAny<int>())).Returns(expected);

            var actual = _calculator.ToHex(mockHexCalculator.Object, 0);

            Assert.AreEqual(expected, actual);
            mockHexCalculator.Verify(m => m.ToHex(It.IsAny<int>()), Times.Once());
        }
    }
}