using Calculator;
using Calculator.Operations;
using Moq;
using NUnit.Framework;

namespace CalculatorTests.OperationTests.ValidatorsTests
{
    class ModifiedCustomValidatorTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            Mock<IOperation<double>> operation = new();
            Assert.IsInstanceOf<IOperation<double>>(operation.Object.AddValidator((double x) => true));
        }

        [Test]
        public void Constructor_CheckNullParamater_ThrowsArgumentNullException()
        {
            Func<double, bool> notInitializedValidator = null;

            Mock<IOperation<double>> operation = new();
            Assert.Throws<ArgumentNullException>(() => operation.Object.AddValidator(notInitializedValidator), string.Format(_errorMessage, "validator"));
        }

        [Test]
        public void Validate_CheckValue_ThrowsValidationException()
        {
            TestingOpForValidation<double> operation = new(double.NaN);
            Assert.Throws<ValidationException>(()=>operation.AddValidator(x => false).Run(), "Value is incorrect!");
        } 

        [Test]
        public void Validate_CheckValue_ThrowsValidationExceptionUsingThrow()
        {
            string errorMessage = "Result is undefined";
            TestingOpForValidation<double> operation = new(double.NaN);
            bool validator(double x) => throw new Exception(errorMessage);
            Assert.Throws<ValidationException>(() => operation.AddValidator(validator).Run(), errorMessage);
        }

        [Test]
        public void Validate_CorrectedValidation()
        {
            TestingOpForValidation<int> operation = new(0);
            Assert.AreEqual(0, operation.AddValidator(x => true).Run());
        }
    }
}
