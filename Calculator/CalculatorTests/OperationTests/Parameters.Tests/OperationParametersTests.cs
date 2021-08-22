using Calculator.Operations.Parameters;
using NUnit.Framework;

namespace CalculatorTests.OperationTests.Parameters.Tests
{
    class OperationParametersTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";
        int _value = 0;

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            Assert.IsAssignableFrom<OperationParameters>(new OperationParameters(0));
        }

        [Test]
        public void Constructor_CheckArgumentNullException_ThrowsArgumentNullException()
        {
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format
                (_errorMessage, "inputValues")), () => new OperationParameters(null));
        }

        [Test]
        public void GetArguments_WithDelegate_ReturnsObjectArray()
        {
            Assert.AreEqual(new object[] { _value }, new OperationParameters(_value).GetArguments());
        }

        [Test]
        public void GetArgumentsTypes_WithDelegate_ReturnsArrOfTypes()
        {
            Assert.AreEqual(new Type[] { _value.GetType() }, new OperationParameters(_value).GetArgumentsTypes());
        }

        [Test]
        public void GetArgumentsTypes_WithDelegateContainingNull_ReturnsArrWithNull()
        {
            Assert.AreEqual(new Type[] { null }, new OperationParameters(new Delegate[] { null }).GetArgumentsTypes());
        }
    }
}
