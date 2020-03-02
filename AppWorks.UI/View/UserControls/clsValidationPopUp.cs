using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using AppWorks.Model;
//using AppWorks.BAL;
using AppWorks.UI.ViewModel;


namespace AppWorks.UI.View.UserControls
{
    public class clsValidationPopUp : ViewModelBase
    {
        string errMsg = string.Empty;
        public string ErrMsgReceipientMnemonic
        {
            get { return errMsg; }
            set
            {
                errMsg = value;
                NotifyPropertyChanged("ErrMsgReceipientMnemonic");
            }
        }

        string errMsgFaxNumber = string.Empty;
        public string ErrMsgFaxNumber
        {
            get { return errMsgFaxNumber; }
            set
            {
                errMsgFaxNumber = value;
                NotifyPropertyChanged("ErrMsgFaxNumber");
            }
        }


        string errMsgEmail = string.Empty;
        public string ErrMsgEmail
        {
            get { return errMsgEmail; }
            set
            {

                errMsgEmail = value;
                NotifyPropertyChanged("ErrMsgEmail");


            }
        }

        private bool emailPopupVisible;
        public bool EmailPopupVisible
        {
            get { return emailPopupVisible; }
            set
            {
                if ((string.IsNullOrEmpty(ErrMsgEmail) && value == false) || (value == true && !string.IsNullOrEmpty(ErrMsgEmail)))
                {
                    emailPopupVisible = value;
                    NotifyPropertyChanged("EmailPopupVisible");
                }
            }
        }

        string errMsgPager = string.Empty;
        public string ErrMsgPager
        {
            get { return errMsgPager; }
            set
            {

                errMsgPager = value;
                NotifyPropertyChanged("ErrMsgPager");
            }
        }

        private bool pagerPopupVisible;
        public bool PagerPopupVisible
        {
            get { return pagerPopupVisible; }
            set
            {
                if ((string.IsNullOrEmpty(ErrMsgPager) && value == false) || (value == true && !string.IsNullOrEmpty(ErrMsgPager)))
                {
                    pagerPopupVisible = value; NotifyPropertyChanged("PagerPopupVisible");
                }
            }
        }

        string errMsgZipCode = string.Empty;
        public string ErrMsgZipCode
        {
            get { return errMsgZipCode; }
            set
            {
                errMsgZipCode = value;
                NotifyPropertyChanged("ErrMsgZipCode");
            }
        }

        string errMsgOfficePhone = string.Empty;
        public string ErrMsgOfficePhone
        {
            get { return errMsgOfficePhone; }
            set
            {
                errMsgOfficePhone = value;
                NotifyPropertyChanged("ErrMsgOfficePhone");
            }
        }

        string errMsgCellPhone = string.Empty;
        public string ErrMsgCellPhone
        {
            get { return errMsgCellPhone; }
            set
            {
                errMsgCellPhone = value;
                NotifyPropertyChanged("ErrMsgCellPhone");
            }
        }

        string _PasswordValid = "Collapsed";
        public string PasswordValid
        {
            get { return _PasswordValid; }
            set
            {
                _PasswordValid = value;
                NotifyPropertyChanged("PasswordValid");
            }
        }

        string _EmailValid = "Collapsed";
        public string EmailValid
        {
            get { return _EmailValid; }
            set
            {
                _EmailValid = value;
                NotifyPropertyChanged("EmailValid");
            }
        }

        string errMsgPassword = string.Empty;
        public string ErrMsgPassword
        {
            get { return errMsgPassword; }
            set
            {
                errMsgPassword = value;
                NotifyPropertyChanged("ErrMsgPassword");
            }
        }

        string errMsgDistributionList = string.Empty;
        public string ErrMsgDistributionList
        {
            get { return errMsgDistributionList; }
            set
            {
                errMsgDistributionList = value;
                NotifyPropertyChanged("ErrMsgDistributionList");
            }
        }


        string errMsgFaxStartEnd = string.Empty;
        public string ErrMsgFaxStartEnd
        {
            get { return errMsgFaxStartEnd; }
            set
            {
                errMsgFaxStartEnd = value;
                NotifyPropertyChanged("ErrMsgFaxStartEnd");
            }
        }

        string _FaxnumberVisible = "Collapsed";
        public string FaxnumberVisible
        {
            get { return _FaxnumberVisible; }
            set
            {
                _FaxnumberVisible = value;
                NotifyPropertyChanged("FaxnumberVisible");
            }
        }

