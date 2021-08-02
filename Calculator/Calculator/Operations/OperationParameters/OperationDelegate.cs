using System;

namespace Calculator.Operations
{
    /// <summary>
    /// Хранит в себе массив делегатов
    /// </summary>
    public class OperationDelegate : IOperationParameters
    {
        private Delegate[] _inputHandlers;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="inputHandlers">Принимаемые параметры для Operation</param>
        public OperationDelegate(params Delegate[] inputHandlers)
        {
            _inputHandlers = inputHandlers ?? throw new ArgumentNullException(nameof(inputHandlers));
        }

        /// <summary>
        /// Метод получения принимаемых параметров для Operation
        /// </summary>
        /// <returns>Принимаемые параметры для Operation</returns>
        public object[] GetArguments()
        {
            return _inputHandlers;
        }
    }
}
