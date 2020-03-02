﻿using AppWorks.UI.ViewModel.PortStorageInvoices;
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
    /// Interaction logic for PrintInvoiceErrorReportWindow.xaml
    /// </summary>
    public partial class PrintInvoiceErrorReportWindow : Window
    {
        private GeneratePortStorageInvoicesVM objGeneratePortStorageInvoicesVM = new GeneratePortStorageInvoicesVM();
        public PrintInvoiceErrorReportWindow(Telerik.Reporting.ReportSource objReport)
        {
            InitializeComponent();
            objGeneratePortStorageInvoicesVM.MyReportSource = objReport;
            this.DataContext = objGeneratePortStorageInvoicesVM;
        }
    }
}
