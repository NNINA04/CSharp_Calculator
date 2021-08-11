namespace Calculator.Operations.Validators
{
    /// <summary>
    /// Выполняет валидацию
    /// </summary>
    /// <typeparam name="TOperationResult">Тип валидирующего значения</typeparam>
    internal class ModifiedCustomValidator<TOperationResult> : IValidator<TOperationResult>
    {
        private readonly Func<TOperationResult, bool> _validator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="validator">Функция валидации</param>
        public ModifiedCustomValidator(Func<TOperationResult, bool> validator)
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
            string message = default;
            try
            {
                if (!_validator(value))
                    message = "Value is incorrect!";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return (message == default, message);
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