        string _SendToLprPrinterVisisble = "Collapsed";
        public string SendToLprPrinterVisisble
        {
            get { return _SendToLprPrinterVisisble; }
            set
            {
                _SendToLprPrinterVisisble = value;
                NotifyPropertyChanged("SendToLprPrinterVisisble");
            }
        }

        string _ErrMsgLPRQueueName = string.Empty;
        public string ErrMsgLPRQueueName
        {
            get { return _ErrMsgLPRQueueName; }
            set
            {
                _ErrMsgLPRQueueName = value;
                NotifyPropertyChanged("ErrMsgLPRQueueName");
            }
        }

        string _ErrMsgLPRHost = string.Empty;
        public string ErrMsgLPRHost
        {
            get { return _ErrMsgLPRHost; }
            set
            {
                _ErrMsgLPRHost = value;
                NotifyPropertyChanged("ErrMsgLPRHost");
            }
        }

        string _WindowsFolderVisible = "Collapsed";
        public string WindowsFolderVisible
        {
            get { return _WindowsFolderVisible; }
            set
            {
                _WindowsFolderVisible = value;
                NotifyPropertyChanged("WindowsFolderVisible");
            }
        }

        string _ErrMsgWindowsFolder = string.Empty;
        public string ErrMsgWindowsFolder
        {
            get { return _ErrMsgWindowsFolder; }
            set
            {
                _ErrMsgWindowsFolder = value;
                NotifyPropertyChanged("ErrMsgWindowsFolder");
            }
        }

        string _ErrMsgInternetPort = string.Empty;
        public string ErrMsgInternetPort
        {
            get { return _ErrMsgInternetPort; }
            set
            {
                _ErrMsgInternetPort = value;
                NotifyPropertyChanged("ErrMsgInternetPort");
            }
        }

        string _ErrMsgInternetHost = string.Empty;
        public string ErrMsgInternetHost
        {
            get { return _ErrMsgInternetHost; }
            set
            {
                _ErrMsgInternetHost = value;
                NotifyPropertyChanged("ErrMsgInternetHost");
            }
        }



        string _ErrMsgSocketPrinterIP = string.Empty;
        public string ErrMsgSocketPrinterIP
        {
            get { return _ErrMsgSocketPrinterIP; }
            set
            {
                _ErrMsgSocketPrinterIP = value;
                NotifyPropertyChanged("ErrMsgSocketPrinterIP");
            }
        }


        string _ErrMsgSocketPrinterPort = string.Empty;
        public string ErrMsgSocketPrinterPort
        {
            get { return _ErrMsgSocketPrinterPort; }
            set
            {
                _ErrMsgSocketPrinterPort = value;
                NotifyPropertyChanged("ErrMsgSocketPrinterPort");
            }
        }

        string _ErrMsgFilenameSpecified = string.Empty;
        public string ErrMsgFilenameSpecified
        {
            get { return _ErrMsgFilenameSpecified; }
            set
            {
                _ErrMsgFilenameSpecified = value;
                NotifyPropertyChanged("ErrMsgFilenameSpecified");
            }
        }

        string _ErrMsgSpecifiedFileExtension = string.Empty;
        public string ErrMsgSpecifiedFileExtension
        {
            get { return _ErrMsgSpecifiedFileExtension; }
            set
            {
                _ErrMsgSpecifiedFileExtension = value;
                NotifyPropertyChanged("ErrMsgSpecifiedFileExtension");
            }
        }

        string _ErrFaxRedirect = string.Empty;
        public string ErrFaxRedirect
        {
            get { return _ErrFaxRedirect; }
            set
            {
                _ErrFaxRedirect = value;
                NotifyPropertyChanged("ErrFaxRedirect");
            }
        }

        string _ErrMsgFaxCoverPageFileName;
        public string ErrMsgFaxCoverPageFileName
        {
            get
            {
                return this._ErrMsgFaxCoverPageFileName;
            }
            set
            {
                _ErrMsgFaxCoverPageFileName = value;
                NotifyPropertyChanged("ErrMsgFaxCoverPageFileName");
            }
        }
        string _ErrMsgSocketPrinterGrid;
        public string ErrMsgSocketPrinterGrid
        {
            get
            {
                return this._ErrMsgSocketPrinterGrid;
            }
            set
            {
                _ErrMsgSocketPrinterGrid = value;
                NotifyPropertyChanged("ErrMsgSocketPrinterGrid");
            }
        }

        string _ErrMsgDomain;
        public string ErrMsgDomain
        {
            get
            {
                return this._ErrMsgDomain;
            }
            set
            {
                _ErrMsgDomain = value;
                NotifyPropertyChanged("ErrMsgDomain");
            }
        }

