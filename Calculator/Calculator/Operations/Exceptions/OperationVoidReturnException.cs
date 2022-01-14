using System;

namespace Calculator.Operations.Exceptions
{
    /// <summary>
    /// Это исключение вызывется когда handler ничего не возвращает и используется Run
    /// </summary>
    public class OperationVoidReturnException : Exception
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public OperationVoidReturnException(string message) : base(message) { }
    }
}
