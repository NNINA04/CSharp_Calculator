using System;

namespace Calculator.Operations.Parameters
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

        /// <summary>
        /// Метод получения типов принимаемых параметров для Operation
        /// </summary>
        /// <returns>Типы принимаемых параметров для Operation</returns>
        Type[] GetArgumentsTypes();
    }
}
