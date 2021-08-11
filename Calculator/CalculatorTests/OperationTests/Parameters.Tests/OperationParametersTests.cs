using Calculator.Operations.Parameters;
using NUnit.Framework;

namespace CalculatorTests.OperationTests.Parameters.Tests
{
    class OperationParametersTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreation()
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
            Assert.AreEqual(new object[] { 0 }, new OperationParameters(0).GetArguments());
        }
    }
}
