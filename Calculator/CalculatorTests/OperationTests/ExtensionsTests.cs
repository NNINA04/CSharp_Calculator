using NUnit.Framework;
using System;
using Calculator.Operations;
using Moq;
using Calculator.Operations.Validators;
using Calculator.Operations.Formatters;

namespace CalculatorTests
{
    public class ExtensionsTests
    {
        // Типизированный объект Operation
        Mock<IOperation<int>> _typedMockOperation = new();

        // Типизированный объект валидации
        Mock<IValidator<int>> _typedMockValidator = new();

        [Test]
        public void OperationExtensions_AddValidator_Typed()
        {
            // Проверка с передачей объекта
            var typedOpWithValidation = _typedMockOperation.Object.AddValidator(_typedMockValidator.Object);
            Assert.IsTrue(typedOpWithValidation is IOperation<int>);
        }

        [Test]
        public void OperationExtensions_AddValidator_PassingFunctionThatReturnsTuple()
        {
            // Проверка с передачей функции
            var typedOpWithFunc = _typedMockOperation.Object.AddValidator((int x) => (true, default));
            Assert.IsTrue(typedOpWithFunc is IOperation<int>);
        }

        [Test]
        public void OperationExtensions_AddValidator_PassingFunctionThatReturnsBool()
        {
            // Проверка с передачей функции возвращающая только истину
            var typedOpWithModifiedFunc = _typedMockOperation.Object.AddValidator((int x) => true);
            Assert.IsTrue(typedOpWithModifiedFunc is IOperation<int>);
        }

        class Validator : IValidator
        {
            public (bool isCorrect, string errorMessage) Validate(object value)
            {
                throw new NotImplementedException();
            }
        }
        [Test]
        public void OperationExtensions_AddValidator_UsingGenericTypeClass()
        {
            // Не типизированный объект Operation
            Mock<IOperation> mockOperation = new();

            // Проверка с использованием типа Validator
            var opWithValidation = mockOperation.Object.AddValidator<Validator>();
            Assert.IsTrue(opWithValidation is IOperation);
        }

        [Test]
        public void OperationExtensions_Addformatter_PassingTypedFormatter()
        {
            // Типизированный объект форматирования
            var typedMockFormatter = new Mock<IFormatter<int, int>>();

            // Проверка с передачей объекта
            var typedOpWithFormatter = _typedMockOperation.Object.AddFormatter(typedMockFormatter.Object);
            Assert.IsTrue(typedOpWithFormatter is IOperation);
        }

        [Test]
        public void OperationExtensions_Addformatter_PassingFunction()
        {
            // Проверка с передачей функции
            var typedOpWithFunc = _typedMockOperation.Object.AddFormatter((int x) => x);
            Assert.IsTrue(typedOpWithFunc is IOperation);
        }

        class Formatter : IFormatter
        {
            public object Format(object values)
            {
                throw new NotImplementedException();
            }
        }
        [Test]
        public void OperationExtensions_Addformatter_UsingGenericTypeClass()
        {
            // Не типизированный объект Operation
            var mockOperation = new Mock<IOperation>();

            // Проверка с использованием типа Formatter
            var opWithFormatter = mockOperation.Object.AddFormatter<Formatter>();
            Assert.IsTrue(opWithFormatter is IOperation);
        }
    }
}
