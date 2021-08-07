using NUnit.Framework;
using System;
using Calculator.Operations;
using Calculator;
using Moq;

namespace CalculatorTests
{
    public class OperationTests
    {
        IMock<IOperation<int>> mockOperation = new Mock<IOperation<int>>();
        Mock<IValidator<int>> mockValidator = new Mock<IValidator<int>>();
        Mock<IFormatter<int, string>> mockFormatter = new Mock<IFormatter<int, string>>();
        Operation<int> emptyOperation = null;

        string incorrectReturnTypeMessage = $"Возвращаемый тип {typeof(double)} делегата handler не соответстует типу {typeof(int)} принимаемого параметра OperationResult данного метода.";
        string incorrectCountArgumentsMessage = "Количество введённых параметров не соответствует количесту аргументов вызываемого метода";
        string incorrectHandlerArgumentTypeMessage = "Параметр типа {0} под индексом 0 не соответствует ожидаемому типу System.Double";
        string errorMessage = "Value cannot be null. (Parameter '{0}')";

        static Func<double, double, double> handlerSum = (x, y) => x + y;
        static Func<double, double, bool> isCorrect = (double Expected, double current) => Math.Abs(Expected - current) < 0.0000000001;

        int? obj = null;
        object[] objects = null;

        DoubleValidator validator = new();
        FactorialFormatter factorialFormatter = new();
        ICalculatorLogic calc = new Calculator.Calculator();
        IOperation operationWithReturnTypeDouble = new Operation<double>(handlerSum);
        IOperationParameters operationParameters = null;

        [Test]
        public void TestOperation()
        {
            var inputValue = new OperationParameters(1, 2);
            Assert.AreEqual(3, new Operation<double>(handlerSum, inputValue).Run());

            Assert.AreEqual(1, new Operation<int>((int x, int? y) => x + 1, new OperationParameters(0, obj)).Run());
            Assert.AreEqual(1, new Operation<int>((int x, int y) => x + 1, new OperationParameters(0, 1)).Run());

            // Переоприделение параметров и проверка Run
            Assert.AreEqual(4, new Operation<double>(handlerSum, inputValue).Run(2, 2));

            IOperationParameters operationParameters = new DelegateParameters(() => 1.1, () => 2.2);
            const double Expected = 3.3;

            Assert.AreEqual(true, isCorrect(Expected, new Operation<double>(handlerSum).Run(operationParameters)));
            Assert.AreEqual(true, isCorrect(Expected, new Operation<double>(handlerSum, operationParameters).Run()));

            IOperation operation = new Operation<double>(handlerSum, inputValue);
            Assert.AreEqual(3, operation.Run());

            Assert.AreEqual(3, operationWithReturnTypeDouble.Run(new OperationParameters(1, 2))); //
            Assert.AreEqual(3, operationWithReturnTypeDouble.Run(1, 2));

        }

        [Test]
        public void NullChecking()
        {
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (string.Format(errorMessage, "handler")),
                                () => new Operation<double>(null));

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (string.Format(errorMessage, "inputHandlers")),
                                () => new DelegateParameters(null));

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (string.Format(errorMessage, "inputValues")),
                                () => new OperationParameters(null));


            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (string.Format(errorMessage, "handlerParams")),
                                () => new Operation<double>(handlerSum, objects));

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                            (string.Format(errorMessage, "operationParameters")),
                                            () => new Operation<double>(handlerSum).Run(operationParameters));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                                ("Значение аргумента под индексом 0 не может быть равным null, так как ожидался тип System.Int32"),
                                () => new Operation<int>((int x) => x + 1, new OperationParameters(obj)).Run());

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                                ("Возвращаемый тип System.Nullable`1[System.Int32] делегата handler не соответстует типу System.Int32 принимаемого параметра OperationResult данного метода."),
                                () => new Operation<int>((int? x) => x + 1, new OperationParameters(obj)).Run());

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                ("Value cannot be null. (Parameter 'handlerParams')"),
                                () => new Operation<int>((int x) => x + 1).Run(objects));

            IFormatter<int, string> test = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (string.Format(errorMessage, "formatter")), () => mockOperation.Object.AddFormatter(test).Run()); //

            Operation<int> t = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (string.Format(errorMessage, "operation")), () => t.AddFormatter(test).Run()); //
        }

        [Test]
        public void NullCheckingUsingMoq()
        {
            IFormatter<int, string> emptyFormatter = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (string.Format(errorMessage, "formatter")), () => mockOperation.Object.AddFormatter(emptyFormatter).Run()); //

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (string.Format(errorMessage, "operation")), () => emptyOperation.AddFormatter(emptyFormatter).Run()); //

            IValidator<int> emptyValidator = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (string.Format(errorMessage, "validator")), () => mockOperation.Object.AddValidator(emptyValidator).Run()); //

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (string.Format(errorMessage, "operation")), () => emptyOperation.AddValidator(emptyValidator).Run()); //
        }

        [Test]
        public void HandlersExceptionUsingDelegates()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (incorrectReturnTypeMessage),
            () => new Operation<int>(handlerSum).Run(new DelegateParameters(() => 1, () => 2)));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (incorrectCountArgumentsMessage),
            () => operationWithReturnTypeDouble.Run(new DelegateParameters(() => 1)));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (string.Format(incorrectHandlerArgumentTypeMessage, "System.String")),
            () => operationWithReturnTypeDouble.Run(new DelegateParameters(() => "1", () => "2")));
        }

        [Test]
        public void HandlersExceptionUsingValues()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                (incorrectReturnTypeMessage),
                () => new Operation<int>(handlerSum).Run(1, 2));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                (incorrectCountArgumentsMessage),
                () => operationWithReturnTypeDouble.Run(1));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                (string.Format(incorrectHandlerArgumentTypeMessage, "System.String")),
                () => operationWithReturnTypeDouble.Run("1", "2"));
        }

        [Test]
        public void TestExtensions()
        {
            Func<int, double> handlerWithParams = (int x) => (double)0 / 3;
            Func<double> handlerWithoutParams = () => (double)0 / 3;

            var operationWithValidator = new Operation<double>(handlerWithParams).AddValidator(validator);
            Assert.AreEqual(0, new Operation<double>(handlerWithoutParams).AddValidator(validator).Run());
            Assert.AreEqual(0, new Operation<double>((int? x) => (double)0 / 3).AddValidator(validator).Run(obj));

            FactorialOperationAdapter fact = new(calc);
            Assert.AreEqual("6! = 720", new Operation<(int, int)>(fact.Factorial, 6).AddFormatter(factorialFormatter).Run());

            mockValidator.Setup(mock => mock.Validate(It.IsAny<int>())).Returns((true, string.Empty)); // Можно указать что угодно, лишь бы тип правильный был
            mockOperation.Object.AddValidator(mockValidator.Object).Run();
            mockValidator.Verify(mock => mock.Validate(It.IsAny<int>()), Times.Once());

            mockFormatter.Setup(mock => mock.Format(It.IsAny<int>())).Returns("1"); // Можно указать что угодно, лишь бы тип правильный был
            mockOperation.Object.AddFormatter(mockFormatter.Object).Run();
            mockFormatter.Verify(mock => mock.Format(It.IsAny<int>()), Times.Once());
        }
    }
}
