namespace Calculator.Operations.Parameters
{
    /// <summary>
    /// Класс хранящий в себе параметры для Operation. Используя делегаты для получения значения
    /// </summary>
    public class DelegateParameters : IOperationParameters
    {
        private readonly Delegate[] _inputHandlers;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="inputHandlers">Принимаемые параметры для Operation</param>
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
    }
}
