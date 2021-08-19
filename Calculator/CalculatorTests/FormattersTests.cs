using Calculator;
using Calculator.Operations.Formatters;
using NUnit.Framework;

namespace CalculatorTests
{
    public class FormattersTests
    {
        private static FactorialFormatter factorialFormatter = new();

        private int inputValue = 6;
        private int outputValue = 720;

        [Test]
        public void Format_ReturnsString()
        {
            Assert.AreEqual("6! = 720", factorialFormatter.Format((inputValue, outputValue)));
        }

        [Test]
        public void Format_ReturnsStringUsingTypeCasting()
        {
            Assert.AreEqual("6! = 720", ((IFormatter)factorialFormatter).Format((inputValue, outputValue)));
        }
    }
}