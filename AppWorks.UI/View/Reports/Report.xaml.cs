using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppWorks.UI.ViewModel.Report;
using Props = AppWorks.UI.Properties;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;

namespace AppWorks.UI.View.Reports
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : UserControl
    {
        private ReportDashboardVM objReportDashboardVM = new ReportDashboardVM();
     
        public Report()
        {
            InitializeComponent();
            this.DataContext = objReportDashboardVM;
            //btnBack.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// This click event for back button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
           CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
            //btnReport.Visibility = Visibility.Collapsed;
            //btnDash.Visibility = System.Windows.Visibility.Visible;
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
        /// This button click event for Vechicle Summary report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnVehicleSummary_Click(object sender, RoutedEventArgs e)
        {
              CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
            //StackPanelFixed_SP1.Children.Clear();
            VehicleSummaryReport uc1 = new VehicleSummaryReport();
            //StackPanelFixed_SP1.Children.Add(uc1);
            uc1.Visibility = System.Windows.Visibility.Visible;
            btnReport.Visibility = Visibility.Visible;
            btnDash.Visibility = System.Windows.Visibility.Collapsed;
            btnBack.Visibility = Visibility.Visible;
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
        /// This button click event for Vechicle listing report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnVehicleListing_Click(object sender, RoutedEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
            //StackPanelFixed_SP1.Children.Clear();
            VehicleListingReport uc1 = new VehicleListingReport();
            //StackPanelFixed_SP1.Children.Add(uc1);
            uc1.Visibility = System.Windows.Visibility.Visible;
            btnReport.Visibility = Visibility.Visible;
            btnDash.Visibility = System.Windows.Visibility.Collapsed;
            btnBack.Visibility = Visibility.Visible;
           // lblHeader.Content = "Port Storage>>Reports>>Vehicle Listing";
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
        /// This button click event for Vechicle Request report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnVehicleRequest_Click(object sender, RoutedEventArgs e)
        {
              CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
            //StackPanelFixed_SP1.Children.Clear();
            VehicleRequestReport uc1 = new VehicleRequestReport();
            //StackPanelFixed_SP1.Children.Add(uc1);
            uc1.Visibility = System.Windows.Visibility.Visible;
            btnReport.Visibility = Visibility.Visible;
            btnDash.Visibility = System.Windows.Visibility.Collapsed;
            btnBack.Visibility = Visibility.Visible;
            //lblHeader.Content = "Admin>>Add User";
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
        /// This button click event for lane Summary report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLaneSummary_Click(object sender, RoutedEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
            //StackPanelFixed_SP1.Children.Clear();
            LaneSummaryReport uc1 = new LaneSummaryReport();
            //StackPanelFixed_SP1.Children.Add(uc1);
            uc1.Visibility = System.Windows.Visibility.Visible;
            btnReport.Visibility = Visibility.Visible;
            btnDash.Visibility = System.Windows.Visibility.Collapsed;
            btnBack.Visibility = Visibility.Visible;
            //lblHeader.Content = "Admin>>Add User";
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
        /// This button click event for Port storage InBound report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPortStorageInBound_Click(object sender, RoutedEventArgs e)
        {
               CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
            //StackPanelFixed_SP1.Children.Clear();
            PortStorageInBoundReport uc1 = new PortStorageInBoundReport();
            //StackPanelFixed_SP1.Children.Add(uc1);
            uc1.Visibility = System.Windows.Visibility.Visible;
            btnReport.Visibility = Visibility.Visible;
            btnDash.Visibility = System.Windows.Visibility.Collapsed;
            btnBack.Visibility = Visibility.Visible;
            //lblHeader.Content = "Admin>>Add User";
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
        /// This button click event for Inventory comarision report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInventoryComparision_Click(object sender, RoutedEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
            //StackPanelFixed_SP1.Children.Clear();
            InventoryComparisionReport uc1 = new InventoryComparisionReport();
            //StackPanelFixed_SP1.Children.Add(uc1);
            uc1.Visibility = System.Windows.Visibility.Visible;
            btnReport.Visibility = Visibility.Visible;
            btnDash.Visibility = System.Windows.Visibility.Collapsed;
            btnBack.Visibility = Visibility.Visible;
            //lblHeader.Content = "Admin>>Add User";
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
        /// This button click event for Invoice Summary report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInvoiceSummary_Click(object sender, RoutedEventArgs e)
        {
           CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
            //StackPanelFixed_SP1.Children.Clear();
            InvoiceSummaryReport uc1 = new InvoiceSummaryReport();
            //StackPanelFixed_SP1.Children.Add(uc1);
            uc1.Visibility = System.Windows.Visibility.Visible;
            btnReport.Visibility = Visibility.Visible;
            btnDash.Visibility = System.Windows.Visibility.Collapsed;
            btnBack.Visibility = Visibility.Visible;
            //lblHeader.Content = "Admin>>Add User";
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
