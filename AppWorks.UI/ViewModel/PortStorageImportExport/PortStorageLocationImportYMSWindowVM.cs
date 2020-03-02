using System;
using System.Windows;
using AppWorks.UI.View.PortStorageImportExport;
using AppWorksService.Properties;
using System.Collections.ObjectModel;
using AppWorks.UI.Common;
using System.Globalization;
using AppWorks.UI.Properties;
using System.Reflection;

namespace AppWorks.UI.ViewModel.PortStorageImportExport
{
    class PortStorageLocationImportYMSWindowVM : ViewModelBase
    {
        string _User = Application.Current.Properties["LoggedInUserName"].ToString();

        #region :: Constructor ::
        public PortStorageLocationImportYMSWindowVM()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                DelegateEventVehicleImport.OnSetLoadBatchValueEvt += new DelegateEventVehicleImport.SetLoadBatchValue(GetPortStorageLocationImportList);
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

        #region :: All Property ::

        private int vehiclesCount;
        public int VehiclesCount
        {
            get { return this.vehiclesCount; }
            set { this.vehiclesCount = value; NotifyPropertyChanged("VehiclesCount"); }
        }


        /// <summary>
        /// This property for BatchId
        /// </summary>
        private int batchId;
        public int BatchId
        {
            get { return this.batchId; }
            set { this.batchId = value; NotifyPropertyChanged("BatchId"); }
        }
        /// <summary>
        /// This property for File Directory
        /// </summary>
        private string fileDirectory;
        public string FileDirectory
        {
            get { return this.fileDirectory; }
            set { this.fileDirectory = value; NotifyPropertyChanged("FileDirectory"); }
        }
        /// <summary>
        /// This proprty for File Name
        /// </summary>
        private string fileName;
        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; NotifyPropertyChanged("FileName"); }
        }
        /// <summary>
        /// This property for File Archive Directory
        /// </summary>
        private string fileArchiveDirectory;
        public string FileArchiveDirectory
        {
            get { return this.fileArchiveDirectory; }
            set { this.fileArchiveDirectory = value; NotifyPropertyChanged("FileArchiveDirectory"); }
        }
        /// <summary>
        /// This proprt for get message from stored procedure
        /// </summary>
        private string message;
        public string Message
        {
            get { return this.message; }
            set { this.message = value; NotifyPropertyChanged("Message"); }
        }
        /// <summary>
        /// This is list to bind data in grid view.
        /// </summary>
        private ObservableCollection<PortStorageVehicleImportProp> portStorageLocationImportList;
        public ObservableCollection<PortStorageVehicleImportProp> PortStorageLocationImportList
        {
            get { return portStorageLocationImportList; }
            set { portStorageLocationImportList = value; NotifyPropertyChanged("PortStorageLocationImportList"); }
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
                    btnLoadBatch_Click = new AppWorxCommand(this.OpenLocationBatchLoadDialog);
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
            // creating the object of service property

            try
            {
                PortStorageVehicleImportProp portStorageVehicleImportProp = new PortStorageVehicleImportProp();
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BatchId = GetPortStorageLocationBatchId();
                if (BatchId > 0)
                {
                    bool Result = SetPortStorageLocationNextBatchId(BatchId);
                    if (Result)
                    {
                        bool Data = LocationImportTransactionProcess(BatchId, _User);
                        if (Data)
                        {
                            portStorageVehicleImportProp = ImportPortStorageLocation(BatchId, _User);
                            if (portStorageVehicleImportProp != null)
                            {
                                if (portStorageVehicleImportProp.ReturnCode == 0)
                                {
                                    //MessageBox.Show(Message);
                                    MessageBox.Show("The Port Storage location update import completed successfully!", " Import Complete.");
                                    GetPortStorageLocationImportList(BatchId);
                                }
                                else
                                {
                                    MessageBox.Show("The following error was encountered while processing the batch, Error Code: " + portStorageVehicleImportProp.ReturnCode + " Error Message: " + portStorageVehicleImportProp.ReturnMessage + "", "Data Import Error");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("There was an error inserting one of the location records, the import has been aborted!", "Error Inserting Record");
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
            // creating the object of service property

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                BatchId = GetPortStorageLocationBatchId();
                if (BatchId > 0)
                {
                    bool Result = SetPortStorageLocationNextBatchId(BatchId);
                    if (Result)
                    {
                        bool Data = LocationImportTransactionProcess(BatchId, _User);
                        if (Data)
                        {
                            GetPortStorageLocationImportList(BatchId);
                        }
                        else
                        {
                            MessageBox.Show("There was an error inserting one of the location records, the import has been aborted!", "Error Inserting Record");
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
                    portStorageVehicleImportProp = ImportPortStorageLocation(BatchId, _User);
                    if (portStorageVehicleImportProp != null)
                    {
                        if (portStorageVehicleImportProp.ReturnCode == 0)
                        {
                            //MessageBox.Show(Message);
                            MessageBox.Show("The Port Storage location update import completed successfully!", " Import Complete.");
                            GetPortStorageLocationImportList(BatchId);
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
                PortStorageLocationImportList = null;
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
        public void OpenLocationBatchLoadDialog(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                PortStorageLocationImportYMSLoadBatchWindow objLoadBatch = new PortStorageLocationImportYMSLoadBatchWindow();
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
        /// This method is used to  Get Port Storage Location Batch Id
        /// </summary>
        /// <returns></returns>
        public int GetPortStorageLocationBatchId()
        {
            int GetBatchId = 0;
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                GetBatchId = _serviceInstance.GetPortStorageLocationBatchId();
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
            return GetBatchId;

        }

        /// <summary>
        /// This method is used to Set Port Storage Location Next BatchId
        /// </summary>
        /// <param name="GetBatchId"></param>
        /// <returns></returns>
        public bool SetPortStorageLocationNextBatchId(int GetBatchId)
        {
            bool result = false;
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = _serviceInstance.SetPortStorageLocationNextBatchId(GetBatchId);
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
        /// This method is used to Import Port Storage Location
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public PortStorageVehicleImportProp ImportPortStorageLocation(int BatchId, string User)
        {
            PortStorageVehicleImportProp result = new PortStorageVehicleImportProp();
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = _serviceInstance.ImportPortStorageLocation(BatchId, User);
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
        /// This method is used to Get Port Storage Location Import List
        /// </summary>
        /// <param name="GetBatchId"></param>
        public void GetPortStorageLocationImportList(int GetBatchId)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                PortStorageLocationImportList = new ObservableCollection<PortStorageVehicleImportProp>(_serviceInstance.GetPortStorageLocationImportList(GetBatchId));
                BatchId = GetBatchId;
                VehiclesCount = PortStorageLocationImportList.Count;
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
        /// This method is used to get file Dircetory Name
        /// </summary>
        /// <returns></returns>
        public string GetFileDirectory()
        {
            string result = string.Empty;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = _serviceInstance.GetPortStorageVehiclesImportFileDirectory();
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
        /// This method is used to get file Name
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            string result = string.Empty;
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = _serviceInstance.GetPortStorageLocationImportFileName();
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
        /// This method is used to get file Archive Directory
        /// </summary>
        /// <returns></returns>
        public string GetFileArchiveDirectory()
        {
            string result = string.Empty;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = _serviceInstance.GetPortStorageVehiclesImportFileArchiveDirectory();
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
        /// This method is used to get data and insert Transaction Process
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool LocationImportTransactionProcess(int BatchId, string User)
        {
            bool result = false;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = _serviceInstance.LocationImportTransactionProcess(BatchId, User);
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
