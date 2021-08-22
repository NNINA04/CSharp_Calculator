using Calculator.Operations;
using NUnit.Framework;
using Moq;
using Calculator.Operations.Decorators;

namespace CalculatorTests.OperationTests.ValidatorsTests
{
    class CustomValidatorTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            Mock<IOperation<double>> operation = new();
            Assert.IsInstanceOf<OperationWithValidation<double>>(operation.Object.AddValidator((double x) => (true, string.Empty)));
        }

        [Test]
        public void Constructor_CheckArgumentNullException_ThrowsArgumentNullException()
        {
            Mock<IOperation<double>> operation = new();
            Func<double, (bool, string)> notInitializedValidator = null;

            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format(_errorMessage, "validator")),
                () => operation.Object.AddValidator(notInitializedValidator));
        }

        [Test]
        public void Run_CheckMethodValidate_ReturnsValue()
        {
            TestingOpForValidation<int> operation = new (0);
            Assert.AreEqual(0, operation.AddValidator(x => (true, string.Empty)).Run());
        }
    }
}
