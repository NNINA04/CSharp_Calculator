using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    interface IValidator<T>
    {
        (bool isCorrect, string errorMessage) Validate(T value);
    }
}
