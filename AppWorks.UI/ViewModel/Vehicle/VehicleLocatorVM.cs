using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using AppWorks.UI.Common;
using AppWorks.UI.Models;
using AppWorks.UI.View.UserControls.Vehicle;
using System.IO;
using System.Reflection;
using System.Globalization;
using AppWorks.UI.Properties;
using System.Threading.Tasks;
using System.ComponentModel;
using Telerik.Windows.Controls;
using AppWorks.Utilities;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class VehicleLocatorVM : ViewModelBase, IDisposable
    {
        int _GridPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]);
        int _GridPageSizeOnScroll = Convert.ToInt32(ConfigurationManager.AppSettings["FindUserGridPageSizeOnScroll"]);

        /// <summary>
        /// constructor of the class VehicleLocatorVM. 
        /// </summary>
        /// <param name="NA"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>13-mar-2016</createddate>
        public VehicleLocatorVM()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                DisplayFindUserTab = false;
                SelectedItems = new ObservableCollection<object>();
                DisplayEditUserTab = true;
                VehicalDetailsViewModel = new VehicleDetailsVM();
                SortState = SortingState.None;
                FillTVehicalRecordStatus(null);
                Count = 0;
                DelegateEventVehicle.OnSetValueEvt += new DelegateEventVehicle.SetValue(NotificationMessageReceived);
                DelegateEventVehicle.OnSetValueEvtCTabmd += new DelegateEventVehicle.SetValueTab(TabChange);
                DelegateEventVehicle.OnSetValueEvtTotalCountPagerCmd += new DelegateEventVehicle.SetValueTotalCountPager(GetTotalPageCount);
                DelegateEventVehicle.OnSetValuePageNumberCmd += new DelegateEventVehicle.SetValuePageNumberClick(GetCurrentPageIndex);
                DelegateEventVehicle.OnSetValueEvtRefreshCmd += new DelegateEventVehicle.SetValueRefreshCmd(GetCommandName);
                DelegateEventVehicle.OnSetValueEvtSearch += new DelegateEventVehicle.SetValueSearch(GetSearchCommand);
                DelegateEventVehicle.OnGridSorted += new DelegateEventVehicle.SetGridSorting(GridSorting);
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                CommonSettings.logger.LogError(this.GetType(), ex);

            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        public void GridSorting(string sortColumn, string sortOrder)
        {
            SetSortAndBindGrid(GridBindAction.Sorted, sortColumn, sortOrder);
        }

        private void SetSortAndBindGrid(GridBindAction action, string sortColumn = null, string sortDir = null)
        {
            if (action.Equals(GridBindAction.Sorted))
            {
                if (GridSortColumn != sortColumn)
                    GridSortOrder = SortingState.Ascending.ToString();
                else
                {
                    GridSortOrder = (GridSortOrder == SortingState.None.ToString() || GridSortOrder == SortingState.Descending.ToString()) ? SortingState.Ascending.ToString() : SortingState.Descending.ToString();
                }
                GridSortColumn = sortColumn;//VehicleLocatorGridColumns.VIN;
                //GridSortOrder = sortDir;//SortingState.Ascending.ToString();     
                BindGridDetails(null);
            }
            else if (action.Equals(GridBindAction.Scrolled))
            {
                if (Count > 0 && Count > VehicleList.Count)
                    BindGridDetails("GridScrolled");
                //GridSortColumn = sortColumn;//VehicleLocatorGridColumns.VIN;
                //GridSortOrder = sortDir;//SortingState.Ascending.ToString();                
            }
            else
            {
                GridSortColumn = VehicleLocatorGridColumns.VIN;
                GridSortOrder = SortingState.Ascending.ToString();
                BindGridDetails(null);
            }
        }

        private IChangeTracking _vehicalDetailsViewModel;
        public IChangeTracking VehicalDetailsViewModel
        {
            get { return _vehicalDetailsViewModel; }
            set
            {
                _vehicalDetailsViewModel = value;
                NotifyPropertyChanged("VehicalDetailsViewModel");
            }
        }

        public override IEnumerable<IChangeTracking> GetChildViewModels()
        {
            return new List<IChangeTracking> { _vehicalDetailsViewModel };
        }

        /// <summary>
        /// Parametrized constructor
        /// </summary>
        public VehicleLocatorVM(object param)
        {

        }

        private void GetCommandName(string value)
        {
            if (value.Equals("Refresh"))
            {
                VehicleList = null;
                Clear(null);
            }
        }

        private void GetSearchCommand(object obj, bool isCommand, DateTime? invoiceDateFrom, DateTime? invoiceDateTo, DateTime? dateInFrom, DateTime? dateInTo
                                            , DateTime? dateRequestFrom, DateTime? dateRequestTo, DateTime? dateOutFrom, DateTime? dateOutTo)
        {


            if (isCommand && obj != null && !string.IsNullOrEmpty(SelectedVehicalStatus))
            {
                if (invoiceDateFrom != null)
                    InBetwDtFrm = invoiceDateFrom;
                else if (invoiceDateTo != null)
                    InBetwDtTo = invoiceDateTo;
                else if (dateInFrom != null)
                    DtInBetwDtFrm = dateInFrom;
                else if (dateInTo != null)
                    DtInBetwDtTo = dateInTo;
                else if (dateRequestFrom != null)
                    DtRequestBetwDtFrm = dateRequestFrom;
                else if (dateRequestTo != null)
                    DtRequestBetwDtTo = dateRequestTo;
                else if (dateOutFrom != null)
                    DtOutBetwDtFrm = dateOutFrom;
                else if (dateOutTo != null)
                    DtOutBetwDtTo = dateOutTo;

                SetSortAndBindGrid(GridBindAction.Searched,null,null);//BindGridDetails(null);

                //obj = null;
                //Application.Current.Properties["SearchCmd"] = "Searched";
                // DelegateEventVehicle.OnSetValueEvtSearch -= new DelegateEventVehicle.SetValueSearch(GetSearchCommand);
                //return;
            }
        }
        /// <summary>
        /// Method TabChange to show hide the tab on page.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>13-mar-2016</createddate>
        private void TabChange(int tabIndex)
        {
            if (tabIndex == (int)Enums.PortStorageTabs.VehicleDetail)
            {
                SeletedTabFinduser = (int)Enums.PortStorageTabs.VehicleDetail;
                VisibilityEditUserTab = true;
            }
            else
            {
                VisibilityEditUserTab = false;
                SeletedTabFinduser = (int)Enums.PortStorageTabs.VehicleLocator;
            }
        }

        /// <summary>
        /// SortColumn of Vehicle Grid
        /// </summary>
        private string _gridSortColumn;
        public string GridSortColumn
        {
            get
            {
                return _gridSortColumn;
            }
            set
            {
                _gridSortColumn = value;
                NotifyPropertyChanged("GridSortColumn");
            }
        }

        /// <summary>
        /// SortOrder of Vehicle Grid
        /// </summary>
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
                NotifyPropertyChanged("GridSortOrder");
            }
        }


        private SortingState _sortState;
        public SortingState SortState
        {
            get
            {
                return _sortState;
            }
            set
            {
                _sortState = value;
                NotifyPropertyChanged("SortState");
            }
        }


        /// <summary>
        /// property for tab index details
        /// </summary>
        private int seletedTabFinduser;
        public int SeletedTabFinduser
        {
            get { return seletedTabFinduser; }
            set
            {
                if (value != null)
                {
                    this.seletedTabFinduser = value;
                    NotifyPropertyChanged("SeletedTabFinduser");
                }
            }
        }

        /// <summary>
        /// property for tab show hide
        /// </summary>
        private bool displayFindUserTab;
        public bool DisplayFindUserTab
        {
            get { return this.displayFindUserTab; }
            set
            {
                this.displayFindUserTab = value;
                NotifyPropertyChanged("DisplayFindUserTab");
            }
        }

        /// <summary>
        /// property for tab show hide
        /// </summary>
        private bool displayEditUserTab;
        public bool DisplayEditUserTab
        {
            get { return this.displayEditUserTab; }
            set
            {
                this.displayEditUserTab = value;
                NotifyPropertyChanged("DisplayEditUserTab");
            }
        }

        /// <summary>
        /// for holding and resturning the value of usercode
        /// </summary>
        private bool visibilityEditUserTab;
        public bool VisibilityEditUserTab
        {
            get { return visibilityEditUserTab; }
            set
            {
                this.visibilityEditUserTab = value;
                NotifyPropertyChanged("VisibilityEditUserTab");
            }
        }

        /// <summary>
        /// Method NotificationMessageReceived to perform the delegate event.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>13-mar-2016</createddate>
        internal void NotificationMessageReceived(string msg)
        {
            Vin = msg;
            vin = msg;
        }

        /// <summary>
        /// property for holding the selected tab index value
        /// </summary>
        private int selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return selectedTabIndex; }
            set
            {
                selectedTabIndex = value;
                NotifyPropertyChanged("SelectedTabIndex");
            }
        }

        /// <summary>
        /// property for holding selected tab index value
        /// </summary>
        private bool vehicalLocatorTab;
        public bool VehicalLocatorTab
        {
            get { return vehicalLocatorTab; }
            set
            {
                vehicalLocatorTab = value;
                NotifyPropertyChanged("VehicalLocatorTab");
            }
        }

        /// <summary>
        /// property for Vehicle detail
        /// </summary>
        private bool vehicleDetailsTab;
        public bool VehicleDetailsTab
        {
            get { return vehicalLocatorTab; }
            set
            {
                vehicleDetailsTab = value;
                NotifyPropertyChanged("VehicleDetailsTab");
            }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                NotifyPropertyChanged("IsActive");
            }
        }

        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set
            {
                customerID = value;
                NotifyPropertyChanged("CustomerID");
            }
        }

        #region multi select

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

        private bool chkSelected;
        public bool ChkSelected
        {
            get { return chkSelected; }
            set
            {
                chkSelected = value;
                NotifyPropertyChanged("ChkSelected");
            }
        }
        #endregion

        #region "Page Properties"

        private string vin;
        public string Vin
        {
            get { return vin; }
            set
            {
                if (value != null)
                {
                    this.vin = value;
                    NotifyPropertyChanged("Vin");
                }
            }
        }

        private string year;
        public string Year
        {
            get { return year; }
            set
            {
                this.year = value;
                NotifyPropertyChanged("Year");
                if (value != null)
                {
                    if (value.Length >= 4 && value.Length <= 20)
                    {
                    }
                    else
                    {
                        // ClsValidationPopUp.ErrMsgPassword = Resources.ErrorCredentialLenght;
                    }
                }
            }
        }

        private string make;
        public string Make
        {
            get { return make; }
            set
            {
                this.make = value;
                NotifyPropertyChanged("Make");
                if (value != null)
                {
                    if (value.Length >= 4 && value.Length <= 20)
                    {
                    }
                    else
                    {
                        // ClsValidationPopUp.ErrMsgPassword = Resources.ErrorCredentialLenght;
                    }
                }
            }
        }

        private string model;
        public string Model
        {
            get { return model; }
            set
            {
                this.model = value;
                NotifyPropertyChanged("Model");
                if (value != null)
                {
                    if (value.Length >= 4 && value.Length <= 20)
                    {
                    }
                    else
                    {
                        // ClsValidationPopUp.ErrMsgPassword = Resources.ErrorCredentialLenght;
                    }
                }
            }
        }

        private string customerNo;
        public string CustomerNumber
        {
            get { return customerNo; }
            set
            {
                this.customerNo = value;
                NotifyPropertyChanged("CustomerNumber");
                if (value != null)
                {
                    if (value.Length >= 4 && value.Length <= 20)
                    {
                    }
                    else
                    {
                        // ClsValidationPopUp.ErrMsgPassword = Resources.ErrorCredentialLenght;
                    }
                }
            }
        }

        private string bayLocation;
        public string BayLocation
        {
            get { return bayLocation; }
            set
            {
                this.bayLocation = value;
                NotifyPropertyChanged("BayLocation");
                if (value != null)
                {
                    if (value.Length >= 4 && value.Length <= 20)
                    {
                    }
                    else
                    {
                        // ClsValidationPopUp.ErrMsgPassword = Resources.ErrorCredentialLenght;
                    }
                }
            }
        }

        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                this.customerName = value;
                NotifyPropertyChanged("CustomerName");
                if (value != null)
                {
                    if (value.Length >= 4 && value.Length <= 20)
                    {
                    }
                    else
                    {
                        //ClsValidationPopUp.ErrMsgPassword = Resources.ErrorCredentialLenght;
                    }
                }
            }
        }

        private Nullable<DateTime> inBetwDtFrm;
        public Nullable<DateTime> InBetwDtFrm
        {
            get { return inBetwDtFrm; }
            set
            {
                this.inBetwDtFrm = value;
                NotifyPropertyChanged("InBetwDtFrm");
                if (value != null)
                {
                }
            }
        }

        private Nullable<DateTime> inBetwDtTo;
        public Nullable<DateTime> InBetwDtTo
        {
            get { return inBetwDtTo; }
            set
            {
                this.inBetwDtTo = value;
                NotifyPropertyChanged("InBetwDtTo");
            }
        }

        private string vehicleStatus;
        public string VehicleStatus
        {
            get { return vehicleStatus; }
            set
            {
                this.vehicleStatus = value;
                NotifyPropertyChanged("VehicleStatus");
            }
        }

        private Nullable<DateTime> dtInBetwDtFrm;
        public Nullable<DateTime> DtInBetwDtFrm
        {
            get { return dtInBetwDtFrm; }
            set
            {
                this.dtInBetwDtFrm = value;
                NotifyPropertyChanged("DtInBetwDtFrm");
            }
        }

        private Nullable<DateTime> dtInBetwDtTo;
        public Nullable<DateTime> DtInBetwDtTo
        {
            get { return dtInBetwDtTo; }
            set
            {
                this.dtInBetwDtTo = value;
                NotifyPropertyChanged("DtInBetwDtTo");
            }
        }


        private string invoiceNumber;
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set
            {
                this.invoiceNumber = value;
                NotifyPropertyChanged("InvoiceNumber");
            }
        }

        private Nullable<DateTime> dtRequestBetwDtFrm;
        public Nullable<DateTime> DtRequestBetwDtFrm
        {
            get { return dtRequestBetwDtFrm; }
            set
            {
                this.dtRequestBetwDtFrm = value;
                NotifyPropertyChanged("DtRequestBetwDtFrm");
            }
        }

        private string custIndent;
        public string CustIndent
        {
            get { return custIndent; }
            set
            {
                this.custIndent = value;
                NotifyPropertyChanged("CustIndent");
            }
        }

        private Nullable<DateTime> dtRequestBetwDtTo;
        public Nullable<DateTime> DtRequestBetwDtTo
        {
            get { return dtRequestBetwDtTo; }
            set
            {
                this.dtRequestBetwDtTo = value;
                NotifyPropertyChanged("DtRequestBetwDtTo");
            }
        }


        private Nullable<DateTime> dtOutBetwDtFrm;
        public Nullable<DateTime> DtOutBetwDtFrm
        {
            get { return dtOutBetwDtFrm; }
            set
            {
                this.dtOutBetwDtFrm = value;
                NotifyPropertyChanged("DtOutBetwDtFrm");
            }
        }

        private Nullable<DateTime> dtOutBetwDtTo;
        public Nullable<DateTime> DtOutBetwDtTo
        {
            get { return dtOutBetwDtTo; }
            set
            {
                this.dtOutBetwDtTo = value;
                NotifyPropertyChanged("DtOutBetwDtTo");
            }

        }


        private decimal? entryRate;
        public decimal? EntryRate
        {
            get { return entryRate; }
            set
            {
                this.entryRate = value;
                NotifyPropertyChanged("EntryRate");
                if (value != null)
                {
                }
            }
        }

        private int? perDiemGraceDays;
        public int? PerDiemGraceDays
        {
            get { return perDiemGraceDays; }
            set
            {
                this.perDiemGraceDays = value;
                NotifyPropertyChanged("PerDiemGraceDays");
                if (value != null)
                {
                }
            }
        }

        private decimal? totalCharge;
        public decimal? TotalCharge
        {
            get { return totalCharge; }
            set
            {
                this.totalCharge = value;
                NotifyPropertyChanged("TotalCharge");

            }
        }
        private DateTime creationDate;
        public DateTime CreationDate
        {
            get { return creationDate; }
            set
            {
                this.creationDate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }

        private DateTime updatedDate;
        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set
            {
                this.updatedDate = value;
                NotifyPropertyChanged("UpdatedDate");
            }
        }
        private DateTime estimatedPickupDate;
        public DateTime EstimatedPickupDate
        {
            get { return estimatedPickupDate; }
            set
            {
                this.estimatedPickupDate = value;
                NotifyPropertyChanged("EstimatedPickupDate");
            }
        }

        private string createdBy;
        public string CreatedBy
        {
            get { return createdBy; }
            set
            {
                this.createdBy = value;
                NotifyPropertyChanged("CreatedBy");
            }
        }

        private string updatedBy;
        public string UpdatedBy
        {
            get { return updatedBy; }
            set
            {
                this.updatedBy = value;
                NotifyPropertyChanged("UpdatedBy");
            }
        }


        private string lastPhysicalDate;
        public string LastPhysicalDate
        {
            get { return lastPhysicalDate; }
            set
            {
                this.lastPhysicalDate = value;
                NotifyPropertyChanged("LastPhysicalDate");
            }
        }

        private int vehicleId;
        public int VehicleId
        {
            get { return vehicleId; }
            set
            {
                if (value != null)
                {
                    this.vehicleId = value;
                    NotifyPropertyChanged("VehicleId");
                }
            }
        }
        private long totalPageCount;
        public long TotalPageCount
        {
            get { return totalPageCount; }
            set
            {
                this.totalPageCount = value;
                NotifyPropertyChanged("TotalPageCount");
            }
        }

        private int currentPageIndex;
        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                this.currentPageIndex = value;
            }
        }

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

        private bool isTaskCompleted = true;
        public bool IsTaskCompleted
        {
            get { return isTaskCompleted; }
            set
            {
                this.isTaskCompleted = value;
                NotifyPropertyChanged("IsTaskCompleted");

            }
        }

        #endregion

        #region "Page Events"
        private AppWorxCommand _btnSubmit_Click;

        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand btnSubmit_Click
        {
            get
            {
                if (_btnSubmit_Click == null)
                {
                    _btnSubmit_Click = new AppWorxCommand(this.BindGridDetails);
                }
                return _btnSubmit_Click;
            }
        }

        private AppWorxCommand _btnClear_Click;
        public AppWorxCommand btnClear_Click
        {
            get
            {
                if (_btnClear_Click == null)
                {
                    _btnClear_Click = new AppWorxCommand(this.Clear);
                }
                return _btnClear_Click;
            }
        }

        /// <summary>
        /// Submit button event
        /// </summary>
        private ICommand btnMultiVinClick;
        public ICommand btnSubmit2_Click
        {
            get
            {
                if (btnMultiVinClick == null)
                {
                    btnMultiVinClick = new AppWorxCommand(
                        param => this.OpenMultiVin(),
                        param => CanCheck
                        );
                }
                return btnMultiVinClick;
            }
        }

        private ICommand btnExport_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnExport_Click
        {
            get
            {
                if (btnExport_Click == null)
                {
                    btnExport_Click = new AppWorxCommand(
                        param => this.CreateCSVFile(),
                        param => CanCheck
                        );
                }
                return btnExport_Click;
            }
        }

        public AppWorxCommand btnContinue_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
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

        public AppWorxCommand btnCancel_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnCancel_Click
        {
            get
            {
                if (btnCancel_Click == null)
                {

                    btnCancel_Click = new AppWorxCommand(this.Cancel);
                }
                return btnCancel_Click;
            }
        }
        /// <summary>
        /// Bind data on row selection 
        /// </summary>
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
        private AppWorxCommand btnClearValue_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnClearValue_Click
        {
            get
            {
                if (btnClearValue_Click == null)
                {
                    btnClearValue_Click = new AppWorxCommand(this.Clear);
                }
                return btnClearValue_Click;
            }
        }

        private AppWorxCommand btnCleaAll_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnClearAll_Click
        {
            get
            {
                if (btnCleaAll_Click == null)
                {
                    btnCleaAll_Click = new AppWorxCommand(this.Clear);
                }
                return btnCleaAll_Click;
            }
        }

        public static bool CanCheck
        {
            get
            {
                return true;
            }
        }
        #endregion

        private ObservableCollection<VehicleInfo> vehicalListInfo;
        public ObservableCollection<VehicleInfo> VehicalListInfo
        {
            get { return vehicalListInfo; }
            private set
            {
                vehicalListInfo = value;
                NotifyPropertyChanged("VehicalListInfo");
            }
        }

        private ObservableCollection<VehicleModel> vehicleList;
        public ObservableCollection<VehicleModel> VehicleList
        {
            get { return vehicleList; }
            private set
            {
                vehicleList = value;
                NotifyPropertyChanged("VehicleList");
            }
        }

        /// <summary>
        /// Get Total page count for grid
        /// </summary>
        /// 
        public void GetTotalPageCount(long totalPageCount)
        {
            TotalPageCount = totalPageCount;
        }

        public void GetCurrentPageIndex(int currentPageIndex)
        {
            CurrentPageIndex = currentPageIndex;

            if (CurrentPageIndex > 0)
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "calling the service ", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                SetSortAndBindGrid(GridBindAction.Scrolled, null, null);//BindGridDetails("GridScrolled");
            }
        }

        /// <summary>
        /// Method OpenMultiVin to open the multi VIN popup.
        /// </summary>
        /// <param name="NA"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        public void OpenMultiVin()
        {
            bool IsWindowExists = false;
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.Title == "Multi VIN")
                    {
                        IsWindowExists = true;
                        window.Topmost = true;
                        break;
                    }
                }
                if (!IsWindowExists)
                {
                    MultiVIN selctor = new MultiVIN(Vin);
                    selctor.Owner = Application.Current.MainWindow;
                    selctor.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                CommonSettings.logger.LogError(this.GetType(), ex);

            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Method BindGridDetails to bind the grid.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        private async void BindGridDetails(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (obj == null)
                {
                    IsTaskCompleted = false;
                    CurrentPageIndex = 0;
                }
                if (CurrentPageIndex == 0)
                {
                    Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
                    Application.Current.Properties["FindUserGridCurrentPageIndex"] = 0;
                }

                // creating the object of service property
                AppWorksService.Properties.VehicleProp objVehicleProp = new AppWorksService.Properties.VehicleProp();

                objVehicleProp.Vin = !string.IsNullOrEmpty(Vin) ? Vin : string.Empty;

                objVehicleProp.CurrentPageIndex = CurrentPageIndex;
                objVehicleProp.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize;

                if (string.IsNullOrEmpty(GridSortColumn))
                {
                    GridSortColumn = VehicleLocatorGridColumns.VIN;
                    GridSortOrder = SortingState.Ascending.ToString();
                }

                objVehicleProp.SortColumn = GridSortColumn;
                objVehicleProp.SortOrder = GridSortOrder;

                objVehicleProp.DefaultPageSize = _GridPageSize;
                objVehicleProp.Year = Year;
                objVehicleProp.Make = Make;
                objVehicleProp.Model = Model;
                objVehicleProp.CustomerNumber = CustomerNumber;
                objVehicleProp.BayLocation = BayLocation;
                objVehicleProp.CustomerName = CustomerName;
                //if (InBetwDtFrm != null && InBetwDtTo != null)
                //{
                if (InBetwDtFrm > InBetwDtTo)
                {
                    MessageBox.Show(Resources.msgDateCompair);
                    return;
                }
                //}
                objVehicleProp.InBetwDtFrm = InBetwDtFrm;
                if (InBetwDtTo != null)
                    objVehicleProp.InBetwDtTo = InBetwDtTo.Value.AddDays(1);


                if (SelectedVehicalStatus != Resources.MsgAll)
                {
                    objVehicleProp.VehicleStatus = SelectedVehicalStatus;
                }

                if (DtInBetwDtFrm > DtInBetwDtTo)
                {
                    MessageBox.Show(Resources.msgDateCompair);
                    return;
                }
                objVehicleProp.DtInBetwDtFrm = DtInBetwDtFrm;

                if (DtInBetwDtTo != null)
                    objVehicleProp.DtInBetwDtTo = DtInBetwDtTo;

                objVehicleProp.InvoiceNumber = InvoiceNumber;

                if (DtRequestBetwDtFrm > DtRequestBetwDtTo)
                {
                    MessageBox.Show(Resources.msgDateCompair);
                    return;
                }

                objVehicleProp.DtRequestBetwDtFrm = DtRequestBetwDtFrm;
                if (DtRequestBetwDtTo != null)
                    objVehicleProp.DtRequestBetwDtTo = DtRequestBetwDtTo.Value.AddDays(1);

                if (DtOutBetwDtFrm > DtOutBetwDtTo)
                {
                    MessageBox.Show(Resources.msgDateCompair);
                    return;
                }

                objVehicleProp.DtOutBetwDtFrm = DtOutBetwDtFrm;
                if (DtOutBetwDtTo != null)
                    objVehicleProp.DtOutBetwDtTo = DtOutBetwDtTo.Value.AddDays(1);
                objVehicleProp.CustIndent = CustIndent;
                objVehicleProp.EntryRate = EntryRate;
                objVehicleProp.PerDiemGraceDays = PerDiemGraceDays;
                objVehicleProp.TotalCharge = TotalCharge;

                var data = await _serviceInstance.GetVehicleSearchDetailsAsync(objVehicleProp);


                IEnumerable<VehicleModel> vehicleModel = data.Select(d => new VehicleModel
                {
                    CustomerID = d.CustomerID,
                    VehiclesID = d.VehiclesID,
                    Vin = d.Vin,
                    Name = d.Name,
                    MakeModel = d.MakeModel,
                    BayLocation = d.BayLocation,
                    DateIn = d.DateIn,
                    DateRequested = d.DateRequested,
                    DateOut = d.DateOut,
                    VehicleStatus = d.VehicleStatus,
                    Year = d.Year,
                    EntryRate = d.EntryRate,
                    PerDiemGraceDays = d.PerDiemGraceDays,
                    TotalCharge = d.TotalCharge,
                    LastPhysicalDate = d.LastPhysicalDate,
                    CreationDate = d.CreationDate,
                    UpdatedDate = d.UpdatedDate,
                    EstimatedPickupDate = d.EstimatedPickupDate,
                    CreatedBy = d.CreatedBy,
                    UpdatedBy = d.UpdatedBy,
                    //Days = d.DateOut != null ? Convert.ToInt32(Convert.ToDateTime(d.DateOut).Subtract((DateTime)d.DateIn).TotalDays) : (d.DateIn != null ? Convert.ToInt32(DateTime.Now.Subtract((DateTime)d.DateIn).TotalDays) : 0),
                    Days = d.DateOut != null ? (Convert.ToDateTime(d.DateOut) - ((DateTime)d.DateIn)).Days + 1 : (d.DateIn != null ? (DateTime.Now - ((DateTime)d.DateIn)).Days + 1 : 0),
                    TotalPageCount = d.TotalPageCount,
                    DefaultEntryRate = d.DefaultEntryRate,
                    DefaultPerDiem = d.DefaultPerDiem,
                    DefaultPerDiemGraceDays = d.DefaultPerDiemGraceDays
                });

                await App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (CurrentPageIndex == 0)
                    {
                        VehicleList = null;
                        VehicleList = new ObservableCollection<VehicleModel>(vehicleModel);
                        IsTaskCompleted = true;
                    }
                    else
                    {
                        if (VehicleList != null && VehicleList.Count > 0)
                        {
                            foreach (VehicleModel ud in new ObservableCollection<VehicleModel>(vehicleModel))
                            {
                                VehicleList.Add(ud);
                                IsTaskCompleted = true;
                            }
                        }
                    }

                    if (VehicleList != null && VehicleList.Count > 0)
                    {
                        Count = VehicleList.ToList().Where(x => x.TotalPageCount > 0).FirstOrDefault().TotalPageCount;
                    }
                    else
                    {
                        Count = 0;
                    }
                }));

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
        /// function CreateCSVFile to return the exported csv file
        /// </summary>
        /// <param name="Na"></param>
        /// <param name="Na"></param>
        /// <returns>csv file</returns>
        public void CreateCSVFile()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (VehicleList != null)
                {
                    AppWorksService.Properties.VehicleProp objVehicleProp = new AppWorksService.Properties.VehicleProp();
                    if (vin != null)
                    {
                        objVehicleProp.Vin = Vin;
                    }
                    else
                    {
                        objVehicleProp.Vin = string.Empty;
                    }
                    objVehicleProp.CurrentPageIndex = 0;
                    objVehicleProp.PageSize = 0;
                    objVehicleProp.Year = Year;
                    objVehicleProp.Make = Make;
                    objVehicleProp.Model = Model;
                    objVehicleProp.CustomerNumber = CustomerNumber;
                    objVehicleProp.BayLocation = BayLocation;
                    objVehicleProp.CustomerName = CustomerName;
                    objVehicleProp.InBetwDtFrm = InBetwDtFrm;
                    objVehicleProp.InBetwDtTo = InBetwDtTo;
                    if (SelectedVehicalStatus != Resources.MsgAll)
                    {
                        objVehicleProp.VehicleStatus = SelectedVehicalStatus;
                    }
                    objVehicleProp.DtInBetwDtFrm = DtInBetwDtFrm;
                    objVehicleProp.DtInBetwDtTo = DtInBetwDtTo;
                    objVehicleProp.InvoiceNumber = InvoiceNumber;
                    objVehicleProp.DtRequestBetwDtFrm = DtRequestBetwDtFrm;
                    objVehicleProp.CustIndent = CustIndent;
                    objVehicleProp.DtRequestBetwDtTo = DtRequestBetwDtTo;
                    objVehicleProp.DtOutBetwDtFrm = DtOutBetwDtFrm;
                    objVehicleProp.DtOutBetwDtTo = DtOutBetwDtTo;
                    objVehicleProp.EntryRate = EntryRate;
                    objVehicleProp.PerDiemGraceDays = PerDiemGraceDays;
                    objVehicleProp.TotalCharge = TotalCharge;
                    var data = _serviceInstance.GetVehicleSearchDetails(objVehicleProp).Select(d => new VehicleModel
                    {
                        VehiclesID = d.VehiclesID,
                        Vin = d.Vin,
                        Name = d.Name,
                        MakeModel = d.MakeModel,
                        BayLocation = d.BayLocation,
                        DateIn = d.DateIn,
                        DateRequested = d.DateRequested,
                        DateOut = d.DateOut,
                        VehicleStatus = d.VehicleStatus,
                        Year = d.Year,
                        LastPhysicalDate = d.LastPhysicalDate,
                        CreationDate = d.CreationDate,
                        UpdatedDate = d.UpdatedDate,
                        EstimatedPickupDate = d.EstimatedPickupDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy,
                        Days = d.DateOut != null ? Convert.ToInt32(Convert.ToDateTime(d.DateOut).Subtract((DateTime)d.DateIn).TotalDays) : (d.DateIn != null ? Convert.ToInt32(DateTime.Now.Subtract((DateTime)d.DateIn).TotalDays) : 0),
                        TotalPageCount = d.TotalPageCount,
                        EntryRate = d.EntryRate,
                        PerDiemGraceDays = d.PerDiemGraceDays,
                        TotalCharge = d.TotalCharge

                    });
                    VehicleList = null;
                    VehicleList = new ObservableCollection<VehicleModel>(data);
                    if (VehicleList != null && VehicleList.Count > 0)
                    {
                        var TotalPageCounttemp = VehicleList.FirstOrDefault().TotalPageCount;
                        DelegateEventVehicle.SetValueMethodTotalCountPager(TotalPageCounttemp);
                    }
                    if (VehicleList.Count > 0)
                    {
                        string[] ExportValue = new string[] { "Customer", "VIN", "Make/Model", "Bay Location", "Date In", "Date Requested", "Date Out", "Storage Days", "Vehicle Status" };
                        if (!Directory.Exists(ConfigurationManager.AppSettings["CSVFilePath"].ToString()))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(ConfigurationManager.AppSettings["CSVFilePath"].ToString()));
                        }
                        StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "PortStorageVehicles.csv", false);

                        foreach (string objVal in ExportValue)
                        {
                            sw.Write(objVal);
                            sw.Write(",");
                        }
                        sw.Write(sw.NewLine);

                        // Row creation
                        foreach (VehicleModel prop in VehicleList)
                        {
                            if (prop.Name != null)
                            {
                                sw.Write(prop.Name);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.Vin != null)
                            {
                                sw.Write(prop.Vin);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.MakeModel != null)
                            {
                                sw.Write(prop.MakeModel);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.BayLocation != null)
                            {
                                sw.Write(prop.BayLocation);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.DateIn != null)
                            {
                                sw.Write(prop.DateIn.Value.Date.ToString("MM/dd/yyy"));
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.DateRequested != null)
                            {
                                sw.Write(prop.DateRequested.Value.Date.ToString("MM/dd/yyy"));
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.DateOut != null)
                            {
                                sw.Write(prop.DateOut.Value.Date.ToString("MM/dd/yyy"));
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }

                            if (prop.Days != null)
                            {
                                sw.Write(prop.Days);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }

                            if (prop.VehicleStatus != null)
                            {
                                sw.Write(prop.VehicleStatus);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            // Do something with propValue
                            sw.Write(sw.NewLine);
                        }
                        sw.Close();
                        System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "PortStorageVehicles.csv");
                    }
                    else
                    {
                        MessageBox.Show(Resources.msgExportResultError);
                    }
                }
                else
                {
                    MessageBox.Show(Resources.msgExportResultError);
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
        /// Used to fill the vehicle collection
        /// </summary>
        /// <param name="selectedItems"></param>
        public static List<VehicleLocatorVM> GetSelectedVehiclesList(ObservableCollection<object> selectedItems)
        {
            List<VehicleLocatorVM> objList = new List<VehicleLocatorVM>();
            foreach (VehicleModel Vl in selectedItems)
            {
                VehicleLocatorVM objVehicalLocator = new VehicleLocatorVM(null);
                objVehicalLocator.Vin = Vl.Vin;
                objVehicalLocator.VehicleId = Convert.ToInt32(Vl.VehiclesID);
                objVehicalLocator.Make = Vl.MakeModel;
                objVehicalLocator.CustomerName = Vl.Name;
                objList.Add(objVehicalLocator);
            }
            return objList;
        }

        /// <summary>
        /// Method Continue to get the details of selected item.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        public void Continue(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (SelectedItems.Count > 0)
                {
                    var list = GetSelectedVehiclesList(SelectedItems);
                    DelegateEventVehicle.SetValueListMethod(list);
                    DelegateEventVehicle.SetValueMethodTab((int)Enums.PortStorageTabs.VehicleDetail);
                    DelegateEventVehicle.SetValueMethodTabEnable(false);
                    DelegateEventVehicle.SetValueMethodCmd("Edit");
                    VehicleList = null;
                    Clear(null);
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
        /// Method cancel to get cancel the search
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        public void Cancel(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                DelegateEventVehicle.SetValueMethodCmd("Cancel");
                VehicleList = null;
                Clear(null);
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
        /// Method Clear to refresh the form fields.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        public void Clear(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                Vin = string.Empty;
                Year = string.Empty;
                Make = string.Empty;
                Model = string.Empty;
                CustomerNumber = string.Empty;
                BayLocation = string.Empty;
                CustomerName = string.Empty;
                InBetwDtFrm = null;
                InBetwDtTo = null;
                VehicleStatus = string.Empty;
                DtInBetwDtFrm = null;
                DtInBetwDtTo = null;
                InvoiceNumber = string.Empty;
                DtRequestBetwDtFrm = null;
                CustIndent = string.Empty;
                DtRequestBetwDtTo = null;
                DtOutBetwDtFrm = null;
                DtOutBetwDtTo = null;
                VehicleList = null;
                EntryRate = null;
                PerDiemGraceDays = null;
                TotalCharge = null;
                FillTVehicalRecordStatus(null);
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

        private List<string> vehicalStatus;
        public List<string> VehicalStatusList
        {
            get { return vehicalStatus; }
            private set
            {
                vehicalStatus = value;
                NotifyPropertyChanged("VehicalStatusList");
            }
        }

        private string selectedVehicalStatus;
        public string SelectedVehicalStatus
        {
            get { return selectedVehicalStatus; }
            set
            {
                selectedVehicalStatus = value;
                NotifyPropertyChanged("SelectedVehicalStatus");
            }
        }

        /// <summary>
        /// Method FillTVehicalRecordStatus to fill the vehicle status dropdown.
        /// </summary>
        /// <param name="NA"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        public void FillTVehicalRecordStatus(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                List<string> List = new List<string>();
                VehicalStatusList = null;
                List = _serviceInstance.VehicalStatusList().Where(x => x != null).ToList();
                List.Insert(0, "All");
                VehicalStatusList = List;
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
        /// Function to fill Customer Data on row selection.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 10,2016</createdOn>
        private async void FillData(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (SelectedItems.Count > 0)
                {
                    var list = GetSelectedVehiclesList(SelectedItems);
                    await Task.Run(() =>
                    {
                        DelegateEventVehicle.SetValueListMethod(list);
                        DelegateEventVehicle.SetValueMethodCmd("Edit");
                    });
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DelegateEventVehicle.OnSetValueEvt -= new DelegateEventVehicle.SetValue(NotificationMessageReceived);
                    DelegateEventVehicle.OnSetValueEvtCTabmd -= new DelegateEventVehicle.SetValueTab(TabChange);
                    DelegateEventVehicle.OnSetValueEvtTotalCountPagerCmd -= new DelegateEventVehicle.SetValueTotalCountPager(GetTotalPageCount);
                    DelegateEventVehicle.OnSetValuePageNumberCmd -= new DelegateEventVehicle.SetValuePageNumberClick(GetCurrentPageIndex);
                    DelegateEventVehicle.OnSetValueEvtRefreshCmd -= new DelegateEventVehicle.SetValueRefreshCmd(GetCommandName);
                    DelegateEventVehicle.OnSetValueEvtSearch -= new DelegateEventVehicle.SetValueSearch(GetSearchCommand);
                    DelegateEventVehicle.OnGridSorted -= new DelegateEventVehicle.SetGridSorting(GridSorting);
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class VehicleInfo
    {
        public string VehiclesIDInfo { get; set; }
        public string VinInfo { get; set; }
        public string YearInfo { get; set; }
        public string NameInfo { get; set; }
        public string MakeModelInfo { get; set; }
        public string BayLocationInfo { get; set; }
        public Nullable<System.DateTime> DateInInfo { get; set; }
        public Nullable<System.DateTime> DateRequestedInfo { get; set; }
        public Nullable<System.DateTime> DateOutInfo { get; set; }
        public Nullable<System.DateTime> DateInDateOutInfo { get; set; }
        public string VehicleStatusInfo { get; set; }
        public int VecihleValInfo { get; set; }
    }

    enum GridBindAction
    {
        Sorted,
        Scrolled,
        Searched
    }
}
