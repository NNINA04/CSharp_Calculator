using System;

namespace Calculator.Operations
{
    /// <summary>
    /// Выполняет форматирование значения
    /// </summary>
    /// <typeparam name="TPrevOperaionResult">Тип возвращаемого значения предыдущей операции</typeparam>
    /// <typeparam name="TCurrentOperationResult">Тип возвращаемого значения данной операции</typeparam>
    class OperationWithFormatter<TPrevOperaionResult, TCurrentOperationResult> : OperationBaseDecorator<TCurrentOperationResult>
    {
        /// <summary>
        /// Предыдущая операция
        /// </summary>
        private readonly IOperation<TPrevOperaionResult> _prevOperation;

        /// <summary>
        /// Объект хранящий в себе реализацию форматирования
        /// </summary>
        private readonly IFormatter<TPrevOperaionResult, TCurrentOperationResult> _formatter;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="operation">Операция</param>
        /// <param name="formatter">Форматер</param>
        /// <exception cref="ArgumentNullException">Если operation является null</exception>
        /// <exception cref="ArgumentNullException">Если formatter является null</exception>
        public OperationWithFormatter(IOperation<TPrevOperaionResult> operation,
        IFormatter<TPrevOperaionResult, TCurrentOperationResult> formatter) : base()
        {
            _prevOperation = operation ?? throw new ArgumentNullException(nameof(operation));
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        /// <summary>
        /// Получает значение из предыдущей операции и форматирует его с помощью форматера
        /// </summary>
        /// <returns>Отформатированное значение</returns>
        public override TCurrentOperationResult Run()
        {
            var value = _prevOperation.Run();
            return _formatter.Format(value);
        }
    }
}



