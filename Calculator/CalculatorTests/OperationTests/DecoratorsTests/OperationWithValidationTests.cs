using Calculator.Operations.Decorators;
using NUnit.Framework;
using Moq;
using Calculator.Operations;
using Calculator.Operations.Validators;

namespace CalculatorTests.OperationTests.DecoratorsTests
{
    class OperationWithValidationTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";
        object[] _transmittedValue = new object[] { 0 };

        [Test]
        public void Constructor_ValidCreationWithoutParameter_ReturnsInstance()
        {
            var operation = new Mock<IOperation>().Object;
            var validator = new Mock<IValidator>().Object;
            Assert.IsAssignableFrom<OperationWithValidation<int>>(new OperationWithValidation<int>(operation, validator));
        }

        [Test]
        public void Constructor_CheckNullExceptionCheckingOperation_ThrowsArgumentNullException()
        {
            var operation = new Mock<IOperation>();
            Assert.Throws<ArgumentNullException>(() => new OperationWithValidation<int>(operation.Object, null), string.Format(_errorMessage, "operation"));
        }

        [Test]
        public void Run_PassingArrObjects_ReturnsOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(_transmittedValue)).Returns(0);

            var validator = new Mock<IValidator>();
            validator.Setup(x => x.Validate(0)).Returns((true, string.Empty));

            Assert.AreEqual(0, new OperationWithValidation<int>(operation.Object, validator.Object).Run(_transmittedValue));
        }

        [Test]
        public void Run_WithoutPassingParameters_ReturnsOperationResult()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(0);

            var validator = new Mock<IValidator>();
            validator.Setup(x => x.Validate(0)).Returns((true, string.Empty));

            Assert.AreEqual(0, new OperationWithValidation<int>(operation.Object, validator.Object).Run());
        }

        [Test]
        public void Run_CheckNullExceptionPassingParameters_ReturnsValidationException()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run(_transmittedValue)).Returns(0);

            var validator = new Mock<IValidator>();
            validator.Setup(x => x.Validate(0)).Returns((false, "Value is undefined"));

            Assert.Throws<Calculator.ValidationException>(() => new OperationWithValidation<int>(operation.Object, validator.Object).Run(_transmittedValue), string.Format(_errorMessage, "errorMessage"));
        }
        
        [Test]
        public void Run_CheckNullExceptionWithoutPassingParameters_ReturnsValidationException()
        {
            var operation = new Mock<IOperation>();
            operation.Setup(x => x.Run()).Returns(double.NaN);

            var validator = new Mock<IValidator>();
            validator.Setup(x => x.Validate(double.NaN)).Returns((false, "Result is Undefined"));

            Assert.Throws<Calculator.ValidationException>(() => new OperationWithValidation<double>(operation.Object, validator.Object).Run(), string.Format(_errorMessage, "errorMessage"));
        }
    }
}
