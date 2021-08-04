namespace Calculator
{
    /// <summary>
    /// Интерфейс форматирования
    /// </summary>
    /// <typeparam name="TInputType">Тип входного значения</typeparam>
    /// <typeparam name="TResultType">Тип результирующего значения</typeparam>
    public interface IFormatter<TInputType, TResultType>
    {
        /// <summary>
        /// Форматирует входное значение
        /// </summary>
        /// <param name="value">Входное значение</param>
        /// <returns>Отформатированное значение</returns>
        TResultType Format(TInputType value);
    }
}
