using Calculator;
using NUnit.Framework;

namespace CalculatorTests
{
    public class DoubleValidatorTests
    {

        private DoubleValidator _doubleValidator;

        [SetUp]
        public void Setup()
        {
            _doubleValidator = new DoubleValidator();
        }

        [Test]
        public void TestDoubleValidator()
        {
            Assert.AreEqual((true, ""), _doubleValidator.Validate(0 / 4));
            Assert.AreEqual((false, "Result is infinity"), _doubleValidator.Validate(double.PositiveInfinity));
            Assert.AreEqual((false, "Result is undefined"), _doubleValidator.Validate(double.NaN));

        }

    }
}