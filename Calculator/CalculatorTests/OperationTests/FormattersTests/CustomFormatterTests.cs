using Calculator.Operations;
using Calculator.Operations.Decorators;
using CalculatorTests.OperationTests.ValidatorsTests;
using Moq;
using NUnit.Framework;
using System;

namespace CalculatorTests.OperationTests.FormattersTests
{
    class CustomFormatterTests
    {
        string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void Constructor_ValidCreation_ReturnsInstance()
        {
            Mock<IOperation<int>> operation = new();
            Assert.IsInstanceOf<OperationWithFormatter<int, int>>(operation.Object.AddFormatter((int x) => 0));
        }

        [Test]
        public void Constructor_CheckArgumentNullException_ThrowsArgumentNullException()
        {
            Mock<IOperation<double>> operation = new();
            Func<double, int> notInitializedFormatter = null;
            Assert.Throws<ArgumentNullException>(() => operation.Object.AddFormatter(notInitializedFormatter),
                string.Format(_errorMessage, "formatter"));
        }

        [Test]
        public void Run_WithoutArguments_ReturnsFormattedValue()
        {
            TestingOpForValidation<int> operation = new(0);
            Assert.AreEqual("Zero", operation.AddFormatter(x => "Zero").Run());
        }
    }
}
