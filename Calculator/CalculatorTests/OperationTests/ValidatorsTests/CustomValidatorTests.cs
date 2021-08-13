using Calculator.Operations;
using NUnit.Framework;
using Moq;

namespace CalculatorTests.OperationTests.ValidatorsTests
{
    class CustomValidatorTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            Mock<IOperation<double>> operation = new();
            Assert.IsInstanceOf<IOperation<double>>(operation.Object.AddValidator((double x) => (true, string.Empty)));
        }

        [Test]
        public void Constructor_CheckNullParamater_ThrowsArgumentNullException()
        {
            Mock<IOperation<double>> operation = new();
            Func<double, (bool, string)> notInitializedValidator = null;
            Assert.Throws<ArgumentNullException>(() => operation.Object.AddValidator(notInitializedValidator),
                string.Format(_errorMessage, "validator"));
        }

        [Test]
        public void Validate_CorrectedValidation()
        {
            TestingOpForValidation<int> operation = new (0);
            Assert.AreEqual(0, operation.AddValidator(x => (true, string.Empty)).Run());
        }
    }
}
