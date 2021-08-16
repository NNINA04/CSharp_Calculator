using Calculator.Operations;
using Calculator.Operations.Decorators;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            var e = new Operation<int>(() => 1).AddFormatter(t => 1).IsVoid;

        }
    }
}
