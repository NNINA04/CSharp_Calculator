using Calculator;
using NUnit.Framework;

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
            Assert.AreEqual(7, _calculator.Sum(2,5));
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
    }
}