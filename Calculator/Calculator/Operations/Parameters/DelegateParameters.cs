namespace Calculator.Operations.Parameters
{
    /// <summary>
    /// Класс хранящий в себе параметры для Operation. Используя делегаты для получения значения
    /// </summary>
    public class DelegateParameters : IOperationParameters
    {
        /// <summary>
        /// Делегаты для получения аргументов для хендлера Operation
        /// </summary>
        private readonly Delegate[] _inputHandlers;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="inputHandlers">Принимаемые параметры для Operation</param>
        /// <exception cref="ArgumentNullException">Аргумент <paramref name="inputHandlers"/> является null</exception> 
        public DelegateParameters(params Delegate[] inputHandlers)
        {
            _inputHandlers = inputHandlers ?? throw new ArgumentNullException(nameof(inputHandlers));
        }

        /// <summary>
        /// Метод получения принимаемых параметров для Operation
        /// </summary>
        /// <returns>Принимаемые параметры для Operation</returns>
        public object[] GetArguments()
        {
            return _inputHandlers.Select(x => x.DynamicInvoke()).ToArray();
        }

        /// <summary>
        /// Метод получения типов принимаемых параметров для Operation
        /// </summary>
        /// <returns>Типы принимаемых параметров для Operation</returns>
        public Type[] GetArgumentsTypes()
        {
            Type[] types = new Type[_inputHandlers.Length];

            for (int i = 0; i < _inputHandlers.Length; i++)
                types[i] = _inputHandlers[i]?.GetType() ?? null;

            return types;
        }
    }
}
