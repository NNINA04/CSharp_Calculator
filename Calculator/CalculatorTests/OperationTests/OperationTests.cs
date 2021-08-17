using Calculator.Operations;
using Calculator.Operations.Parameters;
using NUnit.Framework;
using Calculator.Operations.Exceptions;

namespace CalculatorTests.OperationTests
{
    class OperationTests
    {
        Func<int, int> _mainHandler = (int x) => x;
        Action _voidMainHandler = () => { };
        Delegate _voidMainHandlerWithRequiredArgument = (int i) => { };

        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void RunWithoutReturnValue_PassingOperationParameters()
        {
            new Operation(_voidMainHandlerWithRequiredArgument).RunWithoutReturnValue(new OperationParameters(0));  // <<< СОЗДАТЬ МОК И КАК-то С ПОМОЩЬЮ VERIFY ПРОВЕРИТЬ
        }

        [Test]
        public void RunWithoutReturnValue_PassingObject()
        {
            new Operation(_voidMainHandlerWithRequiredArgument).RunWithoutReturnValue(0); // <<< СОЗДАТЬ МОК И КАК-то С ПОМОЩЬЮ VERIFY ПРОВЕРИТЬ
        }

        [Test]
        public void RunWithoutReturnValue_WithoutAcceptedHandlerParameters()
        {
            new Operation(_voidMainHandler).RunWithoutReturnValue();
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
        public void Run_CheckParametersCount()
        {
            Assert.Throws<ArgumentException>(() => new Operation(_mainHandler).Run(),
            "Количество введённых параметров не соответствует количесту аргументов вызываемого метода");
        }

        [Test]
        public void Constructor_CheckIsNullableParameterType()
        {
            Assert.IsAssignableFrom<Operation>(new Operation((int? x) => x, new int?()));
        }

        [Test]
        public void Run_CheckIsNotNullableParameterType()
        {
            Assert.Throws<ArgumentException>(() => new Operation(_mainHandler, new object[] { null }),
            "Значение аргумента под индексом 0 не может быть равным null, так как ожидался тип System.Int32");
        }

        [Test]
        public void RunWithoutReturnValue_CheckNullOperationParameters()
        {
            OperationParameters operationParameters = null;
            Assert.Throws<ArgumentNullException>(() => new Operation(_voidMainHandlerWithRequiredArgument).
            RunWithoutReturnValue(operationParameters), string.Format(_errorMessage, "operationParameters")); // <<< Нужно как-то заменить на Mock
        }

        [Test]
        public void RunWithoutReturnValue_CheckNullParameters()
        {
            object[] parameters = null;
            Assert.Throws<ArgumentNullException>(() => new Operation(_voidMainHandlerWithRequiredArgument).
            RunWithoutReturnValue(parameters), string.Format(_errorMessage, "handlerParams"));
        }

        [Test]
        public void Run_ExceptionWithoutReturnValue()
        {
            Assert.Throws<OperationVoidReturnException>(() => new Operation(_voidMainHandler).Run(), "Возвращаемое значение хендлера не может быть System.Void");
        }

        [Test]
        public void Run_CheckTypeMatching()
        {
            Assert.Throws<Exception>(() => new Operation((double x) => x, new object[] { "0" }).Run(),
            "Параметр типа System.String под индексом 0 не соответствует ожидаемому типу System.Double");
        }

        [Test]
        public void CheckValues_1()
        {
            Assert.AreEqual(0, new Operation((int? i) => 0).Run(new object[] { null }));
        }

        [Test]
        public void CheckValues_2()
        {
            Assert.AreEqual(0, new Operation((int? i, int j, int? k) => 0).Run(new object[] { null, 1, null }));
        }

        [Test]
        public void ExecuteMainHandler_PassingUnImportantParameter() 
        {
            void Handler(int i = 0) { }
            new Operation(Handler).RunWithoutReturnValue(); // <<< Проверкить как-то на то что метод ничего не возвращает
        }
    }
}
