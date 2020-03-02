using AppWorks.UI.Common;
using AppWorks.UI.Controls.Attributes;
using AppWorks.UI.Properties;
using AppWorks.UI.View.Billing;
using AppWorks.UI.View.UserControls.Vehicle;
using AppWorks.UI.ViewModel.Vehicle;
using AppWorksService.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace AppWorks.UI.ViewModel.Billing
{
    public class AddEditBillingVM : ChangeTrackingViewModel, IDisposable
    {
        int _GridPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]);
        int _GridPageSizeOnScroll = Convert.ToInt32(ConfigurationManager.AppSettings["FindUserGridPageSizeOnScroll"]);

        string NEXT_INVOICE_NUMBER = "NextInvoiceNumber";

        public AddEditBillingVM()
        {
            EnabledNewUser = true;
            EnabledFind = true;
            EnabledPrint = true;
            EnabledCancel = false;
            EnabledModifyDelete = false;
            EnabledPrevNext = false;
            EnabledSaveUpdate = false;
            IsEditable = false;
            Action = Resources.ActionSave;
            StorageHandling = 0.00;
            TotalInvoice = 0.00;
            TotalPaid = 0.00;
            CreditsIssued = 0.00;
            BalanceDue = 0.00;
            TotalUnits = 0;

            //TransportCharges = 0.00;
            //DueFromBrokers = 0.00;
            //DueToBrokers = 0.00;
            //FuelSC = 0.00;
            //FuelSCRate = 0.00;
            //fuelSCRateInd = false;
            //FuelSCInd = false;

            DelegateEventCustomer.OnSetCustomerValueEvt += new DelegateEventCustomer.SetCustomerValue(GetCustomerValue);
            DelegateEventBillingPeriod.OnSetValueListEvt += new DelegateEventBillingPeriod.SetValueList(NotificationMessageReceivedList);
            DelegateEventVehicle.OnSetValueEvtRefreshGridCmd += new DelegateEventVehicle.SetValueRefreshGrid(RefreshGrid);
            DelegateEventBillingPeriod.OnSetValuePageNumberInvoiceCmd += new DelegateEventBillingPeriod.SetValuePageNumberInvoiceClick(GetCurrentPageIndex);
            DelegateEventBillingPeriod.OnSetValuePageNumberLineItemCmd += new DelegateEventBillingPeriod.SetValuePageNumberLineItemClick(GetCurrentLinePageIndex);
            AcceptChanges();
        }

        public int currentindex = 0;
        List<string> listBIlling = new List<string>();
        public void NotificationMessageReceivedList(List<BillingFindVM> Listpo)
        {


            listBIlling = new List<string>();
            for (int i = 0; i < Listpo.Count; i++)
            {
                listBIlling.Add(Listpo[i].BillingID.ToString());
                CustomerName = Listpo[i].CustomerName;
                InvoiceNumber = Listpo[i].InvoiceNumber;
                CustomerNumber = Listpo[i].CustomerNumber;
                InvoiceDate = Listpo[i].InvoiceDate;
                CustomerAddress = Listpo[i].Address;
                EnabledNewUser = true;
                EnabledFind = true;
                EnabledPrint = true;
                EnabledCancel = false;
                EnabledModifyDelete = true;
                EnabledSaveUpdate = false;
                IsEditable = false;
                Action = Resources.ActionUpdate;

                //Driver = Listpo[i].Driver;

            }
            if (listBIlling.Count > 0)
            {
                int index = listBIlling.Count;
                if (listBIlling.Count() > 0)
                {
                    int billingID = Convert.ToInt32(listBIlling[0]);
                    BillingID = billingID;
                    FillInvoiceData(null);
                    FillInvoicLineItems(null);
                    FillBillingData(billingID);
                    if (listBIlling.Count > 1)
                        EnabledPrevNext = true;
                    else
                        enabledPrevNext = false;
                    currentindex = 0;
                }
            }
            else
            {
                //FillUserDetailsinForm(Listpo[0].BillingID.ToString());
            }
        }

        public void RefreshGrid(string value)
        {
            if (value.ToLower().Equals("refresh"))
            {
                FillInvoiceData(null);
            }
        }


        /// <summary>
        /// for Total Records on end of grid
        /// </summary>
        /// 
        private long count;
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
            FillInvoiceData("GridScroled");
        }
        public void GetCurrentLinePageIndex(int currentPageIndx)
        {
            CurrentPageIndex = currentPageIndx;
            FillInvoicLineItems("GridScroled");
        }

        #region BillingProperties

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


        private bool? enabledNewUser;
        public bool? EnabledNewUser
        {
            get { return enabledNewUser; }
            set
            {

                this.enabledNewUser = value;
                NotifyPropertyChanged("EnabledNewUser");
            }
        }

        private bool enabledModifyDelete;
        public bool EnabledModifyDelete
        {
            get { return enabledModifyDelete; }
            set
            {

                this.enabledModifyDelete = value;
                NotifyPropertyChanged("EnabledModifyDelete");
            }
        }

        private bool enabledSaveUpdate;
        public bool EnabledSaveUpdate
        {
            get { return enabledSaveUpdate; }
            set
            {

                this.enabledSaveUpdate = value;
                NotifyPropertyChanged("EnabledSaveUpdate");
            }
        }

        private bool enabledPrevNext;
        public bool EnabledPrevNext
        {
            get { return enabledPrevNext; }
            set
            {

                this.enabledPrevNext = value;
                NotifyPropertyChanged("EnabledPrevNext");
            }
        }

        private bool isEditable;
        public bool IsEditable
        {
            get { return isEditable; }
            set
            {
                this.isEditable = value;
                NotifyPropertyChanged("IsEditable");
            }
        }

        private string action;
        public string Action
        {
            get { return action; }
            set
            {
                if (value != null)
                {
                    this.action = value;
                    NotifyPropertyChanged("Action");
                }
            }
        }

        private bool enabledFind;
        public bool EnabledFind
        {
            get { return enabledFind; }
            set
            {

                this.enabledFind = value;
                NotifyPropertyChanged("EnabledFind");
            }
        }

        private bool enabledPrint;
        public bool EnabledPrint
        {
            get { return enabledPrint; }
            set
            {

                this.enabledPrint = value;
                NotifyPropertyChanged("EnabledPrint");
            }
        }

        private bool enabledCancel;
        public bool EnabledCancel
        {
            get { return enabledCancel; }
            set
            {

                this.enabledCancel = value;
                NotifyPropertyChanged("EnabledCancel");
            }
        }


        private int? customerID;
        public int? CustomerID
        {
            get { return this.customerID; }
            set
            {
                this.customerID = value;
                NotifyPropertyChanged("CustomerID");
            }
        }

        private int billingID;
        public int BillingID
        {
            get { return this.billingID; }
            set
            {
                this.billingID = value;
                NotifyPropertyChanged("BillingID");
            }
        }

        private string customerName;
        public string CustomerName
        {
            get { return this.customerName; }
            set
            {
                this.customerName = value;
                NotifyPropertyChanged("CustomerName");
            }
        }

        private string invoiceStatus;
        public string InvoiceStatus
        {
            get { return this.invoiceStatus; }
            set
            {
                this.invoiceStatus = value;
                NotifyPropertyChanged("InvoiceStatus");
            }
        }

        private string customerNumber;
        public string CustomerNumber
        {
            get { return this.customerNumber; }
            set
            {
                this.customerNumber = value;
                NotifyPropertyChanged("CustomerNumber");
            }
        }

        private string invoiceNumber;
        public string InvoiceNumber
        {
            get { return this.invoiceNumber; }
            set
            {
                this.invoiceNumber = value;
                NotifyPropertyChanged("InvoiceNumber");
            }
        }

        private string customerAddress;
        public string CustomerAddress
        {
            get { return this.customerAddress; }
            set
            {
                this.customerAddress = value;
                NotifyPropertyChanged("CustomerAddress");
            }
        }

        private string status;
        public string Status
        {
            get { return this.status; }
            set
            {
                this.status = value;
                NotifyPropertyChanged("status");
            }
        }

        private string crossRefNumber;
        public string CrossRefNumber
        {
            get { return this.crossRefNumber; }
            set
            {
                this.crossRefNumber = value;
                NotifyPropertyChanged("crossRefNumber");
            }
        }


        private string invoiceType;
        [ChangeTracking]
        public string InvoiceType
        {
            get { return this.invoiceType; }
            set
            {
                this.invoiceType = value;
                NotifyPropertyChanged("invoiceType");
            }
        }


        private double? storageHandling;
        public double? StorageHandling
        {
            get { return this.storageHandling; }
            set
            {
                this.storageHandling = value;
                NotifyPropertyChanged("StorageHandling");
            }
        }

        private DateTime? invoiceDate;
        [ChangeTracking]
        public DateTime? InvoiceDate
        {
            get { return this.invoiceDate; }
            set
            {
                this.invoiceDate = value;
                NotifyPropertyChanged("InvoiceDate");
            }
        }

        private DateTime? createdDate;
        public DateTime? CreatedDate
        {
            get { return this.createdDate; }
            set
            {
                this.createdDate = value;
                NotifyPropertyChanged("CreatedDate");
            }
        }

        private DateTime? updatedDate;
        public DateTime? UpdatedDate
        {
            get { return this.updatedDate; }
            set
            {
                this.updatedDate = value;
                NotifyPropertyChanged("UpdatedDate");
            }
        }

        private string createdBy;
        public string CreatedBy
        {
            get { return this.createdBy; }
            set
            {
                this.createdBy = value;
                NotifyPropertyChanged("CreatedBy");
            }
        }

        private string updatedBy;
        public string UpdatedBy
        {
            get { return this.updatedBy; }
            set
            {
                this.updatedBy = value;
                NotifyPropertyChanged("updatedBy");
            }
        }

        private string paymentMethod;
        [ChangeTracking]
        public string PaymentMethod
        {
            get { return this.paymentMethod; }
            set
            {
                this.paymentMethod = value;
                NotifyPropertyChanged("PaymentMethod");
            }
        }

        #region PROPERTIES TO BE REMOVED AFTER CODE CLEANUP

        //private double? fuelSCRate;
        //public double? FuelSCRate
        //{
        //    get { return this.fuelSCRate; }
        //    set
        //    {
        //        this.fuelSCRate = value;
        //        NotifyPropertyChanged("FuelSCRate");
        //    }
        //}

        //private bool? fuelSCRateInd;
        //public bool? FuelSCRateInd
        //{
        //    get { return this.fuelSCRateInd; }
        //    set
        //    {
        //        this.fuelSCRateInd = value;
        //        NotifyPropertyChanged("FuelSCRateInd");
        //    }
        //}


        //private double? fuelSC;
        //public double? FuelSC
        //{
        //    get { return this.fuelSC; }
        //    set
        //    {
        //        this.fuelSC = value;
        //        NotifyPropertyChanged("FuelSC");
        //    }
        //}

        //private bool? fuelSCInd;
        //public bool? FuelSCInd
        //{
        //    get { return this.fuelSCInd; }
        //    set
        //    {
        //        this.fuelSCInd = value;
        //        NotifyPropertyChanged("FuelSCInd");
        //    }
        //}

        //private string driver;
        //public string Driver
        //{
        //    get { return this.driver; }
        //    set
        //    {
        //        this.driver = value;
        //        NotifyPropertyChanged("Driver");
        //    }
        //}

        //private double? dueToBrokers;
        //public double? DueToBrokers
        //{
        //    get { return this.dueToBrokers; }
        //    set
        //    {
        //        this.dueToBrokers = value;
        //        NotifyPropertyChanged("DueToBrokers");
        //    }
        //}

        //private double? transportCharges;
        //public double? TransportCharges
        //{
        //    get { return this.transportCharges; }
        //    set
        //    {
        //        this.transportCharges = value;
        //        NotifyPropertyChanged("TransportCharges");
        //    }
        //}

        //private double? dueFromBrokers;
        //public double? DueFromBrokers
        //{
        //    get { return this.dueFromBrokers; }
        //    set
        //    {
        //        this.dueFromBrokers = value;
        //        NotifyPropertyChanged("DueFromBrokers");
        //    }
        //}

        //private string newRunInd;
        //public string NewRunInd
        //{
        //    get { return newRunInd; }
        //    set
        //    {
        //        if (value != null)
        //            newRunInd = value;
        //        NotifyPropertyChanged("NewRunInd");
        //    }
        //}

        //private int? runID;
        //public int? RunID
        //{
        //    get { return runID; }
        //    set
        //    {
        //        if (value != null)
        //            runID = value;
        //        NotifyPropertyChanged("RunID");
        //    }
        //}


        //private bool enableTextFuelScRate;
        //public bool EnableTextFuelScRate
        //{
        //    get
        //    {
        //        return enableTextFuelScRate;
        //    }
        //    private set
        //    {
        //        this.enableTextFuelScRate = value;
        //        NotifyPropertyChanged("EnableTextFuelScRate");
        //    }
        //}

        //private bool enableTextFuelSc;
        //public bool EnableTextFuelSc
        //{
        //    get
        //    {
        //        return enableTextFuelSc;
        //    }
        //    private set
        //    {
        //        this.enableTextFuelSc = value;
        //        NotifyPropertyChanged("EnableTextFuelSc");
        //    }
        //}

        #endregion

        private double? totalInvoice;
        public double? TotalInvoice
        {
            get { return this.totalInvoice; }
            set
            {
                this.totalInvoice = value;
                NotifyPropertyChanged("TotalInvoice");
            }
        }

        private double? totalPaid;
        public double? TotalPaid
        {
            get { return this.totalPaid; }
            set
            {
                this.totalPaid = value;
                NotifyPropertyChanged("TotalPaid");
            }
        }

        private decimal? totalUnits;
        public decimal? TotalUnits
        {
            get { return this.totalUnits; }
            set
            {
                this.totalUnits = value;
                NotifyPropertyChanged("TotalUnits");
            }
        }

        private double? creditsIssued;
        public double? CreditsIssued
        {
            get { return this.creditsIssued; }
            set
            {
                this.creditsIssued = value;
                NotifyPropertyChanged("CreditsIssued");
            }
        }

        private double? balanceDue;
        public double? BalanceDue
        {
            get { return this.balanceDue; }
            set
            {
                this.balanceDue = value;
                NotifyPropertyChanged("BalanceDue");
            }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                if (value != null)
                    code = value;
                NotifyPropertyChanged("Code");
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value != null)
                    description = value;
                NotifyPropertyChanged("Description");
            }
        }
        private string codePayment;
        public string CodePayment
        {
            get { return codePayment; }
            set
            {
                if (value != null)
                    codePayment = value;
                NotifyPropertyChanged("CodePayment");
            }
        }
        private string descriptionPayment;
        public string DescriptionPayment
        {
            get { return descriptionPayment; }
            set
            {
                if (value != null)
                    descriptionPayment = value;
                NotifyPropertyChanged("DescriptionPayment");
            }
        }

        private int? creditedOutInd;
        public int? CreditedOutInd
        {
            get { return creditedOutInd; }
            set
            {
                if (value != null)
                    creditedOutInd = value;
                NotifyPropertyChanged("CreditedOutInd");
            }
        }

        private int? creditMemoInd;
        public int? CreditMemoInd
        {
            get { return creditMemoInd; }
            set
            {
                if (value != null)
                    creditMemoInd = value;
                NotifyPropertyChanged("CreditMemoInd");
            }
        }

        private string comments;
        public string Comments
        {
            get { return comments; }
            set
            {
                if (value != null)
                    comments = value;
                NotifyPropertyChanged("Comments");
            }
        }

        private int? vehicleID;
        public int? VehicleID
        {
            get { return vehicleID; }
            set
            {
                if (value != null)
                    vehicleID = value;
                NotifyPropertyChanged("VehicleID");
            }
        }
        private Telerik.Windows.Controls.GridView.GridViewCell _cellInfo;
        public Telerik.Windows.Controls.GridView.GridViewCell CellInfo
        {
            get { return _cellInfo; }
            set
            {
                _cellInfo = value;
                NotifyPropertyChanged("CellInfo");
            }
        }
        private PortStorageVehicleProp selectedUser;

        public PortStorageVehicleProp SelectedUser
        {
            get { return selectedUser; }
            set
            {
                this.selectedUser = value;
                NotifyPropertyChanged("SelectedUser");
            }
        }

        private List<CodeList> lstPaymentMethod;
        public List<CodeList> LstPaymentMethod
        {
            get
            {
                return lstPaymentMethod;
            }
            private set
            {
                this.lstPaymentMethod = value;
                NotifyPropertyChanged("LstPaymentMethod");
            }
        }
        private ObservableCollection<PortStorageVehicleProp> listInvoiceDetails;
        public ObservableCollection<PortStorageVehicleProp> ListInvoiceDetails
        {
            get
            {
                return listInvoiceDetails;
            }
            private set
            {
                this.listInvoiceDetails = value;
                NotifyPropertyChanged("listInvoiceDetails");
            }
        }

        private ObservableCollection<BillingLineItemsProp> listInvoiceLineItems;
        public ObservableCollection<BillingLineItemsProp> ListInvoiceLineItems
        {
            get
            {
                return listInvoiceLineItems;
            }
            private set
            {
                this.listInvoiceLineItems = value;
                NotifyPropertyChanged("ListInvoiceLineItems");
            }
        }

        private List<CodeList> lstInvoiceType;
        public List<CodeList> LstInvoiceType
        {
            get
            {
                return lstInvoiceType;
            }
            private set
            {
                this.lstInvoiceType = value;
                NotifyPropertyChanged("LstInvoiceType");
            }
        }


        #endregion BillingProperties

        #region Events

        private AppWorxCommand btnNew_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnNew_Click
        {
            get
            {
                if (btnNew_Click == null)
                {
                    btnNew_Click = new AppWorxCommand(this.NewCustomerDetail);
                }
                return btnNew_Click;
            }
        }




        private AppWorxCommand btnFind_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnFind_Click
        {
            get
            {
                if (btnFind_Click == null)
                {
                    btnFind_Click = new AppWorxCommand(this.BtnFindData);
                }
                return btnFind_Click;
            }
        }
        private AppWorxCommand btnNext_Click;
        public AppWorxCommand BtnNext_Click
        {
            get
            {
                if (btnNext_Click == null)
                {
                    btnNext_Click = new AppWorxCommand(this.Next);
                }
                return btnNext_Click;
            }
        }

        private AppWorxCommand btnPri_Click;
        public AppWorxCommand BtnPri_Click
        {
            get
            {
                if (btnPri_Click == null)
                {
                    btnPri_Click = new AppWorxCommand(this.Previous);
                }
                return btnPri_Click;
            }
        }


        private AppWorxCommand btnSave_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnSave_Click
        {
            get
            {
                if (btnSave_Click == null)
                {
                    btnSave_Click = new AppWorxCommand(this.InsertBillingData);
                }
                return btnSave_Click;
            }
        }

        private AppWorxCommand btnUpdate_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnUpdate_Click
        {
            get
            {
                if (btnUpdate_Click == null)
                {
                    btnUpdate_Click = new AppWorxCommand(this.UpdateBillingData);
                }
                return btnUpdate_Click;
            }
        }

        private AppWorxCommand btnDelete_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnDelete_Click
        {
            get
            {
                if (btnDelete_Click == null)
                {
                    btnDelete_Click = new AppWorxCommand(this.DeleteBillingData);
                }
                return btnDelete_Click;
            }
        }

        private AppWorxCommand btnResetExp_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnResetExp_Click
        {
            get
            {
                if (btnResetExp_Click == null)
                {
                    btnResetExp_Click = new AppWorxCommand(this.ResetExportedInd);
                }
                return btnResetExp_Click;
            }
        }

        private AppWorxCommand btnCancel_Click;
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
        private AppWorxCommand btnDoubleClick_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnDoubleClick_Click
        {
            get
            {
                if (btnDoubleClick_Click == null)
                {
                    btnDoubleClick_Click = new AppWorxCommand(this.OpenPortStorageVehicle);
                }
                return btnDoubleClick_Click;
            }
        }

        //private AppWorxCommand btnFuelSCRateInd_Click;
        ///// <summary>
        ///// Submit button event
        ///// </summary>
        //    public AppWorxCommand BtnFuelSCRateInd_Click
        //{
        //    get
        //    {
        //        if (btnFuelSCRateInd_Click == null)
        //        {
        //            btnFuelSCRateInd_Click = new AppWorxCommand(this.FuelSCRateInd_Click);
        //        }
        //        return btnFuelSCRateInd_Click;
        //    }
        //}

        //    private AppWorxCommand btnFuelSCInd_Click;
        //    /// <summary>
        //    /// Submit button event
        //    /// </summary>
        //    public AppWorxCommand BtnFuelSCInd_Click
        //    {
        //        get
        //        {
        //            if (btnFuelSCInd_Click == null)
        //            {
        //                btnFuelSCInd_Click = new AppWorxCommand(this.FuelSCInd_Click);
        //            }
        //            return btnFuelSCInd_Click;
        //        }
        //    }

        #region Events to be removed after code cleanup

        ///// <summary>
        ///// Function to enable fuel rate surcharge
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns>NA</returns>
        ///// <createdBy></createdBy>
        ///// <createdOn>May-7,2016</createdOn>
        //public void FuelSCRateInd_Click(object obj)
        //{
        //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        if ((bool)FuelSCRateInd)
        //            EnableTextFuelScRate = true;
        //        else
        //            EnableTextFuelScRate = false;

        //    }
        //    catch (Exception ex)
        //    {
        //        bool displayErrorOnUI = false;
        //        CommonSettings.logger.LogError(this.GetType(), ex);
        //        if (displayErrorOnUI)
        //        { throw; }
        //    }
        //    finally
        //    {
        //        CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //}

        ///// <summary>
        ///// Function to enable fuel rate surcharge
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns>NA</returns>
        ///// <createdBy></createdBy>
        ///// <createdOn>May-7,2016</createdOn>
        //public void FuelSCInd_Click(object obj)
        //{
        //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        if ((bool)FuelSCInd)
        //            EnableTextFuelScRate = true;
        //        else
        //            EnableTextFuelScRate = false;

        //    }
        //    catch (Exception ex)
        //    {
        //        bool displayErrorOnUI = false;
        //        CommonSettings.logger.LogError(this.GetType(), ex);
        //        if (displayErrorOnUI)
        //        { throw; }
        //    }
        //    finally
        //    {
        //        CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }
        //}


        #endregion

        #endregion Events

        private void ResetWindow()
        {
            BillingID = 0;
            CustomerName = string.Empty;
            CustomerNumber = string.Empty;
            InvoiceNumber = string.Empty;
            CustomerAddress = string.Empty;
            Status = string.Empty;
            CrossRefNumber = string.Empty;
            InvoiceType = string.Empty;
            StorageHandling = null;
            InvoiceDate = null;
            paymentMethod = null;
            TotalInvoice = null;
            TotalPaid = null;
            TotalUnits = null;
            CreditsIssued = null;
            BalanceDue = null;
            ListInvoiceDetails = null;
            Comments = null;
            ListInvoiceLineItems = null;
            LstPaymentMethod = null;
            LstInvoiceType = null;
            CreatedBy = string.Empty;
            CreatedDate = null;
            UpdatedBy = string.Empty;
            UpdatedDate = null;
            StorageHandling = 0.00;
            TotalInvoice = 0.00;
            TotalPaid = 0.00;
            CreditsIssued = 0.00;
            BalanceDue = 0.00;
            TotalUnits = 0;

            //TransportCharges = 0.00;
            //DueFromBrokers = 0.00;
            //DueToBrokers = 0.00;
            //FuelSC = 0.00;
            //FuelSCRate = 0.00;
            //DueToBrokers = null;
            //TransportCharges = null;
            //DueFromBrokers = null;
            //FuelSCRateInd = false;
            //Driver = null;
            //FuelSCRate = null;
            //FuelSC = null;
            //FuelSCInd = false;
        }

        /// <summary>
        /// Function NewVehicleDetail to open the customer locator window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public void NewCustomerDetail(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                ResetWindow();
                LoadPaymentMethod();
                LoadInvoiceTypeMethod();
                Status = Resources.StatusPending;
                InvoiceDate = DateTime.Now;
                CustomerLocator objCustLocator = new CustomerLocator();
                objCustLocator.Owner = Application.Current.MainWindow;
                objCustLocator.ShowDialog();
                AcceptChanges();
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
        /// Function NewVehicleDetail to open the customer locator window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public void OpenPortStorageVehicle(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (obj != null)
                {
                    AddEditBillingVM objSelectedUser = (AddEditBillingVM)obj;

                    string value = objSelectedUser.SelectedUser.PortStorageVehiclesID.ToString();
                    Window win = new Window();
                    VehicleDetail eDoc = new VehicleDetail();
                    win.Content = eDoc;
                    win.Title = "Vehicle Details";
                    string cmd = "popup" + "_" + value;
                    DelegateEventVehicle.SetValueMethodPopup(cmd);
                    win.ShowDialog();
                    //objVehicle.ShowDialog();
                    //Window parentWindow = Window.GetWindow(objCustLocator);
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

        #region Code to be removed after database cleanup


        #endregion

        /// <summary>
        /// To Find Invoice number for new add billing record
        /// </summary>
        /// <returns></returns>
        public string CalculateInvoiceNumber()
        {
            string iInvoiceNumber = string.Empty;
            try
            {
                iInvoiceNumber = _serviceInstance.SetDefaultvalue(NEXT_INVOICE_NUMBER);
                if(!string.IsNullOrEmpty(iInvoiceNumber))
                {
                    string nextInvoiceNumber = (Convert.ToInt32(iInvoiceNumber) + 1).ToString();
                    var isUpdated=_serviceInstance.UpdateNextInvoiceNumberValue((Convert.ToInt32(iInvoiceNumber) + 1).ToString());
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


            return iInvoiceNumber;
        }

        void BtnFindData(object obj)
        {
            LoadPaymentMethod();
            LoadInvoiceTypeMethod();
            InvoiceRecordFind objBillingFindVM = new InvoiceRecordFind();
            objBillingFindVM.Owner = Application.Current.MainWindow;
            objBillingFindVM.ShowDialog();
        }

        /// <summary>
        /// Function to Get Payment method.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul 22,2016</createdOn>
        public async void LoadPaymentMethod()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var qry = (await _serviceInstance.LoadCodeListAsync(Enums.CodeType.PaymentMethod.ToString())).Select(d => new CodeList
                {
                    Code1 = d.Code1,
                    Description = d.Description,
                });

                if (qry != null)
                {
                    foreach (var item in qry)
                    {
                        PaymentMethod = item.Code1;
                    }
                }
                LstPaymentMethod = null;
                LstPaymentMethod = new List<CodeList>(qry);
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
        /// Function to Get Payment method.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul 22,2016</createdOn>
        public async void LoadInvoiceTypeMethod()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var qry = (await _serviceInstance.LoadCodeListAsync(Enums.CodeType.InvoiceType.ToString())).Select(d => new CodeList
                {
                    Code1 = d.Code1,
                    Description = d.Description,
                });

                if (qry != null)
                {
                    foreach (var item in qry)
                    {
                        InvoiceType = item.Code1;
                    }
                }
                LstInvoiceType = null;
                LstInvoiceType = new List<CodeList>(qry);
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
        /// Function to insert data in billing table.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul 22,2016</createdOn>
        public void InsertBillingData(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (Action == Resources.ActionSave)
                {
                    if (!string.IsNullOrEmpty(CustomerName))
                    {
                        if (!string.IsNullOrEmpty(InvoiceType))
                        {
                            if (!string.IsNullOrEmpty(PaymentMethod))
                            {
                                BillingListProp objBilling = new BillingListProp();
                                objBilling.CustomerID = CustomerID;
                                objBilling.OutsideCarrierInvoiceInd = 0;
                                objBilling.InvoiceDate = InvoiceDate;
                                objBilling.InvoiceNumber = CalculateInvoiceNumber();
                                objBilling.InvoiceType = InvoiceType;
                                objBilling.PaymentMethod = PaymentMethod;
                                objBilling.InvoiceAmount = (decimal?)TotalInvoice;
                                objBilling.AmountPaid = (decimal?)TotalPaid;
                                objBilling.CreditsIssued = (decimal?)CreditsIssued;
                                objBilling.BalanceDue = (decimal?)BalanceDue;
                                objBilling.PaidInFullInd = 0;
                                objBilling.PaidInFullDate = null;
                                objBilling.InvoiceStatus = "Pending";
                                objBilling.CreditMemoInd = 0;
                                objBilling.CreditedOutInd = 0;
                                objBilling.CreationDate = DateTime.Now;
                                objBilling.CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString(); ;
                                objBilling.StorageCharges = 0; ;

                                
                                //objBilling.TransportCharges = (decimal?)TransportCharges;
                                //objBilling.FuelSurchargeRate = (decimal?)FuelSCRate;
                                //objBilling.FuelSurchargeRateOverrideInd = FuelSCRateInd != null ? Convert.ToInt32(FuelSCRateInd) : 0;
                                //objBilling.FuelSurcharge = (decimal?)FuelSC;
                                //objBilling.FuelSurchargeOverrideInd = FuelSCInd != null ? Convert.ToInt32(FuelSCInd) : 0;
                                //objBilling.DueToOutsideCarriers = (decimal?)DueToBrokers;
                                //objBilling.DueFromOutsideCarriers = (decimal?)DueFromBrokers;

                                int getBillingID = _serviceInstance.InsertBilling(objBilling);
                                if (getBillingID > 0)
                                {
                                    BillingID = getBillingID;
                                    InvoiceNumber = objBilling.InvoiceNumber;
                                    EnabledNewUser = true;
                                    EnabledFind = true;
                                    EnabledPrint = true;
                                    EnabledCancel = false;
                                    if (BillingID > 0)
                                    {
                                        FillBillingData(BillingID);
                                        FillInvoiceData(null);
                                        FillInvoicLineItems(null);
                                        //FillUserDetailsinForm(VehicleId.ToString());
                                        //EnabledPrint = true;
                                        EnabledModifyDelete = true;
                                    }
                                    else
                                        EnabledModifyDelete = true;

                                    EnabledPrevNext = false;
                                    EnabledSaveUpdate = false;
                                    IsEditable = false;
                                    MessageBox.Show(Resources.msgInsertedSuccessfully);
                                }
                                else
                                    MessageBox.Show(Resources.ErrorInserted);
                            }
                            else
                                MessageBox.Show(Resources.ReqPaymentMethod);
                        }
                        else
                            MessageBox.Show(Resources.msgCustomerNameReq);
                    }
                    else
                        MessageBox.Show(Resources.ReqInvoiceType);
                }
                else if (Action == Resources.ActionUpdate)
                {
                    if (!string.IsNullOrEmpty(CustomerName))
                    {
                        if (!string.IsNullOrEmpty(InvoiceType))
                        {
                            if (!string.IsNullOrEmpty(PaymentMethod))
                            {
                                BillingListProp objBilling = new BillingListProp();
                                objBilling.BillingID = BillingID;
                                objBilling.CustomerID = CustomerID;
                                objBilling.OutsideCarrierInvoiceInd = 0;
                                objBilling.InvoiceDate = InvoiceDate;
                                objBilling.InvoiceNumber = InvoiceNumber;
                                objBilling.InvoiceType = InvoiceType;
                                objBilling.PaymentMethod = PaymentMethod;
                                objBilling.InvoiceAmount = (decimal?)TotalInvoice;
                                objBilling.AmountPaid = (decimal?)TotalPaid;
                                objBilling.CreditsIssued = (decimal?)CreditsIssued;
                                objBilling.BalanceDue = (decimal?)BalanceDue;
                                objBilling.PaidInFullInd = 0;
                                objBilling.PaidInFullDate = null;
                                objBilling.InvoiceStatus = Status;
                                objBilling.CreditMemoInd = 0;
                                objBilling.CreditedOutInd = 0;
                                objBilling.Comments = Comments;
                                objBilling.UpdatedDate = DateTime.Now;
                                objBilling.UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                                objBilling.StorageCharges = 0;

                                //objBilling.TransportCharges = (decimal?)TransportCharges;
                                //objBilling.FuelSurchargeRate = (decimal?)FuelSCRate;
                                //objBilling.FuelSurchargeRateOverrideInd = FuelSCRateInd != null ? Convert.ToInt32(FuelSCRateInd) : 0;
                                //objBilling.FuelSurcharge = (decimal?)FuelSC;
                                //objBilling.FuelSurchargeOverrideInd = FuelSCInd != null ? Convert.ToInt32(FuelSCInd) : 0;
                                //objBilling.DueToOutsideCarriers = (decimal?)DueToBrokers;
                                //objBilling.DueFromOutsideCarriers = (decimal?)DueFromBrokers;

                                bool isSuccessful = _serviceInstance.UpdateBillingTab(objBilling);
                                if (isSuccessful)
                                {
                                    MessageBox.Show(Resources.msgUpdatedSuccessfully);
                                    EnabledNewUser = true;
                                    EnabledFind = true;
                                    EnabledPrint = true;
                                    EnabledCancel = false;
                                    if (BillingID > 0)
                                    {
                                        FillBillingData(BillingID);
                                        FillInvoiceData(null);
                                        FillInvoicLineItems(null);
                                        //FillUserDetailsinForm(VehicleId.ToString());
                                        //EnabledPrint = true;
                                        EnabledModifyDelete = true;
                                    }
                                    else
                                        EnabledModifyDelete = true;
                                    if (listBIlling.Count > 1)
                                        EnabledPrevNext = true;
                                    EnabledSaveUpdate = false;
                                    IsEditable = false;
                                }
                                else
                                    MessageBox.Show(Resources.ErrorToUpdate);
                            }
                            else
                                MessageBox.Show(Resources.ErrorInserted);
                        }
                        else
                            MessageBox.Show(Resources.ReqPaymentMethod);
                    }
                    else
                        MessageBox.Show(Resources.msgCustomerNameReq);
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
        /// Function to update data in billing table.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul 22,2016</createdOn>
        public void UpdateBillingData(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                EnabledNewUser = false;
                EnabledFind = false;
                EnabledPrint = true;
                EnabledCancel = true;
                EnabledModifyDelete = false;
                EnabledPrevNext = false;
                EnabledSaveUpdate = true;
                IsEditable = true;
                Action = Resources.ActionUpdate;
                AcceptChanges();
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
        /// Function to update data in billing table.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul 22,2016</createdOn>
        public void DeleteBillingData(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.MsgDeleteConfirm, Resources.msgTitleMessageBoxDelete, MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    if (BillingID > 0)
                    {
                        bool isSuccessful = _serviceInstance.DeleteBillingData(BillingID);

                        if (isSuccessful)
                        {
                            MessageBox.Show(Resources.msgDeleteSuccessfully);
                            ResetWindow();
                            EnabledNewUser = true;
                            EnabledFind = true;
                            EnabledPrint = true;
                            EnabledCancel = false;
                            EnabledModifyDelete = false;
                            EnabledPrevNext = false;
                            EnabledSaveUpdate = false;
                            IsEditable = false;
                            Action = Resources.ActionUpdate;
                            AcceptChanges();
                        }
                        else
                        { MessageBox.Show(Resources.ErrorDelete); }
                    }
                    else
                    { MessageBox.Show(Resources.MsgSelectUser); }
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
                    AcceptChanges();
                }
            }
        }

        public void Previous(object obj)
        {

            if (listBIlling.Count > 1)
            {

                if ((currentindex - 1) >= 0)
                {
                    currentindex = currentindex - 1;
                    int billingIDIndex = Convert.ToInt32(listBIlling[currentindex]);
                    FillInvoiceData(null);
                    FillInvoicLineItems(null);
                    FillBillingData(billingIDIndex);
                    //if ((currentindex) == 0)
                    //    MessageBox.Show(Resources.msgVehiclePrevRecord);

                }
                else
                {
                    MessageBox.Show(Resources.msgVehiclePrevRecord);
                }
            }
        }
        public void Next(object obj)
        {
            try
            {
                if (listBIlling.Count > 1)
                {
                    if ((currentindex + 1) < listBIlling.Count)
                    {
                        currentindex = currentindex + 1;
                        int billingIDIndex = Convert.ToInt32(listBIlling[currentindex]);
                        FillInvoiceData(null);
                        FillInvoicLineItems(null);
                        FillBillingData(billingIDIndex);
                        //if ((currentindex) == listVin.Count -1)
                        //    MessageBox.Show(Resources.msgVehicleLastRecord);

                    }
                    else
                    {
                        MessageBox.Show(Resources.msgVehicleLastRecord);
                    }
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
        }

        public void GetCustomerValue(CustomerInfo custInfo)
        {
            if (custInfo.CustomerID > 0)
            {
                CustomerAddress = custInfo.AddressLine1Info;
                CustomerName = custInfo.CustomerNameInfo;
                customerID = custInfo.CustomerID;
                CreatedDate = custInfo.CreatedDate;
                CreatedBy = custInfo.CreatedBy;
                UpdatedDate = custInfo.UpdatedDate;
                UpdatedBy = custInfo.UpdatedBy;
                EnabledNewUser = false;
                EnabledFind = false;
                EnabledPrint = true;
                EnabledCancel = true;
                EnabledModifyDelete = false;
                EnabledPrevNext = false;
                EnabledSaveUpdate = true;
                IsEditable = true;
                Action = Resources.ActionSave;
            }
        }

        public void FillBillingData(int bililngID)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                BillingListProp billing = _serviceInstance.GetBillingData(bililngID);

                if (billing != null)
                {
                    CustomerID = billing.CustomerID;
                    CrossRefNumber = billing.CrossRefNumber;
                    Status = billing.InvoiceStatus != null ? billing.InvoiceStatus : Resources.StatusPending;
                    InvoiceType = billing.InvoiceType;
                    StorageHandling = (double?)billing.StorageCharges;//billing.TotalCharge != null ? (double?)billing.TotalCharge : 0.00;//ListInvoiceDetails.Select(x => x.TotalCharge).ToList() != null ? (double?)ListInvoiceDetails.Sum(x => x.TotalCharge) : 0.00;
                    InvoiceDate = billing.InvoiceDate != null ? billing.InvoiceDate : DateTime.Now;
                    PaymentMethod = billing.PaymentMethod;
                    TotalInvoice = (double?)billing.InvoiceAmount;//billing.TotalCharge != null ? (double?)billing.TotalCharge : 0.00;//ListInvoiceDetails.Select(x=>x.TotalCharge).ToList()!=null?  (double?) ListInvoiceDetails.Sum(x=>x.TotalCharge): 0.00;
                    TotalPaid = (double?)billing.AmountPaid;
                    CreditsIssued = (double?)billing.CreditsIssued;
                    BalanceDue = (double?)billing.InvoiceAmount;//billing.BalanceDue;
                    CreditedOutInd = billing.CreditedOutInd;
                    CreditMemoInd = billing.CreditMemoInd;
                    Comments = billing.Comments;
                    CreatedDate = billing.CreationDate;
                    CreatedBy = billing.CreatedBy;
                    UpdatedDate = billing.UpdatedDate;
                    UpdatedBy = billing.UpdatedBy;
                    TotalUnits = ListInvoiceDetails!=null && ListInvoiceDetails.Count > 0 ? ListInvoiceDetails.FirstOrDefault().TotalPageCount : 0;
                    InvoiceNumber = billing.InvoiceNumber;
                    CustomerNumber = billing.CustomerNumber;
                    CustomerName = billing.CustomerName;
                    AcceptChanges();

                    //FuelSCRate = (double?)billing.FuelSurchargeRate;
                    //FuelSCRateInd = billing.FuelSurchargeRateOverrideInd != null ? Convert.ToBoolean(billing.FuelSurchargeRateOverrideInd) : false;
                    //FuelSC = (double?)billing.FuelSurcharge;
                    //FuelSCInd = billing.FuelSurchargeOverrideInd != null ? Convert.ToBoolean(billing.FuelSurchargeOverrideInd) : false;
                    //DueFromBrokers = (double?)billing.DueFromOutsideCarriers;
                    //DueToBrokers = (double?)billing.DueToOutsideCarriers;
                    //RunID = billing.RunID;
                    //TransportCharges = (double?)billing.TransportCharges;

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

        public void FillInvoiceData(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                Count = 0; IsEditable = true;
                if (obj == null)
                {
                    CurrentPageIndex = 0;
                }
                if (CurrentPageIndex == 0)
                {
                    Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
                    Application.Current.Properties["FindUserGridCurrentPageIndex"] = 0;
                }
                PortStorageVehicleProp objBillingProp = new PortStorageVehicleProp();

                objBillingProp.BillingID = BillingID;
                objBillingProp.CurrentPageIndex = CurrentPageIndex;
                objBillingProp.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize;
                objBillingProp.DefaultPageSize = _GridPageSize;

                var lstInvoiceData = _serviceInstance.GetStorageVehicleDetails(objBillingProp).ToList();

                if (CurrentPageIndex == 0)
                {
                    ListInvoiceDetails = null;
                    ListInvoiceDetails = new ObservableCollection<PortStorageVehicleProp>(lstInvoiceData);
                }
                else
                {
                    if (ListInvoiceDetails != null && ListInvoiceDetails.Count > 0)
                    {
                        //ObservableCollection<UserDetails> userDetailListOld = ;
                        foreach (PortStorageVehicleProp ud in new ObservableCollection<PortStorageVehicleProp>(lstInvoiceData))
                        {
                            ListInvoiceDetails.Add(ud);
                        }
                    }
                }
                Count = ListInvoiceDetails.Count > 0 ? ListInvoiceDetails.ToList().FirstOrDefault().TotalPageCount : 0;
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

        public void FillInvoicLineItems(object obj)
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
                BillingLineItemsProp objBillingProp = new BillingLineItemsProp();

                objBillingProp.BillingID = BillingID;
                objBillingProp.CurrentPageIndex = CurrentPageIndex;
                objBillingProp.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize;
                objBillingProp.DefaultPageSize = _GridPageSize;

                var lstInvoicelineItems = _serviceInstance.GetLineItemsList(objBillingProp);
                if (CurrentPageIndex == 0)
                {
                    ListInvoiceLineItems = null;
                    ListInvoiceLineItems = new ObservableCollection<BillingLineItemsProp>(lstInvoicelineItems);
                }
                else
                {
                    if (ListInvoiceLineItems != null && ListInvoiceLineItems.Count > 0)
                    {
                        foreach (BillingLineItemsProp ud in new ObservableCollection<BillingLineItemsProp>(lstInvoicelineItems))
                        {
                            ListInvoiceLineItems.Add(ud);
                        }
                    }
                }
                //if (lstInvoicelineItems != null)
                //{
                //    ListInvoiceLineItems = null;
                //    ListInvoiceLineItems = lstInvoicelineItems.ToList();
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
        /// Function to reset exported indicator
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Jul 22,2016</createdOn>
        public void ResetExportedInd(object obj)
        {

            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (BillingID > 0)
                {
                    bool isSuccessfull = _serviceInstance.ResetExportedInd(BillingID);
                    if (isSuccessfull)
                    {
                        MessageBox.Show("Exported indicator has been reset successfully.");
                        FillInvoicLineItems(BillingID);

                    }
                    else
                        MessageBox.Show(Resources.ErrorDelete);
                }
                else
                    MessageBox.Show(Resources.ReqBillingID);
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

        public void Cancel(object obj)
        {
            try
            {
                //ResetWindow();
                EnabledNewUser = true;
                EnabledFind = true;
                EnabledCancel = false;
                if (listBIlling.Count > 1)
                    EnabledPrevNext = true;
                EnabledSaveUpdate = false;
                IsEditable = false;
                if (BillingID > 0)
                {
                    FillBillingData(BillingID);
                    FillInvoiceData(null);
                    FillInvoicLineItems(null);
                    //FillUserDetailsinForm(VehicleId.ToString());
                    //EnabledPrint = true;
                    EnabledModifyDelete = true;
                }
                else
                {
                    //EnabledPrint = false;
                    EnabledModifyDelete = false;
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
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DelegateEventCustomer.OnSetCustomerValueEvt -= new DelegateEventCustomer.SetCustomerValue(GetCustomerValue);
                    DelegateEventBillingPeriod.OnSetValueListEvt -= new DelegateEventBillingPeriod.SetValueList(NotificationMessageReceivedList);
                    DelegateEventVehicle.OnSetValueEvtRefreshGridCmd -= new DelegateEventVehicle.SetValueRefreshGrid(RefreshGrid);
                    DelegateEventBillingPeriod.OnSetValuePageNumberInvoiceCmd -= new DelegateEventBillingPeriod.SetValuePageNumberInvoiceClick(GetCurrentPageIndex);
                    DelegateEventBillingPeriod.OnSetValuePageNumberLineItemCmd -= new DelegateEventBillingPeriod.SetValuePageNumberLineItemClick(GetCurrentLinePageIndex);
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
}
