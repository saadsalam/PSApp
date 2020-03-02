using System.Windows;
using System.Windows.Controls;

namespace AppWorks.UI.Controls.Converters
{
    class RadGridViewHeaderContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StringTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is string && StringTemplate != null)
            {
                return StringTemplate;
            }
            else if (item is TextBlock)
            {
                TextBlock block = item as TextBlock;
                block.Text = block.Text.ToUpper();
                block.FontWeight = FontWeights.SemiBold;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
