using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using AppWorks.UI.Common;
using AppWorks.UI.Models;
using System.IO;
using System.Reflection;
using System.Globalization;
using AppWorks.UI.Properties;
using AppWorks.UI.Model;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using AppWorksService.Properties;

namespace AppWorks.UI.ViewModel.Billing
{
    public class BillingFindVM : ViewModelBase, IDisposable
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
        public BillingFindVM()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //OutsideCarrierDdl(null);
                //DriverTypeDdl(null);
                InvoiceTypeDdl(null);
                InvoiceStatusDdl(null);
                DelegateEventBillingPeriod.OnSetValueEvt += new DelegateEventBillingPeriod.SetValue(NotificationMessageReceived);
                DelegateEventBillingPeriod.OnSetValueEvtTotalCountPagerCmd += new DelegateEventBillingPeriod.SetValueTotalCountPager(GetTotalPageCount);
                DelegateEventBillingPeriod.OnSetValuePageNumberCmd += new DelegateEventBillingPeriod.SetValuePageNumberClick(GetCurrentPageIndex);
                DelegateEventBillingPeriod.OnSetValueEvtRefreshCmd += new DelegateEventBillingPeriod.SetValueRefreshCmd(GetCommandName);
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
        /// 
        /// </summary>
        /// <param name="param"></param>
        public BillingFindVM(object param)
        {

        }

        private void GetCommandName(string value)
        {
            if (value.Equals("Refresh"))
            {
                BillingList = null;
            }
        }

