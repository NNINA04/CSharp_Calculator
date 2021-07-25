using System;


namespace Calculator
{
    /// <summary>
    /// Это исключение вызывется когда проверенные данные с помощью класса который реализует интерфейса IValidator оказались не валидными
    /// </summary>
    class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
