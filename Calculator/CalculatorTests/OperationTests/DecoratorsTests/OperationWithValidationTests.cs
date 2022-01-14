using Calculator.Operations.Decorators;
using NUnit.Framework;
using Moq;
using Calculator.Operations;
using Calculator.Operations.Validators;
using Calculator.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace CalculatorTests.OperationTests.DecoratorsTests
{
    class OperationWithValidationTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";
        object[] _transmittedValue = new object[] { 0 };

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            var operation = new Mock<IOperation>().Object;
            var validator = new Mock<IValidator>().Object;
            Assert.IsAssignableFrom<OperationWithValidation<int>>(new OperationWithValidation<int>(operation, validator));
        }

        [Test]
        public void Constructor_CheckArgumentNullException_ThrowsArgumentNullException()
        {
            var operation = new Mock<IOperation>();
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo(string.Format(_errorMessage, "validator")),
                () => new OperationWithValidation<int>(operation.Object, null));
        }

        [Test]
        public void Run_WithObjectArgument_ReturnsOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(_transmittedValue)).Returns(0);

            var validator = new Mock<IValidator>();
            validator.Setup(x => x.Validate(0)).Returns((true, string.Empty));

            Assert.AreEqual(0, new OperationWithValidation<int>(operation.Object, validator.Object).Run(_transmittedValue));
        }

        [Test]
        public void Run_WithoutArguments_ReturnsOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);

            var validator = new Mock<IValidator>();
            validator.Setup(x => x.Validate(0)).Returns((true, string.Empty));

            Assert.AreEqual(0, new OperationWithValidation<int>(operation.Object, validator.Object).Run());
        }

        [Test]
        public void Run_CheckValidationExceptionWithObjectArgument_ThrowsValidationException()
        {
            const string errorMessage = "Value is undefined";

            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(_transmittedValue)).Returns(0);

            var validator = new Mock<IValidator>();
            validator.Setup(x => x.Validate(0)).Returns((false, errorMessage));

            Assert.Throws(Is.TypeOf<ValidationException>().And.Message.EqualTo(errorMessage),
                () => new OperationWithValidation<int>(operation.Object, validator.Object).Run(_transmittedValue));
        }

        [Test]
        public void Run_CheckValidationExceptionWithoutArguments_ThrowsValidationException()
        {
            const string errorMessage = "Value is undefined";

            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(double.NaN);

            var validator = new Mock<IValidator>();
            validator.Setup(x => x.Validate(double.NaN)).Returns((false, errorMessage));

            Assert.Throws(Is.TypeOf<ValidationException>().And.Message.EqualTo(errorMessage),
                () => new OperationWithValidation<double>(operation.Object, validator.Object).Run());
        }
    }
}
