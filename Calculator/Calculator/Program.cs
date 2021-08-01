using Calculator.Operations;
using System;

namespace Calculator
{
    class Program
    {

        static void Main(string[] args)
        {
#if true
            try
            {
                Func<double, double, double> _handlerSum = (x, y) => x + y;
                var result = new Operation<double>(_handlerSum, null).Run();
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
        }
    }
}
