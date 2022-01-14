using System;

namespace Calculator.Operations.Parameters
{
    /// <summary>
    /// Класс хранящий в себе параметры для Operation
    /// </summary>
    public class OperationParameters : IOperationParameters
    {
        /// <summary>
        /// Аргументы для хендлера Operation
        /// </summary>
        private readonly object[] _values;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="inputValues">Принимаемые параметры для Operation</param>
        /// <exception cref="ArgumentNullException">Аргумент <paramref name="inputValues"/> является null</exception> 
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

        /// <summary>
        /// Метод получения типов принимаемых параметров для Operation
        /// </summary>
        /// <returns>Типы принимаемых параметров для Operation</returns>
        public Type[] GetArgumentsTypes()
        {
            Type[] types = new Type[_values.Length];

            for (int i = 0; i < _values.Length; i++)
                types[i] = _values[i]?.GetType() ?? null;

            return types;
        }
    }
}
