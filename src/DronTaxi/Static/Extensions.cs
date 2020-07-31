using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Media.Imaging;

namespace DronTaxi.Static
{
    public static class Extensions
    {
        /// <summary>
        /// Преобразует переданный объект в объект указанного типа. Объект должен поддерживать <see cref="IConvertible"/>.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        /// <typeparam name="T">Тип преобразования.</typeparam>
        public static T ConvertTo<T>(this object value) where T : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Преобразует переданный объект в объект указанного типа. Объект должен поддерживать <see cref="IConvertible"/>.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        /// <param name="provider">Объект содержащий информацию о форматировании.</param>
        /// <typeparam name="T">Тип преобразования.</typeparam>
        public static T ConvertTo<T>(this object value, IFormatProvider provider) where T : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T), provider);
        }

        /// <summary>
        /// Преобразует переданный объект в объект указанного типа или возвращает значение по умолчанию если преобразование невозможно. Объект должен поддерживать <see cref="IConvertible"/>.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        /// <typeparam name="T">Тип преобразования.</typeparam>
        public static T SafeConvertTo<T>(this object value) where T : IConvertible
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }

            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Преобразует переданный объект в объект указанного типа или возвращает значение по умолчанию если преобразование невозможно. Объект должен поддерживать <see cref="IConvertible"/>.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        /// <param name="provider">Объект содержащий информацию о форматировании.</param>
        /// <typeparam name="T">Тип преобразования.</typeparam>
        public static T SafeConvertTo<T>(this object value, IFormatProvider provider) where T : IConvertible
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T), provider);
            }

            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Преобразует переданный объект в объект указанного типа или возвращает значение по умолчанию если преобразование невозможно. Объект должен поддерживать <see cref="IConvertible"/>.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        /// <param name="defaultValue">Возвращаемое значение "по умолчанию".</param>
        /// <typeparam name="T">Тип преобразования.</typeparam>
        public static T SafeConvertTo<T>(this object value, T defaultValue) where T : IConvertible, IEquatable<T>
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }

            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Преобразует переданный объект в объект указанного типа или возвращает значение по умолчанию если преобразование невозможно. Объект должен поддерживать <see cref="IConvertible"/>.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        /// <param name="defaultValue">Возвращаемое значение "по умолчанию".</param>
        /// <param name="provider">Объект содержащий информацию о форматировании.</param>
        /// <typeparam name="T">Тип преобразования.</typeparam>
        public static T SafeConvertTo<T>(this object value, T defaultValue, IFormatProvider provider) where T : IConvertible, IEquatable<T>
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T), provider);
            }

            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Преобразует переданный объект в nullable-объект указанного типа. В случае неудачи возвращает null. Объект должен поддерживать <see cref="IConvertible"/>.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        /// <typeparam name="T">Тип преобразования.</typeparam>
        public static T? ConvertToNullable<T>(this object value) where T : struct, IConvertible
        {
            try
            {
                return (T?)Convert.ChangeType(value, typeof(T));
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Преобразует переданный объект в nullable-объект указанного типа. В случае неудачи возвращает null. Объект должен поддерживать <see cref="IConvertible"/>.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        /// <param name="provider">Объект содержащий информацию о форматировании.</param>
        /// <typeparam name="T">Тип преобразования.</typeparam>
        public static T? ConvertToNullable<T>(this object value, IFormatProvider provider) where T : struct, IConvertible
        {
            try
            {
                return (T?)Convert.ChangeType(value, typeof(T), provider);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Пытается явно привести объект к указанному типу.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        /// <typeparam name="T">Тип приведения.</typeparam>
        public static T CastTo<T>(this object value)
        {
            return (T)value;
        }

        /// <summary>
        /// Приводит объект к указанному типу или возвращает значение по умолчанию.
        /// </summary>
        /// <typeparam name="T">Тип приведения.</typeparam>
        /// <param name="value">Объект для преобразования.</param>
        public static T SafeCastTo<T>(this object value)
        {
            try
            {
                return (T)value;
            }

            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Преобразует объект в <see cref="Nullable"/>.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        /// <typeparam name="T">Тип преобразования.</typeparam>
        public static T? AsNullable<T>(this object value) where T : struct
        {
            if (value == DBNull.Value)
                return default;

            return (T?)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Преобразует объект в строку.
        /// </summary>
        /// <param name="value">Объект.</param>
        public static string AsString(this object value)
        {
            if (value == DBNull.Value)
                return null;

            return (string)value;
        }

        /// <summary>
        /// Преобразует объект в глобальный идентификатор.
        /// </summary>
        /// <param name="value">Объект.</param>
        public static Guid AsGuid(this object value)
        {
            if (value == DBNull.Value)
                return Guid.Empty;

            return !string.IsNullOrWhiteSpace(value.ToString()) ? Guid.Parse(value.ToString()) : Guid.Empty;
        }

        /// <summary>
        /// Преобразует объект в массив байт.
        /// </summary>
        /// <param name="value">Объект для преобразования.</param>
        public static byte[] AsByteArray(this object value)
        {
            if (value == DBNull.Value)
                return null;

            return (byte[])value;
        }

        /// <summary>
        /// Преобразует массив байт в изображение.
        /// </summary>
        /// <param name="imageData">Массив байт.</param>
        public static BitmapImage ToBitmapImage(this byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            BitmapImage image = new BitmapImage();

            using (MemoryStream mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }

            image.Freeze();

            return image;
        }

        /// <summary>
        /// Добавляет новый параметр и вставляет корректный null, если оный был передан в качестве значения.
        /// </summary>
        /// <param name="collection">Коллекция параметров.</param>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="value">Значение параметра.</param>
        public static SqlParameter AddWithNullCheck(this SqlParameterCollection collection, string parameterName,
            object value)
        {
            return collection.AddWithValue(parameterName, value ?? DBNull.Value);
        }

        /// <summary>
        /// Добавляет новый параметр или обновляет существующий и вставляет корректный null, если оный был передан в качестве значения.
        /// </summary>
        /// <param name="collection">Коллекция параметров.</param>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="value">Значение параметра.</param>
        public static SqlParameter AddOrUpdateWithNullCheck(this SqlParameterCollection collection, string parameterName,
            object value)
        {
            if (collection.Contains(parameterName))
            {
                collection[parameterName].Value = value;
                return collection[parameterName];
            }

            return collection.AddWithValue(parameterName, value ?? DBNull.Value);
        }
    }
}
