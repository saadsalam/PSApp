using AppWorks.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorksService.Properties;
using System.Globalization;
using System.Reflection;
using System.Collections;
using System.Windows;
using AppWorks.UI.Properties;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.ObjectModel;
using Telerik.Reporting.Drawing;
using System.Windows.Input;
using System.IO;

namespace AppWorks.UI.ViewModel.WebPortal
{
    public class VehicleInventoryVM : ViewModelBase
    {
        int _GridPageSize = 30;
        int _GridPageSizeOnScroll = Convert.ToInt32(ConfigurationManager.AppSettings["FindUserGridPageSizeOnScroll"]);

        public VehicleInventoryVM()
        {
            FillCustomers(0);
            Search_Click(null);
            WebPortalDelegate.OnSetValueEvtTotalCountPagerCmd += new WebPortalDelegate.SetValueTotalCountPager(GetTotalPageCount);
            WebPortalDelegate.OnSetValuePageNumberCmd += new WebPortalDelegate.SetValuePageNumberClick(GetCurrentPageIndex);
        }
        #region WebPortal Properties

        private long totalPageCount;
        public long TotalPageCount
        {
            get { return totalPageCount; }
            set
            {
                this.totalPageCount = value;
                NotifyPropertyChanged("TotalPageCount");

            }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                this.selectedIndex = value;
                NotifyPropertyChanged("selectedIndex");

            }
        }

