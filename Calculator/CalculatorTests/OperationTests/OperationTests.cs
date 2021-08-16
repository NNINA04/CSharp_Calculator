using Calculator.Operations;
using Calculator.Operations.Parameters;
using NUnit.Framework;
using Moq;

namespace CalculatorTests.OperationTests
{
    class OperationTests
    {
        Func<int, int> _mainHandler = (int x) => x;
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Operation_Constructor_PassingDelegate()
        {
            Assert.IsAssignableFrom<Operation>(new Operation(_mainHandler));
        }

        [Test]
        public void Operation_Constructor_PassingDelegateAndObject()
        {
            Assert.IsAssignableFrom<Operation>(new Operation(_mainHandler, 0));
        }

        [Test]
        public void Operation_Constructor_PassingDelegateAndOperationParameters()
        {
            var value = new object[] { 1 };
            var operationParameters = new Mock<IOperationParameters>();
            operationParameters.Setup(x => x.GetArguments()).Returns(value);
            operationParameters.Setup(x => x.GetArgumentsTypes()).Returns(new Type[] { value.GetType() });

            Assert.IsAssignableFrom<Operation>(new Operation(_mainHandler, operationParameters.Object));
        }
        
        [Test]
        public void Operation_Constructor_CheckNullDelegate()
        {
            Assert.Throws<ArgumentNullException>(() => new Operation(null), string.Format(_errorMessage, "handler"));
        }

        [Test]
        public void Operation_Constructor_CheckNullParameter()
        {
            object[] handlerParameter = null;
            Assert.Throws<ArgumentNullException>(() => new Operation(_mainHandler, handlerParameter), string.Format(_errorMessage, "inputValues"));
        }

        [Test]
        public void Operation_Constructor_CheckNullOperationParameters()
        {
            IOperationParameters operationParameters = null;
            Assert.Throws<ArgumentNullException>(() => new Operation(_mainHandler, operationParameters), string.Format(_errorMessage, "operationParameters"));
        }

        [Test]
        public void Operation_Run_WithoutAcceptedParameters()
        {
            Assert.AreEqual(0, new Operation(_mainHandler, 0).Run());
        }

        [Test]
        public void Operation_Run_PassingObject()
        {
            Assert.AreEqual(0, new Operation(_mainHandler).Run(0));
        }

        [Test]
        public void Operation_Run_PassingOperationParameters()
        {
            Assert.AreEqual(0, new Operation(_mainHandler).Run(new OperationParameters(0)));
        }

        [Test]
        public void Operation_Run_WithoutAcceptedHandlerParameters()
        {
            Assert.AreEqual(0, new Operation(() => 0).Run());
        }

        [Test]
        public void Operation_Run_CheckNullParameters()
        {
            object[] handlerParameters = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format(_errorMessage, "handlerParams")),
                          () => new Operation(_mainHandler).Run(handlerParameters));
        }

        [Test]
        public void Operation_Run_CheckNullOperationParameters()
        {
            IOperationParameters handlerParameters = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format(_errorMessage, "operationParameters")),
                          () => new Operation(_mainHandler).Run(handlerParameters));
        }

        [Test]
        public void Operation_Run_CheckParametersCount()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().
            And.Message.EqualTo("Количество введённых параметров не соответствует количесту аргументов вызываемого метода"),
                              () => new Operation(_mainHandler).Run());
        }

        [Test]
        public void Operation_Run_CheckIsNullableParameterType()
        {
            Assert.IsAssignableFrom<Operation>(new Operation((int? x) => x, new int?()));
        }

        [Test]
        public void Operation_Run_CheckIsNotNullableParameterType()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().
            And.Message.EqualTo("Значение аргумента под индексом 0 не может быть равным null, так как ожидался тип System.Int32"),
                              () => new Operation(_mainHandler, new object[] { null }));
        }

        [Test]
        public void Operation_Run_CheckTypeMatch()
        {
            //Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo("Параметр типа System.String под индексом 0 не соответствует ожидаемому типу System.Double"),
                //() => new Operation((double x) => x, new object[] { "0" }).Run());
        }
    }
}
