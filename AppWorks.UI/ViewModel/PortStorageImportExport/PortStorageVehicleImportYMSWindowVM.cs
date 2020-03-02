using System;
using AppWorks.UI.View.PortStorageImportExport;
using System.Windows;
using AppWorksService.Properties;
using System.Collections.ObjectModel;
using AppWorks.UI.Common;
using System.Globalization;
using AppWorks.UI.Properties;
using System.Reflection;
namespace AppWorks.UI.ViewModel.PortStorageImportExport
{
    class PortStorageVehicleImportYMSWindowVM : ViewModelBase
    {
        string _User = Application.Current.Properties["LoggedInUserName"].ToString();

        #region :: Constructor ::
        public PortStorageVehicleImportYMSWindowVM()
        {
            DelegateEventVehicleImport.OnSetLoadBatchValueEvt += new DelegateEventVehicleImport.SetLoadBatchValue(GetPortStorageVehicleImportList);
        }
        #endregion

        #region :: All Property ::
        private int vehiclesCount;
        public int VehiclesCount
        {
            get { return this.vehiclesCount; }
            set { this.vehiclesCount = value; NotifyPropertyChanged("VehiclesCount"); }
        }


        private int batchId;
        public int BatchId
        {
            get { return this.batchId; }
            set { this.batchId = value; NotifyPropertyChanged("BatchId"); }
        }

        private string message;
        public string Message
        {
            get { return this.message; }
            set { this.message = value; NotifyPropertyChanged("Message"); }
        }

        private ObservableCollection<PortStorageVehicleImportProp> portStorageVehicleImportList;
        public ObservableCollection<PortStorageVehicleImportProp> PortStorageVehicleImportList
        {
            get { return portStorageVehicleImportList; }
            set { portStorageVehicleImportList = value; NotifyPropertyChanged("PortStorageVehicleImportList"); }
        }
        #endregion

        #region :: Button Click Event ::
        /// <summary>
        /// Import And Process File button event
        /// </summary>
        private AppWorxCommand btnImportAndProcessFile_Click;
        public AppWorxCommand BtnImportAndProcessFile_Click
        {
            get
            {
                if (btnImportAndProcessFile_Click == null)
                {
                    btnImportAndProcessFile_Click = new AppWorxCommand(this.ImportAndProcessFile);
                }
                return btnImportAndProcessFile_Click;
            }
        }

        /// <summary>
        /// Import File button event
        /// </summary>
        private AppWorxCommand btnImportFile_Click;
        public AppWorxCommand BtnImportFile_Click
        {
            get
            {
                if (btnImportFile_Click == null)
                {
                    btnImportFile_Click = new AppWorxCommand(this.ImportFile);
                }
                return btnImportFile_Click;
            }
        }

        /// <summary>
        /// Process File button event
        /// </summary>
        private AppWorxCommand btnProcessFile_Click;
        public AppWorxCommand BtnProcessFile_Click
        {
            get
            {
                if (btnProcessFile_Click == null)
                {
                    btnProcessFile_Click = new AppWorxCommand(this.ProcessFile);
                }
                return btnProcessFile_Click;
            }
        }

        /// <summary>
        /// Clear Finished button event
        /// </summary>
        private AppWorxCommand btnClearFinished_Click;
        public AppWorxCommand BtnClearFinished_Click
        {
            get
            {
                if (btnClearFinished_Click == null)
                {
                    btnClearFinished_Click = new AppWorxCommand(this.ClearFinished);
                }
                return btnClearFinished_Click;
            }
        }

        /// <summary>
        /// Print button event
        /// </summary>
        private AppWorxCommand btnLoadBatch_Click;
        public AppWorxCommand BtnLoadBatch_Click
        {
            get
            {
                if (btnLoadBatch_Click == null)
                {
                    btnLoadBatch_Click = new AppWorxCommand(this.OpenVehicleBatchLoadDialog);
                }
                return btnLoadBatch_Click;
            }
        }
        #endregion

