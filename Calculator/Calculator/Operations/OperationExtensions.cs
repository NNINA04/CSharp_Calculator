namespace Calculator.Operations
{
    /// <summary>
    /// Класс расширения для операций
    /// </summary>
    public static class OperationExtensions
    {
        /// <summary>
        /// Добавляет в процесс получения результата стадию валидации
        /// </summary>
        /// <typeparam name="TOperationResult"></typeparam>
        /// <param name="Operation">Операция</param>
        /// <param name="validator">Валидатор</param>
        /// <returns>Декорированный объект</returns>
        static public IOperation<TOperationResult> AddValidator<TOperationResult>
        (this IOperation<TOperationResult> Operation, IValidator<TOperationResult> validator)
        {
            return new OperationWithValidation<TOperationResult>(Operation, validator);
        }

        //static public IOperation<TOperationResult> AddValidator<TOperationResult> // ПОЗЖЕ СДЕЛАТЬ
        //(this IOperation<TOperationResult> Operation, Func<IValidator<TOperationResult>> validator)
        //{
        //    return new OperationWithValidation<TOperationResult>(Operation, );
        //}

        /// <summary>
        /// Добавляет в процесс получения результата стадию форматированния
        /// </summary>
        /// <typeparam name="TOperationResult">Возвращаемый тип операции</typeparam>
        /// <typeparam name="TFormatterResult">Возвращаемый тип форматтера</typeparam>
        /// <param name="operation">Операция</param>
        /// <param name="formatter">Форматер</param>
        /// <returns>Декорированный объект</returns>
        static public IOperation<TFormatterResult> AddFormatter<TOperationResult, TFormatterResult>
        (this IOperation<TOperationResult> operation, IFormatter<TOperationResult, TFormatterResult> formatter)
        {
            return new OperationWithFormatter<TOperationResult, TFormatterResult>(operation, formatter);
        }
    }
}
