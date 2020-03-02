using AppWorks.UI.Common;
using AppWorks.UI.Controls.Extensions;
using AppWorks.UI.Model;
using AppWorks.UI.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace AppWorks.UI.ViewModel.Location
{
    public class AddLocationVM : ViewModelBase
    {
        public AddLocationVM()
        {
            if (!DesignTimeExtensions.IsDesignTime)
            {
                //Implements Default Initialization Here.
                CustomerID = Convert.ToInt32(Application.Current.Resources["CustomerAdminID"]);
                IsAddButton = true;
                IsUpdateButton = true;
                UserStatusItem = 0;
                BindLocationType(Convert.ToString(Enums.CodeType.LocationType));
                BindLocationType(Convert.ToString(Enums.CodeType.LocationSubType));
                CreationDate = DateTime.Now;
                CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                // gettting the event for modify location
                LocationDeligate.OnSetLocationValueEvt += new LocationDeligate.SetLocationValue(NotificationLocationReceived);
                LocationDeligate.OnSetValueEvtCmd += new LocationDeligate.SetValueCmd(NotificationCmdReceived);
            }
        }


        private void NotificationLocationReceived(LocationModel objLoc)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                LocationId = objLoc.LocationID;
                LocationName = objLoc.LocationName;
                LocationShortName = objLoc.LocationShortName;
                DescriptionLocType = objLoc.LocationType;
                DescriptionLocSubType = objLoc.LocationSubType;
                AddressType = DescriptionLocType;
                AddressLine1 = objLoc.AddressLine1;
                AddressLine2 = objLoc.AddressLine2;
                City = objLoc.City;
                State = objLoc.State;
                Zip = objLoc.Zip;
                Country = objLoc.Country;
                MainPhone = objLoc.MainPhone;
                FirstName = objLoc.PrimaryContactFirstName;
                LastName = objLoc.PrimaryContactLastName;
                Phone = objLoc.MainPhone;
                Extension = objLoc.PrimaryContactExtension;
                ContactCellPhone = objLoc.PrimaryContactCellPhone;
                Email = objLoc.PrimaryContactEmail;

                AlternateContactFirstName = objLoc.AlternateContactFirstName;
                AlternateContactLastName = objLoc.AlternateContactLastName;
                AlternateContactPhone = objLoc.AlternateContactPhone;
                OtherContactExtension = objLoc.AlternateContactExtension;
                AlternateContactCellPhone = objLoc.AlternateContactCellPhone;
                AlternateContactEmail = objLoc.AlternateContactEmail;
                RecordStatus = objLoc.RecordStatus;
                CreatedBy = objLoc.CreatedBy;
                CreationDate = objLoc.CreationDate;
                UpdatedBy = objLoc.UpdatedBy;
                UpdatedDate = objLoc.UpdatedDate;
                OtherPhone1Description = objLoc.OtherPhone1Description;
                OtherPhone1 = objLoc.OtherPhone1;
                otherPhone2Description = objLoc.OtherPhone2Description;
                OtherPhone2 = objLoc.OtherPhone2;
                AddressType = _serviceInstance.GetCustomerAddredssType((int)LocationId, CustomerID);
                //InternalId=objLoc.i
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

        private void NotificationCmdReceived(string cmdName)
        {
            if (cmdName.Equals("Add"))
            {
                IsAddButton = true;
                IsUpdateButton = false;
                //LocationDeligate.SetValueMethodCmd(null);
            }
            else if (cmdName.Equals("Edit"))
            {
                IsAddButton = false;
                IsUpdateButton = true;
                //LocationDeligate.SetValueMethodCmd(null);
            }
        }


        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set
            {
                if (value != null)
                    customerID = value;
                NotifyPropertyChanged("CustomerID");
            }
        }
        private string addressType;
        public string AddressType
        {
            get { return addressType; }
            set
            {
                if (value != null)
                    addressType = value;
                NotifyPropertyChanged("AddressType");
            }
        }

        private bool isAddButton;
        public bool IsAddButton
        {
            get { return isAddButton; }
            set
            {
                this.isAddButton = value;
                NotifyPropertyChanged("IsAddButton");
            }
        }
        private bool isUpdateButton;
        public bool IsUpdateButton
        {
            get { return isUpdateButton; }
            set
            {
                this.isUpdateButton = value;
                NotifyPropertyChanged("IsUpdateButton");
            }
        }
        private AppWorxCommand eBtnAddLocationClick;
        public AppWorxCommand BtnAddLocationClick
        {
            get
            {
                if (eBtnAddLocationClick == null)
                {
                    eBtnAddLocationClick = new AppWorxCommand(AddLocation);
                }
                return eBtnAddLocationClick;
            }
        }


        private AppWorxCommand eBtnUpdateLocationClick;
        public AppWorxCommand BtnUpdateLocationClick
        {
            get
            {
                if (eBtnUpdateLocationClick == null)
                {
                    eBtnUpdateLocationClick = new AppWorxCommand(UpdateLocation);
                }
                return eBtnUpdateLocationClick;
            }
        }

        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        private AppWorxCommand eBtnCancelClick;
        public AppWorxCommand BtnCancelClick
        {
            get
            {
                if (eBtnCancelClick == null)
                {
                    eBtnAddLocationClick = new AppWorxCommand(CancelWindow);
                }
                return eBtnCancelClick;
            }
        }

        #region "BillingAddress Properties"

        private string description;
        public string DescriptionLocType
        {
            get { return description; }
            set
            {
                if (value != null)
                    description = value;
                NotifyPropertyChanged("DescriptionLocType");
            }
        }
        private string descriptionSub;
        public string DescriptionLocSubType
        {
            get { return descriptionSub; }
            set
            {
                if (value != null)
                    descriptionSub = value;
                NotifyPropertyChanged("DescriptionLocSubType");
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
        private ObservableCollection<string> lstLocationType;
        public ObservableCollection<string> ListLocationType
        {
            get { return this.lstLocationType; }

            private set
            {
                this.lstLocationType = value;
                NotifyPropertyChanged("ListLocationType");
            }
        }
        private ObservableCollection<string> lstLocationSubType;
        public ObservableCollection<string> LstLocationSubType
        {
            get { return this.lstLocationSubType; }

            private set
            {
                this.lstLocationSubType = value;
                NotifyPropertyChanged("LstLocationSubType");
            }
        }
        //private List<string> lstRecordStatus;
        //public List<string> LstRecordStatus
        //{
        //    get
        //    {
        //        return lstRecordStatus;
        //    }
        //   private set
        //    {
        //        this.lstRecordStatus = value;
        //        NotifyPropertyChanged("LstRecordStatus");
        //    }
        //}
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
        private string locationType;
        public string LocationType
        {
            get { return locationType; }
            set
            {
                if (value != null)
                    locationType = value;
                NotifyPropertyChanged("LocationType");
            }
        }
        private string locationSubType;
        public string LocationSubType
        {
            get { return locationSubType; }
            set
            {
                if (value != null)
                    locationSubType = value;
                NotifyPropertyChanged("LocationSubType");
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
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != null)
                    firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                if (value != null)
                    phone = value;
                NotifyPropertyChanged("Phone");
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (value != null)
                    email = value;
                NotifyPropertyChanged("Email");
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
        private string locationShortName;
        public string LocationShortName
        {
            get { return locationShortName; }
            set
            {
                if (value != null)
                    locationShortName = value;
                NotifyPropertyChanged("LocationShortName");
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != null)
                    lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        private string extension;
        public string Extension
        {
            get { return extension; }
            set
            {
                if (value != null)
                    extension = value;
                NotifyPropertyChanged("Extension");
            }
        }
        private string contactCellPhone;
        public string ContactCellPhone
        {
            get { return contactCellPhone; }
            set
            {
                if (value != null)
                    contactCellPhone = value;
                NotifyPropertyChanged("ContactCellPhone");
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
        private string alternateContactLastName;
        public string AlternateContactLastName
        {
            get { return alternateContactLastName; }
            set
            {
                if (value != null)
                    alternateContactLastName = value;
                NotifyPropertyChanged("AlternateContactLastName");
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
        private string otherExtension;
        public string OtherContactExtension
        {
            get { return otherExtension; }
            set
            {
                if (value != null)
                    otherExtension = value;
                NotifyPropertyChanged("OtherContactExtension");
            }
        }
        private string alternateContactCellPhone;
        public string AlternateContactCellPhone
        {
            get { return alternateContactCellPhone; }
            set
            {
                if (value != null)
                    alternateContactCellPhone = value;
                NotifyPropertyChanged("AlternateContactCellPhone");
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
        private string recordStatus;
        public string RecordStatus
        {
            get { return recordStatus; }
            set
            {
                if (value != null)
                    recordStatus = value;
                NotifyPropertyChanged("RecordStatus");
            }
        }
        /// <summary>
        /// To Keep Information For Selected Item From ListBox(s)
        /// </summary>
        private int userStatusItem;
        public int UserStatusItem
        {
            get { return userStatusItem; }
            set
            {
                if (value != null)
                { this.userStatusItem = value; }
                NotifyPropertyChanged("UserStatusItem");
            }
        }
        private Nullable<DateTime> creationDate;
        public Nullable<DateTime> CreationDate
        {
            get { return creationDate; }
            set
            {
                if (value != null)
                    creationDate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }
        private string createdBy;
        public string CreatedBy
        {
            get { return createdBy; }
            set
            {
                if (value != null)
                    createdBy = value;
                NotifyPropertyChanged("CreatedBy");
            }
        }
        private Nullable<DateTime> updatedDate;
        public Nullable<DateTime> UpdatedDate
        {
            get { return updatedDate; }
            set
            {
                if (value != null)
                    updatedDate = value;
                NotifyPropertyChanged("UpdatedDate");
            }
        }
        private string updatedBy;
        public string UpdatedBy
        {
            get { return updatedBy; }
            set
            {
                if (value != null)
                    updatedBy = value;
                NotifyPropertyChanged("UpdatedBy");
            }
        }

        private string internalId;
        public string InternalId
        {
            get { return internalId; }
            set
            {
                if (value != null)
                    internalId = value;
                NotifyPropertyChanged("InternalId");
            }
        }
        #endregion


        /// <summary>
        /// Function AddLocation to create the new location
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        private void AddLocation(object obj)
        {
            try
            {
                string errMsg = string.Empty;
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(LocationName) && (!string.IsNullOrEmpty(LocationName)))
                {
                    if (!string.IsNullOrEmpty(DescriptionLocType) && (!string.IsNullOrEmpty(DescriptionLocType)))
                    {
                        if (!string.IsNullOrEmpty(DescriptionLocSubType) && (!string.IsNullOrEmpty(DescriptionLocSubType)))
                        {
                            if (!string.IsNullOrEmpty(Zip) && (!string.IsNullOrEmpty(Zip)))
                            {
                                ValidateEmail emailValidation = new ValidateEmail();
                                bool isValid = emailValidation.IsValidEmail(Email);
                                if (isValid || string.IsNullOrEmpty(Email))
                                {
                                    // creating the object of service property and setting the values
                                    AppWorksService.Properties.LocationList objAddLocationProp = new AppWorksService.Properties.LocationList();

                                    objAddLocationProp.ParentRecordID = Convert.ToInt32(Application.Current.Resources["CustomerAdminID"]);
                                    objAddLocationProp.ParentRecordTable = "Customer";
                                    objAddLocationProp.LocationName = LocationName;
                                    objAddLocationProp.LocationShortName = LocationShortName;
                                    objAddLocationProp.LocationType = DescriptionLocType;
                                    objAddLocationProp.LocationSubType = DescriptionLocSubType;
                                    objAddLocationProp.CustomerLocationCode = string.Empty;
                                    objAddLocationProp.AddressLine1 = AddressLine1;
                                    objAddLocationProp.AddressLine2 = AddressLine2;
                                    objAddLocationProp.City = City;
                                    objAddLocationProp.State = State;
                                    objAddLocationProp.Zip = Zip;
                                    objAddLocationProp.Country = Country;
                                    objAddLocationProp.MainPhone = MainPhone;
                                    objAddLocationProp.FaxNumber = FaxNumber;
                                    objAddLocationProp.PrimaryContactFirstName = FirstName;
                                    objAddLocationProp.PrimaryContactLastName = LastName;
                                    objAddLocationProp.PrimaryContactPhone = Phone;
                                    objAddLocationProp.PrimaryContactExtension = Extension;
                                    objAddLocationProp.PrimaryContactCellPhone = ContactCellPhone;
                                    objAddLocationProp.PrimaryContactEmail = Email;

                                    objAddLocationProp.AlternateContactFirstName = AlternateContactFirstName;
                                    objAddLocationProp.AlternateContactLastName = AlternateContactLastName;
                                    objAddLocationProp.AlternateContactPhone = AlternateContactPhone;
                                    objAddLocationProp.AlternateContactExtension = OtherContactExtension;
                                    objAddLocationProp.AlternateContactCellPhone = AlternateContactCellPhone;
                                    objAddLocationProp.AlternateContactEmail = AlternateContactEmail;
                                    objAddLocationProp.OtherPhone1Description = OtherPhone1Description;

                                    objAddLocationProp.OtherPhone1 = OtherPhone1;
                                    objAddLocationProp.OtherPhone2Description = OtherPhone2Description;
                                    objAddLocationProp.OtherPhone2 = OtherPhone2;
                                    objAddLocationProp.RecordStatus = RecordStatus;
                                    objAddLocationProp.CreationDate = CreationDate;
                                    objAddLocationProp.CreatedBy = CreatedBy;
                                    objAddLocationProp.UpdatedDate = UpdatedDate;
                                    objAddLocationProp.UpdatedBy = updatedBy;
                                    objAddLocationProp.UpdatedBy = InternalId;

                                    // Call service function to add a new location
                                    int data = _serviceInstance.AddLocation(objAddLocationProp);

                                    if (data > 0)
                                    {
                                        LocationId = data;
                                        LocationDeligate.SetValueMethodUpdateCmd("UpdateList");
                                        MessageBox.Show(Resources.msgInsertedSuccessfully);
                                        var window = obj as Window;
                                        if (window != null)
                                        {
                                            window.Close();
                                        }
                                    }
                                }
                                else
                                {
                                    //ClsValidationPopUp.ErrMsgUserPin = Resources.ErrorEmptyPin
                                    errMsg = Resources.ErrorEmail;
                                    AutoLogOffPopup objAutoLogOffPopup = new AutoLogOffPopup(errMsg);
                                    objAutoLogOffPopup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                    objAutoLogOffPopup.ShowDialog();
                                }

                            }
                            else
                            {
                                MessageBox.Show(Resources.msgLocationZipReq);
                            }
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgLocationSubTypeReq);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Resources.msgLocationTypeReq);
                    }
                }
                else
                {
                    MessageBox.Show(Resources.msgLocationNameReq);
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
        /// Function AddLocation to create the new location
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        private void UpdateLocation(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                AppWorksService.Properties.LocationList objUpdateLocationProp = new AppWorksService.Properties.LocationList();
                objUpdateLocationProp.LocationID = (int)LocationId;
                objUpdateLocationProp.LocationName = LocationName;
                objUpdateLocationProp.LocationShortName = LocationShortName;
                objUpdateLocationProp.LocationType = DescriptionLocType;
                objUpdateLocationProp.LocationSubType = DescriptionLocSubType;
                objUpdateLocationProp.CustomerLocationCode = string.Empty;
                objUpdateLocationProp.AddressLine1 = AddressLine1;
                objUpdateLocationProp.AddressLine2 = AddressLine2;
                objUpdateLocationProp.City = City;
                objUpdateLocationProp.State = State;
                objUpdateLocationProp.Zip = Zip;
                objUpdateLocationProp.Country = Country;
                objUpdateLocationProp.MainPhone = MainPhone;
                objUpdateLocationProp.FaxNumber = FaxNumber;
                objUpdateLocationProp.PrimaryContactFirstName = FirstName;
                objUpdateLocationProp.PrimaryContactLastName = LastName;
                objUpdateLocationProp.PrimaryContactPhone = Phone;
                objUpdateLocationProp.PrimaryContactExtension = Extension;
                objUpdateLocationProp.PrimaryContactCellPhone = ContactCellPhone;
                objUpdateLocationProp.PrimaryContactEmail = Email;

                objUpdateLocationProp.AlternateContactFirstName = AlternateContactFirstName;
                objUpdateLocationProp.AlternateContactLastName = AlternateContactLastName;
                objUpdateLocationProp.AlternateContactPhone = AlternateContactPhone;
                objUpdateLocationProp.AlternateContactExtension = OtherContactExtension;
                objUpdateLocationProp.AlternateContactCellPhone = AlternateContactCellPhone;
                objUpdateLocationProp.AlternateContactEmail = AlternateContactEmail;
                objUpdateLocationProp.OtherPhone1Description = OtherPhone1Description;

                objUpdateLocationProp.OtherPhone1 = OtherPhone1;
                objUpdateLocationProp.OtherPhone2Description = OtherPhone2Description;
                objUpdateLocationProp.OtherPhone2 = OtherPhone2;
                objUpdateLocationProp.RecordStatus = RecordStatus;
                objUpdateLocationProp.CreationDate = CreationDate;
                objUpdateLocationProp.CreatedBy = CreatedBy;
                objUpdateLocationProp.UpdatedDate = DateTime.Now;
                objUpdateLocationProp.UpdatedBy = updatedBy;
                objUpdateLocationProp.UpdatedBy = InternalId;

                // Call service function to add a new location
                bool data = _serviceInstance.UpdateLocation(objUpdateLocationProp);
                if (data)
                {
                    if (!AddressType.Equals(null))
                    {
                        //bool isSuccess = objService.UpdateCustomerAddredss(AddressType, CustomerID, objUpdateLocationProp.LocationID);
                        //if (isSuccess)
                        //{
                        //ObservableCollection<LocationModel> LocationModel = new ObservableCollection<Model.LocationModel>();
                        //LocationModel=ObservableCollection<LocationModel>()
                        //LocationDeligate.SetValueLocationListMethod(GetLocationList);

                        //LocationDeligate.OnSetLocationListValueEvt += new LocationDeligate.SetLocationListValue(ObservableCollection<LocationModel> location);
                        // CustomerAdminDeligate.OnSetCustomerAdminPropertiesValueEvt += new CustomerAdminDeligate.SetCustomerAdminPropertiesValue(NotificationCmdReceived);
                        LocationDeligate.SetValueMethodUpdateCmd("UpdateList");
                        MessageBox.Show(Resources.msgUpdatedSuccessfully);
                        //AddLocation objAddLocation = new AddLocation();


                        //}
                        //else
                        //    MessageBox.Show("Data Updated Successfully but unable to set Address!");
                    }

                    var window = obj as Window;
                    if (window != null)
                    {
                        window.Close();
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
        /// Function to close the add location window.
        /// </summary>
        /// <param name="object"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 27,2016</createdOn>
        private void CancelWindow(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                int countWindow = 0;
                foreach (Window window in Application.Current.Windows)
                {
                    if (countWindow == 1)
                    {
                        window.Close();
                    }
                    countWindow++;
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
        /// Function BindLocationType to bind the location sub type dropdown.
        /// </summary>
        /// <param name="NA"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 27,2016</createdOn>
        public void BindLocationType(string codeType)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                var locationTypeList = _serviceInstance.LoadCodeList(codeType).Select(x => x.Code1).ToList();
                List<string> codeList = new List<string>(locationTypeList);
                if (codeType.Equals(Convert.ToString(Enums.CodeType.LocationType)))
                {
                    ListLocationType = new ObservableCollection<string>(codeList);
                }
                else if (codeType.Equals((Convert.ToString(Enums.CodeType.LocationSubType))))
                {
                    LstLocationSubType = new ObservableCollection<string>(codeList);
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
