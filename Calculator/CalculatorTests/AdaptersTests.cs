using Calculator;
using Moq;
using NUnit.Framework;

namespace CalculatorTests
{
    public class AdaptersTests
    {
        [Test]
        public void TestFactorialFormatter()
        {
            int inputValue = 6;
            int outputValue = 720;

            var calculator = new Mock<ICalculatorLogic>();
            calculator.Setup(x=>x.Fact(inputValue)).Returns(outputValue);

            Assert.AreEqual((inputValue, outputValue), new FactorialOperationAdapter(calculator.Object).Factorial(6));
        }
    }
}