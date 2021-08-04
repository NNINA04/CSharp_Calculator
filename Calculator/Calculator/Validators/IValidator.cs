namespace Calculator
{
    /// <summary>
    /// Интерфейс валидатора переменных
    /// </summary>
    /// <typeparam name="T">Тип переменной</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Проверка переменной
        /// </summary>
        /// <param name="value">Переменная над которым будет проводить валидация</param>
        /// <returns>Результат проверки</returns>
        (bool isCorrect, string errorMessage) Validate(T value);
    }
}
