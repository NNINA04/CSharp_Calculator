using System;
using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    /// Класс UI для консольного приложения
    /// </summary>
    public class UI
    {
        /// <summary>
        /// Словарь содержащий элементы меню и действий
        /// </summary>
        public Dictionary<int, (string description, Action action)> MenuItems { get; private set; }

        /// <summary>
        /// Словарь содержащий элементы меню и методы для работы с hex
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
            MatchingTypeToHex matchingTypeToHex = new();
            HexMenu = new Dictionary<int, (string description, Action action)>
            {
                {1, ("BitConverterCalculation", ()=>ProcessOperation(calc.ToHex, () => bitConverterHexCalculator,  InputValue<int>, "Enter a value: ")) },
                {2, ("DictionaryConverter", ()=>ProcessOperation(calc.ToHex, () => matchingTypeToHex,  InputValue<int>, "Enter a value: ")) }
            };

            MenuItems = new Dictionary<int, (string, Action)>
            {
                {1, ("Exit" , () => Environment.Exit(0))},
                {2, ("Sum", () => ProcessOperation(calc.Sum, InputValue<double>, InputValue<double>, "Enter two values: "))},
                {3, ("Substract", () => ProcessOperation(calc.Substract, InputValue<double>, InputValue<double>, "Enter two values: "))},
                {4, ("Multiplicate", () => ProcessOperation(calc.Multiplicate, InputValue<double>, InputValue<double>, "Enter two values: "))},
                {5, ("Divide", () => ProcessOperation(calc.Divide, InputValue<double>, InputValue<double>, "Enter two values: ", doubleValidator))},
                {6, ("Sqrt", () => ProcessOperation(calc.Sqrt, InputValue<double>))},
                {7, ("Cbrt", () => ProcessOperation(calc.Cbrt, InputValue<double>))},
                {8, ("Exp", () => ProcessOperation(calc.Exp, InputValue<double>))},
                {9, ("Fact", () => ProcessOperation(fa.Factorial, InputValue<int>, formatter:factorialFormatter))},
                {10, ("Hex", () =>  SelectAction(HexMenu))}
            };
        }

        /// <summary>
        /// Запускает интерфейс
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
        private static void SelectAction(IDictionary<int, (string description, Action action)> menu, bool nested = false)
        {
            try
            {
                ShowMenu(menu);
                if (int.TryParse(InputValue<string>(), out var item))
                {
                    menu.TryGetValue(item, out var a);
                    a.action();
                }
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
        /// <typeparam name="TArg1">Тип первой переменной, которую необходимо ввести</typeparam>
        /// <typeparam name="TArg2">Тип второй переменной, которую необходимо ввести</typeparam>
        /// <typeparam name="TResult">Тип возвращаемого значения передаваемой функции</typeparam>
        /// <param name="handler">Функция обработки</param>
        /// <param name="inputHandler1">Функция ввода значения первой переменной</param>
        /// <param name="inputHandler2">Функция ввода значения второй переменной</param>
        /// <param name="message">Сообщение о вводе</param>
        /// <param name="validator">Валидатор результата</param>
        /// <param name="formatter">Форматтер результата</param>
        private static void ProcessOperation<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> handler,
        Func<TArg1> inputHandler1, Func<TArg2> inputHandler2, string message,
        IValidator<TResult> validator = null,
        IFormatter<TResult, string> formatter = null)
        {
            Console.Write(message);
            TResult result = handler(inputHandler1(), inputHandler2());
            ShowAndValidateValue(result, validator, formatter);
        }

        /// <summary>
        /// Запрашивает ввод значения, проводит валидацию и выдаёт результат
        /// </summary>
        /// <typeparam name="TArg1">Тип переменной, которую необходимо ввести</typeparam>
        /// <typeparam name="TResult">Тип возвращаемого значения передаваемой функции</typeparam>
        /// <param name="handler">Функция обработки</param>
        /// <param name="inputHandler">Функция ввода значения переменной</param>
        /// <param name="validator">Валидатор результата</param>
        /// <param name="formatter">Форматтер результата</param>
        private static void ProcessOperation<TArg1, TResult>(Func<TArg1, TResult> handler,
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
        /// <param name="formatter">Форматтер переменной</param>
        private static void ShowAndValidateValue<TResult>(TResult value, IValidator<TResult> validator, IFormatter<TResult, string> formatter)
        {
            if (validator != null)
            {
                var (isCorrect, errorMessage) = validator.Validate(value);
                if (!isCorrect)
                {
                    Console.WriteLine(errorMessage);
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
        /// <param name="menu">Словарь с элементами меню</param>
        private static void ShowMenu(IDictionary<int, (string description, Action action)> menu)
        {
            Console.WriteLine("Select an action:");
            foreach (var action in menu)
                Console.WriteLine($"{action.Key}: {action.Value.description}");
            Console.Write("..:");
        }

        /// <summary>
        /// Запрашивает ввод значения
        /// </summary>
        /// <typeparam name="T">Параметр должен быть <see cref="int"/> или <see cref="double"/> или <<see cref="string"/></typeparam>
        /// <returns>Значение</returns>
        static T InputValue<T>() where T : notnull
        {
            if (typeof(T) != typeof(int) && typeof(T) != typeof(double) && typeof(T) != typeof(string))
                throw new ArgumentException("Данный тип не разрешён");
            return (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
        }
    }
}
