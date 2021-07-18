using System;
using System.Linq;

namespace Calculator
{
    /// <summary>
    /// Класс хелпер для <see cref="BitConverter"/>
    /// </summary>
    public static class BitConverterHelper
    {
        /// <summary>
        /// Возвращает является ли кодировка LittleEndian
        /// </summary>
        public static bool IsLittleEndian { get => GetCorrectedEnconding(); }

        /// <summary>
        /// Флаг показывающий проводилась ли проверка кодировки
        /// </summary>
        private static bool _isChecked;

        /// <summary>
        /// Хранит в себе оприделение является ли кодировка LittleEndian
        /// </summary>
        private static bool _isLittleEndian;

        /// <summary>
        /// Конвертирует массив в BigEndian кодировку, если системная кодировка LittleEndian
        /// </summary>
        /// <remarcs>Этот метод актуален, если в массиве находится только одна переменная</remarcs>
        /// <param name="arr">Массив с одной переменной</param>
        /// <returns>Массив в BigEndian кодировке</returns>
        /// <exception cref="ArgumentNullException">Если массив является null</exception>
        public static byte[] ConvertArrayToBigEndian(this byte[] arr)
        {
            if (arr == null)
                throw new ArgumentNullException(nameof(arr));
            return IsLittleEndian ? arr.Reverse().ToArray() : arr;
        }

        /// <summary>
        /// Определяет тип кодировки Endian системы
        /// </summary>
        /// <returns>Является ли кодировка LittleEndian</returns>
        private static bool GetCorrectedEnconding()
        {
            if (!_isChecked)
            {
                _isLittleEndian = BitConverter.GetBytes(16777216)[0] == 0;
                _isChecked = true;
            }
            return _isLittleEndian;
        }
    }
}
