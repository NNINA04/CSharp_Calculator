using NUnit.Framework;
using Calculator.Operations;
using Moq;
using Calculator.Operations.Validators;
using Calculator.Operations.Formatters;
using Calculator.Operations.Decorators;
using System;

namespace CalculatorTests
{
    public class ExtensionsTests
    {
        // Типизированный объект Operation
        Mock<IOperation<int>> _typedMockOperation = new();

        // Типизированный объект валидации
        Mock<IValidator<int>> _typedMockValidator = new();

        [Test]
        public void AddValidator_WithGenericTypeClass_ReturnsInstance()
        {
            // Не типизированный объект Operation
            Mock<IOperation> mockOperation = new();

            // Проверка с использованием типа Validator
            Assert.IsInstanceOf<OperationWithValidation<object>>(mockOperation.Object.AddValidator<Validator>());
        }

        [Test]
        public void AddValidator_WithTypedValidator_ReturnsInstance()
        {
            // Проверка с передачей объекта
            Assert.IsInstanceOf<OperationWithValidation<int>>(_typedMockOperation.Object.AddValidator(_typedMockValidator.Object));
        }

        [Test]
        public void AddValidator_WithFuncThatReturnsTuple_ReturnsInstance()
        {
            // Проверка с передачей функции
            Assert.IsInstanceOf<OperationWithValidation<int>>(_typedMockOperation.Object.AddValidator((int x) => (true, default)));
        }

        [Test]
        public void AddValidator_WithDelegateThatReturnsTrue_ReturnsInstance()
        {
            // Проверка с передачей функции возвращающая только истину
            Assert.IsInstanceOf<OperationWithValidation<int>>(_typedMockOperation.Object.AddValidator((int x) => true));
        }

        [Test]
        public void AddFormatter_WithNonGenericTypeClass_ReturnsInstance()
        {
            // Не типизированный объект Operation
            var mockOperation = new Mock<IOperation>();

            // Проверка с использованием типа Formatter
            Assert.IsInstanceOf<OperationWithFormatter<object, object>>(mockOperation.Object.AddFormatter<Formatter>());
        }

        [Test]
        public void AddFormatter_WithTypedFormatter_ReturnsInstance()
        {
            // Типизированный объект форматирования
            var typedMockFormatter = new Mock<IFormatter<int, int>>();

            // Проверка с передачей объекта
            Assert.IsInstanceOf<OperationWithFormatter<int, int>>(_typedMockOperation.Object.AddFormatter(typedMockFormatter.Object));
        }

        [Test]
        public void AddFormatter_WithDelegate_ReturnsInstance()
        {
            // Проверка с передачей функции
            Assert.IsInstanceOf<OperationWithFormatter<int, int>>(_typedMockOperation.Object.AddFormatter((int x) => x));
        }

        class Validator : IValidator
        {
            public (bool isCorrect, string errorMessage) Validate(object value)
            {
                throw new NotImplementedException();
            }
        }
        class Formatter : IFormatter
        {
            public object Format(object values)
            {
                throw new NotImplementedException();
            }
        }
    }
}
