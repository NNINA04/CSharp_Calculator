using System;

namespace Calculator.Operations.Validators
{
    class ModifiedCustomValidator<TOperationResult> : IValidator<TOperationResult>
    {
        private readonly Func<TOperationResult, bool> _validator;

        public ModifiedCustomValidator(Func<TOperationResult, bool> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

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
    }
}