        #region :: Event Method ::
        /// <summary>
        /// This function is used to Import And Process File in database
        /// </summary>
        /// <param name="obj"></param>
        public void ImportAndProcessFile(object obj)
        {
            try
            {
                PortStorageVehicleImportProp portStorageVehicleImportProp = new PortStorageVehicleImportProp();
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BatchId = GetPortStorageVehiclesBatchId();
                if (BatchId > 0)
                {
                    bool Result = SetPortStorageVehiclesNextBatchId(BatchId);

                    if (Result)
                    {
                        bool isSucceeded = VehicleImportTransactionProcess(BatchId, _User);

                        if (isSucceeded)
                        {
                            portStorageVehicleImportProp = ImportPortStorageVehicles(BatchId, _User);

                            if (portStorageVehicleImportProp != null)
                            {
                                if (portStorageVehicleImportProp.ReturnCode == 0)
                                {                                   
                                    MessageBox.Show("" + portStorageVehicleImportProp.ReturnMessage + "", " Import Complete.");
                                    GetPortStorageVehicleImportList(BatchId);
                                }
                                else
                                {
                                    MessageBox.Show("The following error was encountered while processing the batch, Error Code: " + portStorageVehicleImportProp.ReturnCode + " Error Message: " + portStorageVehicleImportProp.ReturnMessage + "", "Data Import Error");
                                }
                            }                            
                        }
                        else
                        {
                            MessageBox.Show("There was an error inserting one of the vehicle records, the import has been aborted!", "Error Inserting Record");
                        }

                    }
                    else
                    {
                        MessageBox.Show("There was an error setting the next batchid. Import has been aborted!", "Error Setting Next Batch ID");
                    }

                }
                else
                {
                    MessageBox.Show("There was an error getting the next batchid. Import has been aborted!", "Error Getting Batch ID");
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
        /// This function is used to Import File in database
        /// </summary>
        /// <param name="obj"></param>
        public void ImportFile(object obj)
        {

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BatchId = GetPortStorageVehiclesBatchId();
                if (BatchId > 0)
                {
                    bool Result = SetPortStorageVehiclesNextBatchId(BatchId);
                    if (Result)
                    {
                        bool Data = VehicleImportTransactionProcess(BatchId, _User);
                        if (Data)
                        {
                            GetPortStorageVehicleImportList(BatchId);
                        }
                        else
                        {
                            var errorMessage="There was an error inserting one of the vehicle records, the import has been aborted!";
                            var exception = new Exception(errorMessage);
                            MessageBox.Show(errorMessage, "Error Inserting Record");
                            CommonSettings.logger.LogError(this.GetType(), exception);
                        }

                    }
                    else
                    {
                        var errorMessage="There was an error setting the next batchid. Import has been aborted!";
                        var exception = new Exception(errorMessage);
                        MessageBox.Show(errorMessage, "Error Setting Next Batch ID");
                        CommonSettings.logger.LogError(this.GetType(), exception);
                    }

                }
                else
                {
                    var errorMessage = "There was an error getting the next batchid. Import has been aborted!";
                    var exception = new Exception(errorMessage);
                    MessageBox.Show(errorMessage, "Error Getting Batch ID");
                    CommonSettings.logger.LogError(this.GetType(), exception);
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
        /// This function is used to Process File in database
        /// </summary>
        /// <param name="obj"></param>
        public void ProcessFile(object obj)
        {

            try
            {
                PortStorageVehicleImportProp portStorageVehicleImportProp = new PortStorageVehicleImportProp();

                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (BatchId > 0)
                {
                    portStorageVehicleImportProp = ImportPortStorageVehicles(BatchId, _User);

                    if (portStorageVehicleImportProp != null)
                    {
                        if (portStorageVehicleImportProp.ReturnCode == 0)
                        {
                            //MessageBox.Show(Message);
                            MessageBox.Show("" + portStorageVehicleImportProp.ReturnMessage + "", " Import Complete.");
                            GetPortStorageVehicleImportList(BatchId);
                        }
                        else
                        {
                            MessageBox.Show("The following error was encountered while processing the batch, Error Code: " + portStorageVehicleImportProp.ReturnCode + " Error Message: " + portStorageVehicleImportProp.ReturnMessage + "", "Data Import Error");
                        }

                    }


                }
                else
                {
                    MessageBox.Show("Please Load Batch for Process File");
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
        /// This function is used to Clear Finished 
        /// </summary>
        /// <param name="obj"></param>
        public void ClearFinished(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageVehicleImportList = null;
                VehiclesCount = 0;
                BatchId = 0;

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
        /// Function Open Batch Load to open window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn></createdOn>
        public void OpenVehicleBatchLoadDialog(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                PortStorageVehicleImportYMSLoadBatchWindow objLoadBatch = new PortStorageVehicleImportYMSLoadBatchWindow();
                objLoadBatch.Owner = Application.Current.MainWindow;

                objLoadBatch.ShowDialog();
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
        #endregion

        #region :: Method ::
        /// <summary>
        /// This method is used to get batch id from databse for Vehicle.
        /// </summary>
        /// <returns></returns>
        public int GetPortStorageVehiclesBatchId()
        {
            int batchId = 0;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                batchId = _serviceInstance.GetPortStorageVehiclesBatchId();
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

            return batchId;
        }
        /// <summary>
        /// This method is used to update datasbe using batchid
        /// </summary>
        /// <param name="GetBatchId"></param>
        /// <returns></returns>
        public bool SetPortStorageVehiclesNextBatchId(int batchId)
        {
            bool result = false;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = _serviceInstance.SetPortStorageVehiclesNextBatchId(batchId);
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

            return result;
        }

        /// <summary>
        /// This method is used to get message from storeprocedure.
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public PortStorageVehicleImportProp ImportPortStorageVehicles(int BatchId, string User)
        {

            PortStorageVehicleImportProp result = new PortStorageVehicleImportProp();

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                result = _serviceInstance.ImportPortStorageVehicles(BatchId, User);

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

            return result;
        }

        /// <summary>
        /// This method is used get data from database to bind grid.
        /// </summary>
        /// <param name="GetBatchId"></param>
        public void GetPortStorageVehicleImportList(int batchId)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageVehicleImportList = new ObservableCollection<PortStorageVehicleImportProp>(_serviceInstance.GetPortStorageVehicleImportList(batchId));
                BatchId = batchId;
                VehiclesCount = PortStorageVehicleImportList.Count;
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
        /// This method is used to get data and insert Transaction Process for Vehicle
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool VehicleImportTransactionProcess(int BatchId, string User)
        {

            bool result = false;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                result = _serviceInstance.VehicleImportTransactionProcess(BatchId, User);

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

            return result;
        }

        #endregion
    }
}
