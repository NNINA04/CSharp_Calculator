namespace Calculator
{
    /// <summary>
    /// Интерфейс форматирования
    /// </summary>
    public interface IFormatter<TInputType, TResultType>
    {
        TResultType Format(TInputType value);
    }
}
