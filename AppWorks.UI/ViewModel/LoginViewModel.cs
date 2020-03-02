using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using AppWorks.UI.Common;
using AppWorks.UI.View.UserControls;
using AppWorks.UI.Properties;
using System.Reflection;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AppWorksService.Properties;
using AppWorks.WCFAuthentication.Lib.Behaviours;

namespace AppWorks.UI.ViewModel
{
    /// <summary>
    /// LoginViewModel Class To Contain all Property of Login View
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        #region "Class variables"
        public delegate void LoginStatus(bool isSuccess, string userName);
        public static event LoginStatus LoginStatusChanged;

        private clsValidationPopUp clsValidationPopUp = new clsValidationPopUp();
        public clsValidationPopUp ClsValidationPopUp
        {
            get { return this.clsValidationPopUp; }
            set { this.clsValidationPopUp = value; NotifyPropertyChanged("ClsValidationPopUp"); }
        }
        #endregion

        #region "Page Properties"
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                this.password = value;
                NotifyPropertyChanged("Password");
            }
        }

        private string username;
        public string UserName
        {
            get { return username; }
            set
            {
                if (value != null)
                {
                    this.username = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }

        private bool _isLogining;
        public bool IsLogining
        {
            get { return _isLogining; }
            private set
            {
                _isLogining = value;
                NotifyPropertyChanged("IsLogining");
                NotifyPropertyChanged("LoadingBarVisibility");
            }
        }

        public bool LoadingBarVisibility
        {
            get { return IsLogining; }
        }

        private int Counter = 0;
        #endregion

        #region "Page Events"
        private ICommand _btnSubmit_Click;

        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand btnSubmit_Click
        {
            get
            {
                if (_btnSubmit_Click == null)
                {
                    _btnSubmit_Click = new AppWorxCommand(
                        param => this.LoginMethod(),
                        param => CanCheck
                        );
                }
                return _btnSubmit_Click;
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
        /// Function to check the Login Credentials.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        private async void LoginMethod()
        {
            if (IsLogining)
            { return; }

            IsLogining = true;

            await Task.Run(() =>
            {

                string userRole = string.Empty;

                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                try
                {

                    LoginProperties objLoginProp = new LoginProperties();

                    if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrEmpty(Password))
                    {
                        objLoginProp.Username = UserName;
                        objLoginProp.Password = Password;

                        LoginResult result = _serviceInstance.ValidateLogin(objLoginProp);

                        if ((LoginStatusChanged != null) && (result.IsLoginSuccessful))

                        { LoginStatusChanged(true, "Admin"); }

                  
                        if (!result.IsLoginSuccessful)
                        {
                            ClsValidationPopUp.ErrMsgPassword = Resources.ErrorWrongCredential;
                            Password = string.Empty;
                            Counter++;
                            if (Counter >= 3)
                            {
                                // if 3 attempts fail then close the application.
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            // if login validates then get the user role details
                            List<string> userRoles = new List<string>();

                            ClientContext.UserToken = result.Token;

                            if (_serviceInstance == null)
                            {
                                throw new FaultException("Some error occurred while creating the service instance. Please try again.");
                            }

                            var userDetails = _serviceInstance.GetLoggedInUserDetails(objLoginProp);

                            userRoles = userDetails.UserRoles.OrderBy(x => x.ToString()).Select(x => x.ToString()).ToList();

                            List<string> objValidRoleList = ValidRoleList();

                            Application.Current.Properties["Token"] = result.Token;

                            Application.Current.Properties["LoggedInUserNameHome"] = userDetails.FullUserName;

                            Application.Current.Properties["LoggedInUserName"] = objLoginProp.Username;

                            userRole = ValidUserRole(userRoles, objValidRoleList);

                            LoadMainModel(userRole);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password))
                        {
                            ClsValidationPopUp.ErrMsgPassword = Resources.ErrorUsernameAndPasswordText;
                        }
                        else if (string.IsNullOrEmpty(UserName))
                        {
                            ClsValidationPopUp.ErrMsgPassword = Resources.ErrorUsernameText;
                        }
                        else if (string.IsNullOrEmpty(Password))
                        {
                            ClsValidationPopUp.ErrMsgPassword = Resources.ErrorPasswordText;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogErrorToDb(ex);
                    CommonSettings.logger.LogError(this.GetType(), ex);

                    // if the service is in a faulted state then show some message to user
                    if (_serviceInstance.State == CommunicationState.Faulted)
                    {
                        MessageBox.Show("An error has occured while accessing the service. Please close the application and try again.");
                        _serviceInstance.Abort(); 
                        _serviceInstance.Close();
                    }

                    if (ex != null)
                    {
                        if (ex.InnerException != null && ex.InnerException.InnerException != null)
                        {
                            if (ex.InnerException.InnerException.Message.Equals("The wait operation timed out") && ex.InnerException.Message.Equals("A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: TCP Provider, error: 0 - The wait operation timed out.)"))
                            {
                                MessageBox.Show("An error has occured while accessing the service. Please close the application and try again.");
                                return;
                            }
                            if (ex.InnerException.InnerException.Message.Equals("The wait operation timed out"))
                            {
                                MessageBox.Show("Connection Time out.");
                            }
                        }

                    }
                }
                finally
                {
                    IsLogining = false;
                    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                }
            });
        }

        /// <summary>
        /// Method to check the User's Valid Role
        /// </summary>
        /// <param name="existingUserRoleList"></param>
        /// <param name="ValidUserRoleList"></param>
        /// <returns>string</returns>
        private string ValidUserRole(List<string> existingUserRoleList, List<string> validUserRoleList)
        {
            StringBuilder userRoleList = new StringBuilder();

            foreach (string roleName in existingUserRoleList)
            {
                foreach (string vroleList in validUserRoleList)
                {
                    if (roleName.ToString().Replace(" ", "").Trim().ToLower() == vroleList.ToString().Replace(" ", "").Trim().ToLower())
                    {
                        userRoleList.Append(string.Format("{0},", vroleList.ToString().Replace(" ", "").Trim().ToLower()));
                    }
                }
            }

            return userRoleList.ToString().TrimEnd(',');
        }


        /// <summary>
        /// Method to Load Main Model after Login Successful.
        /// </summary>
        private async void LoadMainModel(string userRole)
        {
            await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                CommonSettings.logger.LogInfo(typeof(string),
                    string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart,
                        DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(),
                        MethodBase.GetCurrentMethod().Name));
                try
                {
                    if (!string.IsNullOrEmpty(userRole))
                    {
                        Application.Current.Properties["LoggedInUserRole"] = userRole;

                        HomeWindow homeWindow = new HomeWindow();

                        // close the login window and show home window
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.Title.Equals("login", StringComparison.OrdinalIgnoreCase))
                            {
                                window.Close();
                                homeWindow.Show();
                            }
                        }
                    }
                    else
                    {
                        clsValidationPopUp.ErrMsgPassword = Resources.ErrorNoRoleAsign;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogErrorToDb(ex);
                    bool displayErrorOnUI = false;
                    CommonSettings.logger.LogError(this.GetType(), ex);
                    if (displayErrorOnUI)
                    {
                        throw;
                    }
                }
                finally
                {
                    CommonSettings.logger.LogInfo(typeof(string),
                        string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd,
                            DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(),
                            MethodBase.GetCurrentMethod().Name));
                }

            }));

            IsLogining = false;
        }


        /// <summary>
        /// Method To Store the Valid UserRole.
        /// </summary>
        /// <returns></returns>
        private List<string> ValidRoleList()
        {
            List<string> objList = new List<string>();
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                objList.Add("Administrator");
                objList.Add("Port Storage");
                objList.Add("Security");
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
            return objList;
        }
    }
}
