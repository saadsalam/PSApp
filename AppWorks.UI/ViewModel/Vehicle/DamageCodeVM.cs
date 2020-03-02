using System;
using System.Collections.Generic;
using System.Linq;
using AppWorksService.Properties;
using System.Reflection;
using System.Globalization;
using AppWorks.UI.Common;
using AppWorks.UI.AppService;
using System.Windows;
using AppWorks.UI.Properties;

namespace AppWorks.UI.ViewModel.Vehicle
{
    /// <summary>
    /// A View Model Call for Damage Code
    /// </summary>
    public class DamageCodeVM : ViewModelBase
    {

        public DamageCodeVM(string VINNo, int VehicleIdNo)
        {
            Vin = VINNo;
            VehicleId = VehicleIdNo;
            GetInspectionList();
            InspectionDate = DateTime.Now;
        }

        #region 'DamageCode'

        private string vin;
        public string Vin
        {
            get { return vin; }
            set
            {
                vin = value;
                NotifyPropertyChanged("Vin");
            }
        }

        private int vehicleId;
        public int VehicleId
        {
            get { return vehicleId; }
            set
            {
                vehicleId = value;
                NotifyPropertyChanged("VehicleId");
            }
        }

        private Nullable<DateTime> inspectionDate;
        public Nullable<DateTime> InspectionDate
        {
            get { return inspectionDate; }
            set
            {
                this.inspectionDate = value;
                NotifyPropertyChanged("InspectionDate");
            }
        }

        private string damageCode1;
        public string DamageCode1
        {
            get { return damageCode1; }
            set
            {
                damageCode1 = value;
                NotifyPropertyChanged("DamageCode1");
            }
        }

        private string damageCode2;
        public string DamageCode2
        {
            get { return damageCode2; }
            set
            {
                damageCode2 = value;
                NotifyPropertyChanged("DamageCode2");
            }
        }

        private string damageCode3;
        public string DamageCode3
        {
            get { return damageCode3; }
            set
            {
                damageCode3 = value;
                NotifyPropertyChanged("DamageCode3");
            }
        }

        private string damageCode4;
        public string DamageCode4
        {
            get { return damageCode4; }
            set
            {
                damageCode4 = value;
                NotifyPropertyChanged("DamageCode4");
            }
        }

        private string damageCode5;
        public string DamageCode5
        {
            get { return damageCode5; }
            set
            {
                damageCode5 = value;
                NotifyPropertyChanged("DamageCode5");
            }
        }

        private string damageCode6;
        public string DamageCode6
        {
            get { return damageCode6; }
            set
            {
                damageCode6 = value;
                NotifyPropertyChanged("DamageCode6");
            }
        }

        private string damageCode7;
        public string DamageCode7
        {
            get { return damageCode7; }
            set
            {
                damageCode7 = value;
                NotifyPropertyChanged("DamageCode7");
            }
        }

        private string damageCode8;
        public string DamageCode8
        {
            get { return damageCode8; }
            set
            {
                damageCode8 = value;
                NotifyPropertyChanged("DamageCode8");
            }
        }

        private string damageCode9;
        public string DamageCode9
        {
            get { return damageCode9; }
            set
            {
                damageCode9 = value;
                NotifyPropertyChanged("DamageCode9");
            }
        }

        private string damageCode10;
        public string DamageCode10
        {
            get { return damageCode10; }
            set
            {
                damageCode10 = value;
                NotifyPropertyChanged("DamageCode10");
            }
        }

        /// <summary>
        /// Command For Load Inspection Type.
        /// </summary>
        private string selectedInspectionType;
        public string SelectedInspectionType
        {
            get { return selectedInspectionType; }
            set
            {
                selectedInspectionType = value;
                NotifyPropertyChanged("SelectedInspectionType");
            }
        }

        private int selectedInspectionID;
        public int SelectedInspectionID
        {
            get { return selectedInspectionID; }
            set
            {
                selectedInspectionID = value;
                NotifyPropertyChanged("SelectedInspectionID");
            }
        }

        /// <summary>
        /// Command For Load Inspection Type.
        /// </summary>
        private List<string> inspectionType;
        public List<string> InspectionType
        {
            get { return inspectionType; }
            private set
            {

                inspectionType = value;
                NotifyPropertyChanged("InspectionType");

            }
        }


        /// <summary>
        /// Command For Close the Window.
        /// </summary>
        private AppWorxCommand btnClose;
        public AppWorxCommand BtnClose
        {
            get
            {
                if (btnClose == null)
                    btnClose = new AppWorxCommand(this.Close);
                return btnClose;
            }

        }

        /// <summary>
        /// Command For Save Information.
        /// </summary>
        private AppWorxCommand btnSave;
        public AppWorxCommand BtnSave
        {
            get
            {
                if (btnSave == null)
                    btnSave = new AppWorxCommand(this.Save);
                return btnSave;
            }
        }

        #endregion DamageCoed

