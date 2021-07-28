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
        /// Выполняет основной делегат класса и передаёт в него параметры из делегатов ввода
        /// </summary>
        /// <param name="inputHandlers">Делегат</param>
        /// <returns>Результат выполнения операции</returns>
        public virtual TOperationResult Run(params Delegate[] inputHandlers)
        {
            return _operation.Run(inputHandlers);
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры
        /// </summary>
        /// <param name="values">Принимаемые значения основного делегата</param>
        /// <returns>Результат выполнения операции</returns>
        public TOperationResult Run(params object[] values)
        {
            return _operation.Run(values);
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
        /// Выполняет основной делегат класса и передаёт в него параметры из делегатов ввода
        /// </summary>
        /// <param name="inputHandlers"></param>
        /// <returns>Результат выполнения операции</returns>
        object IOperation.Run(params Delegate[] inputHandlers)
        {
            return Run(inputHandlers);
        }

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        public TOperationResult Run()
        {
            return _operation.Run();
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
