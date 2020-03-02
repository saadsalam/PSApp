using AppWorks.UI.Common;
using AppWorks.UI.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using AppWorksService.Properties; 

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class StorageVehicleOutgateViewModel : ViewModelBase
    {
        string userCode = Application.Current.Properties["LoggedInUserName"].ToString();
        #region Properties

        private string _helpMessage;
        public string HelpMessage
        {
            get { return _helpMessage; }
            private set
            {
                _helpMessage = value;
                NotifyPropertyChanged("HelpMessage");
            }
        }


        private string _vin;
        public string VIN
        {
            get { return _vin; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {

                    if (value.Length < 17)
                    {
                        value = string.Empty;
                    }
                    else if (value.Length > 17)
                    {
                        if ((value.Length == 18) && (value.Substring(0, 1).Equals("I")))
                        {
                            value = value.Remove(0, 1);
                        }
                        else
                        {
                            value = value.Substring(0, 17);
                        }
                    }
                }

                _vin = value;
                SetProperHelpMessage();
                NotifyPropertyChanged("VIN");
            }
        }

        private string _gatePass;
        public string GatePass
        {
            get { return _gatePass; }
            set
            {
                _gatePass = value;
                SetProperHelpMessage();
                NotifyPropertyChanged("GatePass");
            }
        }

        /// <summary>
        /// Property for Date Out
        /// </summary>
        private DateTime dtDateOut = DateTime.Now;
        public DateTime DtDateOut
        {
            get
            {
                return dtDateOut;
            }
            set
            {
                this.dtDateOut = value;
                NotifyPropertyChanged("DtDateOut");
            }
        }
        /// <summary>
        /// This List is used to bind multiple request in grid
        /// </summary>
        public List<StorageVehicleOutgateProp> lstStorageVehicleOutgateProp = new List<StorageVehicleOutgateProp>();
        private Brush _background;
        public Brush Background
        {
            get
            {
                return _background;
            }

            set
            {
                _background = value;
                NotifyPropertyChanged("Background");
            }
        }
        #endregion Properties

        #region Commands

        private ICommand _processCommand;
        public ICommand ProcessCommand
        {
            get
            {
                return _processCommand ??
                    (_processCommand = new AppWorxCommand(ProcessCommand_Executed, ProcessCommand_CanExecute));
            }
        }
        private bool ProcessCommand_CanExecute(object obj)
        {
            return !string.IsNullOrEmpty(VIN)
                && !string.IsNullOrEmpty(GatePass)
                && VIN.EndsWith(GatePass);
        }


        void ProcessCommand_Executed(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                StorageVehicleOutgateProp objData = new StorageVehicleOutgateProp();
                StorageVehicleOutgateProp objStorageVehicleOutgateProp = new StorageVehicleOutgateProp();
                objStorageVehicleOutgateProp.Vin = VIN;
                objStorageVehicleOutgateProp.DateOut = DtDateOut;
                objStorageVehicleOutgateProp.Note = string.Empty;
                objStorageVehicleOutgateProp.User = userCode;
                objData = _serviceInstance.UpdateStorageVehicleOutgateData(objStorageVehicleOutgateProp);
                {
                    if (objData != null)
                    {
                        if (objData.ReturnCode == 0)
                        {
                            MessageBox.Show(Resources.msgSecurity, Resources.msgTitleMessageBoxSecurity);

                        }
                        else
                        {
                            MessageBox.Show(Resources.msgErrorSecurity + objData.ReturnMessage, Resources.msgTitleMessageBoxErrorSecurity);
                        }
                        HelpMessage = Resources.msgSecurityVEHICLEScan;
                        Background = new SolidColorBrush(Colors.Blue);
                        VIN = null;
                        GatePass = null;
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

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                    (_cancelCommand = new AppWorxCommand(CancelCommand_Executed));
            }
        }
        private void CancelCommand_Executed(object obj)
        {
            VIN = string.Empty;
            GatePass = string.Empty;
        }


        #endregion Commands

        #region Constructors

        public StorageVehicleOutgateViewModel()
        {
            SetProperHelpMessage();
        }

        #endregion Constructors

        #region Methods

        private void SetProperHelpMessage()
        {
            if (string.IsNullOrEmpty(VIN))
            {
                HelpMessage = Resources.msgSecurityVEHICLEScan;
                Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                if (!string.IsNullOrEmpty(GatePass) && GatePass.Length != 8)
                {
                    HelpMessage = Resources.msgSecurityGatePass;
                    Background = new SolidColorBrush(Colors.Orange);
                    if (GatePass.Length != 8)
                    {
                        if (GatePass.Length > 8)
                        {
                            HelpMessage = Resources.msgSecurityLongGatePass;
                            Background = new SolidColorBrush(Colors.IndianRed);
                        }
                        else
                        {
                            HelpMessage = Resources.msgSecurityShortGatePass;
                            Background = new SolidColorBrush(Colors.IndianRed);
                        }
                    }

                }
                else if (string.IsNullOrEmpty(GatePass))
                {
                    HelpMessage = Resources.msgSecurityGatePass;
                    Background = new SolidColorBrush(Colors.Orange);
                }
                else if (!VIN.EndsWith(GatePass))
                {
                    HelpMessage = Resources.msgSecurityNoMatch;
                    Background = new SolidColorBrush(Colors.IndianRed);
                }
                else
                {
                    HelpMessage = Resources.msgSecurityMatch;
                    Background = new SolidColorBrush(Colors.Green);
                }
            }
        }

        #endregion Methods
    }
}
