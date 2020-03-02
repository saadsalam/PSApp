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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Props = AppWorks.UI.Properties;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;

namespace AppWorks.UI.View.UserControls.Vehicle
{
    /// <summary>
    /// Interaction logic for StorageVehicleOutgate.xaml
    /// </summary>
    public partial class StorageVehicleOutgate : UserControl
    {
        public StorageVehicleOutgate()
        {
            InitializeComponent();
            Loaded += FocusTextBox_Loaded;
            DataContext = new StorageVehicleOutgateViewModel();
        }
        private void FocusTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                txtVin.Focus();
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtVin.Focus();
        }

    }
}
