﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    /// <summary>
    /// Выполняет арифметические операции
    /// </summary>
    class Calculator : ICalculatorLogic
    {
        /// <summary>
        /// Делит первое число на второе
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Результат деления</returns>
        public double Divide(int x, int y)
        {
            return (double)x / y;
        }

        /// <summary>
        /// Вычитает 2 числа
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Разницу</returns>
        public int Substract(int x, int y)
        {
            return x - y;
        }

        /// <summary>
        /// Перемножает 2 числа
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Результат перемножения</returns>
        public int Multiplicate(int x, int y)
        {
            return x * y;
        }

        /// <summary>
        /// Сумирует 2 числа
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Сумма</returns>
        public int Sum(int x, int y)
        {
            return x + y;
        }
    }
}