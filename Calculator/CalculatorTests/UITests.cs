using NUnit.Framework;

namespace CalculatorTests
{
    public class UITests
    {
        private Calculator.UI _ui = new();
        private string[] _menuActions = new string[] { "Exit", "Sum", "Substract", "Multiplicate", "Divide", "Sqrt", "Cbrt", "Exp", "Fact", "Hex" };

        [Test]
        public void TestMenuItemsDescription()
        {
            for (int i = 0; i < _ui.MenuItems.Count; i++)
            {
                Assert.AreEqual(_menuActions[i], _ui.MenuItems[i+1].description);
            }
        }

        [Test]
        public void IsMenuItemsIsNotNull()
        {
            for (int i = 0; i < _ui.MenuItems.Count; i++)
            {
                Assert.IsNotNull(_ui.MenuItems[i+1]);
            }
        }
    }
}
