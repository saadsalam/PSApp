using System.ComponentModel;

namespace AppWorks.UI.Model
{
    public class LoginModel : INotifyPropertyChanged
    {

        //public string _username;
        public string _password;


        //public string Username { get { return _username; } set { _username = value; } }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyPropertyChanged("Username");
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //#region INotifyPropertyChanged Members

        //public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        //private void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        //}

        //#endregion


    }
}
