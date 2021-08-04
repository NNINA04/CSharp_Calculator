using NUnit.Framework;
using System;
using Calculator.Operations;
using Calculator;

namespace CalculatorTests
{
    public class OperationTests
    {
        Func<double, double, double> _handlerSum = (x, y) => x + y;

        readonly Func<double, double, bool> isCorrect = (double Expected, double current) => Math.Abs(Expected - current) < 0.0000000001;
        readonly int? obj = null;
        readonly object[] objects = null;

        [Test]
        public void TestOperation()
        {
            var inputValue = new OperationParameters(1, 2);
            Assert.AreEqual(3, new Operation<double>(_handlerSum, inputValue).Run());

            Assert.AreEqual(1, new Operation<int>((int x, int? y) => x + 1, new OperationParameters(0, obj)).Run());
            Assert.AreEqual(1, new Operation<int>((int x, int y) => x + 1, new OperationParameters(0, 1)).Run());

            // Переоприделение параметров и проверка Run
            Assert.AreEqual(4, new Operation<double>(_handlerSum, inputValue).Run(2, 2));

            IOperationParameters operationParameters = new DelegateParameters(() => 1.1, () => 2.2);
            const double Expected = 3.3;

            Assert.AreEqual(true, isCorrect(Expected, new Operation<double>(_handlerSum).Run(operationParameters)));
            Assert.AreEqual(true, isCorrect(Expected, new Operation<double>(_handlerSum, operationParameters).Run()));

            IOperation operation = new Operation<double>(_handlerSum, inputValue);
            Assert.AreEqual(3, operation.Run());
            Assert.AreEqual(3, operation.Run());
        }

        [Test]
        public void NullChecking()
        {
            string errorMessage = "Value cannot be null. (Parameter 'handler')";

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (errorMessage),
                                () => new Operation<double>(null).Run(1, 2));

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (errorMessage),
                                () => new Operation<double>(null, new OperationParameters(1, 2)).Run());

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (errorMessage),
                                () => new Operation<double>(null).Run(new DelegateParameters(() => 1, () => 2)));

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (errorMessage),
                                () => new Operation<double>(null, new DelegateParameters(() => 1, () => 2)).Run());

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                                ("Значение аргумента под индексом 0 не может быть равным null, так как ожидался тип System.Int32"),
                                () => new Operation<int>((int x) => x + 1, new OperationParameters(obj)).Run());

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                                ("Возвращаемый тип System.Nullable`1[System.Int32] делегата handler не соответстует типу System.Int32 принимаемого параметра OperationResult данного метода."),
                                () => new Operation<int>((int? x) => x + 1, new OperationParameters(obj)).Run());

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                ("Value cannot be null. (Parameter 'handlerParams')"),
                                () => new Operation<int>((int x) => x + 1).Run(objects));

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo("Value cannot be null. (Parameter 'validator')"),
                                () => new Operation<double>(() => (double)3 / 0).AddValidator(null).Run());
        }
        
        readonly string _incorrectReturnTypeMessage = $"Возвращаемый тип {typeof(double)} делегата handler не соответстует типу {typeof(int)} принимаемого параметра OperationResult данного метода.";

        readonly string _incorrectCountArgumentsMessage = "Количество введённых параметров не соответствует количесту аргументов вызываемого метода";

        readonly string _incorrectHandlerArgumentTypeMessage = "Параметр типа {0} под индексом 0 не соответствует ожидаемому типу System.Double";

        [Test]
        public void HandlersExceptionUsingDelegates()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (_incorrectReturnTypeMessage),
            () => new Operation<int>(_handlerSum).Run(new DelegateParameters(() => 1, () => 2)));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (_incorrectCountArgumentsMessage),
            () => new Operation<double>(_handlerSum).Run(new DelegateParameters(() => 1)));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (string.Format(_incorrectHandlerArgumentTypeMessage, "System.String")),
            () => new Operation<double>(_handlerSum).Run(new DelegateParameters(() => "1", () => "2")));
        }

        [Test]
        public void HandlersExceptionUsingValues()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                (_incorrectReturnTypeMessage),
                () => new Operation<int>(_handlerSum).Run(1, 2));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                (_incorrectCountArgumentsMessage),
                () => new Operation<double>(_handlerSum).Run(1));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                (string.Format(_incorrectHandlerArgumentTypeMessage, "System.String")),
                () => new Operation<double>(_handlerSum).Run("1", "2"));
        }

        readonly DoubleValidator validator = new();

        readonly FactorialFormatter factorialFormatter = new();

        readonly ICalculatorLogic calc = new Calculator.Calculator();

        [Test]
        public void TestExtensions()
        {
            Assert.AreEqual(0, new Operation<double>(() => (double)0 / 3).AddValidator(validator).Run());
            Assert.AreEqual(0, new Operation<double>((int x) => (double)0 / 3).AddValidator(validator).Run(new OperationParameters(1)));
            Assert.AreEqual(0, new Operation<double>((int x) => (double)0 / 3).AddValidator(validator).Run(new DelegateParameters(() => 1)));
            Assert.AreEqual(0, new Operation<double>((int x) => (double)0 / 3).AddValidator(validator).Run(1));
            Assert.AreEqual(0, new Operation<double>((int? x) => (double)0 / 3).AddValidator(validator).Run(obj));

            IOperation operation = new Operation<double>(() => (double)0 / 3);
            Assert.AreEqual(0, operation.Run());

            operation = new Operation<double>((int x) => (double)0 / 3);
            Assert.AreEqual(0, operation.Run(1));
            Assert.AreEqual(0, operation.Run(new OperationParameters(1)));
            Assert.AreEqual(0, operation.Run(new DelegateParameters(() => 1)));

            FactorialProcessAdapter fact = new(calc);
            Assert.AreEqual("6! = 720", new Operation<(int, int)>(fact.Factorial, 6).AddFormatter(factorialFormatter).Run());
        }
        
        [Test]
        public void TestExtensionsExceptions()
        {
            Assert.Throws(Is.TypeOf<ValidationException>().And.Message.EqualTo("Result is infinity"),
                () => new Operation<double>(() => (double)3 / 0).AddValidator(validator).Run());
        }
    }
}