        private int currentPageIndex;
        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                this.currentPageIndex = value;
                NotifyPropertyChanged("CurrentPageIndex");

            }
        }

        private bool? isSelected;
        public bool? IsSelected
        {
            get { return isSelected; }
            set
            {
                this.isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        private string vINNumber;
        public string VINNumber
        {
            get { return vINNumber; }
            set
            {
                this.vINNumber = value;
                NotifyPropertyChanged("VINNumber");
            }
        }

        private int? customerID;
        public int? CustomerID
        {
            get { return customerID; }
            set
            {
                this.customerID = value;
                NotifyPropertyChanged("CustomerID");
            }
        }
        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                this.customerName = value;
                NotifyPropertyChanged("CustomerName");
            }
        }
        private string year;
        public string Year
        {
            get { return year; }
            set
            {
                this.year = value;
                NotifyPropertyChanged("Year");
            }
        }
        private string make;
        public string Make
        {
            get { return make; }
            set
            {
                this.make = value;
                NotifyPropertyChanged("make");
            }
        }
        private string model;
        public string Model
        {
            get { return model; }
            set
            {
                this.model = value;
                NotifyPropertyChanged("Model");
            }
        }

        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                this.color = value;
                NotifyPropertyChanged("Color");
            }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                this.location = value;
                NotifyPropertyChanged("Location");
            }
        }

        private DateTime? dateIn;
        public DateTime? DateIn
        {
            get { return dateIn; }
            set
            {
                this.dateIn = value;
                NotifyPropertyChanged("DateIn");
            }
        }
        private int? days;
        public int? Days
        {
            get { return days; }
            set
            {
                this.days = value;
                NotifyPropertyChanged("Days");
            }
        }


        private DateTime? dateRequested;
        public DateTime? DateRequested
        {
            get { return dateRequested; }
            set
            {
                this.dateRequested = value;
                NotifyPropertyChanged("DateRequested");
            }
        }


        private string vehicleStatus;
        public string VehicleStatus
        {
            get { return vehicleStatus; }
            set
            {
                this.vehicleStatus = value;
                NotifyPropertyChanged("VehicleStatus");
            }
        }

        private string requestedBy;
        public string RequestedBy
        {
            get { return requestedBy; }
            set
            {
                this.requestedBy = value;
                NotifyPropertyChanged("RequestedBy");
            }
        }

        private string rowColor;
        public string RowColor
        {
            get { return rowColor; }
            set
            {
                this.rowColor = value;
                NotifyPropertyChanged("RowColor");
            }
        }

        private string vehicleSummary;
        public string VehicleSummary
        {
            get { return vehicleSummary; }
            set
            {
                this.vehicleSummary = value;
                NotifyPropertyChanged("VehicleSummary");
            }
        }

        private DateTime? dateOut;
        public DateTime? DateOut
        {
            get { return dateOut; }
            set
            {
                this.dateOut = value;
                NotifyPropertyChanged("DateOut");
            }
        }
        private List<UserCustomerList> listCustomer;
        public List<UserCustomerList> ListCustomer
        {
            get
            {
                return listCustomer;
            }
            set
            {
                this.listCustomer = value;
                NotifyPropertyChanged("ListCustomer");
            }
        }

        private ObservableCollection<PortStorageVehicleProp> listInventory;
        public ObservableCollection<PortStorageVehicleProp> ListInventory
        {
            get
            {
                return listInventory;
            }
            set
            {
                this.listInventory = value;
                NotifyPropertyChanged("ListInventory");
            }
        }
        private ObservableCollection<PortStorageVehicleProp> listExport;
        public ObservableCollection<PortStorageVehicleProp> ListExport
        {
            get
            {
                return listExport;
            }
            set
            {
                this.listExport = value;
                NotifyPropertyChanged("ListExport");
            }
        }
        private List<PortStorageVehicleProp> listInventorySelected;
        public List<PortStorageVehicleProp> ListInventorySelected
        {
            get
            {
                return listInventorySelected;
            }
            set
            {
                this.listInventorySelected = value;
                NotifyPropertyChanged("ListInventorySelected");
            }
        }
        private AppWorxCommand btnSearchClick;
        public AppWorxCommand BtnSearchClick
        {
            get
            {
                if (btnSearchClick == null)
                {
                    btnSearchClick = new AppWorxCommand(this.Search_Click);
                }
                return btnSearchClick;
            }
        }

        private AppWorxCommand btnRequestCheckedClick;
        public AppWorxCommand BtnRequestCheckedClick
        {
            get
            {
                if (btnRequestCheckedClick == null)
                {
                    btnRequestCheckedClick = new AppWorxCommand(this.RequestChecked_Click);
                }
                return btnRequestCheckedClick;
            }
        }

        private ICommand btnExport_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnExport_Click
        {
            get
            {
                if (btnExport_Click == null)
                {
                    btnExport_Click = new AppWorxCommand(
                        param => this.CreateCSVFile(),
                        param => CanCheck
                        );
                }
                return btnExport_Click;
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

        /// <summary>
        /// Get Total page count for grid
        /// </summary>
        /// 
        public void GetTotalPageCount(long totalPageCount)
        {
            TotalPageCount = totalPageCount;
        }

        public void GetCurrentPageIndex(int currentPageIndx)
        {
            CurrentPageIndex = currentPageIndx;
            Search_Click("GridScroled");
        }


        /// <summary>
        /// To get the Customers for a particular user
        /// </summary>
        /// <param name="userID"></param>
        public void FillCustomers(int userID)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                var qry = _serviceInstance.GetCustomers(userID).Select(d => new UserCustomerList
                {
                    CustomerName = d.CustomerName,
                    CustomerID = d.CustomerID,
                });

                foreach (var item in qry)
                {
                    CustomerID = item.CustomerID;
                    CustomerName = item.CustomerName;
                    break;
                }

                ListCustomer = new List<UserCustomerList>(qry);

            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to get inventory details for a vin number/customerid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>June-28,2016</createdOn>
        public void Search_Click(object obj)
        {
            try
            {

                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                Count = 0;
                if (CustomerID > 0)
                {
                    if (obj == null)
                    {
                        CurrentPageIndex = 0;
                    }
                    if (CurrentPageIndex == 0)
                    {
                        Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
                        Application.Current.Properties["FindUserGridCurrentPageIndex"] = 0;
                    }

                    PortStorageVehicleProp objVehicleProp = new PortStorageVehicleProp();
                    objVehicleProp.CustomerID = CustomerID;
                    objVehicleProp.VIN = VINNumber;
                    objVehicleProp.CurrentPageIndex = CurrentPageIndex;
                    objVehicleProp.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize; ;
                    objVehicleProp.DefaultPageSize = _GridPageSize;

                    var lstInventory = _serviceInstance.GetPortStorageInventoryDetails(objVehicleProp).Select(x => new PortStorageVehicleProp
                    {
                        VIN = x.VIN != null ? x.VIN : string.Empty,
                        VehicleYear = x.VehicleYear != null ? x.VehicleYear : string.Empty,
                        Make = x.Make != null ? x.Make : string.Empty,
                        Model = x.Model != null ? x.Model : string.Empty,
                        Color = x.Color != null ? x.Color : string.Empty,
                        BayLocation = x.BayLocation != null ? x.BayLocation : string.Empty,
                        DateIn = x.DateIn,
                        DateRequested = x.DateRequested,
                        VehicleStatus = x.VehicleStatus != null ? x.VehicleStatus : string.Empty,
                        RequestedBy = x.RequestedBy != null ? x.RequestedBy : string.Empty,
                        DateOut = x.DateOut,
                        PortStorageVehiclesID = x.PortStorageVehiclesID,
                        IsSelected = x.IsSelected,
                        Days = x.DateOut != null ? Convert.ToInt32(Convert.ToDateTime(x.DateOut).Subtract((DateTime)x.DateIn).TotalDays) : (x.DateIn != null ? Convert.ToInt32(DateTime.Now.Subtract((DateTime)x.DateIn).TotalDays) : 0), //(int)x.DateOut.Value.Subtract((DateTime)x.DateIn).TotalDays : 0//(Convert.ToDateTime(x.DateIn) != null ? (int)DateTime.Now.Subtract((DateTime)x.DateIn).TotalDays : 0)
                        TotalPageCount = x.TotalPageCount
                    });

                    foreach (var item in lstInventory)
                    {
                        IsSelected = item.IsSelected;
                    }

                    if (CurrentPageIndex == 0)
                    {
                        ListInventory = null;
                        ListInventory = new ObservableCollection<PortStorageVehicleProp>(lstInventory);
                    }
                    else
                    {
                        if (ListInventory != null && ListInventory.Count > 0)
                        {
                            foreach (PortStorageVehicleProp ud in new ObservableCollection<PortStorageVehicleProp>(lstInventory))
                            {
                                ListInventory.Add(ud);
                            }
                        }
                    }
                    VehicleSummary = "Vehicles currently in storage: " + ListInventory.Where(x => x.VehicleStatus != Enums.VehicleStatusType.OutGated.ToString()).Count().ToString() + "(Requested: " + ListInventory.Where(x => x.VehicleStatus == Enums.VehicleStatusType.Requested.ToString()).Count().ToString() + ")";
                }
                else
                { MessageBox.Show(Resources.MsgSelectUser); }

                Count = ListInventory.ToList().Where(x => x.TotalPageCount > 0).FirstOrDefault().TotalPageCount;
            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }


        /// <summary>
        /// Function to update vehiclestatus for checked rows
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>June-28,2016</createdOn>
        public void RequestChecked_Click(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                string user = Application.Current.Properties["LoggedInUserName"].ToString();

                listInventorySelected = ListInventory.Where(x => x.IsSelected == true).ToList();
                if (listInventorySelected.Count() > 0)
                {
                    bool isSuccessfull = _serviceInstance.UpdateRequestCheckedVehicles(ListInventorySelected.ToArray(), user);
                    if (isSuccessfull)
                    {
                        MessageBox.Show(Resources.MsgRecordUpdated);
                        Search_Click(null);
                    }
                }
                else
                { MessageBox.Show(Resources.MsgSelectUser); }

            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// function CreateCSVFile to return the exported csv file
        /// </summary>
        /// <param name="Na"></param>
        /// <param name="Na"></param>
        /// <returns>csv file</returns>
        public void CreateCSVFile()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                PortStorageVehicleProp objVehicleProp = new PortStorageVehicleProp();
                objVehicleProp.CustomerID = CustomerID;
                objVehicleProp.VIN = VINNumber;
                objVehicleProp.CurrentPageIndex = 0;
                objVehicleProp.PageSize = 0;
                objVehicleProp.DefaultPageSize = 0;

                var lstInventory = _serviceInstance.GetPortStorageInventoryDetails(objVehicleProp).Select(x => new PortStorageVehicleProp
                {
                    VIN = x.VIN,
                    VehicleYear = x.VehicleYear,
                    Make = x.Make,
                    Model = x.Model,
                    Color = x.Color,
                    BayLocation = x.BayLocation,
                    DateIn = x.DateIn,
                    DateRequested = x.DateRequested,
                    VehicleStatus = x.VehicleStatus,
                    RequestedBy = x.RequestedBy,
                    DateOut = x.DateOut,
                    PortStorageVehiclesID = x.PortStorageVehiclesID,
                    IsSelected = x.IsSelected,
                    Days = x.DateOut.Value != null ? (int)x.DateOut.Value.Subtract((DateTime)x.DateIn).TotalDays : (int)DateTime.Now.Subtract((DateTime)x.DateIn).TotalDays

                });

                ListExport = new ObservableCollection<PortStorageVehicleProp>(lstInventory);

                if (ListExport != null)
                {
                    if (ListExport.Count > 0)
                    {
                        string[] ExportValue = new string[] { "VIN", "Year", "Make", "Model", "Color", "Location", "Arrived", "Days", "Requested", "Status", "Requested By", "Date Out" };

                        if (!Directory.Exists(ConfigurationManager.AppSettings["CSVFilePath"].ToString()))
                        {
                            Directory.CreateDirectory(ConfigurationManager.AppSettings["CSVFilePath"].ToString());
                        }

                        StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "VehicleInventory.csv", false);

                        foreach (string objVal in ExportValue)
                        {
                            sw.Write(objVal);
                            sw.Write(",");
                        }
                        sw.Write(sw.NewLine);

                        // Row creation
                        foreach (PortStorageVehicleProp prop in ListExport)
                        {

                            if (prop.VIN != null)
                            {
                                sw.Write(prop.VIN);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.VehicleYear != null)
                            {
                                sw.Write(prop.VehicleYear);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.Make != null)
                            {
                                sw.Write(prop.Make);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.Model != null)
                            {
                                sw.Write(prop.Model);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.Color != null)
                            {
                                sw.Write(prop.Color);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.BayLocation != null)
                            {
                                sw.Write(prop.BayLocation);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.DateIn != null)
                            {
                                sw.Write(prop.DateIn.Value.Date.ToString("MM/dd/yyy"));
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.Days > 0)
                            {
                                sw.Write(prop.Days);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.DateRequested != null)
                            {
                                sw.Write(prop.DateRequested.Value.Date.ToString("MM/dd/yyy"));
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.VehicleStatus != null)
                            {
                                sw.Write(prop.VehicleStatus);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.RequestedBy != null)
                            {
                                sw.Write(prop.RequestedBy);
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }
                            if (prop.DateOut != null)
                            {
                                sw.Write(prop.DateOut.Value.Date.ToString("MM/dd/yyy"));
                                sw.Write(",");
                            }
                            else
                            {
                                sw.Write("");
                                sw.Write(",");
                            }

                            // Do something with propValue
                            sw.Write(sw.NewLine);
                        }
                        sw.Close();
                        System.Diagnostics.Process.Start(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "PortStorageVehicles.csv");
                    }
                }

            }
            catch (Exception ex)
            {
                CommonSettings.logger.LogError(this.GetType(), ex);
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            }

        }
    }


}
