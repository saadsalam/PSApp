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
    public class InvoiceSummaryReportVM : ViewModelBase
    {
        string userCode = Application.Current.Properties["LoggedInUserName"].ToString();

        public InvoiceSummaryReportVM()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var data = _serviceInstance.GetDAIAddressName(userCode).Select(d => new InvoiceSummaryReport
                {
                    CompanyName = d.CompanyName,
                    AddressLine1 = d.AddressLine1,
                    City = d.City,
                    Phone = d.Phone
                }).FirstOrDefault();
                CompanyName = data.CompanyName;
                AddressLine1 = data.AddressLine1;
                City = data.City;
                Phone = data.Phone;

                // get data and create report here 
                var report = new InvoiceSummaryRPT();

                var data1 = _serviceInstance.LoadInvoiceList(0, StartDate, EndDate).Select(d => new InvoiceSummaryReport
                {
                    InvoiceNumber = d.InvoiceNumber,
                    InvoiceAmount = d.InvoiceAmount,
                    InvoiceDate = d.InvoiceDate,
                    Units = d.Units,
                    CustomerName = d.CustomerName,
                    CompanyName = CompanyName,
                    AddressLine1 = AddressLine1,
                    City = City,
                    Phone = Phone,
                    StartDate = StartDate,
                    EndDate = EndDate,
                }).ToList();

                double totalInvoiceAmount = 0;

                int totalUnits = 0;

                if (data1 != null)
                {
                    data1.Add(new InvoiceSummaryReport
                    {
                        InvoiceNumber = null,
                        InvoiceAmount = totalInvoiceAmount,
                        InvoiceDate = null,
                        Units = totalUnits,
                        CustomerName = null,
                        CompanyName = CompanyName,
                        AddressLine1 = AddressLine1,
                        City = City,
                        Phone = Phone,
                        StartDate = StartDate,
                        EndDate = EndDate
                    });
                }
                report.DataSource = data1;
                //MyReportSource = report;              
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
        private Nullable<System.DateTime> startDate;
        public Nullable<System.DateTime> StartDate
        {
            get { return startDate; }
            set
            {
                if (value != null)
                {
                    startDate = value;
                }
            }
        }
        private Nullable<System.DateTime> endDate;
        public Nullable<System.DateTime> EndDate
        {
            get { return endDate; }
            set
            {
                if (value != null)
                {
                    endDate = value;
                }
            }
        }

        private AppWorxCommand btnSearch_Click;
        /// <summary>
        /// Find button event
        /// </summary>
        public AppWorxCommand BtnSearch_Click
        {
            get
            {
                if (btnSearch_Click == null)
                {
                    btnSearch_Click = new AppWorxCommand(this.Search);
                }
                return btnSearch_Click;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCode"></param>
        public void GetCompanyInfo(string userCode)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                // var dataObject = objService.GetPortStorageVehicleSummeryReport(1, null, null);
                // List<Appworks.Reports.VehicleListingReport> objReportModel = new List<Appworks.Reports.VehicleListingReport>();

                var data = _serviceInstance.GetDAIAddressName(userCode).Select(d => new InvoiceSummaryReport
                {
                    CompanyName = d.CompanyName,
                    AddressLine1 = d.AddressLine1,
                    City = d.City,
                    Phone = d.Phone
                }).FirstOrDefault();

                CompanyName = data.CompanyName;
                AddressLine1 = data.AddressLine1;
                City = data.City;
                Phone = data.City;
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
        /// <summary>
        /// This function is used to search report according to date
        /// </summary>
        /// <param name="obj"></param>
        public void Search(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (StartDate != null && EndDate != null)
                {
                    if (StartDate <= EndDate)
                    {
                        // get data and create report here 
                        var report = new InvoiceSummaryRPT();

                        var data = _serviceInstance.LoadInvoiceList(0, StartDate, EndDate).Select(d => new InvoiceSummaryReport
                        {
                            InvoiceNumber = d.InvoiceNumber,
                            InvoiceAmount = d.InvoiceAmount,
                            InvoiceDate = d.InvoiceDate,
                            CustomerNumber = d.CustomerNumber,
                            Units = d.Units,
                            CustomerName = d.CustomerName,
                            CompanyName = CompanyName,
                            AddressLine1 = AddressLine1,
                            City = City,
                            Phone = Phone,
                            StartDate = StartDate,
                            EndDate = EndDate,
                        }).ToList();

                        //double totalInvoiceAmount = 0;

                        //int totalUnits = 0;

                        if (data != null)
                        {
                            //totalInvoiceAmount = data.Sum(x => x.InvoiceAmount);
                            //totalUnits = data.Sum(x => (int)x.Units);
                            //data.Add(new InvoiceSummaryReport
                            //{
                            //    InvoiceNumber = "Total:",
                            //    InvoiceAmount = totalInvoiceAmount,
                            //    InvoiceDate = null,
                            //    Units = totalUnits,
                            //    CustomerName = null,
                            //    CompanyName = CompanyName,
                            //    AddressLine1 = AddressLine1,
                            //    City = City,
                            //    Phone = Phone,
                            //    StartDate = StartDate,
                            //    EndDate = EndDate,
                            //});
                        }
                        report.DataSource = data;
                        MyReportSource = report;
                    }
                    else
                    {
                        MessageBox.Show(Resources.msgDateCompair);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(Resources.msgDateReq);
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
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }
    }
}
