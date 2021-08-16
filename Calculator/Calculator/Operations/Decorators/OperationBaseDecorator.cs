using Calculator.Operations.Parameters;

namespace Calculator.Operations.Decorators
{
    /// <summary>
    /// Базовый класс декоратора, который выполняет операции
    /// </summary>
    /// <typeparam name="TOperationResult">Тип возвращаемого значения операции</typeparam>
    public abstract class OperationBaseDecorator<TOperationResult> : IOperation<TOperationResult>
    {
        /// <summary>
        /// Возвращает ли операция значение
        /// </summary>
        public bool IsVoid => _operation.IsVoid;

        /// <summary>
        /// Объект для получения результата выполнения метода Run
        /// </summary>
        protected readonly IOperation _operation;

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="operation">Операция</param>
        public OperationBaseDecorator(IOperation operation)
        {
            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
        }

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения операции</returns>
        public virtual TOperationResult Run(IOperationParameters operationParameters)
        {
            return (TOperationResult)_operation.Run(operationParameters);
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры
        /// </summary>
        /// <param name="values">Принимаемые значения основного делегата</param>
        /// <returns>Результат выполнения операции</returns>
        public virtual TOperationResult Run(params object[] values)
        {
            return (TOperationResult)_operation.Run(values);
        }

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        public virtual TOperationResult Run()
        {
            return (TOperationResult)_operation.Run();
        }

        /// <summary>
        /// Выполняет делегат класса передавая в него параметры
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        public void RunWithoutReturnValue(IOperationParameters operationParameters)
        {
            Run(operationParameters);
        }

        /// <summary>
        /// Выполняет делегат класса передавая в него параметры
        /// </summary>
        /// <param name="handlerParams">Параметры основного делегата</param>
        public void RunWithoutReturnValue(params object[] handlerParams)
        {
            Run(handlerParams);
        }

        /// <summary>
        /// Выполняет делегат класса передавая в него параметры
        /// </summary>
        public void RunWithoutReturnValue()
        {
            Run();
        }

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения операции</returns>
        object IOperation.Run(IOperationParameters operationParameters)
        {
            return Run(operationParameters);
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры
        /// </summary>
        /// <param name="values">Принимаемые значения основного делегата</param>
        /// <returns>Результат выполнения операции</returns>
        object IOperation.Run(params object[] values)
        {
            return Run(values);
        }

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        object IOperation.Run()
        {
            return Run();
        }
    }
}
