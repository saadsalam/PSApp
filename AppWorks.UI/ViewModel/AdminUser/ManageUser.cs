using AppWorks.UI.Common;
using System.Windows.Input;


namespace AppWorks.UI.ViewModel.AdminUser
{
    public class ManageUser : ViewModelBase
    {
        public delegate void LoginStatus(bool isSuccess, string userName);
        public static event LoginStatus LoginStatusChanged;

        #region "Properties"

        private string _username;
        private string _phone;
        private string _extension;
        private string _cellPhone;
        private string _fax;
        private string _email;
        private string _password;
        private string _pin;
        private string _labelXOff;
        private string _lableYOff;
        private string _employee;
        private string _recordStatus;
        private string _portPassId;

        public string UserName
        {
            get { return _username; }
            set
            {
                if (value != null)
                {
                    this._username = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                this._password = value;
                NotifyPropertyChanged("Password");
                if (value != null)
                {
                    if (value.Length >= 4)
                    {

                    }
                }
            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (value != null)
                {
                    this._phone = value;
                    NotifyPropertyChanged("Phone");
                }
            }
        }
        public string Extension
        {
            get { return _extension; }
            set
            {
                if (value != null)
                {
                    this._extension = value;
                    NotifyPropertyChanged("Extension");
                }
            }
        }
        public string CellPhone
        {
            get { return _cellPhone; }
            set
            {
                if (value != null)
                {
                    this._cellPhone = value;
                    NotifyPropertyChanged("CellPhone");
                }
            }
        }
        public string Fax
        {
            get { return _fax; }
            set
            {
                if (value != null)
                {
                    this._fax = value;
                    NotifyPropertyChanged("Fax");
                }
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (value != null)
                {
                    this._email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }
        public string Pin
        {
            get { return _pin; }
            set
            {
                if (value != null)
                {
                    this._pin = value;
                    NotifyPropertyChanged("PIN");
                }
            }
        }
        public string LabelXOff
        {
            get { return _labelXOff; }
            set
            {
                if (value != null)
                {
                    this._labelXOff = value;
                    NotifyPropertyChanged("LabelXOff");
                }
            }
        }
        public string LableYOff
        {
            get { return _lableYOff; }
            set
            {
                if (value != null)
                {
                    this._lableYOff = value;
                    NotifyPropertyChanged("LableYOff");
                }
            }
        }
        public string Employee
        {
            get { return _employee; }
            set
            {
                if (value != null)
                {
                    this._employee = value;
                    NotifyPropertyChanged("Employee");
                }
            }
        }
        public string RecordStatus
        {
            get { return _recordStatus; }
            set
            {
                if (value != null)
                {
                    this._recordStatus = value;
                    NotifyPropertyChanged("RecordStatus");
                }
            }
        }
        public string PortPassId
        {
            get { return _portPassId; }
            set
            {
                if (value != null)
                {
                    this._portPassId = value;
                    NotifyPropertyChanged("PortPassId");
                }
            }
        }
        #endregion

        #region "ViewModel Events"
        private ICommand _btnSubmit_Click;

        // ICommand
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
                        param => this.AddUser(),
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
        # endregion

        #region Page "Functions"
        /// <summary>
        /// method to check for user validation
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private void AddUser()
        {
            CommonSettings.logger.LogInfo(typeof(string), "LoginMethod....Start");


            CommonSettings.logger.LogInfo(typeof(string), "LoginMethod....End");
        }
        #endregion
    }
}
