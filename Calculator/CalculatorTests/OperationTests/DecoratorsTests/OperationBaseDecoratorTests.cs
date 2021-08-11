using Calculator.Operations;
using Moq;
using NUnit.Framework;

namespace CalculatorTests.OperationTests.DecoratorsTests
{
    class OperationBaseDecoratorTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreation()
        {
            Mock<IOperation<double>> operation = new();
            Assert.IsInstanceOf<IOperation<int>>(operation.Object.AddFormatter((double x) => 0));
        }

    }
}
