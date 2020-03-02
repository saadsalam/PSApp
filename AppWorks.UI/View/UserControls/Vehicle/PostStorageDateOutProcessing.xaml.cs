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
using AppWorks.UI.ViewModel.Vehicle;
using Props = AppWorks.UI.Properties;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;

namespace AppWorks.UI.View.UserControls.Vehicle
{
    /// <summary>
    /// Interaction logic for PostStorageDateOutProcessing.xaml
    /// </summary>
    public partial class PostStorageDateOutProcessing : UserControl
    {
        public PostStorageDateOutProcessing()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                InitializeComponent();
                Loaded += FocusTextBox_Loaded;
                this.DataContext = new PostStorageDateOutProcessingVM();
              
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

        /// <summary>
        /// This KeyDown event auto insert currunt date when press enter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatededBy></CreatededBy>
        /// <CreatedDate>06 July 2016</CreatedDate>
        private void txtDateOut_KeyDown(object sender, KeyEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                if (e.Key == Key.Enter || e.Key==Key.Tab)
                {
                    string value = txtDateOut.Text;
                    int index = txtDateOut.CaretIndex;
                    if (!string.IsNullOrEmpty(value) && index > 0)
                    {
                        string GetDate = CommonSettings.GetDateValue(value, index);
                        txtDateOut.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy hh:mm tt");
                    }
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

        private void txtDateOut_LostFocus(object sender, RoutedEventArgs e)
        {
            string value = txtDateOut.Text;
            int index = txtDateOut.CaretIndex;
            if (!string.IsNullOrEmpty(value) && index > 0)
            {
                string GetDate = CommonSettings.GetDateValue(value, index);
                txtDateOut.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy hh:mm tt");
            }
        }        

        private void txtVin_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vin = txtVin.Text;
            if (!string.IsNullOrEmpty(vin) && vin.Length >= 8)
            {
                DelegateEventDateAndVehicleProcessing.SetValueMethod("ScanVin");
            }
        }

        private void grdUserList_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            var scrolled = string.IsNullOrEmpty(chkScrolled.Text) || chkScrolled.Text.Equals("NOTSCROLLED");
            if (grdUserList.Items.Count > 0 && scrolled)
            {
                grdUserList.ScrollIntoViewAsync(grdUserList.Items[grdUserList.Items.Count - 1], //the row
                                   grdUserList.Columns[grdUserList.Columns.Count - 1], //the column
                                   null);
                chkScrolled.Text = "SCROLLED";
            }
        }
    }
}
