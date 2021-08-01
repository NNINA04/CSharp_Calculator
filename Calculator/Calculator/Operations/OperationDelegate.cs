using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Calculator.Operations
{
    class OperationDelegate : IOperationParameters
    {
        private Delegate[] _inputHandlers;

        public OperationDelegate(params Delegate[] inputHandlers)
        {
            _inputHandlers = inputHandlers ?? throw new ArgumentNullException(nameof(inputHandlers));
        }

        /// <summary>
        /// Проверка аргументов основного делегата и делегатов на ввод данных на совместимость их друг с другом
        /// </summary>
        /// <param name="handler">Основной делегат вычисления</param>
        /// <param name="inputHandlers">Перечесление делегатов на ввод данных</param>
        static private void CheckValues(Delegate handler, Delegate[] inputHandlers)
        {
            // Если массив делегатов для основного делегата равняется null, то создаётся пустой массив
            if (inputHandlers == null)
                inputHandlers = Array.Empty<Delegate>();

            // Проверяет делегаты inputHandlers на null
            if (inputHandlers.Any(x => x == null))
                throw new ArgumentException($"Перечисление {nameof(inputHandlers)} содержит в себе элемент со значением null");
            
            var handlerArguments = handler.GetMethodInfo().GetParameters();

            // Является ли количество принимаемых параметров handler количеству объектов в inputHandlers
            if (handlerArguments.Length != inputHandlers.Count())
                throw new ArgumentException($"Количество элементов {nameof(inputHandlers)} не соответствует количесту аргументов делегата {nameof(handler)}");

            // Является ли тип возвращаемого параметра inputHandler в inputHandlers, типом аргументов handler
            int index = 0;
            foreach (var item in inputHandlers)
            {
                var handlerArgumentType = handlerArguments[index].ParameterType;
                var inputReturnType = item.GetType().GetMethod("Invoke").ReturnType;
                if (handlerArgumentType != inputReturnType)
                    throw new ArgumentException($"Возвращаемый тип {inputReturnType} делегата в {nameof(inputHandlers)} под индексом {index} " +
                                                $"не соответствует ожидаемому типу {handlerArgumentType} аргумента делегата {nameof(handler)}");
                index++;
            }
        }


    }
}
