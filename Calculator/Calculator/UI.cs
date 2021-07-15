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
                {1, ("Sum", () => ProcessTwoNumbers(_calc.Sum))},
                {2, ("Substract", () => ProcessTwoNumbers(_calc.Substract)) },
                {3, ("Multiplicate", () => ProcessTwoNumbers(_calc.Multiplicate)) },
                {4, ("Divide", () => ProcessTwoNumbers(_calc.Divide)) },
                {5, ("Exit" , () => Environment.Exit(0))}
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
        /// Обрабатывает 2 запрашиваемых числа
        /// </summary>
        /// <param name="handler">Функция обработчик</param>
        private void ProcessTwoNumbers(Func<double, double, double> handler)
        {
            Console.Write("Enter two numbers: ");
            Console.WriteLine($"Result = {handler(InputDoubleNumber(), InputDoubleNumber())}");
            Console.WriteLine();
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
