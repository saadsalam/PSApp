using AppWorks.UI.Common;
using AppWorks.UI.Properties;
using AppWorksService.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AppWorks.UI.ViewModel.CustomerAdmin
{
    public class AddressViewModel : ViewModelBase, IEquatable<AddressViewModel>
    {
        #region Properties

        private List<LocationList> _listBillingAddress;
        public List<LocationList> ListBillingAddress
        {
            get
            {
                return _listBillingAddress;
            }
            private set
            {
                this._listBillingAddress = value;
                NotifyPropertyChanged("ListBillingAddress");
            }
        }

        private int? _locationId;
        public int? LocationId
        {
            get { return _locationId; }
            set
            {
                if (value != null)
                    _locationId = value;
                NotifyPropertyChanged("LocationId");
            }
        }
        private string _locationName;
        public string LocationName
        {
            get { return _locationName; }
            set
            {
                if (value != null)
                    _locationName = value;
                NotifyPropertyChanged("LocationName");
            }
        }
        private string _addressLine1;
        public string AddressLine1
        {
            get { return _addressLine1; }
            set
            {
                if (value != null)
                    _addressLine1 = value;
                NotifyPropertyChanged("AddressLine1");
            }
        }
        private string _addressLine2;
        public string AddressLine2
        {
            get { return _addressLine2; }
            set
            {
                if (value != null)
                    _addressLine2 = value;
                NotifyPropertyChanged("AddressLine2");
            }
        }
        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                if (value != null)
                    _city = value;
                NotifyPropertyChanged("City");
            }
        }
        private string _state;
        public string State
        {
            get { return _state; }
            set
            {
                if (value != null)
                    _state = value;
                NotifyPropertyChanged("State");
            }
        }
        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                if (value != null)
                    _country = value;
                NotifyPropertyChanged("Country");
            }
        }
        private string _zip;
        public string Zip
        {
            get { return _zip; }
            set
            {
                if (value != null)
                    _zip = value;
                NotifyPropertyChanged("Zip");
            }
        }
        private string _mainPhone;
        public string MainPhone
        {
            get
            {
                if (_mainPhone == null)
                    return string.Empty;

                switch (_mainPhone.Length)
                {
                    case 6:
                        return Regex.Replace(_mainPhone, @"(\d{3})(\d{3})", "$1-$2");
                    case 10:
                        return Regex.Replace(_mainPhone, @"(\d{3})(\d{3})(\d{4})", "( $1) $2-$3");
                    case 11:
                        return Regex.Replace(_mainPhone.Replace("-", ""), @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");

                    default:
                        return _mainPhone;
                }
            }
            set
            {
                if (value != null)
                    _mainPhone = value;
                NotifyPropertyChanged("MainPhone");
            }
        }
        private string _faxNumber;
        public string FaxNumber
        {
            get { return _faxNumber; }
            set
            {
                if (value != null)
                    _faxNumber = value;
                NotifyPropertyChanged("FaxNumber");
            }
        }
        private string _primaryContactFirstName;
        public string PrimaryContactFirstName
        {
            get { return _primaryContactFirstName; }
            set
            {
                if (value != null)
                    _primaryContactFirstName = value;
                NotifyPropertyChanged("PrimaryContactFirstName");
            }
        }
        private string _primaryContactPhone;
        public string PrimaryContactPhone
        {
            get
            {
                if (_primaryContactPhone == null)
                    return string.Empty;

                switch (_primaryContactPhone.Length)
                {
                    case 6:
                        return Regex.Replace(_primaryContactPhone, @"(\d{3})(\d{3})", "$1-$2");
                    case 10:
                        return Regex.Replace(_primaryContactPhone, @"(\d{3})(\d{3})(\d{4})", "( $1) $2-$3");
                    case 11:
                        return Regex.Replace(_primaryContactPhone.Replace("-", ""), @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");

                    default:
                        return _primaryContactPhone;
                }
            }
            set
            {
                if (value != null)
                    _primaryContactPhone = value;
                NotifyPropertyChanged("PrimaryContactPhone");
            }
        }
        private string _primaryContactEmail;
        public string PrimaryContactEmail
        {
            get { return _primaryContactEmail; }
            set
            {
                if (value != null)
                    _primaryContactEmail = value;
                NotifyPropertyChanged("PrimaryContactEmail");
            }
        }

        private string _alternateContactFirstName;
        public string AlternateContactFirstName
        {
            get { return _alternateContactFirstName; }
            set
            {
                if (value != null)
                    _alternateContactFirstName = value;
                NotifyPropertyChanged("AlternateContactFirstName");
            }
        }
        private string _alternateContactPhone;
        public string AlternateContactPhone
        {
            get
            {
                if (_alternateContactPhone == null)
                    return string.Empty;

                switch (_alternateContactPhone.Length)
                {
                    case 6:
                        return Regex.Replace(_alternateContactPhone, @"(\d{3})(\d{3})", "$1-$2");
                    case 10:
                        return Regex.Replace(_alternateContactPhone, @"(\d{3})(\d{3})(\d{4})", "( $1) $2-$3");
                    case 11:
                        return Regex.Replace(_alternateContactPhone.Replace("-", ""), @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");

                    default:
                        return _alternateContactPhone;
                }
            }
            set
            {
                if (value != null)
                    _alternateContactPhone = value;
                NotifyPropertyChanged("AlternateContactPhone");
            }
        }
        private string _alternateContactEmail;
        public string AlternateContactEmail
        {
            get { return _alternateContactEmail; }
            set
            {
                if (value != null)
                    _alternateContactEmail = value;
                NotifyPropertyChanged("AlternateContactEmail");
            }
        }

        private string _otherPhone1Description;
        public string OtherPhone1Description
        {
            get { return _otherPhone1Description; }
            set
            {
                if (value != null)
                    _otherPhone1Description = value;
                NotifyPropertyChanged("OtherPhone1Description");
            }
        }
        private string _otherPhone1;
        public string OtherPhone1
        {
            get
            {
                if (_otherPhone1 == null)
                    return string.Empty;

                switch (_otherPhone1.Length)
                {
                    case 6:
                        return Regex.Replace(_otherPhone1, @"(\d{3})(\d{3})", "$1-$2");
                    case 10:
                        return Regex.Replace(_otherPhone1, @"(\d{3})(\d{3})(\d{4})", "( $1) $2-$3");
                    case 11:
                        return Regex.Replace(_otherPhone1.Replace("-", ""), @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");

                    default:
                        return _otherPhone1;
                }
            }
            set
            {
                if (value != null)
                    _otherPhone1 = value;
                NotifyPropertyChanged("OtherPhone1");
            }
        }
        private string _otherPhone2Description;
        public string OtherPhone2Description
        {
            get { return _otherPhone2Description; }
            set
            {
                if (value != null)
                    _otherPhone2Description = value;
                NotifyPropertyChanged("OtherPhone2Description");
            }
        }
        private string _otherPhone2;
        public string OtherPhone2
        {
            get
            {
                if (_otherPhone2 == null)
                    return string.Empty;

                switch (_otherPhone2.Length)
                {
                    case 6:
                        return Regex.Replace(_otherPhone2, @"(\d{3})(\d{3})", "$1-$2");
                    case 10:
                        return Regex.Replace(_otherPhone2, @"(\d{3})(\d{3})(\d{4})", "( $1) $2-$3");
                    case 11:
                        return Regex.Replace(_otherPhone2.Replace("-", ""), @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");

                    default:
                        return _otherPhone2;
                }
            }
            set
            {
                if (value != null)
                    _otherPhone2 = value;
                NotifyPropertyChanged("OtherPhone2");
            }
        }

        #endregion Properties

        #region Constructors

        public AddressViewModel()
        {
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
            OtherPhone1Description = string.Empty;
            OtherPhone1 = string.Empty;
            OtherPhone2Description = string.Empty;
            OtherPhone2 = string.Empty;
        }

        #endregion Constructors

        #region Methods

        public void LoadAddress(int? addressID)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var qry = _serviceInstance.GetBillingStreetAddress((int)addressID).Select(d => new LocationList
                {
                    LocationID = d.LocationID,
                    AddressLine1 = d.AddressLine1,
                    AddressLine2 = d.AddressLine2,
                    City = d.City,
                    State = d.State,
                    Zip = d.Zip,
                    Country = d.Country,
                    MainPhone = d.MainPhone,
                    FaxNumber = d.FaxNumber,
                    PrimaryContactFirstName = d.PrimaryContactFirstName,
                    PrimaryContactPhone = d.PrimaryContactPhone,
                    PrimaryContactEmail = d.PrimaryContactEmail,
                    OtherPhone1Description = d.OtherPhone1Description,
                    OtherPhone1 = d.OtherPhone1,
                    OtherPhone2Description = d.OtherPhone2Description,
                    OtherPhone2 = d.OtherPhone2,
                    AlternateContactFirstName = d.AlternateContactFirstName,
                    AlternateContactPhone = d.AlternateContactPhone,
                    AlternateContactEmail = d.AlternateContactEmail
                });

                foreach (var item in qry)
                {
                    LocationId = item.LocationID;
                    AddressLine1 = item.AddressLine1;
                    AddressLine2 = item.AddressLine2;
                    City = item.City;
                    State = item.State;
                    Zip = item.Zip;
                    Country = item.Country;
                    MainPhone = item.MainPhone;
                    FaxNumber = item.FaxNumber;
                    PrimaryContactFirstName = item.PrimaryContactFirstName;
                    PrimaryContactPhone = item.PrimaryContactPhone;
                    PrimaryContactEmail = item.PrimaryContactEmail;
                    OtherPhone1Description = item.OtherPhone1Description;
                    OtherPhone1 = item.OtherPhone1;
                    OtherPhone2Description = item.OtherPhone2Description;
                    OtherPhone2 = item.OtherPhone2;
                    AlternateContactFirstName = item.AlternateContactFirstName;
                    AlternateContactPhone = item.AlternateContactPhone;
                    AlternateContactEmail = item.AlternateContactEmail;
                }

                ListBillingAddress = new List<LocationList>(qry);

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

        public void CopyFrom(AddressViewModel source)
        {
            AddressLine1 = source.AddressLine1;
            AddressLine2 = source.AddressLine2;
            AlternateContactEmail = source.AlternateContactEmail;
            AlternateContactFirstName = source.AlternateContactFirstName;
            AlternateContactPhone = source.AlternateContactPhone;
            City = source.City;
            Country = source.Country;
            FaxNumber = source.FaxNumber;
            ListBillingAddress = source.ListBillingAddress;
            MainPhone = source.MainPhone;
            OtherPhone1 = source.OtherPhone1;
            OtherPhone1Description = source.OtherPhone1Description;
            OtherPhone2 = source.OtherPhone2;
            OtherPhone2Description = source.OtherPhone2Description;
            PrimaryContactEmail = source.PrimaryContactEmail;
            PrimaryContactFirstName = source.PrimaryContactFirstName;
            PrimaryContactPhone = source.State;
            Zip = source.Zip;
        }

        public bool Equals(AddressViewModel other)
        {
            if (other == null)
                return false;

            return AddressLine1 == other.AddressLine1
                && AddressLine2 == other.AddressLine2
                && AlternateContactEmail == other.AlternateContactEmail
                && AlternateContactFirstName == other.AlternateContactFirstName
                && AlternateContactPhone == other.AlternateContactPhone
                && City == other.City
                && Country == other.Country
                && FaxNumber == other.FaxNumber
                && ListBillingAddress == other.ListBillingAddress
                && MainPhone == other.MainPhone
                && OtherPhone1 == other.OtherPhone1
                && OtherPhone1Description == other.OtherPhone1Description
                && OtherPhone2 == other.OtherPhone2
                && OtherPhone2Description == other.OtherPhone2Description
                && PrimaryContactEmail == other.PrimaryContactEmail
                && PrimaryContactFirstName == other.PrimaryContactFirstName
                && PrimaryContactPhone == other.State
                && Zip == other.Zip;
        }

        #endregion Methods
    }
}
