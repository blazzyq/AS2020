using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Data;
using DronTaxi.Models;
using DronTaxi.Views;

namespace DronTaxi.Converters
{
    public class UserToUserViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is User user)
            {
                return UserView.FromUser(user);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