        /// <summary>
        /// Method NotificationMessageReceived to perform the delegate event.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <cretedby></cretedby>
        /// <createddate>13-mar-2016</createddate>
        private void NotificationMessageReceived(string msg)
        {
            Vin = msg;
            vin = msg;
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

        public int BillingID { get; set; }


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
        private string invoiceStatus;
        public string InvoiceStatus
        {
            get { return invoiceStatus; }
            set
            {
                this.invoiceStatus = value;
                NotifyPropertyChanged("InvoiceStatus");
            }
        }


        private string invoiceType;
        public string InvoiceType
        {
            get { return invoiceType; }
            set
            {
                if (value != null)
                {
                    this.invoiceType = value;
                    NotifyPropertyChanged("InvoiceType");
                }
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
        private DateTime? invoiceDate;
        public DateTime? InvoiceDate
        {
            get { return invoiceDate; }
            set
            {
                this.invoiceDate = value;
                NotifyPropertyChanged("InvoiceDate");
            }
        }


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

        private Nullable<DateTime> createdBetwDtFrm;
        public Nullable<DateTime> CreatedBetwDtFrm
        {
            get { return createdBetwDtFrm; }
            set
            {
                this.createdBetwDtFrm = value;
                NotifyPropertyChanged("CreatedBetwDtFrm");
                if (value != null)
                {
                }
            }
        }

        private Nullable<DateTime> createdBetwDtTo;
        public Nullable<DateTime> CreatedBetwDtTo
        {
            get { return createdBetwDtTo; }
            set
            {
                this.createdBetwDtTo = value;
                NotifyPropertyChanged("CreatedBetwDtTo");
            }
        }


        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                this.address = value;
                NotifyPropertyChanged("Address");
            }
        }

        private Nullable<DateTime> dtInvBetwDtFrm;
        public Nullable<DateTime> DtInvBetwDtFrm
        {
            get { return dtInvBetwDtFrm; }
            set
            {
                this.dtInvBetwDtFrm = value;
                NotifyPropertyChanged("DtInvBetwDtFrm");
            }
        }

        private Nullable<DateTime> dtInvBetwDtTo;
        public Nullable<DateTime> DtInvBetwDtTo
        {
            get { return dtInvBetwDtTo; }
            set
            {
                this.dtInvBetwDtTo = value;
                NotifyPropertyChanged("DtInvBetwDtTo");
            }
        }

        #region PROPERTIES TO BE REMOVED AFTER CODE CLEANUP

        //private string loadNumber;
        //public string LoadNumber
        //{
        //    get { return loadNumber; }
        //    set
        //    {
        //        this.loadNumber = value;
        //        NotifyPropertyChanged("LoadNumber");
        //    }
        //}

        //private string poNumber;
        //public string PONumber
        //{
        //    get { return poNumber; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            this.poNumber = value;
        //            NotifyPropertyChanged("PONumber");
        //        }
        //    }
        //}

        //private string orderNumber;
        //public string OrderNumber
        //{
        //    get { return orderNumber; }
        //    set
        //    {
        //        this.orderNumber = value;
        //        NotifyPropertyChanged("OrderNumber");
        //    }
        //}

        //private string outsideCarrier;
        //public string OutsideCarrier
        //{
        //    get { return outsideCarrier; }
        //    set
        //    {
        //        this.outsideCarrier = value;
        //        NotifyPropertyChanged("OutsideCarrier");
        //    }
        //}

        //private string driver;
        //public string Driver
        //{
        //    get { return driver; }
        //    set
        //    {
        //        this.driver = value;
        //        NotifyPropertyChanged("Driver");
        //    }
        //}

        #endregion

        private Nullable<DateTime> dtPaidBetwDtFrm;
        public Nullable<DateTime> DtPaidBetwDtFrm
        {
            get { return dtPaidBetwDtFrm; }
            set
            {
                this.dtPaidBetwDtFrm = value;
                NotifyPropertyChanged("DtPaidBetwDtFrm");
            }
        }

        private Nullable<DateTime> dtPaidBetwDtTo;
        public Nullable<DateTime> DtPaidBetwDtTo
        {
            get { return dtPaidBetwDtTo; }
            set
            {
                this.dtPaidBetwDtTo = value;
                NotifyPropertyChanged("DtPaidBetwDtTo");
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

        private ObservableCollection<BillingModel> billingList;
        public ObservableCollection<BillingModel> BillingList
        {
            get { return billingList; }
            private set { billingList = value; NotifyPropertyChanged("BillingList"); }
        }

        private ObservableCollection<BillingModel> billingListExp;
        public ObservableCollection<BillingModel> BillingListExp
        {
            get { return billingListExp; }
            private set { billingListExp = value; NotifyPropertyChanged("BillingListExp"); }
        }

        private VehicleModel selectedDisRecipientGridItem;
        public VehicleModel SelectedDisRecipientGridItem
        {
            get { return selectedDisRecipientGridItem; }
            set { selectedDisRecipientGridItem = value; NotifyPropertyChanged("SelectedDisRecipientGridItem"); }
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
            BindGridDetails("GridBind");
        }

        /// <summary>
        /// Method BindGridDetails to bind the grid.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        public void BindGridDetails(object obj)
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
                // creating the object of service property
                AppWorksService.Properties.BillingProp objBillingProp = new AppWorksService.Properties.BillingProp();
                if (vin != null)
                {
                    objBillingProp.Vin = Vin;
                }
                else
                {
                    objBillingProp.Vin = string.Empty;
                }

                objBillingProp.CustomerName = CustomerName;
                objBillingProp.InvoiceStatus = SelectedInvoiceStatus;
                objBillingProp.InvoiceType = SelectedInvoiceType;
                objBillingProp.InvoiceNumber = InvoiceNumber;
                objBillingProp.Vin = Vin;
                objBillingProp.CustomerNumber = CustomerNumber;
                objBillingProp.CustIndent = CustIndent;
                objBillingProp.CreatedBetwDtFrm = CreatedBetwDtFrm;
                if (CreatedBetwDtTo != null)
                    objBillingProp.CreatedBetwDtTo = CreatedBetwDtTo.Value.AddDays(1);
                objBillingProp.DtInvBetwDtFrm = DtInvBetwDtFrm;
                if (DtInvBetwDtTo != null)
                    objBillingProp.DtInvBetwDtTo = DtInvBetwDtTo.Value.AddDays(1);
                objBillingProp.DtPaidBetwDtFrm = DtPaidBetwDtFrm;
                if (DtPaidBetwDtTo != null)
                    objBillingProp.DtPaidBetwDtTo = DtPaidBetwDtTo.Value.AddDays(1);
                objBillingProp.CurrentPageIndex = CurrentPageIndex;
                objBillingProp.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize;
                objBillingProp.DefaultPageSize = _GridPageSize;

                //objBillingProp.Driver = SelectedDriverList;
                //objBillingProp.OutsideCarrier = SelectedoutsideCarrier;
                //objBillingProp.LoadNumber = LoadNumber;
                //objBillingProp.OrderNumber = OrderNumber;
                //objBillingProp.PONumber = PONumber;

                var data = _serviceInstance.BillingSearch(objBillingProp);

                IEnumerable<BillingModel> model = new List<BillingModel>();

                model = data.Select(d => new BillingModel()
                {
                    BillingID = d.BillingID,
                    InvoiceNumber = d.InvoiceNumber,
                    CustomerName = d.CustomerName,
                    CustomerNumber = d.CustomerNumber,
                    VIN = d.Vin,
                    CustIndent = d.CustIndent,
                    InvoiceDate = d.InvoiceDate,
                    InvoiceStatus = d.InvoiceStatus,
                    InvoiceType = d.InvoiceType,
                    Address = d.CustomerAddress,
                    TotalPageCount = d.TotalPageCount
                    //LoadNumber = d.LoadNumber,
                    //OutsideCarrier = d.OutsideCarrier,
                    //Driver = d.Driver,
                    //CarrierName = d.CarrierName,
                    //PONumber = d.PONumber,
                    //OrderNumber = d.OrderNumber,

                });

                if (CurrentPageIndex == 0)
                {
                    BillingList = null;
                    BillingList = new ObservableCollection<BillingModel>(model);
                }
                else
                {
                    if (BillingList != null && BillingList.Count > 0)
                    {
                        foreach (BillingModel ud in new ObservableCollection<BillingModel>(model))
                        {
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                BillingList.Add(ud);
                            });
                        }
                    }
                }

                if (BillingList.ToList().Where(x => x.TotalPageCount > 0).FirstOrDefault() != null)
                {
                    Count = BillingList.ToList().Where(x => x.TotalPageCount > 0).FirstOrDefault().TotalPageCount;
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
                if (BillingList != null)
                {
                    BillingProp objBillingProp = new BillingProp();
                    if (vin != null)
                    {
                        objBillingProp.Vin = Vin;
                    }
                    else
                    {
                        objBillingProp.Vin = string.Empty;
                    }

                    objBillingProp.CustomerName = CustomerName;
                    objBillingProp.InvoiceStatus = SelectedInvoiceStatus;
                    objBillingProp.InvoiceType = SelectedInvoiceType;
                    objBillingProp.InvoiceNumber = InvoiceNumber;
                    objBillingProp.Vin = Vin;
                    objBillingProp.CustomerNumber = CustomerNumber;
                    objBillingProp.CustIndent = CustIndent;
                    objBillingProp.CreatedBetwDtFrm = CreatedBetwDtFrm;
                    objBillingProp.CreatedBetwDtTo = CreatedBetwDtTo;
                    objBillingProp.DtInvBetwDtFrm = DtInvBetwDtFrm;
                    objBillingProp.DtInvBetwDtTo = DtInvBetwDtTo;
                    objBillingProp.DtPaidBetwDtFrm = DtPaidBetwDtFrm;
                    objBillingProp.DtPaidBetwDtTo = DtPaidBetwDtTo;
                    objBillingProp.CurrentPageIndex = CurrentPageIndex;
                    objBillingProp.PageSize = 0;
                    objBillingProp.DefaultPageSize = _GridPageSize;

                    //objBillingProp.PONumber = PONumber;
                    //objBillingProp.OrderNumber = OrderNumber;
                    //objBillingProp.LoadNumber = LoadNumber;
                    //objBillingProp.OutsideCarrier = SelectedoutsideCarrier;
                    //objBillingProp.Driver = SelectedDriverList;

                    var data = _serviceInstance.BillingSearch(objBillingProp).Select(d => new BillingModel()
                    {
                        BillingID = d.BillingID,
                        CustomerName = d.CustomerName,

                        InvoiceDate = d.InvoiceDate,
                        InvoiceNumber = d.InvoiceNumber,
                        InvoiceStatus = d.InvoiceStatus,
                        InvoiceType = d.InvoiceType,
                        TotalPageCount = d.TotalPageCount
                        //LoadNumber = d.LoadNumber,
                        //OutsideCarrierID = d.OutsideCarrierID,
                        //Driver = d.Driver,
                        //CarrierName = d.CarrierName,

                    });

                    if (data != null)
                    {
                        BillingListExp = null;
                        BillingListExp = new ObservableCollection<BillingModel>(data);
                    }

                    if (BillingListExp.Count > 0)
                    {
                        string[] ExportValue = new string[] { "InvoiceNumber ", "CustomerName ", "Invoice Type", "Invoice Date", "Invoice Status" };
                        if (!Directory.Exists(ConfigurationManager.AppSettings["CSVFilePath"].ToString()))
                        {
                            Directory.CreateDirectory(ConfigurationManager.AppSettings["CSVFilePath"].ToString());
                        }
                        StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "BillingInvoice.csv", false);

                        foreach (string objVal in ExportValue)
                        {
                            sw.Write(objVal);
                            sw.Write(",");
                        }
                        sw.Write(sw.NewLine);

                        // Row creation
                        foreach (BillingModel prop in BillingListExp)
                        {
                            if (prop.InvoiceNumber != null)
                            {
                                sw.Write(prop.InvoiceNumber);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.CustomerName != null)
                            {
                                sw.Write(prop.CustomerName);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            //if (prop.CarrierName != null)
                            //{
                            //    sw.Write(prop.CarrierName);
                            //    sw.Write(",");
                            //}
                            //else
                            //{
                            //    sw.Write("");
                            //    sw.Write(",");
                            //}
                            if (prop.InvoiceType != null)
                            {
                                sw.Write(prop.InvoiceType);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.InvoiceDate != null)
                            {
                                sw.Write(prop.InvoiceDate.Value.Date.ToString("MM/dd/yyy"));
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            //if (prop.LoadNumber != null)
                            //{
                            //    sw.Write(prop.LoadNumber);
                            //    sw.Write(",");
                            //}
                            //else
                            //{
                            //    sw.Write("");
                            //    sw.Write(",");
                            //}
                            //if (prop.OutsideCarrierID != null)
                            //{
                            //    sw.Write(prop.OutsideCarrierID);
                            //    sw.Write(",");
                            //}
                            //else
                            //{
                            //    sw.Write("");
                            //    sw.Write(",");
                            //}
                            //if (prop.Driver != null)
                            //{
                            //    sw.Write(prop.Driver);
                            //    sw.Write(",");
                            //}
                            //else
                            //{
                            //    sw.Write("");
                            //    sw.Write(",");
                            //}
                            if (prop.InvoiceStatus != null)
                            {
                                sw.Write(prop.InvoiceStatus);
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
                        System.Diagnostics.Process.Start(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "BillingInvoice.csv");
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
                    ObservableCollection<object> objSel = (ObservableCollection<object>)SelectedItems;
                    List<BillingFindVM> objList = new List<BillingFindVM>();
                    foreach (BillingModel Vl in objSel)
                    {
                        BillingFindVM objBillingFind = new BillingFindVM(null);
                        objBillingFind.BillingID = Vl.BillingID;
                        objBillingFind.CustomerName = Vl.CustomerName;
                        objBillingFind.BillingID = Vl.BillingID;
                        objBillingFind.InvoiceNumber = Vl.InvoiceNumber;
                        objBillingFind.CustomerName = Vl.CustomerName;
                        objBillingFind.CustomerNumber = Vl.CustomerNumber;
                        objBillingFind.Vin = Vl.VIN;
                        objBillingFind.CustIndent = Vl.CustIndent;
                        objBillingFind.InvoiceDate = Vl.InvoiceDate;
                        objBillingFind.InvoiceStatus = Vl.InvoiceStatus;
                        objBillingFind.InvoiceType = Vl.InvoiceType;
                        objBillingFind.Address = Vl.Address;
                        TotalPageCount = Vl.TotalPageCount;
                        objList.Add(objBillingFind);
                        //objBillingFind.PONumber = Vl.PONumber;
                        //objBillingFind.OrderNumber = Vl.OrderNumber;
                        //objBillingFind.LoadNumber = Vl.LoadNumber;
                        //objBillingFind.OutsideCarrier = Vl.OutsideCarrier;
                        //objBillingFind.Driver = Vl.Driver;
                    }

                    DelegateEventBillingPeriod.SetValueListMethod(objList);

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.Title.Equals("Invoice Record Find", StringComparison.OrdinalIgnoreCase))
                        {
                            window.Close();
                        }
                    }
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
                Count = 0;
                CustomerName = string.Empty;
                SelectedInvoiceStatus = null;

                SelectedInvoiceType = null;
                InvoiceNumber = string.Empty;

                vin = string.Empty;
                CustomerNumber = string.Empty;

                CustIndent = string.Empty;
                CreatedBetwDtFrm = null;
                CreatedBetwDtTo = null;

                DtInvBetwDtFrm = null;
                DtInvBetwDtTo = null;

                DtPaidBetwDtFrm = null;
                DtPaidBetwDtTo = null;
                BillingList = null;

                InvoiceTypeDdl(null);
                InvoiceStatusDdl(null);
                SelectedInvoiceStatus = string.Empty;

                //SelectedDriverList = null;
                //SelectedoutsideCarrier = null;
                //OutsideCarrierDdl(null);
                //DriverTypeDdl(null);
                //LoadNumber = string.Empty;
                //OrderNumber = string.Empty;
                //PONumber = string.Empty;
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

        private List<string> invoiceStatusList;
        public List<string> InvoiceStatusList
        {
            get { return invoiceStatusList; }
            private set
            {
                invoiceStatusList = value;
                NotifyPropertyChanged("InvoiceStatusList");
            }
        }

        private string selectedInvoiceStatus;
        public string SelectedInvoiceStatus
        {
            get { return selectedInvoiceStatus; }
            set
            {
                selectedInvoiceStatus = value;
                NotifyPropertyChanged("SelectedInvoiceStatus");
            }
        }

        private List<string> invoiceTypeList;
        public List<string> InvoiceTypeList
        {
            get { return invoiceTypeList; }
            private set
            {
                invoiceTypeList = value;
                NotifyPropertyChanged("InvoiceTypeList");
            }
        }

        private string selectedInvoiceType;
        public string SelectedInvoiceType
        {
            get { return selectedInvoiceType; }
            set
            {
                selectedInvoiceType = value;
                NotifyPropertyChanged("SelectedInvoiceType");
            }
        }

        #region PROPERTIES TO BE REMOVED AFTER CODE CLEANUP

        //private List<string> driverList;
        //public List<string> DriverList
        //{
        //    get { return driverList; }
        //    private set
        //    {
        //        driverList = value;
        //        NotifyPropertyChanged("DriverList");
        //    }
        //}

        //private string selectedDriverList;
        //public string SelectedDriverList
        //{
        //    get { return selectedDriverList; }
        //    set
        //    {
        //        selectedDriverList = value;
        //        NotifyPropertyChanged("SelectedDriverList");
        //    }
        //}



        //private List<string> outsideCarrierList;
        //public List<string> OutsideCarrierList
        //{
        //    get { return outsideCarrierList; }
        //    private set
        //    {
        //        outsideCarrierList = value;
        //        NotifyPropertyChanged("OutsideCarrierList");
        //    }
        //}

        //private string selectedOutsideCarrier;
        //public string SelectedoutsideCarrier
        //{
        //    get { return selectedOutsideCarrier; }
        //    set
        //    {
        //        selectedOutsideCarrier = value;
        //        NotifyPropertyChanged("SelectedoutsideCarrier");
        //    }
        //}

        #endregion


        /// <summary>
        /// Method FillTVehicalRecordStatus to fill the vehicle status dropdown.
        /// </summary>
        /// <param name="NA"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        public void InvoiceStatusDdl(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                List<string> List = _serviceInstance.LoadCodeList("InvoiceStatus").Select(x => x.Description).ToList();
                List.Insert(0, "All");
                InvoiceStatusList = null;
                InvoiceStatusList = List;
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
        /// Method FillTVehicalRecordStatus to fill the vehicle status dropdown.
        /// </summary>
        /// <param name="NA"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        public void InvoiceTypeDdl(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                List<string> List = _serviceInstance.LoadCodeList("InvoiceType").Select(x => x.Description).ToList();
                List.Insert(0, "All");
                InvoiceTypeList = null;
                InvoiceTypeList = List;
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


        #region CODE TO BE REMOVED AFTER CLEANUP

        ///// <summary>
        ///// Method FillTVehicalRecordStatus to fill the vehicle status dropdown.
        ///// </summary>
        ///// <param name="NA"></param>
        ///// <returns>void</returns>
        ///// <createdBy></createdBy>
        ///// <createdOn>Apr-13,2016</createdOn>
        //public void DriverTypeDdl(object obj)
        //{
        //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {

        //        List<string> List = _serviceInstance.DriverList().ToList();
        //        List.Insert(0, "All");
        //        DriverList = null;
        //        DriverList = List;

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
        ///// Method FillTVehicalRecordStatus to fill the vehicle status dropdown.
        ///// </summary>
        ///// <param name="NA"></param>
        ///// <returns>void</returns>
        ///// <createdBy></createdBy>
        ///// <createdOn>Apr-13,2016</createdOn>
        //public void OutsideCarrierDdl(object obj)
        //{
        //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {

        //        List<string> List = _serviceInstance.OutsideCarrier().ToList();
        //        List.Insert(0, "All");
        //        OutsideCarrierList = null;
        //        OutsideCarrierList = List;

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
                ObservableCollection<object> objSel = (ObservableCollection<object>)SelectedItems;
                List<BillingFindVM> objList = new List<BillingFindVM>();
                foreach (BillingModel Vl in objSel)
                {
                    BillingFindVM objBillingFind = new BillingFindVM(null);
                    objBillingFind.BillingID = Vl.BillingID;
                    objBillingFind.CustomerName = Vl.CustomerName;
                    objBillingFind.BillingID = Vl.BillingID;
                    objBillingFind.InvoiceNumber = Vl.InvoiceNumber;
                    objBillingFind.CustomerName = Vl.CustomerName;
                    objBillingFind.CustomerNumber = Vl.CustomerNumber;
                    objBillingFind.Vin = Vl.VIN;
                    objBillingFind.CustIndent = Vl.CustIndent;

                    objBillingFind.InvoiceDate = Vl.InvoiceDate;
                    objBillingFind.InvoiceStatus = Vl.InvoiceStatus;
                    objBillingFind.InvoiceType = Vl.InvoiceType;


                    objBillingFind.Address = Vl.Address;
                    TotalPageCount = Vl.TotalPageCount;
                    objList.Add(objBillingFind);

                    //objBillingFind.Driver = Vl.Driver;
                    //objBillingFind.LoadNumber = Vl.LoadNumber;
                    //objBillingFind.OutsideCarrier = Vl.OutsideCarrier;
                    //objBillingFind.PONumber = Vl.PONumber;
                    //objBillingFind.OrderNumber = Vl.OrderNumber;
                    //objBillingFind.CarrierName = Vl.CarrierName;
                }

                DelegateEventBillingPeriod.SetValueListMethod(objList);
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
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DelegateEventBillingPeriod.OnSetValueEvt -= new DelegateEventBillingPeriod.SetValue(NotificationMessageReceived);
                    DelegateEventBillingPeriod.OnSetValueEvtTotalCountPagerCmd -= new DelegateEventBillingPeriod.SetValueTotalCountPager(GetTotalPageCount);
                    DelegateEventBillingPeriod.OnSetValuePageNumberCmd -= new DelegateEventBillingPeriod.SetValuePageNumberClick(GetCurrentPageIndex);
                    DelegateEventBillingPeriod.OnSetValueEvtRefreshCmd -= new DelegateEventBillingPeriod.SetValueRefreshCmd(GetCommandName);
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


    public class BillingFindMultiSelect : Behavior<RadGridView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            ((BillingFindVM)this.AssociatedObject.DataContext).SelectedItems = this.AssociatedObject.SelectedItems;
        }
    }
}
