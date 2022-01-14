namespace Calculator.Operations.Formatters
{
    /// <summary>
    /// Интерфейс форматирования
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Форматирует входное значение
        /// </summary>
        /// <param name="values">Входное значение</param>
        /// <returns>Отформатированное значение</returns>
        object Format(object values);
    }

    /// <summary>
    /// Интерфейс форматирования
    /// </summary>
    /// <typeparam name="TInputType">Тип входного значения</typeparam>
    /// <typeparam name="TResultType">Тип результирующего значения</typeparam>
    public interface IFormatter<TInputType, TResultType> : IFormatter
    {
        /// <summary>
        /// Форматирует входное значение
        /// </summary>
        /// <param name="values">Входное значение</param>
        /// <returns>Отформатированное значение</returns>
        TResultType Format(TInputType values);
    }
}
