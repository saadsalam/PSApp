using AppWorks.UI.Infrastructure.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace AppWorks.UI.Infrastructure.DataTemplateSelectors
{
    public class PanelBarItemTemplateSelector : StyleSelector
    {
        public Style HierarchicalStyle { get; set; }
        public Style NoChildrenStyle { get; set; }

        public PanelBarItemTemplateSelector()
        {

        }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var value = (NavigationNode)item;

            if (value.Children.Count != 0)
            {
                return HierarchicalStyle;
            }
            return NoChildrenStyle;
        }     
    }
}
