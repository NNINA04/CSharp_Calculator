using Calculator;
using NUnit.Framework;

namespace CalculatorTests
{
    class FormatterUseCase
    {
        [Test]
        public void Operation_Formatter_Run([Range(-100, 100)] int x)
        {
            Assert.AreEqual($"{x}! = {x}", new FactorialFormatter().Format((x, x)));
        }
    }
}
