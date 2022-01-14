using Calculator.Additions;
using Calculator.Interfaces;
using Moq;
using NUnit.Framework;

namespace CalculatorTests
{
    public class AdaptersTests
    {
        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            var calculator = new Mock<ICalculatorLogic>();
            Assert.IsInstanceOf<FactorialOperationAdapter>(new FactorialOperationAdapter(calculator.Object));
        }

        [Test]
        public void Factorial_WithObjectArgument_ReturnsTupleWithInputAndOutputValue()
        {
            int inputValue = 6;
            int outputValue = 720;

            var calculator = new Mock<ICalculatorLogic>();
            calculator.Setup(x => x.Fact(inputValue)).Returns(outputValue);
            Assert.AreEqual((inputValue, outputValue), new FactorialOperationAdapter(calculator.Object).Factorial(6));
            calculator.Verify(x => x.Fact(inputValue), Times.Once);
        }
    }
}