using Calculator;
using NUnit.Framework;

namespace CalculatorTests
{
    public class FormattersTests
    {
        private FactorialFormatter _factorialFormatter;

        [SetUp]
        public void Setup()
        {
            _factorialFormatter = new FactorialFormatter();
        }

        [Test]
        public void TestFactorialFormatter()
        {
            Assert.AreEqual("6! = 720", _factorialFormatter.Format((6, 720)));
        }
    }
}