        string _ErrMsgUploadPrefrenceGrd;
        public string ErrMsgUploadPrefrenceGrd
        {
            get
            {
                return this._ErrMsgUploadPrefrenceGrd;
            }
            set
            {
                _ErrMsgUploadPrefrenceGrd = value;
                NotifyPropertyChanged("ErrMsgUploadPrefrenceGrd");
            }
        }

        string _ErrMsgResultDistributionList;
        public string ErrMsgResultDistributionList
        {
            get
            {
                return this._ErrMsgResultDistributionList;
            }
            set
            {
                _ErrMsgResultDistributionList = value;
                NotifyPropertyChanged("ErrMsgResultDistributionList");
            }
        }

        string _ErrMsgSpecificTimePrintingList;
        public string ErrMsgSpecificTimePrintingList
        {
            get
            {
                return this._ErrMsgSpecificTimePrintingList;
            }
            set
            {
                _ErrMsgSpecificTimePrintingList = value;
                NotifyPropertyChanged("ErrMsgSpecificTimePrintingList");
            }
        }

        string _ErrMsgMaxGroup;
        public string ErrMsgMaxGroup
        {
            get
            {
                return this._ErrMsgMaxGroup;
            }
            set
            {
                _ErrMsgMaxGroup = value;
                NotifyPropertyChanged("ErrMsgMaxGroup");
            }
        }

        string _ErrMsgServerName;
        public string ErrMsgServerName
        {
            get
            {
                return this._ErrMsgServerName;
            }
            set
            {
                _ErrMsgServerName = value;
                NotifyPropertyChanged("ErrMsgServerName");
            }
        }

        string _ErrOutsideLineStringMagic = string.Empty;
        public string ErrOutsideLineStringMagic
        {
            get { return _ErrOutsideLineStringMagic; }
            set
            {
                _ErrOutsideLineStringMagic = value;
                NotifyPropertyChanged("ErrOutsideLineStringMagic");
            }
        }

        string _ErrOutsideLineString = string.Empty;
        public string ErrOutsideLineString
        {
            get { return _ErrOutsideLineString; }
            set
            {
                _ErrOutsideLineString = value;
                NotifyPropertyChanged("ErrOutsideLineString");
            }
        }

        string _ErrSMTPServer = string.Empty;
        public string ErrSMTPServer
        {
            get { return _ErrSMTPServer; }
            set
            {
                _ErrSMTPServer = value;
                NotifyPropertyChanged("ErrSMTPServer");
            }
        }

        string _ErrMsgServerTCPPort = string.Empty;
        public string ErrMsgServerTCPPort
        {
            get { return _ErrMsgServerTCPPort; }
            set
            {
                _ErrMsgServerTCPPort = value;
                NotifyPropertyChanged("ErrMsgServerTCPPort");
            }
        }

        string _ErrMsgWebsiteURL = string.Empty;
        public string ErrMsgWebsiteURL
        {
            get { return _ErrMsgWebsiteURL; }
            set
            {
                _ErrMsgWebsiteURL = value;
                NotifyPropertyChanged("ErrMsgWebsiteURL");
            }
        }

        string _ErrMsgAppDataFolder = string.Empty;
        public string ErrMsgAppDataFolder
        {
            get { return _ErrMsgAppDataFolder; }
            set
            {
                _ErrMsgAppDataFolder = value;
                NotifyPropertyChanged("ErrMsgAppDataFolder");
            }
        }
        string _ErrMsgMediaFolder = string.Empty;
        public string ErrMsgMediaFolder
        {
            get { return _ErrMsgMediaFolder; }
            set
            {
                _ErrMsgMediaFolder = value;
                NotifyPropertyChanged("ErrMsgMediaFolder");
            }
        }


        string _ErrMsgInternetPath = string.Empty;
        public string ErrMsgInternetPath
        {
            get { return _ErrMsgInternetPath; }
            set
            {
                _ErrMsgInternetPath = value;
                NotifyPropertyChanged("ErrMsgInternetPath");
            }
        }


        string errMsgUserCode = string.Empty;
        public string ErrMsgUserCode
        {
            get { return errMsgUserCode; }
            set
            {
                errMsgUserCode = value;
                NotifyPropertyChanged("ErrMsgUserCode");
            }
        }

        string errMsgUserPin = string.Empty;
        public string ErrMsgUserPin
        {
            get { return errMsgUserPin; }
            set
            {
                errMsgUserPin = value;
                NotifyPropertyChanged("ErrMsgUserPin");
            }
        }
    }
}
