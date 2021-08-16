namespace Calculator.Operations.Exceptions
{
    internal class OperationVoidReturnException : Exception 
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public OperationVoidReturnException(string message) : base(message) { }
    }
}
