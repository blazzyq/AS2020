using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using DronTaxi.Properties;
using DronTaxi.Static;

namespace DronTaxi.Converters
{
    public class ByteArrayToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] array)
            {
                return array.ToBitmapImage();
            }

            using (var stream = new MemoryStream())
            {
                Resources.clear_prof.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray().ToBitmapImage();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
