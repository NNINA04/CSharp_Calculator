namespace Calculator
{
    /// <summary>
    /// Интерфейс реализующий конвертацию числа в hex
    /// </summary>
    public interface IHexCalculator
    {
        /// <summary>
        /// Конвертирует число в шестнадцатиричном вид
        /// </summary>
        /// <param name="x">Число</param>
        /// <returns>Число в шестнадцатиричном виде</returns>
        string ToHex(int x);
    }
}
