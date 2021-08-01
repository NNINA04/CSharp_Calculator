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
                new Operation<double>(_handlerSum).Run("1", "2");

                var result = new Operation<double>(_handlerSum, ()=>1, ()=>1);
                double result1 = result.Run();
                Console.WriteLine(result1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
        }
    }
}
