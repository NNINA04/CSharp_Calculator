using Calculator.Operations;
using Calculator.Operations.Formatters;
using Calculator.Operations.Validators;
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
        public Dictionary<int, (string description, IOperation operation)> MenuItems { get; private set; }

        /// <summary>
        /// Словарь содержащий элементы меню и методы для работы с hex
        /// </summary>
        public Dictionary<int, (string description, IOperation operation)> HexMenu { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public UI()
        {
            Calculator calc = new();
            FactorialOperationAdapter factorialAdapter = new(calc);
            DoubleValidator doubleValidator = new();
            FactorialFormatter factorialFormatter = new();
            BitConverterHexCalculator bitConverterHexCalculator = new();
            MatchingTypeToHex matchingTypeToHex = new();

            HexMenu = new Dictionary<int, (string description, IOperation operation)>
            {
                {1, ("BitConverterCalculation", new Operation<int>(calc.ToHex,  InputValueAndValidate<int>())) }, // (), => bitConverterHexCalculator,  InputValueAndValidate<int>, "Enter a value: "
                {2, ("DictionaryConverter", new Operation<int>(calc.ToHex,  InputValueAndValidate<int>())) } // , () => matchingTypeToHex,  InputValueAndValidate<int>, "Enter a value: "))
            };

            MenuItems = new Dictionary<int, (string description, IOperation operation)>
            {
                {1, ("Exit" , new Operation(()=>Environment.Exit(0)))}, 
                {2, ("Sum", new Operation<double>(calc.Sum,  InputValueAndValidate<double>(), InputValueAndValidate<double>()))},
                {3, ("Substract",  new Operation<double>(calc.Substract, InputValueAndValidate<double>(), InputValueAndValidate<double>()))},
                {4, ("Multiplicate", new Operation<double>(calc.Multiplicate,InputValueAndValidate<double>(),InputValueAndValidate<double>()))},
                {5, ("Divide",  new Operation<double>(calc.Divide, InputValueAndValidate<double>(),InputValueAndValidate<double>()))},
                {6, ("Sqrt", new Operation<double>(calc.Sqrt, InputValueAndValidate<double>()))},
                {7, ("Cbrt", new Operation<double>(calc.Cbrt, InputValueAndValidate<double>()))},
                {8, ("Exp", new Operation<double>(calc.Exp, InputValueAndValidate<double>()))},
                {9, ("Fact", new Operation<int>(factorialAdapter.Factorial, InputValueAndValidate<double>()))}, // .AddFormatter(factorialFormatter)
                {10, ("Hex", new Operation(()=>SelectAction(HexMenu)))}  // Позже добавить не тепизированный класс который будет принимать Action
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
        private static void SelectAction(IDictionary<int, (string description, IOperation operation)> menu, bool nested = false)
        {
            try
            {
                ShowMenu(menu);
                menu.TryGetValue(InputValueAndValidate<int>(), out var item);
                Console.WriteLine(item.operation.Run());
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
        /// Выводит меню
        /// </summary>
        /// <param name="menu">Словарь с элементами меню</param>
        private static void ShowMenu(IDictionary<int, (string description, IOperation operation)> menu)
        {
            Console.WriteLine("Select an action:");
            foreach (var action in menu)
                Console.WriteLine($"{action.Key}: {action.Value.description}");
            Console.Write("..:");
        }

        /// <summary>
        /// Запрашивает ввод значения и проводит валидацию
        /// </summary>
        /// <typeparam name="TRequiredType">
        /// Тип, в который конвертируется значение.
        /// Разрешенные типы:  <see cref="int"/>, <see cref="double"/> или <see cref="string"/>
        /// </typeparam>
        /// <returns>Значение</returns>
        /// <exception cref="ArgumentException">Неверный тип возвращаемого значения</exception>
        /// <exception cref="FormatException">Неверное введённое значение</exception>
        static private TRequiredType InputValueAndValidate<TRequiredType>()
        {
            // Проверка на запрещённые типы
            if (typeof(TRequiredType) != typeof(int) && typeof(TRequiredType) != typeof(double) && typeof(TRequiredType) != typeof(string))
                throw new ArgumentException($"Тип {typeof(TRequiredType)} не разрешён!");
            try
            {
                return (TRequiredType)Convert.ChangeType(Console.ReadLine(), typeof(TRequiredType));
            }
            catch (FormatException ex)
            {
                throw new FormatException("You entered a wrong value !", ex);
            }
        }
    }
}
