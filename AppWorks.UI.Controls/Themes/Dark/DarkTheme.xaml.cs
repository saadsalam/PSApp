using System.Windows;
using Telerik.Windows.Controls;

namespace AppWorks.UI.Controls.Themes.Dark
{
    public partial class DarkTheme : ResourceDictionary
    {
        internal DarkTheme()
        {
            InitializeComponent();
            StyleManager.ApplicationTheme = new Expression_DarkTheme();
        }
    }
}
