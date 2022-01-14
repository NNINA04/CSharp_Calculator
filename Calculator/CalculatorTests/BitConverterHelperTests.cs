using Calculator.Additions;
using NUnit.Framework;
using System;
using System.Linq;

namespace CalculatorTests
{
    public class BitConverterHexHelperTest
    {
        [Test]
        public void ConvertArrayToBigEndian_CheckNullException_ThrowsArgumentNullException() 
        {
            Assert.Throws<ArgumentNullException>(() => BitConverterHelper.ConvertArrayToBigEndian(null));
        }

        [Test]
        public void ConvertArrayToBigEndian_CheckConversion_ReturnsBigEndianArray()
        {
            byte[] arr = new byte[4] { 0x0, 0x0, 0x0, 0x1 };

            Assert.AreEqual(arr.Reverse(), BitConverterHelper.ConvertArrayToBigEndian(arr));
        }
    }
}