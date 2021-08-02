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
                                () => new Operation<double>(null).Run(new OperationDelegate(() => 1, () => 2)));

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                                (errorMessage),
                                () => new Operation<double>(null, new OperationDelegate(() => 1, () => 2)).Run());

            int? obj = null;
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
                                ("Значение аргумента под индексом 0 не может быть равным null, так как ожидался тип System.Int32"),
                                () => new Operation<double>((int x) => 1.1, obj).Run());
        }

        readonly string _incorrectReturnTypeMessage = $"Возвращаемый тип {typeof(double)} делегата handler не соответстует типу {typeof(int)} принимаемого параметра OperationResult данного метода.";

        readonly string _incorrectCountArgumentsMessage = "Количество введённых параметров не соответствует количесту аргументов вызываемого метода";

        readonly string _incorrectHandlerArgumentTypeMessage = "Параметр типа {0} под индексом 0 не соответствует ожидаемому типу System.Double";

        [Test]
        public void HandlersExceptionUsingDelegates()
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (_incorrectReturnTypeMessage),
            () => new Operation<int>(_handlerSum).Run(new OperationDelegate(() => 1, () => 2)));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (_incorrectCountArgumentsMessage),
            () => new Operation<double>(_handlerSum).Run(new OperationDelegate(() => 1)));

            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo
            (string.Format(_incorrectHandlerArgumentTypeMessage, "Func`1")),
            () => new Operation<double>(_handlerSum).Run(new OperationDelegate(() => "1", () => "2")));
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
                (string.Format(_incorrectHandlerArgumentTypeMessage, "String")),
                () => new Operation<double>(_handlerSum).Run("1", "2"));
        }
    }
}
