using AppWorks.UI.ViewModel.PortStorageInvoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppWorks.UI.View.PortStorageInvoices
{
    /// <summary>
    /// Interaction logic for GeneratePortStorageInvoices.xaml
    /// </summary>
    public partial class GeneratePortStorageInvoices : UserControl
    {
        public GeneratePortStorageInvoices()
        {
            InitializeComponent();
        }

        

        private void txtCutOffDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = "";
            DelegateEventPortStorageInvoices.SetValueMethodCutOffDate(value);
        }

    }
}
