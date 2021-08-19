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
        public void Constructor_CheckNullParameter_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new OperationParameters(null), string.Format(_errorMessage, "inputValues"));
        }

        [Test]
        public void GetArguments_ReturnsObjectArray()
        {
            Assert.AreEqual(new object[] { _value }, new OperationParameters(_value).GetArguments());
        }

        [Test]
        public void GetArgumentsTypes_ReturnsArrOfTypes()
        {
        Assert.AreEqual(new Type[] { _value.GetType() }, new OperationParameters(_value).GetArgumentsTypes());
        }

        [Test]
        public void GetArgumentsTypes_ReturnsArrWithNull()
        {
            Assert.AreEqual(new Type[] { null }, new OperationParameters(new Delegate[] { null }).GetArgumentsTypes());
        }
    }
}
