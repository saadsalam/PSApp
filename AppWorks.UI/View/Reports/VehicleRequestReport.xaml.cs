using AppWorks.UI.ViewModel.Report;
using AppWorks.UI.ViewModel.Vehicle;
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
    /// Interaction logic for VehicleRequestReport.xaml
    /// </summary>
    public partial class VehicleRequestReport : UserControl
    {
        private VehicleRequestReportVM objVehicleRequestReportVM = new VehicleRequestReportVM();
        public VehicleRequestReport()
        {
            InitializeComponent();
            this.DataContext = objVehicleRequestReportVM;
        }


        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            var sortColumn = headerClicked.Tag as string;
            DelegateEventRequestProcessing.SetOnSortedMethod(sortColumn);
        }
        
    }
}
