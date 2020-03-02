using Appworks.Reports;
using AppWorks.UI.Common;
using AppWorks.UI.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace AppWorks.UI.ViewModel.Report
{
    public class PortStorageInBoundReportVM : ViewModelBase
    {
        string userCode = System.Windows.Application.Current.Properties["LoggedInUserName"].ToString();
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
        private string addressLine1;
        public string AddressLine1
        {
            get { return this.addressLine1; }
            set { this.addressLine1 = value; NotifyPropertyChanged("AddressLine1"); }
        }
        private string city;
        public string City
        {
            get { return this.city; }
            set { this.city = value; NotifyPropertyChanged("City"); }
        }
        private string phone;
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; NotifyPropertyChanged("Phone"); }
        }
        private Nullable<System.DateTime> startDate;
        public Nullable<System.DateTime> StartDate
        {
            get { return startDate; }
            set
            {
                if (value != null)
                {
                    startDate = value;
                }
            }
        }
        private Nullable<System.DateTime> endDate;
        public Nullable<System.DateTime> EndDate
        {
            get { return endDate; }
            set
            {
                if (value != null)
                {
                    endDate = value;
                }
            }
        }

        private AppWorxCommand btnSearch_Click;
        /// <summary>
        /// Find button event
        /// </summary>
        public AppWorxCommand BtnSearch_Click
        {
            get
            {
                if (btnSearch_Click == null)
                {
                    btnSearch_Click = new AppWorxCommand(this.Search);
                }
                return btnSearch_Click;
            }
        }

        #region Constructor
        /// <summary>
        /// This constructor is used to get report data
        /// </summary>
        public PortStorageInBoundReportVM()
        {
            //CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            //try
            //{
            //    var data1 = _serviceInstance.GetDAIAddressName(userCode).Select(d => new Appworks.Reports.PortStorageInBoundReport
            //    {
            //        CompanyName = d.CompanyName,
            //        AddressLine1 = d.AddressLine1,
            //        City = d.City,
            //        Phone = d.Phone
            //    }).FirstOrDefault();

            //    CompanyName = data1.CompanyName;
            //    AddressLine1 = data1.AddressLine1;
            //    City = data1.City;
            //    Phone = data1.Phone;

            //    var report = new PortStorageInBoundRPT();

            //    var data = _serviceInstance.GetPortStorageInBoundList(StartDate, EndDate).Select(d => new Appworks.Reports.PortStorageInBoundReport
            //    {
            //        CustomerName = d.CustomerName,
            //        Baylocation = d.Baylocation,
            //        VIN = d.VIN,
            //        Make = d.Make,
            //        Model = d.Model,
            //        LoadNumber = d.LoadNumber,
            //        Location = d.Location,
            //        DateIn = d.DateIn,
            //        CompanyName = CompanyName,
            //        AddressLine1 = AddressLine1,
            //        City = City,
            //        Phone = Phone

            //    }).ToList();
            //    if (data != null)
            //    {

            //        data.Add(new Appworks.Reports.PortStorageInBoundReport
            //        {
            //            CompanyName = CompanyName,
            //            AddressLine1 = AddressLine1,
            //            City = City,
            //            Phone = Phone
            //        });
            //    }
            //    report.DataSource = data;
            //    //MyReportSource = report;                
            //}
            //catch (Exception ex)
            //{
            //    bool displayErrorOnUI = false;
            //    CommonSettings.logger.LogError(this.GetType(), ex);
            //    if (displayErrorOnUI)
            //    { throw; }
            //}
            //finally
            //{
            //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            //}
        }
        #endregion

        /// <summary>
        /// This function is used to search report according to date
        /// </summary>
        /// <param name="obj"></param>
        public void Search(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (StartDate != null && EndDate != null)
                {
                    if (StartDate <= EndDate)
                    {
                        var report = new PortStorageInBoundRPT();

                        var data = _serviceInstance.GetPortStorageInBoundListAsync(StartDate, EndDate).Result;

                        IList<PortStorageInBoundReport> result = data.Select(d => new PortStorageInBoundReport
                        {
                            CustomerName = d.CustomerName,
                            Baylocation = d.Baylocation,
                            VIN = d.VIN,
                            Make = d.Make,
                            Model = d.Model,
                            LoadNumber = d.LoadNumber,
                            Location = d.Location,
                            DateIn = d.DateIn,
                            CompanyName = CompanyName,
                            AddressLine1 = AddressLine1,
                            City = City,
                            Phone = Phone,
                            DeliveryBayLocation = d.DeliveryBayLocation
                        }).ToList();

                        if (result.Count() > 0)
                        {
                            //result.Add(new Appworks.Reports.PortStorageInBoundReport
                            //{
                            //    CompanyName = CompanyName,
                            //    AddressLine1 = AddressLine1,
                            //    City = City,
                            //    Phone = Phone
                            //});
                            ExportToCSV(result);
                        }
                        else
                        {
                            //System.Windows.MessageBox.Show(Resources.msgNoData);
                            System.Windows.MessageBox.Show("There are no Port Storage records to export for the selected date range.","No Port Storage Records to Export");
                            return;
                        }
                        //report.DataSource = data;
                        //MyReportSource = report;

                    }
                    else
                    {
                        System.Windows.MessageBox.Show(Resources.msgDateCompair);
                        return;
                    }
                }
                else
                { System.Windows.MessageBox.Show(Resources.msgDateReq); }
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
        /// Used to export data to CSV format
        /// </summary>
        /// <param name="portStorageInBoundList"></param>
        private void ExportToCSV(IList<PortStorageInBoundReport> portStorageInBoundList)
        {
            try
            {
                var listToExport = new ObservableCollection<PortStorageInBoundReport>(portStorageInBoundList);

                string[] headers = new string[] { "Drop Off Location", "Load Number", "VIN", "Make", "Model", "Bay Location", "Date In", "Del.Bay Loc." };

                if (!Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString()))
                {
                    Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString());
                }

                using (StreamWriter sw = new StreamWriter(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "PortStorageInBoundRPT.csv", false))
                {
                    foreach (string objVal in headers)
                    {
                        sw.Write(objVal);
                        sw.Write(",");
                    }

                    sw.Write(sw.NewLine);

                    // Row creation
                    foreach (var prop in listToExport)
                    {
                        if (prop.Location != null)
                        {
                            sw.Write(prop.Location);
                            sw.Write(",");
                        }
                        else
                        {
                            sw.Write("");
                            sw.Write(",");
                        }
                        if (prop.LoadNumber != null)
                        {
                            sw.Write(prop.LoadNumber);
                            sw.Write(",");
                        }
                        else
                        {
                            sw.Write("");
                            sw.Write(",");
                        }
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
                        if (prop.Baylocation != null)
                        {
                            sw.Write(prop.Baylocation);
                            sw.Write(",");
                        }
                        else
                        {
                            sw.Write("");
                            sw.Write(",");
                        }

                        if (prop.DateIn != null)
                        {
                            sw.Write(prop.DateIn.Value.Date.ToString("d-MMM-yy"));
                            sw.Write(",");
                        }
                        else
                        {
                            sw.Write("");
                            sw.Write(",");
                        }

                        if (prop.DeliveryBayLocation != null)
                        {
                            sw.Write(prop.DeliveryBayLocation);
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
                }
                System.Diagnostics.Process.Start(System.Configuration.ConfigurationManager.AppSettings["CSVFilePath"].ToString() + "PortStorageInBoundRPT.csv");
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                //throw;
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }
    }
}