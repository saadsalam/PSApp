using AppWorks.UI.Common;
using AppWorks.UI.Model;
using AppWorks.UI.Properties;
using AppWorks.UI.View.UserControls.Location;
using AppWorks.UI.ViewModel.CustomerAdmin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppWorks.UI.ViewModel.Location
{
    public class LocationVM : ViewModelBase, IDisposable
    {
        int gridPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]);
        int _GridPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]);
        int _GridPageSizeOnScroll = 10;
        public LocationVM()
        {
            // Implements Default Initialization Here.
            AppWorksService.Properties.LocationList objLocationProp = new AppWorksService.Properties.LocationList();
            objLocationProp.CurrentPageIndex = CurrentPageIndex;
            objLocationProp.PageSize = gridPageSize;
            if (Application.Current.Resources["CustomerAdminID"] != null)
                CustomerId = Convert.ToInt32(Application.Current.Resources["CustomerAdminID"]);
            objLocationProp.CustomerId = CustomerId;
            SearchLocationList(objLocationProp);
            AfterSearch(null);
            BindLocationType();
            CustomerAdminDeligate.OnSetCustomerAdminPropertiesValueEvt += new CustomerAdminDeligate.SetCustomerAdminPropertiesValue(NotificationCmdReceived);
            LocationDeligate.OnSetValueEvtUpdateCmd += new LocationDeligate.SetValueUpdateCmd(NotificationCmdReceivedToUpdate);
            LocationDeligate.OnSetValueEvtTotalCountPagerCmd += new LocationDeligate.SetValueTotalCountPager(GetTotalPageCount);
            LocationDeligate.OnSetValuePageNumberCmd += new LocationDeligate.SetValuePageNumberClick(GetCurrentPageIndex);
            // DisplayFindUserTab = true;
            // DisplayEditUserTab = false;
            // SeletedTabFinduser = 0;
            // this.GetRecordStatus();
            // this.GetAllRoles();
            //FindUserDeligate.OnSetValueEvtCmdReferesh += new FindUserDeligate.SetValueCmdReferesh(UsersRecordList);
            //FindUserDeligate.OnSetValueEvtTotalCountPagerCmd += new FindUserDeligate.SetValueTotalCountPager(GetTotalPageCount);
            //FindUserDeligate.OnSetValuePageNumberCmd += new FindUserDeligate.SetValuePageNumberClick(GetCurrentPageIndex);
        }
        private void NotificationCmdReceived(bool propValue)
        {
            IsReadOnly = propValue;
        }
        private void NotificationCmdReceivedToUpdate(string value)
        {
            if (value.Equals("UpdateList"))
            {
                SearchLocationList(null);
                AfterSearch(null);
            }
        }
        #region "Page Events"


        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                this.isReadOnly = value;
                NotifyPropertyChanged("IsReadOnly");
            }
        }

        private AppWorxCommand eBtnAddLocationClick;
        public AppWorxCommand BtnAddLocationClick
        {
            get
            {
                if (eBtnAddLocationClick == null)
                {
                    eBtnAddLocationClick = new AppWorxCommand(AddLocationPopup);
                }
                return eBtnAddLocationClick;
            }
        }


        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        private ICommand eBtnModifyLocationClick;
        public ICommand BtnModifyLocationClick
        {
            get
            {
                if (eBtnModifyLocationClick == null)
                {
                    eBtnModifyLocationClick = new AppWorxCommand(ModifyViewLocationPopup);
                }
                return eBtnModifyLocationClick;
            }
        }

        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        private AppWorxCommand eBtnImportClick;
        public AppWorxCommand BtnImportClick
        {
            get
            {
                if (eBtnImportClick == null)
                {
                    eBtnImportClick = new AppWorxCommand(this.ImportLocation);
                }
                return eBtnImportClick;
            }
        }

        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        private AppWorxCommand eBtnDeleteClick;
        public AppWorxCommand BtnDeleteClick
        {
            get
            {
                if (eBtnDeleteClick == null)
                {
                    eBtnDeleteClick = new AppWorxCommand(this.DeleteRecord);
                }
                return eBtnDeleteClick;
            }
        }

        /// <summary>
        /// for Checking parameter during command execution
        /// </summary>
        private AppWorxCommand eBtnSetBillingAddressClick;
        public AppWorxCommand BtnSetBillingAddressClick
        {
            get
            {
                if (eBtnSetBillingAddressClick == null)
                {
                    eBtnSetBillingAddressClick = new AppWorxCommand(this.SetBillingAddress);
                }
                return eBtnSetBillingAddressClick;
            }
        }

        /// <summary>
        /// ICommand Interface for Command Type  
        /// </summary>
        private AppWorxCommand eBtnSetStreetAddressClick;
        public AppWorxCommand BtnSetStreetAddressClick
        {
            get
            {
                if (eBtnSetStreetAddressClick == null)
                {
                    eBtnSetStreetAddressClick = new AppWorxCommand(this.SetStreetAddress);
                }
                return eBtnSetStreetAddressClick;
            }
        }

        /// <summary>
        /// for Checking parameter during command execution
        /// </summary>
        private AppWorxCommand eBtnExportClick;
        public AppWorxCommand BtnExportClick
        {
            get
            {
                if (eBtnExportClick == null)
                {

                    eBtnExportClick = new AppWorxCommand(this.ExportLocation);
                }
                return eBtnExportClick;
            }
        }

        /// <summary>
        /// for Checking parameter during command execution
        /// </summary>
        private AppWorxCommand eBtnReloadClick;
        public AppWorxCommand BtnReloadClick
        {
            get
            {
                if (eBtnReloadClick == null)
                {
                    eBtnReloadClick = new AppWorxCommand(this.SearchLocationList);
                }
                return eBtnReloadClick;
            }
        }

        #endregion



        #region "Page Properties"


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

        private LocationModel selectedItems;
        public LocationModel SelectedItems
        {
            get { return selectedItems; }
            set
            {
                selectedItems = value;
                NotifyPropertyChanged("SelectedItems");
            }
        }

        /// <summary>
        /// for holding and resturning the value of usercode
        /// </summary>
        private bool visibilityEditUserTab;
        public bool VisibilityEditUserTab
        {
            get { return visibilityEditUserTab; }
            set
            {
                this.visibilityEditUserTab = value;
                NotifyPropertyChanged("VisibilityEditUserTab");
            }
        }

        private string selectedLocationType;
        public string SelectedLocationType
        {
            get { return selectedLocationType; }
            set
            {
                if (value != null)
                { this.selectedLocationType = value; }
                NotifyPropertyChanged("SelectedLocationType");
            }
        }

        /// <summary>
        /// for holding and resturning the value of CustomerId
        /// </summary>
        private int customerId;
        public int CustomerId
        {
            get { return customerId; }
            set
            {
                if (value != null)
                { this.customerId = value; }
                NotifyPropertyChanged("CustomerId");
            }
        }


        private long totalPageCount;
        public long TotalPageCount
        {
            get { return totalPageCount; }
            set
            {
                if (value != null)
                {
                    this.totalPageCount = value;
                    NotifyPropertyChanged("TotalPageCount");
                }
            }
        }

        private int currentPageIndex;
        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                if (value != null)
                {
                    this.currentPageIndex = value;
                }
            }
        }
        private LocationModel selectedDisRecipientGridItem;
        public LocationModel SelectedDisRecipientGridItem
        {
            get { return selectedDisRecipientGridItem; }
            set { selectedDisRecipientGridItem = value; NotifyPropertyChanged("SelectedDisRecipientGridItem"); }
        }
        /// <summary>
        /// for holding and resturning the value of lastName
        /// </summary>
        private List<string> locationTypeList;
        public List<string> LocationTypeList
        {
            get { return locationTypeList; }
            private set
            {
                if (value != null)
                { this.locationTypeList = value; }
                NotifyPropertyChanged("LocationTypeList");
            }
        }


        private ObservableCollection<LocationModel> getLocationList;
        public ObservableCollection<LocationModel> GetLocationList
        {
            get { return getLocationList; }
            private set
            {
                if (value != null)
                {
                    this.getLocationList = value;
                    NotifyPropertyChanged("GetLocationList");
                }
            }
        }


        private int seletedTabFinduser;
        public int SeletedTabFinduser
        {
            get { return seletedTabFinduser; }
            set
            {
                if (value != null)
                {
                    this.seletedTabFinduser = value;
                    NotifyPropertyChanged("SeletedTabFinduser");
                }
            }
        }

        #endregion


        /// <summary>
        /// Method To show the All Record Status
        /// </summary>
        public void BindLocationType()
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                var locationTypeList = _serviceInstance.LoadCodeList(Convert.ToString(Enums.CodeType.LocationType)).Select(x => x.Description).ToList();
                List<string> List = new List<string>(locationTypeList);
                LocationTypeList = List;
                LocationTypeList.Insert(0, "All");
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
            SearchLocationList("GridScroled");
            //AfterSearch(null);
        }

        public void AfterSearch(object obj)
        {
            if (GetLocationList != null && GetLocationList.Count > 0)
            {
                LocationDeligate.SetValueMethodCmd("Add");
                var TotalPageCounttemp = GetLocationList.FirstOrDefault().TotalPageCount;
                LocationDeligate.SetValueMethodTotalCountPager(TotalPageCounttemp);
            }
        }
        /// <summary>
        /// Method To Show All Record
        /// </summary>
        public void SearchLocationList(object obj)
        {
            try
            {
                Count = 0;

                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (obj == null)
                {
                    CurrentPageIndex = 0;
                }
                if (CurrentPageIndex == 0)
                {
                    Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
                    Application.Current.Properties["FindUserGridCurrentPageIndex"] = 0;
                }

                AppWorksService.Properties.LocationList objLocationProp = new AppWorksService.Properties.LocationList();

                objLocationProp.CurrentPageIndex = CurrentPageIndex;
                objLocationProp.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize; ;
                objLocationProp.DefaultPageSize = _GridPageSize;

                objLocationProp.CurrentPageIndex = CurrentPageIndex;
                objLocationProp.PageSize = gridPageSize;
                CustomerId = Convert.ToInt32(Application.Current.Resources["CustomerAdminID"]);
                objLocationProp.CustomerId = CustomerId;
                if (SelectedLocationType != null && !SelectedLocationType.Equals("All"))
                {
                    objLocationProp.LocationType = SelectedLocationType;
                }

                var list = _serviceInstance.GetLocationList(objLocationProp).Select(d => new LocationModel
                {
                    CustomerLocationCode = d.CustomerLocationCode,
                    ParentRecordTable = d.ParentRecordTable,
                    LocationID = d.LocationID,
                    LocationName = d.LocationName,
                    LocationShortName = d.LocationShortName,
                    LocationType = d.LocationType,
                    LocationSubType = d.LocationSubType,
                    AddressLine1 = d.AddressLine1,
                    AddressLine2 = d.AddressLine2,
                    City = d.City,
                    State = d.State,
                    Zip = d.Zip,
                    Country = d.Country,
                    MainPhone = d.MainPhone,
                    PrimaryContactFirstName = d.PrimaryContactFirstName,
                    PrimaryContactLastName = d.PrimaryContactLastName,
                    PrimaryContactPhone = d.PrimaryContactPhone,
                    PrimaryContactExtension = d.PrimaryContactExtension,
                    PrimaryContactCellPhone = d.PrimaryContactCellPhone,
                    PrimaryContactEmail = d.PrimaryContactEmail,
                    AlternateContactFirstName = d.AlternateContactFirstName,
                    AlternateContactLastName = d.AlternateContactLastName,
                    AlternateContactPhone = d.AlternateContactPhone,
                    AlternateContactExtension = d.AlternateContactExtension,
                    AlternateContactCellPhone = d.AlternateContactCellPhone,
                    AlternateContactEmail = d.AlternateContactEmail,
                    RecordStatus = d.RecordStatus,
                    CreatedBy = d.CreatedBy,
                    CreationDate = d.CreationDate,
                    UpdatedBy = d.UpdatedBy,
                    UpdatedDate = d.UpdatedDate,
                    OtherPhone1Description = d.OtherPhone1Description,
                    OtherPhone1 = d.OtherPhone1,
                    OtherPhone2Description = d.OtherPhone2Description,
                    OtherPhone2 = d.OtherPhone2,

                    TotalPageCount = d.TotalPageCount
                });

                if (CurrentPageIndex == 0)
                {
                    GetLocationList = null;
                    GetLocationList = new ObservableCollection<LocationModel>(list);
                }
                else
                {
                    if (GetLocationList != null && GetLocationList.Count > 0)
                    {
                        foreach (LocationModel ud in new ObservableCollection<LocationModel>(list))
                        {
                            GetLocationList.Add(ud);
                        }
                    }
                }
                Count = getLocationList.ToList().Where(x => x.TotalPageCount > 0).FirstOrDefault().TotalPageCount;
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

        private GridViewColumn columninfo;
        public GridViewColumn ColumnInfo
        {
            get { return columninfo; }
            set
            {
                if (value != null)
                {
                    this.columninfo = value;
                    NotifyPropertyChanged("ColumnInfo");
                }
            }
        }


        /// <summary>
        /// Function DeleteRecord to delete the location details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        public void DeleteRecord(object obj)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(Resources.MsgDeleteConfirm, Resources.msgTitleMessageBoxDelete, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    int locationId = 0;
                    bool value = _serviceInstance.DeleteLocation(locationId);
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


        /// <summary>
        /// Function AddLocationPopup to open the customer locator window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-27,2016</createdOn>
        public void AddLocationPopup(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //LocationModel objget = new LocationModel();
                //objget = (LocationModel)SelectedDisRecipientGridItem;

                //AddLocation objAddLocation = new AddLocation();
                //LocationDeligate.SetValueLocationMethod(objget);
                //display the add location popup;
                AddLocation objAddLocation = new AddLocation();
                objAddLocation.Owner = Application.Current.MainWindow;
                LocationDeligate.SetValueMethodCmd("Add");
                objAddLocation.ShowDialog();
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
        /// Function ModifyViewLocationPopup allows to modify to locatoion details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-30,2016</createdOn>
        public void ModifyViewLocationPopup(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (SelectedDisRecipientGridItem != null)
                {
                    //display the add location popup;
                    LocationModel location = new LocationModel();
                    location = (LocationModel)SelectedDisRecipientGridItem;

                    AddLocation objAddLocation = new AddLocation();
                    LocationDeligate.SetValueLocationMethod(location);
                    LocationDeligate.SetValueMethodCmd("Edit");

                    objAddLocation.ShowDialog();
                    CustomerAdminDeligate.SetUpdateAddressMethod("Update");
                }
                else
                    MessageBox.Show(Resources.msgSelectRecord);

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
        /// Function SetBillingAddress to set the location details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-30,2016</createdOn>
        public void SetBillingAddress(object obj)
        {
            if (SelectedDisRecipientGridItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(Resources.msgChangeBillingAddConfirm, Resources.msgChangeBillAddressTitleBox, MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    string errMsg = string.Empty;
                    try
                    {
                        LocationModel location = new LocationModel();
                        location = (LocationModel)SelectedDisRecipientGridItem;
                        int customerID = 0;

                        if (Application.Current.Resources["CustomerAdminID"] != null)
                        { customerID = Convert.ToInt32(Application.Current.Resources["CustomerAdminID"]); }

                        CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                        LocationModel objLocation = new LocationModel();
                        bool isSucceeded = _serviceInstance.UpdateCustomerAddredss("BILLINGADDRESS", customerID, location.LocationID);
                        if (isSucceeded)
                        {
                            MessageBox.Show("Billing Address Updated Successfully");
                            CustomerAdminDeligate.SetUpdateAddressMethod("Update");
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
            else
                MessageBox.Show(Resources.msgSelectRecord);
        }

        /// <summary>
        /// Function SetStreetAddress to set the location details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-30,2016</createdOn>
        public void SetStreetAddress(object obj)
        {
            if (SelectedDisRecipientGridItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(Resources.msgChangeStreetAddConfirm, Resources.msgChangeStreetAddressTitleBox, MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    string errMsg = string.Empty;
                    try
                    {
                        var location = new LocationModel();
                        location = (LocationModel)SelectedDisRecipientGridItem;
                        int customerID = 0;

                        if (Application.Current.Resources["CustomerAdminID"] != null)
                        { customerID = Convert.ToInt32(Application.Current.Resources["CustomerAdminID"]); }

                        CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                        var objLocation = new LocationModel();
                        bool isSucceed = _serviceInstance.UpdateCustomerAddredss("MAINADDRESS", customerID, location.LocationID);
                        if (isSucceed)
                        {
                            MessageBox.Show("Street Address Updated Successfully");
                            CustomerAdminDeligate.SetUpdateAddressMethod("Update");
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
            else
            { MessageBox.Show(Resources.msgSelectRecord); }
        }


        /// <summary>
        /// Function ExportLocation to export the location details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-30,2016</createdOn>
        public void ExportLocation(object obj)
        {
            //string errMsg = string.Empty;
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                AppWorksService.Properties.LocationList objLocationProp = new AppWorksService.Properties.LocationList();


                if (GetLocationList != null)
                {
                    if (GetLocationList.Count > 0)
                    {
                        string[] ExportValue = new string[] { "Location Name", "Short Name", "Address Line 1", "Address Line 2", "City", "State", "Zip", "Cust Loc Code", "Main Phone", "Fax", "Primary Contact", "Primary Contact Phone", "Primary Contact Ext", "Delivery Times" };
                        if (!Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString()))
                        {
                            Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString());
                        }
                        using (StreamWriter sw = new StreamWriter(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "Locations.csv", false))
                        {

                            foreach (string objVal in ExportValue)
                            {
                                sw.Write(objVal);
                                sw.Write(",");
                            }
                            sw.Write(sw.NewLine);

                            // Row creation
                            foreach (LocationModel prop in GetLocationList)
                            {
                                if (prop.LocationName != null)
                                {
                                    sw.Write(prop.LocationName);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.LocationShortName != null)
                                {
                                    sw.Write(prop.LocationShortName);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.AddressLine1 != null)
                                {
                                    sw.Write(prop.AddressLine1);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.AddressLine2 != null)
                                {
                                    sw.Write(prop.AddressLine2);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.City != null)
                                {
                                    //sw.Write(prop.DateIn.Value.Date.ToString("MM/dd/yyy"));
                                    sw.Write(prop.City);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.State != null)
                                {
                                    sw.Write(prop.State);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.Zip != null)
                                {
                                    sw.Write(prop.Zip);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.CustomerLocationCode != null)
                                {
                                    sw.Write(prop.CustomerLocationCode);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.MainPhone != null)
                                {
                                    sw.Write(prop.MainPhone);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.FaxNumber != null)
                                {
                                    sw.Write(prop.FaxNumber);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.PrimaryContactPhone != null)
                                {
                                    sw.Write(prop.PrimaryContactPhone);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.PrimaryContactExtension != null)
                                {
                                    sw.Write(prop.PrimaryContactExtension);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                if (prop.DeliveryHoldDate != null)
                                {
                                    sw.Write(prop.DeliveryHoldDate);
                                    sw.Write(",");
                                }
                                else
                                {
                                    sw.Write("");
                                    sw.Write(",");
                                }
                                // Do something with propValue  "Primary Contact", "Primary Contact Phone", "Primary Contact Ext", "Delivery Times" }
                                sw.Write(sw.NewLine);
                            }
                            sw.Close();
                            System.Diagnostics.Process.Start(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "Locations.csv");
                        }
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
        /// Function ImportLocation to import the location
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-30,2016</createdOn>
        public void ImportLocation(object obj)
        {
            //string errMsg = string.Empty;
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //LocationModel objLocation = new LocationModel();
                //CustomerAdminDeligate.SetValueBillStreetAddrMethod(objLocation);
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
                    CustomerAdminDeligate.OnSetCustomerAdminPropertiesValueEvt -= new CustomerAdminDeligate.SetCustomerAdminPropertiesValue(NotificationCmdReceived);
                    LocationDeligate.OnSetValueEvtUpdateCmd -= new LocationDeligate.SetValueUpdateCmd(NotificationCmdReceivedToUpdate);
                    LocationDeligate.OnSetValueEvtTotalCountPagerCmd -= new LocationDeligate.SetValueTotalCountPager(GetTotalPageCount);
                    LocationDeligate.OnSetValuePageNumberCmd -= new LocationDeligate.SetValuePageNumberClick(GetCurrentPageIndex);
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {            
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
