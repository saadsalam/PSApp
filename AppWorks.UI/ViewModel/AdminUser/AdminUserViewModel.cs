using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppWorks.UI.Common;
using AppWorks.UI.View.UserControls;
using AppWorks.UI.Properties;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Text.RegularExpressions;
using AppWorks.UI.ViewModel.AdminUser;
using AppWorks.UI.Model;
using AppWorksService.Properties;
using AppWorks.UI.View.UserControls.ManageUser;
using System.ComponentModel;
using AppWorks.UI.Controls.Extensions;
using AppWorks.UI.Controls.Attributes;

namespace AppWorks.UI.ViewModel
{
    public class AdminUserViewModel : ChangeTrackingViewModel, IDisposable
    {
        /// <summary>
        /// Admin User View Model Declartion 
        /// </summary>

        public AdminUserViewModel()
        {
            NewUser();
            IsEnabled = true;
            FindUserDeligate.OnSetValueEvt += new FindUserDeligate.SetValue(NotificationMessageReceived);
            FindUserDeligate.OnSetValueEvtCmd += new FindUserDeligate.SetValueCmd(NotificationCmdReceived);
            AllRoles(0);
            if (AvailableUserRoles.Count > 0)
            {
                foreach (var item in AvailableUserRoles)
                    if (item.RoleName.Equals(Resources.DefaultRole))
                        item.IsSelected = true;
            }

            if (cmdName == Resources.btnEdit)
            { Action = Resources.ActionUpdate; }
            else
            { Action = Resources.ActionSave; }

            AcceptChanges();
        }

        public void NotificationMessageReceived(string getUserCode)
        {
            UserCode = getUserCode;
            tempUserCode = getUserCode;

        }
        public void NotificationCmdReceived(string getUserCommand)
        {
            cmdName = getUserCommand;
            if (getUserCommand.ToLower().Equals(Resources.msgview.ToLower()))
            {
                DisplayUserButton = Resources.MsgHidden;
            }
            else
            {
                DisplayUserButton = Resources.MsgVisible;
                IsEnabled = false;
            }
            FillUserDetails();
            if (cmdName == Resources.btnEdit)
            {
                Action = Resources.ActionUpdate;
                IsEnabled = true;
            }
            else
            {
                Action = Resources.ActionSave;
                IsEnabled = false;
            }
        }
        #region :: All Private variable ::

        private string cmdName;
        private int loggedUserID;
        private string userCode;
        private string firstName;
        private string lastName;
        private string password;
        private string pIN;
        private string phone;
        private string phoneExtension;
        private string cellPhone;
        private string faxNumber;
        private string emailAddress;
        private string labelXOffset;
        private string labelYOffset;
        private string iMEI;
        private Nullable<System.DateTime> lastConnection;
        private string datsVersion;
        private string recordStatus;
        private Nullable<System.DateTime> creationDate;
        private string createdBy;
        private Nullable<System.DateTime> updatedDate;
        private string updatedBy;
        private string employeeNumber;
        private string portPassIDNumber;
        private string department;
        private string straightTimeRate;
        private string pieceRateRate;
        private string pDIRate;
        private string flatBenefitPayRate;
        private string alternateEmailAddress;
        private ObservableCollection<string> userRole;
        private ObservableCollection<string> newUserRole;
        private ObservableCollection<string> allRole;
        private string selectedItem;

        private ICommand _btnSubmit_Click;
        private ICommand _btnAddRole_Click;
        private ICommand _btnRemoveRole_Click;
        private ICommand _btnCancel_Click;
        private ICommand _btnSaveRole_Click;
        private ICommand _btnCancelRole_Click;

        #endregion

        private string _selectedUserRoles;

        [ChangeTracking]
        public string SelectedUserRoles
        {
            get
            {
                _selectedUserRoles = "Selected user roles:";
                int countSelectedUserRoles = 0;

                foreach (RoleList role in AvailableUserRoles)
                {
                    if (role.IsSelected != null && (bool)role.IsSelected)
                    {
                        _selectedUserRoles += "\n" + role.RoleName;
                        countSelectedUserRoles++;
                    }
                }

                CountSelectedUserRoles = countSelectedUserRoles.ToString();
                return _selectedUserRoles;
            }
        }

        private string _countSelectedUserRoles;
        public string CountSelectedUserRoles
        {
            get
            {
                return _countSelectedUserRoles == "0" ? "Select roles" : "Selected " + _countSelectedUserRoles + " role(s)";
            }
            set
            {
                if (!Equals(_countSelectedUserRoles, value))
                {
                    _countSelectedUserRoles = value;
                    NotifyPropertyChanged("CountSelectedUserRoles");
                }
            }
        }

