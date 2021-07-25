using System;
using System.Collections.Generic;
using Calculator.Operations;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {

            IEnumerable<int> obj = new List<int>();
            obj = new int[] { 12, 3, 3 };

            DoubleValidator validator = new();
            FactorialFormatter factorialFormatter = new();


            try
            {

                Func<(int, int)> handler = () => (1, 2);
                var result = new ProcessOperation<(int, int)>(handler).AddFormatter(factorialFormatter).Run();

                //var result = po.Run(new Func<int>(GetInt), new Func<int>(GetInt));
                //var resultOut = new FactorialFormatter().Format(result);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //new UI().Run();
        }
        private static int GetInt() => 2;
        private static string GetString() => "test";
    }
}

