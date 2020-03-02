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

namespace AppWorks.UI.View.UserControls.CustomerAdmin
{
    /// <summary>
    /// Interaction logic for CustomerNotes.xaml
    /// </summary>
    public partial class CustomerNotes : Window
    {
        public CustomerNotes()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.CustomerAdmin.CustomerAdminVM();
        }
    }
}
