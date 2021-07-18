using System;

namespace CalculatorTests
{
    /// <summary>
    /// Класс реализующий конвертацию числа в hex с использованием <see cref="BitConverter"/>
    /// </summary>
    public class BitConverterHexCalculator : IHexCalculator
    {
        /// <summary>
        /// Конвертацая в шестнадцетеричное значение
        /// </summary>
        /// <param name="x">Число</param>
        /// <returns>Шестнадцетеричное значение</returns>
        public string ToHex(int x)
        {
            var bytes = BitConverter.GetBytes(x).ConvertArrayToBigEndian();
            return BitConverter.ToString(RemoveZeros(bytes)).Replace("-", " ");
        }

        /// <summary>
        /// Удаляет лишние нули по смещению два байта
        /// </summary>
        /// <param name="arr">Не откорректированный массив</param>
        /// <returns>Откорректированный массив</returns>
        static private byte[] RemoveZeros(byte[] arr)
        {
            int offset = 0;

            for (; offset < arr.Length;offset++)
                if (arr[offset] != 0x0) 
                    break;

            offset -= offset == arr.Length ? 2 : 0;
            offset -= offset % 2;

            byte[] result = new byte[arr.Length - offset];
            Array.Copy(arr, offset, result, 0, arr.Length - offset);

            return result;
        }
    }
}
