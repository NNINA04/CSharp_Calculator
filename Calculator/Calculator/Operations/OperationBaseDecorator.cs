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
        /// Вызов метода Run переменной _operation
        /// </summary>
        /// <param name="inputHandlers">Делегат</param>
        /// <returns>Результат выполнения операции</returns>
        public virtual TOperationResult Run(params Delegate[] inputHandlers)
        {
            return _operation.Run(inputHandlers);
        }
    }
}
