using Calculator.Operations;
using Calculator.Operations.Decorators;
using Calculator.Operations.Parameters;
using Moq;
using NUnit.Framework;
using System;

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
        public void Constructor_CheckArgumentNullException_ThrowsArgumentNullException()
        {

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                (string.Format(_errorMessage, "operation")),
                    () => new TestingOperationBaseDecorator<double>(null));
        }

        [Test]
        public void Run_WithOperationParametersArgument_ReturnsTypedOperationResult()
        {
            var operationParameters = new Mock<IOperationParameters>();
            operationParameters.Setup(x => x.GetArguments()).Returns(new object[] { 0 });

            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<IOperationParameters>())).Returns(0);

            var result = new TestingOperationBaseDecorator<int>(operation.Object).Run(operationParameters.Object);
            Assert.IsInstanceOf<int>(result);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Run_WithObjectArguments_ReturnsTypedOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<object[]>())).Returns(0);

            var result = new TestingOperationBaseDecorator<int>(operation.Object).Run(new object[] { 0 });
            Assert.IsInstanceOf<int>(result);
            Assert.AreEqual(0, result);
        }
       
        [Test]
        public void Run_WithoutArguments_ReturnsTypedOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);

            var result = new TestingOperationBaseDecorator<int>(operation.Object).Run();
            Assert.IsInstanceOf<int>(result);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Run_WithOperationParametersArgument_ReturnsNonTypedOperationResult()
        {
            var operationParameters = new Mock<IOperationParameters>();
            var operation = new Mock<IOperation>();

            operation.Setup(x => x.Run(It.IsAny<IOperationParameters>())).Returns(0);
            IOperation operationBaseDecorator = new TestingOperationBaseDecorator<int>(operation.Object);

            var result = operationBaseDecorator.Run(operationParameters.Object);
            Assert.IsInstanceOf<object>(result);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Run_WithObjectArguments_ReturnsNonTypedOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(It.IsAny<object[]>())).Returns(0);

            IOperation operationBaseDecorator = new TestingOperationBaseDecorator<int>(operation.Object);

            var result = operationBaseDecorator.Run(new object[] { 0 });
            Assert.IsInstanceOf<object>(result);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Run_WithoutArguments_ReturnsNonTypedOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);

            IOperation operationBaseDecorator = new TestingOperationBaseDecorator<int>(operation.Object);

            var result = operationBaseDecorator.Run();
            Assert.IsInstanceOf<object>(result);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void RunWithoutReturnValue_WithOperationParametersArgument_CorrectExecution()
        {
            var operation = new Mock<IOperation>();
            var operationParameters = new Mock<IOperationParameters>();

            operation.Setup(x => x.Run(operationParameters.Object)).Returns(0);
            Assert.DoesNotThrow(() => new TestingOperationBaseDecorator<int>(operation.Object).RunWithoutReturnValue(operationParameters.Object));
        }

        [Test]
        public void RunWithoutReturnValue_WithObjectArguments_CorrectExecution()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(0)).Returns(0);
            Assert.DoesNotThrow(() => new TestingOperationBaseDecorator<int>(operation.Object).RunWithoutReturnValue(0));
        }

        [Test]
        public void RunWithoutReturnValue_WithoutArguments_CorrectExecution()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);
            Assert.DoesNotThrow(() => new TestingOperationBaseDecorator<int>(operation.Object).RunWithoutReturnValue());
        }

        [Test]
        public void IsVoid_ReturnsTrueIfHandlerReturnsVoid()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.IsVoid).Returns(false);
            Assert.AreEqual(false, new TestingOperationBaseDecorator<int>(operation.Object).IsVoid);
        }

        [Test]
        public void IsVoid_ReturnsFalseIfHandlerNotReturnsVoid()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.IsVoid).Returns(true);
            Assert.AreEqual(true, new TestingOperationBaseDecorator<int>(operation.Object).IsVoid);
        }

        class TestingOperationBaseDecorator<T> : OperationBaseDecorator<T>
        {
            public TestingOperationBaseDecorator(IOperation operation) : base(operation) { }
        }
    }
}
