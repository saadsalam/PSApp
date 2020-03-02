using Appworks.Reports;
using AppWorks.UI.Common;
using AppWorks.UI.Properties;
using AppWorks.UI.ViewModel.Vehicle;
using AppWorks.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;



namespace AppWorks.UI.ViewModel.Report
{
    public class VehicleRequestReportVM : ViewModelBase
    {
        string userCode = Application.Current.Properties["LoggedInUserName"].ToString();
        #region :: Page Property ::
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
        private bool allNewRequest;
        public bool AllNewRequest
        {
            get { return this.allNewRequest; }
            set { this.allNewRequest = value; NotifyPropertyChanged("AllNewRequest"); }
        }
        private bool byCustomer;
        public bool ByCustomer
        {
            get { return this.byCustomer; }
            set { this.byCustomer = value; NotifyPropertyChanged("ByCustomer"); }
        }
        private bool byVIN;
        public bool ByVIN
        {
            get { return this.byVIN; }
            set { this.byVIN = value; NotifyPropertyChanged("ByVIN"); }
        }
        private bool byDate;
        public bool ByDate
        {
            get { return this.byDate; }
            set { this.byDate = value; NotifyPropertyChanged("ByDate"); }
        }
        private bool byBatch;
        public bool ByBatch
        {
            get { return this.byBatch; }
            set { this.byBatch = value; NotifyPropertyChanged("ByBatch"); }
        }
        private int customerId;
        public int CustomerId
        {
            get { return this.customerId; }
            set { this.customerId = value; NotifyPropertyChanged("CustomerId"); }
        }
        private string vinNumber;
        public string VinNumber
        {
            get { return this.vinNumber; }
            set { this.vinNumber = value; NotifyPropertyChanged("VinNumber"); }
        }
        private int batchId;
        public int BatchId
        {
            get { return this.batchId; }
            set { this.batchId = value; NotifyPropertyChanged("BatchId"); }
        }
        private int reportType;
        public int ReportType
        {
            get { return this.reportType; }
            set { this.reportType = value; NotifyPropertyChanged("ReportType"); }
        }
        private Nullable<System.DateTime> startDateR;
        public Nullable<System.DateTime> StartDateR
        {
            get { return this.startDateR; }
            private set { this.startDateR = value; NotifyPropertyChanged("StartDateR"); }
        }
        private Nullable<System.DateTime> endDateR;
        public Nullable<System.DateTime> EndDateR
        {
            get { return this.endDateR; }
            private set { this.endDateR = value; NotifyPropertyChanged("EndDateR"); }
        }

        private List<string> customerList;
        public List<string> CustomerList
        {
            get { return this.customerList; }
            private set { this.customerList = value; NotifyPropertyChanged("CustomerList"); }
        }

        private string byCustomerName;
        public string ByCustomerName
        {
            get { return this.byCustomerName; }
            set { this.byCustomerName = value; NotifyPropertyChanged("ByCustomerName"); }
        }

        private bool isCheckEnableAll;
        public bool IsCheckEnableAll
        {
            get { return isCheckEnableAll; }
            set
            {
                this.isCheckEnableAll = value;
                NotifyPropertyChanged("IsCheckEnableAll");
            }
        }

        private bool isCheckEnableByCustomer;
        public bool IsCheckEnableByCustomer
        {
            get { return isCheckEnableByCustomer; }
            set
            {
                this.isCheckEnableByCustomer = value;
                NotifyPropertyChanged("IsCheckEnableByCustomer");
            }
        }

        private bool isCheckEnableByVIN;
        public bool IsCheckEnableByVIN
        {
            get { return isCheckEnableByVIN; }
            set
            {
                this.isCheckEnableByVIN = value;
                NotifyPropertyChanged("IsCheckEnableByVIN");
            }
        }

