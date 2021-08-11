using Calculator.Operations.Parameters;
using Calculator.Operations;

namespace CalculatorTests.OperationTests.ValidatorsTests
{
    internal class TestingOpForValidation<T> : IOperation<T>
    {
        private T _value;

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
