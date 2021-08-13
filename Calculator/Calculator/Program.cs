using Calculator.Operations;
using Calculator.Operations.Decorators;
using Calculator.Operations.Formatters;
using Calculator.Operations.Validators;
using System;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            var sdfgsfdg = new Calculator();
            try
            {
                // Assert.IsAssignableFrom<IValidator<double>>(new Operation<double>(() => 0d).AddValidator((double x) => (true, string.Empty)));
                var t = new Operation<double>(() => 0d).AddValidator((double x) => (true, string.Empty));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
