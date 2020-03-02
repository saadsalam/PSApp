using AppWorks.UI.Infrastructure.Navigation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace AppWorks.UI.Infrastructure.Converters
{
    public class OrganizeNodeConverter : IValueConverter
    {
        public OrganizeNodeConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var node = value as NavigationNode;

            if (node != null && node.Children.Count != 0)
            {
                return new List<NavigationNode> {node};
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}