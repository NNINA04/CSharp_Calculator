using Calculator.Operations;
using System;

namespace Calculator
{
    class Program
    {

        static void Main()
        {
            try
            {
                FactorialOperationAdapter fact = new(new Calculator());

                var op = new Operation<(int srcValue, int result)>(fact.Factorial).AddFormatter((x) => $"{x.srcValue}! = {x.result}").Run(6); // РЕШИТЬ!
                Console.WriteLine(op);

                var op2 = new Operation<int>(() => 2).AddValidator(x => x < 1 ? (false, "Error!") : (true, default)).Run();
                Console.WriteLine(op2);

                var op3 = new Operation<int>(() => -1).AddValidator(x => x<0?throw new Exception("TEST"):true).Run();
                Console.WriteLine(op3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
