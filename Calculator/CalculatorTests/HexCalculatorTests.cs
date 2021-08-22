using Calculator;
using NUnit.Framework;

namespace CalculatorTests
{
    public class HexCalculatorTests
    {
        [Test]
        public void ToHex_UsingBitConverterHexCalculator()
        {
            CheckingCorrectWorkingToHex(new BitConverterHexCalculator());
        }

        [Test]
        public void ToHex_UsingMatchingTypeToHex()
        {
            CheckingCorrectWorkingToHex(new MatchingTypeToHex());
        }

        private static void CheckingCorrectWorkingToHex(IHexCalculator hexCalculator)
        {
            Assert.AreEqual("00 00", hexCalculator.ToHex(0x0));

            Assert.AreEqual("FF FF FF FF", hexCalculator.ToHex(-1));

            Assert.AreEqual("00 01", hexCalculator.ToHex(0x00000001));
            Assert.AreEqual("01 01", hexCalculator.ToHex(0x00000101));
            Assert.AreEqual("00 01 01 01", hexCalculator.ToHex(0x00010101));
            Assert.AreEqual("01 01 01 01", hexCalculator.ToHex(0x01010101));

            Assert.AreEqual("10 10 10 10", hexCalculator.ToHex(0x10101010));
            Assert.AreEqual("00 10 10 10", hexCalculator.ToHex(0x00101010));
            Assert.AreEqual("10 10", hexCalculator.ToHex(0x00001010));
            Assert.AreEqual("00 10", hexCalculator.ToHex(0x00000010));
        }
    }
}