        private string custoemrName;
        public string CustoemrName
        {
            get { return custoemrName; }
            set
            {
                this.custoemrName = value;
                NotifyPropertyChanged("CustoemrName");
            }
        }

        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set
            {
                this.customerID = value;
                NotifyPropertyChanged("CustomerID");
            }
        }

        private bool isCheckEnableByDate;
        public bool IsCheckEnableByDate
        {
            get { return isCheckEnableByDate; }
            set
            {
                this.isCheckEnableByDate = value;
                NotifyPropertyChanged("IsCheckEnableByDate");
            }
        }

        private bool isCheckEnableByBatch;
        public bool IsCheckEnableByBatch
        {
            get { return isCheckEnableByBatch; }
            set
            {
                this.isCheckEnableByBatch = value;
                NotifyPropertyChanged("IsCheckEnableByBatch");
            }
        }

        public List<AppWorksService.Properties.PortStorageRequestsReportProp> batchList;
        public List<AppWorksService.Properties.PortStorageRequestsReportProp> BatchList
        {
            get { return batchList; }
            private set
            {
                this.batchList = value;
                NotifyPropertyChanged("BatchList");
            }
        }
        public List<AppWorksService.Properties.PortStorageRequestsReportProp> customeList;
        public List<AppWorksService.Properties.PortStorageRequestsReportProp> CustomeList
        {
            get { return customeList; }
            private set
            {
                this.customeList = value;
                NotifyPropertyChanged("CustomeList");
            }
        }

