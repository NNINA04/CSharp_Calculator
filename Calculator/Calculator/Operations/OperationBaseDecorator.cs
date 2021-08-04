using System;
namespace Calculator.Operations
{
    /// <summary>
    /// Базовый класс декоратора, который выполняет операции
    /// </summary>
    /// <typeparam name="TOperationResult">Тип возвращаемого значения операции</typeparam>
    abstract class OperationBaseDecorator<TOperationResult> : IOperation<TOperationResult>
    {
        /// <summary>
        /// Объект для получения результата выполнения метода Run
        /// </summary>
        protected readonly IOperation<TOperationResult> _operation;
        
        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="operation">Операция</param>
        public OperationBaseDecorator(IOperation<TOperationResult> operation)
        {
            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
        }

        /// <summary>
        /// Конструктор для перехвата управления над _operation
        /// </summary>
        public OperationBaseDecorator() {}
       
        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры
        /// </summary>
        /// <param name="values">Принимаемые значения основного делегата</param>
        /// <returns>Результат выполнения операции</returns>
        public virtual TOperationResult Run(params object[] values)
        {
            return _operation.Run(values);
        }

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        public virtual TOperationResult Run()
        {
            return _operation.Run();
        }

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения операции</returns>
        TOperationResult IOperation<TOperationResult>.Run(IOperationParameters operationParameters)
        {
            return _operation.Run(operationParameters);
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры
        /// </summary>
        /// <param name="values">Принимаемые значения основного делегата</param>
        /// <returns>Результат выполнения операции</returns>
        object IOperation.Run(params object[] values)
        {
            return _operation.Run(values);
        }

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        object IOperation.Run()
        {
            return _operation.Run();
        }

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения операции</returns>
        object IOperation.Run(IOperationParameters operationParameters)
        {
            return _operation.Run(operationParameters);
        }
    }
}
