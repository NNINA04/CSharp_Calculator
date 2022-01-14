using Calculator.Operations.Formatters;
using Calculator.Operations.Parameters;
using System;

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
        /// Объект хранящий в себе реализацию форматирования
        /// </summary>
        private readonly IFormatter _formatter;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="operation">Операция</param>
        /// <param name="formatter">Форматер</param>
        /// <exception cref="ArgumentNullException">Если formatter является null</exception>
        public OperationWithFormatter(IOperation operation, IFormatter formatter) : base(operation)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        /// <summary>
        /// Получает значение из предыдущей операции и форматирует его с помощью форматера
        /// </summary>
        /// <param name="operationParameters">Объект содержащий принимаемые параметры операции</param>
        /// <returns>Отформатированное значение</returns>
        public override TCurrentOperationResult Run(IOperationParameters operationParameters)
        {
            var value = _operation.Run(operationParameters);
            return (TCurrentOperationResult)_formatter.Format(value);
        }

        /// <summary>
        /// Получает значение из предыдущей операции и форматирует его с помощью форматера
        /// </summary>
        /// <param name="values">Принимаемые значения основного делегата</param>
        /// <returns>Отформатированное значение</returns>
        public override TCurrentOperationResult Run(params object[] values)
        {
            var value = _operation.Run(values);
            return (TCurrentOperationResult)_formatter.Format(value);
        }

        /// <summary>
        /// Получает значение из предыдущей операции и форматирует его с помощью форматера
        /// </summary>
        /// <returns>Отформатированное значение</returns>
        public override TCurrentOperationResult Run()
        {
            var value = _operation.Run();
            return (TCurrentOperationResult)_formatter.Format(value);
        }
    }
}



