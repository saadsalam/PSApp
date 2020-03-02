using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace AppWorks.UI.View
{
    /// <summary>
    /// Interaction logic for SystemSettings.xaml
    /// </summary>
    public partial class SystemSettings : Window
    {
        public SystemSettings()
        {
            InitializeComponent();
            this.DataContext = new AppWorks.UI.ViewModel.SystemSettingsVM();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