        private string companyName;
        public string CompanyName
        {
            get { return this.companyName; }
            set { this.companyName = value; NotifyPropertyChanged("CompanyName"); }
        }
        private string companyAddress;
        public string CompanyAddress
        {
            get { return this.companyAddress; }
            set { this.companyAddress = value; NotifyPropertyChanged("CompanyAddress"); }
        }
        private string companycity;
        public string CompanyCity
        {
            get { return this.companycity; }
            set { this.companycity = value; NotifyPropertyChanged("CompanyCity"); }
        }
        private string phone;
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; NotifyPropertyChanged("Phone"); }
        }

        /// <summary>
        /// To display UI busy while batchId is loading
        /// </summary>
        private bool _allControlsEnabled;
        public bool AllControlsEnabled
        {
            get
            {
                return _allControlsEnabled;
            }
            set
            {
                _allControlsEnabled = value;
                NotifyPropertyChanged("AllControlsEnabled");
            }
        }

        private string _gridSortOrder;
        public string GridSortOrder
        {
            get 
            {
                return _gridSortOrder;
            }
            set 
            {
                _gridSortOrder = value;                
            }
        }

        private string _sortedColumn;
        public string SortedColumn
        {
            get
            {
                return _sortedColumn;
            }
            set
            {
                _sortedColumn = value;
            }
        }


        #endregion

        #region :: Page Event ::
        private AppWorxCommand radioButton_Checked;
        /// <summary>
        /// Redion button checked event
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
        /// Search button event
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

        /// <summary>
        /// This function is used to get all customer list from database
        /// </summary>
        private void FillCustomerList()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var data = _serviceInstance.LoadCustomerList().Select(d => new AppWorksService.Properties.PortStorageRequestsReportProp
                {
                    CustomerName = d.CustomerName,
                    CustomerId = d.CustomerId
                });

                foreach (var item in data)
                {
                    CustoemrName = item.CustomerName;
                    CustomerID = item.CustomerId;
                }

                CustomeList = data.ToList();
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

        public VehicleRequestReportVM()
        {
            AllControlsEnabled = true;
            DelegateEventRequestProcessing.OnReprintGridSorted += new DelegateEventRequestProcessing.SetSortingParams(SortReprintGrid);
        }

        public void SortReprintGrid(string value)
        {
            if (SortedColumn != value)
                GridSortOrder = string.Empty;

            GridSortOrder = string.IsNullOrEmpty(GridSortOrder) ? SortOrder.ASC : GridSortOrder.Equals(SortOrder.ASC) ? SortOrder.DESC : SortOrder.ASC;
            AllControlsEnabled = false;
            switch (value)
            {
                default:
                case BatchGridColumnsTagName.BatchId:
                    BatchList = GridSortOrder.Equals(SortOrder.ASC) ? BatchList.OrderBy(x => x.RequestPrintedBatchID).ToList() : BatchList.OrderByDescending(x => x.RequestPrintedBatchID).ToList();
                    break;
                case BatchGridColumnsTagName.Date:
                    BatchList = GridSortOrder.Equals(SortOrder.ASC) ? BatchList.OrderBy(x => x.DateRequestPrinted).ToList() : BatchList.OrderByDescending(x => x.DateRequestPrinted).ToList();
                    break;
                case BatchGridColumnsTagName.Records:
                    BatchList = GridSortOrder.Equals(SortOrder.ASC) ? BatchList.OrderBy(x => x.BatchCount).ToList() : BatchList.OrderByDescending(x => x.BatchCount).ToList();
                    break;
            }
            AllControlsEnabled = true;
            SortedColumn = value;
        }

        /// <summary>
        /// This method is used to get all report data from database
        /// </summary>
        /// <param name="obj"></param>
        public void Search(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var report = new VehicleRequestRPT();

                if (ReportType == 3)
                {
                    if (StartDateR == null || EndDateR == null)
                    {
                        System.Windows.MessageBox.Show(Resources.msgDateReq);

                        return;
                    }
                    else
                    {
                        if (StartDateR > EndDateR)
                        {
                            System.Windows.MessageBox.Show(Resources.msgDateCompair);
                            return;
                        }
                    }
                }

                if (ReportType == 2)
                {
                    if (VinNumber == null)
                    {
                        System.Windows.MessageBox.Show(Resources.msgVINReq);
                        return;
                    }
                }

                if (ReportType == 4)
                {
                    if (BatchId == 0)
                    {
                        MessageBox.Show(Resources.msgBatchReq);
                        return;
                    }
                }

                var data = _serviceInstance.GetPortStorageRequestReport(ReportType, CustomerID, VinNumber, StartDateR, EndDateR, BatchId).Select(d => new Appworks.Reports.VehicleRequestReport
                {
                    VIN = d.VIN,
                    ShortVIN = d.ShortVIN.Contains("*") ? d.ShortVIN : string.Format("*{0}*", d.VIN.Substring(d.VIN.Length - Math.Min(8, d.VIN.Length))),
                    DateRequested = d.DateRequested,
                    DateOut = d.DateOut,
                    Model = string.Format("{0} {1} {2}", d.VehicleYear, d.Make, d.Model),
                    Color = d.Color,
                    CustomerName = d.CustomerName,
                    AdddressLine1 = d.AdddressLine1,
                    AddressLine2 = d.AddressLine2,
                    City = d.City,
                    State = d.State,
                    Zip = d.Zip,
                    FullAddress = (!string.IsNullOrEmpty(d.City) ? d.City + ", " : "") + d.State + " " + d.Zip,
                    BayLocation = d.BayLocation,
                    EstimatedPickupDate = d.EstimatedPickupDate,
                    DealerPrintDate = d.DealerPrintDate,
                    CompanyName = CompanyName,
                    CompanyAddress = CompanyAddress,
                    CompanyCity = CompanyCity,
                    Phone = Phone,
                    DayName = d.DateRequested != null ? d.DateRequested.Value.DayOfWeek.ToString() : "",
                    ReturnToInventory = d.DateRequested != null ? d.DateRequested.Value.AddDays(5) : d.DateRequested,
                    DayNamePickUp = d.EstimatedPickupDate != null ? d.EstimatedPickupDate.Value.DayOfWeek.ToString() : ""


                }).ToList();
                //if (data != null)
                //{

                //    data.Add(new Appworks.Reports.VehicleRequestReport
                //    {
                //        CompanyName = CompanyName,
                //        CompanyAddress = CompanyAddress,
                //        CompanyCity = CompanyCity,
                //        Phone = Phone
                //    });
                //}
                report.DataSource = data;
                MyReportSource = report;

                //}
                //else
                //{

                //    System.Windows.MessageBox.Show(Resources.msgDateCompair);
                //    return;
                //}              
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
        /// This method is used to get all batchlist from database
        /// </summary>
        public async void FillBatchList()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                AllControlsEnabled = false;
                //App.Current.Dispatcher.BeginInvoke(new Action(() =>
                await Task.Run(() =>
                {
                    var data = _serviceInstance.LoadBatchList();
                    //    .GroupBy(x=>x.RequestPrintedBatchID).Select(d => new AppWorksService.Properties.PortStorageRequestsReportProp
                    //{
                    //    RequestPrintedBatchID = d.Key,
                    //    DateRequestPrinted = d.FirstOrDefault().DateRequestPrinted,
                    //    BatchCount = d.Count()
                    //});

                    foreach (var item in data)
                    {
                        BatchId = (int)item.RequestPrintedBatchID;
                    }
                    BatchList = data.OrderByDescending(x=>x.RequestPrintedBatchID).ToList();
                });
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
                AllControlsEnabled = true;
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }
        /// <summary>
        /// This function is used to get selected redio button status
        /// </summary>
        /// <param name="obj"></param>
        private void GetRadioButton(object obj)
        {
            BatchId = 0;
            if (AllNewRequest == true && ByCustomer == false && ByVIN == false && ByDate == false && ByBatch == false)
            {
                ReportType = 0;
                IsCheckEnableByCustomer = false;
                IsCheckEnableByVIN = false;
                IsCheckEnableByDate = false;
                IsCheckEnableByBatch = false;
                CustomeList = null;
                BatchList = null;
                StartDateR = null;
                EndDateR = null;
                VinNumber = null;
            }
            else if (AllNewRequest == false && ByCustomer == true && ByVIN == false && ByDate == false && ByBatch == false)
            {
                ReportType = 1;
                IsCheckEnableByCustomer = true;
                IsCheckEnableByVIN = false;
                IsCheckEnableByDate = false;
                IsCheckEnableByBatch = false;
                FillCustomerList();
                BatchList = null;
                StartDateR = null;
                EndDateR = null;
                VinNumber = null;
            }
            else if (AllNewRequest == false && ByCustomer == false && ByVIN == true && ByDate == false && ByBatch == false)
            {
                ReportType = 2;
                IsCheckEnableByCustomer = false;
                IsCheckEnableByVIN = true;
                IsCheckEnableByDate = false;
                IsCheckEnableByBatch = false;
                CustomeList = null;
                BatchList = null;
                StartDateR = null;
                EndDateR = null;

            }
            else if (AllNewRequest == false && ByCustomer == false && ByVIN == false && ByDate == true && ByBatch == false)
            {
                ReportType = 3;
                IsCheckEnableByCustomer = false;
                IsCheckEnableByVIN = false;
                IsCheckEnableByDate = true;
                IsCheckEnableByBatch = false;
                CustomeList = null;
                BatchList = null;
                VinNumber = null;
            }
            else if (AllNewRequest == false && ByCustomer == false && ByVIN == false && ByDate == false && ByBatch == true)
            {
                ReportType = 4;
                IsCheckEnableByBatch = true;
                IsCheckEnableByCustomer = false;
                IsCheckEnableByVIN = false;
                IsCheckEnableByDate = false;
                FillBatchList();
                CustomeList = null;
                StartDateR = null;
                EndDateR = null;
                VinNumber = null;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DelegateEventRequestProcessing.OnReprintGridSorted -= new DelegateEventRequestProcessing.SetSortingParams(SortReprintGrid);
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }


    public class BatchGridColumnsTagName
    {
        public const string BatchId = "BatchId";
        public const string Date = "Date";
        public const string Records = "Records";
    }
}
