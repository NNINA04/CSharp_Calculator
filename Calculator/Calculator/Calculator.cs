using System;

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
        /// <returns>Экспоненцаильная запись числа</returns>
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
            return $"{temp * sign}e{(moreThenOne ? "+" : "-")}{order}";
        }

        /// <summary>
        /// Вычисляет факториал числа
        /// </summary>
        /// <param name="x">Число</param>
        /// <returns>Факториал</returns>
        /// <exception cref="ArithmeticException">Число меньше нуля</exception>
        public int Fact(int x)
        {
            if (x < 0) throw new ArithmeticException("Число меньше нуля");
            return x <= 1 ? 1 : x * Fact(x - 1);
        }

        /// <summary>
        /// Конвертирует число в hex
        /// </summary>
        /// <param name="hexCalculator">Экземпляр класса расчёта hex значения</param>
        /// <param name="x">Число</param>
        /// <returns>Hex в строковом представлении</returns>
        /// <exception cref="ArgumentNullException">
        /// Объект типа <see cref="IHexCalculator"/> является <see cref="null"/>
        /// </exception>
        public string ToHex(IHexCalculator hexCalculator, int x)
        {
            if (hexCalculator == null) 
                throw new ArgumentNullException(nameof(hexCalculator));
            return hexCalculator.ToHex(x);
        }
    }
}
