using NUnit.Framework;
using Calculator.Operations.Parameters;

namespace CalculatorTests.OperationTests.Parameters.Tests
{
    class DelegateParametersTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";
        Delegate _handler = () => 0;

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            Assert.IsAssignableFrom<DelegateParameters>(new DelegateParameters(_handler));
        }

        [Test]
        public void Constructor_CheckArgumentNullException_ThrowsArgumentNullException()
        {
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                (string.Format(_errorMessage, "inputHandlers")),
                    () => new DelegateParameters(null));
        }

        [Test]
        public void GetArguments_ReturnsObjectArray()
        {
            Assert.AreEqual(new object[] { 0 }, new DelegateParameters(_handler).GetArguments());
        }

        [Test]
        public void GetArgumentsTypes_ReturnsArrOfTypes()
        {
            Assert.AreEqual(new Type[] { _handler.GetType() }, new DelegateParameters(_handler).GetArgumentsTypes());
        }

        [Test]
        public void GetArgumentsTypes_ReturnsArrWithNull()
        {
            Assert.AreEqual(new Type[] { null }, new DelegateParameters(new Delegate[]{ null }).GetArgumentsTypes());
        }
    }
}
