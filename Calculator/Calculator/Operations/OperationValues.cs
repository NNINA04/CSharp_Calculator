using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Calculator.Operations
{
    class OperationValues : IOperationParameters
    {
        private object[] _values;

        public OperationValues(params object[] values)
        {
            _values = values?? throw new ArgumentNullException(nameof(values));
        }
    }
}
