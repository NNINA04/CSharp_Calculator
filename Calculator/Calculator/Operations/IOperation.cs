﻿using Calculator.Operations.Parameters;

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
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения операции</returns>
        new TOperationResult Run(IOperationParameters operationParameters);

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="handlerParams">Принимаемые параметры операции</param>
        /// <returns>Результат выполнения операции</returns>
        new TOperationResult Run(params object[] handlerParams);

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
        /// Возвращает ли операция значение
        /// </summary>
        public bool IsVoid { get; }

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения операции</returns>
        object Run(IOperationParameters operationParameters);

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="handlerParams">Принимаемые параметры операции</param>
        /// <returns>Результат выполнения операции</returns>
        object Run(params object[] handlerParams);

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <returns>Результат выполнения операции</returns>
        object Run();

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        void RunWithoutReturnValue(IOperationParameters operationParameters);

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        /// <param name="handlerParams">Принимаемые параметры операции</param>
        void RunWithoutReturnValue(params object[] handlerParams);

        /// <summary>
        /// Запускает выполнение операции
        /// </summary>
        void RunWithoutReturnValue();
    }
}
