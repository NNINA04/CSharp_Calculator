using Calculator.Operations.Formatters;

namespace Calculator.Additions.Formatters
{
    /// <summary>
    /// Форматтер факториала
    /// </summary>
    public class FactorialFormatter : IFormatter<(int, int), string>
    {
        /// <summary>
        /// Возвращает форматированное значение в виде x! = y
        /// </summary>
        /// <param name="values">Tuple входного и выходного числа</param>
        /// <returns>Отформатированное значение факториала</returns>
        public string Format((int, int) values)
        {
            int inputValue = values.Item1;
            int factorial = values.Item2;
            return $"{inputValue}! = {factorial}";
        }

        /// <summary>
        /// Возвращает форматированное значение в виде x! = y
        /// </summary>
        /// <param name="values">Tuple входного и выходного числа</param>
        /// <returns>Отформатированное значение факториала</returns>
        object IFormatter.Format(object values)
        {
            return Format(((int, int))values);
        }
    }
}
