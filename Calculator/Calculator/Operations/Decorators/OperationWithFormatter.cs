using Calculator.Operations.Formatters;
using Calculator.Operations.Parameters;

namespace Calculator.Operations.Decorators
{
    /// <summary>
    /// Выполняет форматирование значения
    /// </summary>
    /// <typeparam name="TPrevOperaionResult">Тип возвращаемого значения предыдущей операции</typeparam>
    /// <typeparam name="TCurrentOperationResult">Тип возвращаемого значения данной операции</typeparam>
    public class OperationWithFormatter<TPrevOperaionResult, TCurrentOperationResult> : OperationBaseDecorator<TCurrentOperationResult>
    {
        /// <summary>
        /// Предыдущая операция
        /// </summary>
        private readonly IOperation _prevOperation;

        /// <summary>
        /// Объект хранящий в себе реализацию форматирования
        /// </summary>
        private readonly IFormatter _formatter;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="operation">Операция</param>
        /// <param name="formatter">Форматер</param>
        /// <exception cref="ArgumentNullException">Если operation является null</exception>
        /// <exception cref="ArgumentNullException">Если formatter является null</exception>
        public OperationWithFormatter(IOperation operation, IFormatter formatter) : base()
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
            return (TCurrentOperationResult)_formatter.Format(value);
        }

        /// <summary>
        /// Получает значение из предыдущей операции и форматирует его с помощью форматера
        /// </summary>
        /// <param name="values">Принимаемые значения основного делегата</param>
        /// <returns>Отформатированное значение</returns>
        public override TCurrentOperationResult Run(params object[] values)
        {
            var value = _prevOperation.Run(values);
            return (TCurrentOperationResult)_formatter.Format(value);
        }

        /// <summary>
        /// Получает значение из предыдущей операции и форматирует его с помощью форматера
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Отформатированное значение</returns>
        public override TCurrentOperationResult Run(IOperationParameters operationParameters)
        {
            var value = _prevOperation.Run(operationParameters);
            return (TCurrentOperationResult)_formatter.Format(value);
        }
    }
}



