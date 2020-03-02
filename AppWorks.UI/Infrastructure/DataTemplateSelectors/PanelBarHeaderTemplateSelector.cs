using AppWorks.UI.Infrastructure.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace AppWorks.UI.Infrastructure.DataTemplateSelectors
{
    public class PanelBarHeaderTemplateSelector : DataTemplateSelector
    {
        public DataTemplate WhiteHeaderTemplate { get; set; }
        public DataTemplate OrangeHeaderTemplate { get; set; }
        public DataTemplate ListBoxTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var value = (NavigationNode)item;
            if (value != null && value.Name == "Dashboard")
            {
                return WhiteHeaderTemplate;
            }

            if (value != null)
            {
                return ListBoxTemplate;
            }
            return OrangeHeaderTemplate;
        }
    }
}
