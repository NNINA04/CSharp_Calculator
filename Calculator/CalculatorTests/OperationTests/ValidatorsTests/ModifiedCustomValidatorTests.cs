using Calculator.Exceptions;
using Calculator.Operations;
using Calculator.Operations.Decorators;
using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;

namespace CalculatorTests.OperationTests.ValidatorsTests
{
    class ModifiedCustomValidatorTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            Mock<IOperation<double>> operation = new();
            Assert.IsInstanceOf<OperationWithValidation<double>>(operation.Object.AddValidator((double x) => true));
        }

        [Test]
        public void Constructor_CheckArgumentNullException_ThrowsArgumentNullException()
        {
            Func<double, bool> notInitializedValidator = null;

            Mock<IOperation<double>> operation = new();
            Assert.Throws<ArgumentNullException>(() => operation.Object.AddValidator(notInitializedValidator), string.Format(_errorMessage, "validator"));
        }

        [Test]
        public void Validate_CheckValidationExceptionWithDelegate_ThrowsValidationException()
        {
            TestingOpForValidation<double> operation = new(double.NaN);

            Assert.Throws(Is.TypeOf<ValidationException>().And.Message.EqualTo("Value is incorrect!"),
                () => operation.AddValidator(x => false).Run());
        } 

        [Test]
        public void Validate_CheckValidationExceptionWithDelegateAndException_ThrowsValidationException()
        {
            string errorMessage = "Result is undefined";
            bool validator(double x) => throw new Exception(errorMessage);
            TestingOpForValidation<double> operation = new(double.NaN);

            Assert.Throws(Is.TypeOf<ValidationException>().And.Message.EqualTo(errorMessage),
                () => operation.AddValidator(validator).Run());
        }

        [Test]
        public void Run_CheckWorkingWithoutExceptions_ReturnsValue()
        {
            TestingOpForValidation<int> operation = new(0);
            Assert.AreEqual(0, operation.AddValidator(x => true).Run());
        }
    }
}
