using System;
using System.Collections.Generic;

namespace Calculator
{
    public class UI
    {
        /// <summary>
        /// Словарь содержащий элементы меню и действий
        /// </summary>
        public Dictionary<int, (string description, Action action)> MenuItems { get; private set; }

        /// <summary>
        /// Словарь содержащий элементы меню и действий для Hex
        /// </summary>
        public Dictionary<int, (string description, Action action)> HexMenu { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public UI()
        {
            Calculator calc = new();
            FactorialProcessAdapter fa = new(calc);
            DoubleValidator doubleValidator = new();
            FactorialFormatter factorialFormatter = new();
            BitConverterHexCalculator bitConverterHexCalculator = new();
            HexMenu = new Dictionary<int, (string description, Action action)>
            {
                {1, ("BitConverterCalculation", ()=>ProcessOperation(calc.ToHex, () => bitConverterHexCalculator,  InputIntNumber)) }, // Решить проблему с "Enter two values: " 
                {2, ("...", ()=>ProcessOperation(calc.Divide, InputDoubleNumber, InputDoubleNumber, doubleValidator)) }
            };

            MenuItems = new Dictionary<int, (string, Action)>
            {
                {1, ("Exit" , () => Environment.Exit(0))},
                {2, ("Sum", () => ProcessOperation(calc.Sum, InputDoubleNumber, InputDoubleNumber))},
                {3, ("Substract", () => ProcessOperation(calc.Substract, InputDoubleNumber, InputDoubleNumber))},
                {4, ("Multiplicate", () => ProcessOperation(calc.Multiplicate, InputDoubleNumber, InputDoubleNumber))},
                {5, ("Divide", () => ProcessOperation(calc.Divide, InputDoubleNumber, InputDoubleNumber, doubleValidator))},
                {6, ("Sqrt", () => ProcessOperation(calc.Sqrt, InputDoubleNumber))},
                {7, ("Cbrt", () => ProcessOperation(calc.Cbrt, InputDoubleNumber))},
                {8, ("Exp", () => ProcessOperation(calc.Exp, InputDoubleNumber))},
                {9, ("Fact", () => ProcessOperation(fa.Factorial, InputIntNumber, formatter:factorialFormatter))},
                {10, ("Hex", () =>  SelectAction(HexMenu))}
            };
        }

        /// <summary>
        /// Отображения основной интерфейса
        /// </summary>
        public void Run()
        {
            while (true)
                SelectAction(MenuItems, nested: true);
        }

        /// <summary>
        /// Выводит меню и позволяет пользователю выбрать из него действие
        /// </summary>
        /// <param name="menu">Словарь с элементами меню</param>
        /// <param name="nested">Вложенное ли меню</param>
        private void SelectAction(IDictionary<int, (string description, Action action)> menu, bool nested = false)
        {
            try
            {
                ShowMenu(menu);
                if (menu.TryGetValue(InputIntNumber(), out var item))
                    item.action();
                else
                    Console.WriteLine("You entered a wrong value !");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (nested)
                    Console.WriteLine();
            }
        }

        /// <summary>
        /// Запрашивает ввод двух значений, проводит валидацию и выдаёт результат
        /// </summary>
        /// <typeparam name="TArg1">Тип первой переменной, которою необходимо ввести</typeparam>
        /// <typeparam name="TArg2">Тип второй переменной, которою необходимо ввести</typeparam>
        /// <typeparam name="TResult">Тип возвращаемого значения передаваемой функции</typeparam>
        /// <param name="handler">Функция обработки</param>
        /// <param name="inputHandler1">Функция ввода значения первой переменной</param>
        /// <param name="inputHandler2">Функция ввода значения второй переменной</param>
        /// <param name="validator">Валидатор результата</param>
        private void ProcessOperation<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> handler,
        Func<TArg1> inputHandler1, Func<TArg2> inputHandler2,
        IValidator<TResult> validator = null,
        IFormatter<TResult, string> formatter = null)
        {
            Console.Write("Enter two values: ");
            TResult result = handler(inputHandler1(), inputHandler2());
            ShowAndValidateValue(result, validator, formatter);
        }

        /// <summary>
        /// Запрашивает ввод значения, проводит валидацию и выдаёт результат
        /// </summary>
        /// <typeparam name="TArg1">Тип переменной, которою необходимо ввести</typeparam>
        /// <typeparam name="TResult">Тип возвращаемого значения передаваемой функции</typeparam>
        /// <param name="handler">Функция обработки</param>
        /// <param name="inputHandler">Функция ввода значения переменной</param>
        /// <param name="validator">Валидатор результата</param>
        private void ProcessOperation<TArg1, TResult>(Func<TArg1, TResult> handler,
        Func<TArg1> inputHandler,
        IValidator<TResult> validator = null,
        IFormatter<TResult, string> formatter = null)
        {
            Console.Write("Enter a value: ");
            TResult result = handler(inputHandler());
            ShowAndValidateValue(result, validator, formatter);
        }

        /// <summary>
        /// Отображает значение переменной и проводит её валидацию
        /// </summary>
        /// <typeparam name="TResult">Тип переменной</typeparam>
        /// <param name="value">Проверяеммая и выводимая переменная</param>
        /// <param name="validator">Валидатор переменной</param>
        private void ShowAndValidateValue<TResult>(TResult value, IValidator<TResult> validator, IFormatter<TResult, string> formatter)
        {
            if (validator != null)
            {
                var resultOfValidation = validator.Validate(value);
                if (!resultOfValidation.isCorrect)
                {
                    Console.WriteLine(resultOfValidation.errorMessage);
                    return;
                }
            }

            string printValue = value.ToString();
            if (formatter != null)
                printValue = formatter.Format(value);
            Console.WriteLine($"Result: {printValue}");
        }

        /// <summary>
        /// Выводит меню
        /// </summary>
        private void ShowMenu(IDictionary<int, (string description, Action action)> menu)
        {
            Console.WriteLine("Select an action:");
            foreach (var action in menu)
                Console.WriteLine($"{action.Key}: {action.Value.description}");
            Console.Write("..:");
        }

        /// <summary>
        /// Запрашивает ввод числа и проводит валидацию
        /// </summary>
        /// <returns>Число</returns>
        private static int InputIntNumber()
        {
            if (!int.TryParse(Console.ReadLine(), out int value))
                throw new FormatException("You entered not valid value");
            return value;
        }

        /// <summary>
        /// Запрашивает ввод числа и проводит валидацию
        /// </summary>
        /// <returns>Число</returns>
        private static double InputDoubleNumber()
        {
            if (!double.TryParse(Console.ReadLine(), out double value))
                throw new FormatException("You entered not valid value");
            return value;
        }
    }
}
