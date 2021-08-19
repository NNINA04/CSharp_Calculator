using Calculator;
using NUnit.Framework;

namespace CalculatorTests
{
    public class BitConverterHexHelperTest
    {
        [Test]
        public void ConvertArrayToBigEndian_CheckNullException()
        {
            Assert.Throws<ArgumentNullException>(() => BitConverterHelper.ConvertArrayToBigEndian(null));
        }

        [Test]
        public void ConvertArrayToBigEndian_CheckConversion()
        {
            byte[] arr = new byte[4] { 0x0, 0x0, 0x0, 0x1 };

            Assert.AreEqual(arr.Reverse(), BitConverterHelper.ConvertArrayToBigEndian(arr));
        }
    }
}