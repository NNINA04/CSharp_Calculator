using Calculator.Operations.Parameters;
using Calculator.Operations;

namespace CalculatorTests.OperationTests.ValidatorsTests
{
    /// <summary>
    /// Класс для юнит тестов
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого значения операции</typeparam>
    internal class TestingOpForValidation<T> : IOperation<T>
    {
        private T _value;

        public bool IsVoid => throw new NotImplementedException();

        public TestingOpForValidation(T value)
        {
            _value = value;
        }

        public virtual T Run(IOperationParameters operationParameters)
        {
            return _value;
        }

        public virtual T Run(params object[] handlerParams)
        {
            return _value;
        }

        public virtual T Run()
        {
            return _value;
        }

        public void RunWithoutReturnValue(IOperationParameters operationParameters)
        {
            throw new NotImplementedException();
        }

        public void RunWithoutReturnValue(params object[] handlerParams)
        {
            throw new NotImplementedException();
        }

        public void RunWithoutReturnValue()
        {
            throw new NotImplementedException();
        }

        object IOperation.Run(IOperationParameters operationParameters)
        {
            return Run(operationParameters);
        }

        object IOperation.Run(params object[] handlerParams)
        {
            return Run(handlerParams);
        }

        object IOperation.Run()
        {
            return Run();
        }
    }
}
