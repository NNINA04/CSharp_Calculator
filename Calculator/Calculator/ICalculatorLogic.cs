namespace Calculator
{
    /// <summary>
    /// Интерфейс логики калькулятора
    /// </summary>
    public interface ICalculatorLogic
    {
        /// <summary>
        /// Сумирует 2 числа
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Сумма</returns>
        double Sum(double x, double y);

        /// <summary>
        /// Вычитает 2 числа
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Разницу</returns>
        double Substract(double x, double y);

        /// <summary>
        /// Перемножает 2 числа
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Результат перемножения</returns>
        double Multiplicate(double x, double y);

        /// <summary>
        /// Делит первое число на второе
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Результат деления</returns>
        double Divide(double x, double y);

        /// <summary>
        /// Вычисляет квадратный корень
        /// </summary>
        /// <param name="x">Число</param>
        /// <returns>Квадратный корень числа</returns>
        double Sqrt(double x);

        /// <summary>
        /// Вычисляет кубический корень
        /// </summary>
        /// <param name="x">Число</param>
        /// <returns>Кубический корень числа</returns>
        double Cbrt(double x);

        /// <summary>
        /// Представляет число в экспоненциальном виде
        /// </summary>
        /// <param name="x">Число</param>
        /// <returns>Число в экспоненциальном виде</returns>
        string Exp(double x);
    }
}
