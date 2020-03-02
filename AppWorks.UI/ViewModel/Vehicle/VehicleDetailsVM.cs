using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using AppWorks.UI.Common;
using AppWorks.UI.Models;
using AppWorks.UI.View.UserControls.Vehicle;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using AppWorks.UI.Model;
using System.Globalization;
using AppWorksService.Properties;
using AppWorks.UI.Properties;
using Appworks.Reports;
using AppWorks.UI.Controls.Attributes;
using AppWorks.UI.Infrastructure;
using System.Collections.ObjectModel;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class VehicleDetailsVM : ChangeTrackingViewModel, IDisposable
    {
        string userCode = Application.Current.Properties["LoggedInUserName"].ToString();
        string VinNo = string.Empty;
        public VehicleDetailsVM()
        {
            EnabledNewUser = true;
            EnabledFind = true;
            EnabledPrint = true;
            EnabledCancel = false;
            EnabledModifyDelete = false;
            EnabledPrevNext = false;
            EnabledSaveUpdate = false;
            IsEditable = false;
            IsEditableStatus = false;
            IsEnabled = false;
            IsEnabledOnNewVIN = false;
            Action = Resources.ActionSave;
            SelectedVehicles = new ObservableCollection<VehicleInfoViewModel>();
            SelectedVehicles.CollectionChanged += SelectedVehicles_CollectionChanged;

            var data1 = _serviceInstance.GetDAIAddressName(userCode).Select(d => new RequestProcessingPrintModel
            {
                CompanyName = d.CompanyName,
                CompanyAddressLine1 = d.AddressLine1,
                CompanyCity = d.City,
                Phone = d.Phone
            }).FirstOrDefault();

            if (data1 != null)
            {
                CompanyName = data1.CompanyName;
                CompanyAddressLine1 = data1.CompanyAddressLine1;
                CompanyCity = data1.CompanyCity;
                CompanyPhone = data1.Phone;
            }


            ResetWindow();
            DelegateEventVehicle.OnSetValueEvt += new DelegateEventVehicle.SetValue(NotificationMessageReceived);

            DelegateEventCustomer.OnSetCustomerValueEvt += new DelegateEventCustomer.SetCustomerValue(GetCustomerValue);

            DelegateEventVehicle.OnSetValueListEvt += new DelegateEventVehicle.SetValueList(NotificationMessageReceivedList);

            DelegateEventVehicle.OnSetValueEvtCmd += new DelegateEventVehicle.SetValueCmd(GetCommandMode);

            DelegateEventVehicle.OnSetValueEvtDamageCodeCmd += new DelegateEventVehicle.SetValueDamageCode(RebindDamageGrid);

            DelegateEventVehicle.OnSetValueEvtVINDecodeCmd += new DelegateEventVehicle.SetValueVINDecode(GetVINCommandName);

            DelegateEventVehicle.OnSetValueEvtEnableNewCmd += new DelegateEventVehicle.SetValueEnableNew(GetEnableCommand);

            DelegateEventVehicle.OnSetValueEvtPopupCmd += new DelegateEventVehicle.SetValuePopup(GetPopupCommand);

            DelegateEventVehicle.OnSetValueEvtUpCmd += new DelegateEventVehicle.SetValueUpCmd(GetNextCommand);
            AcceptChanges();
        }

        private void SelectedVehicles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems.OfType<VehicleInfoViewModel>().Contains(SelectedVehicle))
                {
                    if (currentindex >= SelectedVehicles.Count)
                    {
                        currentindex = SelectedVehicles.Count - 1;
                    }
                    listVin.Remove(Convert.ToString(SelectedVehicle.VehicleId));
                    SelectedVehicle = currentindex >= 0 ? SelectedVehicles[currentindex] : null;
                }
            }
            NotifyPropertyChanged("IsMultiVehiclesSelected");
        }

        private void GetPopupCommand(string value)
        {
            string cmd = string.Empty;
            string popupValue = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                string[] popuparr = value.Split('_');
                if (popuparr.Count() > 0)
                {
                    cmd = popuparr[0];
                    popupValue = popuparr[1];
                }
            }

            if (cmd.ToLower().Equals("popup"))
            {
                FillTVehicalRecordStatus();
                FillUserDetailsinForm(popupValue);
                EnabledNewUser = false;
                EnabledFind = false;
                EnabledPrint = true;
                EnabledCancel = true;
                EnabledModifyDelete = false;
                EnabledPrevNext = false;
                EnabledSaveUpdate = true;
                IsEditable = true;
                IsEditableStatus = true;
                IsEnabled = true;
                IsEnabledOnNewVIN = true;
                Action = Resources.ActionUpdate;
            }
        }

        private void GetVINCommandName(string value)
        {
            if (!string.IsNullOrEmpty(Vin))
            {
                if (value.Equals("Decode"))
                {
                    DecodeVIN(null);
                }
            }
        }
        private void GetNextCommand(string value)
        {
            if (!string.IsNullOrEmpty(Vin))
            {
                if (value.Equals("Next"))
                {
                    NewVehicleDetail(null);
                }
            }
        }
        private void GetEnableCommand(string value)
        {
            if (value.Equals("EnableNew"))
            {
                EnabledNewUser = true;
                EnabledFind = true;
                EnabledPrint = true;
                EnabledCancel = false;
                EnabledModifyDelete = false;
                EnabledPrevNext = false;
                EnabledSaveUpdate = false;
                IsEditable = false;
                IsEditableStatus = false;
                IsEnabled = false;
                IsEnabledOnNewVIN = false;
                Action = Resources.ActionSave;
            }
        }
        public void NotificationMessageReceived(string vin)
        {
            //VinNo = vin;
            //UserCode = getUserCode;
            //tempUserCode = getUserCode;
            //FillUserDetails();
        }

        public void RebindDamageGrid(string vin)
        {
            //VinNo = vin;
            //UserCode = getUserCode;
            //tempUserCode = getUserCode;
            //FillUserDetails();
            //DelegateEventVehicle.OnSetValueEvtDamageCodeCmd -= new DelegateEventVehicle.SetValueDamageCode(RebindDamageGrid);

            BindVehicleDamageDetail(VehicleId);
        }

        private void GetCommandMode(string command)
        {
            if (command.ToLower(CultureInfo.CurrentCulture).Equals(Resources.btnEdit))
            {
                DisplayUserButton = Resources.MsgHidden;
                DisplayModifyButton = true;
            }
            else if (command.Equals("cancel", StringComparison.OrdinalIgnoreCase))
            {
                DelegateEventVehicle.SetValueMethodTab((int)Enums.PortStorageTabs.VehicleDetail);
                DelegateEventVehicle.SetValueMethodTabEnable(false);
                EnabledModifyDelete = false;
                EnabledPrint = false;
                ResetOnCancel();
            }
            else
            {
                DisplayModifyButton = false;
                DisplayUserButton = Resources.MsgVisible;
            }
        }
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
        private string displayUserButton;
        public string DisplayUserButton
        {
            get { return this.displayUserButton; }
            set { this.displayUserButton = value; NotifyPropertyChanged("DisplayUserButton"); }
        }
        private bool displayModifyButton;
        public bool DisplayModifyButton
        {
            get { return this.displayModifyButton; }
            set { this.displayModifyButton = value; NotifyPropertyChanged("DisplayModifyButton"); }
        }
        public void GetCustomerValue(CustomerInfo custInfo)
        {
            //IsModifyUser = true;
            //Action = Resources.ActionSave;
            //IsSaveUser = true;
            //IsNewUser = false;
            EnabledNewUser = false;
            EnabledFind = false;
            EnabledPrint = true;
            EnabledCancel = true;
            EnabledModifyDelete = false;
            EnabledPrevNext = false;
            EnabledSaveUpdate = true;
            IsEditable = true;
            IsEditableStatus = true;
            IsEnabledOnNewVIN = true;
            IsEnabled = true;
            PrevEntryRate = (decimal?)0.00;
            PrevPerDiemGraceDays = 0;
            if (VehicleId == 0)
            {
                Action = Resources.ActionSave;
            }
            CustomerName = custInfo.CustomerNameInfo;
            CustomerNumber = custInfo.CustomerCode;
            CustomerID = custInfo.CustomerID;
            EntryRate = custInfo.EntryRate;
            PerDiemGraceDays = custInfo.PerDiemGraceDays;
            DefaultEntryRate = (decimal?)0.00;
            DefaultPerDiemGraceDays = 0;
            PrevEntryRate = (decimal?)0.00;
            PrevPerDiemGraceDays = 0;

            //EntryRate=custInfo.
            //tempUserCode = getUserCode;
            //FillUserDetails();
            // set default value for vehicle details

            CreationDate = DateTime.Now;
            CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
        }


        public int currentindex = 0;
        List<string> listVin = new List<string>();


        public bool IsMultiVehiclesSelected
        {
            get { return SelectedVehicles.Count > 1; }
        }

        private VehicleInfoViewModel _selectedVehicle;
        public VehicleInfoViewModel SelectedVehicle
        {
            get { return _selectedVehicle; }
            set
            {
                _selectedVehicle = value;
                if (value != null)
                {
                    FillUserDetailsinForm(value.VehicleId.ToString());
                    currentindex = SelectedVehicles.IndexOf(value);
                }
                NotifyPropertyChanged("SelectedVehicle");
            }
        }

        /// <summary>
        /// Called when collection is changed and row is selected from Vehicle Locator UI
        /// </summary>
        /// <param name="Listpo"></param>
        public async void NotificationMessageReceivedList(List<VehicleLocatorVM> vehicles)
        {
            //check if VehicalStatusList is null then bind it
            if (VehicalStatusList == null)
            {
                FillTVehicalRecordStatus();
            }

            await App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                listVin = new List<string>();

                SelectedVehicles.Clear();

                foreach (VehicleLocatorVM item in vehicles)
                {
                    listVin.Add(item.VehicleId.ToString());
                    SelectedVehicles.Add(new VehicleInfoViewModel(item, SelectedVehicles));
                }

                SelectedVehicle = SelectedVehicles.FirstOrDefault();
                EnabledNewUser = true;
                EnabledFind = true;
                EnabledPrint = true;
                EnabledCancel = false;
                EnabledModifyDelete = true;
                EnabledSaveUpdate = false;
                IsEditable = false;
                IsEditableStatus = false;
                IsEnabled = false;
                IsEnabledOnNewVIN = false;
                Action = Resources.ActionUpdate;
                if (listVin.Count > 1)
                {
                    if (listVin.Count > 0)
                        EnabledPrevNext = true;
                    else
                        enabledPrevNext = false;
                    currentindex = 0;
                }
            }));
        }

        #region Commands
        private AppWorxCommand btnChangeCustomer;
        public AppWorxCommand BtnChangeCustomer
        {
            get
            {
                if (btnChangeCustomer == null)
                {
                    btnChangeCustomer = new AppWorxCommand(this.ChangeCustomer);
                }
                return btnChangeCustomer;
            }
        }

        private AppWorxCommand btnPri_Click;
        public AppWorxCommand BtnPri_Click
        {
            get
            {
                if (btnPri_Click == null)
                {
                    btnPri_Click = new AppWorxCommand(PreviousCommand_Executed, PreviousCommand_CanExecute);
                }
                return btnPri_Click;
            }
        }

        private AppWorxCommand btnNext_Click;
        public AppWorxCommand BtnNext_Click
        {
            get
            {
                if (btnNext_Click == null)
                {
                    btnNext_Click = new AppWorxCommand(this.NextCommand_Executed, NextCommand_CanExecute);
                }
                return btnNext_Click;
            }
        }

        private AppWorxCommand _btnFind_Click;
        public AppWorxCommand btnFind_Click
        {
            get
            {
                if (_btnFind_Click == null)
                {
                    _btnFind_Click = new AppWorxCommand(param => this.FindVehicle(),
                        param => CanCheck
                        );
                }
                return _btnFind_Click;
            }
        }

        private AppWorxCommand chkEntryrateChecked_Click;
        public AppWorxCommand ChkEntryrateChecked_Click
        {
            get
            {
                if (chkEntryrateChecked_Click == null)
                {
                    chkEntryrateChecked_Click = new AppWorxCommand(EntryRate_Chekced);
                }
                return chkEntryrateChecked_Click;
            }
        }

        private AppWorxCommand unchkEntryrateChecked_Click;
        public AppWorxCommand UnChkEntryrateChecked_Click
        {
            get
            {
                if (unchkEntryrateChecked_Click == null)
                {
                    unchkEntryrateChecked_Click = new AppWorxCommand(EntryRate_Unchekced);
                }
                return unchkEntryrateChecked_Click;
            }
        }

        private AppWorxCommand chkPerDiemChecked_Click;
        public AppWorxCommand ChkPerDiemChecked_Click
        {
            get
            {
                if (chkPerDiemChecked_Click == null)
                {
                    chkPerDiemChecked_Click = new AppWorxCommand(PerDiemDays_Chekced);
                }
                return chkPerDiemChecked_Click;
            }
        }

        private AppWorxCommand unchkPerDiemChecked_Click;
        public AppWorxCommand UnchkPerDiemChecked_Click
        {
            get
            {
                if (unchkPerDiemChecked_Click == null)
                {
                    unchkPerDiemChecked_Click = new AppWorxCommand(PerDiemDays_Unchekced);
                }
                return unchkPerDiemChecked_Click;
            }
        }

        private bool PreviousCommand_CanExecute(object obj)
        {
            return SelectedVehicles.Count > 1;
        }

        private void PreviousCommand_Executed(object obj)
        {
            if ((currentindex - 1) >= 0)
            {
                currentindex--;
                SelectedVehicle = SelectedVehicles[currentindex];
            }
            else
            {
                MessageBox.Show(Resources.msgVehiclePrevRecord);
            }
        }

        private bool NextCommand_CanExecute(object obj)
        {
            return SelectedVehicles.Count > 1;
        }

        private void NextCommand_Executed(object obj)
        {
            try
            {
                if ((currentindex + 1) < listVin.Count)
                {
                    currentindex++;
                    SelectedVehicle = SelectedVehicles[currentindex];
                }
                else
                {
                    MessageBox.Show(Resources.msgVehicleLastRecord);
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
        #endregion

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
        #endregion

        #region "Page Properties"

        private string vin;
        [ChangeTracking]
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

        private bool isStroke;
        public bool IsStroke
        {
            get { return isStroke; }
            set
            {
                if (value != null)
                {
                    this.isStroke = value;
                    NotifyPropertyChanged("IsStroke");
                }
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

        private string year;
        [ChangeTracking]
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
        [ChangeTracking]
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
        [ChangeTracking]
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
        [ChangeTracking]
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
                        // ClsValidationPopUp.ErrMsgPassword = Resources.ErrorCredentialLenght;
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
                if (value != null)
                {
                }
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

        private Nullable<DateTime> dtInBetwDtFrm;
        public Nullable<DateTime> DtInBetwDtFrm
        {
            get { return dtInBetwDtFrm; }
            set
            {
                this.dtInBetwDtFrm = value;
                NotifyPropertyChanged("DtInBetwDtFrm");
                if (value != null)
                {
                }
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
                if (value != null)
                {
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

        private DateTime? invoiceDate;
        public DateTime? InvoiceDate
        {
            get { return invoiceDate; }
            set
            {
                this.invoiceDate = value;
                NotifyPropertyChanged("InvoiceDate");
                if (value != null)
                {

                }
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
                if (value != null)
                {
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
        private string state;
        public string State
        {
            get { return this.state; }
            set { this.state = value; NotifyPropertyChanged("State"); }
        }
        private string zip;
        public string Zip
        {
            get { return this.zip; }
            set { this.zip = value; NotifyPropertyChanged("Zip"); }
        }


        /// Extra properties for insert vehicle details
        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set
            {
                this.customerID = value;
                NotifyPropertyChanged("CustomerID");
                if (value != null)
                {
                }
            }
        }

        private string bodystyle;
        [ChangeTracking]
        public string Bodystyle
        {
            get { return bodystyle; }
            set
            {
                this.bodystyle = value;
                NotifyPropertyChanged("Bodystyle");
                if (value != null)
                {
                }
            }
        }

        private string color;
        [ChangeTracking]
        public string Color
        {
            get { return color; }
            set
            {
                this.color = value;
                NotifyPropertyChanged("Color");
                if (value != null)
                {
                }
            }
        }

        private string vehicleLength;
        public string VehicleLength
        {
            get { return vehicleLength; }
            set
            {
                this.vehicleLength = value;
                NotifyPropertyChanged("VehicleLength");
                if (value != null)
                {
                }
            }
        }

        private string vehicleWidth;
        public string VehicleWidth
        {
            get { return vehicleWidth; }
            set
            {
                this.vehicleWidth = value;
                NotifyPropertyChanged("VehicleWidth");
                if (value != null)
                {
                }
            }
        }

        private string vehicleHeight;
        public string VehicleHeight
        {
            get { return vehicleHeight; }
            set
            {
                this.vehicleHeight = value;
                NotifyPropertyChanged("VehicleHeight");
                if (value != null)
                {
                }
            }
        }

        private string customerIdentification;
        public string CustomerIdentification
        {
            get { return customerIdentification; }
            set
            {
                this.customerIdentification = value;
                NotifyPropertyChanged("CustomerIdentification");
                if (value != null)
                {
                }
            }
        }

        private string sizeClass;
        public string SizeClass
        {
            get { return sizeClass; }
            set
            {
                this.sizeClass = value;
                NotifyPropertyChanged("SizeClass");
                if (value != null)
                {
                }
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
        private decimal? preventryRate;
        public decimal? PrevEntryRate
        {
            get { return preventryRate; }
            set
            {
                this.preventryRate = value;
                NotifyPropertyChanged("PrevEntryRate");
                if (value != null)
                {
                }
            }
        }

        private decimal? defaultEntryRate;
        public decimal? DefaultEntryRate
        {
            get { return defaultEntryRate; }
            set
            {
                this.defaultEntryRate = value;
                NotifyPropertyChanged("DefaultEntryRate");
            }
        }

        private decimal? defaultPerDiem;
        public decimal? DefaultPerDiem
        {
            get { return defaultPerDiem; }
            set
            {
                this.defaultPerDiem = value;
                NotifyPropertyChanged("DefaultPerDiem");
            }
        }
        private int prevPerDiemGraceDays;
        public int PrevPerDiemGraceDays
        {
            get { return prevPerDiemGraceDays; }
            set
            {
                this.prevPerDiemGraceDays = value;
                NotifyPropertyChanged("PrevPerDiemGraceDays");
            }
        }
        private int defaultPerDiemGraceDays;
        public int DefaultPerDiemGraceDays
        {
            get { return defaultPerDiemGraceDays; }
            set
            {
                this.defaultPerDiemGraceDays = value;
                NotifyPropertyChanged("DefaultPerDiemGraceDays");
            }
        }

        private bool entryRateOverrideInd;
        public bool EntryRateOverrideInd
        {
            get { return entryRateOverrideInd; }
            set
            {
                this.entryRateOverrideInd = value;
                NotifyPropertyChanged("EntryRateOverrideInd");
                if (value != null)
                {
                }
            }
        }

        private int? perDiemGraceDays;
        [ChangeTracking]
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

        private bool perDiemGraceDaysOverrideInd;
        public bool PerDiemGraceDaysOverrideInd
        {
            get { return perDiemGraceDaysOverrideInd; }
            set
            {
                this.perDiemGraceDaysOverrideInd = value;
                NotifyPropertyChanged("PerDiemGraceDaysOverrideInd");
            }
        }

        private string totalCharge;
        public string TotalCharge
        {
            get { return totalCharge; }
            set
            {
                this.totalCharge = value;
                NotifyPropertyChanged("TotalCharge");
                if (value != null)
                {
                }
            }
        }

        private string billedInd;
        public string BilledInd
        {
            get { return billedInd; }
            set
            {
                this.billedInd = value;
                NotifyPropertyChanged("BilledInd");
                if (value != null)
                {
                }
            }
        }        

        private int billingID;
        public int BillingID
        {
            get { return billingID; }
            set
            {
                this.billingID = value;
                NotifyPropertyChanged("BillingID");
                if (value != null)
                {
                }
            }
        }

        private DateTime? dateBilled;
        public DateTime? DateBilled
        {
            get { return dateBilled; }
            set
            {
                this.dateBilled = value;
                NotifyPropertyChanged("DateBilled");
                if (value != null)
                {
                }
            }
        }

        private bool vinDecodedInd;
        public bool VinDecodedInd
        {
            get { return vinDecodedInd; }
            set
            {
                this.vinDecodedInd = value;
                NotifyPropertyChanged("VinDecodedInd");
            }
        }

        private AppWorxCommand vinDecodeCheckBox_Checked;
        public AppWorxCommand VINDecodeCheckBox_Checked
        {
            get
            {
                if (vinDecodeCheckBox_Checked == null)
                {
                    vinDecodeCheckBox_Checked = new AppWorxCommand(this.VINDecode_Checked);
                }
                return vinDecodeCheckBox_Checked;
            }
        }

        private string note;
        [ChangeTracking]
        public string Note
        {
            get { return note; }
            set
            {
                this.note = value ?? string.Empty;
                NotifyPropertyChanged("Note");
            }
        }

        private string recordStatus;
        [ChangeTracking]
        public string RecordStatus
        {
            get { return recordStatus; }
            set
            {
                this.recordStatus = value;
                NotifyPropertyChanged("RecordStatus");
                if (value != null)
                {
                }
            }
        }

        private DateTime? creationDate;
        public DateTime? CreationDate
        {
            get { return creationDate; }
            set
            {
                this.creationDate = value;
                NotifyPropertyChanged("CreationDate");
                if (value != null)
                {
                }
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
                if (value != null)
                {
                }
            }
        }

        private DateTime? updatedDate;
        public DateTime? UpdatedDate
        {
            get { return updatedDate; }
            set
            {
                this.updatedDate = value;
                NotifyPropertyChanged("UpdatedDate");
                if (value != null)
                {
                }
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
                if (value != null)
                {
                }
            }
        }

        private string creditHoldInd;
        public string CreditHoldInd
        {
            get { return creditHoldInd; }
            set
            {
                this.creditHoldInd = value;
                NotifyPropertyChanged("CreditHoldInd");
                if (value != null)
                {
                }
            }
        }

        private string creditHoldBy;
        public string CreditHoldBy
        {
            get { return creditHoldBy; }
            set
            {
                this.creditHoldBy = value;
                NotifyPropertyChanged("CreditHoldBy");
                if (value != null)
                {
                }
            }
        }

        private string requestPrintedInd;
        public string RequestPrintedInd
        {
            get { return requestPrintedInd; }
            set
            {
                this.requestPrintedInd = value;
                NotifyPropertyChanged("RequestPrintedInd");
                if (value != null)
                {
                }
            }
        }

        private DateTime? estimatedPickupDate;
        [ChangeTracking]
        public DateTime? EstimatedPickupDate
        {
            get { return estimatedPickupDate; }
            set
            {
                this.estimatedPickupDate = value;
                NotifyPropertyChanged("EstimatedPickupDate");
                if (value != null)
                {
                }
            }
        }

        private DateTime? dealerPrintDate;
        [ChangeTracking]
        public DateTime? DealerPrintDate
        {
            get { return dealerPrintDate; }
            set
            {
                this.dealerPrintDate = value;
                NotifyPropertyChanged("DealerPrintDate");
                if (value != null)
                {
                }
            }
        }

        private string dealerPrintBy;
        public string DealerPrintBy
        {
            get { return dealerPrintBy; }
            set
            {
                this.dealerPrintBy = value;
                NotifyPropertyChanged("DealerPrintBy");
                if (value != null)
                {
                }
            }
        }

        private string requestedBy;
        [ChangeTracking]
        public string RequestedBy
        {
            get { return requestedBy; }
            set
            {
                this.requestedBy = value;
                NotifyPropertyChanged("RequestedBy");
                if (value != null)
                {
                }
            }
        }

        private string requestPrintedBatchID;
        public string RequestPrintedBatchID
        {
            get { return requestPrintedBatchID; }
            set
            {
                this.requestPrintedBatchID = value;
                NotifyPropertyChanged("RequestPrintedBatchID");
                if (value != null)
                {
                }
            }
        }

        private DateTime? dateRequestd;
        public DateTime? DateRequestd
        {
            get { return dateRequestd; }
            set
            {
                this.dateRequestd = value;
                NotifyPropertyChanged("DateRequestd");
                if (value != null)
                {
                }
            }
        }

        private DateTime? lastPhysicalDate;
        [ChangeTracking]
        public DateTime? LastPhysicalDate
        {
            get { return lastPhysicalDate; }
            set
            {
                this.lastPhysicalDate = value;
                NotifyPropertyChanged("LastPhysicalDate");
                if (value != null)
                {
                }
            }
        }

        private DateTime? dateIn;
        public DateTime? DateIn
        {
            get { return dateIn; }
            set
            {
                this.dateIn = value;
                NotifyPropertyChanged("DateIn");
            }
        }

        private DateTime? dateOut;
        public DateTime? DateOut
        {
            get { return dateOut; }
            set
            {
                this.dateOut = value;
                NotifyPropertyChanged("DateOut");
            }
        }


        private DateTime? requestPrintdDate;
        public DateTime? RequestPrintdDate
        {
            get { return requestPrintdDate; }
            set
            {
                this.requestPrintdDate = value;
                NotifyPropertyChanged("RequestPrintdDate");
            }
        }

        private int vehicleId;
        public int VehicleId
        {
            get { return vehicleId; }
            set
            {
                this.vehicleId = value;
                NotifyPropertyChanged("VehicleId");
                if (value != null)
                {
                }
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

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                this.isEnabled = value;
                NotifyPropertyChanged("IsEnabled");
            }
        }

        private bool isEnabledOnNewVIN;
        public bool IsEnabledOnNewVIN
        {
            get { return isEnabledOnNewVIN; }
            set
            {
                this.isEnabledOnNewVIN = value;
                NotifyPropertyChanged("IsEnabledOnNewVIN");
            }
        }

        private bool isEnabledDateIn;
        public bool IsEnabledDateIn
        {
            get { return isEnabledDateIn; }
            set
            {
                this.isEnabledDateIn = value;
                NotifyPropertyChanged("IsEnabledDateIn");
            }
        }


        private bool isEditableStatus;
        public bool IsEditableStatus
        {
            get { return isEditableStatus; }
            set
            {
                this.isEditableStatus = value;
                NotifyPropertyChanged("IsEditableStatus");
            }
        }
        #endregion

        #region "Page Events"
        private AppWorxCommand btnOk_Click;

        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnOk_Click
        {
            get
            {
                if (btnOk_Click == null)
                {
                    btnOk_Click = new AppWorxCommand(this.InsertVehicleDetails);
                }
                return btnOk_Click;
            }
        }



        private ICommand btnMultiVinClick;
        /// <summary>
        /// Submit button event
        /// </summary>
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

        private AppWorxCommand btnClear_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnClear_Click
        {
            get
            {
                if (btnClear_Click == null)
                {
                    btnClear_Click = new AppWorxCommand(this.Continue);
                }
                return btnClear_Click;
            }
        }

        private AppWorxCommand btnPrint_Click;
        /// <summary>
        /// print button event
        /// </summary>
        public AppWorxCommand BtnPrint_Click
        {
            get
            {
                if (btnPrint_Click == null)
                {
                    btnPrint_Click = new AppWorxCommand(this.OpenPrintDialog);
                }
                return btnPrint_Click;
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
                    btnCancel_Click = new AppWorxCommand(this.Cancle);
                }
                return btnCancel_Click;
            }
        }

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
                    btnNew_Click = new AppWorxCommand(this.NewVehicleDetail);
                }
                return btnNew_Click;
            }
        }

        public static bool CanCheck
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Submit button event
        /// </summary>
        private AppWorxCommand btnEditPerDiem_Click;
        public AppWorxCommand BtnEditPerDiem_Click
        {
            get
            {
                if (btnEditPerDiem_Click == null)
                {
                    btnEditPerDiem_Click = new AppWorxCommand(this.UpdatePerDiemDetails);
                }
                return btnEditPerDiem_Click;
            }
        }
        #endregion

        #region Observable Collections
        private ObservableCollection<VehicleModel> vehicleList;
        public ObservableCollection<VehicleModel> VehicleList
        {
            get { return vehicleList; }
            set { vehicleList = value; NotifyPropertyChanged("VehicleList"); }
        }

        public ObservableCollection<VehicleInfoViewModel> SelectedVehicles { get; private set; }

        #endregion

        private void VINDecode_Checked(object obj)
        {
            IsEnabled = selectedVehicalStatus.ToUpper() == "OUTGATED" ? false : !VinDecodedInd;
        }

        private void EntryRate_Chekced(object obj)
        {
            if (EntryRate != null)
                PrevEntryRate = EntryRate;
            else
                PrevEntryRate = (decimal?)0.00;

            EntryRate = (decimal?)0.00;
        }

        private void EntryRate_Unchekced(object obj)
        {
            if (PrevEntryRate > 0)
                EntryRate = PrevEntryRate;
            else
                EntryRate = DefaultEntryRate;
        }

        private void PerDiemDays_Chekced(object obj)
        {
            if (PerDiemGraceDays != null)
                PrevPerDiemGraceDays = (int)PerDiemGraceDays;
            else
                PrevPerDiemGraceDays = 0;
            PerDiemGraceDays = 0;
        }

        private void PerDiemDays_Unchekced(object obj)
        {
            if (PrevPerDiemGraceDays > 0)
                PerDiemGraceDays = PrevPerDiemGraceDays;
            else
                PerDiemGraceDays = DefaultPerDiemGraceDays;
        }

        private void OpenMultiVin()
        {
            // MultiVIN selctor = new MultiVIN();
            //selctor.Show();
        }

        /// <summary>
        /// Method To Populate the Record in Find User 
        /// </summary>
        private void FindVehicle()
        {
            try
            {
                DelegateEventVehicle.SetValueMethodTab((int)Enums.PortStorageTabs.VehicleLocator);
                DelegateEventVehicle.SetValueMethodTabEnable(true);
                DelegateEventVehicle.SetValueMethodRefreshCmd("Refresh");
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
        /// Function ChangeCustomer to change the customer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public void ChangeCustomer(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure to change the customer that this vehicle belongs to?", "Change Customer", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    CustomerLocator objCustLocator = new CustomerLocator();
                    objCustLocator.ShowDialog();
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



        public void InsertVehicleDetails(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                bool result = false;
                if (Action == Resources.ActionSave)
                {
                    if (!string.IsNullOrEmpty(Vin) && (!string.IsNullOrEmpty(CustomerName)))
                    {
                        VehicleProp objVehicleProp = new VehicleProp();

                        objVehicleProp.Model = Model;
                        objVehicleProp.CustomerNumber = CustomerNumber;

                        objVehicleProp.CustomerName = CustomerName;
                        objVehicleProp.InBetwDtFrm = InBetwDtFrm;
                        objVehicleProp.InBetwDtTo = InBetwDtTo;

                        objVehicleProp.DtInBetwDtFrm = DtInBetwDtFrm;
                        objVehicleProp.DtInBetwDtTo = DtInBetwDtTo;
                        objVehicleProp.InvoiceNumber = InvoiceNumber;
                        objVehicleProp.InvoiceDate = InvoiceDate;
                        objVehicleProp.DtRequestBetwDtFrm = DtRequestBetwDtFrm;
                        objVehicleProp.CustIndent = CustIndent;
                        objVehicleProp.DtRequestBetwDtTo = DtRequestBetwDtTo;
                        objVehicleProp.DtOutBetwDtFrm = DtOutBetwDtFrm;
                        objVehicleProp.DtOutBetwDtTo = DtOutBetwDtTo;



                        // insert data
                        if (CustomerID > 0)
                        {
                            objVehicleProp.CustomerID = Convert.ToInt32(CustomerID);
                        }
                        objVehicleProp.Bodystyle = Bodystyle;
                        //objVehicleProp.Color = Color;
                        objVehicleProp.VehicleLength = VehicleLength;
                        objVehicleProp.VehicleWidth = VehicleWidth;
                        objVehicleProp.VehicleHeight = VehicleHeight;
                        //objVehicleProp.CustomerIdentification = CustomerIdentification;
                        objVehicleProp.SizeClass = SizeClass;
                        //objVehicleProp.EntryRate =Convert.ToDecimal(EntryRate);

                        objVehicleProp.EntryRateOverrideInd = Convert.ToInt32(EntryRateOverrideInd);

                        //objVehicleProp.PerDiemGraceDays = PerDiemGraceDays;

                        objVehicleProp.PerDiemGraceDaysOverrideInd = Convert.ToInt32(PerDiemGraceDaysOverrideInd);

                        //objVehicleProp.TotalCharge =Convert.ToDecimal(TotalCharge);
                        if (!string.IsNullOrEmpty(BilledInd))
                        {
                            objVehicleProp.BilledInd = Convert.ToInt32(BilledInd);
                        }
                        objVehicleProp.BillingID = BillingID;
                        if (DateBilled != null)
                        {
                            objVehicleProp.DateBilled = DateBilled;
                        }
                        else
                        {
                            objVehicleProp.DateBilled = null;
                        }

                        objVehicleProp.VinDecodedInd = Convert.ToInt32(VinDecodedInd);

                        //objVehicleProp.Note = Note;
                        //objVehicleProp.RecordStatus = RecordStatus;
                        //objVehicleProp.CreationDate = CreationDate;
                        //objVehicleProp.CreatedBy = CreatedBy;
                        //objVehicleProp.UpdatedDate = UpdatedDate;
                        //objVehicleProp.UpdatedBy = UpdatedBy;
                        if (!string.IsNullOrEmpty(CreditHoldInd))
                        {
                            objVehicleProp.CreditHoldInd = Convert.ToInt32(CreditHoldInd);
                        }
                        objVehicleProp.CreditHoldBy = CreditHoldBy;
                        if (!string.IsNullOrEmpty(RequestPrintedInd))
                        {
                            objVehicleProp.RequestPrintedInd = Convert.ToInt32(RequestPrintedInd);
                        }
                        objVehicleProp.EstimatedPickupDate = EstimatedPickupDate;
                        objVehicleProp.DealerPrintDate = DealerPrintDate;
                        objVehicleProp.DealerPrintBy = DealerPrintBy;
                        //objVehicleProp.RequestedBy = RequestedBy;
                        if (!string.IsNullOrEmpty(RequestPrintedBatchID))
                        {
                            objVehicleProp.RequestPrintedBatchID = Convert.ToInt32(RequestPrintedBatchID);
                        }
                        objVehicleProp.DateRequestPrinted = RequestPrintdDate;
                        //objVehicleProp.LastPhysicalDate = LastPhysicalDate;

                        // New properties
                        objVehicleProp.Vin = Vin;
                        objVehicleProp.BayLocation = BayLocation;
                        objVehicleProp.Vin = Vin;
                        objVehicleProp.Year = Year;
                        objVehicleProp.Make = Make;
                        objVehicleProp.Model = Model;
                        objVehicleProp.VehicleStatus = VehicleStatus;
                        objVehicleProp.CustomerIdentification = CustomerIdentification;
                        if (EntryRate > 0)
                            objVehicleProp.EntryRate = Convert.ToDecimal(EntryRate);
                        objVehicleProp.DateIn = DateIn;
                        objVehicleProp.PerDiemGraceDays = PerDiemGraceDays;
                        objVehicleProp.DateRequested = DateRequestd;
                        if (TotalCharge != null)
                        {
                            decimal totalCharge;
                            decimal.TryParse(TotalCharge, out totalCharge);
                            if (totalCharge > 0)
                                objVehicleProp.TotalCharge = Convert.ToDecimal(TotalCharge);
                        }
                        objVehicleProp.RequestedBy = RequestedBy;
                        objVehicleProp.EstimatedPickupDate = EstimatedPickupDate;
                        objVehicleProp.RecordStatus = RecordStatus;
                        objVehicleProp.DateOut = DateOut;
                        //objVehicleProp.UpdatedDate = UpdatedDate;
                        objVehicleProp.LastPhysicalDate = LastPhysicalDate;
                        objVehicleProp.Color = Color;
                        objVehicleProp.CreationDate = CreationDate;
                        //objVehicleProp.UpdatedDate = UpdatedDate;
                        objVehicleProp.CreatedBy = CreatedBy;
                        //objVehicleProp.UpdatedBy = updatedBy;
                        objVehicleProp.Note = Note;
                        objVehicleProp.VehicleStatus = selectedVehicalStatus;



                        if (Vin.Length == 17)
                        {
                            bool isDuplicate = false;
                            isDuplicate = _serviceInstance.CheckMultipleVIN(Vin);
                            if (!isDuplicate)
                            {
                                if (!string.IsNullOrEmpty(Year))
                                {
                                    if (!string.IsNullOrEmpty(Make))
                                    {
                                        if (!string.IsNullOrEmpty(Model))
                                        {
                                            if (selectedVehicalStatus.ToUpper() == "REQUESTED")
                                            {
                                                if (DateIn != null)
                                                {
                                                    if (DateRequestd != null)
                                                    {
                                                        if (DateRequestd >= DateIn)
                                                        {
                                                            objVehicleProp.DateOut = null;
                                                            int data = _serviceInstance.InsertVehicleDetails(objVehicleProp);

                                                            if (data > 0)
                                                            {
                                                                MessageBox.Show(Resources.msgInsertedSuccessfully);
                                                                PrevEntryRate = EntryRate;
                                                                if (PerDiemGraceDays != null)
                                                                    PrevPerDiemGraceDays = (int)PerDiemGraceDays;
                                                                else
                                                                    PrevPerDiemGraceDays = 0;
                                                                //ResetWindow();
                                                                EnabledNewUser = true;
                                                                EnabledFind = true;
                                                                EnabledPrint = true;
                                                                EnabledCancel = false;
                                                                EnabledModifyDelete = true;
                                                                EnabledPrevNext = false;
                                                                EnabledSaveUpdate = false;
                                                                IsEditable = false;
                                                                IsEditableStatus = false;
                                                                IsEnabled = false;
                                                                IsEnabledOnNewVIN = false;
                                                                IsEnabledDateIn = false;
                                                                VehicleId = data;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Requested Date Cannot be smaller than Date In");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Please enter Requested date.");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Please enter Date In.");
                                                }

                                            }
                                            else if (selectedVehicalStatus.ToUpper() == "OUTGATED")
                                            {
                                                if (DateIn != null)
                                                {
                                                    if (DateRequestd != null)
                                                    {
                                                        if (DateOut != null)
                                                        {
                                                            if (DateRequestd >= DateIn)
                                                            {
                                                                if (DateOut >= DateRequestd)
                                                                {
                                                                    int data = _serviceInstance.InsertVehicleDetails(objVehicleProp);

                                                                    if (data > 0)
                                                                    {
                                                                        MessageBox.Show(Resources.msgInsertedSuccessfully);
                                                                        PrevEntryRate = EntryRate;
                                                                        if (PerDiemGraceDays != null)
                                                                            PrevPerDiemGraceDays = (int)PerDiemGraceDays;
                                                                        else
                                                                            PrevPerDiemGraceDays = 0;
                                                                        //ResetWindow();
                                                                        EnabledNewUser = true;
                                                                        EnabledFind = true;
                                                                        EnabledPrint = true;
                                                                        EnabledCancel = false;
                                                                        EnabledModifyDelete = true;
                                                                        EnabledPrevNext = false;
                                                                        EnabledSaveUpdate = false;
                                                                        IsEditable = false;
                                                                        IsEditableStatus = false;
                                                                        IsEnabled = false;
                                                                        IsEnabledOnNewVIN = false;
                                                                        IsEnabledDateIn = false;
                                                                        VehicleId = data;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Date Out Cannot be smaller than Date Requested");
                                                                }

                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Date Requested Cannot be smaller than Date In");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Please enter Date Out");

                                                        }

                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Please enter Date Requested.");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Please enter Date In.");
                                                }
                                            }
                                            else
                                            {
                                                int data = _serviceInstance.InsertVehicleDetails(objVehicleProp);

                                                if (data > 0)
                                                {
                                                    MessageBox.Show(Resources.msgInsertedSuccessfully);
                                                    PrevEntryRate = EntryRate;
                                                    if (PerDiemGraceDays != null)
                                                        PrevPerDiemGraceDays = (int)PerDiemGraceDays;
                                                    else
                                                        PrevPerDiemGraceDays = 0;
                                                    //ResetWindow();
                                                    EnabledNewUser = true;
                                                    EnabledFind = true;
                                                    EnabledPrint = true;
                                                    EnabledCancel = false;
                                                    EnabledModifyDelete = true;
                                                    EnabledPrevNext = false;
                                                    EnabledSaveUpdate = false;
                                                    IsEditable = false;
                                                    IsEditableStatus = false;
                                                    IsEnabled = false;
                                                    IsEnabledOnNewVIN = false;
                                                    IsEnabledDateIn = false;
                                                    VehicleId = data;
                                                }
                                            }

                                        }
                                        else
                                            MessageBox.Show(Resources.ReqModel);
                                    }
                                    else
                                        MessageBox.Show(Resources.ReqMake);

                                }
                                else
                                    MessageBox.Show(Resources.ReqYear);


                            }
                            else
                                MessageBox.Show(Resources.MsgDuplicateVIN);
                        }
                        else
                        {
                            MessageBox.Show(Resources.VINAlert);
                        }
                    }

                    else
                    {
                        MessageBox.Show(Resources.msgVINCustomerReq);
                    }

                }
                else if (Action == Resources.ActionUpdate)
                {
                    VehicleProp objVehicleProp = new VehicleProp();
                    objVehicleProp.CustomerID = CustomerID;
                    objVehicleProp.VehiclesID = VehicleId;
                    objVehicleProp.Vin = Vin;
                    objVehicleProp.BayLocation = BayLocation;
                    objVehicleProp.Vin = Vin;
                    objVehicleProp.Year = Year;
                    objVehicleProp.Make = Make;
                    objVehicleProp.Model = Model;
                    objVehicleProp.VehicleStatus = selectedVehicalStatus;
                    objVehicleProp.CustomerIdentification = CustomerIdentification;
                    objVehicleProp.EntryRate = EntryRate;
                    objVehicleProp.EntryRateOverrideInd = Convert.ToInt32(EntryRateOverrideInd);
                    objVehicleProp.DateIn = DateIn;
                    objVehicleProp.PerDiemGraceDays = PerDiemGraceDays;
                    objVehicleProp.PerDiemGraceDaysOverrideInd = Convert.ToInt32(PerDiemGraceDaysOverrideInd);
                    objVehicleProp.DateRequested = DateRequestd;
                    if (!string.IsNullOrEmpty(TotalCharge))
                    {
                        objVehicleProp.TotalCharge = Convert.ToDecimal(TotalCharge);
                    }
                    objVehicleProp.RequestedBy = RequestedBy;
                    objVehicleProp.EstimatedPickupDate = EstimatedPickupDate;
                    objVehicleProp.RecordStatus = recordStatus;
                    objVehicleProp.DateOut = dateOut;
                    objVehicleProp.LastPhysicalDate = LastPhysicalDate;
                    objVehicleProp.Color = Color;
                    objVehicleProp.UpdatedDate = DateTime.Now;
                    objVehicleProp.UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                    objVehicleProp.Note = Note;
                    objVehicleProp.DealerPrintDate = DealerPrintDate;
                    objVehicleProp.Bodystyle = Bodystyle;
                    objVehicleProp.InvoiceNumber = InvoiceNumber;
                    objVehicleProp.InvoiceDate = InvoiceDate;
                    objVehicleProp.RequestPrintedInd = RequestPrintedInd!=null?Convert.ToInt32(RequestPrintedInd):0;   // Set old RequestPrintInd during update
                    objVehicleProp.VinDecodedInd = Convert.ToInt32(VinDecodedInd);
                    if (!string.IsNullOrEmpty(Vin) && (!string.IsNullOrEmpty(CustomerName)))
                    {
                        if (Vin.Length == 17)
                        {
                            if (!string.IsNullOrEmpty(Year))
                            {
                                if (!string.IsNullOrEmpty(Make))
                                {
                                    if (!string.IsNullOrEmpty(Model))
                                    {

                                        if (selectedVehicalStatus.ToUpper() == "REQUESTED")
                                        {
                                            if (DateIn != null)
                                            {
                                                if (DateRequestd != null)
                                                {
                                                    if (DateRequestd >= DateIn)
                                                    {
                                                        objVehicleProp.VinDecodedInd = 0;
                                                        objVehicleProp.DateOut = null;
                                                        result = _serviceInstance.UpdateVehicalSearchDetails(objVehicleProp);
                                                        if (result)
                                                        {
                                                            FillUserDetailsinForm(VehicleId.ToString());
                                                            MessageBox.Show(Resources.msgUpdatedSuccessfully);
                                                            DelegateEventVehicle.SetValueMethodRefreshGrid("refresh");
                                                            PrevEntryRate = EntryRate;
                                                            if (PerDiemGraceDays != null)
                                                                PrevPerDiemGraceDays = (int)PerDiemGraceDays;
                                                            else
                                                                PrevPerDiemGraceDays = 0;
                                                            //ResetWindow();
                                                            EnabledNewUser = true;
                                                            EnabledFind = true;
                                                            EnabledPrint = true;
                                                            EnabledCancel = false;
                                                            EnabledModifyDelete = true;
                                                            if (listVin.Count > 1)
                                                                EnabledPrevNext = true;
                                                            EnabledSaveUpdate = false;
                                                            IsEditable = false;
                                                            IsEditableStatus = false;
                                                            IsEnabled = false;
                                                            IsEnabledOnNewVIN = false;
                                                            IsEnabledDateIn = false;

                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Requested Date Cannot be smaller than Date In");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Please enter Requested date.");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Please enter Date In.");
                                            }

                                        }
                                        else if (selectedVehicalStatus.ToUpper() == "OUTGATED")
                                        {
                                            if (DateIn != null)
                                            {
                                                if (DateRequestd != null)
                                                {
                                                    if (DateOut != null)
                                                    {
                                                        if (DateRequestd >= DateIn)
                                                        {
                                                            if (DateOut >= DateRequestd)
                                                            {
                                                                result = _serviceInstance.UpdateVehicalSearchDetails(objVehicleProp);
                                                                if (result)
                                                                {
                                                                    FillUserDetailsinForm(VehicleId.ToString());
                                                                    MessageBox.Show(Resources.msgUpdatedSuccessfully);
                                                                    DelegateEventVehicle.SetValueMethodRefreshGrid("refresh");
                                                                    PrevEntryRate = EntryRate;
                                                                    if (PerDiemGraceDays != null)
                                                                        PrevPerDiemGraceDays = (int)PerDiemGraceDays;
                                                                    else
                                                                        PrevPerDiemGraceDays = 0;
                                                                    //ResetWindow();
                                                                    EnabledNewUser = true;
                                                                    EnabledFind = true;
                                                                    EnabledPrint = true;
                                                                    EnabledCancel = false;
                                                                    EnabledModifyDelete = true;
                                                                    if (listVin.Count > 1)
                                                                        EnabledPrevNext = true;
                                                                    EnabledSaveUpdate = false;
                                                                    IsEditable = false;
                                                                    IsEditableStatus = false;
                                                                    IsEnabled = false;
                                                                    IsEnabledOnNewVIN = false;
                                                                    IsEnabledDateIn = false;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Date Out Cannot be smaller than Date Requested");
                                                            }

                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Date Requested Cannot be smaller than Date In");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Please enter Date Out");

                                                    }

                                                }
                                                else
                                                {
                                                    MessageBox.Show("Please enter Date Requested.");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Please enter Date In.");
                                            }
                                        }
                                        else
                                        {
                                            if (selectedVehicalStatus.ToUpper() == "ININVENTORY")
                                            {
                                                objVehicleProp.DateRequested = null;
                                                objVehicleProp.DateOut= null;
                                            }

                                            result = _serviceInstance.UpdateVehicalSearchDetails(objVehicleProp);
                                            if (result)
                                            {
                                                FillUserDetailsinForm(VehicleId.ToString());
                                                MessageBox.Show(Resources.msgUpdatedSuccessfully);
                                                DelegateEventVehicle.SetValueMethodRefreshGrid("refresh");
                                                PrevEntryRate = EntryRate;
                                                if (PerDiemGraceDays != null)
                                                    PrevPerDiemGraceDays = (int)PerDiemGraceDays;
                                                else
                                                    PrevPerDiemGraceDays = 0;
                                                //ResetWindow();
                                                EnabledNewUser = true;
                                                EnabledFind = true;
                                                EnabledPrint = true;
                                                EnabledCancel = false;
                                                EnabledModifyDelete = true;
                                                if (listVin.Count > 1)
                                                    EnabledPrevNext = true;
                                                EnabledSaveUpdate = false;
                                                IsEditable = false;
                                                IsEditableStatus = false;
                                                IsEnabled = false;
                                                IsEnabledOnNewVIN = false;
                                                IsEnabledDateIn = false;

                                            }
                                        }

                                    }
                                    else
                                        MessageBox.Show(Resources.ReqModel);
                                }
                                else
                                    MessageBox.Show(Resources.ReqMake);

                            }
                            else
                                MessageBox.Show(Resources.msgCalanderYearReq);
                        }
                        else
                        {
                            MessageBox.Show(Resources.VINAlert);
                        }

                    }
                    else
                    {
                        MessageBox.Show(Resources.msgVINCustomerReq);
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


        public void CreateCSVFile()
        {

            if (!Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString()))
            {
                Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString());
            }
            using (StreamWriter sw = new StreamWriter(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "PortStorageVehicles.csv", false))
            {

                VehicleModel objModel = new VehicleModel();
                PropertyInfo[] Props = typeof(VehicleModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                bool isDisplayNameAttributeDefined = false;
                foreach (PropertyInfo prop in Props)
                {
                    isDisplayNameAttributeDefined = Attribute.IsDefined(prop, typeof(DisplayNameAttribute));
                    if (isDisplayNameAttributeDefined)
                    {
                        DisplayNameAttribute dna = (DisplayNameAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute));
                        if (dna != null)
                            sw.Write(dna.DisplayName);
                        sw.Write(",");
                    }
                    else
                    {
                        sw.Write(prop.Name);
                    }
                    isDisplayNameAttributeDefined = false;
                }
                sw.Write(sw.NewLine);

                // Row creation
                foreach (VehicleModel prop in VehicleList)
                {
                    if (prop.VehiclesID != null)
                    {
                        sw.Write(prop.VehiclesID);
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
                        sw.Write(prop.DateIn);
                        sw.Write(",");
                    }
                    else
                    {
                        sw.Write("");
                        sw.Write(",");
                    }
                    if (prop.DateRequested != null)
                    {
                        sw.Write(prop.DateRequested);
                        sw.Write(",");
                    }
                    else
                    {
                        sw.Write("");
                        sw.Write(",");
                    }
                    if (prop.DateOut != null)
                    {
                        sw.Write(prop.DateOut);
                        sw.Write(",");
                    }
                    else
                    {
                        sw.Write("");
                        sw.Write(",");
                    }
                    if (prop.DateInDateOut != null)
                    {
                        sw.Write(prop.DateInDateOut);
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
                File.Open(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "PortStorageVehicles.csv", FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
        }


        public void Continue(object obj)
        {
            try
            {
                VehicleProp objVehicleProp = new VehicleProp();
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


        public void Clear(object obj)
        {
            try
            {
                AppWorksService.Properties.VehicleProp objVehicleProp = new AppWorksService.Properties.VehicleProp();
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

        public void Cancle(object obj)
        {
            try
            {
                //ResetWindow();
                EnabledNewUser = true;
                EnabledFind = true;
                EnabledCancel = false;
                if (listVin.Count > 1)
                    EnabledPrevNext = true;
                EnabledSaveUpdate = false;
                IsEditable = false;
                IsEditableStatus = false;
                IsEnabled = false;
                IsEnabledOnNewVIN = false;
                IsEnabledDateIn = false;
                if (VehicleId > 0)
                {
                    FillUserDetailsinForm(VehicleId.ToString());
                    EnabledPrint = true;
                    EnabledModifyDelete = true;
                }
                else
                {
                    EnabledPrint = false;
                    EnabledModifyDelete = false;
                    ResetOnCancel();
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

        /// <summary>
        /// Reset all fields on cancel button click
        /// </summary>
        private void ResetOnCancel()
        {
            CustomerName = string.Empty;
            CustomerNumber = string.Empty;
            Vin = string.Empty;
            Year = string.Empty;
            Make = string.Empty;
            Model = string.Empty;
            BayLocation = string.Empty;
            InBetwDtFrm = null;
            InBetwDtTo = null;
            VehicleStatus = string.Empty;
            DtInBetwDtFrm = null;
            DtInBetwDtTo = null;
            DateIn = null;
            DateOut = null;
            InvoiceNumber = string.Empty;
            DtRequestBetwDtFrm = null;
            CustIndent = string.Empty;
            DtRequestBetwDtTo = null;
            DtOutBetwDtFrm = null;
            DtOutBetwDtTo = null;
            InvoiceDate = null;
            CustomerID = 0;
            Bodystyle = string.Empty;
            Color = string.Empty;
            VehicleLength = string.Empty;
            VehicleWidth = string.Empty;
            VehicleHeight = string.Empty;
            CustomerIdentification = string.Empty;
            SizeClass = string.Empty;
            EntryRate = (decimal?)0.00;
            DefaultEntryRate = (decimal?)0.00;
            DefaultPerDiem = (decimal?)0.00;
            DefaultPerDiemGraceDays = 0;
            EntryRateOverrideInd = false;
            PerDiemGraceDays = 0;
            PerDiemGraceDaysOverrideInd = false;
            TotalCharge = "0.00";
            BilledInd = string.Empty;
            BillingID = 0;
            DateBilled = null;
            VinDecodedInd = false;
            Note = string.Empty;
            RecordStatus = string.Empty;
            CreationDate = null;
            CreatedBy = string.Empty;
            UpdatedDate = null;
            UpdatedBy = string.Empty;
            CreditHoldInd = string.Empty;
            CreditHoldBy = string.Empty;
            RequestPrintedInd = string.Empty;
            EstimatedPickupDate = null;
            DealerPrintDate = null;
            DealerPrintBy = string.Empty;
            RequestedBy = string.Empty;
            RequestPrintedBatchID = string.Empty;
            RequestPrintdDate = null;
            LastPhysicalDate = null;
            DateRequestd = null;
            VehicalStatusList = null;
            VehicleId = 0;
            DamageInfoList = null;
            VehicalHistoryList = null;
            SelectedVehicles.Clear();
            VehicleList = null;
            AcceptChanges();
        }

        /// <summary>
        /// Used to reset the window
        /// </summary>        
        private void ResetWindow()
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
            DateIn = null;
            DateOut = null;
            InvoiceNumber = string.Empty;
            DtRequestBetwDtFrm = null;
            CustIndent = string.Empty;
            DtRequestBetwDtTo = null;
            DtOutBetwDtFrm = null;
            DtOutBetwDtTo = null;
            // insert details prop
            CustomerID = 0;
            Bodystyle = string.Empty;
            Color = string.Empty;
            VehicleLength = string.Empty;
            VehicleWidth = string.Empty;
            VehicleHeight = string.Empty;
            CustomerIdentification = string.Empty;
            SizeClass = string.Empty;
            EntryRate = (decimal?)0.00;
            DefaultEntryRate = (decimal?)0.00;
            DefaultPerDiem = (decimal?)0.00;
            DefaultPerDiemGraceDays = 0;
            EntryRateOverrideInd = false;
            PerDiemGraceDays = 0;
            PerDiemGraceDaysOverrideInd = false;
            TotalCharge = "0.00";
            BilledInd = string.Empty;
            BillingID = 0;
            DateBilled = null;
            //VinDecodedInd = string.Empty;
            Note = string.Empty;
            RecordStatus = string.Empty;
            CreationDate = null;
            CreatedBy = string.Empty;
            UpdatedDate = null;
            UpdatedBy = string.Empty;
            CreditHoldInd = string.Empty;
            CreditHoldBy = string.Empty;
            RequestPrintedInd = string.Empty;
            EstimatedPickupDate = null;
            DealerPrintDate = null;
            DealerPrintBy = string.Empty;
            RequestedBy = string.Empty;
            RequestPrintedBatchID = string.Empty;
            RequestPrintdDate = null;
            LastPhysicalDate = null;
            DateRequestd = null;
            VehicalStatusList = null;
            VehicleId = 0;
            DamageInfoList = null;
            VehicalHistoryList = null;
            SelectedVehicles.Clear();
            VehicleList = null;
            AcceptChanges();
        }



        /// <summary>
        /// Function OpenPrintDialog to open the print window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public void OpenPrintDialog(object obj)
        {
            try
            {
                if (selectedVehicalStatus.Equals("Requested") || (selectedVehicalStatus.Equals("OutGated")))
                {
                    var report = new PrintRequestRPT();
                    //display the print popup;
                    PrintRequestReport objPrinModel = new PrintRequestReport();
                    objPrinModel.Color = Color;
                    objPrinModel.BayLocation = BayLocation;
                    objPrinModel.Vin = Vin;
                    objPrinModel.ShortVIN = Vin != "" && Vin != null ? "*" + Vin.Substring(Vin.Length - Math.Min(8, Vin.Length)) + "*" : "";
                    objPrinModel.DealerPrintDate = DealerPrintDate;
                    objPrinModel.DateRequested = DateRequestd;
                    objPrinModel.DateOut = DateOut;
                    objPrinModel.PickeupDate = EstimatedPickupDate;
                    objPrinModel.Name = CustomerName;
                    objPrinModel.AdddressLine1 = AddressLine1;
                    objPrinModel.FullAddress = (!string.IsNullOrEmpty(City) ? City + ", " : "") + State + " " + Zip;
                    objPrinModel.MakeModel = Year + " " + Make + " " + Model;
                    objPrinModel.CompanyName = CompanyName;
                    objPrinModel.CompanyAddressLine1 = CompanyAddressLine1;
                    objPrinModel.CompanyCity = CompanyCity;
                    objPrinModel.Phone = CompanyPhone;
                    if (DateRequestd != null)
                    {
                        objPrinModel.DayName = DateRequestd.Value.DayOfWeek.ToString();
                        objPrinModel.ReturnToInventory = DateRequestd.Value.AddDays(5);
                    }
                    if (EstimatedPickupDate != null)
                    {
                        objPrinModel.DayNamePickUp = EstimatedPickupDate.Value.DayOfWeek.ToString();
                    }
                    report.DataSource = objPrinModel;
                    MyReportSource = report;

                    PrintRequestVehicleReportWindow objPrint = new PrintRequestVehicleReportWindow(MyReportSource);
                    objPrint.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You cannot print the request report for a vehicle that has not been requested.");
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


        /// <summary>
        /// Function NewVehicleDetail to open the customer locator window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public void NewVehicleDetail(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                EnabledNewUser = true;
                EnabledFind = true;
                EnabledPrint = false;
                EnabledCancel = false;
                EnabledModifyDelete = false;
                EnabledPrevNext = false;
                EnabledSaveUpdate = false;
                IsEditable = false;
                IsEditableStatus = false;
                IsEnabled = false;
                IsEnabledOnNewVIN = true;
                IsEnabledDateIn = true;

                if (VehicleId == 0)
                {
                    Action = Resources.ActionSave;
                }

                ResetWindow();

                CreationDate = DateTime.Now;
                CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                DateIn = DateTime.Now;
                FillTVehicalRecordStatus();
                RecordStatus = Resources.StatusActive;
                //display the customer locator popup;
                CustomerLocator objCustLocator = new CustomerLocator();
                objCustLocator.Owner = Application.Current.MainWindow;
                objCustLocator.ShowDialog();
                //Focus on VIN textbox after selecting the record from New customer window
                DelegateEventVehicle.SetValueMethodFocusNext(false);
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
        /// Property To Bind the PerDiemInfoType
        /// <created>11 May 2016</created>
        /// </summary>
        private List<PerDiemPropInfo> perDiemInfoList;
        public List<PerDiemPropInfo> PerDiemInfoList
        {
            get { return perDiemInfoList; }
            private set
            {
                perDiemInfoList = value;
                NotifyPropertyChanged("PerDiemInfoList");
            }
        }

        /// <summary>
        /// Method To Bind the PerDiem Info In GridView
        /// </summary>
        /// <param name="obj"></param>
        public void BindPerDiemInfoList(int vehicleId)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                AppWorksService.Properties.PerDiemProp objPerDiemProp = new AppWorksService.Properties.PerDiemProp();
                var list = _serviceInstance.GetPerDiemVehicalDetails(vehicleId).Select(d => new PerDiemPropInfo
                {
                    PerDiemDateInfo = d.PerDiemDate,
                    PortStoragePerDiemIDInfo = d.PortStoragePerDiemID,
                    PerDiemValInfo = d.PerDiemVal,
                    PortStorageVehiclesIDInfo = d.PortStorageVehiclesID
                }).ToList();
                PerDiemInfoList = new List<PerDiemPropInfo>(list);

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
        /// Property To Bind the VehicalHistoryDetails
        /// <created>11 May 2016</created>
        /// </summary>
        private List<VehicalHistoryLocator> vehicalHistoryList;
        public List<VehicalHistoryLocator> VehicalHistoryList
        {
            get { return vehicalHistoryList; }
            set
            {
                vehicalHistoryList = value;
                NotifyPropertyChanged("VehicalHistoryList");
            }
        }


        /// <summary>
        /// Method To Bind the Vehical History Info In GridView
        /// </summary>
        /// <param name="obj"></param>
        private void BindVehicalHistoryInfoList(int VehicleId)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                AppWorksService.Properties.PortStorageLocationHistoryProp objPortStorageLocationHistoryProp = new AppWorksService.Properties.PortStorageLocationHistoryProp();
                var list = _serviceInstance.GetLocationHistory(VehicleId).Select(d => new VehicalHistoryLocator
                {
                    BayLocationInfo = d.BayLocation,
                    LocationDateInfo = d.LocationDate,
                    UpdatedbyInfo = d.Createdby      // Set UpdatedbyInfo
                }).ToList();
                VehicalHistoryList = new List<VehicalHistoryLocator>(list);

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
        /// Command To Call The Edit Damage Code.
        /// <create>11 May 2016</create>
        private AppWorxCommand btnEditDamageCode;
        public AppWorxCommand BtnEditDamageCode
        {
            get
            {
                if (btnEditDamageCode == null)
                {
                    btnEditDamageCode = new AppWorxCommand(this.EditDamageWindow);
                }
                return btnEditDamageCode;
            }
        }

        /// <summary>
        /// Function EditDamageWindow to open the damage code window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-9,2016</createdOn>
        public void EditDamageWindow(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //DamageCode objDamageCode = new DamageCode(Vin, VehicleId);
                //objDamageCode.ShowDialog();
                EditDamageCode objEditDamageCode = new EditDamageCode(((AppWorks.UI.ViewModel.Vehicle.DamagePropInfo)(obj)).DamageDetailId);
                objEditDamageCode.Owner = Application.Current.MainWindow;
                objEditDamageCode.ShowDialog();
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
        /// Command To Call The Add Damage Code.
        /// <create>11 May 2016</create>
        private AppWorxCommand btnAddDamageCode;
        public AppWorxCommand BtnAddDamageCode
        {
            get
            {
                if (btnAddDamageCode == null)
                {
                    btnAddDamageCode = new AppWorxCommand(this.AddDamageWindow);
                }
                return btnAddDamageCode;
            }
        }
        /// <summary>
        /// Property To Bind the PerDiemInfoType
        /// <created>11 May 2016</created>
        /// </summary>
        private List<DamagePropInfo> damageInfoList;
        public List<DamagePropInfo> DamageInfoList
        {
            get { return damageInfoList; }
            private set
            {
                damageInfoList = value;
                NotifyPropertyChanged("DamageInfoList");
            }
        }

        /// <summary>
        /// Function AddDamageWindow to open the damage code window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-9,2016</createdOn>
        public void AddDamageWindow(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                DamageCode objDamageCode = new DamageCode(Vin, VehicleId);
                objDamageCode.ShowDialog();
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


        private AppWorxCommand btnDecodeVIN;
        public AppWorxCommand BtnDecodeVIN
        {
            get
            {
                if (btnDecodeVIN == null)
                {
                    btnDecodeVIN = new AppWorxCommand(this.DecodeVIN);
                }
                return btnDecodeVIN;
            }
        }

        /// <summary>
        /// Function DecocdeVIN to decode the VIN number
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-9,2016</createdOn>
        public void DecodeVIN(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                bool focusNext = false;
                Make = string.Empty;
                Year = string.Empty;
                Model = string.Empty;
                Bodystyle = string.Empty;

                if (!string.IsNullOrEmpty(Vin))
                {
                    if (Vin.Length == 17)
                    {

                        var lstPsVehicle = _serviceInstance.GetDecodedPortStorageVIN(Vin);

                        if (lstPsVehicle != null)
                        {
                            Year = lstPsVehicle.VehicleYear;
                            Make = lstPsVehicle.Make;
                            Model = lstPsVehicle.Model;
                            Bodystyle = lstPsVehicle.Bodystyle;
                            VehicleLength = lstPsVehicle.VehicleLength;
                            VehicleWidth = lstPsVehicle.VehicleWidth;
                            VehicleHeight = lstPsVehicle.VehicleHeight;
                            //VinDecodedInd = "0";
                            focusNext = true;
                        }
                        else
                        {
                            var lstDecodeVin = _serviceInstance.GetDecodedVINForVINDecode(Vin);
                            if (lstDecodeVin != null)
                            {
                                Year = lstDecodeVin.VehicleYear;
                                Make = lstDecodeVin.Make;
                                Model = lstDecodeVin.Model;
                                Bodystyle = lstDecodeVin.Bodystyle;
                                VehicleLength = lstDecodeVin.VehicleLength;
                                VehicleWidth = lstDecodeVin.VehicleWidth;
                                VehicleHeight = lstDecodeVin.VehicleHeight;
                                focusNext = true;
                            }
                            else
                            {
                                //decode from web
                                var lstVINDataWeb = _serviceInstance.GetDecodeDataFromWeb(Vin, Bodystyle);
                                if (lstVINDataWeb != null)
                                {
                                    if (lstVINDataWeb.DecodeError.Length == 0)
                                    {
                                        Year = lstVINDataWeb.VehicleYear;
                                        Make = lstVINDataWeb.Make;
                                        Model = lstVINDataWeb.Model;
                                        Bodystyle = lstVINDataWeb.Bodystyle;
                                        VehicleLength = lstVINDataWeb.VehicleLength;
                                        VehicleWidth = lstVINDataWeb.VehicleWidth;
                                        VehicleHeight = lstVINDataWeb.VehicleHeight;
                                        focusNext = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show(lstVINDataWeb.DecodeError);
                                        focusNext = false;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show(Resources.ReqVIN17);
                        focusNext = false;
                    }
                }
                else
                {
                    MessageBox.Show(Resources.ReqVIN);
                    focusNext = false;
                }

                DelegateEventVehicle.SetValueMethodFocusNext(focusNext);
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
        [ChangeTracking]
        public string SelectedVehicalStatus
        {
            get { return selectedVehicalStatus; }
            set
            {
                selectedVehicalStatus = value;
                NotifyPropertyChanged("SelectedVehicalStatus");
            }
        }

        public void FillTVehicalRecordStatus()
        {
            try
            {
                List<string> List = new List<string>();
                List = _serviceInstance.VehicalStatusList().Where(x => x != null).ToList();
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

            }
        }

        /// <summary>
        /// Function FillUserDetailsinForm to fill the form details
        /// </summary>
        /// <param name="VIN"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-9,2016</createdOn>
        public async void FillUserDetailsinForm(string vehiclesID)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                VehicleProp objVehicleProp = new VehicleProp();
                objVehicleProp.VehiclesID = Convert.ToInt32(vehiclesID);
                BindVehicalHistoryInfoList(objVehicleProp.VehiclesID);

                var list = await _serviceInstance.CallVehialDetailsbyVINAsync(objVehicleProp);

                IEnumerable<VehicalRecordList> vehicleProp = new List<VehicalRecordList>();

                vehicleProp = list.Select(d => new VehicalRecordList
                {
                    BilledInd=d.BilledInd, //returns true if billed index is 1,
                    CustomerID = d.CustomerID,
                    VinBind = d.Vin,
                    CustomerBind = d.CustomerName,
                    CustomerNoBind = d.CustomerNumber,
                    BayLocationBind = d.BayLocation,
                    YearBind = d.Year,
                    MakeBind = d.Make,
                    ModelBind = d.Model,
                    BodyStyleBind = d.Bodystyle,
                    VehicalStatusBind = d.VehicleStatus,
                    CustomerIdntBind = d.CustomerIdentification,
                    EntryRateBind = d.EntryRate,
                    EntryRateIndBind = d.EntryRateOverrideInd,
                    PerDiemBind = d.PerDiemGraceDays,
                    PerDiemGraceDaysIndBind = d.PerDiemGraceDaysOverrideInd,
                    DateInBind = d.DateIn,
                    PerdiemGraceDayBind = d.PerDiemGraceDaysOverrideInd,
                    DefaultEntryRate = d.DefaultEntryRate,
                    DefaultPerDiem = d.DefaultPerDiem,
                    DefaultPerDiemGraceDays = d.DefaultPerDiemGraceDays,
                    DateRequestedBind = d.DateRequested,
                    TotalChangesBind = d.TotalCharge.ToString(),
                    RequestedByBind = d.RequestedBy,
                    InvoiceNoBind = d.InvoiceNumber,
                    EastPickUpDateBind = d.EstimatedPickupDate,
                    RecordStatusBind = d.RecordStatus,
                    DateOutBind = d.DateOut,
                    ColorBind = d.Color,
                    CreationDateBind = d.CreationDate,
                    CreatedByBind = d.CreatedBy,
                    UpdateDateBind = d.UpdatedDate,
                    UpdatedByBind = d.UpdatedBy,
                    LastPhysicalDateBind = d.LastPhysicalDate,
                    DealerPrintDateBind = d.DealerPrintDate,
                    NoteBind = d.Note,
                    AdddressLine1 = d.AdddressLine1,
                    City = d.City,
                    State = d.State,
                    Zip = d.Zip,
                    InvoiceDateBind = d.InvoiceDate,
                    VinDecodedInd = d.VinDecodedInd,
                    VehicleIdBind = (d.VehiclesID == null) ? 0 : Convert.ToInt32(d.VehiclesID),
                });

                VehicalRecordInfo = vehicleProp.ToList();
                BilledInd = VehicalRecordInfo[0].BilledInd.ToString();
                // Binding Control 
                CustomerID = VehicalRecordInfo[0].CustomerID;
                CustomerNumber = VehicalRecordInfo[0].CustomerNoBind;
                CustomerName = VehicalRecordInfo[0].CustomerBind;
                Vin = VehicalRecordInfo[0].VinBind;
                BayLocation = VehicalRecordInfo[0].BayLocationBind;
                Year = VehicalRecordInfo[0].YearBind;
                Make = VehicalRecordInfo[0].MakeBind;
                Model = VehicalRecordInfo[0].ModelBind;
                Bodystyle = VehicalRecordInfo[0].BodyStyleBind;
                SelectedVehicalStatus = VehicalRecordInfo[0].VehicalStatusBind;
                CustomerIdentification = VehicalRecordInfo[0].CustomerIdntBind;
                EntryRate = VehicalRecordInfo[0].EntryRateBind;
                EntryRateOverrideInd = Convert.ToBoolean(VehicalRecordInfo[0].EntryRateIndBind);
                DateIn = VehicalRecordInfo[0].DateInBind;
                VinDecodedInd = Convert.ToBoolean(VehicalRecordInfo[0].VinDecodedInd);
                PerDiemGraceDays = VehicalRecordInfo[0].PerDiemBind;
                PerDiemGraceDaysOverrideInd = Convert.ToBoolean(VehicalRecordInfo[0].PerDiemGraceDaysIndBind);
                DateRequestd = VehicalRecordInfo[0].DateRequestedBind;
                TotalCharge = VehicalRecordInfo[0].TotalChangesBind;
                RequestedBy = VehicalRecordInfo[0].RequestedByBind;
                InvoiceNumber = VehicalRecordInfo[0].InvoiceNoBind;
                DealerPrintDate = VehicalRecordInfo[0].DealerPrintDateBind;
                InvoiceDate = VehicalRecordInfo[0].InvoiceDateBind;
                EstimatedPickupDate = VehicalRecordInfo[0].EastPickUpDateBind;
                RecordStatus = VehicalRecordInfo[0].RecordStatusBind;
                DateOut = VehicalRecordInfo[0].DateOutBind;
                LastPhysicalDate = VehicalRecordInfo[0].LastPhysicalDateBind;
                Color = VehicalRecordInfo[0].ColorBind;
                CreationDate = VehicalRecordInfo[0].CreationDateBind;
                UpdatedDate = VehicalRecordInfo[0].UpdateDateBind;
                CreatedBy = VehicalRecordInfo[0].CreatedByBind;
                UpdatedBy = VehicalRecordInfo[0].UpdatedByBind;
                Note = VehicalRecordInfo[0].NoteBind;
                VehicleId = VehicalRecordInfo[0].VehicleIdBind;
                DefaultEntryRate = VehicalRecordInfo[0].DefaultEntryRate;
                DefaultPerDiem = VehicalRecordInfo[0].DefaultPerDiem;
                if (VehicalRecordInfo[0].DefaultPerDiemGraceDays != null)
                {
                    DefaultPerDiemGraceDays = (int)VehicalRecordInfo[0].DefaultPerDiemGraceDays;
                }
                AddressLine1 = VehicalRecordInfo[0].AdddressLine1;
                City = VehicalRecordInfo[0].City;
                State = VehicalRecordInfo[0].State;
                Zip = VehicalRecordInfo[0].Zip;
                RequestPrintedInd = Convert.ToString(list.Select(x => x.RequestPrintedInd).FirstOrDefault());    // Set existing RequestPrintInd status
                BindPerDiemInfoList(VehicleId);
                BindVehicleDamageDetail(VehicleId);
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

        private List<VehicalRecordList> vehicalRecordInfo;
        public List<VehicalRecordList> VehicalRecordInfo
        {
            get { return vehicalRecordInfo; }
            private set
            {
                vehicalRecordInfo = value;
                NotifyPropertyChanged("VehicalRecordInfo");
            }
        }

        public AppWorxCommand btnModify_Click;
        public AppWorxCommand BtnModify_Click
        {
            get
            {
                if (btnModify_Click == null)
                {
                    btnModify_Click = new AppWorxCommand(this.ModifyRecord);
                }
                return btnModify_Click;
            }
        }
        /// <summary>
        /// To Delete The Existing Record.
        /// </summary>
        /// <param name="obj"></param>
        /// 
        public AppWorxCommand btnDelete_Click;
        public AppWorxCommand BtnDelete_Click
        {
            get
            {
                if (btnDelete_Click == null)
                {
                    btnDelete_Click = new AppWorxCommand(this.DeleteRecord);
                }
                return btnDelete_Click;
            }
        }

        /// <summary>
        /// Used to delete a record from selected vehicle list
        /// </summary>
        /// <param name="obj"></param>
        public void DeleteRecord(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.MsgDeleteConfirm, Resources.msgTitleMessageBoxDelete, System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                bool result = false;
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {
                    VehicleProp objVehicleProp = new VehicleProp();
                    // creating the object of service property

                    objVehicleProp.VehiclesID = VehicleId;
                    if (objVehicleProp.VehiclesID > 0)
                    {
                        result = _serviceInstance.DeleteVehicalSearchDetails(objVehicleProp.VehiclesID);

                        if (result)
                        {
                            MessageBox.Show(Resources.msgDeleteSuccessfully);
                            EnabledNewUser = true;
                            EnabledFind = true;
                            EnabledPrint = true;
                            EnabledCancel = false;
                            EnabledModifyDelete = true;
                            EnabledPrevNext = true;
                            EnabledSaveUpdate = false;
                            IsEditable = false;
                            IsEditableStatus = false;
                            IsEnabled = false;
                            IsEnabledOnNewVIN = false;
                            IsEnabledDateIn = false;
                            Action = Resources.ActionUpdate;
                            var deletedSelectedVehicle = SelectedVehicles.FirstOrDefault(x => x.VehicleId == VehicleId);

                            var vinToDelete = listVin.FirstOrDefault(x => string.Equals(x, Convert.ToString(VehicleId), StringComparison.OrdinalIgnoreCase));

                            if (!string.IsNullOrEmpty(vinToDelete))
                            {
                                listVin.Remove(vinToDelete);
                            }

                            if (deletedSelectedVehicle != null)
                            { SelectedVehicles.Remove(deletedSelectedVehicle); }

                            //If there is not vehcile to delete then clear the window and reset buttons
                            if (SelectedVehicles.Count == 0)
                            {
                                ResetWindow();
                                EnabledModifyDelete = false;
                                EnabledPrevNext = false;
                                EnabledPrint = false;
                            }
                        }
                    }
                    else
                        MessageBox.Show(Resources.MsgSelectUser);

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
        public void ModifyRecord(object obj)
        {
            //bool result = false;
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
                IsEnabledDateIn = true;
                IsEnabledOnNewVIN = true;                
                IsEnabled = true;
                IsEditableStatus = true;
                Action = Resources.ActionUpdate;

                if (BilledInd.Equals("0", StringComparison.OrdinalIgnoreCase))
                {   
                    IsEditableStatus = true;
                }
                else 
                {
                    IsEditableStatus = false;
                    IsEnabledOnNewVIN = false;
                }

                if (selectedVehicalStatus.ToUpper() == "OUTGATED")
                {
                    IsEnabled = false;
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
        /// Method To update per diem details
        /// </summary>
        /// <param name="obj"></param>
        public void UpdatePerDiemDetails(object obj)
        {
            try
            {
                AppWorksService.Properties.PerDiemProp objPerDiemProp = new AppWorksService.Properties.PerDiemProp();
                string deimId = objPerDiemProp.PortStoragePerDiemID;
                //var list = _serviceInstance.UpdatePerDiemDetails().Select(d => new PerDiemPropInfo
                //{
                //    PerDiemDateInfo = d.PerDiemDate,
                //    PortStoragePerDiemIDInfo = d.PortStoragePerDiemID,
                //    PerDiemValInfo = d.PerDiemVal,
                //    PortStorageVehiclesIDInfo = d.PortStorageVehiclesID
                //}).ToList();
                //PerDiemInfoList = new List<PerDiemPropInfo>(list);               
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
            }
        }

        /// <summary>
        /// Method To Bind the demage details In GridView
        /// </summary>
        /// <param name="obj"></param>
        public void BindVehicleDamageDetail(int vehicleId)
        {
            try
            {
                AppWorksService.Properties.InspectionDamageProp objPerDiemProp = new AppWorksService.Properties.InspectionDamageProp();
                var list = _serviceInstance.BindVehicleDamageDetail(vehicleId).Select(d => new DamagePropInfo
                {
                    InsPDate = d.InsPDate,
                    InsPType = d.InsPType,
                    InsPBy = d.InsPBy,
                    Attened = d.Attened,
                    STI = d.STI,
                    CleanVehicleId = d.CleanVehicleId,
                    DamageCode = d.DamageCode,
                    DamageDetailId = d.DamageDetailId
                }).ToList();
                DamageInfoList = new List<DamagePropInfo>(list);
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DelegateEventVehicle.OnSetValueEvt -= new DelegateEventVehicle.SetValue(NotificationMessageReceived);
                    DelegateEventCustomer.OnSetCustomerValueEvt -= new DelegateEventCustomer.SetCustomerValue(GetCustomerValue);
                    DelegateEventVehicle.OnSetValueListEvt -= new DelegateEventVehicle.SetValueList(NotificationMessageReceivedList);
                    DelegateEventVehicle.OnSetValueEvtCmd -= new DelegateEventVehicle.SetValueCmd(GetCommandMode);
                    DelegateEventVehicle.OnSetValueEvtDamageCodeCmd -= new DelegateEventVehicle.SetValueDamageCode(RebindDamageGrid);
                    DelegateEventVehicle.OnSetValueEvtVINDecodeCmd -= new DelegateEventVehicle.SetValueVINDecode(GetVINCommandName);
                    DelegateEventVehicle.OnSetValueEvtEnableNewCmd -= new DelegateEventVehicle.SetValueEnableNew(GetEnableCommand);
                    DelegateEventVehicle.OnSetValueEvtPopupCmd -= new DelegateEventVehicle.SetValuePopup(GetPopupCommand);
                    DelegateEventVehicle.OnSetValueEvtUpCmd -= new DelegateEventVehicle.SetValueUpCmd(GetNextCommand);
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

    /// <summary>
    /// Property to Contains the Record of PerDiemInfo
    /// <create>11 May 2016</create>
    /// </summary>
    public class PerDiemPropInfo
    {
        public string PortStoragePerDiemIDInfo { get; set; }
        public Nullable<System.DateTime> PerDiemDateInfo { get; set; }
        public Nullable<decimal> PerDiemValInfo { get; set; }
        public Nullable<int> PortStorageVehiclesIDInfo { get; set; }
    }

    /// <summary>
    /// Property to Contains the Record of Vehical History Location
    /// <create>11 May 2016</create>
    /// </summary>
    public class VehicalHistoryLocator
    {
        public int PortStorageVehicalsIDInfo { get; set; }
        public string BayLocationInfo { get; set; }
        public Nullable<DateTime> LocationDateInfo { get; set; }
        public Nullable<DateTime> CreationDateInfo { get; set; }
        public string CreatedbyInfo { get; set; }
        public Nullable<DateTime> UpdatedDateInfo { get; set; }
        public string UpdatedbyInfo { get; set; }
    }


    public class VehicalRecordList
    {
        private string customerBind;
        public int BilledInd { get; set; }
        public int CustomerID { get; set; }
        public string CustomerBind { get; set; }
        public string VinBind { get; set; }
        public string CustomerNoBind { get; set; }
        public string BayLocationBind { get; set; }
        public string YearBind { get; set; }
        public string MakeBind { get; set; }
        public string ModelBind { get; set; }
        public string BodyStyleBind { get; set; }
        public string VehicalStatusBind { get; set; }
        public string CustomerIdntBind { get; set; }
        public decimal? EntryRateBind { get; set; }
        public int EntryRateIndBind { get; set; }
        public int? PerDiemBind { get; set; }
        public Nullable<DateTime> DateInBind { get; set; }
        public int PerdiemGraceDayBind { get; set; }
        public int PerDiemGraceDaysIndBind { get; set; }
        public Nullable<DateTime> DateRequestedBind { get; set; }
        public string TotalChangesBind { get; set; }
        public string RequestedByBind { get; set; }
        public string InvoiceNoBind { get; set; }
        public Nullable<DateTime> DealerPrintDateBind { get; set; }
        public Nullable<DateTime> InvoiceDateBind { get; set; }
        public Nullable<DateTime> EastPickUpDateBind { get; set; }
        public string RecordStatusBind { get; set; }
        public Nullable<DateTime> DateOutBind { get; set; }
        public Nullable<DateTime> LastPhysicalDateBind { get; set; }
        public string ColorBind { get; set; }
        public string NoteBind { get; set; }
        public Nullable<DateTime> CreationDateBind { get; set; }
        public Nullable<DateTime> UpdateDateBind { get; set; }
        public string CreatedByBind { get; set; }
        public string UpdatedByBind { get; set; }
        public int VehicleIdBind { get; set; }
        public decimal? DefaultEntryRate { get; set; }
        public decimal? DefaultPerDiem { get; set; }
        public int? DefaultPerDiemGraceDays { get; set; }
        public string AdddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int PerDiemGraceDaysOverrideInd { get; set; }
        public int EntryRateOverrideInd { get; set; }
        public int VinDecodedInd { get; set; }
    }

    /// <summary>
    /// Property to Contains the damage detils
    /// <create>11 May 2016</create>
    /// </summary>
    public class DamagePropInfo
    {
        public Nullable<System.DateTime> InsPDate { get; set; }
        public string InsPType { get; set; }
        public string InsPBy { get; set; }
        public int? Attened { get; set; }
        public string STI { get; set; }
        public int CleanVehicleId { get; set; }
        public string DamageCode { get; set; }
        public int DamageDetailId { get; set; }
        public string BayLocationInfo { get; set; }
        public Nullable<DateTime> LocationDateInfo { get; set; }
        public string UpdatedbyInfo { get; set; }
    }
}
