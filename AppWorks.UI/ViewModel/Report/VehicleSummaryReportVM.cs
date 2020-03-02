using System;
using System.Collections.Generic;
using System.Linq;
using Appworks.Reports;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;
using System.Windows;
using AppWorks.UI.Properties;

namespace AppWorks.UI.ViewModel.Report
{
    public class VehicleSummaryReportVM : ViewModelBase
    {
        string userCode = Application.Current.Properties["LoggedInUserName"].ToString();
        #region :: Page Property ::
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
        private bool dateIn;
        public bool DateIn
        {
            get { return this.dateIn; }
            set { this.dateIn = value; NotifyPropertyChanged("DateIn"); }
        }
        private bool requestDate;
        public bool RequestDate
        {
            get { return this.requestDate; }
            set { this.requestDate = value; NotifyPropertyChanged("RequestDate"); }
        }
        private int reportType;
        public int ReportType
        {
            get { return this.reportType; }
            set { this.reportType = value; NotifyPropertyChanged("ReportType"); }
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
        private string heading;
        public string Heading
        {
            get { return this.heading; }
            set { this.heading = value; NotifyPropertyChanged("heading"); }
        }
        #endregion

        #region :: Button Event ::
        private AppWorxCommand radioButton_Checked;
        /// <summary>
        /// Redio button check event
        /// </summary>
        public AppWorxCommand RadioButton_Checked
        {
            get
            {
                if (radioButton_Checked == null)
                {
                    radioButton_Checked = new AppWorxCommand(this.GetRadioButton);
                }
                return radioButton_Checked;
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

        #endregion

        #region Events

        public event EventHandler RefreshReportRequested;
        private void OnRefreshReportRequested()
        {
            EventHandler handler = RefreshReportRequested;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion Events

        #region :: Constructor ::
        /// <summary>
        /// This constructor is used to get report data
        /// </summary>
        public VehicleSummaryReportVM()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                DateIn = true;
                Heading = Resources.TextReceived;

                var data1 = _serviceInstance.GetDAIAddressName(userCode).Select(d => new VehicleSummaryReport
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

                var report = new VehicleSummaryRPT();

                var data = _serviceInstance.GetPortStorageVehicleSummeryReport(0, StartDate, EndDate).Select(d => new Appworks.Reports.VehicleSummaryReport
                {
                    CustomerName = d.CustomerName,
                    //UnitCount = d.Count,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    CompanyName = CompanyName,
                    AddressLine1 = AddressLine1,
                    City = City,
                    Phone = Phone,
                    HeadingText = Heading
                }).ToList();
                if (data != null)
                {
                    data.Add(new VehicleSummaryReport
                    {
                        StartDate = StartDate,
                        EndDate = EndDate,
                        CompanyName = CompanyName,
                        AddressLine1 = AddressLine1,
                        City = City,
                        Phone = Phone,
                        HeadingText = Heading
                    });
                }

                report.DataSource = data;
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
        #endregion

        #region :: Page Function ::
        /// <summary>
        /// This function is used to search report according to date
        /// </summary>
        /// <param name="obj"></param>
        public void Search(object obj)
        {
            if (StartDate == null || EndDate == null)
            {
                MessageBox.Show(Resources.msgDateReq);
                return;
            }
            if (StartDate > EndDate)
            {
                MessageBox.Show(Resources.msgDateCompair);
                return;
            }
            OnRefreshReportRequested();
        }

        public IEnumerable<VehicleSummaryReport> LoadData()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            if (StartDate == null || EndDate == null || StartDate > EndDate)
                return Enumerable.Empty<VehicleSummaryReport>();

            List<VehicleSummaryReport> data = new List<VehicleSummaryReport>();
            try
            {
                if (StartDate != null && EndDate != null)
                {
                    if (StartDate <= EndDate)
                    {
                        data = _serviceInstance.GetPortStorageVehicleSummeryReport(ReportType, StartDate, EndDate).Select(d => new Appworks.Reports.VehicleSummaryReport
                        {
                            CustomerName = d.CustomerName,
                            UnitCount = d.Count,
                            StartDate = StartDate,
                            EndDate = EndDate,
                            CompanyName = CompanyName,
                            AddressLine1 = AddressLine1,
                            City = city,
                            Phone = Phone,
                            HeadingText = "Storage Vehicles " + Heading + " Between " + StartDate.Value.ToShortDateString() + " And " + EndDate.Value.ToShortDateString(),
                        }).ToList();

                        int totalUnits = 0;
                        if (data != null)
                        {
                            totalUnits = data.Sum(s => s.UnitCount);
                            data.Add(new VehicleSummaryReport
                            {
                                CustomerName = Resources.msgTotalUnitReport,
                                UnitCount = totalUnits,
                                StartDate = StartDate,
                                EndDate = EndDate,
                                CompanyName = CompanyName,
                                AddressLine1 = AddressLine1,
                                City = City,
                                Phone = Phone,
                                HeadingText = "Storage Vehicles " + Heading + " Between " + StartDate.Value.ToShortDateString() + " And " + EndDate.Value.ToShortDateString(),
                            });
                        }
                        else
                        {

                            MessageBox.Show(Resources.msgDateCompair);
                            return data;
                        }

                    }
                    else
                    {
                        MessageBox.Show(Resources.msgDateReq);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
                return data;
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to get redio button value for check
        /// </summary>
        /// <param name="obj"></param>
        private void GetRadioButton(object obj)
        {

            try
            {
                if (DateIn == true && RequestDate == false)
                {
                    ReportType = 0;
                    Heading = Resources.TextReceived;
                }
                else if (DateIn == false && RequestDate == true)
                {
                    ReportType = 1;
                    Heading = Resources.TextRequested;
                }
                else if (DateIn == false && RequestDate == false)
                {
                    ReportType = 2;
                    Heading = Resources.TextPickedUp;
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

            }
        }
        #endregion
    }
}