        private List<RoleList> availableUserRoles;
        public List<RoleList> AvailableUserRoles
        {
            get { return this.availableUserRoles; }
            set
            {
                if (this.availableUserRoles == value) return;
                this.availableUserRoles = value;
                NotifyPropertyChanged("AvailableUserRoles");
            }
        }

        private clsValidationPopUp clsValidationPopUp = new clsValidationPopUp();
        public clsValidationPopUp ClsValidationPopUp
        {
            get { return this.clsValidationPopUp; }
            set { this.clsValidationPopUp = value; NotifyPropertyChanged("ClsValidationPopUp"); }
        }

        private string displayUserButton;
        public string DisplayUserButton
        {
            get { return this.displayUserButton; }
            set { this.displayUserButton = value; NotifyPropertyChanged("DisplayUserButton"); }
        }


        public ICommand _btnFindUser_Click;
        public ICommand btnFindUser_Click
        {
            get
            {
                if (_btnFindUser_Click == null)
                {
                    _btnFindUser_Click = new AppWorxCommand(
                        param => this.OnFindUserRaised(),
                        param => CanCheck
                        );
                }
                return _btnFindUser_Click;
            }
        }

        private void OnFindUserRaised()
        {
            FindUser dialog = new FindUser();
            dialog.Owner = Application.Current.MainWindow;
            dialog.ShowDialog();
            AcceptChanges();
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.isEnabled = value; NotifyPropertyChanged("IsEnabled"); }
        }

        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        public ICommand btnSubmit_Click
        {
            get
            {
                if (_btnSubmit_Click == null)
                {
                    _btnSubmit_Click = new AppWorxCommand(
                        param => this.AddNewAdminUser(),
                        param => CanCheck
                        );
                }
                return _btnSubmit_Click;
            }
        }


        private ICommand _btnUpdate_Click;
        public ICommand btnUpdate_Click
        {
            get
            {
                if (_btnUpdate_Click == null)
                {
                    _btnUpdate_Click = new AppWorxCommand(
                        param => this.UpdateRecord(),
                        param => CanCheck
                        );
                }
                return _btnUpdate_Click;
            }
        }
        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        public ICommand btnAddRole_Click
        {
            get
            {
                if (_btnAddRole_Click == null)
                {
                    _btnAddRole_Click = new AppWorxCommand(
                        param => this.LoadNewUseRole(),
                        param => CanCheck
                        );
                }
                return _btnAddRole_Click;
            }
        }

        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        public ICommand btnRemoveRole_Click
        {
            get
            {
                if (_btnRemoveRole_Click == null)
                {
                    _btnRemoveRole_Click = new AppWorxCommand(
                        param => this.RemoveUseRole(),
                        param => CanCheck
                        );
                }
                return _btnRemoveRole_Click;
            }
        }


        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        public ICommand btnSaveRole_Click
        {
            get
            {
                if (_btnSaveRole_Click == null)
                {
                    _btnSaveRole_Click = new AppWorxCommand(
                        param => this.SaveRole(),
                        param => CanCheck
                        );
                }
                return _btnSaveRole_Click;
            }
        }

        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        public ICommand btnCancelRole_Click
        {
            get
            {
                if (_btnCancelRole_Click == null)
                {
                    _btnCancelRole_Click = new AppWorxCommand(
                        param => this.FillUserDetails(),
                        param => CanCheck
                        );
                }
                return _btnCancelRole_Click;
            }
        }


        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        public ICommand btnCancel_Click
        {
            get
            {
                if (_btnCancel_Click == null)
                {
                    _btnCancel_Click = new AppWorxCommand(
                        param => this.CancelClick(),
                        param => CanCheck
                        );
                }
                return _btnCancel_Click;
            }
        }

        /// <summary>
        /// for Checking parameter during command execution
        /// </summary>
        public bool CanCheck
        {
            get
            {
                NotifyPropertyChanged("SelectedUserRoles");
                return true;
            }
        }

        public int LoggedUserId
        {
            get
            {
                return loggedUserID;
            }
            set
            {
                if (value != null)
                { this.loggedUserID = value; }
                NotifyPropertyChanged("LoggedUserId");
            }
        }

        private string tempuserCode;
        /// <summary>
        /// for holding and resturning the value of usercode
        /// </summary>
        public string tempUserCode
        {
            get
            {
                return tempuserCode;
            }
            set
            {
                if (value != null)
                { this.tempuserCode = value; }
            }
        }

        /// <summary>
        /// for holding and resturning the value of usercode
        /// </summary>
        [ChangeTracking]
        public string UserCode
        {
            get
            {
                return userCode;
            }
            set
            {
                if (value != null && !Equals(userCode, value))
                {
                    this.userCode = value;
                    NotifyPropertyChanged("UserCode");
                }
            }
        }

