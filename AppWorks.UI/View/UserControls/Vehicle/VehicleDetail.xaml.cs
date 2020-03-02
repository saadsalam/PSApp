using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppWorks.UI.ViewModel.Vehicle;
using Props = AppWorks.UI.Properties;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;


namespace AppWorks.UI.View.UserControls.Vehicle
{
    /// <summary>
    /// Interaction logic for VehicleDetail.xaml
    /// </summary>
    public partial class VehicleDetail : UserControl
    {
        public VehicleDetail()
        {
            InitializeComponent();
            DelegateEventVehicle.OnSetValueEvtFocusNext += new DelegateEventVehicle.SetValueFocusNext(GetNextFocus);
        }

        public void FocusListOfItemsIfExist()
        {
            if (selectedVehiclesList == null)
                return;
            if (selectedVehiclesList.Items != null && selectedVehiclesList.Items.Count > 1)
            {
                var element = selectedVehiclesList.ItemContainerGenerator.ContainerFromIndex(selectedVehiclesList.SelectedIndex) as UIElement;
                if (element != null)
                {
                    element.Focus();
                }
            }
        }
        private void GetNextFocus(bool value)
        {
            if (value)
                txtBayLocation.Focus();
            else
                txtVIN.Focus();

        }

        private void txtVIN_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != Key.Tab && e.Key != Key.Escape)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
            {
                int countDecodeVIN = txtVIN.MaxLength - (txtVIN.CaretIndex);
                if (countDecodeVIN == 0)
                {
                    DelegateEventVehicle.SetValueMethodVINDecode("Decode");
                }
            }           
        }

        /// <summary>
        /// This KeyDown event auto insert currunt date when press enter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatededBy></CreatededBy>
        /// <CreatedDate>06 July 2016</CreatedDate>
        private void txtDateRequested_KeyDown(object sender, KeyEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    string value = txtDateRequested.Text;
                    int index = txtDateRequested.CaretIndex;
                    if (!string.IsNullOrEmpty(value) && index > 0)
                    {
                        string GetDate = CommonSettings.GetDateValue(value, index);
                        txtDateRequested.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy hh:mm tt");
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

        /// <summary>
        /// This KeyDown event auto insert currunt date when press enter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatededBy></CreatededBy>
        /// <CreatedDate>06 July 2016</CreatedDate>
        private void txtDateIn_KeyDown(object sender, KeyEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    string value = txtDateIn.Text;
                    int index = txtDateIn.CaretIndex;
                    if (!string.IsNullOrEmpty(value) && index > 0)
                    {
                        string GetDate = CommonSettings.GetDateValue(value, index);
                        txtDateIn.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy hh:mm tt");
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

        /// <summary>
        /// This KeyDown event auto insert currunt date when press enter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatededBy></CreatededBy>
        /// <CreatedDate>06 July 2016</CreatedDate>
        //private void txtPhycalDate_KeyDown(object sender, KeyEventArgs e)
        //{
        //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {

        //        if (e.Key == Key.Enter || e.Key== Key.Tab)
        //        {
        //            string value = txtPhycalDate.Text;
        //            int index = txtPhycalDate.CaretIndex;
        //            if (!string.IsNullOrEmpty(value) && index > 0)
        //            {
        //                string GetDate = CommonSettings.GetDateValue(value, index);
        //                txtPhycalDate.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy hh:mm tt");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        bool displayErrorOnUI = false;
        //        CommonSettings.logger.LogError(this.GetType(), ex);
        //        if (displayErrorOnUI)
        //        { throw; }
        //    }
        //    finally
        //    {
        //        CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }

        //}

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

                if (e.Key == Key.Enter || e.Key == Key.Tab)
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

        /// <summary>
        /// This KeyDown event auto insert currunt date when press enter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatededBy></CreatededBy>
        /// <CreatedDate>06 July 2016</CreatedDate>
        //private void txtPickUpDate_KeyDown(object sender, KeyEventArgs e)
        //{
        //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        if (e.Key == Key.Enter || e.Key==Key.Tab)
        //        {
        //            string value = txtPickUpDate.Text;
        //            int index = txtPickUpDate.CaretIndex;
        //            if (!string.IsNullOrEmpty(value) && index > 0)
        //            {
        //                string GetDate = CommonSettings.GetDateValue(value, index);
        //                txtPickUpDate.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy hh:mm tt");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        bool displayErrorOnUI = false;
        //        CommonSettings.logger.LogError(this.GetType(), ex);
        //        if (displayErrorOnUI)
        //        { throw; }
        //    }
        //    finally
        //    {
        //        CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }

        //}

        /// <summary>
        /// This KeyDown event auto insert currunt date when press enter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatededBy></CreatededBy>
        /// <CreatedDate>06 July 2016</CreatedDate>
        //private void txtDealerPrintDate_KeyDown(object sender, KeyEventArgs e)
        //{
        //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        if (e.Key == Key.Enter || e.Key==Key.Tab)
        //        {
        //            string value = txtDealerPrintDate.Text;
        //            int index = txtDealerPrintDate.CaretIndex;
        //            if (!string.IsNullOrEmpty(value) && index > 0)
        //            {
        //                string GetDate = CommonSettings.GetDateValue(value, index);
        //                txtDealerPrintDate.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy hh:mm tt");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        bool displayErrorOnUI = false;
        //        CommonSettings.logger.LogError(this.GetType(), ex);
        //        if (displayErrorOnUI)
        //        { throw; }
        //    }
        //    finally
        //    {
        //        CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }

        //}

    }
}
