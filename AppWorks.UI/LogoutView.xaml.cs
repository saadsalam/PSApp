using System.Windows;
using System.Windows.Controls;
using AppWorks.UI.ViewModel;
using AppWorks.UI;

namespace AppWorks.UI
{
    /// <summary>
    /// Interaction logic for LogoutView.xaml
    /// </summary>
    public partial class LogoutView : UserControl
    {
        public LogoutView()
        {
            InitializeComponent();
            this.DataContext=new LogoutViewModel();
        }
    }
}
