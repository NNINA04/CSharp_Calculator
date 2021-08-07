using System;

namespace Calculator.Operations.Validators
{
    public class CustomValidatorWithFunc<TOperationResult> : IValidator<TOperationResult>
    {
        private readonly Func<TOperationResult, (bool isCorrect, string errorMessage)> _validator;

        public CustomValidatorWithFunc(Func<TOperationResult, (bool isCorrect, string errorMessage)> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        
        public (bool isCorrect, string errorMessage) Validate(TOperationResult value)
        {
            return _validator(value);
        }
    }
}
