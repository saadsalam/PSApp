using Appworks.Reports;
using AppWorks.UI.Common;
using AppWorks.UI.Model;
using AppWorks.UI.Properties;
using AppWorks.UI.View.PortStorageInvoices;
using AppWorks.UI.View.UserControls.Vehicle;
using AppWorks.UI.ViewModel.Vehicle;
using AppWorksService.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace AppWorks.UI.ViewModel.PortStorageInvoices
{
    public class GeneratePortStorageInvoicesVM : ViewModelBase, IDisposable
    {
        private const string PORTSTORAGE_INVOICE_NUMBER_PREFIX = "PortStorageInvoiceNumberPrefix";

        private const string PORTSTORAGE_INVOICE_NUMBER_LENGTH = "PortStorageInvoiceNumberLength";

        private const string NEXT_PORTSTORAGE_INVOICE_NUMBER = "NextPortStorageInvoiceNumber";

        private const string ALPHABETS_STRING = "ABDEFGHIJKLMNOPQRSTUVWXYZ"; //C not in list as it denotes a credit memo

        private const string BILL_LINE_ITEM_DESC_TEXT = "Dealer Port Storage";

        private const string INVOICE_TYPE = "StorageCharge";

        string userCode = Application.Current.Properties["LoggedInUserName"].ToString();

        #region "Class level variables"
        decimal iDailyStorageCharges;
        decimal? iEntryRate;
        int? iPerDiemGraceDays;
        string iInvoiceNumberPrefix = string.Empty;
        int iInvoiceNumberLength;
        int iInvoiceNumberNumber;
        BillingProp objBillingprop = null;
        List<PortStorageVehicleProp> iVehicleDetailsList = null;
        List<LoadInfoListModel> objLoadInfoList = null;
        BillingLineItemsModel iBillingLineItemsRow = null;
        string iCustomerCode = string.Empty;
        decimal? iInvoiceAmount = 0;
        string ErrorMessage = string.Empty;
        #endregion

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
        /// constructor of the class
        /// </summary>
        /// <param name="cutoffDate"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        public GeneratePortStorageInvoicesVM()
        {
            var report = new PrintInvoiceErrorRPT();
            CutoffDate = CalculateNearestCutoffDate(DateTime.Now);   // = DateTime.Now.AddDays(-3);
            SelectInvoicesToReprintCommand = new AppWorxCommand(OnSelectInvoicesToReprintCommandRaised);
            DelegateEventPortStorageInvoices.OnSetValueCutOffDate += new DelegateEventPortStorageInvoices.SetValueCutOffDate(FindPortStorageInvoices);
            IsGenerateInvoiceProcessed = true;
        }

        #region "Properties of class"

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
        private string companyaddressLine1;
        public string CompanyAddressLine1
        {
            get { return this.companyaddressLine1; }
            set { this.companyaddressLine1 = value; NotifyPropertyChanged("CompanyAddressLine1"); }
        }
        private string companycity;
        public string CompanyCity
        {
            get { return this.companycity; }
            set { this.companycity = value; NotifyPropertyChanged("CompanyCity"); }
        }
        private string companyphone;
        public string CompanyPhone
        {
            get { return this.companyphone; }
            set { this.companyphone = value; NotifyPropertyChanged("CompanyPhone"); }
        }
        private DateTime selectableCutoffDateStart = DateTime.Now.AddYears(-1000);
        public DateTime SelectableCutoffDateStart
        {
            get { return this.selectableCutoffDateStart; }
            set
            {
                if (this.selectableCutoffDateStart == value) return;
                this.selectableCutoffDateStart = value;
                NotifyPropertyChanged("SelectableCutoffDateStart");
            }
        }

        private DateTime selectableCutoffDateEnd = DateTime.Now;
        public DateTime SelectableCutoffDateEnd
        {
            get { return this.selectableCutoffDateEnd; }
            set
            {
                if (this.selectableCutoffDateEnd == value) return;
                this.selectableCutoffDateEnd = value;
                NotifyPropertyChanged("SelectableCutoffDateEnd");
            }
        }

        private ICollection<DateTime> cutoffBlackoutDates;
        public ICollection<DateTime> CutoffBlackoutDates
        {
            get { return this.cutoffBlackoutDates; }
            set
            {
                if (this.cutoffBlackoutDates == value) return;
                this.cutoffBlackoutDates = value;
                NotifyPropertyChanged("CutoffBlackoutDates");
            }
        }

        private bool isSelectInvoicesToReprintPanelVisible;
        public bool IsSelectInvoicesToReprintPanelVisible
        {
            get { return this.isSelectInvoicesToReprintPanelVisible; }
            set
            {
                if (this.isSelectInvoicesToReprintPanelVisible == value) return;
                this.isSelectInvoicesToReprintPanelVisible = value;
                NotifyPropertyChanged("IsSelectInvoicesToReprintPanelVisible");
            }
        }

        public AppWorxCommand SelectInvoicesToReprintCommand { get; private set; }

        private ObservableCollection<PortStorageInvoicesProp> portStorageInvoicesList;
        public ObservableCollection<PortStorageInvoicesProp> PortStorageInvoicesList
        {
            get { return portStorageInvoicesList; }
            private set
            {
                this.portStorageInvoicesList = value;
                NotifyPropertyChanged("PortStorageInvoicesList");
            }
        }

        private DateTime? cutoffDate;
        public DateTime? CutoffDate
        {
            get { return this.cutoffDate; }
            set { this.cutoffDate = value; NotifyPropertyChanged("CutoffDate"); }
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

        private List<PrintInvoiceErrorReport> portStoragePrintErrorInvoiceList;
        public List<PrintInvoiceErrorReport> PortStoragePrintErrorInvoiceList
        {
            get { return portStoragePrintErrorInvoiceList; }
            private set
            {
                this.portStoragePrintErrorInvoiceList = value;
                NotifyPropertyChanged("PortStoragePrintErrorInvoiceList");
            }
        }
        private bool status;
        public bool Status
        {
            get { return this.status; }
            set { this.status = value; NotifyPropertyChanged("Status"); }
        }

        private bool _isGenerateInvoiceProcessed;
        public bool IsGenerateInvoiceProcessed
        {
            get
            {
                return _isGenerateInvoiceProcessed;
            }
            set
            {
                _isGenerateInvoiceProcessed = value;
                NotifyPropertyChanged("IsGenerateInvoiceProcessed");
            }
        }


        #endregion

        #region "Events of the class"
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
        private AppWorxCommand btnFind_Click;
        /// <summary>
        /// Find button event
        /// </summary>
        public AppWorxCommand BtnFind_Click
        {
            get
            {
                if (btnFind_Click == null)
                {
                    btnFind_Click = new AppWorxCommand(this.FindPortStorageInvoices);
                }
                return btnFind_Click;
            }
        }

        private AppWorxCommand btnGenerateInvoices_Click;
        public AppWorxCommand BtnGenerateInvoices_Click
        {
            get
            {
                if (btnGenerateInvoices_Click == null)
                {
                    btnGenerateInvoices_Click = new AppWorxCommand(this.GenerateInvoices);
                }
                return btnGenerateInvoices_Click;
            }
        }

        //private AppWorxCommand btnPrintErrors_Click;
        ///// <summary>
        ///// Find button event
        ///// </summary>
        //public AppWorxCommand BtnPrintErrors_Click
        //{
        //    get
        //    {
        //        if (btnPrintErrors_Click == null)
        //        {
        //            //btnPrintErrors_Click = new AppWorxCommand(this.GetPrintErrorsForInvoice);
        //        }
        //        return btnPrintErrors_Click;
        //    }
        //}

        private AppWorxCommand btnPrintInvoice_Click;
        /// <summary>
        /// Find button event
        /// </summary>
        public AppWorxCommand BtnPrintInvoice_Click
        {
            get
            {
                if (btnPrintInvoice_Click == null)
                {
                    btnPrintInvoice_Click = new AppWorxCommand(this.OpenPrintInvoice);
                }
                return btnPrintInvoice_Click;
            }
        }


        private AppWorxCommand btnApproveInvoices_Click;
        /// <summary>
        /// ApproveInvoices button event
        /// </summary
        public AppWorxCommand BtnApproveInvoices_Click
        {
            get
            {
                if (btnApproveInvoices_Click == null)
                {
                    btnApproveInvoices_Click = new AppWorxCommand(this.ApproveInvoices);
                }
                return btnApproveInvoices_Click;
            }
        }


        private AppWorxCommand btnClearProcessed_Click;
        /// <summary>
        /// Find button event
        /// </summary>
        public AppWorxCommand BtnClearProcessed_Click
        {
            get
            {
                if (btnClearProcessed_Click == null)
                {
                    btnClearProcessed_Click = new AppWorxCommand(this.ClearProcessed);
                }
                return btnClearProcessed_Click;
            }
        }

        #endregion

        private DateTime CalculateNearestCutoffDate(DateTime startingFrom)
        {
            while (startingFrom.DayOfWeek != DayOfWeek.Saturday)
            {
                startingFrom = startingFrom.AddDays(-1);
            }
            return startingFrom;
        }

        private static ICollection<DateTime> CalculateCutoffBlackoutDates(DateTime startDate, DateTime endDate)
        {
            Debug.Assert(endDate > startDate);

            LinkedList<DateTime> cutoffDates = new LinkedList<DateTime>();

            DateTime currentDate = startDate.Date;
            while (currentDate <= endDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday)
                {
                    cutoffDates.AddLast(currentDate);
                }
                currentDate = currentDate.AddDays(-1);
            }
            return cutoffDates;
        }

        public void OpenPortStorageVehicle(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (obj != null)
                {
                    ObservableCollection<object> objSel = (ObservableCollection<object>)SelectedItems;
                    List<PortStorageVehicleProp> VehicleDetailsList = new List<PortStorageVehicleProp>();
                    List<VehicleLocatorVM> objList = new List<VehicleLocatorVM>();
                    foreach (PortStorageInvoicesProp Vl in objSel)
                    {
                        VehicleDetailsList = ((List<PortStorageVehicleProp>)_serviceInstance.GetPortStorageVehicleList(CutoffDate, Vl.CustomerId).ToList());

                        foreach (PortStorageVehicleProp V in VehicleDetailsList)
                        {
                            VehicleLocatorVM objVehicalLocator = new VehicleLocatorVM(null);
                            objVehicalLocator.Vin = V.VIN;
                            objVehicalLocator.VehicleId = V.PortStorageVehiclesID;
                            objVehicalLocator.Make = V.Make;
                            objVehicalLocator.CustomerName = Vl.CustName;
                            objList.Add(objVehicalLocator);
                        }
                    }

                    Window win = new Window();
                    VehicleDetail eDoc = new VehicleDetail();
                    win.Content = eDoc;
                    win.Title = "Vehicle Details";
                    eDoc.DataContext = new VehicleDetailsVM();
                    DelegateEventVehicle.SetValueListMethod(objList);
                    DelegateEventVehicle.SetValueMethodTab(1);
                    DelegateEventVehicle.SetValueMethodTabEnable(false);
                    DelegateEventVehicle.SetValueMethodCmd("Edit");
                    Application.Current.MainWindow = win;
                    win.ShowDialog();
                }
            }
            catch (Exception ex)
            {
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void ApproveInvoices(object obj)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            int billingId = 0;
            bool result = false;
            string iComma = string.Empty;
            string lvSqlString = string.Empty;
            string iBillingIDString = string.Empty;

            try
            {
                if (SelectedItems.Count > 0)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(Resources.MsgApproveConfirm, Resources.msgTitleMessageBoxApprove, System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                        // creating the object of service property

                        objBillingprop = new BillingProp();

                        ObservableCollection<object> objSel = (ObservableCollection<object>)SelectedItems;
                        List<PortStorageInvoicesProp> objList = new List<PortStorageInvoicesProp>();
                        List<PortStorageInvoicesProp> tempList = new List<PortStorageInvoicesProp>();

                        foreach (PortStorageInvoicesProp Vl in objSel)
                        {
                            tempList.Add(Vl);
                            PortStorageInvoicesProp objGeneratePortStorageInvoicesVM = new PortStorageInvoicesProp();
                            objGeneratePortStorageInvoicesVM.CustomerId = Vl.CustomerId;
                            objGeneratePortStorageInvoicesVM.CustomerCode = Vl.CustomerCode;
                            objGeneratePortStorageInvoicesVM.CustName = Vl.CustName;
                            objGeneratePortStorageInvoicesVM.Unit = Vl.Unit;
                            objGeneratePortStorageInvoicesVM.BillingAddressID = Vl.BillingAddressID;

                            objList.Add(objGeneratePortStorageInvoicesVM);
                            //PortStorageInvoicesList.Remove(Vl);
                        }

                        if (objList != null)
                        {
                            foreach (var item in objList)
                            {
                                bool iSuccessInd = true;
                                // set the default value for the class and parameters
                                iSuccessInd = SetDefaultValues(null);
                                if (iSuccessInd)
                                {
                                    objBillingprop.CustomerID = item.CustomerId;
                                    if (item.BillingAddressID == 0)
                                    {
                                        item.Status = "Customer Address Missing";
                                        iSuccessInd = false;
                                    }
                                    else if (string.IsNullOrEmpty(item.CustomerCode))
                                    {
                                        item.Status = "Customer Code Missing";
                                        iSuccessInd = false;
                                    }
                                    if (iSuccessInd)
                                    {
                                        if (CutoffDate != null)
                                        {
                                            objBillingprop.InvoiceDate = CutoffDate;
                                        }
                                        else
                                        {
                                            objBillingprop.InvoiceDate = DateTime.Now;
                                        }
                                        objBillingprop.PaymentMethod = "Bill To Customer";
                                        /// insert the details of billing and get billing id
                                        billingId = _serviceInstance.InsertBillingId(objBillingprop);
                                        //if (billingId > 0)
                                        //{
                                        if (billingId > 0)
                                        {
                                            /// get the list of vehicles from database
                                            bool resultVehicle = GetVehicleDetails(CutoffDate, item.CustomerId);
                                            if (resultVehicle)
                                            {
                                                foreach (var line in iVehicleDetailsList.ToList())
                                                {
                                                    if (line.TotalCharge > 0)
                                                    {
                                                        PortStorageVehicleProp objVehicleDetails = new PortStorageVehicleProp();
                                                        objVehicleDetails.TotalCharge = line.TotalCharge;
                                                        objVehicleDetails.EntryRate = line.EntryRate;
                                                        objVehicleDetails.PerDiemGraceDays = (line.PerDiemGraceDays == null) ? 0 : line.PerDiemGraceDays;
                                                        objVehicleDetails.BilledInd = 1;
                                                        objVehicleDetails.BillingID = billingId;
                                                        objVehicleDetails.VehicleStatus = "OutGated";
                                                        objVehicleDetails.PortStorageVehiclesID = line.PortStorageVehiclesID;
                                                        /// update the vehicle details
                                                        bool updated = _serviceInstance.UpdatePostStorageVehicles(objVehicleDetails);
                                                        if (!updated)
                                                        {
                                                            item.Status = "Error Updating Vehicle Record";
                                                            iSuccessInd = false;
                                                        }
                                                    }
                                                }
                                                if (iSuccessInd)
                                                {
                                                    iSuccessInd = CalculateRatesAndTotals();

                                                    if (iSuccessInd)
                                                    {
                                                        objBillingprop.BillingID = billingId;
                                                        iSuccessInd = _serviceInstance.UpdateBilling(objBillingprop);
                                                        if (iSuccessInd)
                                                        {
                                                            GenerateInvoiceAccountingEntries(billingId, objBillingprop);
                                                            if (iSuccessInd)
                                                            {
                                                                if (!string.IsNullOrEmpty(ErrorMessage))
                                                                {
                                                                    item.Status = ErrorMessage;
                                                                }
                                                                else
                                                                {
                                                                    item.Status = "Processed";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                item.Status = "Error Updating Billing Record With Totals";
                                                                iSuccessInd = false;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            item.Status = "Error Updating Billing Record With Totals";
                                                            iSuccessInd = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        iSuccessInd = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                item.Status = "Error Getting Vehicle Details";
                                                iSuccessInd = false;
                                            }
                                        }
                                        else
                                        {
                                            item.Status = "Error Getting Internal Billing Id";
                                            iSuccessInd = false;
                                        }
                                        //}
                                        //else
                                        //{
                                        //    item.Status = "Error Inserting Invoice Record";
                                        //    iSuccessInd = false;
                                        //}
                                    }
                                }
                                else
                                {
                                    item.Status = "Error Setting Default Values";
                                    iSuccessInd = false;
                                }
                                if (iSuccessInd)
                                {

                                    if (!string.IsNullOrEmpty(ErrorMessage))
                                    {
                                        item.Status = ErrorMessage;
                                    }
                                    else
                                    {
                                        item.Status = "Processed";
                                    }
                                    iBillingIDString = iBillingIDString + iComma + billingId;
                                    iComma = ",";
                                }
                            }
                        }

                        string MailingProcess = ConfigurationManager.AppSettings["MailingProcess"];

                        if (MailingProcess.Equals("Yes"))
                        {
                            SendMail(tempList);
                        }

                        foreach (PortStorageInvoicesProp item in tempList)
                        {
                            PortStorageInvoicesList.Remove(item);
                        }
                        foreach (PortStorageInvoicesProp item in objList)
                        { PortStorageInvoicesList.Add(item); }

                        PortStorageInvoicesList.OrderBy(X => X.CustName);
                    }
                }
                else
                { MessageBox.Show(Resources.MsgSelectUser); }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void OnSelectInvoicesToReprintCommandRaised(object obj)
        {
            IsSelectInvoicesToReprintPanelVisible = !IsSelectInvoicesToReprintPanelVisible;
        }

        /// <summary>
        /// Function to get the invoice list
        /// </summary>
        /// <param name="cutoffDate"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        public void FindPortStorageInvoices(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                Count = 0;
                PortStorageInvoicesList = new ObservableCollection<PortStorageInvoicesProp>(_serviceInstance.GetPortStorageInvoicesList(CutoffDate));
                PortStorageInvoicesList.Select(x => x.CustomerId).FirstOrDefault();
                Count = PortStorageInvoicesList.Count;
            }
            catch (Exception ex)
            {
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
        /// To genrate invoices on button click
        /// </summary>
        /// <param name="obj"></param>
        public async void GenerateInvoices(object obj)
        {
            int billingId = 0;

            string iComma = string.Empty;
            string lvSqlString = string.Empty;
            string iBillingIDString = string.Empty;
            IsGenerateInvoiceProcessed = false;

            await Task.Run(() =>
            {
                try
                {
                    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    objBillingprop = new BillingProp();
                    if (PortStorageInvoicesList != null)
                    {
                        foreach (var invoiceItem in PortStorageInvoicesList)
                        {
                            bool iSuccessInd = true;

                            // set the default value for the class and parameters
                            iSuccessInd = SetDefaultValues(null);

                            if (iSuccessInd)
                            {
                                objBillingprop.CustomerID = invoiceItem.CustomerId;

                                if (invoiceItem.BillingAddressID == 0)
                                {
                                    invoiceItem.Status = "Customer Address Missing";
                                    iSuccessInd = false;
                                }
                                else if (string.IsNullOrEmpty(invoiceItem.CustomerCode))
                                {
                                    invoiceItem.Status = "Customer Code Missing";
                                    iSuccessInd = false;
                                }

                                if (iSuccessInd)
                                {
                                    if (CutoffDate != null)
                                    {
                                        objBillingprop.InvoiceDate = CutoffDate;
                                    }
                                    else
                                    {
                                        objBillingprop.InvoiceDate = DateTime.Now;
                                    }


                                    objBillingprop.PaymentMethod = "Bill To Customer";

                                    bool resultVehicle = GetVehicleDetails(CutoffDate, invoiceItem.CustomerId);
                                    if (resultVehicle)
                                    {
                                        var sumOfTotalCharge = (from detail in iVehicleDetailsList where detail.TotalCharge > 0 select detail).ToList().Sum(x => x.TotalCharge);
                                        if (sumOfTotalCharge > 0)
                                        {
                                            //Insert the details of billing and get billing id
                                            billingId = _serviceInstance.InsertBillingId(objBillingprop);
                                            if (billingId > 0)
                                            {
                                                foreach (var vehicleDetail in iVehicleDetailsList.ToList())
                                                {
                                                    if (vehicleDetail.TotalCharge > 0)
                                                    {
                                                        PortStorageVehicleProp objVehicleDetails = new PortStorageVehicleProp();
                                                        objVehicleDetails.TotalCharge = vehicleDetail.TotalCharge;
                                                        objVehicleDetails.EntryRate = vehicleDetail.EntryRate;
                                                        objVehicleDetails.PerDiemGraceDays = (vehicleDetail.PerDiemGraceDays == null) ? 0 : vehicleDetail.PerDiemGraceDays;
                                                        objVehicleDetails.BilledInd = 1;
                                                        objVehicleDetails.BillingID = billingId;
                                                        objVehicleDetails.VehicleStatus = Enum.GetName(typeof(Enums.VehicleStatus), Enums.VehicleStatus.OutGated).ToString();
                                                        objVehicleDetails.PortStorageVehiclesID = vehicleDetail.PortStorageVehiclesID;

                                                        //update the vehicles
                                                        bool updated = _serviceInstance.UpdatePostStorageVehicles(objVehicleDetails);

                                                        if (!updated)
                                                        {
                                                            invoiceItem.Status = "Error Updating Vehicle Record";
                                                            iSuccessInd = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        continue; //invoiceItem.Status = "Error Unable to create total charges";
                                                    }
                                                }
                                                if (iSuccessInd)
                                                {
                                                    iSuccessInd = CalculateRatesAndTotals();

                                                    if (iSuccessInd)
                                                    {
                                                        objBillingprop.BillingID = billingId;

                                                        iSuccessInd = _serviceInstance.UpdateBilling(objBillingprop);

                                                        if (iSuccessInd)
                                                        {
                                                            iSuccessInd = GenerateInvoiceAccountingEntries(billingId, objBillingprop);

                                                            if (iSuccessInd)
                                                            {
                                                                invoiceItem.Status = "Processed";
                                                            }
                                                            else
                                                            {
                                                                invoiceItem.Status = "Error Updating Billing Record With Totals";
                                                                iSuccessInd = false;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            invoiceItem.Status = "Error Updating Billing Record With Totals";
                                                            iSuccessInd = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        iSuccessInd = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                invoiceItem.Status = "Error Getting Internal Billing Id";
                                                iSuccessInd = false;
                                            }

                                        }
                                        else
                                        {
                                            invoiceItem.Status = "UnInvoiced";
                                            iSuccessInd = false;
                                        }

                                    }
                                    else
                                    {
                                        invoiceItem.Status = "Error Getting Vehicle Details";
                                        iSuccessInd = false;
                                    }

                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                invoiceItem.Status = "Error Setting Default Values";
                                iSuccessInd = false;
                            }
                            if (iSuccessInd)
                            {
                                invoiceItem.Status = "Processed";
                                iBillingIDString = iBillingIDString + iComma + billingId;
                                iComma = ",";
                            }
                        }
                    }
                    ObservableCollection<PortStorageInvoicesProp> tempPortStorageInvoicesList = new ObservableCollection<PortStorageInvoicesProp>();
                    tempPortStorageInvoicesList = PortStorageInvoicesList;
                    PortStorageInvoicesList = null;
                    PortStorageInvoicesList = tempPortStorageInvoicesList;
                }
                catch (Exception ex)
                {
                    IsGenerateInvoiceProcessed = true;
                    bool displayErrorOnUI = false;
                    CommonSettings.logger.LogError(this.GetType(), ex);
                    if (displayErrorOnUI)
                    { throw; }
                }
                finally
                {
                    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    IsGenerateInvoiceProcessed = true;
                }
            });
        }

        ///// <summary>
        ///// method GetPrintErrorsForInvoice to print the error invoice
        ///// </summary>
        ///// <param name="object"></param>
        ///// <returns></returns>
        ///// <createdBy></createdBy>
        ///// <createdOn>May-31,2016</createdOn>
        //public void GetPrintErrorsForInvoice(object obj)
        //{
        //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

        //    var data1 = _serviceInstance.GetDAIAddressName(userCode).Select(d => new Appworks.Reports.PrintInvoiceErrorReport
        //    {
        //        CompanyName = d.CompanyName,
        //        CompanyAddressLine1 = d.AddressLine1,
        //        CompnayCity = d.City,
        //        CompanyPhone = d.Phone
        //    }).FirstOrDefault();

        //    if (data1 != null)
        //    {
        //        CompanyName = data1.CompanyName;
        //        CompanyAddressLine1 = data1.CompanyAddressLine1;
        //        CompanyCity = data1.CompnayCity;
        //        CompanyPhone = data1.CompanyPhone;
        //    }

        //    try
        //    {
        //        //var report = new PrintInvoiceErrorRPT();

        //        //var data = _serviceInstance.GetPrintErrorsForInvoiceList().Select(d => new Appworks.Reports.PrintInvoiceErrorReport
        //        //{

        //        //    RunID = d.RunID,
        //        //    CustomerName = d.CustomerName,
        //        //    OrderNumber = d.OrderNumber,
        //        //    VIN = d.VIN,

        //        //}).ToList();
        //        //data.Add(new Appworks.Reports.PrintInvoiceErrorReport
        //        //{
        //        //    CompanyName = CompanyName,
        //        //    CompanyAddressLine1 = CompanyAddressLine1,
        //        //    CompnayCity = CompanyCity,
        //        //    CompanyPhone = CompanyPhone
        //        //});
        //        //PortStoragePrintErrorInvoiceList = data.ToList();
        //        //report.DataSource = data;
        //        //MyReportSource = report;

        //        //if (PortStoragePrintErrorInvoiceList.Count > 0)
        //        //{
        //        //    //PrintWindow objPrint = new PrintWindow(PortStoragePrintErrorInvoiceList);
        //        //    //objPrint.ShowDialog();
        //        //    PrintInvoiceErrorReportWindow objPrintWindow = new PrintInvoiceErrorReportWindow(MyReportSource);
        //        //    objPrintWindow.Owner = Application.Current.MainWindow;
        //        //    objPrintWindow.ShowDialog();
        //        //}
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

        /// <summary>
        /// Set the default values for invoice
        /// </summary>
        /// <param name="iOldInvoiceNumber"></param>
        /// <returns></returns>
        public bool SetDefaultValues(string iOldInvoiceNumber)
        {
            long iPosition = 0;
            bool result = false;
            int lStringSize = 0;
            string iAlphabetString = string.Empty;
            int iInvoiceNumberLength = 0;
            string tUserCode = string.Empty;
            string lFormatString = string.Empty;
            string iInvoiceNumber = string.Empty;
            string iInvoiceNumberPrefix = string.Empty;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                // Sets the default values on a New record
                objBillingprop.CreationDate = DateTime.Now;
                objBillingprop.CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString();

                // get the invoice number prefix for PortStorage
                string invoiceNumberPrefixForPortStorage = _serviceInstance.SetDefaultvalue(PORTSTORAGE_INVOICE_NUMBER_PREFIX);

                if (!string.IsNullOrEmpty(invoiceNumberPrefixForPortStorage))
                {
                    iInvoiceNumberPrefix = invoiceNumberPrefixForPortStorage;
                }
                else
                {
                    MessageBox.Show(Resources.msgGenrateInvoicePrefix);
                    return result;
                }

                // get the invoice number length
                string invoiceNumberLength = _serviceInstance.SetDefaultvalue(PORTSTORAGE_INVOICE_NUMBER_LENGTH);

                if (!string.IsNullOrEmpty(invoiceNumberLength))
                {
                    iInvoiceNumberLength = Convert.ToInt32(invoiceNumberLength);
                }
                else
                {
                    MessageBox.Show(Resources.msgGenrateInvoiceLength);
                    return result;
                }

                //if there is no OldInvoiceNumber
                if ((iOldInvoiceNumber == null) || (iOldInvoiceNumber.Length < 1))
                {
                    //get the next invoice number
                    string nextInvoiceNumber = _serviceInstance.SetDefaultvalue(NEXT_PORTSTORAGE_INVOICE_NUMBER);

                    if (!string.IsNullOrEmpty(nextInvoiceNumber))
                    {
                        iInvoiceNumberNumber = Convert.ToInt32(nextInvoiceNumber);
                    }
                    else
                    {
                        MessageBox.Show(Resources.msgGenrateInvoicePrefixNext);
                        return result;
                    }

                    //set the next invoice number
                    bool isNextInvoiceNumberUpdated = _serviceInstance.UpdateSettingsValue((iInvoiceNumberNumber + 1).ToString());

                    if (!isNextInvoiceNumberUpdated)
                    {
                        MessageBox.Show(Resources.msgGenrateInvoicePrefixNext);
                        return result;
                    }

                    lStringSize = (iInvoiceNumberLength - (iInvoiceNumberPrefix.Length) - (iInvoiceNumberNumber.ToString().Length));// = (8-(p.lenght)-(3237.length)
                    lFormatString = lStringSize.ToString() + "P0";
                    iInvoiceNumber = iInvoiceNumberPrefix;
                    for (int index = 0; index < lFormatString.Length; index++)
                    {
                        iInvoiceNumber += "0";
                    }
                    iInvoiceNumber += iInvoiceNumberNumber;
                }

                //This else part will only run when we have a OldInvoiceNumber coming from database
                //Currently we are not getting OldInvoice Number from databas
                //This logic is according to the OMNIS
                //Need to refactor this when we have OldInvoiceNumber
                else
                {
                    iAlphabetString = ALPHABETS_STRING;

                    //Calculate iPosition as pos(    right(iUninvoicedStorageUnitsList.iOldInvoiceNumber, 1)     ,iAlphabetString)

                    //returns the substring comprising the last n characters
                    var subStrFromRight = iOldInvoiceNumber.Right(1);

                    iPosition = ALPHABETS_STRING.IndexOf(subStrFromRight);

                    if (iPosition == 0)
                    {
                        iInvoiceNumber = string.Concat(iOldInvoiceNumber, 'A');
                        iPosition = 1;
                    }
                    else
                    {
                        //iInvoiceNumber = con(left(iUninvoicedStorageUnitsList.iOldInvoiceNumber,len(iUninvoicedStorageUnitsList.iOldInvoiceNumber)-1),mid(iAlphabetString,iPosition,1))
                    }

                    int getCount = _serviceInstance.GetBillingCount(iInvoiceNumber);

                    int iCount = getCount;

                    while (iCount > 0)
                    {
                        if (iCount > 0)
                        {
                            //Do $ctask.tStmt.$fetchinto(iCount) Returns #F
                        }
                        else
                        {
                            return false;
                        }
                        if (iCount > 0)
                        {
                            iPosition = iPosition + 1;
                            //iInvoiceNumber = con(left(iInvoiceNumber,len(iInvoiceNumber)-1),mid(iAlphabetString,iPosition,1));
                            //iInvoiceNumber = con(left(iInvoiceNumber,iInvoiceNumber.Length-1),mid(iAlphabetString,iPosition,1));
                        }
                    }
                }

                objBillingprop.OutsideCarrierInvoiceInd = 0;
                objBillingprop.InvoiceNumber = iInvoiceNumber;
                objBillingprop.InvoiceType = Enum.GetName(typeof(Enums.InvoiceType), Enums.InvoiceType.StorageCharge);
                objBillingprop.TransportCharges = 0;
                objBillingprop.FuelSurchargeRate = 0;
                objBillingprop.FuelSurchargeRateOverrideInd = 0;
                objBillingprop.FuelSurcharge = 0;
                objBillingprop.FuelSurchargeOverrideInd = 0;
                objBillingprop.OtherCharge1 = 0;
                objBillingprop.OtherCharge1Description = "N/A";
                objBillingprop.OtherCharge2 = 0;
                objBillingprop.OtherCharge2Description = "N/A";
                objBillingprop.OtherCharge3 = 0;
                objBillingprop.OtherCharge3Description = "N/A";
                objBillingprop.OtherCharge4 = 0;
                objBillingprop.OtherCharge4Description = "N/A";
                objBillingprop.InvoiceAmount = 0;
                objBillingprop.AmountPaid = 0;
                objBillingprop.CreditsIssued = 0;
                objBillingprop.PaidInFullInd = 0;
                objBillingprop.CreditMemoInd = 0;
                objBillingprop.CreditedOutInd = 0;
                objBillingprop.StorageCharges = 0;
                objBillingprop.InvoiceStatus = Enum.GetName(typeof(Enums.InvoiceStatus), Enums.InvoiceStatus.Pending);
                objBillingprop.DueToOutsideCarriers = 0;
                objBillingprop.DueFromOutsideCarriers = 0;
                result = true;
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return result;
        }

        /// <summary>
        /// function SgetInsertnamesFromSchema to generate the insert query data
        /// </summary>
        /// <param name="pvSchemaName"></param>
        /// <param name="pvExclusionList"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        private string getInsertnamesFromSchema(string pvSchemaName, string pvExclusionList)
        {
            ///Added code to allow the sql row object name to be passed in
            string lvInsertnamesString = string.Empty;
            string lvRowObjectName = string.Empty;
            string lvWorkingString = string.Empty;
            lvWorkingString = "as";
            string pvRowObjectName = string.Empty;
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (pvRowObjectName.Length > 0)
                {
                    lvRowObjectName = pvRowObjectName;
                }
                else
                {
                    lvRowObjectName = "iSqlRow";
                }
                lvWorkingString = getColumnNamesFromSchema(pvSchemaName, pvExclusionList);
                lvInsertnamesString = "(" + lvWorkingString + ")";
                lvWorkingString = getValuesPartFromSchema(pvSchemaName, pvExclusionList, lvRowObjectName);
                lvInsertnamesString = lvInsertnamesString + " VALUES ('," + lvWorkingString + ",')";
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return lvInsertnamesString;
        }

        /// <summary>
        /// function getColumnNamesFromSchema to generate the column details for insert query
        /// </summary>
        /// <param name="pvSchemaName"></param>
        /// <param name="pvExclusionList"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        private string getColumnNamesFromSchema(string pvSchemaName, string pvExclusionList)
        {
            string lvColumnNames = string.Empty;
            pvExclusionList = "," + pvExclusionList + ",";      // make sure there are commas bracketing both sides to make it easier to find field exactly.
            List<string> list = new List<string>();
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                // create a service function to get all the column name from database
                list = _serviceInstance.GetTableColumnNames(pvSchemaName).ToList();
                if ((list != null) && (list.Count > 0))
                {
                    foreach (string line in list)
                    {
                        if (lvColumnNames.Length > 0)
                        {   // if we have values already, then add a comma
                            lvColumnNames = lvColumnNames + ",";
                        }
                        if (!("," + line + ",").Equals(pvExclusionList))                /// If it is NOT in the exclusion list...then add to the comma delimited list
                        {
                            lvColumnNames = lvColumnNames + line;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return lvColumnNames;
        }

        /// <summary>
        /// function getValuesPartFromSchema to generate the column value details for insert query
        /// </summary>
        /// <param name="pvSchemaName"></param>
        /// <param name="pvExclusionList"></param>
        /// <param name="pvRowObjectName"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        private string getValuesPartFromSchema(string pvSchemaName, string pvExclusionList, string pvRowObjectName)
        {

            string lvInsertNames = string.Empty;
            List<string> list = new List<string>();
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                pvExclusionList = "," + pvExclusionList + ",";     /// make sure there are commas bracketing both sides to make it easier to find field exactly.

                /// create a service function to get all the column name from database
                list = _serviceInstance.GetTableColumnNames(pvSchemaName).ToList();
                if ((list != null) && (list.Count > 0))
                {
                    foreach (string line in list)
                    {
                        if (lvInsertNames.Length > 0)
                        {   //; if we have values already, then add a comma
                            lvInsertNames = lvInsertNames + ",";
                        }
                        if (!("," + line + ",").Equals(pvExclusionList))                /// If it is NOT in the exclusion list...then add to the comma delimited list
                        {
                            lvInsertNames = lvInsertNames + "[@" + pvRowObjectName + "." + line + "]";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return lvInsertNames;

        }

        /// <summary>
        /// Get the details from the portstoragevehicles table
        /// </summary>
        /// <param name="cuttOffDate"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private bool GetVehicleDetails(DateTime? cuttOffDate, int customerId)
        {
            bool getDetails = false;

            iVehicleDetailsList = new List<PortStorageVehicleProp>();

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                // get the details from the portstoragevehicles table
                iVehicleDetailsList = ((List<PortStorageVehicleProp>)_serviceInstance.GetPortStorageVehicleList(cuttOffDate, customerId).ToList());

                if (iVehicleDetailsList == null || iVehicleDetailsList.Count == 0)
                {
                    return false;
                }

                //Set current list iVehicleDetailsList
                foreach (PortStorageVehicleProp vehicle in iVehicleDetailsList.ToList())
                {
                    if ((vehicle.EntryRate == 0 || vehicle.EntryRate == null) && (vehicle.EntryRateOverrideInd == 0))
                    {
                        //see if we have a rate
                        int rateCount = _serviceInstance.PsRatesCount(vehicle.DateIn, Convert.ToInt32(vehicle.CustomerID));

                        if (rateCount == 0)
                        { continue; }

                        if (rateCount > 0)
                        {
                            //get the rate details
                            var rates = _serviceInstance.PsRatesInvoice(vehicle.DateIn, Convert.ToInt32(vehicle.CustomerID));

                            //If flag true
                            if (rates != null)
                            {
                                iEntryRate = rates.EntryFee;

                                iPerDiemGraceDays = rates.PerDiemGraceDays;

                                vehicle.EntryRate = iEntryRate;

                                if (vehicle.PerDiemGraceDaysOverrideInd == 0)
                                {
                                    vehicle.PerDiemGraceDays = iPerDiemGraceDays;
                                }

                                //  update the rates in the vehicle table
                                _serviceInstance.UpdateVehicleRates(iEntryRate, iPerDiemGraceDays, vehicle.PortStorageVehiclesID);
                            }
                            else { continue; }
                        }
                    }
                }

                //from 1 to #LN step 1
                foreach (PortStorageVehicleProp vehicleDetail in iVehicleDetailsList)
                {
                    decimal dailystorageCharge = 1;

                    bool result = CalculateDailyStorageCharges(vehicleDetail.PortStorageVehiclesID, Application.Current.Properties["LoggedInUserName"].ToString(), dailystorageCharge, out ErrorMessage);

                    if (result)
                    {
                        vehicleDetail.TotalCharge = (vehicleDetail.EntryRate == null ? (decimal)0.00 : vehicleDetail.EntryRate) + iDailyStorageCharges;
                    }
                    else
                    {
                        continue;
                    }
                }

                getDetails = true;
            }
            catch (Exception ex)
            {
                getDetails = false;
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return getDetails;
        }

        /// <summary>
        /// To calculate the port storage per diem charges of vehicle
        /// </summary>
        /// <param name="vehiclesId"></param>
        /// <param name="userCode"></param>
        /// <param name="dailyStorageCharges"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        public bool CalculateDailyStorageCharges(int vehiclesId, string userCode, decimal dailyStorageCharges, out string Message)
        {
            bool result = false;
            string spResult = string.Empty;
            Message = string.Empty;
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                spResult = _serviceInstance.CalculatePortStoragePerDiem(vehiclesId, userCode);

                // Move data now to the vehicle and orders tables.
                if (!string.IsNullOrEmpty(spResult))
                {
                    string[] res = spResult.Split(',');

                    if (Convert.ToInt32(res[0]) == 0)
                    {
                        if (!string.IsNullOrEmpty(res[2]))
                        {
                            iDailyStorageCharges = Convert.ToDecimal(res[2]);
                        }
                        else
                        {
                            iDailyStorageCharges = 0;
                        }
                        result = true;
                    }
                    else
                    {
                        Message = res[1];
                        return false;
                    }
                }
                else
                {
                    Message = string.Empty;
                    return false;
                }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return result;
        }

        /// <summary>
        /// function calcRatesAndTotals to calculate the rates and total.
        /// </summary>
        /// <param name="NA"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        bool CalculateRatesAndTotals()
        {
            bool result = false;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var sumOfTotalCharge = (from detail in iVehicleDetailsList where detail.TotalCharge > 0 select detail).ToList().Sum(x => x.TotalCharge);

                objBillingprop.StorageCharges = objBillingprop.InvoiceAmount = objBillingprop.BalanceDue = sumOfTotalCharge;

                result = true;
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return result;
        }

        /// <summary>
        /// To manage the account entry
        /// </summary>
        /// <param name="billingId"></param>
        /// <param name="iInvoiceDataRow"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        bool GenerateInvoiceAccountingEntries(int billingId, BillingProp iInvoiceDataRow)
        {
            string iCustomerOf;
            int? iBulkBillingInd;
            decimal lTotalCarrierPay = 0;
            iBillingLineItemsRow = new BillingLineItemsModel();
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                int customerId = Convert.ToInt32(iInvoiceDataRow.CustomerID);
                var getCustInfo = _serviceInstance.GetCustomerInfo(customerId);
                if (getCustInfo != null)
                {
                    iCustomerOf = getCustInfo.CustomerOf;
                    iCustomerCode = getCustInfo.CustomerCode;
                    iBulkBillingInd = getCustInfo.BulkBillingInd;
                }

                iBillingLineItemsRow.CustomerID = iInvoiceDataRow.CustomerID;
                iBillingLineItemsRow.BillingID = iInvoiceDataRow.BillingID;
                iBillingLineItemsRow.InvoiceNumber = iInvoiceDataRow.InvoiceNumber;
                iBillingLineItemsRow.InvoiceDate = iInvoiceDataRow.InvoiceDate;
                iBillingLineItemsRow.CreditMemoInd = null;
                iBillingLineItemsRow.ExportedInd = 0;
                iBillingLineItemsRow.ExportBatchID = null;
                iBillingLineItemsRow.ExportedDate = null;
                iBillingLineItemsRow.ExportedBy = null;
                iBillingLineItemsRow.CreationDate = DateTime.Now;
                iBillingLineItemsRow.CreatedBy = "Accounting Export";
                iInvoiceAmount = iInvoiceDataRow.InvoiceAmount;

                bool flag = false;

                if (iInvoiceDataRow.InvoiceType == Enum.GetName(typeof(Enums.InvoiceType), Enums.InvoiceType.TransportCharge).ToString())
                {
                    if ((iInvoiceDataRow.PaymentMethod == "Bill To Customer") || (iInvoiceDataRow.DueFromOutsideCarriers == 0))     //;; simple situation, just need to write the invoice line
                    {
                        //flag = getOutsideCarrierLegList(billingId);
                        flag = false;
                        if (flag == false)
                        {
                            MessageBox.Show(Resources.msgBillingError);
                            return false;
                        }
                        if (lTotalCarrierPay == iInvoiceDataRow.InvoiceAmount)     //;; rare situation, but if the broker gets 100% there should be no entry for reflect
                        {
                            if ((iInvoiceDataRow.PaymentMethod == "COD") || (iInvoiceDataRow.PaymentMethod == "Prepay"))
                            {
                                return true;
                            }
                        }
                        flag = createMainInvoiceLineItem(billingId, iInvoiceDataRow);
                        if (flag == false)
                        {
                            MessageBox.Show(Resources.msgBillingError);
                            return false;
                        }
                        //if (iMiscellaneousAdditive > 0)
                        //{
                        //    //Do method ...createMiscellaneousAdditiveLineItem (billingId) Returns #F
                        //    if (flag == false)
                        //    {
                        //        MessageBox.Show("There was an error creating the miscellaneous additve line item.");
                        //        return false;
                        //    }
                        //}
                        //if (iDealerReimbursement > 0)
                        //{
                        //    //Do method ...createDealerReimbursementLineItem (billingId) Returns #F
                        //    if (flag == false)
                        //    {
                        //        MessageBox.Show("There was an error creating the dealer reimbursement line item.");
                        //        return false;
                        //    }
                        //}
                        //if (iInvoiceDataRow.DueToOutsideCarriers > 0)
                        //{
                        //    //Calculate iBillingLineItemsRow.CustomerNumber as iCustomerCode
                        //    //Do method ...createBrokerageRevenueLineItem (billingId) Returns #F     ;; need to pull back the outside carrier information to write the information to the outside carrier credits table
                        //    if (flag == false)
                        //    {
                        //        MessageBox.Show("There was an error creating the brokerage revenue line item.");
                        //        return false;
                        //    }
                        //    //Calculate iBillingLineItemsRow.CustomerID as iInvoiceDataRow.CustomerID
                        //    //Calculate iBillingLineItemsRow.BillingID as iInvoiceDataRow.BillingID
                        //    //Calculate iBillingLineItemsRow.CustomerNumber as iCustomerCode
                        //    //Calculate iBillingLineItemsRow.InvoiceNumber as iInvoiceDataRow.InvoiceNumber
                        //    //Calculate iBillingLineItemsRow.InvoiceDate as iInvoiceDataRow.InvoiceDate
                        //    //Do method ...getOutsideCarrierPayables (billingId) Returns #F
                        //    if (flag == false)
                        //    {
                        //        MessageBox.Show("There was an error inserting one of the Outside Carrier Credit records.");
                        //        return false;
                        //    }
                        //}
                    }
                }

                //Deals with Port Storage only
                else if (iInvoiceDataRow.InvoiceType == Enum.GetName(typeof(Enums.InvoiceType), Enums.InvoiceType.StorageCharge).ToString())
                {
                    flag = CreateStorageLineItem(iInvoiceDataRow.InvoiceAmount);

                    if (!flag)
                    {
                        MessageBox.Show(Resources.msgDealerError);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return true;
        }

        /// <summary>
        /// function createStorageLineItem to manage the bill line item entry
        /// </summary>
        /// <param name="NA"></param>
        /// <param name="NA"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        bool CreateStorageLineItem(decimal? invoiceAmount)
        {
            bool result = false;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                iBillingLineItemsRow.CustomerNumber = iCustomerCode;
                iBillingLineItemsRow.DebitAccountNumber = getCode("GLAccount", "Accounts Receivable Control");
                if ((iBillingLineItemsRow.DebitAccountNumber.Length >= 5) && (iBillingLineItemsRow.DebitAccountNumber.Substring(0, 5).Equals("Error")))
                {
                    MessageBox.Show(Resources.msgGenrateInvoiceAccountReceivable);
                    return false;
                }
                iBillingLineItemsRow.DebitProfitCenterNumber = string.Empty;
                iBillingLineItemsRow.DebitCostCenterNumber = string.Empty;
                iBillingLineItemsRow.CreditAccountNumber = getCode("GLAccount", "Boston Autoport Storage");
                if ((iBillingLineItemsRow.CreditAccountNumber.Length >= 5) && (iBillingLineItemsRow.CreditAccountNumber.Substring(0, 5).Equals("Error")))
                {
                    MessageBox.Show(Resources.msgGenrateInvoiceBostonAutoport);
                    return false;
                }
                iBillingLineItemsRow.CreditProfitCenterNumber = getCode("ProfitCenter", "Handling");
                if ((iBillingLineItemsRow.CreditProfitCenterNumber.Length >= 5) && (iBillingLineItemsRow.CreditProfitCenterNumber.Substring(0, 5).Equals("Error")))
                {
                    MessageBox.Show(Resources.msgGenrateInvoiceProfitCenter);
                    return false;
                }
                iBillingLineItemsRow.CreditCostCenterNumber = getCode("CostCenter", "Port Storage Program");// Returns //     ;; get the credit cost center
                if ((iBillingLineItemsRow.CreditCostCenterNumber.Length >= 5) && (iBillingLineItemsRow.CreditCostCenterNumber.Substring(0, 5).Equals("Error")))
                {
                    MessageBox.Show(Resources.msgGenrateInvoiceCostCenter);
                    return false;
                }

                iBillingLineItemsRow.ARTransactionAmount = invoiceAmount;
                iBillingLineItemsRow.Description = BILL_LINE_ITEM_DESC_TEXT;
                BillingLineItemsProp objBillingLineItemsProp = new BillingLineItemsProp();
                objBillingLineItemsProp.CustomerID = iBillingLineItemsRow.CustomerID;
                objBillingLineItemsProp.BillingID = iBillingLineItemsRow.BillingID;
                objBillingLineItemsProp.CustomerNumber = iBillingLineItemsRow.CustomerNumber;
                objBillingLineItemsProp.InvoiceNumber = iBillingLineItemsRow.InvoiceNumber;
                objBillingLineItemsProp.InvoiceDate = iBillingLineItemsRow.InvoiceDate;
                objBillingLineItemsProp.DebitAccountNumber = iBillingLineItemsRow.DebitAccountNumber;
                objBillingLineItemsProp.DebitProfitCenterNumber = iBillingLineItemsRow.DebitProfitCenterNumber;
                objBillingLineItemsProp.DebitCostCenterNumber = iBillingLineItemsRow.DebitCostCenterNumber;
                objBillingLineItemsProp.CreditAccountNumber = iBillingLineItemsRow.CreditAccountNumber;
                objBillingLineItemsProp.CreditProfitCenterNumber = iBillingLineItemsRow.CreditProfitCenterNumber;
                objBillingLineItemsProp.CreditCostCenterNumber = iBillingLineItemsRow.CreditCostCenterNumber;
                objBillingLineItemsProp.ARTransactionAmount = invoiceAmount;
                objBillingLineItemsProp.CreditMemoInd = iBillingLineItemsRow.CreditMemoInd;
                objBillingLineItemsProp.Description = iBillingLineItemsRow.Description;
                objBillingLineItemsProp.ExportedInd = iBillingLineItemsRow.ExportedInd;
                objBillingLineItemsProp.ExportBatchID = iBillingLineItemsRow.ExportBatchID;
                objBillingLineItemsProp.ExportedDate = iBillingLineItemsRow.ExportedDate;
                objBillingLineItemsProp.ExportedBy = iBillingLineItemsRow.ExportedBy;
                objBillingLineItemsProp.CreationDate = iBillingLineItemsRow.CreationDate;
                objBillingLineItemsProp.CreatedBy = iBillingLineItemsRow.CreatedBy;
                objBillingLineItemsProp.UpdatedDate = iBillingLineItemsRow.UpdatedDate;
                objBillingLineItemsProp.UpdatedBy = iBillingLineItemsRow.UpdatedBy;
                int billingLineItemsId = _serviceInstance.InsertBillingLineItems(objBillingLineItemsProp);

                if (billingLineItemsId > 0)
                { result = true; }

                if (!result)
                {
                    MessageBox.Show(Resources.msgBillingLineError);
                    return false;
                }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return result;
        }

        /// <summary>
        /// function getCode to manage the code response from database
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="codeDescription"></param>
        /// <returns>string</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        private string getCode(string codeType, string codeDescription)
        {
            string getCodeData = string.Empty;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var getCodeInfo = _serviceInstance.GetCodeDetailsForInvoice(codeType, codeDescription).ToList();

                if ((getCodeInfo != null) && getCodeInfo.Count >= 1)
                {
                    if (getCodeInfo.Count == 1)
                    {
                        getCodeData = getCodeInfo.FirstOrDefault().Code1;
                    }
                    else if (getCodeInfo.Count > 1)
                    {
                        getCodeData = "Error: Too Many";
                    }
                }
                else
                {
                    getCodeData = "Error: Not Found";
                }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return getCodeData;
        }

        #region Code To be removed after cleanup

        ///// <summary>
        ///// function getOutsideCarrierLegList to manage the outside carrier details
        ///// </summary>
        ///// <param name="billingId"></param>
        ///// <returns>string</returns>
        ///// <createdBy></createdBy>
        ///// <createdOn>May-31,2016</createdOn>
        //private bool getOutsideCarrierLegList(int billingId)
        //{
        //    bool isSuccessfull = false;
        //    decimal pvRatePercentage = 100;
        //    try
        //    {
        //        CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

        //        var list = _serviceInstance.GetLoadInfo(billingId, pvRatePercentage).Select(d => new LoadInfoListModel
        //        {
        //            OutsideCarrierID = d.OutsideCarrierID,
        //            VehicleID = d.VehicleID,
        //            LegsId = d.LegsID,
        //            LoadID = d.LoadID,
        //            ChargeRate = d.ChargeRate,
        //            OutsideCarrierBasePay = d.OutsideCarrierBasePay,
        //            OutsideCarrierFuelSCPay = d.OutsideCarrierFuelSCPay,
        //            OutsideCarrierTotalPay = d.OutsideCarrierTotalPay,
        //            OutsideCarrierName = d.OutsideCarrierName,
        //            LoadNumber = d.LoadNumber,
        //            ExportedInd = d.ExportedInd,
        //            ElectronicBillOfLadingInd = d.ElectronicBillOfLadingInd
        //        });
        //        objLoadInfoList = new List<LoadInfoListModel>(list);
        //        if ((objLoadInfoList == null) || (objLoadInfoList.Count == 0))
        //        {
        //            isSuccessfull = false;
        //        }
        //        else
        //        {
        //            isSuccessfull = true;
        //        }
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
        //    return isSuccessfull;
        //}

        #endregion




        /// <summary>
        /// function createMainInvoiceLineItem to manage the invoice details
        /// </summary>
        /// <param name="pvRecordId"></param>
        /// <param name="iInvoiceDataRow"></param>
        /// <returns>bool</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        private bool createMainInvoiceLineItem(int pvRecordId, BillingProp iInvoiceDataRow)
        {
            decimal? lGrossBrokerageRevenue = 0;
            decimal? iGrossBrokerageRevenue = 0;
            decimal? iMiscellaneousAdditive = 0;
            //Set current list iOutsideCarrierLegsList
            iBillingLineItemsRow = new BillingLineItemsModel();
            int iDAIOCSplitInd;
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (objLoadInfoList.Count > 0)
                {
                    iGrossBrokerageRevenue = objLoadInfoList[0].ChargeRate;
                    lGrossBrokerageRevenue = iGrossBrokerageRevenue;
                    if (iInvoiceDataRow.TransportCharges == lGrossBrokerageRevenue)
                    {
                        iGrossBrokerageRevenue = iInvoiceDataRow.InvoiceAmount;
                        return true;
                    }
                    else if (iInvoiceDataRow.FuelSurcharge > 0)     //;; &(iBulkBillingInd=0)
                    {
                        lGrossBrokerageRevenue = lGrossBrokerageRevenue + (iInvoiceDataRow.FuelSurcharge * (lGrossBrokerageRevenue / iInvoiceDataRow.TransportCharges));
                        iGrossBrokerageRevenue = lGrossBrokerageRevenue;
                    }
                    else
                    {
                        iGrossBrokerageRevenue = lGrossBrokerageRevenue;
                    }
                }
                else
                {
                    iGrossBrokerageRevenue = 0;
                }
                List<VehicleLegCountModel> getLegList = new List<VehicleLegCountModel>();
                var list = _serviceInstance.GetVehicleLegsInfo(pvRecordId).Select(d => new VehicleLegCountModel
                {
                    LegsCount1 = d.LegsCount1,
                    LegsCount2 = d.LegsCount2
                });
                getLegList = new List<VehicleLegCountModel>(list);
                if ((getLegList != null) && (getLegList.Count > 0)) //true
                {
                    var filterValue = getLegList.Where(x => x.LegsCount1 > 0 && x.LegsCount2 > 0).ToList();
                    if ((filterValue != null) && (filterValue.Count > 0))
                    {
                        iDAIOCSplitInd = 1;
                    }
                    else
                    {
                        iDAIOCSplitInd = 0;
                    }
                }
                else
                {
                    MessageBox.Show(Resources.msgGenrateInvoiceDetermining);
                    return false;
                }
                iBillingLineItemsRow.CustomerNumber = iCustomerCode;
                iBillingLineItemsRow.DebitAccountNumber = getCode("GLAccount", "Accounts Receivable Control");
                if ((iBillingLineItemsRow.DebitAccountNumber.Substring(0, 5)).ToLower(CultureInfo.CurrentCulture).Equals(Resources.msgError))
                {
                    MessageBox.Show(Resources.msgGenrateInvoiceAccountReceivable);
                    return false;
                }
                iBillingLineItemsRow.DebitProfitCenterNumber = string.Empty;
                iBillingLineItemsRow.DebitCostCenterNumber = string.Empty;
                iBillingLineItemsRow.CreditAccountNumber = iCustomerCode;
                iBillingLineItemsRow.CreditProfitCenterNumber = getCode("ProfitCenter", "Transportation");
                if ((iBillingLineItemsRow.CreditProfitCenterNumber.Substring(0, 5)).ToLower(CultureInfo.CurrentCulture).Equals(Resources.msgError))
                {
                    MessageBox.Show(Resources.msgGenrateInvoiceProfitCenter);
                    return false;
                }

                //Begin statement
                int GetCCNum = _serviceInstance.GetInvoiceCreditCostCenterNumber();
                iBillingLineItemsRow.CreditCostCenterNumber = GetCCNum.ToString();

                if ((string.IsNullOrEmpty(iBillingLineItemsRow.CreditCostCenterNumber)) || (Convert.ToInt32(iBillingLineItemsRow.CreditCostCenterNumber) == 0))
                {
                    MessageBox.Show(Resources.msgGenrateInvoiceCostCenterSales);
                    return false;
                }
                iBillingLineItemsRow.ARTransactionAmount = iInvoiceDataRow.InvoiceAmount - (iGrossBrokerageRevenue - iMiscellaneousAdditive);
                if (iBillingLineItemsRow.ARTransactionAmount == 0)
                {
                    return true;
                }
                iBillingLineItemsRow.Description = "Invoice For Transportation";
                //  insert the line
                BillingLineItemsProp objBillingLineItemsProp = new BillingLineItemsProp();
                objBillingLineItemsProp.CustomerID = iBillingLineItemsRow.CustomerID;
                objBillingLineItemsProp.BillingID = iBillingLineItemsRow.BillingID;
                objBillingLineItemsProp.CustomerNumber = iBillingLineItemsRow.CustomerNumber;
                objBillingLineItemsProp.InvoiceNumber = iBillingLineItemsRow.InvoiceNumber;
                objBillingLineItemsProp.InvoiceDate = iBillingLineItemsRow.InvoiceDate;
                objBillingLineItemsProp.DebitAccountNumber = iBillingLineItemsRow.DebitAccountNumber;
                objBillingLineItemsProp.DebitProfitCenterNumber = iBillingLineItemsRow.DebitProfitCenterNumber;
                objBillingLineItemsProp.DebitCostCenterNumber = iBillingLineItemsRow.DebitCostCenterNumber;
                objBillingLineItemsProp.CreditAccountNumber = iBillingLineItemsRow.CreditAccountNumber;
                objBillingLineItemsProp.CreditProfitCenterNumber = iBillingLineItemsRow.CreditProfitCenterNumber;
                objBillingLineItemsProp.CreditCostCenterNumber = iBillingLineItemsRow.CreditCostCenterNumber;
                objBillingLineItemsProp.ARTransactionAmount = iBillingLineItemsRow.ARTransactionAmount;
                objBillingLineItemsProp.CreditMemoInd = iBillingLineItemsRow.CreditMemoInd;
                objBillingLineItemsProp.Description = iBillingLineItemsRow.Description;
                objBillingLineItemsProp.ExportedInd = iBillingLineItemsRow.ExportedInd;
                objBillingLineItemsProp.ExportBatchID = iBillingLineItemsRow.ExportBatchID;
                objBillingLineItemsProp.ExportedDate = iBillingLineItemsRow.ExportedDate;
                objBillingLineItemsProp.ExportedBy = iBillingLineItemsRow.ExportedBy;
                objBillingLineItemsProp.CreationDate = iBillingLineItemsRow.CreationDate;
                objBillingLineItemsProp.CreatedBy = iBillingLineItemsRow.CreatedBy;
                objBillingLineItemsProp.UpdatedDate = iBillingLineItemsRow.UpdatedDate;
                objBillingLineItemsProp.UpdatedBy = iBillingLineItemsRow.UpdatedBy;
                int billingLineItemId = _serviceInstance.InsertBillingLineItems(objBillingLineItemsProp);
                if (billingLineItemId == 0)
                {
                    MessageBox.Show(Resources.msgBillingLineError);
                    return false;
                }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

            return true;
        }

        /// <summary>
        /// This method id used to open window print invoices.
        /// </summary>
        /// <param name="obj"></param>
        public void OpenPrintInvoice(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PrintInvoiceWindow objPrintInvoiceWindow = new PrintInvoiceWindow();
                objPrintInvoiceWindow.Owner = Application.Current.MainWindow;
                objPrintInvoiceWindow.ShowDialog();
            }
            catch (Exception ex)
            {
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
        /// Function to clear processed
        /// </summary>
        /// <param name="cutoffDate"></param>
        /// <returns></returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-31,2016</createdOn>
        public void ClearProcessed(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                var list = PortStorageInvoicesList.Where(x => x.Status.Trim() == "Processed").ToList();
                if (list.Count > 0)
                {
                    foreach (PortStorageInvoicesProp item in list)
                        PortStorageInvoicesList.Remove(item);
                }
            }
            catch (Exception ex)
            {
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
        /// This method used to send email functionlity for approve invoice
        /// </summary>
        /// <param name="customerName"></param>
        public static void SendMail(List<PortStorageInvoicesProp> list)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {


                string MailServer = ConfigurationManager.AppSettings["MailServer"];
                string FromEmailId = ConfigurationManager.AppSettings["FromEmailId"];
                string ToEmailId = ConfigurationManager.AppSettings["ToEmailId"];
                string Password = ConfigurationManager.AppSettings["Password"];
                int Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                if (!string.IsNullOrEmpty(MailServer) && !string.IsNullOrEmpty(FromEmailId) && !string.IsNullOrEmpty(ToEmailId) && !string.IsNullOrEmpty(Password) && Port > 0)
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        using (
                        SmtpClient SmtpServer = new SmtpClient(MailServer))
                        {
                            mail.From = new MailAddress(FromEmailId);
                            mail.To.Add(ToEmailId);
                            mail.Subject = "Generate Invoices";

                            //mail.Body += "Hi Team,";
                            mail.Body += "<table width='100%' style='border:Solid 1px Black;'>";
                            mail.Body += "<tr>";
                            mail.Body += "<td> <b>Customer Name</b>";
                            mail.Body += "</td>";
                            mail.Body += "<td> <b>Unit</b>";
                            mail.Body += "</td>";
                            mail.Body += "</tr>";
                            foreach (var item in list)
                            {
                                mail.Body += "<tr>";
                                mail.Body += "<td stlye='color:blue;'>" + item.CustName + "</td>" + "<td stlye='color:blue;'>" + item.Unit + "</td>";
                                mail.Body += "</tr>";
                            }
                            mail.Body += "</table>";

                            mail.Body += "<br /><br /><br />" + "These are above invoice has been generated successfully.<br /><br /><br /><br />" + "Thanks & Regards <br />" + "Appworks Team";
                            mail.IsBodyHtml = true;
                            SmtpServer.Port = Port;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(FromEmailId, Password);
                            SmtpServer.EnableSsl = true;
                            SmtpServer.Send(mail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                //CommonSettings.logger.LogError(this.GetType(), ex);
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
                    DelegateEventPortStorageInvoices.OnSetValueCutOffDate -= new DelegateEventPortStorageInvoices.SetValueCutOffDate(FindPortStorageInvoices);
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }

    public class PortStorageInvoicesMultiSelect : Behavior<RadGridView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            ((GeneratePortStorageInvoicesVM)this.AssociatedObject.DataContext).SelectedItems = this.AssociatedObject.SelectedItems;
        }
    }
}
