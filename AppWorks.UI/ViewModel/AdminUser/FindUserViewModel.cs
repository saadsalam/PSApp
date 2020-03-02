using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using AppWorks.UI.Common;
using AppWorks.UI.View.UserControls;
using AppWorks.UI.Properties;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Configuration;
using AppWorks.UI.Controls.Extensions;

using AppWorksService.Properties;

namespace AppWorks.UI.ViewModel.AdminUser
{
    /// <summary>
    /// Model View Implentation for Find User
    /// </summary>
    public class FindUserViewModel : ViewModelBase, IDisposable
    {
        int _GridPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]);
        int _GridPageSizeOnScroll = Convert.ToInt32(ConfigurationManager.AppSettings["FindUserGridPageSizeOnScroll"]);


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


        public event EventHandler CloseWindowRequested;
        private void OnCloseWindowRequested()
        {
            EventHandler handler = CloseWindowRequested;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public FindUserViewModel()
        {
            DisplayFindUserTab = true;
            DisplayEditUserTab = false;
            SeletedTabFinduser = 0;
            if (!DesignTimeExtensions.IsDesignTime)
            {
                GetRecordStatus();
                GetAllRoles(0);
                FindUserDeligate.OnSetValueEvtCmdReferesh += new FindUserDeligate.SetValueCmdReferesh(UsersRecordList);
                FindUserDeligate.OnSetValueEvtTotalCountPagerCmd += new FindUserDeligate.SetValueTotalCountPager(GetTotalPageCount);
                FindUserDeligate.OnSetValuePageNumberCmd += new FindUserDeligate.SetValuePageNumberClick(GetCurrentPageIndex);             
            }
        }

        #region

        private string selectedStatus;
        private string selectedRole;
        private string firstName;
        private string lastName;
        private string userCode;
        private List<RoleList> roleList;
        private ObservableCollection<string> recordStatus;
        private string roleName;
        private string recordStatusName;
        private ICommand _btnClearResult_Click;
        private string cmdName;
        private Timer _searchTimer;
        #endregion

        private clsValidationPopUp clsValidationPopUp = new clsValidationPopUp();
        public clsValidationPopUp ClsValidationPopUp
        {
            get { return this.clsValidationPopUp; }
            set { this.clsValidationPopUp = value; NotifyPropertyChanged("ClsValidationPopUp"); }
        }


        private AppWorxCommand _btnSearch_Click;
        public AppWorxCommand btnSearch_Click
        {
            get
            {
                if (_btnSearch_Click == null)
                {
                    _btnSearch_Click = new AppWorxCommand(this.UsersRecordList);
                }
                return _btnSearch_Click;
            }
        }

        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        public ICommand btnClearResult_Click
        {
            get
            {
                if (_btnClearResult_Click == null)
                {
                    _btnClearResult_Click = new AppWorxCommand(
                        param => this.ClearAll(),
                        param => CanCheck
                        );
                }
                return _btnClearResult_Click;
            }
        }

        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>


        private AppWorxCommand _btnEdit_Click;
        public AppWorxCommand btnEdit_Click
        {
            get
            {
                if (_btnEdit_Click == null)
                {
                    //DisplayFindUserTab = false;
                    //DisplayEditUserTab = true;
                    //cmdName = "Edit";
                    _btnEdit_Click = new AppWorxCommand(this.FillUserData);
                }
                return _btnEdit_Click;
            }
        }

        private AppWorxCommand _btnEditSel_Click;
        public AppWorxCommand btnEditSel_Click
        {
            get
            {
                if (_btnEditSel_Click == null)
                {
                    //DisplayFindUserTab = false;
                    //DisplayEditUserTab = true;
                    //cmdName = "Edit";
                    _btnEditSel_Click = new AppWorxCommand(this.FillUserDataOnSelection);
                }
                return _btnEditSel_Click;
            }
        }

        private AppWorxCommand _btnView_Click;
        public AppWorxCommand btnView_Click
        {
            get
            {
                if (_btnView_Click == null)
                {
                    _btnView_Click = new AppWorxCommand(this.ViewUserData);
                }
                return _btnView_Click;
            }
        }

        /// <summary>
        /// for Checking parameter during command execution
        /// </summary>
        /// 
        private AppWorxCommand _btnDelete_Click;
        public AppWorxCommand btnDelete_Click
        {
            get
            {
                if (_btnDelete_Click == null)
                {

                    _btnDelete_Click = new AppWorxCommand(this.DeleteRecord);
                }
                return _btnDelete_Click;
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

        public string SelectedRole
        {
            get { return selectedRole; }
            set
            {
                if (value != null)
                { this.selectedRole = value; }
                NotifyPropertyChanged("SelectedRole");
            }
        }

        public string SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                if (value != null)
                { this.selectedStatus = value; }
                NotifyPropertyChanged("SelectedStatus");
            }
        }

        /// <summary>
        /// for holding and resturning the value of usercode
        /// </summary>
        public string UserCode
        {
            get { return userCode; }
            set
            {
                if (value != null)
                {
                    this.userCode = value;
                    _searchTimer = new Timer(UsersRecordList, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(500));
                }
                NotifyPropertyChanged("UserCode");
            }
        }

        /// <summary>
        /// for holding and resturning the value of firstName
        /// </summary>
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
        /// for holding and resturning the value of lastName
        /// </summary>
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

        private string _roleName;
        public string RoleName
        {
            get { return _roleName; }
            set
            {
                if (value != null)
                {
                    this._roleName = value;
                    NotifyPropertyChanged("RoleName");
                }
            }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (value != null)
                {
                    this.selectedIndex = value;
                    NotifyPropertyChanged("SelectedIndex");
                }
            }
        }
        /// <summary>
        /// for holding and resturning the value of lastName
        /// </summary>
        public List<RoleList> RoleList
        {
            get { return roleList; }
            private set
            {
                if (value != null)
                { this.roleList = value; }
                NotifyPropertyChanged("RoleList");
            }
        }

        /// <summary>
        /// for holding and resturning the value of lastName
        /// </summary>
        public ObservableCollection<string> RecordStatus
        {
            get { return recordStatus; }
            private set
            {
                if (value != null)
                { this.recordStatus = value; }
                NotifyPropertyChanged("RecordStatus");
            }
        }

        private ObservableCollection<UserDetails> userDetailList;
        public ObservableCollection<UserDetails> UserDetailList
        {
            get { return userDetailList; }
            private set
            {
                if (value != null)
                {
                    this.userDetailList = value;
                    NotifyPropertyChanged("UserDetailList");
                }
            }
        }


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
        /// Method To show the All Record Status
        /// </summary>
        public ObservableCollection<string> GetRecordStatus()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                FindUserProp objFindUserProp = new FindUserProp();
                string[] lst = _serviceInstance.RecordList();
                RecordStatus = new ObservableCollection<string>(lst);
                RecordStatus.Insert(0, "All");
                SelectedStatus = "All";
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
            return RecordStatus;

        }

        /// <summary>
        /// Method To show the All Roles 
        /// </summary>
        public void GetAllRoles(int userID)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                FindUserProp objFindUserProp = new FindUserProp();
                var lst = _serviceInstance.GetRolesSelection(0);
                if (lst.Count() > 0)
                {
                    RoleList = null;
                    RoleList = lst.ToList();
                    RoleList.Insert(0, new RoleList { RoleName = "All" });
                    RoleName = "All";
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
        /// Method To Clear All Record
        /// </summary>
        public void ClearAll()
        {
            try
            {
                FirstName = string.Empty;
                LastName = string.Empty;
                UserCode = string.Empty;
                RoleList.Clear();
                GetAllRoles(0);
                RecordStatus.Clear();
                GetRecordStatus();
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
            UsersRecordList("GridScroled");
        }


        /// <summary>
        /// Method To Show All Record
        /// </summary>
        public void UsersRecordList(object obj)
        {
            try
            {
                Count = 0;
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (obj == null)
                {
                    CurrentPageIndex = 0;
                }
                if (CurrentPageIndex == 0)
                {
                    Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
                    Application.Current.Properties["FindUserGridCurrentPageIndex"] = 0;
                }

                FindUserProp objFindUserProp = new FindUserProp();
                objFindUserProp.CurrentPageIndex = CurrentPageIndex;
                objFindUserProp.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize; ;
                objFindUserProp.DefaultPageSize = _GridPageSize;
                objFindUserProp.FirstName = FirstName;
                objFindUserProp.LastName = LastName;
                objFindUserProp.UserCode = UserCode;
                objFindUserProp.SelectedRole = RoleName;

                if (SelectedStatus != null && !SelectedStatus.Equals("All"))
                {
                    objFindUserProp.selectedStatusRole = SelectedStatus;
                }

                var list = _serviceInstance.GetUserRecord(objFindUserProp).Select(d => new UserDetails
                {
                    UdUserCode = d.UserCode,
                    UdUserFirstName = d.FirstName,
                    UdUserLastName = d.LastName,
                    UdUserStatus = d.UserStatus,
                    UdUserID = d.UserID,
                    TotalPageCount = d.TotalPageCount
                });

                if (CurrentPageIndex == 0)
                {
                    UserDetailList = null;
                    UserDetailList = new ObservableCollection<UserDetails>(list);
                }
                else
                {
                    if (UserDetailList != null && UserDetailList.Count > 0)
                    {
                        foreach (UserDetails ud in new ObservableCollection<UserDetails>(list))
                        {
                            UserDetailList.Add(ud);
                        }
                    }
                }
                Count = userDetailList.ToList().Where(x => x.TotalPageCount > 0).FirstOrDefault().TotalPageCount;
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

                if (_searchTimer != null)
                    _searchTimer.Dispose();
            }

        }

        private GridViewColumn columninfo;
        public GridViewColumn ColumnInfo
        {
            get { return columninfo; }
            set
            {
                if (value != null)
                {
                    this.columninfo = value;
                    NotifyPropertyChanged("ColumnInfo");
                }
            }
        }

        /// <summary>
        /// Method To Populate the Record in Find User 
        /// </summary>
        public void FillUserData(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                cmdName = Resources.btnEdit;
                VisibilityEditUserTab = true;
                FindUserDeligate.SetValueMethod(((AppWorks.UI.ViewModel.AdminUser.UserDetails)(obj)).UdUserCode);
                FindUserDeligate.SetValueMethodCmd(cmdName);
                SeletedTabFinduser = 1;
                OnCloseWindowRequested();

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
        /// Method To Populate the Record in Find User 
        /// </summary>
        public void FillUserDataOnSelection(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                Telerik.Windows.Controls.GridView.GridViewCell objSelectedUser = (Telerik.Windows.Controls.GridView.GridViewCell)obj;
                //AppWorks.UI.ViewModel.AdminUser.UserDetails selected = (AppWorks.UI.ViewModel.AdminUser.UserDetails) objSelectedUser;

                cmdName = Resources.btnView;
                VisibilityEditUserTab = true;
                FindUserDeligate.SetValueMethod(((AppWorks.UI.ViewModel.AdminUser.UserDetails)(objSelectedUser.Value)).UdUserCode);
                FindUserDeligate.SetValueMethodCmd(cmdName);
                SeletedTabFinduser = 1;
                //foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                //{
                //    if (window.Title.Equals("Find User"))
                //    {
                //        window.Close();
                //    }
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
        /// Method To Populate the Record in Find User 
        /// </summary>
        public void ViewUserData(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                cmdName = Resources.msgview;
                VisibilityEditUserTab = true;
                FindUserDeligate.SetValueMethod(((AppWorks.UI.ViewModel.AdminUser.UserDetails)(obj)).UdUserCode);
                FindUserDeligate.SetValueMethodCmd(cmdName);
                SeletedTabFinduser = 1;

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
        /// Function to Delete record for user.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>

        public void DeleteRecord(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                MessageBoxResult messageBoxResult = MessageBox.Show(Resources.MsgDeleteConfirm, Resources.msgTitleMessageBoxDelete, MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    string errMsg = string.Empty;
                    FindUserProp objFindUserProp = new FindUserProp();
                    objFindUserProp.UserID = obj.GetType().GetProperty("UdUserID").GetValue(obj).ToString();
                    objFindUserProp.RecordStatus = "Inactive";
                    objFindUserProp.FirstName = obj.GetType().GetProperty("UdUserFirstName").GetValue(obj).ToString(); ;
                    objFindUserProp.LastName = obj.GetType().GetProperty("UdUserLastName").GetValue(obj).ToString(); ;
                    objFindUserProp.UserCode = obj.GetType().GetProperty("UdUserCode").GetValue(obj).ToString(); ;
                    objFindUserProp.SelectedRole = SelectedRole;
                    string currentUserName = Application.Current.Properties["LoggedInUserName"].ToString().ToUpper();
                    string deleteUserName = objFindUserProp.UserCode.ToUpper();
                    if (deleteUserName != currentUserName)
                    {
                        int value = _serviceInstance.RemoveUserDetails(objFindUserProp);
                        if (SelectedStatus != null && !SelectedStatus.Equals("All"))
                        {
                            objFindUserProp.selectedStatusRole = SelectedStatus;
                        }
                        if (obj.GetType().GetProperty("UdUserStatus").GetValue(obj) != null && !obj.GetType().GetProperty("UdUserStatus").GetValue(obj).ToString().Equals("All"))
                        {
                            objFindUserProp.selectedStatusRole = obj.GetType().GetProperty("UdUserStatus").GetValue(obj).ToString(); ;
                        }
                        FindUserDeligate.SetValueMethodCmdReferesh(string.Empty);
                        MessageBox.Show(Resources.msgDeleteSuccessfully);
                        UsersRecordList(null);
                    }
                    else
                    {
                        MessageBox.Show(Resources.WarnDeleteCurrentUser);
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    FindUserDeligate.OnSetValueEvtCmdReferesh -= new FindUserDeligate.SetValueCmdReferesh(UsersRecordList);
                    FindUserDeligate.OnSetValueEvtTotalCountPagerCmd -= new FindUserDeligate.SetValueTotalCountPager(GetTotalPageCount);
                    FindUserDeligate.OnSetValuePageNumberCmd -= new FindUserDeligate.SetValuePageNumberClick(GetCurrentPageIndex);
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
    /// <summary>
    /// Class To Create to Keep The Information Of Users Search
    /// </summary>
    public class UserDetails //: ViewModelBase
    {
        #region

        //private int userUdID;
        //private string userUdfirstName;
        //private string userUdlastName;
        //private string userUdCode;
        //private string userUdStatus;


        #endregion
        public int UdUserID { get; set; }
        public string UdUserFirstName { get; set; }
        public string UdUserLastName { get; set; }
        public string UdUserCode { get; set; }
        public string UdUserStatus { get; set; }
        public long TotalPageCount { get; set; }

    }


}
