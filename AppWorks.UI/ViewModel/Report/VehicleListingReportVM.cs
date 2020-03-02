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
    public class VehicleListingReportVM : ViewModelBase
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
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected = value) return;

                isSelected = value;

            }
        }
        private bool groupByDealerInd;
        public bool GroupByDealerInd
        {
            get { return groupByDealerInd; }
            set
            {
                if (groupByDealerInd = value) return;

                groupByDealerInd = value;

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
        private AppWorxCommand radioButton_Checked;
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

        private AppWorxCommand checkBox_Checked;
        public AppWorxCommand CheckBox_Checked
        {
            get
            {
                if (checkBox_Checked == null)
                {
                    checkBox_Checked = new AppWorxCommand(this.GroupByDealer);
                }
                return checkBox_Checked;
            }
        }
        /// <summary>
        /// This finction used to Checkbox for Group By Dealer
        /// </summary>
        /// <param name="obj"></param>
        private void GroupByDealer(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (IsSelected)
                {
                    GroupByDealerInd = true;
                }
                else
                {
                    GroupByDealerInd = false;
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
        /// <summary>
        /// This function is used to get redio button status
        /// </summary>
        /// <param name="obj"></param>
        private void GetRadioButton(object obj)
        {

            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
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
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
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



        #region Constructor 
        /// <summary>
        /// This constructor is used to get report data
        /// </summary>
        public VehicleListingReportVM()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                DateIn = true;
                Heading = Resources.TextReceived;
                var data1 = _serviceInstance.GetDAIAddressName(userCode).Select(d => new VehicleListingReport
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
                // get data and create report here 
                var report = new VehicleListingRPT();
                // var dataObject = objService.GetPortStorageVehicleSummeryReport(1, null, null);
                //  List<Appworks.Reports.VehicleListingReportProp> objReportModel = new List<Appworks.Reports.VehicleListingReportProp>();

                var data = _serviceInstance.GetVehicleListingReport(0, StartDate, EndDate, GroupByDealerInd).Select(d => new Appworks.Reports.VehicleListingReport
                {
                    CustomerName = d.CustomerName,
                    CustomerName1=d.CustomerName1,
                    VIN = d.VIN,
                    Model = d.Model,
                    DateIn = d.DateIn,
                    DateRequested = d.DateRequested,
                    DateOut = d.DateOut,
                    Color = d.Color,
                    BayLocation = d.BayLocation,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    CompanyName = CompanyName,
                    AddressLine1 = AddressLine1,
                    City = City,
                    Phone = Phone,
                    HeadingText = Heading,
                }).ToList();
                var totalCount = data.Count;
                if (data != null)
                {

                    data.Add(new VehicleListingReport
                    {
                        CompanyName = CompanyName,
                        AddressLine1 = AddressLine1,
                        City = City,
                        Phone = Phone,
                        HeadingText = Heading,
                        StartDate = StartDate,
                        EndDate = EndDate,
                        UnitCount = totalCount,
                        IsGrouped=GroupByDealerInd
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

        /// <summary>
        /// This function is used to search report according to date
        /// </summary>
        /// <param name="obj"></param>
        public void Search(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                var report = new VehicleListingRPT();
                // var dataObject = objService.GetPortStorageVehicleSummeryReport(1, null, null);
                // List<Appworks.Reports.VehicleListingReport> objReportModel = new List<Appworks.Reports.VehicleListingReport>();
                if (StartDate != null && EndDate != null)
                {
                    if (StartDate <= EndDate)
                    {
                        var data = _serviceInstance.GetVehicleListingReport(ReportType, StartDate, EndDate, GroupByDealerInd).Select(d => new Appworks.Reports.VehicleListingReport
                        {
                            CustomerName = d.CustomerName,
                            CustomerName1 = d.CustomerName1,
                            VIN = d.VIN,
                            Model =string.Format("{0} {1} ",d.Make,d.Model),
                            DateIn = d.DateIn,
                            DateRequested = d.DateRequested,
                            DateOut = d.DateOut,
                            Color = d.Color,
                            BayLocation = d.BayLocation,
                            StartDate = StartDate,
                            EndDate = EndDate,
                            CompanyName = CompanyName,
                            AddressLine1 = AddressLine1,
                            Make=d.Make,
                            City = City,
                            Phone = Phone,
                            HeadingText = "Storage Vehicles " + Heading + " Between " + StartDate.Value.ToShortDateString() + " And " + EndDate.Value.ToShortDateString(),
                            UnitCount=d.Count,
                            IsGrouped=GroupByDealerInd,
                            TempIndex=d.TempIndex
                        }).ToList();                                               
                        
                        //int totalUnits = 0;
                        //if (data != null)
                        //{

                        //    totalUnits = data.Count();
                        //    data.Add(new VehicleListingReport
                        //    {
                        //        CustomerName = null,
                        //        VIN = Resources.msgTotalUnitReport,
                        //        Model = totalUnits.ToString(),
                        //        DateIn = null,
                        //        DateRequested = null,
                        //        DateOut = null,
                        //        Color = null,
                        //        BayLocation = null,
                        //        CompanyName = CompanyName,
                        //        AddressLine1 = AddressLine1,
                        //        City = City,
                        //        Phone = Phone,
                        //        HeadingText = "Storage Vehicles " + Heading + " Between " + StartDate.Value.ToShortDateString() + " And " + StartDate.Value.ToShortDateString(),
                        //        //StartDate = StartDate,
                        //        //EndDate = EndDate
                        //    });
                        //}
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
