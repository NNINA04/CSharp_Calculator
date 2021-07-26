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
    class Operation<OperationResult> : IOperation<OperationResult>
    {
        /// <summary>
        /// Основной делегат
        /// </summary>
        private Delegate _handler;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        public Operation(Delegate handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }
        
        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры из делегатов ввода
        /// </summary>
        /// <param name="inputHandlers">Делегаты на ввод данных</param>
        /// <returns>Результат выполнения</returns>
        public virtual OperationResult Run(params Delegate[] inputHandlers)
        {
            if (inputHandlers == null)
                inputHandlers = Array.Empty<Delegate>();

            CheckHandlersOnNull(inputHandlers);
            HandlerValidate(_handler, inputHandlers);

            var handlerArguments = inputHandlers.Select(x => x.DynamicInvoke()).ToArray();
            return (OperationResult)_handler.DynamicInvoke(handlerArguments);
        }

        /// <summary>
        /// Проверка аргументов основного делегата и делегатов на ввод данных на совместимость их друг с другом
        /// </summary>
        /// <param name="handler">Основной делегат вычисления</param>
        /// <param name="inputHandlers">Перечесление делегатов на ввод данных</param>
        static private void HandlerValidate(Delegate handler, IEnumerable<Delegate> inputHandlers)
        {
            var handlerType = handler.GetType();

            var handlerReturnType = handlerType.GetMethod("Invoke").ReturnType;

            // Является ли возвращаемый тип handler, типом THandlerResult
            if (handlerReturnType != typeof(OperationResult))
                throw new ArgumentException($"Возвращаемый тип {handlerReturnType} делегата {nameof(handler)} " +
                                            $"не соответстует типу {typeof(OperationResult)} " +
                                            $"принимаемого параметра {nameof(OperationResult)} данного метода.");
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
                    throw new ArgumentException($"Возвращаемый тип {inputReturnType} делегата в {nameof(inputHandlers)} под индексом {index}" +
                                                $" не соответствует ожидаемому типу {handlerArgumentType} аргумента делегата {nameof(handler)}");
                index++;
            }
        }
        
        /// <summary>
        /// Проверка делегатов на ввод данных на null
        /// </summary>
        /// <param name="inputHandlers">Перечесление делегатов на ввод данных</param>
        static private void CheckHandlersOnNull(IEnumerable<Delegate> inputHandlers)
        {
            // Проверяет inputHandlers на null
            if (inputHandlers == null)
                throw new ArgumentNullException(nameof(inputHandlers));

            // Проверяет объекты inputHandlers на null
            foreach (var item in inputHandlers)
            {
                // Проверяет item на null
                if (item == null)
                    throw new ArgumentException($"Перечисление {nameof(inputHandlers)} содержит в себе элемент со значением null");
            }
        }
    }
}
