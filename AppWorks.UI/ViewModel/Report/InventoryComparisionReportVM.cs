#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using Appworks.Reports;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;
using System.Windows;
using AppWorks.UI.Properties;
using AppWorks.UI.ViewModel.Vehicle;
using AppWorks.Utilities;
#endregion

namespace AppWorks.UI.ViewModel.Report
{
    public class InventoryComparisionReportVM : ViewModelBase
    {
        string userCode = Application.Current.Properties["LoggedInUserName"].ToString();

        public InventoryComparisionReportVM()
        {
            DelegateEventRequestProcessing.OnReprintGridSorted += new DelegateEventRequestProcessing.SetSortingParams(SortReprintGrid);
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var data = _serviceInstance.GetDAIAddressName(userCode).Select(d => new InventoryComparisionReport
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


                var batchData = _serviceInstance.GetBatchLocationImport().Select(d => new InventoryComparisionReport
                {
                    BatchID = d.BatchID,
                    RecordsCount = d.RecordsCount,
                    CreattionDate = d.CreattionDate
                }).OrderByDescending(x=>x.BatchID);

                BatchList = new List<InventoryComparisionReport>(batchData);
                // get data and create report here 
                var report = new InventoryComparisionRPT();

                var data1 = _serviceInstance.GetInventoryComparisionList(0).Select(d => new InventoryComparisionReport
                {
                    VIN = d.VIN,
                    Make = d.Make,
                    Model = d.Model,
                    Color = d.Color,
                    DateIn = d.DateIn,
                    Status = d.Status,
                    Location = d.Location,
                    CompanyName = CompanyName,
                    AddressLine1 = AddressLine1,
                    City = city,
                    Phone = Phone
                }).ToList();
                if (data1 != null)
                {

                    data1.Add(new InventoryComparisionReport
                    {
                        CompanyName = CompanyName,
                        AddressLine1 = AddressLine1,
                        City = City,
                        Phone = Phone
                    });
                }

                report.DataSource = data1;
                // MyReportSource = report;               
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

        private void SortReprintGrid(string value)
        {
            if (SortedColumn != value)
                GridSortOrder = string.Empty;

            GridSortOrder = string.IsNullOrEmpty(GridSortOrder) ? SortOrder.ASC : GridSortOrder.Equals(SortOrder.ASC) ? SortOrder.DESC : SortOrder.ASC;
            switch (value)
            {
                default:
                case BatchGridColumnsTagName.BatchId:
                    BatchList = GridSortOrder.Equals(SortOrder.ASC) ? BatchList.OrderBy(x => x.BatchID).ToList() : BatchList.OrderByDescending(x => x.BatchID).ToList();
                    break;
                case BatchGridColumnsTagName.Date:
                    BatchList = GridSortOrder.Equals(SortOrder.ASC) ? BatchList.OrderBy(x => x.CreattionDate).ToList() : BatchList.OrderByDescending(x => x.CreattionDate).ToList();
                    break;
                case BatchGridColumnsTagName.Records:
                    BatchList = GridSortOrder.Equals(SortOrder.ASC) ? BatchList.OrderBy(x => x.RecordsCount).ToList() : BatchList.OrderByDescending(x => x.RecordsCount).ToList();
                    break;
            }
            SortedColumn = value;
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

        private int? batchID;
        public int? BatchID
        {
            get { return batchID; }
            set
            {
                if (value != null)
                {
                    batchID = value;
                }
                NotifyPropertyChanged("BatchID");
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
        private List<InventoryComparisionReport> batchList;
        public List<InventoryComparisionReport> BatchList
        {
            get { return batchList; }
            private set
            {
                if (value != null)
                {
                    batchList = value;
                }
                NotifyPropertyChanged("BatchList");
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
        /// This function is used to search report according to date
        /// </summary>
        /// <param name="obj"></param>
        public void Search(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (BatchID > 0)
                {
                    // get data and create report here 
                    var report = new InventoryComparisionRPT();
                    var data = _serviceInstance.GetInventoryComparisionList(BatchID).Select(d => new InventoryComparisionReport
                    {
                        VIN = d.VIN,
                        Make = d.Make,
                        Model = d.Model,
                        Color = d.Color,
                        DateIn = d.DateIn,
                        Status = d.Status,
                        Location = d.Location,
                        CompanyName = CompanyName,
                        AddressLine1 = AddressLine1,
                        City = city,
                        Phone = Phone
                    });

                    report.DataSource = data;
                    MyReportSource = report;
                }
                else
                {
                    MessageBox.Show(Resources.msgBatchReq);
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
