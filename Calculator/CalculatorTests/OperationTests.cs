using NUnit.Framework;
using System;
using Calculator.Operations;
namespace CalculatorTests
{
public class OperationTests
{
        Func<double, double, double> _handlerSum = (x, y) => x + y;

        [Test]
        public void TestOperation()
        {
            Assert.AreEqual(3, new Operation<double>(_handlerSum).Run(1, 2));
            Assert.AreEqual(3, new Operation<double>(_handlerSum, 1, 2).Run());
            Assert.AreEqual(3, new Operation<double>(_handlerSum, 2, 2).Run(1, 2));
        }

        [Test]
        public void HandlerNullChecking()
        {
            string errorMessage = "Value cannot be null. (Parameter 'handler')";

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (errorMessage),
                                () => new Operation<double>(null).Run(1, 2));

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (errorMessage),
                                () => new Operation<double>(null, 1, 2).Run());

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (errorMessage),
                                () => new Operation<double>(null).Run(() => 1, () => 2));

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (errorMessage),
                                () => new Operation<double>(null, () => 1, () => 2).Run());
        }

        [Test]
        public void InputHandlersNullChecking()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                   ("Перечисление inputHandlers содержит в себе элемент со значением null"),
                   () => new Operation<double>(_handlerSum).Run(null, null));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                   ("Перечисление inputHandlers содержит в себе элемент со значением null"),
                   () => new Operation<double>(_handlerSum, null, null).Run());
        }

        readonly string _incorrectReturnTypeMessage = $"Возвращаемый тип {typeof(double)} делегата handler не соответстует типу {typeof(int)} принимаемого параметра OperationResult данного метода.";

        static string GetMessageOfincorrectCountArguments(string incorrectType) { return $"Количество элементов {incorrectType} не соответствует количесту аргументов делегата handler"; }

        readonly string _incorrectHandlerArgumentTypeMessage = $"Возвращаемый тип {typeof(string)} делегата в inputHandlers под индексом 0 не соответствует ожидаемому типу {typeof(double)} аргумента делегата handler";

        [Test]
        public void HandlersExceptionUsingDelegates()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (_incorrectReturnTypeMessage),
            () => new Operation<int>(_handlerSum).Run(() => 1, () => 2));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (GetMessageOfincorrectCountArguments("inputHandlers")),
            () => new Operation<double>(_handlerSum).Run(() => 1));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (_incorrectHandlerArgumentTypeMessage),
            () => new Operation<double>(_handlerSum).Run(() => "1", () => "2"));
        }

        [Test]
        public void HandlersExceptionUsingValues()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                (_incorrectReturnTypeMessage),
                () => new Operation<int>(_handlerSum).Run(1, 2));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                (GetMessageOfincorrectCountArguments("inputParams")),
                () => new Operation<double>(_handlerSum).Run(1));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                (_incorrectHandlerArgumentTypeMessage),
                () => new Operation<double>(_handlerSum).Run("1", "2"));
        }
    }
}
