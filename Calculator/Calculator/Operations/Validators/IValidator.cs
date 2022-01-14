namespace Calculator.Operations.Validators
{
    /// <summary>
    /// Интерфейс валидатора переменных
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Проверка переменной
        /// </summary>
        /// <param name="value">Переменная над которой будет проводиться валидация</param>
        /// <returns>Результат проверки</returns>
        (bool isCorrect, string errorMessage) Validate(object value);
    }

    /// <summary>
    /// Интерфейс валидатора переменных
    /// </summary>
    /// <typeparam name="T">Тип переменной</typeparam>
    public interface IValidator<T> : IValidator
    {
        /// <summary>
        /// Проверка переменной
        /// </summary>
        /// <param name="value">Переменная над которой будет проводиться валидация</param>
        /// <returns>Результат проверки</returns>
        (bool isCorrect, string errorMessage) Validate(T value);
    }
}
