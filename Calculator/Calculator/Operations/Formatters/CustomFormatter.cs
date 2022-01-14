using System;

namespace Calculator.Operations.Formatters
{
    /// <summary>
    /// Выполняет форматирование
    /// </summary>
    /// <typeparam name="TInputType">Тип входного значения</typeparam>
    /// <typeparam name="TResultType">Тип результирующего значения</typeparam>
    internal class CustomFormatter<TInputType, TResultType> : IFormatter<TInputType, TResultType>
    {
        private readonly Func<TInputType, TResultType> _formatter;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="formatter">Функция форматирования</param>
        /// <exception cref="ArgumentNullException">Аргумент <paramref name="formatter"/> является null</exception> 
        public CustomFormatter(Func<TInputType, TResultType> formatter)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        /// <summary>
        /// Форматирует входное значение
        /// </summary>
        /// <param name="value">Входное значение</param>
        /// <returns>Отформатированное значение</returns>
        public TResultType Format(TInputType value)
        {
            return _formatter(value);
        }

        /// <summary>
        /// Форматирует входное значение
        /// </summary>
        /// <param name="value">Входное значение</param>
        /// <returns>Отформатированное значение</returns>
        public object Format(object value)
        {
            return Format((TInputType)value);
        }
    }
}
