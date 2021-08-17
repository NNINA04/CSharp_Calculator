using System.Diagnostics.CodeAnalysis;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            new UI().Run();
        }
    }

    /*
     AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property | 
     AttributeTargets.Parameter | AttributeTargets.Constructor | AttributeTargets.Parameter, Inherited = false)
     

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property | 
    AttributeTargets.Parameter | AttributeTargets.Constructor | AttributeTargets.Parameter, Inherited = false)]
    class TestOperation : Attribute
    {
        private readonly Delegate _handler;

        public TestOperation([DisallowNull][NotNull] Delegate handler)
        {
            _handler = handler;
        }

        public static object Test([DisallowNull][NotNull] Delegate handler)
        {
            return null;
        }
    }
    */
}
