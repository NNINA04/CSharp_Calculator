namespace Calculator
{
    /// <summary>
    /// Адаптер формата возвращаемого значения при расчёте факториала
    /// </summary>
    public class FactorialOperationAdapter
    {
        /// <summary>
        /// Интерфейс класса Calculator
        /// </summary>
        private readonly ICalculatorLogic _calculator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="calculator">Объект класса который реализует <see cref="ICalculatorLogic"/></param>
        public FactorialOperationAdapter(ICalculatorLogic calculator)
        {
            _calculator = calculator;
        }

        /// <summary>
        /// Возвращает результат расчёта факториала и входное значение
        /// </summary>
        /// <param name="inputValue">Число</param>
        /// <returns>Tuple входного числа и результата</returns>
        public (int inputValue, int result) Factorial(int inputValue)
        {
            return (inputValue, _calculator.Fact(inputValue));
        }
    }
}
