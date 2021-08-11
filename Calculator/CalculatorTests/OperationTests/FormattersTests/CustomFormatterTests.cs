using Calculator.Operations;
using CalculatorTests.OperationTests.ValidatorsTests;
using Moq;
using NUnit.Framework;

namespace CalculatorTests.OperationTests.FormattersTests
{
    class CustomFormatterTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreation()
        {
            Mock<IOperation<double>> operation = new();
            Assert.IsInstanceOf<IOperation<int>>(operation.Object.AddFormatter((double x) => 0));
        }

        [Test]
        public void Constructor_CheckNullParamater_ThrowsArgumentNullException()
        {
            Mock<IOperation<double>> operation = new();
            Func<double, int> notInitializedFormatter = null;
            Assert.Throws<ArgumentNullException>(() => operation.Object.AddFormatter(notInitializedFormatter),
                string.Format(_errorMessage, "formatter"));
        }

        [Test]
        public void Format_ReturnsFormattedValue()
        {
            TestingOpForValidation<int> operation = new(0);
            Assert.AreEqual("Zero", operation.AddFormatter(x => "Zero").Run());
        }
    }
}
