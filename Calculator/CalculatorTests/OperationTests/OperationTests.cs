using Calculator.Operations;
using Calculator.Operations.Parameters;
using NUnit.Framework;
using Calculator.Operations.Exceptions;

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
            Assert.IsAssignableFrom<ArgumentNullException>(new Operation(() => { }));
        }

        [Test]
        public void Constructor_CheckNullHandler()
        {
            Assert.Throws<ArgumentNullException>(() => new Operation(null), string.Format(_errorMessage, "handler"));
        }

        [Test]
        public void Constructor_CheckNullOperationParameters()
        {
            Assert.Throws<ArgumentNullException>(() => new Operation(() => 0, (IOperationParameters)null), string.Format(_errorMessage, "operationParameters"));
        }

        [Test]
        public void RunWithoutReturnValue_PassingOperationParameters()
        {
            Assert.DoesNotThrow(() => new Operation(_voidMainHandlerWithRequiredArgument).RunWithoutReturnValue(0));
        }

        [Test]
        public void RunWithoutReturnValue_PassingObject()
        {
            Assert.DoesNotThrow(() => new Operation(_voidMainHandlerWithRequiredArgument).RunWithoutReturnValue(0));
        }

        [Test]
        public void RunWithoutReturnValue_WithoutAcceptedHandlerParameters()
        {
            Assert.DoesNotThrow(() => new Operation(_voidMainHandler).RunWithoutReturnValue());
        }

        [Test]
        public void Run_CheckNullOperationParameters()
        {
            IOperationParameters handlerParameters = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format
            (_errorMessage, "operationParameters")), () => new Operation(_mainHandler).Run(handlerParameters));
        }

        [Test]
        public void Run_CheckNullParameters()
        {
            object[] handlerParameters = null;
            Assert.Throws<ArgumentNullException>(() => new Operation(_mainHandler).Run(handlerParameters),
            string.Format(_errorMessage, "handlerParams"));
        }

        [Test]
        public void RunWithoutReturnValue_CheckNullOperationParameters()
        {
            OperationParameters operationParameters = null;
            Assert.Throws<ArgumentNullException>(() => new Operation(_voidMainHandlerWithRequiredArgument).
            RunWithoutReturnValue(operationParameters), string.Format(_errorMessage, "operationParameters"));
        }

        [Test]
        public void RunWithoutReturnValue_CheckNullParameters()
        {
            object[] parameters = null;
            Assert.Throws<ArgumentNullException>(() => new Operation(_voidMainHandlerWithRequiredArgument).
            RunWithoutReturnValue(parameters), string.Format(_errorMessage, "handlerParams"));
        }

        [Test]
        public void Run_CheckParametersCount()
        {
            Assert.Throws<ArgumentException>(() => new Operation(_mainHandler).Run(),
            "Количество введённых параметров не соответствует количесту аргументов вызываемого метода");
        }

        [Test]
        public void Run_ExceptionWithoutReturnValue()
        {
            Assert.Throws<OperationVoidReturnException>(() => new Operation(_voidMainHandler).Run(), "Возвращаемое значение хендлера не может быть System.Void");
        }
        
        [Test]
        public void Run_CheckIsNotNullableParameterType()
        {
            Assert.Throws<ArgumentException>(() => new Operation(_mainHandler, new object[] { null }),
            "Значение аргумента под индексом 0 не может быть равным null, так как ожидался тип System.Int32");
        }

        [Test]
        public void Run_CheckValues_UsingNullableParameters()
        {
            Assert.AreEqual(0, new Operation((int? i, int j, int? k) => 0).Run(new object[] { null, 1, null }));
        }

        [Test]
        public void Run_CheckTypeMatching()
        {
            Assert.Throws<Exception>(() => new Operation((double x) => x, new object[] { "0" }).Run(),
            "Параметр типа System.String под индексом 0 не соответствует ожидаемому типу System.Double");
        }

        [Test]
        public void ExecuteMainHandler_PassingUnImportantParameter()
        {
            void Handler(int i = 0) { }
            Assert.DoesNotThrow(() => new Operation(Handler).RunWithoutReturnValue());
        }
    }
}
