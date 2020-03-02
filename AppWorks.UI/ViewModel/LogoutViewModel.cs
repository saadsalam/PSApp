using System;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using AppWorks.UI.Common;
using AppWorks.UI;
using AppWorks.UI.View.UserControls;
using AppWorks.UI.Properties;
using System.Reflection;
using AppWorks.UI.ViewModel.Vehicle;

namespace AppWorks.UI.ViewModel
{
    /// <summary>
    /// Class To Perform the functionality of Logout ViewModel
    /// </summary>
    public class LogoutViewModel : ViewModelBase
    {
        #region "Page Events"
        private ICommand _btnSubmit_Click;

        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand btnSubmit_Click
        {
            get
            {
                if (_btnSubmit_Click == null)
                {
                    _btnSubmit_Click = new AppWorxCommand(
                        param => this.ShowLogin(),
                        param => CanCheck
                        );
                }
                return _btnSubmit_Click;
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
        /// Function to Jump on Login Page.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        private void ShowLogin()
        {           
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                MainWindow objMainWindow = new MainWindow();
                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.Title.ToLower(CultureInfo.CurrentCulture).Equals("logout"))
                    {
                        window.Close();
                    }
                }
                objMainWindow.ShowDialog();
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
