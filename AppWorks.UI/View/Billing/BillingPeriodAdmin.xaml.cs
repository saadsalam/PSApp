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
using AppWorks.UI.ViewModel.Billing;

namespace AppWorks.UI.View.Billing
{
    /// <summary>
    /// Interaction logic for BillingPeriodAdmin.xaml
    /// </summary>
    public partial class BillingPeriodAdmin : UserControl
    {
        public BillingPeriodAdmin()
        {
            InitializeComponent();
            DataContext = new BillingPeriodAdminVM();
        }
        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab)
            {
                txtPeriodNumber.Focus();
            }
        }
        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPeriodNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// This KeyDown event auto insert currunt date when press enter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatededBy></CreatededBy>
        /// <CreatedDate>06 July 2016</CreatedDate>
        //private void txtEndDate_KeyDown(object sender, KeyEventArgs e)
        //{
        //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        if (e.Key == Key.Enter || e.Key== Key.Tab)
        //        {
        //            string value = txtEndDate.Text;
        //            int index = txtEndDate.CaretIndex;
        //            if (!string.IsNullOrEmpty(value) && index > 0)
        //            {
        //                string GetDate = CommonSettings.GetDateValue(value, index);
        //                txtEndDate.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy");
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
