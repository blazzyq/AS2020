using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronTaxi.Static
{
    /// <summary>
    /// Для работы с массивами байт.
    /// </summary>
    public static class ByteArrayHelper
    {
        /// <summary>
        /// Копирует содержимое указанных массивов в один.
        /// </summary>
        /// <param name="arrays">Массивы.</param>
        public static byte[] ConcatArrays(params byte[][] arrays)
        {
            byte[] result = new byte[arrays.Sum(a => a.Length)];

            int offset = 0;

            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, result, offset, array.Length);

                offset += array.Length;
            }

            return result;
        }
    }
}
