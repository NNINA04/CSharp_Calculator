using System;

namespace Calculator.Extensions
{
    /// <summary>
    /// Класс c методами расширениями класса <see cref="Type"/>
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Проверяет является ли тип <see cref="Nullable"/>
        /// </summary>
        /// <param name="objectType">Проверямый тип</param>
        /// <returns>Если тип значения является <see cref="Nullable"/></returns>
        /// <exception cref="ArgumentNullException">Аргумент <paramref name="objectType"/> является null</exception>
        public static bool IsNullable(this Type objectType)
        {
            if (objectType == null)
                throw new ArgumentNullException(nameof(objectType));
            return Nullable.GetUnderlyingType(objectType) != null;
        }
    }
}
