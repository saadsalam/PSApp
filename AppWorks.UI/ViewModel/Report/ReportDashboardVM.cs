using AppWorks.UI.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AppWorks.UI.View.Reports;
using AppWorks.UI.Properties;

namespace AppWorks.UI.ViewModel.Report
{
    public class ReportDashboardVM : ViewModelBase
    {

        #region Properties

        private object _currentReportControl;
        public object CurrentReportControl
        {
            get { return _currentReportControl; }
            private set
            {
                _currentReportControl = value;
                NotifyPropertyChanged("CurrentReportControl");
            }
        }

        #endregion Properties

        #region Commands

        private AppWorxCommand btnVehicleSummary_Click;
        /// <summary>
        /// Vehicle Summary button event
        /// </summary>
        public AppWorxCommand BtnVehicleSummary_Click
        {
            get
            {
                if (btnVehicleSummary_Click == null)
                {
                    btnVehicleSummary_Click = new AppWorxCommand(this.OpenVehicleSummaryReport);
                }
                return btnVehicleSummary_Click;
            }
        }
        /// <summary>
        /// This function is used to open window for Vehicle Summary Report
        /// </summary>
        /// <param name="obj"></param>
        private void OpenVehicleSummaryReport(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                VehicleSummaryReport objOpen = new VehicleSummaryReport();
                CurrentReportControl = objOpen;
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

        private AppWorxCommand btnVehicleListing_Click;
        /// <summary>
        /// vehicle Listing button event
        /// </summary>
        public AppWorxCommand BtnVehicleListing_Click
        {
            get
            {
                if (btnVehicleListing_Click == null)
                {
                    btnVehicleListing_Click = new AppWorxCommand(this.OpenVehicleListingReport);
                }
                return btnVehicleListing_Click;
            }
        }
        /// <summary>
        /// This function is used to open window for Vehicle Listing Report
        /// </summary>
        /// <param name="obj"></param>
        private void OpenVehicleListingReport(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                VehicleListingReport objOpen = new VehicleListingReport();
                CurrentReportControl = objOpen;
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

        private AppWorxCommand btnVehicleRequest_Click;
        /// <summary>
        /// vehicle Request button event
        /// </summary>
        public AppWorxCommand BtnVehicleRequest_Click
        {
            get
            {
                if (btnVehicleRequest_Click == null)
                {
                    btnVehicleRequest_Click = new AppWorxCommand(this.OpenVehicleRequestReport);
                }
                return btnVehicleRequest_Click;
            }
        }
        /// <summary>
        /// This function is used to open window for Vehicle Request Report
        /// </summary>
        /// <param name="obj"></param>
        private void OpenVehicleRequestReport(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                VehicleRequestReport objOpen = new VehicleRequestReport();
                CurrentReportControl = objOpen;
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

        private AppWorxCommand btnLaneSummary_Click;
        /// <summary>
        /// vehicle Lane Summary Listing button event
        /// </summary>
        public AppWorxCommand BtnLaneSummary_Click
        {
            get
            {
                if (btnLaneSummary_Click == null)
                {
                    btnLaneSummary_Click = new AppWorxCommand(this.OpenLaneSummaryReport);
                }
                return btnLaneSummary_Click;
            }
        }
        /// <summary>
        ///   
        /// This function is used to open window for Lane Summary Report
        /// </summary>
        /// <param name="obj"></param>
        private void OpenLaneSummaryReport(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                LaneSummaryReport objOpen = new LaneSummaryReport();
                CurrentReportControl = objOpen;
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

        private AppWorxCommand btnInvoiceSummary_Click;
        /// <summary>
        /// Invoice Summary button event
        /// </summary>
        public AppWorxCommand BtnInvoiceSummary_Click
        {
            get
            {
                if (btnInvoiceSummary_Click == null)
                {
                    btnInvoiceSummary_Click = new AppWorxCommand(this.OpenInvoiceSummaryReport);
                }
                return btnInvoiceSummary_Click;
            }
        }
        /// <summary>
        /// This function is used to open window for Port Storage InBound Report
        /// </summary>
        /// <param name="obj"></param>
        private void OpenInvoiceSummaryReport(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                InvoiceSummaryReport objOpen = new InvoiceSummaryReport();
                CurrentReportControl = objOpen;
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

        private AppWorxCommand btnInventoryComparision_Click;
        /// <summary>
        /// Inventory Comparision button event
        /// </summary>
        public AppWorxCommand BtnInventoryComparision_Click
        {
            get
            {
                if (btnInventoryComparision_Click == null)
                {
                    btnInventoryComparision_Click = new AppWorxCommand(this.OpenInventoryComparisionReport);
                }
                return btnInventoryComparision_Click;
            }
        }
        /// <summary>
        /// This function is used to open window for Inventory Comparision Report
        /// </summary>
        /// <param name="obj"></param>
        private void OpenInventoryComparisionReport(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                InventoryComparisionReport objOpen = new InventoryComparisionReport();
                CurrentReportControl = objOpen;
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

        private AppWorxCommand btnPortStorageInBound_Click;
        /// <summary>
        /// Port Storage InBound button event
        /// </summary>
        public AppWorxCommand BtnPortStorageInBound_Click
        {
            get
            {
                if (btnPortStorageInBound_Click == null)
                {
                    btnPortStorageInBound_Click = new AppWorxCommand(this.OpenPortStorageInBoundReport);
                }
                return btnPortStorageInBound_Click;
            }
        }
        /// <summary>
        /// This function is used to open window for Port Storage InBound Report
        /// </summary>
        /// <param name="obj"></param>
        private void OpenPortStorageInBoundReport(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                PortStorageInBoundReport objOpen = new PortStorageInBoundReport();
                CurrentReportControl = objOpen;
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

        private AppWorxCommand _clearCurrentReportCommand;
        public AppWorxCommand ClearCurrentReportCommand
        {
            get
            {
                return _clearCurrentReportCommand ??
                    (_clearCurrentReportCommand = new AppWorxCommand(x => CurrentReportControl = null));
            }
        }

        #endregion Commands

        #region Constructors

        public ReportDashboardVM()
        {
            string Report = string.Empty;
        }

        #endregion Constructors

    }
}
