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
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));

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
                return;
            }

            //CheckArguments(_handler, inputHandlers);
            //_inputHandlers = inputHandlers;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="handler">Основной делегат</param>
        /// <param name="handlerParams">Передаваемые значения в основной делегат</param>
        public Operation(Delegate handler, params object[] handlerParams) : this(handler)
        {
            //CheckValues(handler, handlerParams);
            //_handlerParams = handlerParams;
        }

        /// <summary>
        /// Выполняет основной делегат класса и передаёт в него параметры из делегатов ввода
        /// </summary>
        /// <param name="inputHandlers">Делегаты на ввод данных</param>
        /// <returns>Результат выполнения</returns>
        public virtual OperationResult Run(params Delegate[] inputHandlers)
        {
            //CheckArguments(_handler, inputHandlers);
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
            //CheckValues(_handler, values);
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
    }
}
