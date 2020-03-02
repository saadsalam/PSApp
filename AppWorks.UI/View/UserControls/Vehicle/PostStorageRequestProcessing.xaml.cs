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
    /// Interaction logic for PostStorageRequestProcessing.xaml
    /// </summary>
    public partial class PostStorageRequestProcessing : UserControl
    {
        public PostStorageRequestProcessing()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                InitializeComponent();
                Loaded += FocusTextBox_Loaded;
                this.DataContext = new PostStorageRequestProcessingVM();
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

                txtVIN.Focus();
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
        private void txtRequestDate_KeyDown(object sender, KeyEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (e.Key == Key.Enter || e.Key==Key.Tab)
                {
                    string value = txtRequestDate.Text;
                    int index = txtRequestDate.CaretIndex;
                    if (!string.IsNullOrEmpty(value) && index > 0)
                    {
                        string GetDate = CommonSettings.GetDateValue(value, index);
                        txtRequestDate.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy hh:mm tt");
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

        private void txtVIN_KeyUp(object sender, KeyEventArgs e)
        {
            int countDecodeVIN = txtVIN.MaxLength - (txtVIN.CaretIndex);
            if (countDecodeVIN == 0)
            {
                string Vin = txtVIN.Text;
                //DelegateEventVehicle.SetValueMethodGetVin(Vin);
            }
        }

        private void txtRequestDate_LostFocus(object sender, RoutedEventArgs e)
        {
            string value = txtRequestDate.Text;
            int index = txtRequestDate.CaretIndex;
            if (!string.IsNullOrEmpty(value) && index > 0)
            {
                string GetDate = CommonSettings.GetDateValue(value, index);
                txtRequestDate.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy hh:mm tt");
            }
        }

        private void txtVIN_TextChanged(object sender, TextChangedEventArgs e)
        {
          var vin = txtVIN.Text;
            if (!string.IsNullOrEmpty(vin) && vin.Length == 17)
            {
                DelegateEventDateAndVehicleProcessing.SetValueMethod("ScanVin");
            }
        }


        private void grdUserList_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            if (grdUserList.Items.Count > 0)
            {
                grdUserList.ScrollIntoViewAsync(grdUserList.Items[grdUserList.Items.Count - 1], //the row
                                   grdUserList.Columns[grdUserList.Columns.Count - 1], //the column
                                   null);
            }
        }
    }
}
