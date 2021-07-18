using NUnit.Framework;

namespace CalculatorTests
{
    public class HexCalculatorTests
    {
        private BitConverterHexCalculator _bitConverterHexCalculator;

        [SetUp]
        public void Setup()
        {
            _bitConverterHexCalculator = new BitConverterHexCalculator();
        }

        [Test]
        public void TestHexConverter()
        {
            Assert.AreEqual("00 00", _bitConverterHexCalculator.ToHex(0x0));

            Assert.AreEqual("FF FF FF FF", _bitConverterHexCalculator.ToHex(-1));

            Assert.AreEqual("00 01", _bitConverterHexCalculator.ToHex(0x00000001));
            Assert.AreEqual("01 01", _bitConverterHexCalculator.ToHex(0x00000101));
            Assert.AreEqual("00 01 01 01", _bitConverterHexCalculator.ToHex(0x00010101));
            Assert.AreEqual("01 01 01 01", _bitConverterHexCalculator.ToHex(0x01010101));

            Assert.AreEqual("10 10 10 10", _bitConverterHexCalculator.ToHex(0x10101010));
            Assert.AreEqual("00 10 10 10", _bitConverterHexCalculator.ToHex(0x00101010));
            Assert.AreEqual("10 10", _bitConverterHexCalculator.ToHex(0x00001010));
            Assert.AreEqual("00 10", _bitConverterHexCalculator.ToHex(0x00000010));
        }
    }
}