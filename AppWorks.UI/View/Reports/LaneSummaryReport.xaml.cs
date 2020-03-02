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
    /// Interaction logic for LaneSummaryReport.xaml
    /// </summary>
    public partial class LaneSummaryReport : UserControl
    {
        private LaneSummaryReportVM objLaneSummaryReportVM = new LaneSummaryReportVM();
        public LaneSummaryReport()
        {
            InitializeComponent();
            this.DataContext = objLaneSummaryReportVM;
        }
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var typeReportSource = objLaneSummaryReportVM.Report;
        //    //typeReportSource.ty = "Appworks.Reports.Report3, AppWorks.Reports";

        //    this.ReportViewerLaneSummary.ReportSource = typeReportSource;
        //}
    }
}
