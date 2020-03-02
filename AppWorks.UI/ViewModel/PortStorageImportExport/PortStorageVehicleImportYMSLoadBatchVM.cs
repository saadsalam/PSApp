using System;
using System.Collections.Generic;
using System.Linq;
using AppWorks.UI.View.PortStorageImportExport;
using System.Windows;
using AppWorksService.Properties;
using AppWorks.UI.Common;
using System.Globalization;
using AppWorks.UI.Properties;
using System.Reflection;

namespace AppWorks.UI.ViewModel.PortStorageImportExport
{
    class PortStorageVehicleImportYMSLoadBatchVM : ViewModelBase
    {
        public PortStorageVehicleImportYMSLoadBatchVM()
        {
            DelegateEventVehicleImport.OnSetFilterVinValueEvt += new DelegateEventVehicleImport.SetFilterVinValue(GetvehicleLoadBatchList);
            GetvehicleLoadBatchList(Vin);
        }
        private int batchId;
        public int BatchId
        {
            get { return batchId; }
            private set
            {
                if (value != null)
                {
                    batchId = value;
                }
                NotifyPropertyChanged("BatchId");
            }
        }
        private string vin;
        public string Vin
        {
            get { return vin; }
            private set
            {
                if (value != null)
                {
                    vin = value;
                }
                NotifyPropertyChanged("Vin");
            }
        }
        private List<PortStorageVehicleImportProp> batchList;
        public List<PortStorageVehicleImportProp> BatchList
        {
            get { return batchList; }
            private set
            {
                if (value != null)
                {
                    batchList = value;
                }
                NotifyPropertyChanged("BatchList");
            }
        }

        private AppWorxCommand btnContinue_Click;
        public AppWorxCommand BtnContinue_Click
        {
            get
            {
                if (btnContinue_Click == null)
                {
                    btnContinue_Click = new AppWorxCommand(this.Continue);
                }
                return btnContinue_Click;
            }
        }
        private AppWorxCommand btnCancel_Click;
        public AppWorxCommand BtnCancel_Click
        {
            get
            {
                if (btnCancel_Click == null)
                {
                    btnCancel_Click = new AppWorxCommand(this.Close);
                }
                return btnCancel_Click;
            }
        }

        public PortStorageVehicleImportProp SelectedBatch { get; set; }

        private void Continue(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (SelectedBatch != null && SelectedBatch.BatchId > 0)
                {
                    DelegateEventVehicleImport.SetLoadBatchValueMethod(SelectedBatch.BatchId.Value);

                    var window = obj as PortStorageVehicleImportYMSLoadBatchWindow;
                    if (window != null)
                    {
                        window.Close();
                    }
                }
                else
                {
                    MessageBox.Show(Resources.MsgSelectUser);
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

        private void Close(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var window = obj as PortStorageVehicleImportYMSLoadBatchWindow;
                if (window != null)
                {
                    window.Close();
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
        /// 
        /// </summary>
        /// <param name="Vin"></param>
        private void GetvehicleLoadBatchList(string vin)
        {            
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                var batchData = _serviceInstance.LoadVehiclesBatchList(vin).Select(d => new PortStorageVehicleImportProp
                {
                    BatchId = d.BatchId,
                    BatchCount = d.BatchCount,
                    CreationDate = d.CreationDate,
                    Vin = d.Vin
                });                

                BatchList = new List<PortStorageVehicleImportProp>(batchData);
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
