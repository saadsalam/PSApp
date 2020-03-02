
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
    /// Interaction logic for PrintRequestReportWindow.xaml
    /// </summary>
    public partial class PrintRequestReportWindow : Window
    {
        private PrintRequestReportVM objPrintRequestReportVM = new PrintRequestReportVM();
        public PrintRequestReportWindow(Telerik.Reporting.ReportSource objReport)
        {
            InitializeComponent();
            objPrintRequestReportVM.MyReportSource = objReport;
            this.DataContext = objPrintRequestReportVM;
        }
    }
}
