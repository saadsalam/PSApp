using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AppWorks.UI.Infrastructure.Converters
{
    public class BooleanToHidenVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isInverse = parameter is bool && (bool)parameter;
            bool isVisible = (bool)value;

            if (isVisible)
            {
                return !isInverse ? Visibility.Visible : Visibility.Hidden;
            }
            return !isInverse ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
