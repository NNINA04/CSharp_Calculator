using System;

namespace Calculator.Operations
{
    /// <summary>
    /// Интерфейс для операций
    /// </summary>
    /// <typeparam name="TOperationResult">Тип возвращаемого значения операции</typeparam>
    public interface IOperation<TOperationResult>
    {
        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="inputHandlers">Делегаты для ввода аргументов</param>
        /// <returns>Результат выполнения операции</returns>
        TOperationResult Run(params Delegate[] inputHandlers);
    }
}
