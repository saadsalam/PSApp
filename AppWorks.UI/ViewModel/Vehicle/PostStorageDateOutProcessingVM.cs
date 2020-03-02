using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppWorks.UI.Common;
using AppWorks.UI.Models;
using System.Reflection;
using System.Globalization;
using AppWorks.UI.Model;
using AppWorks.UI.View.Print;
using AppWorksService.Properties;
using AppWorks.UI.Properties;
using AppWorks.UI.View.UserControls.Vehicle;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class PostStorageDateOutProcessingVM : ViewModelBase, IDisposable
    {
        public PostStorageDateOutProcessingVM()
        {
            DelegateEventDateAndVehicleProcessing.OnSetValueEvt += new DelegateEventDateAndVehicleProcessing.SetValue(SetCommand);
        }

        private void SetCommand(string value)
        {
            //on pasting vin
            if (value.Equals("ScanVin"))
            {
                UpdateAndGetPortStorageDateOutProcessignDetails(null);
            }
        }



        #region "Page Properties"
        /// <summary>
        /// Property for Scrolled
        /// </summary>
        private string _scrolled;
        public string Scrolled
        {
            get
            {
                return _scrolled;
            }
            set
            {
                this._scrolled = value;
                NotifyPropertyChanged("Scrolled");
            }
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
        private DateTime dtRequestDate = DateTime.Now;
        public DateTime DtRequestDate
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
        public ObservableCollection<VehicleModel> RequestProcessingList { get; } = new ObservableCollection<VehicleModel>();
        #endregion

        #region "Page Events"

        private AppWorxCommand btnSubmit_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnSubmit_Click
        {
            get
            {
                if (btnSubmit_Click == null)
                {
                    btnSubmit_Click = new AppWorxCommand(this.UpdateAndGetPortStorageDateOutProcessignDetails);
                }
                return btnSubmit_Click;
            }
        }

        private AppWorxCommand btnCancel_Click;
        /// <summary>
        /// Clear button event
        /// </summary>
        public AppWorxCommand BtnCancel_Click
        {
            get
            {
                if (btnCancel_Click == null)
                {
                    btnCancel_Click = new AppWorxCommand(this.Cancle);
                }
                return btnCancel_Click;
            }
        }

        private AppWorxCommand btnClearFinished_Click;
        /// <summary>
        /// Clear Processed button event
        /// </summary>
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

        private AppWorxCommand btnViewSelected_Click;
        /// <summary>
        /// Clear Processed button event
        /// </summary>
        public AppWorxCommand BtnViewSelected_Click
        {
            get
            {
                if (btnViewSelected_Click == null)
                {
                    btnViewSelected_Click = new AppWorxCommand(this.ViewSelected);
                }
                return btnViewSelected_Click;
            }
        }

        //Clear the finished Records from the Grid
        private void ClearFinished(object obj)
        {
            RequestProcessingList.Clear();
            //VehicleStatus
        }

        //View the Selected Vehicle
        private void ViewSelected(object obj)
        {
            if (RequestProcessingList != null)
            {
                var vehicleModel = RequestProcessingList.FirstOrDefault();
                var home = new HomeWindowVM();
                var vehicleLocator = new VehicleLocatorVM();
                var vehicleDetails = new VehicleDetail();
                var window = new Window()
                {
                    Content = vehicleDetails
                };
                window.DataContext = vehicleLocator.VehicalDetailsViewModel;
                vehicleLocator.Vin = vehicleModel.Vin;
                vehicleLocator.VehicleId = vehicleModel.VehiclesID;
                vehicleLocator.CustomerID = vehicleModel.CustomerID;
                vehicleLocator.CreatedBy = vehicleModel.CreatedBy;
                vehicleLocator.SelectedItems = new ObservableCollection<object>() { vehicleLocator };
                var list = new List<VehicleLocatorVM>() { vehicleLocator };
                DelegateEventVehicle.SetValueListMethod(list);
                DelegateEventVehicle.SetValueMethodTabEnable(false);
                DelegateEventVehicle.SetValueMethodCmd("Edit");
                window.Show();
            }
        }

        private AppWorxCommand btnProcessBatch_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnProcessBatch_Click
        {
            get
            {
                if (btnProcessBatch_Click == null)
                {
                    btnProcessBatch_Click = new AppWorxCommand(this.UpdatePortStorageDateOutBatchProcess);
                }
                return btnProcessBatch_Click;
            }
        }

        private AppWorxCommand btnPrint_Click;
        /// <summary>
        /// Print button event
        /// </summary>
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
        #endregion

        #region "Page Functions and Methods"
        /// <summary>
        /// Function Cancle to refresh the page state
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-11,2016</createdOn>
        public void Cancle(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //empty the grid                
                RequestProcessingList.Clear();
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI) { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function UpdatePortStorageDateOutProcessignDetails to get the vehicle details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-11,2016</createdOn>
        public void UpdateAndGetPortStorageDateOutProcessignDetails(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (string.IsNullOrEmpty(Vin))
                {
                    MessageBox.Show(Resources.msgVINReq, Resources.msgTitleMessageBox);
                    return;
                }


                // Cannot allow multiples.
                if (RequestProcessingList.Any(it => it.Vin.Contains(Vin)))
                {
                    MessageBox.Show(Resources.msgVehicleListAlready, Resources.msgTitleMessageBoxVehicleList);
                    ClearFiled();
                    return;
                }

                // If there are no vehicles matching VIN, alert, and end.
                List<VehicleProp> vehiclesMatchingVIN = CheckMultipleVecheleDetailByVIN(Vin.ToUpper());
                if (vehiclesMatchingVIN.Any() == false)
                {
                    MessageBox.Show(Resources.msgProcessingVINFound, Resources.msgTitleMessageBoxVehicleNotFound);
                    return;
                }

                // Create LINQ statement grabbing first two vehicles matching VIN that do not have date out.
                string shortVin = Vin.Substring(0, 8);
                IEnumerable<VehicleProp> firstTwoVehiclesWithoutDateOut = vehiclesMatchingVIN.Where(x => x.DateOut == null).Take(2);

                // If there are no vehicles that have no date out, alert with information on vehicle that does, and end.
                if (firstTwoVehiclesWithoutDateOut.Any() == false)
                {
                    VehicleProp vehicleFound = GetVecheleDetailByVIN(shortVin);
                    MessageBox.Show($"{Resources.msgDateoutProcess + vehicleFound.Name} IN: {vehicleFound.DateIn} REQ: {vehicleFound.DateRequested} OUT: {vehicleFound.DateOut}", "Vehicle Not Found");
                    ClearFiled();
                    return;
                }

                // If there are multiple vehicles matching VIN with no date out, alert, and end.
                if (firstTwoVehiclesWithoutDateOut.Count() == 2)
                {
                    MessageBox.Show(Resources.msgMultipleVINProcessing, Resources.msgTitleMessageBoxMultipleVIN);
                    return;
                }

                // Update vehicle details to make date out now.
                var updateVehicle = new VehicleProp
                {
                    VehiclesID = firstTwoVehiclesWithoutDateOut.First().VehiclesID,
                    Vin = Vin.Substring(0, 8),
                    DateOut = DtDateOut,
                    Note = Note,
                };
                int value = _serviceInstance.UpdatePortStorageDateOutProcessignDetails(updateVehicle);

                // If update was successful, add vehicle to list for display on grid.
                if (value > 0)
                {
                    VehicleProp foundVehicle = _serviceInstance.GetPortStorageDateOutProcessignDetails(shortVin).First();
                    VehicleModel vehicleModel = new VehicleModel
                    {
                        Vin = foundVehicle.Vin,
                        Name = foundVehicle.Name,
                        DateIn = foundVehicle.DateIn,
                        DateOut = DtDateOut,
                        MakeModel = foundVehicle.MakeModel,
                        VehiclesID = foundVehicle.VehiclesID,
                        BayLocation = foundVehicle.BayLocation,
                        VehicleStatus = foundVehicle.VehicleStatus,
                    };
                    RequestProcessingList.Add(vehicleModel);
                }

                ClearFiled();
                Scrolled = "NOTSCROLLED";

            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI) { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            }
        }

        /// <summary>
        /// Function UpdatePortStorageDateOut to update the vehicle details to database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-11,2016</createdOn>
        public void UpdatePortStorageDateOutBatchProcess(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                VehicleProp objVehicleProp = new VehicleProp();

                foreach (var item in RequestProcessingList)
                {
                    objVehicleProp.Vin = item.Vin;
                    objVehicleProp.DateOut = item.DateOut;
                    objVehicleProp.VehiclesID = item.VehiclesID;
                    objVehicleProp.UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                    string message = _serviceInstance.DateOutBatchProcess(objVehicleProp);
                    if (!string.IsNullOrEmpty(message))
                    {
                        var data = _serviceInstance.GetPortStorageDateOutProcessignDetails(objVehicleProp.Vin).Select(d => new VehicleModel
                        {
                            Vin = d.Vin,
                            Name = d.Name,
                            DateIn = d.DateIn,
                            DateOut = d.DateOut,
                            MakeModel = d.MakeModel,
                            BayLocation = d.BayLocation,
                            VehicleStatus = message,
                        });
                        VehicleModel objVehicleModel = new VehicleModel();
                        objVehicleModel = data.FirstOrDefault();
                        RequestProcessingList.Add(objVehicleModel);
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI) { throw; }
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
        public List<VehicleProp> CheckMultipleVecheleDetailByVIN(string VIN)
        {
            List<VehicleProp> result = new List<VehicleProp>();

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = _serviceInstance.CheckMultipleVecheleDetailByVIN(VIN).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI) { throw; }
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
                DateoutProcessingModel objPrinModel = new DateoutProcessingModel();
                objPrinModel.Vin = Vin;
                objPrinModel.VehiclesID = string.Empty;
                objPrinModel.BayLocation = BayLocation;
                PrintWindow objPrint = new PrintWindow(objPrinModel);
                objPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI) { throw; }
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
        /// Function GetVecheleDetailByVIN to get the vehicle details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-7,2016</createdOn>
        private VehicleProp GetVecheleDetailByVIN(string VIN)
        {
            VehicleProp result = new VehicleProp();
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                result = _serviceInstance.GetVecheleDetailByVIN(VIN, null);
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI) { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return result;
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
                    DelegateEventDateAndVehicleProcessing.OnSetValueEvt -= new DelegateEventDateAndVehicleProcessing.SetValue(SetCommand);
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
