using Calculator.Operations;
using Calculator.Operations.Parameters;
using NUnit.Framework;
using Calculator.Operations.Exceptions;
using Calculator.Exceptions;
using System;

namespace CalculatorTests.OperationTests
{
    class OperationTests
    {
        static Func<int, int> _mainHandler = (int x) => x;
        static Action _voidMainHandler = () => { };
        static Delegate _voidMainHandlerWithRequiredArgument = (int i) => { };
        static string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            Assert.IsInstanceOf<Operation>(new Operation(() => { }));
        }

        [Test]
        public void Constructor_CheckArgumentNullExceptionWithDelegate_ThrowsArgumentNullException()
        {
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format(_errorMessage, "handler")),
                () => new Operation(null));
        }

        [Test]
        public void Constructor_CheckArgumentNullExceptionWithOperationParametersArgument_ThrowsArgumentNullException()
        {
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format(_errorMessage, "operationParameters")),
                () => new Operation(() => 0, (IOperationParameters)null));
        }

        [Test]
        public void Constructor_CheckArgumentExceptionAndWithObjectArgument_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Operation(_mainHandler, new object[] { null }),
                "Значение аргумента под индексом 0 не может быть равным null, так как ожидался тип System.Int32");
        }

        [Test]
        public void Run_CheckValuesWithNullableParameters_ReturnsValue()
        {
            Assert.AreEqual(0, new Operation((int? i, int j, int? k) => 0).Run(new object[] { null, 1, null }));
        }

        [Test]
        public void Run_CheckArgumentNullExceptionWithOperationParametersArgument_ThrowsArgumentNullException()
        {
            IOperationParameters handlerParameters = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                (string.Format(_errorMessage, "operationParameters")),
                () => new Operation(_mainHandler).Run(handlerParameters));
        }

        [Test]
        public void Run_CheckArgumentNullExceptionWithObjectArgument_ThrowsArgumentNullException()
        {
            object[] handlerParameters = null;
            Assert.Throws<ArgumentNullException>(() => new Operation(_mainHandler).Run(handlerParameters),
                string.Format(_errorMessage, "handlerParams"));
        }

        [Test]
        public void Run_CheckTypeMatchingExceptionWithObjectArgument_ThrowsTypeMatchingException()
        {
            Assert.Throws(Is.TypeOf<TypeMatchingException>().And.Message.EqualTo("Ошибка соответствия типов"),
                () => new Operation((double x) => x, new object[] { "0" }).Run());
        }

        [Test]
        public void Run_CheckArgumentExceptionAndParametersCount_ThrowsArgumentException()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                ("Количество введённых параметров не соответствует количесту аргументов вызываемого метода"),
                    () => new Operation(_mainHandler).Run());
        }

        [Test]
        public void Run_CheckOperationVoidReturnExceptionWithoutArguments_ThrowsOperationVoidReturnException()
        {
            Assert.Throws(Is.TypeOf<OperationVoidReturnException>().And.Message.EqualTo
                ("Возвращаемое значение хендлера не может быть System.Void"),
                    () => new Operation(_voidMainHandler).Run());
        }

        [Test]
        public void RunWithoutReturnValue_WithOperationParametersArgument_CorrectExecution()
        {
            Assert.DoesNotThrow(() => new Operation(_voidMainHandlerWithRequiredArgument).RunWithoutReturnValue(0));
        }

        [Test]
        public void RunWithoutReturnValue_WithObjectArgument_CorrectExecution()
        {
            Assert.DoesNotThrow(() => new Operation(_voidMainHandlerWithRequiredArgument).RunWithoutReturnValue(0));
        }

        [Test]
        public void RunWithoutReturnValue_WithoutArguments_CorrectExecution()
        {
            Assert.DoesNotThrow(() => new Operation(_voidMainHandler).RunWithoutReturnValue());
        }

        [Test]
        public void RunWithoutReturnValue_CheckArgumentNullExceptionWithOperationParametersArgument_ThrowsArgumentNullException()
        {
            IOperationParameters operationParameters = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format
                (_errorMessage, "operationParameters")),
                    () => new Operation(_voidMainHandlerWithRequiredArgument).RunWithoutReturnValue(operationParameters));
        }

        [Test]
        public void RunWithoutReturnValue_CheckArgumentNullExceptionWithObjectArgument_ThrowsArgumentNullException()
        {
            object[] parameters = null;
            Assert.Throws<ArgumentNullException>(() => new Operation(_voidMainHandlerWithRequiredArgument).
            RunWithoutReturnValue(parameters), string.Format(_errorMessage, "handlerParams"));

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                (string.Format(_errorMessage, "handlerParams")),
                    () => new Operation(_voidMainHandlerWithRequiredArgument).RunWithoutReturnValue(parameters));
        }

        [Test]
        public void RunWithoutReturnValue_WithoutArgumentsAndNotRequiredParameter_CorrectExecution()
        {
            void Handler(int i = 0) { }
            Assert.DoesNotThrow(() => new Operation(Handler).RunWithoutReturnValue());
        }
    }
}
