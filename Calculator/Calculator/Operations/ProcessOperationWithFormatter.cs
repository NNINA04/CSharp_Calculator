
using System;

// 1: Были проблемы с свойствами (get; set;)
// 2: Оптимизация (возможно)
// 3: ... Точно не помню

namespace Calculator.Operations
{
    // ======================================================================================================================================================================================== //

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="THandlerResult">Возвращаемое значение ProcessOperation</typeparam>
    /// <typeparam name="TResult">Возвращаемое значение данной операции</typeparam>
    class ProcessOperationWithFormatter<THandlerResult, TResult> : IProcessOperation<TResult>
    {
        private readonly IProcessOperation<THandlerResult> _processOperation;
        private readonly IFormatter<THandlerResult, TResult> _formatter;

        public ProcessOperationWithFormatter(IProcessOperation<THandlerResult> processOperation, IFormatter<THandlerResult, TResult> formatter)
        {
            _processOperation = processOperation ?? throw new ArgumentNullException(nameof(processOperation));
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public IProcessOperation<TResult> AddValidator(IValidator<TResult> validator)
        {
            throw new NotImplementedException();
        }
        public IProcessOperation<TResultType> AddFormatter<TResultType>(IFormatter<TResult, TResultType> formatter)
        {
            throw new NotImplementedException();
        }

        public TResult Run(params Delegate[] inputHandlers)
        {
            var value = _processOperation.Run(inputHandlers);
            return _formatter.Format(value);
        }
    }
}



