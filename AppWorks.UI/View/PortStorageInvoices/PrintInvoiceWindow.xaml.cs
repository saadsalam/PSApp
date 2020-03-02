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
using AppWorks.UI.ViewModel.PortStorageInvoices;
using Props = AppWorks.UI.Properties;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;
namespace AppWorks.UI.View.PortStorageInvoices
{
    /// <summary>
    /// Interaction logic for PrintInvoiceWindow.xaml
    /// </summary>
    public partial class PrintInvoiceWindow : Window
    {

        public PrintInvoiceWindow()
        {
            InitializeComponent();
            DataContext = new PrintInvoiceVM();
        }

    }
}
