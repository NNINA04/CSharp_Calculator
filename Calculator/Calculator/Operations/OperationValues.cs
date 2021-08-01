using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Calculator.Operations
{
    class OperationValues : IOperationParameters
    {
        private object[] _values;

        public OperationValues(params object[] values)
        {
            _values = values?? throw new ArgumentNullException(nameof(values));
        }

        /// <summary>
        /// Проверяет соответствуют ли типы принимаемых параметров основного хендлера на типы значений
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="inputParams">Принимаемые параметры основного делегата</param>
        static void CheckValues(Delegate handler, object[] inputParams)
        {
            // Если массив входных параметров для основного делегата равняется null, то создаётся пустой массив
            if (inputParams == null)
                inputParams = Array.Empty<object>();

            var handlerArguments = handler.GetMethodInfo().GetParameters();
            int handlerArgumentsLength = handlerArguments.Length;
            int handlerParamsLength = inputParams.Length;

            // Проверяет количество
            if (handlerParamsLength != handlerArgumentsLength)
                throw new ArgumentException($"Количество элементов {nameof(inputParams)} не соответствует количесту аргументов делегата {nameof(handler)}");

            // Проверяет совместимости типов
            for (int index = 0; index < handlerParamsLength; index++)
                try
                {
                    Convert.ChangeType(inputParams[index], handlerArguments[index].ParameterType);
                }
                catch (Exception)
                {
                    throw new ArgumentException($"Тип {inputParams[index].GetType()} под индексом {index} не соответствует ожидаемому типу {handlerArguments[index].ParameterType} аргумента делегата {nameof(handler)}");
                }
        }
    }
}
