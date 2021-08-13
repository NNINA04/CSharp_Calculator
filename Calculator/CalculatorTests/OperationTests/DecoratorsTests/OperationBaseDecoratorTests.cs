using Calculator.Operations;
using Calculator.Operations.Decorators;
using Calculator.Operations.Parameters;
using Moq;
using NUnit.Framework;

namespace CalculatorTests.OperationTests.DecoratorsTests
{
    class OperationBaseDecoratorTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_CheckNullException_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TestingOperationBaseDecorator<double>(null), string.Format(_errorMessage, "operation"));
        }

        [Test]
        public void Constructor_ValidCreationWithoutParameter_ReturnsInstance()
        {
            Assert.IsAssignableFrom<TestingOperationBaseDecorator<double>>(new TestingOperationBaseDecorator<double>());
        }

        [Test]
        public void Constructor_ValidCreationWithParameter_ReturnsInstance()
        {
            var operation = new Mock<IOperation<double>>();
            Assert.IsAssignableFrom<TestingOperationBaseDecorator<double>>(new TestingOperationBaseDecorator<double>(operation.Object));
        }
       
        [Test]
        public void Run_PassingOperationParameters_ReturnsOperationResult()
        {
            var operationParameters = new Mock<IOperationParameters>();
            operationParameters.Setup(x => x.GetArguments()).Returns(new object[] { 0 });

            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<IOperationParameters>())).Returns(0);
            Assert.AreEqual(0, new TestingOperationBaseDecorator<int>(operation.Object).Run(operationParameters.Object));
        }

        [Test]
        public void Run_PassingObjects_ReturnsOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<object[]>())).Returns(0);
            Assert.AreEqual(0, new TestingOperationBaseDecorator<int>(operation.Object).Run(new object[] { 0 }));
        }

        [Test]
        public void Run_ReturnsOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);
            Assert.AreEqual(0, new TestingOperationBaseDecorator<int>(operation.Object).Run());
        }

        [Test]
        public void Run_PassingOperationParameters_Run_ReturnsObject()
        {
            var operationParameters = new Mock<IOperationParameters>();
            operationParameters.Setup(x => x.GetArguments()).Returns(new object[] { 0 });

            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<IOperationParameters>())).Returns(0);

            IOperation operationBaseDecorator = new TestingOperationBaseDecorator<int>(operation.Object);
            Assert.AreEqual(0, operationBaseDecorator.Run(operationParameters.Object));
        }

        [Test]
        public void Run_PassingObjects_Run_ReturnsObject()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<object[]>())).Returns(0);

            IOperation operationBaseDecorator = new TestingOperationBaseDecorator<int>(operation.Object);
            Assert.AreEqual(0, operationBaseDecorator.Run(new object[] { 0 }));
        }

        [Test]
        public void Run_ReturnsObject()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);

            IOperation operationBaseDecorator = new TestingOperationBaseDecorator<int>(operation.Object);
            Assert.AreEqual(0, operationBaseDecorator.Run());
        }

        class TestingOperationBaseDecorator<T> : OperationBaseDecorator<T>
        {
            public TestingOperationBaseDecorator(IOperation operation) : base(operation) { }
            public TestingOperationBaseDecorator() : base() { }
        }
    }
}
