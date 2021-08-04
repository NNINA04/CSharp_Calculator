using System;
using System.Reflection;

namespace Calculator.Operations
{
    /// <summary>
    /// Выполняет делегат передав в него параметры
    /// </summary>
    /// <typeparam name="OperationResult">Возвращаемый тип основного хендлера</typeparam>
    public class Operation<OperationResult> : IOperation<OperationResult>
    {
        /// <summary>
        /// Основной делегат
        /// </summary>
        private Delegate _handler;

        /// <summary>
        /// Параметры основного делегата
        /// </summary>
        private object[] _handlerParams;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        public Operation(Delegate handler)
        {
            _handler = handler ?? throw new ArgumentNullException($"{nameof(handler)}");

            var handlerReturnType = handler.GetType().GetMethod("Invoke").ReturnType;

            // Является ли возвращаемый тип handler, типом THandlerResult
            if (handlerReturnType != typeof(OperationResult))
                throw new ArgumentException($"Возвращаемый тип {handlerReturnType} делегата {nameof(handler)} " +
                                            $"не соответстует типу {typeof(OperationResult)} " +
                                            $"принимаемого параметра {nameof(OperationResult)} данного метода.");
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        public Operation(Delegate handler, IOperationParameters operationParameters) : this(handler)
        {
            var handlerArguments = operationParameters.GetArguments();
            CheckValues(_handler, handlerArguments);
            _handlerParams = handlerArguments;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="handlerParams">Параметры основного делегата</param>
        public Operation(Delegate handler, params object[] handlerParams) : this(handler)
        {
            _handlerParams = handlerParams ?? throw new ArgumentNullException(nameof(handlerParams));
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения</returns>
        public virtual OperationResult Run(IOperationParameters operationParameters)
        {
            return Run(operationParameters.GetArguments());
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <param name="handlerParams">Параметры основного делегата</param>
        /// <returns>Результат выполнения</returns>
        public virtual OperationResult Run(params object[] handlerParams)
        {
            if (handlerParams == null && _handler.GetMethodInfo().GetParameters().Length > 0)
                throw new ArgumentNullException(nameof(handlerParams));

            CheckValues(_handler, handlerParams);
            return (OperationResult)_handler.DynamicInvoke(handlerParams);
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <returns>Результат выполнения</returns>
        public virtual OperationResult Run()
        {
            return Run(_handlerParams);
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения</returns>
        object IOperation.Run(IOperationParameters operationParameters)
        {
            return Run(operationParameters);
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <param name="handlerParams">Принимаемые параметры основного хендлера</param>
        /// <returns>Результат выполнения</returns>
        object IOperation.Run(params object[] handlerParams)
        {
            return Run(handlerParams);
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <returns>Результат выполнения</returns>
        object IOperation.Run()
        {
            return Run();
        }

        /// <summary>
        /// Проверяет совместимость основного хендлеа с принимаемыми параметрами
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="handlerParams">Принимаемые параметры основного делегата</param>
        static void CheckValues(Delegate handler, object[] handlerParams)
        {
            // Если массив входных параметров для основного делегата равняется null, то создаётся пустой массив
            if (handlerParams == null)
                handlerParams = Array.Empty<object>();

            var handlerArguments = handler.GetMethodInfo().GetParameters();
            int handlerArgumentsLength = handlerArguments.Length;
            int handlerParamsLength = handlerParams.Length;

            // Проверяет количество
            if (handlerParamsLength != handlerArgumentsLength)
                throw new ArgumentException($"Количество введённых параметров не соответствует количесту аргументов вызываемого метода");

            // Проверяет совместимости типов
            for (int index = 0; index < handlerParamsLength; index++)
            {
                if ((!handlerArguments[index].ParameterType.IsValueType || handlerArguments[index].ParameterType.IsNullable()) && handlerParams[index] == null)
                    continue;
                try
                {
                    // Является ли тип принимаемого параметра handler значимым || Является ли тип принимаемого параметра handler Nullable || Является ли объект handlerParams null
                    if (handlerArguments[index].ParameterType.IsValueType && !handlerArguments[index].ParameterType.IsNullable() && handlerParams[index] == null)
                        throw new ArgumentException($"Значение аргумента под индексом {index} не может быть равным null, так как ожидался тип {handlerArguments[index].ParameterType}");

                    // Является ли объект handlerParams null || Является ли тип объекта handlerParams типом string
                    if (handlerParams[index] != null && handlerParams[index].GetType() == typeof(string))
                        throw new Exception("Ошибка соответствия типов");

                    Convert.ChangeType(handlerParams[index], handlerArguments[index].ParameterType);
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Параметр типа {handlerParams[index].GetType()} под индексом {index} не соответствует ожидаемому типу {handlerArguments[index].ParameterType}", ex);
                }
            }
        }
    }
}
