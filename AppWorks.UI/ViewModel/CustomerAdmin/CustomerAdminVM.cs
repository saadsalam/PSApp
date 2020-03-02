using AppWorks.UI.Common;
using AppWorks.UI.Controls.Attributes;
using AppWorks.UI.Properties;
using AppWorks.UI.View.UserControls;
using AppWorks.UI.View.UserControls.CustomerAdmin;
using AppWorks.UI.View.UserControls.Vehicle;
using AppWorks.UI.ViewModel.Location;
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

namespace AppWorks.UI.ViewModel.CustomerAdmin
{
    public class CustomerAdminVM : ChangeTrackingViewModel
    {
        int _GridPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]);
        int _GridPageSizeOnScroll = Convert.ToInt32(ConfigurationManager.AppSettings["FindUserGridPageSizeOnScroll"]);
        public CustomerAdminVM()
        {
            Visibility = Resources.MsgHidden;
            EnabledCancel = false;
            EnabledDelete = false;
            EnabledFind = true;
            EnabledModify = false;
            EnabledNew = true;
            EnabledSaveUpdate = false;
            IsAddRateEnabled = false;
            EnableModifyDeleteRate = false;
            Text = Resources.btnSave;
            DelegateEventCustomer.OnSetCustomerValueEvt += new DelegateEventCustomer.SetCustomerValue(GetCustomerValue);
            LocationDeligate.OnSetValueEvtCmd += new LocationDeligate.SetValueCmd(NotificationCmdReceivedToUpdate);

            CustomerAdminDeligate.OnSetUpdateAddressValueEvt += new CustomerAdminDeligate.SetUpdateAddressValue(NotificationCmdReceivedToUpdateAddress);
            CustomerAdminDeligate.OnSetValuePageNumberCmd += CustomerAdminDeligate_OnSetValuePageNumberCmd;
            RateAdminDelegate.OnNotifyBindEvt += RateAdminDelegate_OnNotifyBindEvt;
            //CustomerAdminDeligate.SetValueBillStreetAddrMethod(SelectedItems);
            //CustomerAdminDeligate.OnSetCustomerAdminPropertiesValueEvt += new CustomerAdminDeligate.SetCustomerAdminPropertiesValue(this.IsReadOnly);
            BindPortStorageRatesGrid(null);
            AcceptChanges();

        }
       

        private void NotificationCmdReceivedToUpdate(string command)
        {
            if (command.Equals("UpdateList", StringComparison.OrdinalIgnoreCase))
            {
                GetCustomerValue(null);
                AcceptChanges();
            }
        }

        private void NotificationCmdReceivedToUpdateAddress(string command)
        {
            if (command.Equals("Update", StringComparison.OrdinalIgnoreCase))
            {
                if (Application.Current.Resources["CustomerAdminID"] != null)
                    CustomerID = Convert.ToInt32(Application.Current.Resources["CustomerAdminID"]);
                GetCustomerByID(CustomerID);
                AcceptChanges();
            }
        }

        #region "BillingAddress Properties"
        private List<LocationList> listBillingAddress;

        public List<LocationList> ListBillingAddress
        {
            get
            {
                return listBillingAddress;
            }
            private set
            {
                this.listBillingAddress = value;
                NotifyPropertyChanged("ListBillingAddress");
            }
        }

        private bool isAddRateEnabled;
        public bool IsAddRateEnabled
        {
            get { return isAddRateEnabled; }
            set
            {
                if (value != null)
                    isAddRateEnabled = value;
                NotifyPropertyChanged("IsAddRateEnabled");
            }
        }

        private bool enableModifyDeleteRate;
        public bool EnableModifyDeleteRate
        {
            get { return enableModifyDeleteRate; }
            set
            {
                if (value != null)
                    enableModifyDeleteRate = value;
                NotifyPropertyChanged("EnableModifyDeleteRate");
            }
        }

        private int? locationId;
        public int? LocationId
        {
            get { return locationId; }
            set
            {
                if (value != null)
                    locationId = value;
                NotifyPropertyChanged("LocationId");
            }
        }
        private string locationName;
        public string LocationName
        {
            get { return locationName; }
            set
            {
                if (value != null)
                    locationName = value;
                NotifyPropertyChanged("LocationName");
            }
        }
        private string addressLine1;
        public string AddressLine1
        {
            get { return addressLine1; }
            set
            {
                if (value != null)
                    addressLine1 = value;
                NotifyPropertyChanged("AddressLine1");
            }
        }
        private string addressLine2;
        public string AddressLine2
        {
            get { return addressLine2; }
            set
            {
                if (value != null)
                    addressLine2 = value;
                NotifyPropertyChanged("AddressLine2");
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
        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                if (value != null)
                    country = value;
                NotifyPropertyChanged("Country");
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
        private string mainPhone;
        public string MainPhone
        {
            get { return mainPhone; }
            set
            {
                if (value != null)
                    mainPhone = value;
                NotifyPropertyChanged("MainPhone");
            }
        }
        private string faxNumber;
        public string FaxNumber
        {
            get { return faxNumber; }
            set
            {
                if (value != null)
                    faxNumber = value;
                NotifyPropertyChanged("FaxNumber");
            }
        }
        private string primaryContactFirstName;
        public string PrimaryContactFirstName
        {
            get { return primaryContactFirstName; }
            set
            {
                if (value != null)
                    primaryContactFirstName = value;
                NotifyPropertyChanged("PrimaryContactFirstName");
            }
        }
        private string primaryContactPhone;
        public string PrimaryContactPhone
        {
            get { return primaryContactPhone; }
            set
            {
                if (value != null)
                    primaryContactPhone = value;
                NotifyPropertyChanged("PrimaryContactPhone");
            }
        }
        private string primaryContactEmail;
        public string PrimaryContactEmail
        {
            get { return primaryContactEmail; }
            set
            {
                if (value != null)
                    primaryContactEmail = value;
                NotifyPropertyChanged("PrimaryContactEmail");
            }
        }

        private string alternateContactFirstName;
        public string AlternateContactFirstName
        {
            get { return alternateContactFirstName; }
            set
            {
                if (value != null)
                    alternateContactFirstName = value;
                NotifyPropertyChanged("AlternateContactFirstName");
            }
        }
        private string alternateContactPhone;
        public string AlternateContactPhone
        {
            get { return alternateContactPhone; }
            set
            {
                if (value != null)
                    alternateContactPhone = value;
                NotifyPropertyChanged("AlternateContactPhone");
            }
        }
        private string alternateContactEmail;
        public string AlternateContactEmail
        {
            get { return alternateContactEmail; }
            set
            {
                if (value != null)
                    alternateContactEmail = value;
                NotifyPropertyChanged("AlternateContactEmail");
            }
        }

        private string otherPhone1Description;
        public string OtherPhone1Description
        {
            get { return otherPhone1Description; }
            set
            {
                if (value != null)
                    otherPhone1Description = value;
                NotifyPropertyChanged("OtherPhone1Description");
            }
        }
        private string otherPhone1;
        public string OtherPhone1
        {
            get { return otherPhone1; }
            set
            {
                if (value != null)
                    otherPhone1 = value;
                NotifyPropertyChanged("OtherPhone1");
            }
        }
        private string otherPhone2Description;
        public string OtherPhone2Description
        {
            get { return otherPhone2Description; }
            set
            {
                if (value != null)
                    otherPhone2Description = value;
                NotifyPropertyChanged("OtherPhone2Description");
            }
        }
        private string otherPhone2;
        public string OtherPhone2
        {
            get { return otherPhone2; }
            set
            {
                if (value != null)
                    otherPhone2 = value;
                NotifyPropertyChanged("OtherPhone2");
            }
        }
        #endregion

        #region "StreetAddress Properties"

        private List<LocationList> listStreetAddress;

        public List<LocationList> ListStreetAddress
        {
            get
            {
                return listStreetAddress;
            }
            private set
            {
                this.listStreetAddress = value;
                NotifyPropertyChanged("ListStreetAddress");
            }
        }

        private int? locationIdStreet;
        public int? LocationIdStreet
        {
            get { return locationIdStreet; }
            set
            {
                if (value != null)
                    locationIdStreet = value;
                NotifyPropertyChanged("LocationIdStreet");
            }
        }
        private string locationNameStreet;
        public string LocationNameStreet
        {
            get { return locationNameStreet; }
            set
            {
                if (value != null)
                    locationNameStreet = value;
                NotifyPropertyChanged("LocationNameStreet");
            }
        }
        private string addressLine1Street;
        public string AddressLine1Street
        {
            get { return addressLine1Street; }
            set
            {
                if (value != null)
                    addressLine1Street = value;
                NotifyPropertyChanged("AddressLine1Street");
            }
        }
        private string addressLine2Street;
        public string AddressLine2Street
        {
            get { return addressLine2Street; }
            set
            {
                if (value != null)
                    addressLine2Street = value;
                NotifyPropertyChanged("AddressLine2Street");
            }
        }
        private string cityStreet;
        public string CityStreet
        {
            get { return cityStreet; }
            set
            {
                if (value != null)
                    cityStreet = value;
                NotifyPropertyChanged("CityStreet");
            }
        }
        private string stateStreet;
        public string StateStreet
        {
            get { return stateStreet; }
            set
            {
                if (value != null)
                    stateStreet = value;
                NotifyPropertyChanged("StateStreet");
            }
        }
        private string countryStreet;
        public string CountryStreet
        {
            get { return countryStreet; }
            set
            {
                if (value != null)
                    countryStreet = value;
                NotifyPropertyChanged("CountryStreet");
            }
        }
        private string zipStreet;
        public string ZipStreet
        {
            get { return zipStreet; }
            set
            {
                if (value != null)
                    zipStreet = value;
                NotifyPropertyChanged("ZipStreet");
            }
        }
        private string mainPhoneStreet;
        public string MainPhoneStreet
        {
            get { return mainPhoneStreet; }
            set
            {
                if (value != null)
                    mainPhoneStreet = value;
                NotifyPropertyChanged("MainPhoneStreet");
            }
        }
        private string faxNumberStreet;
        public string FaxNumberStreet
        {
            get { return faxNumberStreet; }
            set
            {
                if (value != null)
                    faxNumberStreet = value;
                NotifyPropertyChanged("FaxNumberStreet");
            }
        }
        private string primaryContactFirstNameStreet;
        public string PrimaryContactFirstNameStreet
        {
            get { return primaryContactFirstNameStreet; }
            set
            {
                if (value != null)
                    primaryContactFirstNameStreet = value;
                NotifyPropertyChanged("PrimaryContactFirstNameStreet");
            }
        }
        private string primaryContactPhoneStreet;
        public string PrimaryContactPhoneStreet
        {
            get { return primaryContactPhoneStreet; }
            set
            {
                if (value != null)
                    primaryContactPhoneStreet = value;
                NotifyPropertyChanged("PrimaryContactPhoneStreet");
            }
        }
        private string primaryContactEmailStreet;
        public string PrimaryContactEmailStreet
        {
            get { return primaryContactEmailStreet; }
            set
            {
                if (value != null)
                    primaryContactEmailStreet = value;
                NotifyPropertyChanged("PrimaryContactEmailStreet");
            }
        }

        private string alternateContactFirstNameStreet;
        public string AlternateContactFirstNameStreet
        {
            get { return alternateContactFirstNameStreet; }
            set
            {
                if (value != null)
                    alternateContactFirstNameStreet = value;
                NotifyPropertyChanged("AlternateContactFirstNameStreet");
            }
        }
        private string alternateContactPhoneStreet;
        public string AlternateContactPhoneStreet
        {
            get { return alternateContactPhoneStreet; }
            set
            {
                if (value != null)
                    alternateContactPhoneStreet = value;
                NotifyPropertyChanged("AlternateContactPhoneStreet");
            }
        }
        private string alternateContactEmailStreet;
        public string AlternateContactEmailStreet
        {
            get { return alternateContactEmailStreet; }
            set
            {
                if (value != null)
                    alternateContactEmailStreet = value;
                NotifyPropertyChanged("AlternateContactEmailStreet");
            }
        }

        private string otherPhone1DescriptionStreet;
        public string OtherPhone1DescriptionStreet
        {
            get { return otherPhone1DescriptionStreet; }
            set
            {
                if (value != null)
                    otherPhone1DescriptionStreet = value;
                NotifyPropertyChanged("OtherPhone1DescriptionStreet");
            }
        }
        private string otherPhone1Street;
        public string OtherPhone1Street
        {
            get { return otherPhone1Street; }
            set
            {
                if (value != null)
                    otherPhone1Street = value;
                NotifyPropertyChanged("OtherPhone1Street");
            }
        }
        private string otherPhone2DescriptionStreet;
        public string OtherPhone2DescriptionStreet
        {
            get { return otherPhone2DescriptionStreet; }
            set
            {
                if (value != null)
                    otherPhone2DescriptionStreet = value;
                NotifyPropertyChanged("OtherPhone2DescriptionStreet");
            }
        }
        private string otherPhone2Street;
        public string OtherPhone2Street
        {
            get { return otherPhone2Street; }
            set
            {
                if (value != null)
                    otherPhone2Street = value;
                NotifyPropertyChanged("OtherPhone2Street");
            }
        }
        #endregion

        #region PageProperties
        private int currentPageIndex;
        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                this.currentPageIndex = value;
            }
        }
        #endregion

        #region "Port Storage Rates"
        private AppWorxCommand btnAddRate_Click;
        /// <summary>
        /// Add Port Storage Rate button event
        /// </summary>
        public AppWorxCommand BtnAddRate_Click
        {
            get
            {
                if (btnAddRate_Click == null)
                {
                    btnAddRate_Click = new AppWorxCommand(this.AddStorageRate);
                }
                return btnAddRate_Click;
            }
        }

        private AppWorxCommand btnModifyRate_Click;
        /// <summary>
        /// Modify Port Storage Rate button event
        /// </summary>
        public AppWorxCommand BtnModifyRate_Click
        {
            get
            {
                if (btnModifyRate_Click == null)
                {
                    btnModifyRate_Click = new AppWorxCommand(this.ModifyStorageRate);
                }
                return btnModifyRate_Click;
            }
        }

        private AppWorxCommand btnDeleteRate_Click;
        /// <summary>
        /// Find button event
        /// </summary>
        public AppWorxCommand BtnDeleteRate_Click
        {
            get
            {
                if (btnDeleteRate_Click == null)
                {
                    btnDeleteRate_Click = new AppWorxCommand(this.DeleteStorageRate);
                }
                return btnDeleteRate_Click;
            }
        }

        private AppWorxCommand btnReloadRate_Click;
        /// <summary>
        /// Find button event
        /// </summary>
        public AppWorxCommand BtnReloadRate_Click
        {
            get
            {
                if (btnReloadRate_Click == null)
                {
                    btnReloadRate_Click = new AppWorxCommand(this.ReloadPortStorageRateList);
                }
                return btnReloadRate_Click;
            }
        }


        private AppWorxCommand fillRateAdmin;
        /// <summary>
        /// Find button event
        /// </summary>
        public AppWorxCommand FillRateAdmin
        {
            get
            {
                if (fillRateAdmin == null)
                {
                    fillRateAdmin = new AppWorxCommand(this.FillPortStorageRateAdmin);
                }
                return fillRateAdmin;
            }
        }

        private PortStorageRateList selectedRateItem;
        public PortStorageRateList SelectedRateItem
        {
            get { return selectedRateItem; }
            set
            {
                if (value != null)
                    selectedRateItem = value;
                NotifyPropertyChanged("SelectedRateItem");
            }
        }


        #endregion

        private DateTime? rateStartDate;
        [ChangeTracking]
        public DateTime? RateStartDate
        {
            get { return rateStartDate; }
            set
            {
                this.rateStartDate = value;
                NotifyPropertyChanged("RateStartDate");
                if (value != null)
                {
                }
            }
        }

        private DateTime? rateEndDate;
        [ChangeTracking]
        public DateTime? RateEndDate
        {
            get { return rateEndDate; }
            set
            {
                this.rateEndDate = value;
                NotifyPropertyChanged("RateEndDate");
                if (value != null)
                {
                }
            }
        }


        private bool enabledNew;
        public bool EnabledNew
        {
            get { return enabledNew; }
            set
            {
                this.enabledNew = value;
                NotifyPropertyChanged("EnabledNew");
            }
        }

        private bool enabledModify;
        public bool EnabledModify
        {
            get { return enabledModify; }
            set
            {
                this.enabledModify = value;
                NotifyPropertyChanged("EnabledModify");
            }
        }

        private bool enabledDelete;
        public bool EnabledDelete
        {
            get { return enabledDelete; }
            set
            {
                this.enabledDelete = value;
                NotifyPropertyChanged("EnabledDelete");
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

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                this.text = value;
                NotifyPropertyChanged("Text");
            }
        }
        public static bool CanCheck
        {
            get
            {
                return true;
            }
        }
        private string visibility;
        public string Visibility
        {
            get { return visibility; }
            set
            {
                if (value != null)
                    visibility = value;
                NotifyPropertyChanged("Visibility");
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

        private ObservableCollection<PortStorageRateList> portStorageRates;
        public ObservableCollection<PortStorageRateList> PortStorageRates
        {
            get { return portStorageRates; }
            set
            {
                if (value != null)
                { portStorageRates = value; }
                NotifyPropertyChanged("PortStorageRates");
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

        private List<CodeList> lstRecordStatus;
        public List<CodeList> LstRecordStatus
        {
            get
            {
                return lstRecordStatus;
            }
            private set
            {
                this.lstRecordStatus = value;
                NotifyPropertyChanged("LstRecordStatus");
            }
        }

        private List<CodeList> lstParentCompanyType;
        public List<CodeList> LstParentCompanyType
        {
            get
            {
                return lstParentCompanyType;
            }
            private set
            {
                this.lstParentCompanyType = value;
                NotifyPropertyChanged("LstParentCompanyType");
            }
        }

        private AddressViewModel _billingAddressViewModel;
        public AddressViewModel BillingAddressViewModel
        {
            get { return _billingAddressViewModel ?? (_billingAddressViewModel = new AddressViewModel()); }
            private set
            {
                _billingAddressViewModel = value;
                NotifyPropertyChanged("BillingAddressViewModel");
            }
        }

        public bool IsStreetAddressSameAsBilling
        {
            get { return ReferenceEquals(BillingAddressViewModel, StreetAddressViewModel); }
            set
            {
                if (value)
                {
                    StreetAddressViewModel = BillingAddressViewModel;
                }
                else
                {
                    StreetAddressViewModel = new AddressViewModel();
                    StreetAddressViewModel.CopyFrom(BillingAddressViewModel);
                }
                NotifyPropertyChanged("IsStreetAddressSameAsBilling");
            }
        }

        private AddressViewModel _streetAddressViewModel;
        public AddressViewModel StreetAddressViewModel
        {
            get
            {
                return _streetAddressViewModel ?? (_streetAddressViewModel = new AddressViewModel());
            }
            private set
            {
                _streetAddressViewModel = value;
                NotifyPropertyChanged("StreetAddressViewModel");
            }
        }

        /// <summary>
        /// Function Load customer type
        /// </summary>
        void LoadCutomerType()
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
                foreach (var item in qry)
                {
                    Code = item.Code1;
                    Description = item.Description;
                    break;
                }
                if (qry != null)
                {
                    ListCustomerType = null;
                    ListCustomerType = new List<CodeList>(qry);
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
        /// Function to Get Payment method
        /// </summary>
        void LoadPaymentMethod()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                var qry = _serviceInstance.LoadCodeList(Enums.CodeType.PaymentMethod.ToString()).Select(d => new CodeList
                {
                    Code1 = d.Code1,
                    Description = d.Description,
                    CodeType = d.CodeType
                });
                foreach (var item in qry)
                {
                    Code = item.Code1;
                    Description = item.Description;
                    break;
                }
                if (qry != null)
                {
                    LstPaymentMethod = null;
                    LstPaymentMethod = new List<CodeList>(qry);
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
        /// Function to Get Record Status
        /// </summary>
        void LoadRecordStatus()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var qry = _serviceInstance.LoadCodeList(Enums.CodeType.RecordStatus.ToString()).Select(d => new CodeList
                {
                    Code1 = d.Code1,
                    Description = d.Description,
                    CodeType = d.CodeType
                });
                foreach (var item in qry)
                {
                    Code = item.Code1;
                    Description = item.Description;
                    break;
                }
                if (qry != null)
                {
                    LstRecordStatus = null;
                    LstRecordStatus = new List<CodeList>(qry);
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
        /// Function to get customer notes.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 25,2016</createdOn>
        void LoadCustomerNotes(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var qry = _serviceInstance.NotesList(CustomerID, StartDate, EndDate).Select(d => new CustomerNoteList
                {
                    CustomerNotesID = d.CustomerNotesID,
                    Note = d.Note,
                    CreationDate = d.CreationDate,
                    CreatedBy = d.CreatedBy
                });

                ListNotesList = new List<CustomerNoteList>(qry);

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
        /// Function to Get Parent Company Type.
        /// </summary>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 25,2016</createdOn>
        void LoadParentCompanyType()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var qry = _serviceInstance.LoadCodeList(Enums.CodeType.TruckingCompany.ToString()).Select(d => new CodeList
                {
                    Code1 = d.Code1,
                    Description = d.Description,
                    CodeType = d.CodeType
                });
                if (qry != null)
                {
                    lstParentCompanyType = null;
                    lstParentCompanyType = new List<CodeList>(qry);
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
        /// Function AddLocationPopup to open the customer locator window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        public void AddCustomerNotePopup(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //display the add customernotes popup;
                CustomerNotes objCustomerNotes = new CustomerNotes();
                objCustomerNotes.ShowDialog();
                //objAddLocation.ShowDialog();
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
        /// for loading comobox values
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        public void LoadNewControls(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                BillingAddressViewModel = null;
                CustomerID = 0;
                CustomerNumber = string.Empty;
                CustomerName = string.Empty;
                ShortName = string.Empty;
                DBAName = string.Empty;
                CustomerType = string.Empty;
                //Billing Address
                AddressLine1 = string.Empty;
                AddressLine2 = string.Empty;
                City = string.Empty;
                State = string.Empty;
                Zip = string.Empty;
                Country = string.Empty;
                MainPhone = string.Empty;
                FaxNumber = string.Empty;
                PrimaryContactFirstName = string.Empty;
                PrimaryContactPhone = string.Empty;
                PrimaryContactEmail = string.Empty;
                AlternateContactFirstName = string.Empty;
                AlternateContactEmail = string.Empty;
                AlternateContactPhone = string.Empty;
                otherPhone1Description = string.Empty;
                OtherPhone1 = string.Empty;
                OtherPhone2Description = string.Empty;
                OtherPhone2 = string.Empty;
                //Street Address
                AddressLine1Street = string.Empty;
                AddressLine2Street = string.Empty;
                CityStreet = string.Empty;
                StateStreet = string.Empty;
                ZipStreet = string.Empty;
                CountryStreet = string.Empty;
                MainPhoneStreet = string.Empty;
                FaxNumberStreet = string.Empty;
                PrimaryContactFirstNameStreet = string.Empty;
                PrimaryContactPhoneStreet = string.Empty;
                PrimaryContactEmailStreet = string.Empty;
                AlternateContactFirstNameStreet = string.Empty;
                AlternateContactEmailStreet = string.Empty;
                AlternateContactPhoneStreet = string.Empty;
                otherPhone1DescriptionStreet = string.Empty;
                OtherPhone1Street = string.Empty;
                OtherPhone2DescriptionStreet = string.Empty;
                OtherPhone2Street = string.Empty;
                //Other Information
                VendorNumber = string.Empty;
                DefaultBillingMethod = string.Empty;
                RecordStatus = string.Empty;
                SortOrder = 0;
                EligibleForAutoLoad = 0;
                ALwaysVVIPInd = 0;
                CollectionIssueInd = 0;
                AssignedToExternalCollectionsInd = 0;
                BulkBillingInd = 0;
                DoNotExportInvoiceInd = 0;
                DoNotPrintInvoiceInd = 0;
                HideNewFreightFromBrokerInd = 0;
                PortStorageCustomerInd = 1;
                AutoPortExportCustomerInd = 0;
                RequiresPONumberInd = 0;
                SendEmailConfirmationsInd = 0;
                sTIFollowupRequiredInd = 0;
                DamagePhotoRequiredInd = 0;
                ApplyFuelSurchargeInd = 0;
                FuelSurchargePercent = 0;
                LoadNumberPrefix = string.Empty;
                LoadNumberLength = 0;
                NextLoadNUmber = 0;
                BookingNumberPrefix = string.Empty;
                HandHeldScannerCustomerCode = string.Empty;
                InternalComment = string.Empty;
                driverComment = string.Empty;
                CreatedDate = DateTime.Now;
                CreatedBy = string.Empty;
                UpdatedDate = null;
                UpdatedBy = string.Empty;

                Visibility = Resources.MsgHidden;

                EnabledCancel = true;
                EnabledDelete = false;
                EnabledFind = false;
                EnabledModify = false;
                EnabledNew = false;
                EnabledSaveUpdate = true;

                IsReadOnlyCustomerCode = true;
                IsReadOnlyCustomer = true;
                IsReadOnly = true;
                IsReadOnlyText = false;
                Text = Resources.btnSave;
                CustomerAdminDeligate.SetValueCustomerAdminPropertiesMethod(IsReadOnly); //SetValueMethodCmd("Edit");
                Insert = true;
                Update = false;
                LoadCutomerType();
                LoadParentCompanyType();
                LoadPaymentMethod();
                LoadRecordStatus();
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
        /// Function to Get customer notes.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 25,2016</createdOn>
        void InsertCustomerNotes(CustomerNoteList customerNotes)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                bool result = _serviceInstance.AddCustomerNotes(customerNotes);
                var qry = _serviceInstance.NotesList(CustomerID, StartDate, EndDate).Select(d => new CustomerNoteList
                {
                    CustomerNotesID = d.CustomerNotesID,
                    Note = d.Note,
                    CreationDate = d.CreationDate,
                    CreatedBy = d.CreatedBy
                });

                ListNotesList = new List<CustomerNoteList>(qry);

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
        void NewCustomerLocator(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
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
                    btnFind_Click = new AppWorxCommand(this.NewCustomerLocator);
                }
                return btnFind_Click;
            }
        }

        /// <summary>
        /// Function to Cancle Find Period Popup.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 23,2016</createdOn>
        private void CancelWindow(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //LoadNewControls(null);
                if (CustomerID > 0)
                {

                    GetCustomerValue(custTemp);
                    EnabledModify = true;

                }
                else
                {
                    LoadNewControls(null);
                    EnabledModify = false;
                }
                IsCustomer = false;
                Visibility = Resources.MsgHidden;
                IsReadOnlyCustomerCode = false;
                IsReadOnlyCustomer = false;
                IsReadOnly = false;
                IsReadOnlyText = true;

                EnabledCancel = false;
                EnabledDelete = false;
                EnabledFind = true;

                EnabledNew = true;
                EnabledSaveUpdate = false;
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
        /// Function to get customer by id
        /// </summary>
        /// <param name="customerID"></param>
        void GetCustomerByID(int customerID)
        {
            BillingAddressViewModel = null;
            StreetAddressViewModel = null;
            CustomerID = 0;
            CustomerNumber = string.Empty;
            CustomerName = string.Empty;
            ShortName = string.Empty;
            DBAName = string.Empty;
            CustomerType = string.Empty;
            //Billing Address
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Zip = string.Empty;
            Country = string.Empty;
            MainPhone = string.Empty;
            FaxNumber = string.Empty;
            PrimaryContactFirstName = string.Empty;
            PrimaryContactPhone = string.Empty;
            PrimaryContactEmail = string.Empty;
            AlternateContactFirstName = string.Empty;
            AlternateContactEmail = string.Empty;
            AlternateContactPhone = string.Empty;
            otherPhone1Description = string.Empty;
            OtherPhone1 = string.Empty;
            OtherPhone2Description = string.Empty;
            OtherPhone2 = string.Empty;
            //Street Address
            AddressLine1Street = string.Empty;
            AddressLine2Street = string.Empty;
            CityStreet = string.Empty;
            StateStreet = string.Empty;
            ZipStreet = string.Empty;
            CountryStreet = string.Empty;
            MainPhoneStreet = string.Empty;
            FaxNumberStreet = string.Empty;
            PrimaryContactFirstNameStreet = string.Empty;
            PrimaryContactPhoneStreet = string.Empty;
            PrimaryContactEmailStreet = string.Empty;
            AlternateContactFirstNameStreet = string.Empty;
            AlternateContactEmailStreet = string.Empty;
            AlternateContactPhoneStreet = string.Empty;
            otherPhone1DescriptionStreet = string.Empty;
            OtherPhone1Street = string.Empty;
            OtherPhone2DescriptionStreet = string.Empty;
            OtherPhone2Street = string.Empty;
            //Other Information
            VendorNumber = string.Empty;
            DefaultBillingMethod = string.Empty;
            RecordStatus = string.Empty;
            SortOrder = 0;
            EligibleForAutoLoad = 0;
            ALwaysVVIPInd = 0;
            CollectionIssueInd = 0;
            AssignedToExternalCollectionsInd = 0;
            BulkBillingInd = 0;
            DoNotExportInvoiceInd = 0;
            DoNotPrintInvoiceInd = 0;
            HideNewFreightFromBrokerInd = 0;
            PortStorageCustomerInd = 0;
            AutoPortExportCustomerInd = 0;
            RequiresPONumberInd = 0;
            SendEmailConfirmationsInd = 0;
            sTIFollowupRequiredInd = 0;
            DamagePhotoRequiredInd = 0;
            ApplyFuelSurchargeInd = 0;
            FuelSurchargePercent = 0;
            LoadNumberPrefix = string.Empty;
            LoadNumberLength = 0;
            NextLoadNUmber = 0;
            BookingNumberPrefix = string.Empty;
            HandHeldScannerCustomerCode = string.Empty;
            InternalComment = string.Empty;
            driverComment = string.Empty;
            CreatedDate = DateTime.Now;
            CreatedBy = string.Empty;
            UpdatedDate = null;
            UpdatedBy = string.Empty;

            CustomerSearchProp objCustomerSearchProp = new CustomerSearchProp();
            objCustomerSearchProp.CustomerID = customerID;
            objCustomerSearchProp.CurrentPageIndex = 0;
            objCustomerSearchProp.PageSize = 1;
            objCustomerSearchProp.DefaultPageSize = 1;
            objCustomerSearchProp.PortStorageCustomerInd = 0;

            var list = _serviceInstance.GetCustomerSearchDetails(objCustomerSearchProp);
            List<CustomerSearchProp> cust = new List<CustomerSearchProp>();
            cust = list.ToList();
            if (cust.Count > 0)
            {
                foreach (CustomerSearchProp custInfo in cust)
                {
                    LoadCutomerType();
                    LoadParentCompanyType();
                    LoadPaymentMethod();
                    LoadRecordStatus();

                    CustomerName = custInfo.CustomerName;
                    CustomerNumber = custInfo.CustomerCode;
                    CustomerID = Convert.ToInt32(custInfo.CustomerID);
                    DBAName = custInfo.DBAName;
                    ShortName = custInfo.ShortName;
                    Code1 = custInfo.CustomerType;
                    CustomerOf = custInfo.CustomerOf;
                    BillingAddressID = custInfo.BillingAddressID;
                    MainAddressID = custInfo.MainAddressID;
                    CustomerType = custInfo.CustomerType;
                    CustomerOf = custInfo.CustomerOf;
                    //OtherInformation
                    VendorNumber = custInfo.VendorNumber;
                    DefaultBillingMethod = custInfo.DefaultBillingMethod;
                    RecordStatus = custInfo.RecordStatus;
                    SortOrder = custInfo.SortOrder;
                    EligibleForAutoLoad = custInfo.EligibleForAutoLoad;
                    ALwaysVVIPInd = custInfo.ALwaysVVIPInd;
                    CollectionIssueInd = custInfo.CollectionIssueInd;
                    AssignedToExternalCollectionsInd = custInfo.AssignedToExternalCollectionsInd;
                    BulkBillingInd = custInfo.BulkBillingInd;
                    DoNotPrintInvoiceInd = custInfo.DoNotPrintInvoiceInd;
                    DoNotExportInvoiceInd = custInfo.DoNotExportInvoiceInd;
                    HideNewFreightFromBrokerInd = custInfo.HideNewFreightFromBrokerInd;
                    PortStorageCustomerInd = custInfo.PortStorageCustomerInd;
                    AutoPortExportCustomerInd = custInfo.AutoPortExportCustomerInd;
                    RequiresPONumberInd = custInfo.RequiresPONumberInd;
                    SendEmailConfirmationsInd = custInfo.SendEmailConfirmationsInd;
                    STIFollowupRequiredInd = custInfo.STIFollowupRequiredInd;
                    DamagePhotoRequiredInd = custInfo.DamagePhotoRequiredInd;
                    ApplyFuelSurchargeInd = custInfo.ApplyFuelSurchargeInd;
                    FuelSurchargePercent = custInfo.FuelSurchargePercent;
                    LoadNumberPrefix = custInfo.LoadNumberPrefix;
                    LoadNumberLength = custInfo.LoadNumberLength;
                    NextLoadNUmber = custInfo.NextLoadNUmber;
                    BookingNumberPrefix = custInfo.BookingNumberPrefix;
                    handHeldScannerCustomerCode = custInfo.HandHeldScannerCustomerCode;
                    InternalComment = custInfo.InternalComment;
                    DriverComment = custInfo.DriverComment;
                    CreatedDate = custInfo.CreatedDate;
                    CreatedBy = custInfo.CreatedBy;
                    UpdatedDate = custInfo.UpdatedDate;
                    UpdatedBy = custInfo.UpdatedBy;

                    BillingAddressViewModel.LoadAddress(BillingAddressID);
                    if (BillingAddressID == MainAddressID)
                        StreetAddressViewModel = BillingAddressViewModel;
                    else
                        StreetAddressViewModel.LoadAddress(MainAddressID);
                    break;
                }

            }
        }

        CustomerInfo custTemp;
        public void GetCustomerValue(CustomerInfo custInfo)
        {
            //ResetWindow();
            //display the customer locator popup;
            EnabledCancel = false;
            EnabledDelete = true;
            EnabledFind = true;
            EnabledModify = true;
            EnabledNew = true;
            EnabledSaveUpdate = false;

            IsReadOnlyCustomer = false;
            IsReadOnlyCustomerCode = false;
            IsReadOnly = false;
            IsReadOnlyText = true;
            CustomerAdminDeligate.SetValueCustomerAdminPropertiesMethod(IsReadOnly);

            custTemp = new CustomerInfo();
            custTemp = custInfo;
            LoadCutomerType();
            LoadParentCompanyType();
            LoadPaymentMethod();
            LoadRecordStatus();
            custTemp = custInfo;
            CustomerName = custInfo.CustomerNameInfo;
            CustomerNumber = custInfo.CustomerCode;
            CustomerID = Convert.ToInt32(custInfo.CustomerID);
            DBAName = custInfo.DBAName;
            ShortName = custInfo.ShortName;
            Code1 = custInfo.CustomerType;
            CustomerOf = custInfo.CustomerOf;
            BillingAddressID = custInfo.BillingAddressID;
            MainAddressID = custInfo.MainAddressID;
            CustomerType = custInfo.CustomerType;
            CustomerOf = custInfo.CustomerOf;
            //OtherInformation
            VendorNumber = custInfo.VendorNumber;
            DefaultBillingMethod = custInfo.DefaultBillingMethod;
            RecordStatus = custInfo.RecordStatus;
            SortOrder = custInfo.SortOrder;
            EligibleForAutoLoad = custInfo.EligibleForAutoLoad;
            ALwaysVVIPInd = custInfo.ALwaysVVIPInd;
            CollectionIssueInd = custInfo.CollectionIssueInd;
            AssignedToExternalCollectionsInd = custInfo.AssignedToExternalCollectionsInd;
            BulkBillingInd = custInfo.BulkBillingInd;
            DoNotPrintInvoiceInd = custInfo.DoNotPrintInvoiceInd;
            DoNotExportInvoiceInd = custInfo.DoNotExportInvoiceInd;
            HideNewFreightFromBrokerInd = custInfo.HideNewFreightFromBrokerInd;
            PortStorageCustomerInd = custInfo.PortStorageCustomerInd;
            AutoPortExportCustomerInd = custInfo.AutoPortExportCustomerInd;
            RequiresPONumberInd = custInfo.RequiresPONumberInd;
            SendEmailConfirmationsInd = custInfo.SendEmailConfirmationsInd;
            STIFollowupRequiredInd = custInfo.STIFollowupRequiredInd;
            DamagePhotoRequiredInd = custInfo.DamagePhotoRequiredInd;
            ApplyFuelSurchargeInd = custInfo.ApplyFuelSurchargeInd;
            FuelSurchargePercent = custInfo.FuelSurchargePercent;
            LoadNumberPrefix = custInfo.LoadNumberPrefix;
            LoadNumberLength = custInfo.LoadNumberLength;
            NextLoadNUmber = custInfo.NextLoadNUmber;
            BookingNumberPrefix = custInfo.BookingNumberPrefix;
            handHeldScannerCustomerCode = custInfo.HandHeldScannerCustomerCode;
            InternalComment = custInfo.InternalComment;
            DriverComment = custInfo.DriverComment;
            CreatedDate = custInfo.CreatedDate;
            CreatedBy = custInfo.CreatedBy;
            UpdatedDate = custInfo.UpdatedDate;
            UpdatedBy = custInfo.UpdatedBy;
            RateStartDate = null;
            RateEndDate = null;
            BindPortStorageRatesGrid(null);

            BillingAddressViewModel.LoadAddress(BillingAddressID);
            if (BillingAddressID == MainAddressID)
                StreetAddressViewModel = BillingAddressViewModel;
            else
                StreetAddressViewModel.LoadAddress(MainAddressID);
            //AddLocation objAddLocation = new AddLocation();
            LocationDeligate.SetValueMethodUpdateCmd("UpdateList");
            //LoadCustomerNotes(null);
            if (CustomerID > 0)
            {
                IsCustomer = true;
                Visibility = Resources.MsgVisible;
                Text = Resources.btnUpdate;
                Application.Current.Resources["CustomerAdminID"] = CustomerID;
                LocationDeligate.SetValueMethodUpdateCmd("UpdateList");
            }
        }

        #region "Customer Information Properties"
        private string customerType;
        [ChangeTracking]
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

        private int portStorageRateID;
        public int PortStorageRateID
        {
            get { return portStorageRateID; }
            set
            {
                this.portStorageRateID = value;
                NotifyPropertyChanged("PortStorageRateID");
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
        private string dbaName;
        [ChangeTracking]
        public string DBAName
        {
            get { return dbaName; }
            set
            {
                this.dbaName = value;
                NotifyPropertyChanged("dbaName");
            }
        }
        private string shortName;
        [ChangeTracking]
        public string ShortName
        {
            get { return shortName; }
            set
            {
                this.shortName = value;
                NotifyPropertyChanged("ShortName");
            }
        }
        private string code1;
        public string Code1
        {
            get { return code1; }
            set
            {
                this.code1 = value;
                NotifyPropertyChanged("Code1");
            }
        }
        private string customerOf;
        public string CustomerOf
        {
            get { return customerOf; }
            set
            {
                this.customerOf = value;
                NotifyPropertyChanged("CustomerOf");
            }
        }
        private int? billingAddressID;
        public int? BillingAddressID
        {
            get { return billingAddressID; }
            set
            {
                this.billingAddressID = value;
                NotifyPropertyChanged("BillingAddressID");
            }
        }
        private int? mainAddressID;
        public int? MainAddressID
        {
            get { return mainAddressID; }
            set
            {
                this.mainAddressID = value;
                NotifyPropertyChanged("MainAddressID");
            }
        }
        private int? sortOrder;
        [ChangeTracking]
        public int? SortOrder
        {
            get { return sortOrder; }
            set
            {
                this.sortOrder = value;
                NotifyPropertyChanged("SortOrder");
            }
        }
        private string customerName;
        [ChangeTracking]
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                this.customerName = value;
                NotifyPropertyChanged("CustomerName");
            }
        }
        private string customerNumber;
        [ChangeTracking]
        public string CustomerNumber
        {
            get { return customerNumber; }
            set
            {
                this.customerNumber = value;
                NotifyPropertyChanged("CustomerNumber");
            }
        }
        private string vendorNumber;
        [ChangeTracking]
        public string VendorNumber
        {
            get { return vendorNumber; }
            set
            {
                if (value != null)
                {
                    this.vendorNumber = value;
                    NotifyPropertyChanged("VendorNumber");
                }
            }
        }
        private string defaultBillingMethod;
        [ChangeTracking]
        public string DefaultBillingMethod
        {
            get { return defaultBillingMethod; }
            set
            {
                if (value != null)
                {
                    this.defaultBillingMethod = value;
                    NotifyPropertyChanged("DefaultBillingMethod");
                }
            }
        }
        private int? eligibleForAutoLoad;
        [ChangeTracking]
        public int? EligibleForAutoLoad
        {
            get { return eligibleForAutoLoad; }
            set
            {
                if (value != null)
                {
                    this.eligibleForAutoLoad = value;
                    NotifyPropertyChanged("EligibleForAutoLoad");
                }
            }
        }

        private int? aLwaysVVIPInd;
        [ChangeTracking]
        public int? ALwaysVVIPInd
        {
            get { return aLwaysVVIPInd; }
            set
            {
                if (value != null)
                {
                    this.aLwaysVVIPInd = value;
                    NotifyPropertyChanged("ALwaysVVIPInd");
                }
            }
        }
        private int? collectionIssueInd;
        [ChangeTracking]
        public int? CollectionIssueInd
        {
            get { return collectionIssueInd; }
            set
            {
                if (value != null)
                {
                    this.collectionIssueInd = value;
                    NotifyPropertyChanged("CollectionIssueInd");
                }
            }
        }
        private int? assignedToExternalCollectionsInd;
        [ChangeTracking]
        public int? AssignedToExternalCollectionsInd
        {
            get { return assignedToExternalCollectionsInd; }
            set
            {
                if (value != null)
                {
                    this.assignedToExternalCollectionsInd = value;
                    NotifyPropertyChanged("AssignedToExternalCollectionsInd");
                }
            }
        }
        private int? bulkBillingInd;
        [ChangeTracking]
        public int? BulkBillingInd
        {
            get { return bulkBillingInd; }
            set
            {
                if (value != null)
                {
                    this.bulkBillingInd = value;
                    NotifyPropertyChanged("BulkBillingInd");
                }
            }
        }
        private int? doNotPrintInvoiceInd;
        [ChangeTracking]
        public int? DoNotPrintInvoiceInd
        {
            get { return doNotPrintInvoiceInd; }
            set
            {
                if (value != null)
                {
                    this.doNotPrintInvoiceInd = value;
                    NotifyPropertyChanged("DoNotPrintInvoiceInd");
                }
            }
        }
        private int? doNotExportInvoiceInd;
        [ChangeTracking]
        public int? DoNotExportInvoiceInd
        {
            get { return doNotExportInvoiceInd; }
            set
            {
                if (value != null)
                {
                    this.doNotExportInvoiceInd = value;
                    NotifyPropertyChanged("DoNotExportInvoiceInd");

                }
            }
        }
        private int? hideNewFreightFromBrokerInd;
        [ChangeTracking]
        public int? HideNewFreightFromBrokerInd
        {
            get { return hideNewFreightFromBrokerInd; }
            set
            {
                if (value != null)
                {
                    this.hideNewFreightFromBrokerInd = value;
                    NotifyPropertyChanged("HideNewFreightFromBrokerInd");

                }
            }
        }
        private int? portStorageCustomerInd;
        [ChangeTracking]
        public int? PortStorageCustomerInd
        {
            get { return portStorageCustomerInd; }
            set
            {
                if (value != null)
                {
                    this.portStorageCustomerInd = value;
                    NotifyPropertyChanged("PortStorageCustomerInd");

                }
            }
        }
        private int? autoPortExportCustomerInd;
        [ChangeTracking]
        public int? AutoPortExportCustomerInd
        {
            get { return autoPortExportCustomerInd; }
            set
            {
                if (value != null)
                {
                    this.autoPortExportCustomerInd = value;
                    NotifyPropertyChanged("AutoPortExportCustomerInd");

                }
            }
        }
        private int? requiresPONumberInd;
        [ChangeTracking]
        public int? RequiresPONumberInd
        {
            get { return requiresPONumberInd; }
            set
            {
                if (value != null)
                {
                    this.requiresPONumberInd = value;
                    NotifyPropertyChanged("RequiresPONumberInd");

                }
            }
        }
        private int? sendEmailConfirmationsInd;
        [ChangeTracking]
        public int? SendEmailConfirmationsInd
        {
            get { return sendEmailConfirmationsInd; }
            set
            {
                if (value != null)
                {
                    this.sendEmailConfirmationsInd = value;
                    NotifyPropertyChanged("SendEmailConfirmationsInd");

                }
            }
        }
        private int? sTIFollowupRequiredInd;
        [ChangeTracking]
        public int? STIFollowupRequiredInd
        {
            get { return sTIFollowupRequiredInd; }
            set
            {
                if (value != null)
                {
                    this.sTIFollowupRequiredInd = value;
                    NotifyPropertyChanged("STIFollowupRequiredInd");

                }
            }
        }
        private int? damagePhotoRequiredInd;
        [ChangeTracking]
        public int? DamagePhotoRequiredInd
        {
            get { return damagePhotoRequiredInd; }
            set
            {
                if (value != null)
                {
                    this.damagePhotoRequiredInd = value;
                    NotifyPropertyChanged("DamagePhotoRequiredInd");

                }
            }
        }
        private int? applyFuelSurchargeInd;
        [ChangeTracking]
        public int? ApplyFuelSurchargeInd
        {
            get { return applyFuelSurchargeInd; }
            set
            {
                if (value != null)
                {
                    this.applyFuelSurchargeInd = value;
                    NotifyPropertyChanged("ApplyFuelSurchargeInd");

                }
            }
        }
        private float? fuelSurchargePercent;
        [ChangeTracking]
        public float? FuelSurchargePercent
        {
            get { return fuelSurchargePercent; }
            set
            {
                if (value != null)
                {
                    this.fuelSurchargePercent = value;
                    NotifyPropertyChanged("FuelSurchargePercent");

                }
            }
        }
        private string loadNumberPrefix;
        [ChangeTracking]
        public string LoadNumberPrefix
        {
            get { return loadNumberPrefix; }
            set
            {
                if (value != null)
                {
                    this.loadNumberPrefix = value;
                    NotifyPropertyChanged("LoadNumberPrefix");

                }
            }
        }
        private int? loadNumberLength;
        [ChangeTracking]
        public int? LoadNumberLength
        {
            get { return loadNumberLength; }
            set
            {
                if (value != null)
                {
                    this.loadNumberLength = value;
                    NotifyPropertyChanged("LoadNumberLength");

                }
            }
        }
        private int? nextLoadNUmber;
        [ChangeTracking]
        public int? NextLoadNUmber
        {
            get { return nextLoadNUmber; }
            set
            {
                if (value != null)
                {
                    this.nextLoadNUmber = value;
                    NotifyPropertyChanged("NextLoadNUmber");

                }
            }
        }
        private string bookingNumberPrefix;
        [ChangeTracking]
        public string BookingNumberPrefix
        {
            get { return bookingNumberPrefix; }
            set
            {
                if (value != null)
                {
                    this.bookingNumberPrefix = value;
                    NotifyPropertyChanged("BookingNumberPrefix");

                }
            }
        }

        private string handHeldScannerCustomerCode;
        [ChangeTracking]
        public string HandHeldScannerCustomerCode
        {
            get { return handHeldScannerCustomerCode; }
            set
            {
                if (value != null)
                {
                    this.handHeldScannerCustomerCode = value;
                    NotifyPropertyChanged("HandHeldScannerCustomerCode");

                }
            }
        }
        private string internalComment;
        public string InternalComment
        {
            get { return internalComment; }
            set
            {
                if (value != null)
                {
                    this.internalComment = value;
                    NotifyPropertyChanged("InternalComment");

                }
            }
        }

        private string driverComment;
        public string DriverComment
        {
            get { return driverComment; }
            set
            {
                if (value != null)
                {
                    this.driverComment = value;
                    NotifyPropertyChanged("DriverComment");
                }
            }
        }

        private DateTime? createdDate;
        public DateTime? CreatedDate
        {
            get { return createdDate; }
            set
            {
                this.createdDate = value;
                NotifyPropertyChanged("CreatedDate");
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

        private DateTime? updatedDate;
        public DateTime? UpdatedDate
        {
            get { return updatedDate; }
            set
            {
                this.updatedDate = value;
                NotifyPropertyChanged("UpdatedDate");
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

        #endregion

        #region 'Notes Properties'

        private int customerNotesID;
        public int CustomerNotesID
        {
            get { return customerNotesID; }
            set
            {
                customerNotesID = value;
                NotifyPropertyChanged("CustomerNotesID");
            }
        }

        private string note;
        public string Note
        {
            get { return note; }
            set
            {
                if (value != null)
                    note = value;
                NotifyPropertyChanged("Note");
            }
        }

        private DateTime? creationDateNote;
        public DateTime? CreationDateNote
        {
            get { return creationDateNote; }
            set
            {
                if (value != null)
                    creationDateNote = value;
                NotifyPropertyChanged("CreationDate");
            }
        }

        private DateTime? startDate;
        public DateTime? StartDate
        {
            get { return startDate; }
            set
            {
                if (value != null)
                    startDate = value;
                NotifyPropertyChanged("StartDate");
            }
        }

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                if (value != null)
                    endDate = value;
                NotifyPropertyChanged("EndDate");
            }
        }

        private string createdByNote;
        public string CreatedByNote
        {
            get { return createdByNote; }
            set
            {
                if (value != null)
                    createdByNote = value;
                NotifyPropertyChanged("CreatedByNote");
            }
        }

        private List<CustomerNoteList> listNotesList;

        public List<CustomerNoteList> ListNotesList
        {
            get
            {
                return listNotesList;
            }
            private set
            {
                this.listNotesList = value;
                NotifyPropertyChanged("ListNotesList");
            }
        }



        private AppWorxCommand btnReload_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnReload_Click
        {
            get
            {
                if (btnReload_Click == null)
                {
                    btnReload_Click = new AppWorxCommand(this.LoadCustomerNotes);
                }
                return btnReload_Click;
            }
        }

        private AppWorxCommand btnAddNoteClick;
        public AppWorxCommand BtnAddNoteClick
        {
            get
            {
                if (btnAddNoteClick == null)
                {
                    btnAddNoteClick = new AppWorxCommand(AddCustomerNotePopup);
                }
                return btnAddNoteClick;
            }
        }

        private AppWorxCommand btnInsertNoteClick;
        public AppWorxCommand BtnInsertNoteClick
        {
            get
            {
                if (btnInsertNoteClick == null)
                {
                    btnInsertNoteClick = new AppWorxCommand(AddCustomerNotePopup);
                }
                return btnInsertNoteClick;
            }
        }

        private AppWorxCommand btnNew_Click;
        public AppWorxCommand BtnNew_Click
        {
            get
            {
                if (btnNew_Click == null)
                {
                    btnNew_Click = new AppWorxCommand(LoadNewControls);
                }
                return btnNew_Click;
            }
        }

        private AppWorxCommand btnModify_Click;
        public AppWorxCommand BtnModify_Click
        {
            get
            {
                if (btnModify_Click == null)
                {
                    btnModify_Click = new AppWorxCommand(this.Edit);
                }
                return btnModify_Click;
            }
        }

        private AppWorxCommand btnOK_Click;
        public AppWorxCommand BtnOK_Click
        {
            get
            {
                if (btnOK_Click == null)
                {
                    btnOK_Click = new AppWorxCommand(this.Save);
                }
                return btnOK_Click;
            }
        }

        private AppWorxCommand btnCancel_Click;
        /// <summary>
        /// Cancel button event
        /// </summary>
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

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                this.isReadOnly = value;
                NotifyPropertyChanged("IsReadOnly");
            }
        }

        private bool isReadOnlyText;
        public bool IsReadOnlyText
        {
            get { return isReadOnlyText; }
            set
            {
                this.isReadOnlyText = value;
                NotifyPropertyChanged("IsReadOnlyText");
            }
        }

        private bool isReadOnlyCustomer;
        public bool IsReadOnlyCustomer
        {
            get { return isReadOnlyCustomer; }
            set
            {
                this.isReadOnlyCustomer = value;
                NotifyPropertyChanged("IsReadOnlyCustomer");
            }
        }
        private bool isReadOnlyCustomerCode;
        public bool IsReadOnlyCustomerCode
        {
            get { return isReadOnlyCustomerCode; }
            set
            {
                this.isReadOnlyCustomerCode = value;
                NotifyPropertyChanged("IsReadOnlyCustomerCode");
            }
        }
        private bool insert;
        public bool Insert
        {
            get { return insert; }
            set
            {
                this.insert = value;
                NotifyPropertyChanged("Insert");
            }
        }
        private bool update;
        public bool Update
        {
            get { return update; }
            set
            {
                this.update = value;
                NotifyPropertyChanged("Update");
            }
        }

        private bool isCustomer;
        public bool IsCustomer
        {
            get { return isCustomer; }
            set
            {
                this.isCustomer = value;
                NotifyPropertyChanged("IsCustomer");
            }
        }
        #endregion

        /// <summary>
        /// Function saving the details of customer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public void Save(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                int isSucceed = 0;
                CustomerSearchProp objCustomer = new CustomerSearchProp();
                objCustomer.CustomerID = CustomerID;

                objCustomer.MainAddressID = MainAddressID;
                objCustomer.BillingAddressID = BillingAddressID;

                objCustomer.CustomerName = CustomerName;
                objCustomer.ShortName = ShortName;
                objCustomer.CustomerCode = CustomerNumber;
                objCustomer.CustomerType = CustomerType;
                objCustomer.DBAName = DBAName;
                objCustomer.VendorNumber = VendorNumber;
                objCustomer.DefaultBillingMethod = DefaultBillingMethod;
                objCustomer.RecordStatus = RecordStatus;
                objCustomer.SortOrder = SortOrder;
                objCustomer.EligibleForAutoLoad = EligibleForAutoLoad;
                objCustomer.ALwaysVVIPInd = ALwaysVVIPInd;
                objCustomer.CollectionIssueInd = CollectionIssueInd;
                objCustomer.AssignedToExternalCollectionsInd = AssignedToExternalCollectionsInd;
                objCustomer.BulkBillingInd = BulkBillingInd;
                objCustomer.DoNotPrintInvoiceInd = DoNotPrintInvoiceInd;
                objCustomer.DoNotExportInvoiceInd = DoNotExportInvoiceInd;
                objCustomer.HideNewFreightFromBrokerInd = HideNewFreightFromBrokerInd;
                objCustomer.PortStorageCustomerInd = PortStorageCustomerInd;
                objCustomer.AutoPortExportCustomerInd = AutoPortExportCustomerInd;
                objCustomer.RequiresPONumberInd = RequiresPONumberInd;
                objCustomer.SendEmailConfirmationsInd = SendEmailConfirmationsInd;
                objCustomer.STIFollowupRequiredInd = STIFollowupRequiredInd;
                objCustomer.DamagePhotoRequiredInd = DamagePhotoRequiredInd;
                objCustomer.ApplyFuelSurchargeInd = ApplyFuelSurchargeInd;
                objCustomer.FuelSurchargePercent = FuelSurchargePercent;
                objCustomer.LoadNumberPrefix = LoadNumberPrefix;
                objCustomer.LoadNumberLength = LoadNumberLength;
                objCustomer.NextLoadNUmber = NextLoadNUmber;
                objCustomer.BookingNumberPrefix = BookingNumberPrefix;
                objCustomer.HandHeldScannerCustomerCode = HandHeldScannerCustomerCode;
                objCustomer.DriverComment = DriverComment;
                objCustomer.InternalComment = InternalComment;

                if (Insert)
                {
                    if (!string.IsNullOrEmpty(CustomerNumber))
                    {
                        if (!string.IsNullOrEmpty(customerName))
                        {
                            if (!string.IsNullOrEmpty(CustomerType))
                            {
                                var list = _serviceInstance.GetCustomerSearchDetails(objCustomer).Where(x => x.CustomerCode == CustomerNumber).ToList();
                                if (list.Count == 0)
                                {
                                    objCustomer.CreatedDate = DateTime.Now;
                                    objCustomer.CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                                    isSucceed = _serviceInstance.InsertCustomer(objCustomer);
                                    if (isSucceed > 0)
                                    {
                                        CustomerID = isSucceed;
                                        MessageBox.Show(Resources.msgInsertedCustomer);
                                        Text = Resources.btnSave;
                                        //LoadNewControls(null);
                                        IsCustomer = false;
                                        Visibility = Resources.MsgHidden;
                                        IsReadOnlyCustomerCode = false;
                                        IsReadOnlyCustomer = false;
                                        IsReadOnly = false;
                                        IsReadOnlyText = true;
                                        if (CustomerID > 0)
                                        {
                                            EnabledModify = true;
                                            GetCustomerByID(CustomerID);
                                        }
                                        else
                                        {
                                            EnabledModify = false;
                                            //GetCustomerByID(CustomerID);
                                        }

                                        EnabledCancel = false;
                                        EnabledDelete = false;
                                        EnabledFind = true;
                                        EnabledNew = true;
                                        EnabledSaveUpdate = false;
                                        CustomerAdminDeligate.SetValueCustomerAdminPropertiesMethod(IsReadOnly);

                                        AcceptChanges();
                                    }

                                }
                                else
                                {
                                    MessageBox.Show(Resources.msgCodeAlready);
                                }
                            }
                            else
                                MessageBox.Show(Resources.msgCustomerTypeReq);
                        }
                        else
                            MessageBox.Show(Resources.msgCustomerNameReq);

                    }
                    else
                        MessageBox.Show(Resources.msgCustomerCodeReq);
                }
                else if (Update)
                {
                    objCustomer.UpdatedDate = DateTime.Now;
                    objCustomer.UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();

                    bool isSucceedup = _serviceInstance.UpdateCustomerSearchDetails(objCustomer);
                    if (isSucceedup)
                    {
                        if (IsChanged)
                            MessageBox.Show(Resources.msgUpdatedSuccessfully);
                        else
                            MessageBox.Show(Resources.msgNothingToUpdate);
                        Text = Resources.btnSave;
                        // LoadNewControls(null);
                        IsCustomer = false;
                        Visibility = Resources.MsgHidden;
                        IsReadOnlyCustomerCode = false;
                        IsReadOnlyCustomer = false;
                        IsReadOnly = false;
                        IsReadOnlyText = true;

                        EnabledCancel = false;
                        EnabledDelete = false;
                        EnabledFind = true;
                        if (CustomerID > 0)
                        {
                            EnabledModify = true;
                            GetCustomerByID(CustomerID);
                        }
                        else
                        {
                            EnabledModify = false;
                            //GetCustomerByID(CustomerID);
                        }
                        EnabledNew = true;
                        EnabledSaveUpdate = false;
                        CustomerAdminDeligate.SetValueCustomerAdminPropertiesMethod(IsReadOnly);
                        AcceptChanges();
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
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

        }

        /// <summary>
        /// Function for enabling controls for editing
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public void Edit(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (CustomerID > 0)
                {
                    Insert = false;
                    Update = true;
                    Text = Resources.btnUpdate;
                    EnabledCancel = true;
                    EnabledDelete = false;
                    EnabledFind = false;
                    EnabledModify = false;
                    EnabledNew = false;
                    EnabledSaveUpdate = true;
                    this.IsReadOnly = true;
                    this.IsReadOnlyText = false;
                    this.IsReadOnlyCustomer = true;
                    this.IsReadOnlyCustomerCode = false;
                    CustomerAdminDeligate.SetValueCustomerAdminPropertiesMethod(IsReadOnly);
                    AcceptChanges();
                }
                else
                {
                    MessageBox.Show(Resources.MsgFindCustomer);
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
        /// 
        /// </summary>
        /// <returns></returns>
        void BindPortStorageRatesGrid(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                PortStorageRateList objPortStorageRateProp = new PortStorageRateList();
                objPortStorageRateProp.CustomerID = CustomerID;
                objPortStorageRateProp.StartDate = RateStartDate;
                objPortStorageRateProp.EndDate = RateEndDate;
                objPortStorageRateProp.CurrentPageIndex = CurrentPageIndex;
                objPortStorageRateProp.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize; ;
                objPortStorageRateProp.DefaultPageSize = _GridPageSize;
                var storageRates = _serviceInstance.GetPortStorageRateList(objPortStorageRateProp);

                if (CurrentPageIndex == 0)
                {
                    PortStorageRates = null;
                    PortStorageRates = new ObservableCollection<PortStorageRateList>(storageRates);
                }
                else
                {
                    if (PortStorageRates != null && PortStorageRates.Count > 0)
                    {
                        foreach (PortStorageRateList ud in new ObservableCollection<PortStorageRateList>(storageRates))
                        {
                            PortStorageRates.Add(ud);
                        }
                    }
                }
               
                if (CustomerID > 0)
                {
                    IsAddRateEnabled = true;
                }
                EnableModifyDeleteRate = false;
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

        #region Storage Rates Methods

        void AddStorageRate(object obj)
        {
            Window window = new Window
            {
                Title = "Port Storage Rate Admin",
                Content = new PortStorageRateAdmin(),
                Width = 350,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,                
                ResizeMode = ResizeMode.NoResize
            };
            if (CustomerID > 0)
            {
                SelectedRateItem = new PortStorageRateList();
                SelectedRateItem.CustomerName = CustomerName;
                SelectedRateItem.CustomerID = CustomerID;
                RateAdminDelegate.SetRateAdminValueMethod(SelectedRateItem);
            }
            window.Show();
        }

        private void DeleteStorageRate(object obj)
        {
            MessageBoxResult result = MessageBox.Show(Resources.MsgDeleteConfirm);
            if (result == MessageBoxResult.OK && SelectedRateItem != null)
            {
                var deleted = _serviceInstance.DeletePortStorageRate(SelectedRateItem.PortStorageRatesID);
                if (deleted) 
                    MessageBox.Show(Resources.msgDeleteSuccessfully);
                BindPortStorageRatesGrid(null);
            }
        }

        private void ModifyStorageRate(object obj)
        {
            Window window = new Window
            {
                Title = "Port Storage Rate Admin",
                Content = new PortStorageRateAdmin(),
                Width = 350,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,  
                ResizeMode = ResizeMode.NoResize
            };
            if (SelectedRateItem != null && !string.IsNullOrEmpty(CustomerName))
            {
                SelectedRateItem.CustomerName = CustomerName;
                RateAdminDelegate.SetRateAdminValueMethod(SelectedRateItem);
            }
            window.Show();

        }

        private void ReloadPortStorageRateList(object obj)
        {
            if (RateStartDate == null || RateEndDate == null)
            {
                MessageBox.Show(Resources.msgDateReq);
            }
            else
                BindPortStorageRatesGrid(obj);
        }

        private void FillPortStorageRateAdmin(object obj)
        {
            if (SelectedRateItem != null)
                EnableModifyDeleteRate = true;
            RateAdminDelegate.SetRateAdminValueMethod(SelectedRateItem);

        }

        void CustomerAdminDeligate_OnSetValuePageNumberCmd(int pageNumber)
        {
            CurrentPageIndex = pageNumber;
            BindPortStorageRatesGrid(null);
        }

        void RateAdminDelegate_OnNotifyBindEvt(bool value)
        {
            if (value)
            {
                BindPortStorageRatesGrid(null);
            }
        }
        #endregion
    }
}
