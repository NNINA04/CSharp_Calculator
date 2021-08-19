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
        public void Constructor_CheckReturnType()
        {
            Assert.Throws<ArgumentException>(() => new Operation<int>(() => string.Empty), "Возвращаемый тип System.String делегата handler не соответстует типу System.Int32 принимаемого параметра OperationResult данного метода");
        }

        [Test]
        public void Constructor_PassingDelegate()
        {
            Assert.IsAssignableFrom<Operation<int>>(new Operation<int>(_mainHandler));
        }

        [Test]
        public void Constructor_PassingDelegateAndObject()
        {
            Assert.IsAssignableFrom<Operation<int>>(new Operation<int>(_mainHandler, 0));
        }

        [Test]
        public void OperationConstructor_PassingDelegateAndOperationParameters()
        {
            var value = new object[] { 1 };
            var operationParameters = new Mock<IOperationParameters>();

            operationParameters.Setup(x => x.GetArguments()).Returns(value);
            operationParameters.Setup(x => x.GetArgumentsTypes()).Returns(new Type[] { value.GetType() });

            Assert.IsAssignableFrom<Operation<int>>(new Operation<int>(_mainHandler, operationParameters.Object));
        }

        [Test]
        public void Run_WithoutAcceptedParameters()
        {
            Assert.AreEqual(0, new Operation<int>(_mainHandler, 0).Run());
        }

        [Test]
        public void Run_PassingObject()
        {
            Assert.AreEqual(0, new Operation<int>(_mainHandler).Run(0));
        }

        [Test]
        public void Run_PassingOperationParameters()
        {
            Assert.AreEqual(0, new Operation<int>(_mainHandler).Run(new OperationParameters(0)));
        }

        [Test]
        public void Run_CheckNullParameters()
        {
            object[] handlerParameters = null;
            Assert.Throws<ArgumentNullException>(() => new Operation<int>(_mainHandler).Run(handlerParameters), string.Format(_errorMessage, "inputValues"));
        }

        [Test]
        public void Run_CheckNullOperationParameters()
        {
            IOperationParameters handlerParameters = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format(_errorMessage, "operationParameters")),
                          () => new Operation<int>(_mainHandler).Run(handlerParameters));
        }
    }
}
