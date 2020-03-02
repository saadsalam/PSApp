using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppWorksService.Properties;
using System.Windows;
using System.Windows.Input;
using AppWorks.UI.Properties;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using AppWorks.UI.Controls.Attributes;
using AppWorks.UI.View.UserControls.CodeAdmin;
using AppWorks.UI.View;

namespace AppWorks.UI.ViewModel
{
    public class SystemSettingsVM : ChangeTrackingViewModel
    {
        List<AdminUserProp> prevSettings;
        public SystemSettingsVM()
        {
            GetSystemSettings();
            GetCompanyInformation();
            AcceptChanges();
        }

        private ICommand _newButton_Click;
        public ICommand NewButton_Click
        {
            get
            {
                if (_newButton_Click == null)
                {
                    _newButton_Click = new AppWorxCommand(
                        param => this.CreateNew(),
                        param => CanCheck
                        );
                }
                return _newButton_Click;
            }
        }


        private ICommand _findButton_Click;
        public ICommand FindButton_Click
        {
            get
            {
                if (_findButton_Click == null)
                {
                    _findButton_Click = new AppWorxCommand(
                        param => this.Find(),
                        param => CanCheck
                        );
                }
                return _findButton_Click;
            }
        }

        public void CreateNew()
        {
            string mode = "New";
            OpenSettingsWindow(mode);
        }

        public void Find()
        {
            string mode = "Find";
            OpenSettingsWindow(mode);
        }

        public void OpenSettingsWindow(string mode)
        {
            Window window = new SystemsSettingsAdmin(mode)
            {
                Title = string.Format("{0} System Settings", mode),
                Width = 700,
                Height = 550,
                ResizeMode = ResizeMode.CanMinimize,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = 150,
                Top = 50                
            };
            window.Show();
        }

        /// <summary>
        /// for Checking parameter during command execution
        /// </summary>
        public static bool CanCheck
        {
            get
            {
                return true;
            }
        }

        private ICommand _btnSubmit_Click;
        public ICommand btnSubmit_Click
        {
            get
            {
                if (_btnSubmit_Click == null)
                {
                    _btnSubmit_Click = new AppWorxCommand(
                        param => this.UpdateCompanyInfo(),
                        param => CanCheck
                        );
                }
                return _btnSubmit_Click;
            }
        }

        /// <summary>
        /// Fill button event
        /// </summary>
        private AppWorxCommand btnSave_Click;
        public AppWorxCommand BtnSave_Click
        {
            get
            {
                if (btnSave_Click == null)
                {
                    btnSave_Click = new AppWorxCommand(this.Save_Click);
                }
                return btnSave_Click;
            }
        }

        private string companyName;
        [ChangeTracking]
        public string CompanyName
        {
            get { return companyName; }
            set
            {
                if (value != null)
                {
                    this.companyName = value;
                    NotifyPropertyChanged("CompanyName");
                }
            }
        }

        private string addressLine1;
        [ChangeTracking]
        public string AddressLine1
        {
            get { return addressLine1; }
            set
            {
                if (value != null)
                {
                    this.addressLine1 = value;
                    NotifyPropertyChanged("AddressLine1");
                }
            }
        }

        private string addressLine2;
        [ChangeTracking]
        public string AddressLine2
        {
            get { return addressLine2; }
            set
            {
                if (value != null)
                {
                    this.addressLine2 = value;
                    NotifyPropertyChanged("AddressLine2");
                }
            }
        }

        private string city;
        [ChangeTracking]
        public string City
        {
            get { return city; }
            set
            {
                if (value != null)
                {
                    this.city = value;
                    NotifyPropertyChanged("City");
                }
            }
        }

        private string state;
        [ChangeTracking]
        public string State
        {
            get { return state; }
            set
            {
                if (value != null)
                {
                    this.state = value;
                    NotifyPropertyChanged("State");
                }
            }
        }

        private string zip;
        [ChangeTracking]
        public string Zip
        {
            get { return zip; }
            set
            {
                if (value != null)
                {
                    this.zip = value;
                    NotifyPropertyChanged("Zip");
                }
            }
        }

        private string phone;
        [ChangeTracking]
        public string Phone
        {
            get { return phone; }
            set
            {
                if (value != null)
                {
                    this.phone = value;
                    NotifyPropertyChanged("Phone");
                }
            }
        }

        private string fax;
        [ChangeTracking]
        public string Fax
        {
            get { return fax; }
            set
            {
                if (value != null)
                {
                    this.fax = value;
                    NotifyPropertyChanged("Fax");
                }
            }
        }

        private string systemName;
        [ChangeTracking]
        public string SystemName
        {
            get { return systemName; }
            set
            {
                if (value != null)
                {
                    this.systemName = value;
                    NotifyPropertyChanged("SystemName");
                }
            }
        }

        private int? nextOrderNumber;
        [ChangeTracking]
        public int? NextOrderNumber
        {
            get { return nextOrderNumber; }
            set
            {
                if (value != null)
                {
                    this.nextOrderNumber = value;
                    NotifyPropertyChanged("NextOrderNumber");
                }
            }
        }

        private string createdBy;
        [ChangeTracking]
        public string CreatedBy
        {
            get { return createdBy; }
            set
            {
                if (value != null)
                {
                    this.createdBy = value;
                    NotifyPropertyChanged("CreatedBy");
                }
            }
        }

        private string updatedBy;
        [ChangeTracking]
        public string UpdatedBy
        {
            get { return updatedBy; }
            set
            {
                if (value != null)
                {
                    this.updatedBy = value;
                    NotifyPropertyChanged("UpdatedBy");
                }
            }
        }

