using Calculator.Operations.Parameters;
using System;
using System.Reflection;

namespace Calculator.Operations
{
    /// <summary>
    /// Выполняет делегат передав в него параметры
    /// </summary>
    public class Operation : IOperation
    {
        /// <summary>
        /// Основной делегат
        /// </summary>
        private readonly Delegate _handler;

        /// <summary>
        /// Параметры основного делегата
        /// </summary>
        private readonly object[] _handlerParams;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        public Operation(Delegate handler, IOperationParameters operationParameters) : 
            this(handler, operationParameters?.GetArguments() ?? throw new ArgumentNullException(nameof(operationParameters))) { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>ы
        /// <param name="handlerParams">Параметры основного делегата</param>
        public Operation(Delegate handler, params object[] handlerParams) : this(handler)
        {
            _handlerParams = handlerParams ?? throw new ArgumentNullException(nameof(handlerParams));
            CheckValues(_handler, handlerParams);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        public Operation(Delegate handler)
        {
            _handler = handler ?? throw new ArgumentNullException($"{nameof(handler)}");
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения</returns>
        public virtual object Run(IOperationParameters operationParameters)
        {
            if (operationParameters == null)
                throw new ArgumentNullException(nameof(operationParameters));
            return Run(operationParameters.GetArguments());
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <param name="handlerParams">Параметры основного делегата</param>
        /// <returns>Результат выполнения</returns>
        public virtual object Run(params object[] handlerParams)
        {
            if (handlerParams == null)
                throw new ArgumentNullException(nameof(handlerParams));

            CheckValues(_handler, handlerParams);
            return _handler.DynamicInvoke(handlerParams);
        }

        /// <summary>
        /// Выполняет основной делегат класса передавая в него параметры
        /// </summary>
        /// <returns>Результат выполнения</returns>
        public virtual object Run()
        {
            return Run(_handlerParams ?? Array.Empty<object>());
        }

        /// <summary>
        /// Проверяет совместимость основного хендлеа с принимаемыми параметрами
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="handlerParams">Принимаемые параметры основного делегата</param>
        private static void CheckValues(Delegate handler, object[] handlerParams)
        {
            // Проверяет основной делегат на null
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            // Проверяет массив входных параметров для основного делегата на null
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
                // Является ли тип принимаемого параметра handler ссылочным || Является ли тип принимаемого параметра handler Nullable && Является ли объект handlerParams null
                if ((!handlerArguments[index].ParameterType.IsValueType || handlerArguments[index].ParameterType.IsNullable()) && handlerParams[index] == null)
                    continue;
                try
                {
                    // Является ли тип принимаемого параметра handler значимым && Является ли тип принимаемого параметра handler не Nullable && Является ли объект handlerParams null
                    if (handlerArguments[index].ParameterType.IsValueType && !handlerArguments[index].ParameterType.IsNullable() && handlerParams[index] == null)
                        throw new ArgumentException($"Значение аргумента под индексом {index} не может быть равным null, так как ожидался тип {handlerArguments[index].ParameterType}");
                        
                    // Если объект handlerParams не равняется null && Тип объекта handlerParams является типом string
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
