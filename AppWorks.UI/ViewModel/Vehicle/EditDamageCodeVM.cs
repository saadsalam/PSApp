using System;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using AppWorks.UI.Common;
using AppWorks.UI.Properties;
using System.Reflection;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class EditDamageCodeVM : ViewModelBase
    {
        public EditDamageCodeVM(int damageDetailId)
        {
            DamageDetailsId = damageDetailId;
        }


        #region "Page Properties"
        private int damageDetailsId;
        public int DamageDetailsId
        {
            get { return damageDetailsId; }
            set
            {
                this.damageDetailsId = value;
            }
        }

        private string damageCode;
        public string DamageCode
        {
            get { return damageCode; }
            set
            {
                if (value != null)
                {
                    this.damageCode = value;
                    NotifyPropertyChanged("DamageCode");
                }
            }
        }
        #endregion

        #region "Page Events"
        private ICommand btnSubmit_Click;

        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnSubmit_Click
        {
            get
            {
                if (btnSubmit_Click == null)
                {
                    btnSubmit_Click = new AppWorxCommand(
                        param => this.UpdateDamageCode(),
                        param => CanCheck
                        );
                }
                return btnSubmit_Click;
            }
        }

        private ICommand btnCancel_Click;

        /// <summary>
        /// Cancel button event
        /// </summary>
        public ICommand BtnCancel_Click
        {
            get
            {
                if (btnCancel_Click == null)
                {
                    btnCancel_Click = new AppWorxCommand(
                        param => this.CancelDamageCode(),
                        param => CanCheck
                        );
                }
                return btnCancel_Click;
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
        /// Function to update the damage code.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>may-19,2016</createdOn>
        private void UpdateDamageCode()
        {
            string userRole = string.Empty;
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if ((DamageCode != null) && (DamageCode.Length == 5))
                {
                    int DamageCodeNum;
                    if (int.TryParse(DamageCode, out DamageCodeNum))
                    {
                        AppWorksService.Properties.PortStorageDamageDetailsProp objDamageCodeProp = new AppWorksService.Properties.PortStorageDamageDetailsProp();
                        objDamageCodeProp.DamageCode = DamageCode;
                        objDamageCodeProp.PSVehicleDamageDetailID = DamageDetailsId;
                        var result = _serviceInstance.EditDamageCode(objDamageCodeProp);
                        if (result <= 0)
                        {
                            MessageBox.Show(Resources.ErrorDamageCode);
                        }
                        else
                        {
                            DelegateEventVehicle.SetValueMethodDamageCode("bindgrid");
                            CancelDamageCode();
                        }
                    }
                    else
                    {
                        MessageBox.Show(Resources.msgDamageCodeValidation);
                    }
                }
                else
                {
                    MessageBox.Show(Resources.msgDamageCodeNumberValidation);
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
        /// Function to cancel the updation of damage code.
        /// </summary>
        /// <param name="Na"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>may-19,2016</createdOn>
        public void CancelDamageCode()
        {
            try
            {
                this.DamageCode = string.Empty;
                this.DamageDetailsId = 0;
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.DataContext == this)
                    {
                        window.Close();
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

    }
}
