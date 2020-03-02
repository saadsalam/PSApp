using System.Windows;
using Telerik.Windows.Controls;

namespace AppWorks.UI.Controls.Themes.Light
{
    public partial class LightTheme : ResourceDictionary
    {
        internal LightTheme()
        {
            InitializeComponent();
            StyleManager.ApplicationTheme = new Windows8Theme();
        }
    }
}
