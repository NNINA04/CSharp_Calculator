using Calculator;
using NUnit.Framework;

namespace CalculatorTests
{
    public class AdaptersTests
    {
        private FactorialProcessAdapter _factorialProcessAdapter;

        [SetUp]
        public void Setup()
        {
            _factorialProcessAdapter = new FactorialProcessAdapter(new Calculator.Calculator());
        }

        [Test]
        public void TestFactorialFormatter()
        {
            Assert.AreEqual((6, 720), _factorialProcessAdapter.Factorial(6));
            Assert.AreEqual((0, 1), _factorialProcessAdapter.Factorial(0));
        }
    }
}