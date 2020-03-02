using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppWorks.UI.Common;
using AppWorks.UI.Models;
using AppWorks.UI.View.UserControls.Vehicle;
using System.Reflection;
using AppWorksService.Properties;
using System.Globalization;
using AppWorks.UI.Model;
using AppWorks.UI.Properties;
using Appworks.Reports;
using AppWorks.Utilities;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class PostStorageRequestProcessingVM : ViewModelBase, IDisposable
    {
        string userCode = Application.Current.Properties["LoggedInUserName"].ToString();

        public PostStorageRequestProcessingVM()
        {
            try
            {
                DelegateEventDateAndVehicleProcessing.OnSetValueEvt += new DelegateEventDateAndVehicleProcessing.SetValue(Executecommand);

                var data = _serviceInstance.GetDAIAddressName(userCode).Select(d => new RequestProcessingPrintModel
                {
                    CompanyName = d.CompanyName,
                    CompanyAddressLine1 = d.AddressLine1,
                    CompanyCity = d.City,
                    Phone = d.Phone
                }).FirstOrDefault();

                if (data != null)
                {
                    CompanyName = data.CompanyName;
                    CompanyAddressLine1 = data.CompanyAddressLine1;
                    CompanyCity = data.CompanyCity;
                    Phone = data.Phone;
                }
            }
            catch (Exception ex)
            {
                DelegateEventDateAndVehicleProcessing.OnSetValueEvt -= new DelegateEventDateAndVehicleProcessing.SetValue(Executecommand);
                LogHelper.LogErrorToDb(ex);
                var data = _serviceInstance.GetDAIAddressName(userCode).Select(d => new RequestProcessingPrintModel
                {
                    CompanyName = d.CompanyName,
                    CompanyAddressLine1 = d.AddressLine1,
                    CompanyCity = d.City,
                    Phone = d.Phone
                }).FirstOrDefault();

                if (data != null)
                {
                    DelegateEventDateAndVehicleProcessing.OnSetValueEvt += new DelegateEventDateAndVehicleProcessing.SetValue(Executecommand);
                    CompanyName = data.CompanyName;
                    CompanyAddressLine1 = data.CompanyAddressLine1;
                    CompanyCity = data.CompanyCity;
                    Phone = data.Phone;
                }
               
            }
     
        }

        private void Executecommand(string value)
        {
            if (value.Equals("ScanVin", StringComparison.OrdinalIgnoreCase))
            {
                UpdateAndGetPortStorageProcessignDetails(null);
            }
        }

        private void GetVINCommandName(string value)
        {
            Vin = value;
            UpdateAndGetPortStorageProcessignDetails(Vin);
        }

        #region "Page Properties"
        /// <summary>
        /// This is property used to Change Report functionality
        /// </summary>
        Telerik.Reporting.ReportSource _myReportSource;

        public Telerik.Reporting.ReportSource MyReportSource
        {
            get
            {
                return _myReportSource;
            }
            set
            {
                // Check if it's really a change 
                if (value == _myReportSource)
                    return;

                // Change Report 
                _myReportSource = value;

                // Notify attached View(s) 
                NotifyPropertyChanged("MyReportSource");
                //RaisePropertyChanged("Report");
            }
        }

        private string companyName;
        public string CompanyName
        {
            get { return this.companyName; }
            set { this.companyName = value; NotifyPropertyChanged("CompanyName"); }
        }
        private string companyAddressLine1;
        public string CompanyAddressLine1
        {
            get { return this.companyAddressLine1; }
            set { this.companyAddressLine1 = value; NotifyPropertyChanged("CompanyAddressLine1"); }
        }
        private string companyCity;
        public string CompanyCity
        {
            get { return this.companyCity; }
            set { this.companyCity = value; NotifyPropertyChanged("CompanyCity"); }
        }
        private string phone;
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; NotifyPropertyChanged("Phone"); }
        }
        /// <summary>
        /// Property for VIN
        /// </summary>
        private string vin;
        public string Vin
        {
            get
            {
                return vin;
            }
            set
            {
                if (value != null)
                {
                    this.vin = value;
                    NotifyPropertyChanged("Vin");
                }
            }
        }
        /// <summary>
        /// Property for Year
        /// </summary>
        private string year;
        public string Year
        {
            get
            {
                return year;
            }
            set
            {
                this.year = value;
                NotifyPropertyChanged("Year");
            }
        }
        /// <summary>
        /// Property for Model
        /// </summary>
        private string model;
        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                this.model = value;
                NotifyPropertyChanged("Model");
            }
        }
        /// <summary>
        /// Property for Location
        /// </summary>
        private string bayLocation;
        public string BayLocation
        {
            get
            {
                return bayLocation;
            }
            set
            {
                this.bayLocation = value;
                NotifyPropertyChanged("BayLocation");
            }
        }
        /// <summary>
        /// Property for Customer Name
        /// </summary>
        private string customerName;
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                this.customerName = value;
                NotifyPropertyChanged("CustomerName");
            }
        }
        /// <summary>
        /// Property for Vehicle status
        /// </summary>
        private string vehicleStatus;
        public string VehicleStatus
        {
            get
            {
                return vehicleStatus;
            }
            set
            {
                this.vehicleStatus = value;
                NotifyPropertyChanged("VehicleStatus");
            }
        }
        /// <summary>
        /// Property for Request Date
        /// </summary>
        private DateTime? dtRequestDate = DateTime.Now;
        public DateTime? DtRequestDate
        {
            get
            {
                return dtRequestDate;
            }
            set
            {
                this.dtRequestDate = value;
                NotifyPropertyChanged("DtRequestDate");
            }
        }
        /// <summary>
        /// Property for Date In
        /// </summary>
        private DateTime dtDateIn;
        public DateTime DtDateIn
        {
            get
            {
                return dtDateIn;
            }
            set
            {
                this.dtDateIn = value;
                NotifyPropertyChanged("DtDateIn");
            }
        }
        /// <summary>
        /// Property for Note
        /// </summary>
        private string note;
        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                this.note = value;
                NotifyPropertyChanged("Note");
            }
        }
        /// <summary>
        /// this is used to bind data in grid
        /// </summary>
        private ObservableCollection<VehicleModel> requestProcessingList;
        public ObservableCollection<VehicleModel> RequestProcessingList
        {
            get { return requestProcessingList; }
            private set { requestProcessingList = value; NotifyPropertyChanged("RequestProcessingList"); }
        }
        private bool _isReturnedToInventory;
        public bool IsReturnedToInventory
        {
            get { return _isReturnedToInventory; }
            set
            {
                if (_isReturnedToInventory = value) return;

                _isReturnedToInventory = value;
            }
        }

        /// <summary>
        /// This List is used to bind multiple request in grid
        /// </summary>
        public List<VehicleModel> RequestProcessingListTemp = new List<VehicleModel>();



        #endregion

        /// <summary>
        /// for Total Records on end of grid
        /// </summary>
        /// 
        private long count;
        public long Count
        {
            get { return count; }
            set
            {
                this.count = value;
                NotifyPropertyChanged("Count");
            }
        }

        #region "Page Events"
        /// <summary>
        /// Submit button event is used to add record for request processing
        /// </summary>
        private AppWorxCommand btnSubmit_Click;
        public AppWorxCommand BtnSubmit_Click
        {
            get
            {
                if (btnSubmit_Click == null)
                {
                    btnSubmit_Click = new AppWorxCommand(this.UpdateAndGetPortStorageProcessignDetails);
                }
                return btnSubmit_Click;
            }
        }
        /// <summary>
        /// Submit button event for cancel 
        /// </summary>
        private AppWorxCommand btnCancel_Click;
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
        /// <summary>
        /// this button event for batch process functionility.
        /// </summary>
        private AppWorxCommand btnProcessBatch_Click;
        public AppWorxCommand BtnProcessBatch_Click
        {
            get
            {
                if (btnProcessBatch_Click == null)
                {
                    btnProcessBatch_Click = new AppWorxCommand(this.UpdatePortStorageRequestDateForBatchProcess);
                }
                return btnProcessBatch_Click;
            }
        }
        /// <summary>
        /// Print button event
        /// </summary>
        private AppWorxCommand btnPrint_Click;
        public AppWorxCommand BtnPrint_Click
        {
            get
            {
                if (btnPrint_Click == null)
                {
                    btnPrint_Click = new AppWorxCommand(this.OpenPrintDialog);
                }
                return btnPrint_Click;
            }
        }

        private AppWorxCommand checkBox_Checked;
        public AppWorxCommand CheckBox_Checked
        {
            get
            {
                if (checkBox_Checked == null)
                {
                    checkBox_Checked = new AppWorxCommand(this.ReturnToInventory);
                }
                return checkBox_Checked;
            }
        }
        #endregion

        #region "Page Functions and Methods"
        /// <summary>
        /// Method Cancel to refresh the page details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-11,2016</createdOn>
        void Cancel(object obj)
        {
            try
            {
                VehicleProp objVehicleProp = new VehicleProp();
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
        }

        /// <summary>
        /// Function UpdatePortStorageProcessignDetails to update the vehicle details with bind the grid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-11,2016</createdOn>
        public void UpdateAndGetPortStorageProcessignDetails(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                if (!string.IsNullOrEmpty(Vin))
                {

                    VehicleProp objVehicleProp = new VehicleProp();
                    objVehicleProp.Vin = Vin;
                    var item = RequestProcessingListTemp.Find(it => it.Vin.Contains(Vin));
                    int? vehicleId = item != null ? item.VehiclesID : (int?)null;
                    if (item != null)
                    {
                        MessageBox.Show(Resources.msgVehicleListAlready, Resources.msgTitleMessageBoxVehicleList);
                        ClearFiled();
                        return;
                    }
                    else
                    {
                        List<VehicleProp> lstvechileProp = new List<VehicleProp>();
                        lstvechileProp = CheckMultipleVecheleDetailByVIN(objVehicleProp.Vin);

                        if (lstvechileProp.Count == 0)
                        {
                            MessageBox.Show(Resources.msgProcessingVINFound, Resources.msgTitleMessageBoxVehicleNotFound);
                            return;
                        }

                        var pendingVehicles = lstvechileProp.Where(x => x.DateOut == null).ToList();

                        if (pendingVehicles.Count > 1)
                        {
                            MessageBox.Show(Resources.msgMultipleVINProcessing, Resources.msgTitleMessageBoxMultipleVIN);
                            return;
                        }
                        else if (pendingVehicles.Count == 0)
                        {
                            VehicleProp vechileProp = GetVecheleDetailByVIN(objVehicleProp.Vin, vehicleId);
                            if (vechileProp != null)
                            {
                                MessageBox.Show(Resources.msgDateoutProcess + vechileProp.Name + " IN: " + vechileProp.DateIn + " REQ: " + vechileProp.DateRequested + " OUT:" + vechileProp.DateOut + "", Resources.msgTitleMessageBoxVehicleNotFound);
                                ClearFiled();
                                return;
                            }
                        }
                        else
                        {
                            objVehicleProp.UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                            objVehicleProp.UpdatedDate = DateTime.Now;
                            objVehicleProp.DateRequested = dtRequestDate;
                            objVehicleProp.Note = note;
                            var vehicle = pendingVehicles.FirstOrDefault();
                            if (vehicle != null)
                            {
                                objVehicleProp.VehiclesID = vehicle.VehiclesID;
                            }
                            if (IsReturnedToInventory)
                            {
                                objVehicleProp.EstimatedPickupDate = objVehicleProp.DealerPrintDate = null;
                                objVehicleProp.RequestedBy = null;
                            }
                            int value = _serviceInstance.UpdatePortStorageProcessignDetails(objVehicleProp);
                            if (value > 0)
                            {
                                var data = _serviceInstance.GetPortStorageProcessignDetails(objVehicleProp.VehiclesID.ToString()).Select(d => new VehicleModel
                                {
                                    Vin = d.Vin,
                                    Name = d.Name,
                                    DateIn = d.DateIn,
                                    DateRequested = objVehicleProp.DateRequested,
                                    MakeModel = d.MakeModel,
                                    BayLocation = d.BayLocation,
                                    VehicleStatus = d.VehicleStatus,
                                    VehiclesID = d.VehiclesID
                                });
                                VehicleModel objVehicleModel = new VehicleModel();
                                objVehicleModel = data.FirstOrDefault(x => x.VehiclesID == objVehicleProp.VehiclesID);
                                RequestProcessingListTemp.Add(objVehicleModel);
                                RequestProcessingList = new ObservableCollection<VehicleModel>(RequestProcessingListTemp);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Resources.msgVINReq, Resources.msgTitleMessageBox);
                }
                Count = RequestProcessingList.Count;
                ClearFiled();

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
        /// Function UpdatePortStorageRequestDateForBatchProcess to update the vehicle details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-11,2016</createdOn>
        public void UpdatePortStorageRequestDateForBatchProcess(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                MyReportSource = null;
                var report = new PrintRequestRPT();
                //var report = new VehicleRequestRPT();
                VehicleProp objVehicleProp = new VehicleProp();
                List<PrintRequestReport> lstRequestProcessingPrintModel = new List<PrintRequestReport>();
                if (RequestProcessingList != null)
                {
                    RequestProcessingListTemp.Clear();

                    foreach (var item in RequestProcessingList)
                    {
                        objVehicleProp.Vin = item.Vin;
                        objVehicleProp.DateRequested = item.DateRequested;
                        objVehicleProp.VehiclesID = item.VehiclesID;
                        objVehicleProp.DealerPrintDate = item.DealerPrintDate;
                        objVehicleProp.EstimatedPickupDate = item.EstimatedPickupDate;
                        objVehicleProp.UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                        string message = _serviceInstance.RequestBatchProcess(objVehicleProp);
                        if (!string.IsNullOrEmpty(message))
                        {
                            //var data = _serviceInstance.GetPortStorageProcessignDetails(objVehicleProp.Vin).Select(d => new VehicleModel//instead of passing vin we will pass vehicleId
                            var data = _serviceInstance.GetPortStorageProcessignDetails(objVehicleProp.VehiclesID.ToString()).Select(d => new VehicleModel
                            {
                                VehiclesID = d.VehiclesID,
                                Vin = d.Vin,
                                Name = d.Name,
                                DateIn = d.DateIn,
                                DateRequested = d.DateRequested,
                                MakeModel = d.MakeModel,
                                Year = d.Year,
                                Make = d.Make,
                                BayLocation = d.BayLocation,
                                VehicleStatus = message,
                                Color = d.Color,
                                EstimatedPickupDate = d.EstimatedPickupDate,
                                DealerPrintDate = d.DealerPrintDate,
                                AdddressLine1 = d.AdddressLine1,
                                City = d.City,
                                State = d.State,
                                Zip = d.Zip,
                                IsProcessedRequestedOut = message.Equals(RequestedVehicleStatus.Updated)
                            });
                            VehicleModel objVehicleModel = new VehicleModel();
                            objVehicleModel = data.FirstOrDefault();
                            RequestProcessingListTemp.Add(objVehicleModel);
                            if ((bool)objVehicleModel.IsProcessedRequestedOut)
                            {
                                lstRequestProcessingPrintModel.Add(new PrintRequestReport()
                                {
                                    Vin = objVehicleModel.Vin,
                                    ShortVIN = objVehicleModel.Vin != "" && objVehicleModel.Vin != null ? "*" + objVehicleModel.Vin.Substring(objVehicleModel.Vin.Length - Math.Min(8, objVehicleModel.Vin.Length)) + "*" : "",
                                    BayLocation = objVehicleModel.BayLocation,
                                    DateRequested = objVehicleModel.DateRequested,
                                    Color = objVehicleModel.Color,
                                    PickeupDate = objVehicleModel.EstimatedPickupDate,
                                    MakeModel = objVehicleModel.Year + " " + objVehicleModel.Make + " " + objVehicleModel.MakeModel,
                                    DealerPrintDate = objVehicleModel.DealerPrintDate,
                                    CompanyName = CompanyName,
                                    CompanyAddressLine1 = CompanyAddressLine1,
                                    CompanyCity = CompanyCity,
                                    Phone = Phone,
                                    AdddressLine1 = objVehicleModel.AdddressLine1,
                                    Name = objVehicleModel.Name,
                                    FullAddress = (!string.IsNullOrEmpty(objVehicleModel.City) ? objVehicleModel.City + ", " : "") + objVehicleModel.State + " " + objVehicleModel.Zip,
                                    DayName = objVehicleModel.DateRequested != null ? objVehicleModel.DateRequested.Value.DayOfWeek.ToString() : "",
                                    ReturnToInventory = objVehicleModel.DateRequested != null ? objVehicleModel.DateRequested.Value.AddDays(5) : objVehicleModel.DateRequested,
                                    DayNamePickUp = objVehicleModel.EstimatedPickupDate != null ? objVehicleModel.EstimatedPickupDate.Value.DayOfWeek.ToString() : ""
                                });
                            }
                        }
                    }
                    RequestProcessingList = new ObservableCollection<VehicleModel>(RequestProcessingListTemp);
                }
                var dk = lstRequestProcessingPrintModel.ToList();
                report.DataSource = dk;
                MyReportSource = report;

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
        /// Function GetVecheleDetailByVIN to get the vehicle details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        private VehicleProp GetVecheleDetailByVIN(string VIN, int? vehicleId)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            VehicleProp result = new VehicleProp(); // create object to call vehicle property
            try
            {
                result = _serviceInstance.GetVecheleDetailByVIN(VIN, vehicleId);
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
        /// Function GetVecheleDetailByVIN to get the vehicle details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        public List<VehicleProp> CheckMultipleVecheleDetailByVIN(string VIN)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            List<VehicleProp> result = new List<VehicleProp>();
            try
            {
                result = _serviceInstance.CheckMultipleVecheleDetailByVIN(VIN).ToList();
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
        /// Function OpenPrintDialog to open the print window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-11,2016</createdOn>
        public void OpenPrintDialog(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                //display the print popup;
                if (MyReportSource != null)
                {
                    PrintRequestReportWindow objPrint = new PrintRequestReportWindow(MyReportSource);
                    objPrint.Owner = Application.Current.MainWindow;
                    var vinsToPrint = RequestProcessingListTemp.Where(x => (bool)x.IsProcessedRequestedOut).Select(s => s.VehiclesID.ToString()).ToArray();
                    var isUpdated = _serviceInstance.UpdateRequestPrintIndexForVehicles(vinsToPrint);
                    if (isUpdated)
                    { objPrint.ShowDialog(); }
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
        /// This function is used to clear all field in form
        /// </summary>
        private void ClearFiled()
        {
            Vin = string.Empty;
            Note = string.Empty;
        }

        /// <summary>
        /// This function used to Checkbox for ReturnToInventory
        /// </summary>
        /// <param name="obj"></param>
        private void ReturnToInventory(object obj)
        {
            DtRequestDate = IsReturnedToInventory ? (DateTime?)null : DateTime.Now;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DelegateEventDateAndVehicleProcessing.OnSetValueEvt -= new DelegateEventDateAndVehicleProcessing.SetValue(Executecommand);
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
