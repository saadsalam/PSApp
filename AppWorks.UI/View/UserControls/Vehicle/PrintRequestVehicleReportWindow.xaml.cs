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

namespace AppWorks.UI.View.UserControls.Vehicle
{
    /// <summary>
    /// Interaction logic for PrintRequestVehicleReportWindow.xaml
    /// </summary>
    public partial class PrintRequestVehicleReportWindow : Window
    {
        private VehicleDetailsVM objVehicleDetailsVM = new VehicleDetailsVM();
        public PrintRequestVehicleReportWindow(Telerik.Reporting.ReportSource objReport)
        {
            InitializeComponent();
            objVehicleDetailsVM.MyReportSource = objReport;
            this.DataContext = objVehicleDetailsVM;
        }
    }
}
