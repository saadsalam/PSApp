using System;
using System.Linq;
using Appworks.Reports;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;
using System.Windows;
using AppWorks.UI.Properties;
namespace AppWorks.UI.ViewModel.Report
{
    public class LaneSummaryReportVM : ViewModelBase
    {
        string userCode = Application.Current.Properties["LoggedInUserName"].ToString();
        /// <summary>
        /// This is property used to Change Report functionality
        /// </summary>
        Telerik.Reporting.ReportSource _myReportSource;

        public Telerik.Reporting.ReportSource MyReportSource
        {
            get
            {
                return _myReportSource;
            }
            set
            {
                // Check if it's really a change 
                if (value == _myReportSource)
                    return;

                // Change Report 
                _myReportSource = value;

                // Notify attached View(s) 
                NotifyPropertyChanged("MyReportSource");
                //RaisePropertyChanged("Report");
            }
        }

        private string companyName;
        public string CompanyName
        {
            get { return this.companyName; }
            set { this.companyName = value; NotifyPropertyChanged("CompanyName"); }
        }
        private string addressLine1;
        public string AddressLine1
        {
            get { return this.addressLine1; }
            set { this.addressLine1 = value; NotifyPropertyChanged("AddressLine1"); }
        }
        private string city;
        public string City
        {
            get { return this.city; }
            set { this.city = value; NotifyPropertyChanged("City"); }
        }
        private string phone;
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; NotifyPropertyChanged("Phone"); }
        }

        #region Constructor
        /// <summary>
        /// This constructor is used to get report data
        /// </summary>
        public LaneSummaryReportVM()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var data1 = _serviceInstance.GetDAIAddressName(userCode).Select(d => new LaneSummaryReport
                {
                    CompanyName = d.CompanyName,
                    AddressLine1 = d.AddressLine1,
                    City = d.City,
                    Phone = d.Phone
                }).FirstOrDefault();

                CompanyName = data1.CompanyName;
                AddressLine1 = data1.AddressLine1;
                City = data1.City;
                Phone = data1.Phone;

                var report = new LaneSummaryRPT();

                var data = _serviceInstance.GetPortStorageLaneSummaryList().Select(d => new LaneSummaryReport
                {
                    CustomerName = d.CustomerName,
                    Baylocation = d.Baylocation,
                    RecordsCount = d.RecordsCount,
                    CompanyName = CompanyName,
                    AddressLine1 = AddressLine1,
                    City = City,
                    Phone = Phone

                }).ToList();

                if (data != null)
                {

                    data.Add(new LaneSummaryReport
                    {
                        CompanyName = CompanyName,
                        AddressLine1 = AddressLine1,
                        City = City,
                        Phone = Phone
                    });
                }
                report.DataSource = data;
                MyReportSource = report;
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
        #endregion
    }
}
