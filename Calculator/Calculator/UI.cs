using System;

namespace Calculator
{
    public class UI
    {
        /// <summary>
        /// Объект класса Calculator
        /// </summary>
        private Calculator _calc;

        /// <summary>
        /// Конструктор
        /// </summary>
        public UI()
        {
            _calc = new Calculator();
        }

        /// <summary>
        /// Выполняет действие в зависимости от ввода пользователя
        /// </summary>
        public void Run()
        {
            while (true)
            {
                ShowMenu();

                int action = int.Parse(Console.ReadLine());

                double result = 0;

                switch (action)
                {
                    case 1:
                        result = Sum();
                        break;
                    case 2:
                        result = Substract();
                        break;
                    case 3:
                        result = Multiplicate();
                        break;
                    case 4:
                        result = Divide();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("You entered wrong value !");
                        continue;
                }
                Console.WriteLine($"Result = {result}");
            }
        }

        /// <summary>
        /// Выводит меню
        /// </summary>
        private void ShowMenu()
        {
            Console.WriteLine("Select an action:");
            Console.WriteLine("1: Sum");
            Console.WriteLine("2: Substract");
            Console.WriteLine("3: Multiplicate");
            Console.WriteLine("4: Divide");
            Console.WriteLine("5: Exit");
            Console.Write("..:");
        }

        /// <summary>
        /// Выполнение сложения
        /// </summary>
        /// <returns>Сумма</returns>
        private int Sum()
        {
            Console.Write("Enter two numbers: ");
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());

            return _calc.Sum(x, y);
        }

        /// <summary>
        /// Выполнение вычитания
        /// </summary>
        /// <returns>Разница</returns>
        private int Substract()
        {
            Console.Write("Enter two numbers: ");
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());
            return _calc.Substract(x, y);
        }

        /// <summary>
        /// Выполнение умножения
        /// </summary>
        /// <returns>Результат перемножения</returns>
        private int Multiplicate()
        {
            Console.Write("Enter two numbers: ");
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());
            return _calc.Multiplicate(x, y);
        }

        /// <summary>
        /// Выполнение деления
        /// </summary>
        /// <returns>Результат деления</returns>
        private double Divide()
        {
            Console.Write("Enter two numbers: ");
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());
            return _calc.Divide(x, y);
        }
    }
}
