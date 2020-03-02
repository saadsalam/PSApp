using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppWorks.UI.Common;
using AppWorks.UI.View.UserControls;
using AppWorks.UI.Properties;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Text.RegularExpressions;
using AppWorks.UI.ViewModel.Vehicle;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class MultiVinVM : ViewModelBase
    {

        public MultiVinVM(string VINNo)
        {
            VIN = VINNo;
        }
        private string vin;
        public string VIN
        {
            get { return vin; }
            set
            {
                if (value != null)
                {
                    this.vin = value;
                    NotifyPropertyChanged("VIN");
                }
            }
        }

        #region "Page Events"
        //private ICommand _btnSubmit_Click;

        /// <summary>
        /// Submit button event
        /// </summary>
        private AppWorxCommand saveClick;
        public AppWorxCommand SaveClick
        {
            get
            {
                if (saveClick == null)
                {

                    saveClick = new AppWorxCommand(this.ContinueVinClick);
                }
                return saveClick;
            }
        }

        /// <summary>
        /// Submit button event
        /// </summary>
        private AppWorxCommand clearClick;
        public AppWorxCommand ClearClick
        {
            get
            {
                if (clearClick == null)
                {

                    clearClick = new AppWorxCommand(this.ClearBox);
                }
                return clearClick;
            }
        }

        /// <summary>
        /// sucessor method
        /// </summary>
        /// <param name="data"></param>
        public void ContinueVinClick(object data)
        {
            try
            {
                if (vin != null)
                {
                    DelegateEventVehicle.SetValueMethod(VIN.Replace(" ", ""));

                    foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                    {
                        if (window.DataContext == this)
                        {
                            window.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please! Enter the VIN into Listbox or Click the Cancel Button.");
                }

            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
        }

        /// <summary>
        /// method to clear the textbo value
        /// </summary>
        /// <param name="data"></param>
        public void ClearBox(object data)
        {
            try
            {
                this.VIN = string.Empty;
                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.DataContext == this)
                    {
                        window.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
        }

        public static bool CanCheck
        {
            get
            {
                return true;
            }
        }
        #endregion



        /// <summary>
        /// Function to check the Login Credentials.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        private void SetVIN()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                // creating the object of service property
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
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

       
    }
}
