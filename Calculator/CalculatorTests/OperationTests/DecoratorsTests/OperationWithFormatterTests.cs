using Calculator.Operations;
using Calculator.Operations.Decorators;
using Calculator.Operations.Formatters;
using Calculator.Operations.Parameters;
using Moq;
using NUnit.Framework;

namespace CalculatorTests.OperationTests.DecoratorsTests
{
    class OperationWithFormatterTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_CheckNullExceptionCheckingOperation_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new OperationWithFormatter<int, int>(null, null), string.Format(_errorMessage, "operation"));
        }

        [Test]
        public void Constructor_CheckNullExceptionCheckingFormatter_ThrowsArgumentNullException()
        {
            var operation = new Mock<IOperation>().Object;
            Assert.Throws<ArgumentNullException>(() => new OperationWithFormatter<int, int>(operation, null), string.Format(_errorMessage, "formatter"));
        }

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            var operation = new Mock<IOperation>().Object;
            var formatter = new Mock<IFormatter>().Object;
            Assert.IsAssignableFrom<OperationWithFormatter<int, int>>(new OperationWithFormatter<int, int>(operation, formatter));
        }

        [Test]
        public void Run_PassingOperationParameters_ReturnsZero()
        {
            var operationParameters = new Mock<IOperationParameters>().Object;

            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(operationParameters)).Returns(0);

            var formatter = new Mock<IFormatter>();
            formatter.Setup(x => x.Format(0)).Returns(0);

            Assert.AreEqual(0, new OperationWithFormatter<int, int>(operation.Object, formatter.Object).Run(operationParameters));
        }

        [Test]
        public void Run_PassingObjectArray_ReturnsZero()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(new object[] { 0 })).Returns(0);

            var formatter = new Mock<IFormatter>();
            formatter.Setup(x => x.Format(0)).Returns(0);

            Assert.AreEqual(0, new OperationWithFormatter<int, int>(operation.Object, formatter.Object).Run(new object[] { 0 }));
        }

        [Test]
        public void Run_WithoutPassingParameters_ReturnsZero()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);

            var formatter = new Mock<IFormatter>();
            formatter.Setup(x => x.Format(0)).Returns(0);

            Assert.AreEqual(0, new OperationWithFormatter<int, int>(operation.Object, formatter.Object).Run());
        }
    }
}