        /// <summary>
        /// To Keep Information For FirstName
        /// </summary>
        [ChangeTracking]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != null)
                { this.firstName = value; }
                NotifyPropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// To Keep Information For LastName
        /// </summary>
        [ChangeTracking]
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != null)
                { this.lastName = value; }
                NotifyPropertyChanged("LastName");
            }
        }

        private int userID;
        public int UserID
        {
            get { return userID; }
            set
            {
                this.userID = value;
                NotifyPropertyChanged("UserID");
            }
        }


        /// <summary>
        /// To Keep Information For Password
        /// </summary>
        [ChangeTracking]
        public string Password
        {
            get { return password; }
            set
            {
                if (value != null)
                { this.password = value; }
                NotifyPropertyChanged("Password");
            }
        }

        /// <summary>
        /// To Keep Information For PIN
        /// </summary>
        [ChangeTracking]
        public string PIN
        {
            get { return pIN; }
            set
            {
                if (value != null)
                { this.pIN = value; }
                NotifyPropertyChanged("PIN");
            }
        }

        /// <summary>
        /// To Keep Information For Action
        /// </summary>
        private string action;
        public string Action
        {
            get { return action; }
            set
            {
                if (value != null)
                    action = value;
                NotifyPropertyChanged("Action");
            }
        }
        /// <summary>
        /// To Keep Information For Phone
        /// </summary>
        [ChangeTracking]
        public string Phone
        {
            get
            {
                if (phone == null)
                    return string.Empty;

                switch (phone.Length)
                {
                    case 6:
                        return Regex.Replace(phone, @"(\d{3})(\d{3})", "$1-$2");
                    case 10:
                        return Regex.Replace(phone, @"(\d{3})(\d{3})(\d{4})", "( $1) $2-$3");
                    case 11:
                        return Regex.Replace(phone.Replace("-", ""), @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");

                    default:
                        return phone;
                }

                //return phone;
            }
            set
            {
                if (value != null)
                { this.phone = value; }
                NotifyPropertyChanged("Phone");
            }
        }

        /// <summary>
        /// To Keep Information For PhoneExtention
        /// </summary>
        [ChangeTracking]
        public string PhoneExtention
        {
            get { return phoneExtension; }
            set
            {
                if (value != null)
                { this.phoneExtension = value; }
                NotifyPropertyChanged("PhoneExtention");
            }
        }

        /// <summary>
        /// To Keep Information For CellPhone
        /// </summary>
        [ChangeTracking]
        public string CellPhone
        {
            get
            {
                if (cellPhone == null)
                    return string.Empty;

                switch (cellPhone.Length)
                {
                    case 6:
                        return Regex.Replace(cellPhone, @"(\d{3})(\d{3})", "$1-$2");
                    case 10:
                        return Regex.Replace(cellPhone, @"(\d{3})(\d{3})(\d{4})", "( $1) $2-$3");
                    case 11:
                        return Regex.Replace(cellPhone.Replace("-", ""), @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");

                    default:
                        return cellPhone;
                }
            }
            set
            {
                if (value != null)
                { this.cellPhone = value; }
                NotifyPropertyChanged("CellPhone");
            }
        }

        /// <summary>
        /// To Keep Information For FaxNumber
        /// </summary>
        [ChangeTracking]
        public string FaxNumber
        {
            get
            {
                if (faxNumber == null)
                    return string.Empty;

                switch (faxNumber.Length)
                {
                    case 6:
                        return Regex.Replace(faxNumber, @"(\d{3})(\d{3})", "$1-$2");
                    case 10:
                        return Regex.Replace(faxNumber, @"(\d{3})(\d{3})(\d{4})", "( $1) $2-$3");
                    case 11:
                        return Regex.Replace(faxNumber.Replace("-", ""), @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                    default:
                        return faxNumber;
                }
            }
            set
            {
                if (value != null)
                { this.faxNumber = value; }
                NotifyPropertyChanged("FaxNumber");
            }
        }

        /// <summary>
        /// To Keep Information For EmailAddress
        /// </summary>
        [ChangeTracking]
        public string EmailAddress
        {
            get
            {
                if (emailAddress == null)
                    return string.Empty;
                if (!Regex.IsMatch(emailAddress, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    ClsValidationPopUp.ErrMsgEmail = Resources.ErrorEmail;
                }
                return emailAddress;
            }
            set
            {
                if (value != null)
                { this.emailAddress = value; }
                NotifyPropertyChanged("EmailAddress");
            }
        }

        /// <summary>
        /// To Keep Information For LabelXOffset
        /// </summary>
        [ChangeTracking]
        public string LabelXOffset
        {
            get { return labelXOffset; }
            set
            {
                if (value != null)
                { this.labelXOffset = value; }
                NotifyPropertyChanged("LabelXOffset");
            }
        }

        /// <summary>
        /// To Keep Information For LabelYOffSet
        /// </summary>
        [ChangeTracking]
        public string LabelYOffset
        {
            get { return labelYOffset; }
            set
            {
                if (value != null)
                { this.labelYOffset = value; }
                NotifyPropertyChanged("LabelYOffset");
            }
        }

        /// <summary>
        /// To Keep Information For IEMI
        /// </summary>
        public string IMEI
        {
            get { return iMEI; }
            set
            {
                if (value != null)
                { this.iMEI = value; }
                NotifyPropertyChanged("IMEI");
            }
        }

        /// <summary>
        /// To Keep Information For LastConnection
        /// </summary>
        public DateTime? LastConnection
        {
            get { return lastConnection; }
            set
            {
                if (value != null)
                { this.lastConnection = value; }
                NotifyPropertyChanged("LastConnection");
            }
        }

        /// <summary>
        /// To Keep Information For DatsVersion
        /// </summary>
        public string DatsVersion
        {
            get { return datsVersion; }
            set
            {
                if (value != null)
                { this.datsVersion = value; }
                NotifyPropertyChanged("DatsVersion");
            }
        }

        /// <summary>
        /// To Keep Information For RecordStatus
        /// </summary>
        [ChangeTracking]
        public string RecordStatus
        {
            get { return recordStatus; }
            set
            {
                if (value != null)
                { this.recordStatus = value; }
                NotifyPropertyChanged("RecordStatus");
            }
        }

        /// <summary>
        /// To Keep Information For CeationDate
        /// </summary>
        [ChangeTracking]
        public DateTime? CreationDate
        {
            get { return creationDate; }
            set
            {
                if (value != null)
                { this.creationDate = value; }
                NotifyPropertyChanged("CreationDate");
            }
        }

        /// <summary>
        /// To Keep Information For CreatedBy
        /// </summary>
        [ChangeTracking]
        public string CreatedBy
        {
            get { return createdBy; }
            set
            {
                if (value != null)
                { this.createdBy = value; }
                NotifyPropertyChanged("CreatedBy");
            }
        }

        /// <summary>
        /// To Keep Information For UpdateDate
        /// </summary>
        [ChangeTracking]
        public DateTime? UpdatedDate
        {
            get { return updatedDate; }
            set
            {
                if (value != null)
                { this.updatedDate = value; }
                NotifyPropertyChanged("UpdatedDate");
            }
        }

        /// <summary>
        /// To Keep Information For UpdateBy
        /// </summary>
        [ChangeTracking]
        public string UpdatedBy
        {
            get { return updatedBy; }
            set
            {
                if (value != null)
                { this.updatedBy = value; }
                NotifyPropertyChanged("UpdatedBy");
            }
        }

        /// <summary>
        /// To Keep Information For EmployeeNumber
        /// </summary>
        [ChangeTracking]
        public string EmployeeNumber
        {
            get { return employeeNumber; }
            set
            {
                if (value != null)
                { this.employeeNumber = value; }
                NotifyPropertyChanged("EmployeeNumber");
            }
        }

        /// <summary>
        /// To Keep Information For PortPassIDNumber
        /// </summary>
        [ChangeTracking]
        public string PortPassIDNumber
        {
            get { return portPassIDNumber; }
            set
            {
                if (value != null)
                { this.portPassIDNumber = value; }
                NotifyPropertyChanged("PortPassIDNumber");
            }
        }

        /// <summary>
        /// To Keep Information For Department
        /// </summary>
        public string Department
        {
            get { return department; }
            set
            {
                if (value != null)
                { this.department = value; }
                NotifyPropertyChanged("Department");
            }
        }

        /// <summary>
        /// To Keep Information For StraightTimeRate
        /// </summary>
        public string StraightTimeRate
        {
            get { return straightTimeRate; }
            set
            {
                if (value != null)
                { this.straightTimeRate = value; }
                NotifyPropertyChanged("StraightTimeRate");
            }
        }

        /// <summary>
        /// To Keep Information For FirstName
        /// </summary>
        public string PieceRateRate
        {
            get { return pieceRateRate; }
            set
            {
                if (value != null)
                { this.pieceRateRate = value; }
                NotifyPropertyChanged("PieceRateRate");
            }
        }

        /// <summary>
        /// To Keep Information For PDIRate
        /// </summary>
        public string PDIRate
        {
            get { return pDIRate; }
            set
            {
                if (value != null)
                { this.pDIRate = value; }
                NotifyPropertyChanged("PDIRate");
            }
        }

        /// <summary>
        /// To Keep Information For FlatBenefitPayRate
        /// </summary>
        public string FlatBenefitPayRate
        {
            get { return flatBenefitPayRate; }
            set
            {
                if (value != null)
                { this.flatBenefitPayRate = value; }
                NotifyPropertyChanged("FlatBenefitPayRate");
            }
        }

        /// <summary>
        /// To Keep Information For AlternateEmailAddress
        /// </summary>
        public string AlternateEmailAddress
        {
            get { return alternateEmailAddress; }
            set
            {
                if (value != null)
                { this.alternateEmailAddress = value; }
                NotifyPropertyChanged("AlternateEmailAddress");
            }
        }

        /// <summary>
        /// To Keep Information For Selected Item From ListBox(s)
        /// </summary>
        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (value != null)
                { this.selectedItem = value; }
                NotifyPropertyChanged("SelectedItem");
            }
        }

        /// <summary>
        /// To Keep Information For Selected Item From ListBox(s)
        /// </summary>
        private int _selectedRecordStatus;
        public int SelectedRecordStatus
        {
            get { return _selectedRecordStatus; }
            set
            {
                this._selectedRecordStatus = value;
                NotifyPropertyChanged("SelectedRecordStatus");
            }
        }

        /// <summary>
        /// To Keep Information For UserRole
        /// </summary>
        public ObservableCollection<string> UserRole
        {
            get { return userRole; }
            private set
            {
                if (value != null)
                { this.userRole = value; }
                NotifyPropertyChanged("UserRole");
            }
        }

        public void NewUser()
        {
            UserID = 0;
            UserCode = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Password = string.Empty;
            PIN = string.Empty;
            Phone = string.Empty;
            PhoneExtention = string.Empty;
            CellPhone = string.Empty;
            FaxNumber = string.Empty;
            EmailAddress = string.Empty;
            IMEI = string.Empty;
            LabelXOffset = "0.00";
            LabelYOffset = "0.00";
            LastConnection = null;
            DatsVersion = string.Empty;
            CreationDate = null;
            UpdatedDate = null;
            UpdatedBy = null;
            EmployeeNumber = string.Empty;
            PortPassIDNumber = DateTime.Now.ToString("MMddyyhhmmss");
            Department = string.Empty;
            StraightTimeRate = string.Empty;
            PieceRateRate = string.Empty;
            PDIRate = string.Empty;
            FlatBenefitPayRate = string.Empty;
            AlternateEmailAddress = string.Empty;
            SelectedRecordStatus = (int)Enums.RecordStatus.Active;
            RecordStatus = Enum.GetName(typeof(Enums.RecordStatus), Enums.RecordStatus.Active);
            CreationDate = DateTime.Now;
            CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
            UpdatedBy = string.Empty;
        }

        /// <summary>
        /// On Cancel button click event
        /// </summary>
        public void CancelClick()
        {
            NewUser();
            Action = Resources.ActionSave;
            cmdName = Resources.btnSave;
            AcceptChanges();
        }

        public void SaveRole()
        {
            string errMsg = string.Empty;
            errMsg = Resources.MsgRecordUpdated;
            AutoLogOffPopup objAutoLogOffPopup = new AutoLogOffPopup(errMsg);
            objAutoLogOffPopup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            objAutoLogOffPopup.ShowDialog();
        }

        /// <summary>
        /// Function to check the Login Credentials.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        private void AddNewAdminUser()
        {
            string errMsg = string.Empty;

            bool result;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                AdminUserProp objAdminUserProp = new AdminUserProp();

                if (cmdName == Resources.btnEdit)
                {
                    UpdateRecord();
                    Action = Resources.ActionSave;
                    cmdName = Resources.btnSave;
                    NewUser();
                    AcceptChanges();
                }
                else
                {
                    if (!string.IsNullOrEmpty(UserCode))
                    {
                        ClsValidationPopUp.ErrMsgUserCode = null;
                        if (!string.IsNullOrEmpty(PIN))
                        {
                            ClsValidationPopUp.ErrMsgUserPin = null;

                            if (!string.IsNullOrEmpty(EmailAddress))
                            {
                                ValidateEmail emailValidation = new ValidateEmail();
                                bool isValid = emailValidation.IsValidEmail(EmailAddress);
                                if (!isValid)
                                {
                                    MessageBox.Show(Resources.ErrorEmail);
                                    return;
                                }

                            }

                            //if (isValid || string.IsNullOrEmpty(EmailAddress))
                            //{
                            objAdminUserProp.UserCode = UserCode;
                            objAdminUserProp.FirstName = FirstName;
                            objAdminUserProp.LastName = LastName;
                            objAdminUserProp.Password = Password;
                            objAdminUserProp.PIN = PIN;
                            objAdminUserProp.Phone = Phone;
                            objAdminUserProp.PhoneExtension = PhoneExtention;
                            objAdminUserProp.CellPhone = CellPhone;
                            objAdminUserProp.EmailAddress = EmailAddress;
                            objAdminUserProp.FaxNumber = FaxNumber;
                            objAdminUserProp.LabelXOffset = LabelXOffset;
                            objAdminUserProp.LabelYOffset = LabelYOffset;
                            objAdminUserProp.IMEI = IMEI;
                            objAdminUserProp.LastConnection = LastConnection;
                            objAdminUserProp.datsVersion = datsVersion;
                            objAdminUserProp.RecordStatus = RecordStatus;
                            objAdminUserProp.CreationDate = CreationDate;
                            objAdminUserProp.CreatedBy = CreatedBy;
                            objAdminUserProp.UpdatedDate = UpdatedDate;
                            objAdminUserProp.UpdatedBy = UpdatedBy;
                            objAdminUserProp.EmployeeNumber = EmployeeNumber;
                            objAdminUserProp.PortPassIDNumber = PortPassIDNumber;
                            objAdminUserProp.Department = Department;
                            objAdminUserProp.StraightTimeRate = StraightTimeRate;
                            objAdminUserProp.PieceRateRate = PieceRateRate;
                            objAdminUserProp.PDIRate = PDIRate;
                            objAdminUserProp.FlatBenefitPayRate = FlatBenefitPayRate;
                            objAdminUserProp.AlternateEmailAddress = AlternateEmailAddress;

                            result = _serviceInstance.IsUserExists(objAdminUserProp, AvailableUserRoles.ToArray());

                            if (result != true)
                            {
                                //ClsValidationPopUp.ErrMsgUserCode = Resources.ErrorUserCode
                                MessageBox.Show(Resources.ErrorUserCode);
                                //AutoLogOffPopup objAutoLogOffPopup = new AutoLogOffPopup(errMsg);
                                //objAutoLogOffPopup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                //objAutoLogOffPopup.ShowDialog();
                            }
                            else
                            {
                                //ClsValidationPopUp.ErrMsgUserPin = Resources.ErrorEmptyPin
                                //NewUser();
                                MessageBox.Show(Resources.msgNewUserAdded);
                                Action = Resources.ActionSave;
                                cmdName = Resources.btnSave;
                                NewUser();
                                //AutoLogOffPopup objAutoLogOffPopup = new AutoLogOffPopup(Resources.msgNewUserAdded);
                                //objAutoLogOffPopup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                //objAutoLogOffPopup.ShowDialog();
                                AcceptChanges();
                            }
                            //}
                            //else
                            //{
                            //    //ClsValidationPopUp.ErrMsgUserPin = Resources.ErrorEmptyPin
                            //    MessageBox.Show(Resources.ErrorEmail);
                            //    //AutoLogOffPopup objAutoLogOffPopup = new AutoLogOffPopup(errMsg);
                            //    //objAutoLogOffPopup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            //    //objAutoLogOffPopup.ShowDialog();
                            //}

                            //}
                            //else
                            //{
                            //    //ClsValidationPopUp.ErrMsgUserPin = Resources.ErrorEmptyPin
                            //    errMsg = Resources.emp;
                            //    AutoLogOffPopup objAutoLogOffPopup = new AutoLogOffPopup(errMsg);
                            //    objAutoLogOffPopup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            //    objAutoLogOffPopup.ShowDialog();
                            //}

                        }
                        else
                        {
                            //ClsValidationPopUp.ErrMsgUserPin = Resources.ErrorEmptyPin
                            MessageBox.Show(Resources.ErrorEmptyPin);
                            //AutoLogOffPopup objAutoLogOffPopup = new AutoLogOffPopup(errMsg);
                            //objAutoLogOffPopup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            //objAutoLogOffPopup.ShowDialog();
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(UserCode))
                        {
                            //ClsValidationPopUp.ErrMsgUserCode = Resources.ErrorEmptyUserCode
                            MessageBox.Show(Resources.ErrorEmptyUserCode);
                            //AutoLogOffPopup objAutoLogOffPopup = new AutoLogOffPopup(errMsg);
                            //objAutoLogOffPopup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            //objAutoLogOffPopup.ShowDialog();
                        }
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
        /// Function to Bind the Exsting User's Role.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-22,2016</createdOn>
        /// <modifiedOn>Apr-23,2016</modifiedOn>
        public ObservableCollection<string> LoadExistingUseRole()
        {
            try
            {
                //AppWorksService.Properties.AdminUserProp objAdminUserProp = new AppWorksService.Properties.AdminUserProp();
                //objAdminUserProp.UserCode = UserCode;
                //objAdminUserProp.Password = Password;
                //objAdminUserProp.PIN = PIN;
                //CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //string[] lst = _serviceInstance.ExistingUserRole(objAdminUserProp);
                ////UserRole = new ObservableCollection<string>(lst);
                //foreach (string item in lst)
                //{
                //    AvailableUserRoles.First(selectableRole => string.Equals(selectableRole.Instance, item)).IsSelected = true;
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
            return userRole;

        }

        /// <summary>
        /// Function to Bind the All User's Role.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-23,2016</createdOn>
        public ObservableCollection<string> AllRoles(int userId)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                AdminUserProp objAdminUserProp = new AdminUserProp();

                var lst = _serviceInstance.GetRolesSelection(userId);
                if (lst.Count() > 0)
                {
                    AvailableUserRoles = null;
                    AvailableUserRoles = lst.ToList();
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
            return userRole;
        }

        /// <summary>
        /// Function to Bind the Updated User's Role.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-23,2016</createdOn>
        public ObservableCollection<string> LoadNewUseRole()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!UserRole.Contains(SelectedItem))
                {
                    AdminUserProp objAdminUserProp = new AdminUserProp();
                    objAdminUserProp.UserCode = UserCode;
                    objAdminUserProp.Password = Password;
                    objAdminUserProp.PIN = PIN;
                    objAdminUserProp.newRole = SelectedItem;
                    string[] lst = _serviceInstance.ModifiedUserRole(objAdminUserProp);

                    UserRole = new ObservableCollection<string>(lst);
                }
                else
                {
                    MessageBox.Show(Resources.ErrorAlreadyRoleAsign);
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
            return userRole;

        }

        /// <summary>
        /// Function to Bind the Removed/Updated User's Role.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-23,2016</createdOn>
        public ObservableCollection<string> RemoveUseRole()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                AppWorksService.Properties.AdminUserProp objAdminUserProp = new AppWorksService.Properties.AdminUserProp();
                objAdminUserProp.UserCode = UserCode;
                objAdminUserProp.Password = Password;
                objAdminUserProp.PIN = PIN;
                objAdminUserProp.newRole = SelectedItem;
                string[] lst = _serviceInstance.RemoveUserRole(objAdminUserProp);
                UserRole = new ObservableCollection<string>(lst);
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
            return userRole;

        }


        private IList<AdminUserinfo> userDetails;
        public IList<AdminUserinfo> UserDetails
        {
            get { return userDetails; }
            private set
            {
                if (value != null)
                {
                    this.userDetails = value;
                    NotifyPropertyChanged("UserDetails");
                }
            }
        }

        /// <summary>
        /// Function to Bind the Record For Modify.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-28,2016</createdOn>
        public void FillUserDetails()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                AdminUserProp objAdminUserProp = new AdminUserProp();
                objAdminUserProp.UserCode = tempUserCode;
                var list = _serviceInstance.GetModificationRecord(objAdminUserProp);

                UserID = list.UserIDInfo;
                UserCode = list.UserCodeInfo;
                FirstName = list.FirstNameInfo;
                LastName = list.LastNameInfo;
                Phone = list.PhoneInfo;
                FaxNumber = list.FaxNumberInfo;
                PhoneExtention = list.ExtentionInfo;
                CellPhone = list.CellPhoneInfo;
                LabelXOffset = list.LblXOffsetInfo.ToString();
                LabelYOffset = list.LblYOffsetInfo.ToString();
                PIN = list.PinInfo;
                EmailAddress = list.EmailInfo;
                Password = list.PasswordInfo;
                PortPassIDNumber = list.PortPassIdInfo;
                EmployeeNumber = list.EmployeeInfo;
                RecordStatus = list.RecordStatusInfo;
                CreatedBy = list.CreatedByInfo;
                CreationDate = list.CreationDateInfo;
                if (DisplayUserButton == "Hidden")
                    UpdatedDate = list.CreationDateInfo;
                else
                    UpdatedDate = DateTime.Now;
                UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                if (UserID > 0)
                    AllRoles(UserID);
                //LoadExistingUseRole();
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
        /// Function to Modify the Record.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>List</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-29,2016</createdOn>
        public void UpdateRecord()
        {
            try
            {
                string errMsg = string.Empty;
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                AdminUserProp objAdminUserProp = new AdminUserProp();
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    ValidateEmail emailValidation = new ValidateEmail();
                    bool isValid = emailValidation.IsValidEmail(EmailAddress);
                    if (!isValid)
                    {
                        MessageBox.Show(Resources.ErrorEmail);
                        return;
                    }
                }
                objAdminUserProp.UserID = UserID;
                objAdminUserProp.UserCode = UserCode;
                objAdminUserProp.FirstName = FirstName;
                objAdminUserProp.LastName = LastName;
                objAdminUserProp.Password = Password;
                objAdminUserProp.PIN = PIN;
                objAdminUserProp.Phone = Phone;
                objAdminUserProp.PhoneExtension = PhoneExtention;
                objAdminUserProp.CellPhone = CellPhone;
                objAdminUserProp.EmailAddress = EmailAddress;
                objAdminUserProp.FaxNumber = FaxNumber;
                objAdminUserProp.LabelXOffset = LabelXOffset;
                objAdminUserProp.LabelYOffset = LabelYOffset;
                objAdminUserProp.IMEI = IMEI;
                objAdminUserProp.LastConnection = LastConnection;
                objAdminUserProp.datsVersion = datsVersion;
                objAdminUserProp.RecordStatus = RecordStatus;
                objAdminUserProp.CreationDate = CreationDate;
                objAdminUserProp.CreatedBy = CreatedBy;
                objAdminUserProp.UpdatedDate = UpdatedDate;
                objAdminUserProp.UpdatedBy = UpdatedBy;
                objAdminUserProp.EmployeeNumber = EmployeeNumber;
                objAdminUserProp.PortPassIDNumber = PortPassIDNumber;
                objAdminUserProp.Department = Department;
                objAdminUserProp.StraightTimeRate = StraightTimeRate;
                objAdminUserProp.PieceRateRate = PieceRateRate;
                objAdminUserProp.PDIRate = PDIRate;
                objAdminUserProp.FlatBenefitPayRate = FlatBenefitPayRate;
                objAdminUserProp.AlternateEmailAddress = AlternateEmailAddress;

                if (userCode != null)
                {
                    _serviceInstance.UpdateUserDetails(objAdminUserProp, AvailableUserRoles.ToArray());
                    MessageBox.Show(Resources.msgUpdatedSuccessfully);
                    AcceptChanges();
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
                    FindUserDeligate.OnSetValueEvt -= new FindUserDeligate.SetValue(NotificationMessageReceived);
                    FindUserDeligate.OnSetValueEvtCmd -= new FindUserDeligate.SetValueCmd(NotificationCmdReceived);
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





    public class AdminUserinfo : ViewModelBase
    {
        private int userID;
        public int UserIDI
        {
            get { return userID; }
            set
            {
                if (value != null)
                {
                    userID = value;
                }
            }

        }

        private string userCode;
        public string UserCodeI
        {
            get { return userCode; }
            set
            {
                if (value != null)
                {
                    userCode = value;
                }
            }
        }

        private string firstName;
        public string FirstNameI
        {
            get { return firstName; }
            set
            {
                if (value != null)
                {
                    firstName = value;
                }
            }
        }

        private string lastName;
        public string LastNameI
        {
            get { return lastName; }
            set
            {
                if (value != null)
                {
                    lastName = value;
                }
            }
        }

        private string phone;
        public string PhoneI
        {
            get { return phone; }
            set
            {
                if (value != null)
                {
                    phone = value;
                }
            }
        }

        private string extention;
        public string ExtentionI
        {
            get { return extention; }
            set
            {
                if (value != null)
                {
                    extention = value;
                }
            }
        }

        private string cellPhone;
        public string CellPhoneI
        {
            get { return cellPhone; }
            set
            {
                if (value != null)
                {
                    cellPhone = value;
                }
            }
        }

        private string faxnumber;
        public string FaxNumberI
        {
            get { return faxnumber; }
            set
            {
                if (value != null)
                {
                    faxnumber = value;
                }
            }
        }

        private string email;
        public string EmailI
        {
            get { return email; }
            set
            {
                if (value != null)
                {
                    email = value;
                }
            }
        }

        private string password;
        public string PasswordI
        {
            get { return password; }
            set
            {
                if (value != null)
                {
                    password = value;
                }
            }
        }

        private string pin;
        public string PinI
        {
            get { return pin; }
            set
            {
                if (value != null)
                {
                    pin = value;
                }
            }
        }

        private decimal lblXOffset;
        public decimal LblXOffsetI
        {
            get { return lblXOffset; }
            set
            {
                if (value != null)
                {
                    lblXOffset = value;
                }
            }
        }

        private decimal lblYOffset;
        public decimal LblYOffsetI
        {
            get { return lblYOffset; }
            set
            {
                if (value != null)
                {
                    lblYOffset = value;
                }
            }
        }

        private string employee;
        public string EmployeeI
        {
            get { return employee; }
            set
            {
                if (value != null)
                {
                    employee = value;
                }
            }
        }


        private string recordStatus;
        public string RecordStatusI
        {
            get { return recordStatus; }
            set
            {
                if (value != null)
                {
                    recordStatus = value;
                }
            }
        }


        private string portPassId;
        public string PortPassIdI
        {
            get { return portPassId; }
            set
            {
                if (value != null)
                {
                    portPassId = value;
                }
            }
        }

        private Nullable<System.DateTime> creationDate;
        public Nullable<System.DateTime> CreationDateI
        {
            get { return creationDate; }
            set
            {
                if (value != null)
                {
                    creationDate = value;
                }
            }
        }

        private string createdBy;
        public string CreatedByI
        {
            get { return createdBy; }
            set
            {
                if (value != null)
                {
                    createdBy = value;
                }
            }
        }
    }
}
