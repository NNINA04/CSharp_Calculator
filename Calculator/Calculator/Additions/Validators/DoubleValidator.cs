using Calculator.Operations.Validators;

namespace Calculator.Additions.Validators
{
    /// <summary>
    /// Валидатор значений типа <see cref="double"/>> 
    /// </summary>
    public class DoubleValidator : IValidator<double>
    {
        /// <summary>
        /// Проверка переменной
        /// </summary>
        /// <param name="value">Переменная над которым будет проводить валидация</param>
        /// <returns>Результат проверки</returns>
        public (bool isCorrect, string errorMessage) Validate(double value)
        {
            string errorMessage = default;

            if (double.IsInfinity(value))
                errorMessage = "Result is infinity";
            else if (double.IsNaN(value))
                errorMessage = "Result is undefined";

            return (string.IsNullOrEmpty(errorMessage), errorMessage);
        }
        
        /// <summary>
        /// Проверка переменной
        /// </summary>
        /// <param name="value">Переменная над которым будет проводить валидация</param>
        /// <returns>Результат проверки</returns>
        (bool isCorrect, string errorMessage) IValidator.Validate(object value)
        {
            return Validate((double)value);
        }
    }
}
