using AppWorks.UI.ViewModel.PortStorageInvoices;
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

namespace AppWorks.UI.View.PortStorageInvoices
{
    /// <summary>
    /// Interaction logic for PrintInvoiceReportWindow.xaml
    /// </summary>
    public partial class PrintInvoiceReportWindow : Window
    {
        private PrintInvoiceVM objPrintInvoiceVM = new PrintInvoiceVM();
        public PrintInvoiceReportWindow(Telerik.Reporting.ReportSource objReport)
        {
            InitializeComponent();
            objPrintInvoiceVM.MyReportSource = objReport;
            this.DataContext = objPrintInvoiceVM;
        }
    }
}
