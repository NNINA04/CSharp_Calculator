using Calculator.Operations.Validators;
using NUnit.Framework;

namespace CalculatorTests
{
    public class DoubleValidatorTests
    {
        IValidator _doubleValidator = new DoubleValidator();

        [Test]
        public void Validate_CheckInfinityValue_ReturnsTuple()
        {
            Assert.AreEqual((false, "Result is infinity"), _doubleValidator.Validate(double.PositiveInfinity));
        }

        [Test]
        public void Validate_CheckUndeninedValue_ReturnsTuple()
        {
            Assert.AreEqual((false, "Result is undefined"), _doubleValidator.Validate(double.NaN));
        }
    }
}