using System;
using System.Globalization;
using System.Windows.Data;

namespace AppWorks.UI.Infrastructure.Converters
{
    public class PanelBarNodeHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string header = (string)value;
            return string.IsNullOrEmpty(header) ? header : header.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}