        /// <summary>
        /// Method To Close The Window.
        /// </summary>
        public void Close(object obj)
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }

        }

        /// <summary>
        /// Method To Save The Data.
        /// </summary>
        public async void Save(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (Vin.Length < 1)
                {
                    MessageBox.Show(Resources.msgVINReq);
                }
                else if (InspectionDate == null)
                {
                    MessageBox.Show(Resources.msgInspectionDateReq);
                }
                else if (string.IsNullOrEmpty(damageCode1) && string.IsNullOrEmpty(damageCode2) && string.IsNullOrEmpty(damageCode3) && string.IsNullOrEmpty(damageCode4) && string.IsNullOrEmpty(damageCode5) && string.IsNullOrEmpty(damageCode6) && string.IsNullOrEmpty(damageCode7) && string.IsNullOrEmpty(damageCode8) && string.IsNullOrEmpty(damageCode9) && string.IsNullOrEmpty(damageCode10))
                {
                    MessageBox.Show(Resources.msgDamageCodeReq);
                }
                else if (string.IsNullOrEmpty(SelectedInspectionType) || selectedInspectionType == "Please Select")
                {
                    MessageBox.Show(Resources.msgInspectionTypeReq);
                }
                else
                {
                    if (SelectedInspectionType.ToLower(CultureInfo.CurrentCulture).Equals(Resources.InspectionType1.ToLower(CultureInfo.CurrentCulture)))
                    {
                        SelectedInspectionID = 0;
                    }
                    else if (SelectedInspectionType.ToLower(CultureInfo.CurrentCulture).Equals(Resources.InspectionType2.ToLower(CultureInfo.CurrentCulture)))
                    {
                        SelectedInspectionID = 1;
                    }
                    else if (SelectedInspectionType.ToLower(CultureInfo.CurrentCulture).Equals(Resources.InspectionType3.ToLower(CultureInfo.CurrentCulture)))
                    {
                        SelectedInspectionID = 2;
                    }
                    bool isError = false;

                    List<string> damageCodes = new List<string>();

                    if (!string.IsNullOrEmpty(damageCode1))
                    {
                        if ((damageCode1.Length == 5))
                        {
                            damageCodes.Add(damageCode1);
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgDamageCodeNumberValidation);
                            isError = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(damageCode2))
                    {
                        if ((damageCode2.Length == 5))
                        {
                            damageCodes.Add(damageCode2);
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgDamageCodeNumberValidation);
                            isError = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(damageCode3))
                    {
                        if ((damageCode3.Length == 5))
                        {
                            damageCodes.Add(damageCode3);
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgDamageCodeNumberValidation);
                            isError = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(damageCode4))
                    {
                        if ((damageCode4.Length == 5))
                        {
                            damageCodes.Add(damageCode4);
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgDamageCodeNumberValidation);
                            isError = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(damageCode5))
                    {
                        if ((damageCode5.Length == 5))
                        {
                            damageCodes.Add(damageCode5);
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgDamageCodeNumberValidation);
                            isError = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(damageCode6))
                    {
                        if ((damageCode6.Length == 5))
                        {
                            damageCodes.Add(damageCode6);
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgDamageCodeNumberValidation);
                            isError = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(damageCode7))
                    {
                        if ((damageCode7.Length == 5))
                        {
                            damageCodes.Add(damageCode7);
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgDamageCodeNumberValidation);
                            isError = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(damageCode8))
                    {
                        if ((damageCode8.Length == 5))
                        {
                            damageCodes.Add(damageCode8);
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgDamageCodeNumberValidation);
                            isError = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(damageCode9))
                    {
                        if ((damageCode9.Length == 5))
                        {
                            damageCodes.Add(damageCode9);
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgDamageCodeNumberValidation);
                            isError = true;
                        }
                    }
                    if (!string.IsNullOrEmpty(damageCode10))
                    {
                        if ((damageCode10.Length == 5))
                        {
                            damageCodes.Add(damageCode10);
                        }
                        else
                        {
                            MessageBox.Show(Resources.msgDamageCodeNumberValidation);
                            isError = true;
                        }
                    }

                    //if there is no error then save the damage codes
                    if (!isError)
                    {
                        foreach (var code in damageCodes)
                        {
                            DamageCodeProp objDamageCodeProp = new DamageCodeProp();
                            objDamageCodeProp.PortStorageVehiclesID = VehicleId;
                            objDamageCodeProp.PSVehicleInspectionID = SelectedInspectionID;
                            objDamageCodeProp.DamageDescription = string.Empty;
                            objDamageCodeProp.DamageCode = code;
                            objDamageCodeProp.InspectedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                            objDamageCodeProp.InspectionDate = DateTime.Now;
                            objDamageCodeProp.SelectedInspectionType = SelectedInspectionType;
                            await _serviceInstance.AddDamageCodeAsync(objDamageCodeProp);
                        }

                        Close(null);
                        DelegateEventVehicle.SetValueMethodDamageCode("bindgrid");
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
        /// Method For Load Inspection List.
        /// </summary>
        public void GetInspectionList()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                List<string> List = new List<string>();
                InspectionType = null;
                List = _serviceInstance.InspectionTypeOnly().Where(x => x != null).ToList();
                List.Insert(0, "Please Select");
                InspectionType = List;
                selectedInspectionType = "Please Select";
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
    }
}
