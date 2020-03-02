using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorks.UI;
using AppWorksService.Properties;
using System.Windows;
using AppWorks.UI.Common;
using System.Globalization;
using AppWorks.UI.Properties;
using System.Reflection;
using System.Collections.ObjectModel;

namespace AppWorks.UI.ViewModel.CustomerAdmin
{
    class PortStorageRateAdminVM : ChangeTrackingViewModel
    {
        public PortStorageRateAdminVM()
        {
            IsEnabled = false;
            RateAdminDelegate.OnSetRateAdminValueEvt -= RateAdminDelegate_OnSetRateAdminValueEvt;
            RateAdminDelegate.OnSetRateAdminValueEvt += RateAdminDelegate_OnSetRateAdminValueEvt;
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value != null)
                    isEnabled = value;
            }
        }

        private int portStorageRateID;
        public int PortStorageRateID
        {
            get { return portStorageRateID; }
            set
            {
                if (value != null)
                    portStorageRateID = value;
                NotifyPropertyChanged("PortStorageRateID");
            }
        }

        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                if (value != null)
                    customerName = value;
                NotifyPropertyChanged("customerName");
            }
        }

        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set
            {
                if (value != null)
                    customerID = value;
                NotifyPropertyChanged("CustomerID");
            }
        }

        private Decimal? entryFee;
        public Decimal? EntryFee
        {
            get { return entryFee; }
            set
            {
                if (value != null)
                    entryFee = value;
                NotifyPropertyChanged("EntryFee");
            }
        }

        private Decimal? perDiem;
        public Decimal? PerDiem
        {
            get { return perDiem; }
            set
            {
                if (value != null)
                    perDiem = value;
                NotifyPropertyChanged("PerDiem");
            }
        }

        private int? perDiemGraceDays;
        public int? PerDiemGraceDays
        {
            get { return perDiemGraceDays; }
            set
            {
                if (value != null)
                    perDiemGraceDays = value;
                NotifyPropertyChanged("PerDiemGraceDays");
            }
        }

        private DateTime? startDate;
        public DateTime? StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                NotifyPropertyChanged("StartDate");
            }
        }

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                NotifyPropertyChanged("EndDate");
            }
        }

        private DateTime? creationDate;
        public DateTime? CreationDate
        {
            get { return creationDate; }
            set
            {
                if (value != null)
                    creationDate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }

        private DateTime? updatedDate;
        public DateTime? UpdatedDate
        {
            get { return updatedDate; }
            set
            {
                if (value != null)
                    updatedDate = value;
                NotifyPropertyChanged("UpdatedDate");
            }
        }

        private string createdBy;
        public string CreatedBy
        {
            get { return createdBy; }
            set
            {
                if (value != null)
                    createdBy = value;
                NotifyPropertyChanged("CreatedBy");
            }
        }

        private string updatedBy;
        public string UpdatedBy
        {
            get { return updatedBy; }
            set
            {
                if (value != null)
                    updatedBy = value;
                NotifyPropertyChanged("UpdatedBy");
            }
        }


        private AppWorxCommand btnOk_Click;
        /// <summary>
        /// ok button event
        /// </summary>
        public AppWorxCommand BtnOk_Click
        {
            get
            {
                if (btnOk_Click == null)
                {
                    btnOk_Click = new AppWorxCommand(this.AddPortStorageRate);
                }
                return btnOk_Click;
            }
        }


        private AppWorxCommand btnCancel_Click;
        /// <summary>
        /// cancel button event
        /// </summary>
        public AppWorxCommand BtnCancel_Click
        {
            get
            {
                if (btnCancel_Click == null)
                {
                    btnCancel_Click = new AppWorxCommand(this.Cancel);
                }
                return btnCancel_Click;
            }
        }

        private void Cancel(object obj)
        {
            StartDate = null;
            EndDate = null;
            PerDiem = 0;
            PerDiemGraceDays = 0;
            EntryFee = 0;
            PerDiem = 0;
            PerDiemGraceDays = 0;
            StartDate = null;
            EndDate = null;
            PortStorageRateID = 0;
            UpdatedBy = null;
            UpdatedDate = null;
            CreatedBy = null;
            CreationDate = null;
            CustomerName = null;
            if (Application.Current.Windows[1] != null && Application.Current.Windows[1].Title == "Port Storage Rate Admin")
            {
                Application.Current.Windows[1].Close();
            }
        }

        private void AddPortStorageRate(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var result=false;
                if ( PortStorageRateID > 0)
                {
                    if (StartDate.Equals(null))
                    {
                        MessageBox.Show(Resources.msgStratDateReq);
                    }
                    else
                    {
                        PortStorageRateList objPortStorageRateProp = new PortStorageRateList();
                        objPortStorageRateProp.PortStorageRatesID = PortStorageRateID;
                        objPortStorageRateProp.CustomerID = CustomerID;
                        objPortStorageRateProp.EntryFee = EntryFee;
                        objPortStorageRateProp.StartDate = StartDate;
                        objPortStorageRateProp.EndDate = EndDate;
                        objPortStorageRateProp.PerDiem = PerDiem;
                        objPortStorageRateProp.PerDiemGraceDays = PerDiemGraceDays;
                        objPortStorageRateProp.CreatedBy = CreatedBy;
                        objPortStorageRateProp.CreationDate = CreationDate;
                        objPortStorageRateProp.UpdatedDate = DateTime.Now;
                        objPortStorageRateProp.UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                        result = _serviceInstance.UpdatePortStorageRate(objPortStorageRateProp);
                        if (result)
                        {
                            MessageBox.Show(Resources.MsgRecordUpdated);
                            Cancel(null);                            
                        }
                    }
                }
                else
                {
                    if (StartDate.Equals(null))
                    {
                        MessageBox.Show(Resources.msgStratDateReq);
                    }
                    else
                    {
                        PortStorageRateList objPortStorageRateProp = new PortStorageRateList();
                        objPortStorageRateProp.CustomerID = CustomerID;
                        objPortStorageRateProp.EntryFee = EntryFee;
                        objPortStorageRateProp.StartDate = StartDate;
                        objPortStorageRateProp.EndDate = EndDate;
                        objPortStorageRateProp.PerDiem = PerDiem;
                        objPortStorageRateProp.PerDiemGraceDays = PerDiemGraceDays;
                        objPortStorageRateProp.CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                        objPortStorageRateProp.CreationDate = DateTime.Now;
                        objPortStorageRateProp.UpdatedDate = null;
                        objPortStorageRateProp.UpdatedBy = string.Empty;
                        result = _serviceInstance.AddPortStorageRate(objPortStorageRateProp);
                        if (result)
                        {
                            MessageBox.Show(Resources.msgInsertedSuccessfully);
                            Cancel(null);
                        }
                    }
                }
                RateAdminDelegate.SetRateAdminModifiedMethod(result);
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

        void RateAdminDelegate_OnSetRateAdminValueEvt(PortStorageRateList selectedItems)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (selectedItems.PortStorageRatesID > 0)
                {
                    EntryFee = selectedItems.EntryFee;
                    PerDiem = selectedItems.PerDiem;
                    PerDiemGraceDays = selectedItems.PerDiemGraceDays;
                    StartDate = selectedItems.StartDate;
                    EndDate = selectedItems.EndDate;
                    PortStorageRateID = selectedItems.PortStorageRatesID;
                    UpdatedBy = selectedItems.UpdatedBy;
                    UpdatedDate = selectedItems.UpdatedDate;
                    CreatedBy = selectedItems.CreatedBy;
                    CreationDate = selectedItems.CreationDate;
                    CustomerID = Convert.ToInt32(selectedItems.CustomerID);
                    CustomerName = selectedItems.CustomerName;
                }
                else
                {
                    CustomerID = Convert.ToInt32(selectedItems.CustomerID);
                    EntryFee = 0;
                    PerDiem = 0;
                    PerDiemGraceDays = 0;
                    CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                    CreationDate = DateTime.Now;
                    CustomerName = selectedItems.CustomerName;
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
                    RateAdminDelegate.OnSetRateAdminValueEvt -= RateAdminDelegate_OnSetRateAdminValueEvt;
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
