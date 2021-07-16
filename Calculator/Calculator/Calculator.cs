using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    /// <summary>
    /// Выполняет арифметические операции
    /// </summary>
    public class Calculator : ICalculatorLogic
    {
        /// <summary>
        /// Делит первое число на второе
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Результат деления</returns>
        public double Divide(double x, double y)
        {
            return x / y;
        }

        /// <summary>
        /// Вычитает 2 числа
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Разницу</returns>
        public double Substract(double x, double y)
        {
            return x - y;
        }

        /// <summary>
        /// Перемножает 2 числа
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Результат перемножения</returns>
        public double Multiplicate(double x, double y)
        {
            return x * y;
        }

        /// <summary>
        /// Сумирует 2 числа
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Сумма</returns>
        public double Sum(double x, double y)
        {
            return x + y;
        }

        /// <summary>
        /// Вычисляет квадратный корень
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Квадратный корень числа</returns>
        public double Sqrt(double x)
        {
            return Math.Sqrt(x);
        }

        /// <summary>
        /// Вычисляет кубический корень
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Кубический корень числа</returns>
        public double Cbrt(double x)
        {
            return Math.Cbrt(x);
        }

        /// <summary>
        /// Преобразует число в экспоненциальную запись
        /// </summary>
        /// <param name="x">Число</param>
        /// <returns>Экспоненцаильную запись числа</returns>
        public string Exp(double x)
        {
            double temp = Math.Abs(x);
            int sign = x < 0 ? -1 : 1;
            bool moreThenOne = temp >= 1;

            uint order = 0;

            while (!(temp >= 1 && temp < 10))
            {
                temp = moreThenOne ? temp / 10 : temp * 10;
                order++;
            }

            return  $"{temp * sign}e{(moreThenOne ? "+" : "-")}{order}";
        }
    }
}
