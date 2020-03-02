using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;
using AppWorks.UI.Common;
using System.Configuration;
using AppWorksService.Properties;
using AppWorks.UI.Properties;
using System.Windows;
using AppWorks.UI.View.UserControls;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppWorks.UI.ViewModel.Vehicle
{

    public class CustomerVM : ViewModelBase
    {
        int _GridPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]);
        int _GridPageSizeOnScroll = Convert.ToInt32(ConfigurationManager.AppSettings["FindUserGridPageSizeOnScroll"]);

        #region Events

        public event EventHandler CloseWindowRequested;
        private void OnCloseWindowRequested()
        {
            EventHandler handler = CloseWindowRequested;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion Events

        public CustomerVM()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.Title.Split(' ')[0] == Resources.WinHomeTitle)
                {
                    HomeWindow home = (HomeWindow)win;
                    foreach (Telerik.Windows.Controls.RadTabControl tb in FindVisualChildren<Telerik.Windows.Controls.RadTabControl>(win))
                    {
                        if (tb.Name == "VehicleLoc" || tb.Name == "tabControl")
                        {
                            IsPortStorage = 1;
                            //foreach (UIElement ui in home)
                            //{
                            //    AppWorks.UI.View.UserControls.Vehicle.PortStorageVehicalLocator vechileloc = (AppWorks.UI.View.UserControls.Vehicle.PortStorageVehicalLocator)ui;
                            //    if (vechileloc.Name == Resources.UControlPSVehicleLoc)
                            //    {
                            //        IsPortStorage = 1;
                            //    }
                            //} 
                        }
                    }

                }
            }

            LoadCutomerType();
            DelegateEventCustomer.OnSetValueEvtTotalCountPagerCmd += new DelegateEventCustomer.SetValueTotalCountPager(GetTotalPageCount);
            DelegateEventCustomer.OnSetValuePageNumberCmd += new DelegateEventCustomer.SetValuePageNumberClick(GetCurrentPageIndex);
            Application.Current.Properties["SearchCount"] = 0;

        }
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        //public void FillDetails(object obj)
        //{
        //    DelegateEventCustomer.OnSetCustomerValueEvt += new DelegateEventCustomer.SetCustomerValue(GetCustomerValue);
        //    LoadBillingAddress(BillingAddressID);
        //    LoadStreetAddress(MainAddressID);

        //}
        #region 'CustomerViewModel'
        /// <summary>
        /// Fill button event
        /// </summary>
        private AppWorxCommand btnFill_Click;
        public AppWorxCommand BtnFill_Click
        {
            get
            {
                if (btnFill_Click == null)
                {
                    btnFill_Click = new AppWorxCommand(this.Continue);
                }
                return btnFill_Click;
            }
        }

        private int? isPortStorage;
        public int? IsPortStorage
        {
            get { return isPortStorage; }
            set
            {
                isPortStorage = value;
                NotifyPropertyChanged("isPortStorage");
            }
        }

        private int cutomerId;
        public int CustomerID
        {
            get { return cutomerId; }
            set
            {
                cutomerId = value;
                NotifyPropertyChanged("CustomerID");
            }
        }

        private string cutomerCode;
        public string CustomerCode
        {
            get { return cutomerCode; }
            set
            {
                if (value != null)
                    cutomerCode = value;
                NotifyPropertyChanged("CustomerCode");
            }
        }

        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                if (value != null)
                    customerName = value;
                NotifyPropertyChanged("CustomerName");
            }
        }

        private string customerType;
        public string CustomerType
        {
            get { return customerType; }
            set
            {
                if (value != null)
                    customerType = value;
                NotifyPropertyChanged("CustomerType");
            }
        }

        private string addressLine1;
        public string AddressLine
        {
            get { return addressLine1; }
            set
            {
                if (value != null)
                    addressLine1 = value;
                NotifyPropertyChanged("AddressLine");
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (value != null)
                    city = value;
                NotifyPropertyChanged("City");
            }
        }

        private string state;
        public string State
        {
            get { return state; }
            set
            {
                if (value != null)
                    state = value;
                NotifyPropertyChanged("State");
            }
        }

        private string zip;
        public string Zip
        {
            get { return zip; }
            set
            {
                if (value != null)
                    zip = value;
                NotifyPropertyChanged("Zip");
            }
        }
        private long totalPageCount;
        public long TotalPageCount
        {
            get { return totalPageCount; }
            set
            {
                if (value != null)
                {
                    this.totalPageCount = value;
                    NotifyPropertyChanged("TotalPageCount");
                }
            }
        }

        private int currentPageIndex;
        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                if (value != null)
                {
                    this.currentPageIndex = value;
                }
            }
        }
        private string dbaName;
        public string DbaNAME
        {
            get { return dbaName; }
            set
            {
                if (value != null)
                    dbaName = value;
                NotifyPropertyChanged("DbaName");
            }
        }
        private string shortName;
        public string ShortName
        {
            get { return shortName; }
            set
            {
                if (value != null)
                    shortName = value;
                NotifyPropertyChanged("ShortName");
            }
        }
        private List<CodeList> lstCustomerType;
        public List<CodeList> ListCustomerType
        {
            get
            {
                return lstCustomerType;
            }
            private set
            {
                this.lstCustomerType = value;
                NotifyPropertyChanged("ListCustomerType");
            }
        }

        private ObservableCollection<CustomerInfo> customerList;
        public ObservableCollection<CustomerInfo> CustomerList
        {
            get { return customerList; }
            private set
            {
                this.customerList = value;
                NotifyPropertyChanged("CustomerList");
            }
        }

        #endregion

        #region 'Page Events'

        private CustomerInfo selectedDisRecipientGridItem;
        public CustomerInfo SelectedDisRecipientGridItem
        {
            get { return selectedDisRecipientGridItem; }
            set { selectedDisRecipientGridItem = value; NotifyPropertyChanged("SelectedDisRecipientGridItem"); }
        }

        private ObservableCollection<object> selectedItems;
        public ObservableCollection<object> SelectedItems
        {
            get { return selectedItems; }
            set
            {
                selectedItems = value;
                NotifyPropertyChanged("SelectedItems");
            }
        }

        private AppWorxCommand btnSearch;
        public AppWorxCommand BtnSearch
        {
            get
            {
                if (btnSearch == null)
                {
                    btnSearch = new AppWorxCommand(this.Search);
                }
                return btnSearch;
            }
        }

        private AppWorxCommand btnClear;
        public AppWorxCommand BtnClear
        {
            get
            {
                if (btnClear == null)
                {
                    btnClear = new AppWorxCommand(this.Clear);
                }
                return btnClear;
            }
        }

        private AppWorxCommand btnContinue_Click;
        public AppWorxCommand BtnContinue_Click
        {
            get
            {
                if (btnContinue_Click == null)
                {
                    btnContinue_Click = new AppWorxCommand(this.Continue);
                }
                return btnContinue_Click;
            }
        }
        private AppWorxCommand btnFillData_Click;
        public AppWorxCommand BtnFillData_Click
        {
            get
            {
                if (btnFillData_Click == null)
                {
                    btnFillData_Click = new AppWorxCommand(this.FillData);
                }
                return btnFillData_Click;
            }
        }

        private AppWorxCommand btnCancel_Click;
        public AppWorxCommand BtnCancel_Click
        {
            get
            {
                if (btnCancel_Click == null)
                {
                    btnCancel_Click = new AppWorxCommand(this.CancelWindow);
                }
                return btnCancel_Click;
            }
        }
        #endregion
        /// <summary>
        /// Get Total page count for grid
        /// </summary>
        /// 
        public void GetTotalPageCount(long totalPageCount)
        {
            TotalPageCount = totalPageCount;
        }

        public void GetCurrentPageIndex(int currentPageIndx)
        {
            CurrentPageIndex = currentPageIndx;
            if (Convert.ToInt32(Application.Current.Properties["SearchCount"]) > 0)
                Search("GridScroled");
        }


        /// <summary>
        /// for Total Records on end of grid
        /// </summary>
        /// 
        private long count = 0;
        public long Count
        {
            get { return count; }
            set
            {
                this.count = value;
                NotifyPropertyChanged("Count");
            }
        }
        /// <summary>
        /// Function to Search the Customer List.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 10,2016</createdOn>
        private async void Search(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (obj == null)
                {
                    CurrentPageIndex = 0;
                }

                if (CurrentPageIndex == 0)
                {
                    Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
                    Application.Current.Properties["FindUserGridCurrentPageIndex"] = 0;
                }

                CustomerSearchProp objCustomerSearchProp = new CustomerSearchProp();
                objCustomerSearchProp.CustomerCode = CustomerCode;
                objCustomerSearchProp.CustomerName = CustomerName;
                objCustomerSearchProp.CustomerType = CustomerType;
                objCustomerSearchProp.City = City;
                objCustomerSearchProp.State = State;
                objCustomerSearchProp.Zip = Zip;
                objCustomerSearchProp.DBAName = dbaName;
                objCustomerSearchProp.ShortName = shortName;
                objCustomerSearchProp.CurrentPageIndex = CurrentPageIndex;
                objCustomerSearchProp.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize; ;
                objCustomerSearchProp.DefaultPageSize = _GridPageSize;
                objCustomerSearchProp.PortStorageCustomerInd = IsPortStorage;

                var data = await _serviceInstance.GetCustomerSearchDetailsAsync(objCustomerSearchProp);

                IEnumerable<CustomerInfo> customerInfo = data.Select(d => new CustomerInfo
                {
                    CustomerID = d.CustomerID,
                    CustomerNameInfo = d.CustomerName,
                    AddressLine1Info = d.AddressLine1,
                    CityInfo = d.City,
                    SatetInfo = d.State,
                    ZipInfo = d.Zip,
                    CustomerCode = d.CustomerCode,
                    DBAName = d.DBAName,
                    ShortName = d.ShortName,
                    CustomerType = d.CustomerType,
                    CustomerOf = d.CustomerOf,
                    BillingAddressID = d.BillingAddressID,
                    MainAddressID = d.MainAddressID,
                    VendorNumber = d.VendorNumber,
                    DefaultBillingMethod = d.DefaultBillingMethod,
                    RecordStatus = d.RecordStatus,
                    SortOrder = d.SortOrder,
                    EligibleForAutoLoad = d.EligibleForAutoLoad,
                    ALwaysVVIPInd = d.ALwaysVVIPInd,
                    CollectionIssueInd = d.CollectionIssueInd,
                    AssignedToExternalCollectionsInd = d.AssignedToExternalCollectionsInd,
                    BulkBillingInd = d.BulkBillingInd,
                    DoNotPrintInvoiceInd = d.DoNotPrintInvoiceInd,
                    DoNotExportInvoiceInd = d.DoNotExportInvoiceInd,
                    HideNewFreightFromBrokerInd = d.HideNewFreightFromBrokerInd,
                    PortStorageCustomerInd = d.PortStorageCustomerInd,
                    AutoPortExportCustomerInd = d.AutoPortExportCustomerInd,
                    RequiresPONumberInd = d.RequiresPONumberInd,
                    SendEmailConfirmationsInd = d.SendEmailConfirmationsInd,
                    STIFollowupRequiredInd = d.STIFollowupRequiredInd,
                    DamagePhotoRequiredInd = d.DamagePhotoRequiredInd,
                    ApplyFuelSurchargeInd = d.ApplyFuelSurchargeInd,
                    FuelSurchargePercent = d.FuelSurchargePercent,
                    LoadNumberPrefix = d.LoadNumberPrefix,
                    LoadNumberLength = d.LoadNumberLength,
                    NextLoadNUmber = d.NextLoadNUmber,
                    BookingNumberPrefix = d.BookingNumberPrefix,
                    HandHeldScannerCustomerCode = d.HandHeldScannerCustomerCode,
                    DriverComment = d.DriverComment,
                    InternalComment = d.InternalComment,
                    CreatedDate = d.CreatedDate,
                    CreatedBy = d.CreatedBy,
                    UpdatedDate = d.UpdatedDate,
                    UpdatedBy = d.UpdatedBy,
                    TotalPageCount = d.TotalPageCount,
                    EntryRate = d.EntryRate,
                    PerDiemGraceDays = d.PerDiemGraceDays
                });

                //if index is 0 need to fill the List
                if (CurrentPageIndex == 0)
                {
                    CustomerList = null;
                    CustomerList = new ObservableCollection<CustomerInfo>(customerInfo);
                }
                //otherwise add into the list
                else
                {
                    if (CustomerList != null && CustomerList.Count > 0)
                    {
                        foreach (CustomerInfo ud in new ObservableCollection<CustomerInfo>(customerInfo))
                        {
                            CustomerList.Add(ud);
                        }
                    }
                }

                Application.Current.Properties["SearchCount"] = Convert.ToInt32(Application.Current.Properties["SearchCount"]) + 1;

                //If list count is > 0
                if (CustomerList != null && CustomerList.ToList().Count > 0)
                {
                    //Update Total record count on UI
                    Count = CustomerList.ToList().Where(x => x.TotalPageCount > 0).FirstOrDefault().TotalPageCount;
                }
                else
                {
                    Count = 0;
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
        /// Function to Clear the Customer List.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 10,2016</createdOn>
        private void Clear(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                CustomerID = 0;
                CustomerCode = string.Empty;
                CustomerName = string.Empty;
                AddressLine = string.Empty;
                City = string.Empty;
                State = string.Empty;
                Zip = string.Empty;
                CustomerList = null;
                Count = 0;
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
        /// Function to Get Customer Type.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 10,2016</createdOn>
        public void LoadCutomerType()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var qry = _serviceInstance.LoadCodeList(Enums.CodeType.CustomerType.ToString()).Select(d => new CodeList
                {
                    Code1 = d.Code1,
                    Description = d.Description,
                    CodeType = d.CodeType
                });
                ListCustomerType = new List<CodeList>(qry);

                ListCustomerType.Insert(0, new CodeList { Code1 = Resources.InsertAll, Description = Resources.InsertAll });
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
        /// Function to Fill Customer Record.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 10,2016</createdOn>
        public void FillCustomerInfo()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

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
        /// Function to Search the Customer List.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 10,2016</createdOn>
        private void CancelWindow(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                Application.Current.Properties["SearchCount"] = 0;

                OnCloseWindowRequested();

                DelegateEventVehicle.SetValueMethodEnableNew("EnableNew");
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
        /// Function to Clear the Customer List.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 10,2016</createdOn>
        private void Continue(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (SelectedDisRecipientGridItem != null)
                {
                    DelegateEventCustomer.SetCustomerValueMethod(SelectedDisRecipientGridItem);
                    DelegateEventVehicle.SetValueMethodCmd("Add");
                    //int countWindow = 0;
                    Application.Current.Properties["SearchCount"] = 0;
                    OnCloseWindowRequested();
                    //foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                    //{
                    //    if (countWindow == 1)
                    //    {
                    //        window.Close();
                    //    }
                    //    countWindow++;
                    //}
                }
                else
                {
                    MessageBox.Show(Resources.MsgSelectUser);
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
        /// Function to FIll Customer Data on row selection.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 10,2016</createdOn>
        private void FillData(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                DelegateEventCustomer.SetCustomerValueMethod(SelectedDisRecipientGridItem);
                DelegateEventVehicle.SetValueMethodCmd("Add");
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


    public class CustomerInfo
    {
        public int CustomerID { get; set; }
        public string CustomerNameInfo { get; set; }
        public string AddressLine1Info { get; set; }
        public string CityInfo { get; set; }
        public string SatetInfo { get; set; }
        public string ZipInfo { get; set; }
        public string CustomerCode { get; set; }
        public long TotalPageCount { get; set; }
        public string DBAName { get; set; }
        public string ShortName { get; set; }
        public string CustomerOf { get; set; }
        public string CustomerType { get; set; }
        public int? BillingAddressID { get; set; }
        public int? MainAddressID { get; set; }
        public decimal? EntryRate { get; set; }
        public int? PerDiemGraceDays { get; set; }

        /// Other Information Properties

        public string VendorNumber { get; set; }
        public string DefaultBillingMethod { get; set; }
        public string RecordStatus { get; set; }
        public int? SortOrder { get; set; }
        public int? EligibleForAutoLoad { get; set; }
        public int? ALwaysVVIPInd { get; set; }
        public int? CollectionIssueInd { get; set; }
        public int? AssignedToExternalCollectionsInd { get; set; }
        public int? BulkBillingInd { get; set; }
        public int? DoNotPrintInvoiceInd { get; set; }
        public int? DoNotExportInvoiceInd { get; set; }
        public int? HideNewFreightFromBrokerInd { get; set; }
        public int? PortStorageCustomerInd { get; set; }
        public int? AutoPortExportCustomerInd { get; set; }
        public int? RequiresPONumberInd { get; set; }
        public int? SendEmailConfirmationsInd { get; set; }
        public int? STIFollowupRequiredInd { get; set; }
        public int? DamagePhotoRequiredInd { get; set; }
        public int? ApplyFuelSurchargeInd { get; set; }
        public float? FuelSurchargePercent { get; set; }
        public string LoadNumberPrefix { get; set; }
        public int? LoadNumberLength { get; set; }
        public int? NextLoadNUmber { get; set; }
        public string BookingNumberPrefix { get; set; }
        public string HandHeldScannerCustomerCode { get; set; }
        public string InternalComment { get; set; }
        public string DriverComment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public decimal? DefaultEntryRate { get; set; }
        public decimal? DefaultPerDiem { get; set; }
        public int? DefaultPerDiemGraceDays { get; set; }
    }
}
