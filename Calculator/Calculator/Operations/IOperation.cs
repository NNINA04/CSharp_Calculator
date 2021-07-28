using System;

namespace Calculator.Operations
{
    /// <summary>
    /// Интерфейс для операций
    /// </summary>
    /// <typeparam name="TOperationResult">Тип возвращаемого значения операции</typeparam>
    public interface IOperation<TOperationResult> : IOperation
    {
        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="inputHandlers">Делегаты для ввода аргументов</param>
        /// <returns>Результат выполнения операции</returns>
        new TOperationResult Run(params Delegate[] inputHandlers);

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="values">Принимаемые параметры операции</param>
        /// <returns>Результат выполнения операции</returns>
        new TOperationResult Run(params object[] values);

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        new TOperationResult Run();
    }

    /// <summary>
    /// Не типизированный интерфейс для операций
    /// </summary>
    public interface IOperation 
    {
        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="values">Принимаемые параметры операции</param>
        /// <returns>Результат выполнения операции</returns>
        object Run(params object[] values);

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="inputHandlers">Делегаты для ввода аргументов</param>
        /// <returns>Результат выполнения операции</returns>
        object Run(params Delegate[] inputHandlers);

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        object Run();
    }
}
