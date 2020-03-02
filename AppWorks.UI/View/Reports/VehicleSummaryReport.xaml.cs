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
using Appworks.Reports;
using Telerik.Reporting;

namespace AppWorks.UI.View.Reports
{
    /// <summary>
    /// Interaction logic for VehicleSummaryReport.xaml
    /// </summary>
    public partial class VehicleSummaryReport : UserControl
    {
        private VehicleSummaryReportVM objVehicleSummaryReportVM = new VehicleSummaryReportVM();
		private	InstanceReportSource _reportSource = null;

        public VehicleSummaryReport()
        {
            InitializeComponent();
            this.DataContext = objVehicleSummaryReportVM;
			objVehicleSummaryReportVM.RefreshReportRequested += ObjVehicleSummaryReportVM_RefreshReportRequested;
        }

		private void ObjVehicleSummaryReportVM_RefreshReportRequested(object sender, EventArgs e)
		{
			if (_reportSource == null)
			{
				VehicleSummaryRPT reportDocument = new VehicleSummaryRPT();
				_reportSource = new InstanceReportSource();
				_reportSource.ReportDocument = reportDocument;
				reportDocument.DataSource = new ObjectDataSource(objVehicleSummaryReportVM, "LoadData");
				ReportViewerVehicleSummary.ReportSource = _reportSource;
			}
			ReportViewerVehicleSummary.RefreshReport();
		}
	}
}
