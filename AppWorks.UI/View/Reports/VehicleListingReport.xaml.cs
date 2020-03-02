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
using AppWorks.UI.ViewModel.Report;

namespace AppWorks.UI.View.Reports
{
    /// <summary>
    /// Interaction logic for VehicleListingReport.xaml
    /// </summary>
    public partial class VehicleListingReport : UserControl
    {
        private VehicleListingReportVM objVehicleListingReportVM = new VehicleListingReportVM();
        public VehicleListingReport()
        {
            InitializeComponent();
            this.DataContext = objVehicleListingReportVM;
        }

        
    }
}
