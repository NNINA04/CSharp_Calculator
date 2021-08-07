using System;

namespace Calculator.Operations.Formatters
{
    public class CustomFormatter<TInputType, TResultType> : IFormatter<TInputType, TResultType>
    {
        private readonly Func<TInputType, TResultType> _formatter;

        public CustomFormatter(Func<TInputType, TResultType> formatter)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public TResultType Format(TInputType value)
        {
            return _formatter(value);
        }
    }
}