        private DateTime? createdDate;
        [ChangeTracking]
        public DateTime? CreatedDate
        {
            get { return createdDate; }
            set
            {
                if (value != null)
                {
                    this.createdDate = value;
                    NotifyPropertyChanged("createdDate");
                }
            }
        }

        private DateTime? updatedDate;
        [ChangeTracking]
        public DateTime? UpdatedDate
        {
            get { return updatedDate; }
            set
            {
                if (value != null)
                {
                    this.updatedDate = value;
                    NotifyPropertyChanged("UpdatedDate");
                }
            }
        }
        private string valueKey;
        public string ValueKey
        {
            get { return valueKey; }
            set
            {
                if (value != null)
                {
                    this.valueKey = value;
                    NotifyPropertyChanged("ValueKey");
                }
            }
        }

        private string valueDescription;
        public string ValueDescription
        {
            get { return valueDescription; }
            set
            {
                if (value != null)
                {
                    this.valueDescription = value;
                    NotifyPropertyChanged("ValueDescription");
                }
            }
        }

        private List<AdminUserProp> listSettings;
        public List<AdminUserProp> ListSettings
        {
            get { return listSettings; }
            set
            {
                if (value != null)
                {
                    this.listSettings = value;
                    NotifyPropertyChanged("ListSettings");
                }
            }
        }

        public void GetSystemSettings()
        {
            var lstSystemSettings = _serviceInstance.GetSystemSettings();
            if (lstSystemSettings.Count() > 0)
            {
                ListSettings = null;
                ListSettings = lstSystemSettings.ToList();
            }
        }

        public void GetCompanyInformation()
        {
            string userCode = Application.Current.Properties["LoggedInUserName"].ToString();
            var lstSystemSettings = _serviceInstance.GetDAIAddressName(userCode).FirstOrDefault();
            if (lstSystemSettings != null)
            {

                CompanyName = lstSystemSettings.CompanyName;
                AddressLine1 = lstSystemSettings.AddressLine1;
                AddressLine2 = lstSystemSettings.AddressLine2;
                City = lstSystemSettings.CitySep;
                State = lstSystemSettings.State;
                Zip = lstSystemSettings.Zip;
                Phone = lstSystemSettings.Phone;
                //Phone = Phone;
                Fax = lstSystemSettings.FaxNumber;
                //Fax = Fax.Length == 10 && Fax != null ? "(" + Fax.Substring(0, 3) + ")" + " " + Fax.Substring(3, 3) + "-" + Fax.Substring(6, Fax.Length - 6) : Fax;
                SystemName = lstSystemSettings.SystemName;
                NextOrderNumber = lstSystemSettings.NextOrderNumber;
                CreatedBy = lstSystemSettings.CreatedBy;
                CreatedDate = lstSystemSettings.CreatedDate;
                UpdatedBy = lstSystemSettings.UpdatedBy;
                UpdatedDate = lstSystemSettings.UpdatedDate;
            }

        }

        public void UpdateCompanyInfo()
        {
            if (!string.IsNullOrEmpty(CompanyName))
            {
                string FormattedSpclChPhone = string.Empty;
                String numbersOnlyPhone = string.Empty;
                string FormattedSpclChFax = string.Empty;
                String numbersOnlyFax = string.Empty;
                if (Phone.Length > 0 && Phone != null)
                {
                    FormattedSpclChPhone = fnRemoveSplChars(Phone);
                    numbersOnlyPhone = Regex.Replace(FormattedSpclChPhone, @"[^\d]", String.Empty);
                }

                if (Fax.Length > 0 & Fax != null)
                {
                    FormattedSpclChFax = fnRemoveSplChars(Fax);
                    numbersOnlyFax = Regex.Replace(FormattedSpclChFax, @"[^\d]", String.Empty);
                }
                UserApplicationConstantsProp userapp = new UserApplicationConstantsProp();
                userapp.CompanyName = CompanyName;
                userapp.AddressLine1 = AddressLine1;
                userapp.AddressLine2 = AddressLine2;
                userapp.CitySep = City;
                userapp.State = State;
                userapp.Zip = Zip;
                userapp.Phone = numbersOnlyPhone;
                userapp.FaxNumber = numbersOnlyFax;
                userapp.SystemName = SystemName;
                userapp.NextOrderNumber = NextOrderNumber;
                userapp.UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                userapp.UpdatedDate = DateTime.Now;
                userapp.UserCode = Application.Current.Properties["LoggedInUserName"].ToString();

                bool isSuccessful = _serviceInstance.UpdateCompanyInfo(userapp);

                if (isSuccessful)
                {
                    MessageBox.Show(Resources.msgUpdatedSuccessfully);
                    AcceptChanges();
                }
                else
                { MessageBox.Show(Resources.ErrorToUpdate); }
            }
            else
            { MessageBox.Show(Resources.ReqCompanyName); }
        }

        public static string fnRemoveSplChars(string strMyString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in strMyString)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public void Save_Click(object obj)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            bool isSucceed = false;

            var lstSystemSettings = _serviceInstance.GetSystemSettings();
            if (lstSystemSettings.Count() > 0)
            {
                prevSettings = null;
                prevSettings = lstSystemSettings.ToList();
            }

            for (int i = 0; i < ListSettings.Count; i++)
            {
                if (ListSettings[i].ValueDescription != prevSettings[i].ValueDescription)
                {
                    AdminUserProp setting = new AdminUserProp();
                    setting.ValueDescription = ListSettings[i].ValueDescription;
                    setting.ValueKey = ListSettings[i].ValueKey;
                    _serviceInstance.UpdateSystemSettings(setting);
                }
            }
            isSucceed = true;

            if (isSucceed)
                MessageBox.Show(Resources.msgUpdatedSuccessfully);
            Mouse.OverrideCursor = Cursors.Arrow;

        }

        public static T DeepCopy<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }

    }
}
