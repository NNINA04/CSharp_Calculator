using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Calculator.Operations
{
    /// <summary>
    /// Выполняет делегат передав в него результаты других делегатов
    /// </summary>
    /// <typeparam name="OperationResult">Возвращаемый тип основного хендлера</typeparam>
    public class Operation<OperationResult> : IOperation<OperationResult>
    {
        /// <summary>
        /// Основной делегат
        /// </summary>
        private Delegate _handler;

        /// <summary>
        /// Делегаты на ввод значения
        /// </summary>
        private Delegate[] _inputHandlers;

        /// <summary>
        /// Принимаемые значения основного делегата
        /// </summary>
        private object[] _handlerParams;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        public Operation(Delegate handler)
        {
            _handler = handler ?? throw new ArgumentNullException($"{nameof(handler)}");

            var handlerReturnType = handler.GetType().GetMethod("Invoke").ReturnType;

            // Является ли возвращаемый тип handler, типом THandlerResult
            if (handlerReturnType != typeof(OperationResult))
                throw new ArgumentException($"Возвращаемый тип {handlerReturnType} делегата {nameof(handler)} " +
                                            $"не соответстует типу {typeof(OperationResult)} " +
                                            $"принимаемого параметра {nameof(OperationResult)} данного метода.");
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="inputHandlers">Делегаты на ввод данных</param>
        public Operation(Delegate handler, params Delegate[] inputHandlers) : this(handler)
        {
            if (inputHandlers.Any(x => x == null))
            {

                //CheckValues(handler, inputHandlers);
                //_handlerParams = inputHandlers;
                //return;
            }

            CheckValues(_handler, inputHandlers);
            _inputHandlers = inputHandlers;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="handlerParams">Передаваемые значения в основной делегат</param>
        public Operation(Delegate handler, params object[] handlerParams) : this(handler)
        {
            CheckValues(handler, handlerParams);
            _handlerParams = handlerParams;
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры из делегатов ввода
        /// </summary>
        /// <param name="inputHandlers">Делегаты на ввод данных</param>
        /// <returns>Результат выполнения</returns>
        public virtual OperationResult Run(params Delegate[] inputHandlers)
        {
            CheckValues(_handler, inputHandlers);
            var handlerArguments = inputHandlers.Select(x => x.DynamicInvoke()).ToArray();
            return (OperationResult)_handler.DynamicInvoke(handlerArguments);
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры
        /// </summary>
        /// <param name="values">Принимаемые параметры основного хендлера</param>
        /// <returns>Результат выполнения</returns>
        public virtual OperationResult Run(params object[] values)
        {
            CheckValues(_handler, values);
            return (OperationResult)_handler.DynamicInvoke(values.ToArray());
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры
        /// </summary>
        /// <param name="values">Принимаемые параметры основного хендлера</param>
        /// <returns>Результат выполнения</returns>
        object IOperation.Run(params object[] values)
        {
            return Run(values);
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры из делегатов ввода
        /// </summary>
        /// <param name="inputHandlers">Делегаты на ввод данных</param>
        /// <returns>Результат выполнения</returns>
        object IOperation.Run(params Delegate[] inputHandlers)
        {
            return Run(inputHandlers);
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры из делегатов ввода
        /// </summary>
        /// <returns>Результат выполнения</returns>
        object IOperation.Run()
        {
            return Run();
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры из делегатов ввода
        /// </summary>
        /// <returns>Результат выполнения</returns>
        public OperationResult Run()
        {
            if (_handlerParams != null)
                return Run(_handlerParams);
            return Run(_inputHandlers);
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
            {
                try
                {
                    Convert.ChangeType(inputParams[index], handlerArguments[index].ParameterType);
                }
                catch (Exception)
                {
                    throw new ArgumentException($"Тип {inputParams[index].GetType()} под индексом {index} не соответствует ожидаемому типу {handlerArguments[index].ParameterType} аргумента делегата {nameof(handler)}");
                }
                if (inputParams[index].GetType() == typeof(string))
                    throw new ArgumentException($"Возвращаемый тип {typeof(string)} делегата в inputHandlers под индексом {index} не соответствует ожидаемому типу {handlerArguments[index].ParameterType} аргумента делегата handler");
            }
        }
    }
}
