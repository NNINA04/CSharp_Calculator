using System;

// 1: Были проблемы с свойствами (get; set;)
// 2: Оптимизация (возможно)
// 3: ... Точно не помню

namespace Calculator.Operations
{
    interface IProcessOperation<THandlerResult>
    {
        IProcessOperation<THandlerResult> AddValidator(IValidator<THandlerResult> validator);
        IProcessOperation<TResultType> AddFormatter<TResultType>(IFormatter<THandlerResult, TResultType> formatter);
        THandlerResult Run(params Delegate[] inputHandlers);
    }
}



