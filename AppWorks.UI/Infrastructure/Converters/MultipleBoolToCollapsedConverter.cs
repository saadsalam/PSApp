using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Linq;

namespace AppWorks.UI.Infrastructure.Converters
{
    class MultipleBoolToCollapsedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isInverse = parameter is bool && (bool)parameter;
            bool isVisible = values.OfType<bool>().Any(value => value); //(bool)values[0] || (bool)values[1];

            if (isVisible)
            {
                return !isInverse ? Visibility.Visible : Visibility.Collapsed;
            }
            return !isInverse ? Visibility.Collapsed : Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
