using Calculator.Extensions;
using NUnit.Framework;
using System;

namespace CalculatorTests
{
    class TypeExtensionTests
    {
        static string _errorMessage = "Value cannot be null. (Parameter '{0}')";

        [Test]
        public void IsNullable_CheckType_ReturnsTrue()
        {
            Assert.AreEqual(true, typeof(bool?).IsNullable());
        }

        [Test]
        public void IsNullable_CheckType_ReturnsFalse()
        {
            Assert.AreEqual(false, typeof(bool).IsNullable());
        }

        [Test]
        public void IsNullable_CheckType_ThrowsArgumentNullException()
        {
            Type type = null;
            Assert.Throws(Is.TypeOf<ArgumentNullException>().And.Message.EqualTo
                (string.Format(_errorMessage, "objectType")), () => type.IsNullable());
        }
    }
}
