using NUnit.Framework;
using Calculator.Operations;

namespace CalculatorTests.OperationTests
{
    class OperationUseCases
    {
        [Test]
        public void Operation_Run([Range(-100, 100)] int x)
        {
            Assert.AreEqual(x + x, new Operation<int>((int x, int y) => x + y).Run(x, x));
        }
        
        [Test]
        public void Operation_Validator_Run([Range(-100, 100)] int x)
        {
            Assert.AreEqual(x+x, new Operation<int>((int x) => x + x).AddValidator((int x)=>(true, string.Empty)).Run(x));
        }

        [Test]
        public void Operation_Formatter_Run([Range(-100, 100)] int x)
        {
            Assert.AreEqual(x + x, new Operation<int>((int x) => x + x).AddFormatter((int x) => x).Run(x));
        }
    }
}
