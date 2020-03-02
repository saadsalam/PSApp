using AppWorks.UI.Common;
using AppWorks.UI.ViewModel.PortStorageImportExport;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
using Props = AppWorks.UI.Properties;

namespace AppWorks.UI.View.PortStorageImportExport
{
    /// <summary>
    /// Interaction logic for PortStorageVehicleImportYMSLoadBatchWindow.xaml
    /// </summary>
    public partial class PortStorageVehicleImportYMSLoadBatchWindow : Window
    {
        public PortStorageVehicleImportYMSLoadBatchWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This keyUp event filter data press enter button and tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatededBy></CreatededBy>
        /// <CreatedDate>21 July 2016</CreatedDate>
        private void txtVIN_KeyUp(object sender, KeyEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (e.Key == Key.Enter)
                {

                    string Vin = txtVIN.Text;
                    DelegateEventVehicleImport.SetFilterVinValueMethod(Vin);
                }
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

        private void txtFullVIN_LostFocus(object sender, RoutedEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                string Vin = txtVIN.Text;
                DelegateEventVehicleImport.SetFilterVinValueMethod(Vin);
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
    }
}
