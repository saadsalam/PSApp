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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppWorks.UI.View.UserControls
{
    /// <summary>
    /// Interaction logic for PortStorageRateAdmin.xaml
    /// </summary>
    public partial class PortStorageRateAdmin : UserControl
    {
        public PortStorageRateAdmin()
        {
            this.DataContext = new ViewModel.CustomerAdmin.PortStorageRateAdminVM();
            InitializeComponent();
            StartDate.Culture.DateTimeFormat.SetAllDateTimePatterns(new string[] { "M/d/yyyy" }, 'd');
            EndDate.Culture.DateTimeFormat.SetAllDateTimePatterns(new string[] { "M/d/yyyy" }, 'd');
        }     
    }
}
