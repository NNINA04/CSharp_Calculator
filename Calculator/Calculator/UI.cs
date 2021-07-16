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
        /// Конструктор
        /// </summary>
        public UI()
        {
            Calculator _calc = new();
            
            MenuItems = new Dictionary<int, (string, Action)>
            {
                {1, ("Exit" , () => Environment.Exit(0))},
                {2, ("Sum", () => ProcessOperation(_calc.Sum, InputDoubleNumber, InputDoubleNumber))},
                {3, ("Substract", () => ProcessOperation(_calc.Substract, InputDoubleNumber, InputDoubleNumber))},
                {4, ("Multiplicate", () => ProcessOperation(_calc.Multiplicate, InputDoubleNumber, InputDoubleNumber))},
                {5, ("Divide", () => ProcessOperation(_calc.Divide, InputDoubleNumber, InputDoubleNumber))},
                {6, ("Sqrt", () => ProcessOperation(_calc.Sqrt, InputDoubleNumber))},
                {7, ("Cbrt", () => ProcessOperation(_calc.Cbrt, InputDoubleNumber))},
                {8, ("Exp", () => ProcessOperation(_calc.Exp, InputDoubleNumber))}
            };
        }

        /// <summary>
        /// Отображения интерфейса
        /// </summary>
        public void Run()
        {
            while (true)
            {
                ShowMenu();
                try
                {
                    if (MenuItems.TryGetValue(InputIntNumber(), out var item))
                        item.action();
                    else
                    {
                        Console.WriteLine("You entered wrong value !");
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
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
        IValidator<TResult> validator = null)
        {
            Console.Write("Enter two values: ");
            TResult result = handler(inputHandler1(), inputHandler2());
            ShowAndValidateValue(result, validator);
        }

        /// <summary>
        /// Запрашивает ввод значения, проводит валидацию и выдаёт результат
        /// </summary>
        /// <typeparam name="TArg1">Тип переменной, которою необходимо ввести</typeparam>
        /// <typeparam name="TResult">Тип возвращаемого значения передаваемой функции</typeparam>
        /// <param name="handler">Функция обработки</param>
        /// <param name="inputHandler">Функция ввода значения переменной</param>
        /// <param name="validator">Валидатор результата</param>
        private void ProcessOperation<TArg1, TResult>(Func<TArg1, TResult> handler, Func<TArg1> inputHandler, IValidator<TResult> validator = null)
        {
            Console.Write("Enter a value: ");
            TResult result = handler(inputHandler());
            ShowAndValidateValue(result, validator);
        }

        /// <summary>
        /// Отображает значение переменной и проводит её валидацию
        /// </summary>
        /// <typeparam name="TResult">Тип переменной</typeparam>
        /// <param name="value">Проверяеммая и выводимая переменная</param>
        /// <param name="validator">Валидатор переменной</param>
        private void ShowAndValidateValue<TResult>(TResult value, IValidator<TResult> validator)
        {
            if (validator != null)
            {
                var resultOfValidation = validator.Validate(value);
                if (!resultOfValidation.isCorrect)
                {
                    Console.WriteLine(resultOfValidation.errorMessage + Environment.NewLine);
                    return;
                }
            }
            Console.WriteLine($"Result = {value}" + Environment.NewLine);
        }

        /// <summary>
        /// Выводит меню
        /// </summary>
        private void ShowMenu()
        {
            Console.WriteLine("Select an action:");
            foreach (var action in MenuItems)
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
