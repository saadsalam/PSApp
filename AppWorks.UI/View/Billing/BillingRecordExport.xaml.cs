using System.Windows;
using AppWorks.UI.ViewModel.Billing;

namespace AppWorks.UI.View.Billing
{
    /// <summary>
    /// Interaction logic for BillingRecordExport.xaml
    /// </summary>
    public partial class BillingRecordExport : Window
    {
        public BillingRecordExport()
        {
            InitializeComponent();
            this.DataContext = new BillingExportVM();
            this.Owner = Application.Current.MainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
