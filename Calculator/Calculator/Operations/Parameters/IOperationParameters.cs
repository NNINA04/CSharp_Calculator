namespace Calculator.Operations
{
    /// <summary>
    /// Интерфейс для получения принимаемых аргументов для Operation
    /// </summary>
    public interface IOperationParameters
    {
        /// <summary>
        /// Метод получения принимаемых параметров для Operation
        /// </summary>
        /// <returns>Принимаемые параметры для Operation</returns>
        object[] GetArguments(); 
    }
}
