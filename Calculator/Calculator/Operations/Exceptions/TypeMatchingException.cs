namespace Calculator.Operations.Exceptions
{
    /// <summary>
    /// Это исключение вызывется когда происходит не соответствие типов
    /// </summary>
    public class TypeMatchingException : Exception
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public TypeMatchingException(string message) : base(message) { }
    }
}
