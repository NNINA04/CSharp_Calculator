using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Calculator.Operations
{
    class OperationDelegate : IOperationParameters
    {
        private Delegate[] _inputHandlers;

        public OperationDelegate(params Delegate[] inputHandlers)
        {
            _inputHandlers = inputHandlers ?? throw new ArgumentNullException(nameof(inputHandlers));
        }
    }
}
