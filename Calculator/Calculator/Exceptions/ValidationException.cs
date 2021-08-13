namespace Calculator
{
    /// <summary>
    /// Это исключение вызывется когда проверенные данные с помощью класса который реализует интерфейса IValidator оказались не валидными
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public ValidationException(string message) : base(message) { }
    }
}
