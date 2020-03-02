using AppWorks.UI.Common;
using AppWorks.UI.Controls.Attributes;
using AppWorks.UI.Properties;
using AppWorks.UI.View.WebPortal;
using AppWorksService.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace AppWorks.UI.ViewModel.WebPortal
{
    public class WebPortalVM : ChangeTrackingViewModel, IDisposable
    {
        int _GridPageSize = 30;
        int _GridPageSizeOnScroll = Convert.ToInt32(ConfigurationManager.AppSettings["FindUserGridPageSizeOnScroll"]);
        public WebPortalVM()
        {
            IsModify = true;
            IsButtonPanel = Resources.MsgHidden;
            isVisibleInfo = Resources.MsgHidden;
            LoadUsers(null);
            View_Click(null);
            RegisterDelegates();
            currentView = new WebPortalUsersView();
            AcceptChanges();
        }

        /// <summary>
        /// Regsiter delegates for this module
        /// </summary>
        void RegisterDelegates()
        {
            WebPortalDelegate.OnSetValueEvtTotalCountPagerCmd += new WebPortalDelegate.SetValueTotalCountPager(GetTotalPageCount);
            WebPortalDelegate.OnSetValuePageNumberCmd += new WebPortalDelegate.SetValuePageNumberClick(GetCurrentPageIndex);
            WebPortalDelegate.OnSetValueEvtEditCmd += new WebPortalDelegate.SetValueEditCmd(GetEditCmd);
        }

        /// <summary>
        /// Get the current page index
        /// </summary>
        /// <param name="currentPageIndx"></param>
        public void GetEditCmd(string cmd, object obj)
        {
            if (cmd == "Edit")
            {
                Edit_Click(obj);
            }
            else if (cmd == "Delete")
            {
                Delete_Click(obj);
            }
        }

        #region WebPortal Properties

        private object currentView;
        public object CurrentView
        {
            get { return this.currentView; }
            set
            {
                if (this.currentView == value) return;
                this.currentView = value;
                NotifyPropertyChanged("CurrentView");
            }
        }

        private string isVisibleInfo;
        public string IsVisibleInfo
        {
            get { return isVisibleInfo; }
            set { isVisibleInfo = value; NotifyPropertyChanged("IsVisibleInfo"); }
        }

        private bool userInfoEnabled;
        public bool UserInfoEnabled
        {
            get { return userInfoEnabled; }
            set { userInfoEnabled = value; NotifyPropertyChanged("UserInfoEnabled"); }
        }
        private bool isModify;
        public bool IsModify
        {
            get { return isModify; }
            set { isModify = value; NotifyPropertyChanged("IsModify"); }
        }
        private string isButtonPanel;
        public string IsButtonPanel
        {
            get { return isButtonPanel; }
            set { isButtonPanel = value; NotifyPropertyChanged("IsButtonPanel"); }
        }
        private string isAction;
        public string IsAction
        {
            get { return isAction; }
            set { isAction = value; NotifyPropertyChanged("IsAction"); }
        }

        private int? userID;
        public int? UserID
        {
            get { return userID; }
            set
            {
                this.userID = value;
                NotifyPropertyChanged("UserID");
            }
        }

        private string userName;
        [ChangeTracking]
        public string UserName
        {
            get { return userName; }
            set
            {
                this.userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        private string password;
        [ChangeTracking]
        public string Password
        {
            get { return password; }
            set
            {
                this.password = value;
                NotifyPropertyChanged("Password");
            }
        }

        private string firstName;
        [ChangeTracking]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                this.firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        private string lastName;
        [ChangeTracking]
        public string LastName
        {
            get { return lastName; }
            set
            {
                this.lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        private DateTime? lastLogin;
        [ChangeTracking]
        public DateTime? LastLogin
        {
            get { return lastLogin; }
            set
            {
                this.lastLogin = value;
                NotifyPropertyChanged("LastLogin");
            }
        }
        private string email;
        [ChangeTracking]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                this.email = value;
                NotifyPropertyChanged("Email");
            }
        }
        private string phone;
        [ChangeTracking]
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                this.phone = value;
                NotifyPropertyChanged("Phone");
            }
        }
        private View.UserControls.clsValidationPopUp clsValidationPopUp = new View.UserControls.clsValidationPopUp();
        public View.UserControls.clsValidationPopUp ClsValidationPopUp
        {
            get { return this.clsValidationPopUp; }
            set { this.clsValidationPopUp = value; NotifyPropertyChanged("ClsValidationPopUp"); }
        }
        private int? isActive;
        [ChangeTracking]
        public int? IsActive
        {
            get { return isActive; }
            set
            {
                this.isActive = value;
                NotifyPropertyChanged("IsActive");
            }
        }

        private int moduleID;
        public int ModuleID
        {
            get { return moduleID; }
            set
            {
                this.moduleID = value;
                NotifyPropertyChanged("ModuleID");
            }
        }

        private string moduleName;
        public string ModuleName
        {
            get { return moduleName; }
            set
            {
                this.moduleName = value;
                NotifyPropertyChanged("ModuleName");
            }
        }

        private string moduleCode;
        public string ModuleCode
        {
            get { return moduleCode; }
            set
            {
                this.moduleCode = value;
                NotifyPropertyChanged("ModuleCode");
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                this.isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        private ObservableCollection<WebPortalUserList> listUser;
        public ObservableCollection<WebPortalUserList> ListUser
        {
            get
            {
                return listUser;
            }
            set
            {
                this.listUser = value;
                NotifyPropertyChanged("ListUser");
            }
        }

        private WebPortalUserList selectedUser;

        public WebPortalUserList SelectedUser
        {
            get { return selectedUser; }
            set
            {
                this.selectedUser = value;
                NotifyPropertyChanged("SelectedUser");
            }
        }
        private List<ModuleList> listModule;
        public List<ModuleList> ListModule
        {
            get
            {
                return listModule;
            }
            set
            {
                this.listModule = value;
                NotifyPropertyChanged("ListModule");
            }
        }
        private List<UserCustomerList> listCustomer;
        public List<UserCustomerList> ListCustomer
        {
            get
            {
                return listCustomer;
            }
            set
            {
                this.listCustomer = value;
                NotifyPropertyChanged("ListCustomer");
            }
        }
        private List<RoleList> listRole;
        public List<RoleList> ListRole
        {
            get
            {
                return listRole;
            }
            set
            {
                this.listRole = value;
                NotifyPropertyChanged("ListRole");
            }
        }

        private List<GroupList> listGroup;
        public List<GroupList> ListGroup
        {
            get
            {
                return listGroup;
            }
            set
            {
                this.listGroup = value;
                NotifyPropertyChanged("ListGroup");
            }
        }
        private AppWorxCommand btnViewClick;
        public AppWorxCommand BtnViewClick
        {
            get
            {
                if (btnViewClick == null)
                {
                    btnViewClick = new AppWorxCommand(this.View_Click);
                }
                return btnViewClick;
            }
        }
        private AppWorxCommand btnEditClick;
        public AppWorxCommand BtnEditClick
        {
            get
            {
                if (btnEditClick == null)
                {
                    btnEditClick = new AppWorxCommand(this.Edit_Click);
                }
                return btnEditClick;
            }
        }
        private AppWorxCommand btnDeleteClick;
        public AppWorxCommand BtnDeleteClick
        {
            get
            {
                if (btnDeleteClick == null)
                {
                    btnDeleteClick = new AppWorxCommand(this.Delete_Click);
                }
                return btnDeleteClick;
            }
        }

        private AppWorxCommand btnNewClick;
        public AppWorxCommand BtnNewClick
        {
            get
            {
                if (btnNewClick == null)
                {
                    btnNewClick = new AppWorxCommand(this.New_Click);
                }
                return btnNewClick;
            }
        }

        private AppWorxCommand btnCancelClick;
        public AppWorxCommand BtnCancelClick
        {
            get
            {
                if (btnCancelClick == null)
                {
                    btnCancelClick = new AppWorxCommand(this.Cancel_Click);
                }
                return btnCancelClick;
            }
        }

        private AppWorxCommand btnSaveClick;
        public AppWorxCommand BtnSaveClick
        {
            get
            {
                if (btnSaveClick == null)
                {
                    btnSaveClick = new AppWorxCommand(this.Save_Click);
                }
                return btnSaveClick;
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
                NotifyPropertyChanged("CurrentPageIndex");
            }
        }
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
        /// Get Total page count for grid
        /// </summary>
        /// 
        public void GetTotalPageCount(long totalPageCount)
        {
            TotalPageCount = totalPageCount;
        }

        /// <summary>
        /// Get the current page index
        /// </summary>
        /// <param name="currentPageIndx"></param>
        public void GetCurrentPageIndex(int currentPageIndx)
        {
            CurrentPageIndex = currentPageIndx;
            LoadUsers("GridScroled");
        }

        /// <summary>
        /// Method to load all the users.
        /// </summary>
        /// <param name="obj"></param>
        public async void LoadUsers(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                Count = 0;
                             
                if (obj == null)
                {
                    CurrentPageIndex = 0;
                }
                if (CurrentPageIndex == 0)
                {
                    Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
                    Application.Current.Properties["FindUserGridCurrentPageIndex"] = 0;
                }

                WebPortalUserList objUserList = new WebPortalUserList();
                objUserList.CurrentPageIndex = CurrentPageIndex;
                objUserList.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize; ;
                objUserList.DefaultPageSize = _GridPageSize;

                var portalUsers = (await _serviceInstance.GetUsersAsync(objUserList)).Select(d => new WebPortalUserList
                {
                    userID = d.userID,
                    username = d.username,
                    password = d.password,
                    firstname = d.firstname,
                    lastname = d.lastname,
                    lastLogin = d.lastLogin,
                    email = d.email,
                    phone = d.phone,
                    isActive = d.isActive,
                    TotalPageCount = d.TotalPageCount
                }); ;               

                if (CurrentPageIndex == 0)
                {
                    ListUser = null;
                    ListUser = new ObservableCollection<WebPortalUserList>(portalUsers);
                }
                else
                {
                    if (ListUser != null && ListUser.Count > 0)
                    {
                        foreach (WebPortalUserList ud in new ObservableCollection<WebPortalUserList>(portalUsers))
                        {
                            ListUser.Add(ud);
                        }
                    }
                }

                Count = ListUser.ToList().Where(x => x.TotalPageCount > 0).FirstOrDefault().TotalPageCount;
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }
        /// <summary>
        /// Function to view user's information.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        public void View_Click(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                WebPortalVM objSelectedUser = (WebPortalVM)obj;
                if (objSelectedUser != null) //Todo
                {
                    if (objSelectedUser.SelectedUser != null)
                    {
                        UserInfoEnabled = false;
                        IsModify = true;
                        IsButtonPanel = Resources.MsgHidden;
                        IsVisibleInfo = Resources.MsgVisible;
                        UserID = objSelectedUser.SelectedUser.userID;
                        UserName = objSelectedUser.SelectedUser.username;
                        Password = objSelectedUser.SelectedUser.password;
                        Email = objSelectedUser.SelectedUser.email;
                        Phone = objSelectedUser.SelectedUser.phone;
                        IsActive = (int?)objSelectedUser.SelectedUser.isActive;
                        FirstName = objSelectedUser.SelectedUser.firstname;
                        LastName = objSelectedUser.SelectedUser.lastname;
                        LastLogin = objSelectedUser.SelectedUser.lastLogin;
                        if (UserID > 0)
                        {
                            listCustomer = null;
                            ListModule = null;
                            ListRole = null;
                            ListGroup = null;
                            FillModules((int)UserID);
                            FillCustomers((int)UserID);
                            FillRoles((int)userID);
                            FillGroups((int)(UserID));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
                WebPortalUserList objFirstUser = new WebPortalUserList();
                objFirstUser = ListUser.ToList().FirstOrDefault();
                UserInfoEnabled = false;
                IsModify = true;
                IsButtonPanel = Resources.MsgHidden;
                IsVisibleInfo = Resources.MsgVisible;
                UserID = objFirstUser.userID;
                UserName = objFirstUser.username;
                Password = objFirstUser.password;
                Email = objFirstUser.email;
                Phone = objFirstUser.phone;
                IsActive = (int)objFirstUser.isActive;
                FirstName = objFirstUser.firstname;
                LastName = objFirstUser.lastname;
                LastLogin = objFirstUser.lastLogin;
                if (UserID > 0)
                {
                    listCustomer = null;
                    ListModule = null;
                    ListRole = null;
                    ListGroup = null;
                    FillModules((int)UserID);
                    FillCustomers((int)UserID);
                    FillRoles((int)userID);
                    FillGroups((int)(UserID));
                }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }


        public void Edit_Click(object obj)
        {

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (obj != null)
                {

                    WebPortalUserList objSelectedUser = (WebPortalUserList)obj;
                    if (objSelectedUser != null)
                    {
                        UserInfoEnabled = true;
                        IsModify = true;
                        IsButtonPanel = Resources.MsgVisible;
                        IsAction = Resources.ActionUpdate;
                        IsVisibleInfo = Resources.MsgVisible;
                        UserID = objSelectedUser.userID;
                        UserName = objSelectedUser.username;
                        Password = objSelectedUser.password;
                        Email = objSelectedUser.email;
                        Phone = objSelectedUser.phone;
                        IsActive = (int)objSelectedUser.isActive;
                        FirstName = objSelectedUser.firstname;
                        LastName = objSelectedUser.lastname;
                        LastLogin = objSelectedUser.lastLogin;
                        if (UserID > 0)
                        {
                            listCustomer = null;
                            ListModule = null;
                            ListRole = null;
                            ListGroup = null;
                            FillModules((int)UserID);
                            FillCustomers((int)UserID);
                            FillRoles((int)userID);
                            FillGroups((int)(UserID));
                        }
                        AcceptChanges();
                    }
                    CurrentView = new WebPortalUserDetailsView();
                }
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to create a new customer.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        public void New_Click(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                UserInfoEnabled = true;
                IsModify = false;
                IsButtonPanel = Resources.MsgVisible;
                IsAction = Resources.ActionSave;
                IsVisibleInfo = Resources.MsgVisible;
                if (ListCustomer == null)
                    FillCustomers(0);
                if (ListModule == null)
                    FillModules(0);
                if (ListRole == null)
                    FillRoles(0);
                if (ListGroup == null)
                    FillGroups(0);
                ClearUserInfo();
                CurrentView = new WebPortalUserDetailsView();
                AcceptChanges();
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to delete a particular user.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        public void Delete_Click(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                MessageBoxResult result = MessageBox.Show("Are You Sure to Delete this User?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    WebPortalUserList objSelectedUser = (WebPortalUserList)obj;

                    if (objSelectedUser != null)
                    {
                        UserID = objSelectedUser.userID;

                        bool isSuccessful = _serviceInstance.DeleteUser((int)UserID);

                        if (isSuccessful)
                        {
                            MessageBox.Show(Resources.MsgRecordDeleted);
                            IsModify = true;
                            IsButtonPanel = Resources.MsgHidden;
                            UserInfoEnabled = false;
                            ClearUserInfo();
                            ListUser = null;
                            LoadUsers(null);
                            IsVisibleInfo = Resources.MsgHidden;
                        }
                        else
                        {
                            MessageBox.Show(Resources.WarnDeleteCurrentUser);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to cancel an action.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        public void Cancel_Click(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                IsModify = true;
                UserInfoEnabled = false;
                IsButtonPanel = Resources.MsgHidden;
                IsVisibleInfo = Resources.MsgHidden;
                if (ListCustomer == null)
                    FillCustomers(0);
                if (ListModule == null)
                    FillModules(0);
                if (ListRole == null)
                    FillRoles(0);
                if (ListGroup == null)
                    FillGroups(0);
                ClearUserInfo();
                View_Click(null);
                CurrentView = new WebPortalUsersView();
                AcceptChanges();
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// For clearing user information from controls
        /// </summary>
        public void ClearUserInfo()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                UserID = null;
                UserName = null;
                Password = null;
                Email = null;
                IsActive = null;
                FirstName = null;
                LastName = null;
                LastLogin = null;
                Phone = null;
                ListCustomer = ListCustomer.Select(x => new UserCustomerList { CustomerName = x.CustomerName, CustomerID = x.CustomerID, IsSelected = false }).ToList();
                ListModule = ListModule.Select(x => new ModuleList { ModuleID = x.ModuleID, ModuleName = x.ModuleName, IsSelected = false, ModuleCode = x.ModuleCode }).ToList();
                ListRole = ListRole.Select(x => new RoleList { RoleID = x.RoleID, RoleName = x.RoleName, Description = x.Description, IsSelected = false }).ToList();
                ListGroup = ListGroup.Select(x => new GroupList { GroupID = x.GroupID, GroupName = x.GroupName, Description = x.Description, IsSelected = false }).ToList();
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get the modules for a particular user
        /// </summary>
        /// <param name="userID"></param>
        public void FillModules(int userID)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var qry = _serviceInstance.GetModules(userID).Select(d => new ModuleList
                {
                    ModuleID = d.ModuleID,
                    ModuleName = d.ModuleName,
                    ModuleCode = d.ModuleCode,
                    IsSelected = d.IsSelected
                });

                ListModule = new List<ModuleList>(qry);
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get the Customers for a particular user
        /// </summary>
        /// <param name="userID"></param>
        public void FillCustomers(int userID)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var qry = _serviceInstance.GetCustomers(userID).Select(d => new UserCustomerList
                {
                    CustomerName = d.CustomerName,
                    CustomerID = d.CustomerID,
                    IsSelected = d.IsSelected,
                    UserID = d.UserID
                });

                ListCustomer = new List<UserCustomerList>(qry);
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get the Roles for a particular user
        /// </summary>
        /// <param name="userID"></param>
        public void FillRoles(int userID)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var qry = _serviceInstance.GetRoles(userID).Select(d => new RoleList
                {
                    RoleName = d.RoleName,
                    RoleID = d.RoleID,
                    Description = d.Description,
                    IsSelected = d.IsSelected,
                    UserID = d.UserID
                });

                ListRole = new List<RoleList>(qry);

            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// To get the Groups for a particular user
        /// </summary>
        /// <param name="userID"></param>
        public void FillGroups(int userID)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var qry = _serviceInstance.GetGroups(userID).Select(d => new GroupList
                {
                    GroupName = d.GroupName,
                    GroupID = d.GroupID,
                    Description = d.Description,
                    IsSelected = d.IsSelected,
                    UserID = d.UserID
                });

                ListGroup = new List<GroupList>(qry);

            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to Save/Update a particular user.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        public void Save_Click(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                WebPortalUserList objUserProp = new WebPortalUserList();

                if (UserID != null && UserID > 0)
                { objUserProp.userID = (int)UserID; }

                objUserProp.username = UserName;
                objUserProp.password = Password;
                objUserProp.firstname = FirstName;
                objUserProp.lastname = LastName;
                objUserProp.email = Email;
                objUserProp.phone = Phone;
                objUserProp.isActive = IsActive;
                objUserProp.isSuperUser = 0;
                objUserProp.lastLogin = DateTime.Now;
                if (IsAction == Resources.ActionSave)
                {
                    objUserProp.whoCreated = Application.Current.Properties["LoggedInUserName"].ToString();
                    objUserProp.dateCreated = DateTime.Now;
                }
                else if (IsAction == Resources.ActionUpdate)
                {
                    objUserProp.whoModified = Application.Current.Properties["LoggedInUserName"].ToString();
                    objUserProp.dateModified = DateTime.Now;
                }

                if (!string.IsNullOrEmpty(UserName))
                {
                    if (!string.IsNullOrEmpty(Password))
                    {
                        if (!string.IsNullOrEmpty(Email))
                        {
                            ValidateEmail emailValidation = new ValidateEmail();
                            bool isValid = emailValidation.IsValidEmail(Email);
                            if (isValid)
                            {
                                bool checkDuplicateUserName = _serviceInstance.CheckDuplicateEmail(Email);
                                bool checkDuplicateEmailID = _serviceInstance.CheckDuplicatePortalUserName(UserName);
                                bool isSuccessfull = false;
                                if (IsAction == Resources.ActionSave)
                                {
                                    if (!checkDuplicateUserName)
                                    {
                                        if (!checkDuplicateEmailID)
                                            isSuccessfull = _serviceInstance.InsertUpdateUser(objUserProp, ListCustomer.ToArray(), ListModule.ToArray(), ListRole.ToArray(), ListGroup.ToArray());
                                        else
                                            MessageBox.Show(Resources.MsgDuplicateUserName);
                                    }
                                    else
                                        MessageBox.Show(Resources.MsgDuplicateEmailID);
                                }
                                else if (IsAction == Resources.ActionUpdate)
                                    isSuccessfull = _serviceInstance.InsertUpdateUser(objUserProp, ListCustomer.ToArray(), ListModule.ToArray(), ListRole.ToArray(), ListGroup.ToArray());

                                if (isSuccessfull)
                                {
                                    if (IsAction == Resources.ActionUpdate)
                                        MessageBox.Show(Resources.msgUpdatedSuccessfully);
                                    else if (IsAction == Resources.ActionSave)
                                        MessageBox.Show(Resources.msgInsertedSuccessfully);
                                    ClearUserInfo();
                                    CurrentView = new WebPortalUsersView();
                                    IsVisibleInfo = Resources.MsgHidden;
                                    IsModify = true;
                                    IsButtonPanel = Resources.MsgHidden;
                                    IsAction = Resources.ActionSave;
                                    LoadUsers(null);
                                    View_Click(null);
                                    AcceptChanges();
                                }
                            }
                            else
                            { MessageBox.Show(Resources.ErrorEmail); }
                        }
                        else
                        { MessageBox.Show(Resources.ReqEmail); }
                    }
                    else
                    { MessageBox.Show(Resources.ReqPassword); }
                }
                else
                { MessageBox.Show(Resources.ReqrUserName); }
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
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
                    WebPortalDelegate.OnSetValueEvtTotalCountPagerCmd -= new WebPortalDelegate.SetValueTotalCountPager(GetTotalPageCount);
                    WebPortalDelegate.OnSetValuePageNumberCmd -= new WebPortalDelegate.SetValuePageNumberClick(GetCurrentPageIndex);
                    WebPortalDelegate.OnSetValueEvtEditCmd -= new WebPortalDelegate.SetValueEditCmd(GetEditCmd);
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
