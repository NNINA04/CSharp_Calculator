using Calculator.Operations.Parameters;
using System;

namespace Calculator.Operations
{
    /// <summary>
    /// Выполняет делегат передав в него параметры
    /// </summary>
    /// <typeparam name="OperationResult">Возвращаемый тип основного хендлера</typeparam>
    public class Operation<OperationResult> : Operation, IOperation<OperationResult>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        public Operation(Delegate handler, IOperationParameters operationParameters) : base(handler, operationParameters)
        {
            CheckTypeCompatibility(handler);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="handlerParams">Параметры основного делегата</param>
        public Operation(Delegate handler, params object[] handlerParams) : base(handler, handlerParams)
        {
            CheckTypeCompatibility(handler);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        public Operation(Delegate handler) : base(handler)
        {
            CheckTypeCompatibility(handler);
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения</returns>
        public new virtual OperationResult Run(IOperationParameters operationParameters)
        {
            return Run(operationParameters?.GetArguments() ?? throw new ArgumentNullException(nameof(operationParameters)));
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <param name="handlerParams">Параметры основного делегата</param>
        /// <returns>Результат выполнения</returns>
        public new virtual OperationResult Run(params object[] handlerParams)
        {
            if (handlerParams == null)
                throw new ArgumentNullException(nameof(handlerParams));

            return (OperationResult)base.Run(handlerParams);
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <returns>Результат выполнения</returns>
        public new virtual OperationResult Run()
        {
            return (OperationResult)base.Run();
        }

        /// <summary>
        /// Проверяет совместимость типов
        /// </summary>
        /// <param name="handler">Основной хендлер</param>
        private static void CheckTypeCompatibility(Delegate handler)
        {
            var handlerReturnType = handler.GetType().GetMethod("Invoke").ReturnType;

            // Является ли возвращаемый тип handler, типом THandlerResult
            if (handlerReturnType != typeof(OperationResult))
                throw new ArgumentException($"Возвращаемый тип {handlerReturnType} делегата {nameof(handler)} " +
                                            $"не соответстует типу {typeof(OperationResult)} " +
                                            $"принимаемого параметра {nameof(OperationResult)} данного метода");
        }
    }
}
