namespace Calculator.Operations.Validators
{
    /// <summary>
    /// Выполняет валидацию
    /// </summary>
    /// <typeparam name="TOperationResult">Тип валидирующего значения</typeparam>
    internal class CustomValidatorWithFunc<TOperationResult> : IValidator<TOperationResult>
    {
        private readonly Func<TOperationResult, (bool isCorrect, string errorMessage)> _validator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="validator">Функция валидации</param>
        public CustomValidatorWithFunc(Func<TOperationResult, (bool isCorrect, string errorMessage)> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        /// Проверка переменной
        /// </summary>
        /// <param name="value">Переменная над которой будет проводиться валидация</param>
        /// <returns>Результат проверки</returns>
        public (bool isCorrect, string errorMessage) Validate(TOperationResult value)
        {
            return _validator(value);
        }

        /// <summary>
        /// Проверка переменной
        /// </summary>
        /// <param name="value">Переменная над которой будет проводиться валидация</param>
        /// <returns>Результат проверки</returns>
        (bool isCorrect, string errorMessage) IValidator.Validate(object value)
        {
            return Validate((TOperationResult)value);
        }
    }
}
