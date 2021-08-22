using NUnit.Framework;
using Calculator.Operations;

namespace CalculatorTests.OperationTests
{
    class OperationUseCases
    {
        [Test]
        public void Run_Check_ReturnsCorrectExecution([Range(-100, 100)] int x)
        {
            Assert.AreEqual(x + x, new Operation<int>((int x, int y) => x + y).Run(x, x));
        }
        
        [Test]
        public void Run_CheckOperationWithValidator_ReturnsValue([Range(-100, 100)] int x)
        {
            Assert.AreEqual(x+x, new Operation<int>((int x) => x + x).AddValidator((int x)=>(true, string.Empty)).Run(x));
        }

        [Test]
        public void Run_CheckOperationWithFormatter_Run_ReturnsValue([Range(-100, 100)] int x)
        {
            Assert.AreEqual(x + x, new Operation<int>((int x) => x + x).AddFormatter((int x) => x).Run(x));
        }
    }
}
