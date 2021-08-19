using Calculator.Operations.Exceptions;
using Calculator.Operations.Parameters;
using System.Reflection;

namespace Calculator.Operations
{
    /// <summary>
    /// Выполняет делегат передав в него параметры
    /// </summary>
    public class Operation : IOperation
    {
        /// <summary>
        /// Возвращает ли операция значение
        /// </summary>
        public bool IsVoid { get => IsReturnTypeVoid(); }

        /// <summary>
        /// Основной делегат
        /// </summary>
        private readonly Delegate _handler;

        /// <summary>
        /// Параметры основного делегата
        /// </summary>
        private readonly IOperationParameters _operationParameters;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>ы
        /// <param name="operationParameters">Параметры основного делегата</param>
        public Operation(Delegate handler, IOperationParameters operationParameters) : this(handler)
        {
            _operationParameters = operationParameters ?? throw new ArgumentNullException(nameof(operationParameters));
            CheckValues(_handler, _operationParameters);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>ы
        /// <param name="handlerParams">Параметры основного делегата</param>
        public Operation(Delegate handler, params object[] handlerParams) : this(handler, new OperationParameters(handlerParams)) { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        public Operation(Delegate handler)
        {
            _handler = handler ?? throw new ArgumentNullException($"{nameof(handler)}");
        }

        /// <summary>
        /// Выполняет делегат класса передавая в него параметры
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Результат выполнения</returns>
        public virtual object Run(IOperationParameters operationParameters)
        {
            if (operationParameters == null)
                throw new ArgumentNullException(nameof(operationParameters));

            if (IsVoid)
                throw new OperationVoidReturnException($"Возвращаемое значение хендлера не может быть {typeof(void)}");

            return ExecuteMainHandler(_handler, operationParameters);
        }

        /// <summary>
        /// Выполняет делегат класса передавая в него параметры
        /// </summary>
        /// <param name="handlerParams">Параметры основного делегата</param>
        /// <returns>Результат выполнения</returns>
        public virtual object Run(params object[] handlerParams)
        {
            if (handlerParams == null)
                throw new ArgumentNullException(nameof(handlerParams));

            return Run(new OperationParameters(handlerParams));
        }

        /// <summary>
        /// Выполняет делегат класса передавая в него параметры
        /// </summary>
        /// <returns>Результат выполнения</returns>
        public virtual object Run()
        {
            return Run(_operationParameters ?? new OperationParameters());
        }

        /// <summary>
        /// Выполняет делегат класса передавая в него параметры
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        public void RunWithoutReturnValue(IOperationParameters operationParameters)
        {
            if (operationParameters == null)
                throw new ArgumentNullException(nameof(operationParameters));

            ExecuteMainHandler(_handler, operationParameters);
        }

        /// <summary>
        /// Выполняет делегат класса передавая в него параметры
        /// </summary>
        /// <param name="handlerParams">Параметры основного делегата</param>
        public void RunWithoutReturnValue(params object[] handlerParams)
        {
            if (handlerParams == null)
                throw new ArgumentNullException(nameof(handlerParams));

            RunWithoutReturnValue(new OperationParameters(handlerParams));
        }

        /// <summary>
        /// Выполняет делегат класса передавая в него параметры
        /// </summary>
        public void RunWithoutReturnValue()
        {
            RunWithoutReturnValue(_operationParameters ?? new OperationParameters());
        }

        /// <summary>
        /// Выполняет делегат класса передавая в него параметры
        /// </summary>
        /// <param name="handler">Основной делегат</param>ы
        /// <param name="operationParameters">Параметры основного делегата</param>
        /// <returns>Результат выполнения</returns>
        private static object ExecuteMainHandler(Delegate handler, IOperationParameters operationParameters)
        {
            CheckValues(handler, operationParameters);

            object[] handlerParams = operationParameters.GetArguments();
            var mainHandlerRequiredParameters = handler.GetMethodInfo().GetParameters();

            object[] newHandlerParams = mainHandlerRequiredParameters.Select((x, i) =>
                        x.IsOptional && i >= handlerParams.Length ? x.DefaultValue : handlerParams[i]).ToArray();

            CheckTypeMatching(handler, newHandlerParams);

            return handler.DynamicInvoke(newHandlerParams);
        }

        /// <summary>
        /// Проверять совместимость принимаемых параметров хендлеров
        /// </summary>
        /// <param name="handler">Основной хендлер</param>
        /// <param name="operationParameters">Объект содержащий параметры для основного хендлера</param>
        private static void CheckValues(Delegate handler, IOperationParameters operationParameters)
        {
            // Проверяет объект содержащий массив входных параметров на null
            if (operationParameters == null)
                throw new ArgumentNullException(nameof(operationParameters));

            Type[] typesOfArguments = operationParameters.GetArgumentsTypes();

            // Проверяет основной делегат на null
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            var mainHandlerRequiredParameters = handler.GetMethodInfo().GetParameters();
            int mainHandlerArgumentsLength = mainHandlerRequiredParameters.Length;

            int ParametersCount = typesOfArguments.Length;

            try
            {
                // Решает проблему с неверным количеством аргументов
                int newLength = mainHandlerRequiredParameters.Select((x, i) => x.IsOptional && i
                >= typesOfArguments.Length ? x.DefaultValue : typesOfArguments[i]).ToArray().Length;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentException($"Количество введённых параметров не соответствует количесту аргументов вызываемого метода");
            }

            // Проверяет совместимости типов
            for (int index = 0; index < ParametersCount; index++)
            {

                // Является ли тип принимаемого параметра handler ссылочным || Является ли тип принимаемого параметра handler Nullable && Является ли объект handlerParams null
                if ((!mainHandlerRequiredParameters[index].ParameterType.IsValueType || mainHandlerRequiredParameters[index].ParameterType.IsNullable()) && typesOfArguments[index] == null)
                    continue;
                try
                {
                    // Является ли тип принимаемого параметра handler значимым && Является ли тип принимаемого параметра handler не Nullable && Является ли объект handlerParams null
                    if (mainHandlerRequiredParameters[index].ParameterType.IsValueType && !mainHandlerRequiredParameters[index].ParameterType.IsNullable() && typesOfArguments[index] == null)
                        throw new ArgumentException($"Значение аргумента под индексом {index} не может быть равным null, так как ожидался тип {mainHandlerRequiredParameters[index].ParameterType}");
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        /// <summary>
        /// Проверять совместимость принимаемых параметров хендлеров
        /// </summary>
        /// <param name="handler">Основной хендлер</param>
        /// <param name="operationParameters">Принимаемые параметры хендлера</param>
        private static void CheckTypeMatching(Delegate handler, object[] operationParameters)
        {
            // Проверяет основной делегат на null
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            // Проверяет объект содержащий массив входных параметров на null
            if (operationParameters == null)
                throw new ArgumentNullException(nameof(operationParameters));

            var mainHandlerRequiredParameters = handler.GetMethodInfo().GetParameters();
            int ParametersCount = operationParameters.Length;

            // Проверяет совместимости типов
            for (int index = 0; index < ParametersCount; index++)
            {
                if ((!mainHandlerRequiredParameters[index].ParameterType.IsValueType || mainHandlerRequiredParameters[index].ParameterType.IsNullable()) && operationParameters[index] == null)
                    continue;
                try
                {
                    // Если объект handlerParams не равняется null && Тип объекта handlerParams является типом string
                    if (operationParameters[index] != null && operationParameters[index].GetType() == typeof(string))
                        throw new TypeMatchingException("Ошибка соответствия типов"); // <<<<<<<
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Проверяет является ли возвращаемый тип хендлена <see cref="void"/>
        /// </summary>
        /// <returns>Результат проверки</returns>
        private bool IsReturnTypeVoid()
        {
            return _handler.GetType().GetMethod("Invoke").ReturnType == typeof(void);
        }
    }
}
