using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CalculatorTests
{
    public class UITests
    {
        
        private Calculator.UI _ui;

        [SetUp]
        public void Setup()
        {
            _ui = new Calculator.UI();
        }

        /// <summary>
        /// Проверка на количество элементов
        /// </summary>
        [Test]
        public void TestMenuItemsCount()
        {
            Assert.AreEqual(5, _ui.MenuItems.Count);
        }

        /// <summary>
        /// Проверка первого значения
        /// </summary>
        [Test]
        public void TestMenuItemsDescription()
        {
            Assert.AreEqual("Sum", _ui.MenuItems[1].description);
            Assert.AreEqual("Substract", _ui.MenuItems[2].description);
            Assert.AreEqual("Multiplicate", _ui.MenuItems[3].description);
            Assert.AreEqual("Divide", _ui.MenuItems[4].description);
            Assert.AreEqual("Exit", _ui.MenuItems[5].description);
        }

        /// <summary>
        /// Проверка на Null
        /// </summary>
        [Test]
        public void TestMenuItemsIsNotNull()
        {
            Assert.IsNotNull(_ui.MenuItems[1].action);
            Assert.IsNotNull(_ui.MenuItems[2].action);
            Assert.IsNotNull(_ui.MenuItems[3].action);
            Assert.IsNotNull(_ui.MenuItems[4].action);
            Assert.IsNotNull(_ui.MenuItems[5].action);
        }

        /// <summary>
        /// Проверка на тип объекта
        /// </summary>
        [Test]
        public void TestMenuItemsType()
        {
            Assert.AreEqual(typeof(Action), _ui.MenuItems[1].action.GetType());
        }
    }
}