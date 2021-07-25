using System;

namespace Calculator.Operations
{
    class ProcessOperationWithValidation<THandlerResult> : IProcessOperation<THandlerResult>
    {
        private readonly IProcessOperation<THandlerResult> _processOperation;
        private readonly IValidator<THandlerResult> _validator;
        public ProcessOperationWithValidation(IProcessOperation<THandlerResult> processOperation, IValidator<THandlerResult> validator)
        {
            _processOperation = processOperation ?? throw new ArgumentNullException(nameof(processOperation));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public IProcessOperation<THandlerResult> AddValidator(IValidator<THandlerResult> validator)
        {
            return _processOperation.AddValidator(validator);
        }
        public IProcessOperation<TResultType> AddFormatter<TResultType>(IFormatter<THandlerResult, TResultType> formatter)
        {
            return _processOperation.AddFormatter(formatter);
        }
        public THandlerResult Run(params Delegate[] inputHandlers)
        {
            var value = _processOperation.Run(inputHandlers);
            var (isCorrect, errorMessage) = _validator.Validate(value);
            return isCorrect ? value : throw new ValidationException(errorMessage);
        }
    }
}



