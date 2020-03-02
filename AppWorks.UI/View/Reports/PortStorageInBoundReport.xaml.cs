using AppWorks.UI.ViewModel.Report;
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

namespace AppWorks.UI.View.Reports
{
    /// <summary>
    /// Interaction logic for PortStorageInBoundReport.xaml
    /// </summary>
    public partial class PortStorageInBoundReport : UserControl
    {       
        public PortStorageInBoundReport()
        {
            InitializeComponent();
            this.DataContext = new PortStorageInBoundReportVM();
        }
    }
}
