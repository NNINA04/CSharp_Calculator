using Calculator.Operations.Parameters;
using Calculator.Operations;
using Moq;
using NUnit.Framework;

namespace CalculatorTests.OperationTests
{
    class TypedOperationTests
    {
        Func<int, int> _mainHandler = (int x) => x;
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreationWithDelegateAndOperationArguments_ReturnsInstance()
        {
            var value = new object[] { 1 };
            var operationParameters = new Mock<IOperationParameters>();

            operationParameters.Setup(x => x.GetArguments()).Returns(value);
            operationParameters.Setup(x => x.GetArgumentsTypes()).Returns(new Type[] { value.GetType() });

            Assert.IsAssignableFrom<Operation<int>>(new Operation<int>(_mainHandler, operationParameters.Object));
        }

        [Test]
        public void Constructor_ValidCreationWithDelegateAndObjectArguments_ReturnsInstance()
        {
            Assert.IsAssignableFrom<Operation<int>>(new Operation<int>(_mainHandler, 0));
        }

        [Test]
        public void Constructor_ArgumentsWithDelegateArgument_ReturnsInstance()
        {
            Assert.IsAssignableFrom<Operation<int>>(new Operation<int>(_mainHandler));
        }
        
        [Test]
        public void Constructor_CheckArgumentException_ThrowsArgumentException()
        {
            string errorMessage = "Возвращаемый тип System.String делегата handler не соответстует типу System.Int32 " +
                "принимаемого параметра TOperationResult данного метода";

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo(errorMessage),
                () => new Operation<int>(() => string.Empty));
        }

        [Test]
        public void Run_WithOperationParametersArgument_ReturnsValue()
        {
            Assert.AreEqual(0, new Operation<int>(_mainHandler).Run(new OperationParameters(0)));
        }

        [Test]
        public void Run_WithObjectArgument_ReturnsValue()
        {
            Assert.AreEqual(0, new Operation<int>(_mainHandler).Run(0));
        }

        [Test]
        public void Run_WithoutArguments_ReturnsValue()
        {
            Assert.AreEqual(0, new Operation<int>(_mainHandler, 0).Run());
        }

        [Test]
        public void Run_CheckArgumentNullExceptionWithOperationParametersArgument_ThrowsArgumentNullException()
        {
            IOperationParameters handlerParameters = null;

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format
                (_errorMessage, "operationParameters")),
                    () => new Operation<int>(_mainHandler).Run(handlerParameters));
        }

        [Test]
        public void Run_CheckArgumentNullExceptionWithObjectArgument_ThrowsArgumentNullException()
        {
            object[] handlerParameters = null;

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format
                (_errorMessage, "inputValues")),
                    () => new Operation<int>(_mainHandler).Run(handlerParameters));
        }
    }
}
