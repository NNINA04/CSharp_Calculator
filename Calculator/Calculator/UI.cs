using Calculator.Additions;
using Calculator.Additions.Formatters;
using Calculator.Additions.Validators;
using Calculator.Operations;
using Calculator.Operations.Parameters;
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
        protected Dictionary<int, (string description, IOperation operation)> MenuItems { get; private set; }

        /// <summary>
        /// Словарь содержащий элементы меню и методы для работы с hex
        /// </summary>
        protected Dictionary<int, (string description, IOperation operation)> HexMenu { get; private set; }

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
                {1, ("BitConverterCalculation", new Operation<string>(calc.ToHex,  new DelegateParameters(()=>bitConverterHexCalculator, InputValueAndValidate<int>))) },
                {2, ("DictionaryConverter", new Operation<string>(calc.ToHex,  new DelegateParameters(()=>matchingTypeToHex,InputValueAndValidate<int>))) }
            };
            MenuItems = new Dictionary<int, (string description, IOperation operation)>
            {
                {1, ("Exit" , new Operation(()=>Environment.Exit(0)))},
                {2, ("Sum", new Operation<double>(calc.Sum, new DelegateParameters(InputValueAndValidate<double>, InputValueAndValidate<double>)))},
                {3, ("Substract",  new Operation<double>(calc.Substract, new DelegateParameters(InputValueAndValidate<double>, InputValueAndValidate<double>)))},
                {4, ("Multiplicate", new Operation<double>(calc.Multiplicate,new DelegateParameters(InputValueAndValidate<double>,InputValueAndValidate<double>)))},
                {5, ("Divide",  new Operation<double>(calc.Divide, new DelegateParameters(InputValueAndValidate<double>,InputValueAndValidate<double>)).AddValidator(doubleValidator))},
                {6, ("Sqrt", new Operation<double>(calc.Sqrt, new DelegateParameters(InputValueAndValidate<double>)))},
                {7, ("Cbrt", new Operation<double>(calc.Cbrt, new DelegateParameters(InputValueAndValidate<double>)))},
                {8, ("Exp", new Operation<string>(calc.Exp, new DelegateParameters(InputValueAndValidate<double>)))},
                {9, ("Fact", new Operation<(int,int)>(factorialAdapter.Factorial, new DelegateParameters(InputValueAndValidate<int>)).AddFormatter(factorialFormatter))},
                {10,("Hex", new Operation(SelectAction, HexMenu))}
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
                int inputvalue = InputValueAndValidate<int>();
                menu.TryGetValue(inputvalue, out var item);
                if (inputvalue == 1)
                    item.operation.RunWithoutReturnValue();
                else
                    ShowValue(item.operation.Run());
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
        /// Отображает результат выполнения операции
        /// </summary>
        /// <param name="value"></param>
        private static void ShowValue(object value)
        {
            string printValue = value.ToString();
            Console.WriteLine($"Result: {printValue}");
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
        private static TRequiredType InputValueAndValidate<TRequiredType>()
        {
            // Проверка на запрещённые типы
            if (typeof(TRequiredType) != typeof(int) && typeof(TRequiredType) != typeof(double) && typeof(TRequiredType) != typeof(string))
                throw new ArgumentException($"Тип {typeof(TRequiredType)} не разрешён!");
            try
            {
                Console.Write($"Enter a value: ");
                return (TRequiredType)Convert.ChangeType(Console.ReadLine(), typeof(TRequiredType));
            }
            catch (FormatException ex)
            {
               throw new FormatException("You entered a wrong value !", ex);
            }
        }
    }
}
