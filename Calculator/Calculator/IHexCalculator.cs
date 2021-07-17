namespace Calculator
{
    /// <summary>
    /// Интерфейс реализующий конвертацию числа в hex
    /// </summary>
    public interface IHexCalculator
    {
        string ToHex(int x);
    }
}
