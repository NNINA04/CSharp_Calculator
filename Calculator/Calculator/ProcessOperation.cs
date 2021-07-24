using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Calculator
{
    static class ProcessOperation
    {
        static public TOut Run<THandlerResult, TOut>(Delegate handler, IEnumerable<Delegate> inputHandlers,
        IValidator<THandlerResult> validator = null, IFormatter<THandlerResult, TOut> formatter = null)
        {
            CheckHandlersOnNull(handler, inputHandlers);
            HandlerValidate<THandlerResult>(handler, inputHandlers);

            // Основная реализация данного класа
            var handlerArguments = inputHandlers.Select(x => x.DynamicInvoke()).ToArray();
            var result = (THandlerResult)handler.DynamicInvoke(handlerArguments);

            if (validator != null)
            {
                var (isCorrect, errorMessage) = validator.Validate(result);
                if (!isCorrect)
                    throw new ValidationException(errorMessage);
            }

            TOut outValue = (TOut)Convert.ChangeType(result, typeof(TOut));
            if (formatter != null)
                outValue = formatter.Format(result);

            return outValue;
        }

        private static void ShowAndValidateValue<TResult>(TResult value, IValidator<TResult> validator, IFormatter<TResult, string> formatter)
        {
            if (validator != null)
            {
                var (isCorrect, errorMessage) = validator.Validate(value);
                if (!isCorrect)
                {
                    Console.WriteLine(errorMessage);
                    return;
                }
            }

            string printValue = value.ToString();
            if (formatter != null)
                printValue = formatter.Format(value);
            Console.WriteLine($"Result: {printValue}");
        }

        static private void HandlerValidate<THandlerResult>(Delegate handler, IEnumerable<Delegate> inputHandlers) 
        {
            var handlerType = handler.GetType();

            var handlerReturnType = handlerType.GetMethod("Invoke").ReturnType;

            // Является ли возвращаемый тип handler, типом THandlerResult
            if (handlerReturnType != typeof(THandlerResult))
                throw new ArgumentException($"Возвращаемый тип {handlerReturnType} делегата {nameof(handler)} " +
                                            $"не соответстует типу {typeof(THandlerResult)} " +
                                            $"принимаемого параметра {nameof(THandlerResult)} данного метода.");

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

        static private void CheckHandlersOnNull(object handler, IEnumerable<object> inputHandlers)
        {
            // Проверяет handler на null
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

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

        public static IEnumerable<Type> GetParentTypes(this Type type)
        {

            // is there any base type?
            if (type == null)
                yield break;

            // return all implemented or inherited interfaces
            foreach (var i in type.GetInterfaces())
                yield return i;

            // return all inherited types
            var currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;
                currentBaseType = currentBaseType.BaseType;
            }
        }
    }
}


