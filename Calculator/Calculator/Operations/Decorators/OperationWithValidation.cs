using Calculator.Operations.Validators;

namespace Calculator.Operations.Decorators
{
    /// <summary>
    /// Выполняет валидацию значения
    /// </summary>
    /// <typeparam name="TCurrentOperationResult">Тип возвращаемого значения данной операции</typeparam>
    public class OperationWithValidation<TCurrentOperationResult> : OperationBaseDecorator<TCurrentOperationResult>
    {
        /// <summary>
        /// Объект хранящий в себе реализацию валидации
        /// </summary>
        private readonly IValidator _validator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="operation">Операция</param>
        /// <param name="validator">Валидатор</param>
        /// exception cref="ArgumentNullException">Аргумент <paramref name="validator"/> является null</exception> 
        public OperationWithValidation(IOperation operation, IValidator validator) : base(operation)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        /// Получает значение из базового метода Run и проводит валидацию над ним
        /// </summary>
        /// <param name="values">Параметры основного делегата</param>
        /// <returns>Результат выполнения базового метода Run</returns>
        /// <exception cref="ValidationException">Если isCorrect является false</exception>
        public override TCurrentOperationResult Run(params object[] values)
        {
            var value = base.Run(values);
            var (isCorrect, errorMessage) = _validator.Validate(value);
            return isCorrect ? value : throw new ValidationException(errorMessage);
        }

        /// <summary>
        /// Проводит валидацию
        /// </summary>
        /// <returns>Результат выполнения базового метода Run</returns>
        /// <exception cref="ValidationException">Если isCorrect является false</exception>
        public override TCurrentOperationResult Run()
        {
            var value = base.Run();
            var (isCorrect, errorMessage) = _validator.Validate(value);
            return isCorrect ? value : throw new ValidationException(errorMessage);
        }
    }
}



