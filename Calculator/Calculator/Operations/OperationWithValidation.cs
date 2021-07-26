using System;

namespace Calculator.Operations
{
    /// <summary>
    /// Выполняет валидацию значения
    /// </summary>
    /// <typeparam name="TCurrentOperationResult">Тип возвращаемого значения данной операции</typeparam>
    class OperationWithValidation<TCurrentOperationResult> : OperationBaseDecorator<TCurrentOperationResult>
    {
        /// <summary>
        /// Объект хранящий в себе реализацию валидации
        /// </summary>
        private readonly IValidator<TCurrentOperationResult> _validator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Operation">Операция</param>
        /// <param name="validator">Валидатор</param>
        /// <exception cref="ArgumentNullException">Если validator является null</exception>
        public OperationWithValidation(IOperation<TCurrentOperationResult> Operation, IValidator<TCurrentOperationResult> validator) : base(Operation)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        /// <summary>
        /// Получает значение из базового метода Run и проводит валидацию над ним
        /// </summary>
        /// <param name="inputHandlers">Делегаты на ввод данных</param>
        /// <returns>Результат выполнения базового метода Run</returns>
        /// <exception cref="ValidationException">Если isCorrect является false</exception>
        public override TCurrentOperationResult Run(params Delegate[] inputHandlers)
        {
            var value = base.Run(inputHandlers);
            var (isCorrect, errorMessage) = _validator.Validate(value);
            return isCorrect ? value : throw new ValidationException(errorMessage);
        }
    }
}



