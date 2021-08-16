using Calculator.Operations.Parameters;

namespace Calculator.Operations
{
    /// <summary>
    /// Выполняет делегат передав в него параметры
    /// </summary>
    /// <typeparam name="TOperationResult">Возвращаемый тип основного хендлера</typeparam>
    public class Operation<TOperationResult> : Operation, IOperation<TOperationResult>
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
        public new virtual TOperationResult Run(IOperationParameters operationParameters)
        {
            if (operationParameters == null)
                throw new ArgumentNullException(nameof(operationParameters));

            return (TOperationResult)base.Run(operationParameters);
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <param name="handlerParams">Параметры основного делегата</param>
        /// <returns>Результат выполнения</returns>
        public new virtual TOperationResult Run(params object[] handlerParams)
        {
            return (TOperationResult)base.Run(new OperationParameters(handlerParams));
        }
        
        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <returns>Результат выполнения</returns>
        public new virtual TOperationResult Run()
        {
            return (TOperationResult)base.Run();
        }
        
        /// <summary>
        /// Проверяет совместимость типов
        /// </summary>
        /// <param name="handler">Основной хендлер</param>
        private static void CheckTypeCompatibility(Delegate handler)
        {
            var handlerReturnType = handler.GetType().GetMethod("Invoke").ReturnType;

            // Является ли возвращаемый тип handler, типом THandlerResult
            if (handlerReturnType != typeof(TOperationResult))
                throw new ArgumentException($"Возвращаемый тип {handlerReturnType} делегата {nameof(handler)} " +
                                            $"не соответстует типу {typeof(TOperationResult)} " +
                                            $"принимаемого параметра {nameof(TOperationResult)} данного метода");
        }
    }
}
