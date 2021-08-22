namespace Calculator
{
    /// <summary>
    /// Класс c методами расширениями класса <see cref="Type"/>
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Проверяет является ли тип <see cref="Nullable"/>
        /// </summary>
        /// <param name="type">Проверямый тип</param>
        /// <returns>Значение если тип является <see cref="Nullable"/></returns>
        public static bool IsNullable(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}
