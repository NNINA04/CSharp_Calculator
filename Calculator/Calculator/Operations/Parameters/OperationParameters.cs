namespace Calculator.Operations.Parameters
{
    /// <summary>
    /// Класс хранящий в себе параметры для Operation
    /// </summary>
    public class OperationParameters : IOperationParameters
    {
        private readonly object[] _values;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="inputValues">Принимаемые параметры для Operation</param>
        public OperationParameters(params object[] inputValues)
        {
            _values = inputValues ?? throw new ArgumentNullException(nameof(inputValues));
        }

        /// <summary>
        /// Метод получения принимаемых параметров для Operation
        /// </summary>
        /// <returns>Принимаемые параметры для Operation</returns>
        public object[] GetArguments()
        {
            return _values;
        }
    }
}
