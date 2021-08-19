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
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            var operation = new Mock<IOperation<double>>();
            Assert.IsAssignableFrom<TestingOperationBaseDecorator<double>>(new TestingOperationBaseDecorator<double>(operation.Object));
        }

        [Test]
        public void Constructor_CheckNullException_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TestingOperationBaseDecorator<double>(null), string.Format(_errorMessage, "operation"));
        }
       
        [Test]
        public void Run_PassingOperationParameters_ReturnsTypedOperationResult()
        {
            var operationParameters = new Mock<IOperationParameters>();
            operationParameters.Setup(x => x.GetArguments()).Returns(new object[] { 0 });

            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<IOperationParameters>())).Returns(0);
            Assert.AreEqual(0, new TestingOperationBaseDecorator<int>(operation.Object).Run(operationParameters.Object));
        }

        [Test]
        public void Run_PassingObjects_ReturnsTypedOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<object[]>())).Returns(0);
            Assert.AreEqual(0, new TestingOperationBaseDecorator<int>(operation.Object).Run(new object[] { 0 }));
        }

        [Test]
        public void Run_WithoutPassingParameters_ReturnsTypedOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);
            Assert.AreEqual(0, new TestingOperationBaseDecorator<int>(operation.Object).Run());
        }
        
        [Test]
        public void Run_PassingOperationParameters_ReturnsNonTypedOperationResult()
        {
            var operationParameters = new Mock<IOperationParameters>();
            operationParameters.Setup(x => x.GetArguments()).Returns(new object[] { 0 });

            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<IOperationParameters>())).Returns(0);

            IOperation operationBaseDecorator = new TestingOperationBaseDecorator<int>(operation.Object);
            Assert.AreEqual(0, operationBaseDecorator.Run(operationParameters.Object));
        }
        
        [Test]
        public void Run_PassingObjects_ReturnsNonTypedOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<object[]>())).Returns(0);

            IOperation operationBaseDecorator = new TestingOperationBaseDecorator<int>(operation.Object);
            Assert.AreEqual(0, operationBaseDecorator.Run(new object[] { 0 }));
        }

        [Test]
        public void Run_WithoutPassingParameters_ReturnsNonTypedOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);

            IOperation operationBaseDecorator = new TestingOperationBaseDecorator<int>(operation.Object);
            Assert.AreEqual(0, operationBaseDecorator.Run());
        }
        
        [Test]
        public void RunWithoutReturnValue_PassingOperationParameters()
        {
            var operationParameters = new Mock<IOperationParameters>();
            var operation = new Mock<IOperation>();

            operation.Setup(x => x.Run(operationParameters.Object)).Returns(0);
            new TestingOperationBaseDecorator<int>(operation.Object).RunWithoutReturnValue(operationParameters.Object);
            operation.Verify(x => x.Run(operationParameters.Object), Times.Once);
        }

        [Test]
        public void RunWithoutReturnValue_PassingObject()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(0)).Returns(0);
            new TestingOperationBaseDecorator<int>(operation.Object).RunWithoutReturnValue(0);
            operation.Verify(x => x.Run(0), Times.Once);
        }

        [Test]
        public void RunWithoutReturnValue_WithoutPassingParameters()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);
            new TestingOperationBaseDecorator<int>(operation.Object).RunWithoutReturnValue();
            operation.Verify(x => x.Run(), Times.Once);
        }

        [Test]
        public void IsVoid_ReturnsTrueIfHandlerReturnsVoid()
        {
            var operation = new Mock<IOperation>();
            Assert.AreEqual(false, new TestingOperationBaseDecorator<int>(operation.Object).IsVoid);
        }

        class TestingOperationBaseDecorator<T> : OperationBaseDecorator<T>
        {
            public TestingOperationBaseDecorator(IOperation operation) : base(operation) { }
        }
    }
}
