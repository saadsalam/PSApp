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
using AppWorks.UI.ViewModel.Location;
using Props = AppWorks.UI.Properties;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;


namespace AppWorks.UI.View.UserControls.Location
{
    /// <summary>
    /// Interaction logic for AddLocation.xaml
    /// </summary>
    public partial class AddLocation : Window
    {
        public AddLocation()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This is key down event for input onlu numeric value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKeyEvent(sender, e);
        }
        /// <summary>
        /// This is key down event for input onlu numeric value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtExtention_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKeyEvent(sender, e);
        }
        /// <summary>
        /// This is key down event for input onlu numeric value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCellPhone_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKeyEvent(sender, e);
        }
        /// <summary>
        /// This is key down event for input onlu numeric value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFaxNumber_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKeyEvent(sender, e);
        }

        /// <summary>
        /// Key Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleKeyEvent(object sender, KeyEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && e.Key != Key.Tab)
                {
                    e.Handled = true;
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

    }
}
