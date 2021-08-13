using NUnit.Framework;
using Calculator.Operations.Parameters;

namespace CalculatorTests.OperationTests.Parameters.Tests
{
    class DelegateParametersTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            Assert.IsAssignableFrom<DelegateParameters>(new DelegateParameters(() => 0));
        }

        [Test]
        public void Constructor_CheckNullParameter_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DelegateParameters(null), string.Format(_errorMessage, "inputHandlers"));
        }

        [Test]
        public void GetArguments_ReturnsObjectArray()
        {
            Assert.AreEqual(new object[] { 0 }, new DelegateParameters(() => 0).GetArguments());
        }
    }
}
