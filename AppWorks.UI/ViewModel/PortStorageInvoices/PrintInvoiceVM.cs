using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;
using System.Windows;
using System;
using AppWorks.UI.Common;
using AppWorksService.Properties;
using AppWorks.UI.View.PortStorageInvoices;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using System.Windows.Data;
using AppWorks.UI.View.Print;
using AppWorks.UI.Model;
using AppWorks.UI.Models;
using AppWorks.UI.Properties;
using Appworks.Reports;


namespace AppWorks.UI.ViewModel.PortStorageInvoices
{
    public class PrintInvoiceVM : ViewModelBase
    {
        string userCode = Application.Current.Properties["LoggedInUserName"].ToString();
        /// <summary>
        /// This is property used to Change Report functionality
        /// </summary>
        private Telerik.Reporting.ReportSource _myReportSource;

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

        /// <summary>
        /// to print invoice summary
        /// </summary>
        private Telerik.Reporting.ReportSource _myReportSummarySource;
        public Telerik.Reporting.ReportSource MyReportSummarySource
        {
            get
            {
                return _myReportSummarySource;
            }
            set
            {
                // Check if it's really a change 
                if (value == _myReportSummarySource)
                    return;

                // Change Report 
                _myReportSummarySource = value;

                // Notify attached View(s) 
                NotifyPropertyChanged("MyReportSummarySource");
                //RaisePropertyChanged("Report");
            }
        }

        public PrintInvoiceVM()
        {
            var report = new PrintInvoiceRPT();
            UnPrint = true;
            BothCopiesInd = true;

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
                CompanyPhone = data.Phone;
            }
        }

        private string companyName;
        public string CompanyName
        {
            get { return this.companyName; }
            set { this.companyName = value; NotifyPropertyChanged("CompanyName"); }
        }
        private string companyaddressLine1;
        public string CompanyAddressLine1
        {
            get { return this.companyaddressLine1; }
            set { this.companyaddressLine1 = value; NotifyPropertyChanged("CompanyAddressLine1"); }
        }
        private string companycity;
        public string CompanyCity
        {
            get { return this.companycity; }
            set { this.companycity = value; NotifyPropertyChanged("CompanyCity"); }
        }
        private string companyphone;
        public string CompanyPhone
        {
            get { return this.companyphone; }
            set { this.companyphone = value; NotifyPropertyChanged("CompanyPhone"); }
        }
        private bool reprintInd;
        public bool ReprintInd
        {
            get { return this.reprintInd; }
            set { this.reprintInd = value; NotifyPropertyChanged("ReprintInd"); }
        }

        private bool bothCopiesInd;
        public bool BothCopiesInd
        {
            get { return this.bothCopiesInd; }
            set { this.bothCopiesInd = value; NotifyPropertyChanged("BothCopiesInd"); }
        }
        private bool customerCopyInd;
        public bool CustomerCopyInd
        {
            get { return this.customerCopyInd; }
            set { this.customerCopyInd = value; NotifyPropertyChanged("CustomerCopyInd"); }
        }

        private bool diversifiedCopyInd;
        public bool DiversifiedCopyInd
        {
            get { return this.diversifiedCopyInd; }
            set { this.diversifiedCopyInd = value; NotifyPropertyChanged("DiversifiedCopyInd"); }
        }

        private bool reprintType;
        public bool ReprintType
        {
            get { return this.reprintType; }
            set { this.reprintType = value; NotifyPropertyChanged("ReprintType"); }
        }

        private bool unPrint;
        public bool UnPrint
        {
            get { return this.unPrint; }
            set { this.unPrint = value; NotifyPropertyChanged("UnPrint"); }
        }

        private bool byNumber;
        public bool ByNumber
        {
            get { return this.byNumber; }
            set { this.byNumber = value; NotifyPropertyChanged("ByNumber"); }
        }
        private bool byDate;
        public bool ByDate
        {
            get { return this.byDate; }
            set { this.byDate = value; NotifyPropertyChanged("ByDate"); }
        }
        private bool printedDate;
        public bool PrintedDate
        {
            get { return this.printedDate; }
            set { this.printedDate = value; NotifyPropertyChanged("PrintedDate"); }
        }
        private bool invoiceDate;
        public bool InvoiceDate
        {
            get { return this.invoiceDate; }
            set { this.invoiceDate = value; NotifyPropertyChanged("InvoiceDate"); }
        }


        private bool isEnabledbyNumber;
        public bool IsEnabledByNumber
        {
            get { return this.isEnabledbyNumber; }
            set { this.isEnabledbyNumber = value; NotifyPropertyChanged("IsEnabledByNumber"); }
        }
        private bool isEnabledbyDate;
        public bool IsEnabledByDate
        {
            get { return this.isEnabledbyDate; }
            set { this.isEnabledbyDate = value; NotifyPropertyChanged("IsEnabledByDate"); }
        }

        private bool isEnabledprintedDate;
        public bool IsEnabledPrintedDate
        {
            get { return this.isEnabledprintedDate; }
            set { this.isEnabledprintedDate = value; NotifyPropertyChanged("IsEnabledPrintedDate"); }
        }

        private bool isEnabledinvoiceDate;
        public bool IsEnabledInvoiceDate
        {
            get { return this.isEnabledinvoiceDate; }
            set { this.isEnabledinvoiceDate = value; NotifyPropertyChanged("IsEnabledInvoiceDate"); }
        }

        private bool isEnabledFromDate;
        public bool IsEnabledFromDate
        {
            get { return this.isEnabledFromDate; }
            set { this.isEnabledFromDate = value; NotifyPropertyChanged("IsEnabledFromDate"); }
        }

        private bool isEnabledToDate;
        public bool IsEnabledToDate
        {
            get { return this.isEnabledToDate; }
            set { this.isEnabledToDate = value; NotifyPropertyChanged("IsEnabledToDate"); }
        }

        private bool isEnabledFromNumber;
        public bool IsEnabledFromNumber
        {
            get { return this.isEnabledFromNumber; }
            set { this.isEnabledFromNumber = value; NotifyPropertyChanged("IsEnabledFromNumber"); }
        }

        private bool isEnabledToNumber;
        public bool IsEnabledToNumber
        {
            get { return this.isEnabledToNumber; }
            set { this.isEnabledToNumber = value; NotifyPropertyChanged("IsEnabledToNumber"); }
        }




        private DateTime dateValue;

        public DateTime DateValue
        {
            get
            {
                return dateValue.Date;
            }
        }

        private Nullable<System.DateTime> invoiceDateFrom;
        public Nullable<System.DateTime> InvoiceDateFrom
        {
            get { return invoiceDateFrom; }
            set
            {
                this.invoiceDateFrom = value;
                NotifyPropertyChanged("InvoiceDateFrom");

            }
        }

        private Nullable<System.DateTime> invoiceDateTo;
        public Nullable<System.DateTime> InvoiceDateTo
        {
            get { return invoiceDateTo; }
            set
            {
                this.invoiceDateTo = value;
                NotifyPropertyChanged("InvoiceDateTo");

            }
        }
        private string invoiceNumberFrom;
        public string InvoiceNumberFrom
        {
            get { return this.invoiceNumberFrom; }
            set { this.invoiceNumberFrom = value; NotifyPropertyChanged("InvoiceNumberFrom"); }
        }

        private string invoiceNumberTo;
        public string InvoiceNumberTo
        {
            get { return this.invoiceNumberTo; }
            set { this.invoiceNumberTo = value; NotifyPropertyChanged("InvoiceNumberTo"); }
        }

        private int copiesToPrint;
        public int CopiesToPrint
        {
            get { return this.copiesToPrint; }
            set { this.copiesToPrint = value; NotifyPropertyChanged("CopiesToPrint"); }
        }

        private int dateType;
        public int DateType
        {
            get { return this.dateType; }
            set { this.dateType = value; NotifyPropertyChanged("DateType"); }
        }

        private List<Appworks.Reports.PrintInvoiceReport> printInvoiceList;
        public List<Appworks.Reports.PrintInvoiceReport> PrintInvoiceList
        {
            get { return printInvoiceList; }
            private set
            {
                this.printInvoiceList = value;
                NotifyPropertyChanged("PrintInvoiceList");
            }
        }


        private bool isCheckReprint;
        public bool IsCheckReprint
        {
            get { return isCheckReprint; }
            set
            {
                this.isCheckReprint = value;
                NotifyPropertyChanged("IsCheckReprint");
            }
        }
        private bool isCheckPrintDate;
        public bool IsCheckPrintDate
        {
            get { return isCheckPrintDate; }
            set
            {
                this.isCheckPrintDate = value;
                NotifyPropertyChanged("IsCheckPrintDate");
            }
        }


        private AppWorxCommand radioButton_Checked;
        public AppWorxCommand RadioButton_Checked
        {
            get
            {
                if (radioButton_Checked == null)
                {
                    radioButton_Checked = new AppWorxCommand(this.GetRadioButton);
                }
                return radioButton_Checked;
            }
        }
        private AppWorxCommand radioButtonBy_Checked;
        public AppWorxCommand RadioButtonBy_Checked
        {
            get
            {
                if (radioButtonBy_Checked == null)
                {
                    radioButtonBy_Checked = new AppWorxCommand(this.GetRadioButtonByValue);
                }
                return radioButtonBy_Checked;
            }
        }


        private AppWorxCommand btnPrintInvoiceContinue_Click;
        /// <summary>
        /// Find button event
        /// </summary>
        public AppWorxCommand BtnPrintInvoiceContinue_Click
        {
            get
            {
                if (btnPrintInvoiceContinue_Click == null)
                {
                    btnPrintInvoiceContinue_Click = new AppWorxCommand(this.OpenPrintInvoiceContinue);
                }
                return btnPrintInvoiceContinue_Click;
            }
        }

        private AppWorxCommand btnCancel_Click;
        /// <summary>
        /// Cancel button event
        /// </summary>
        public AppWorxCommand BtnCancel_Click
        {
            get
            {
                if (btnCancel_Click == null)
                {
                    btnCancel_Click = new AppWorxCommand(this.CancelWindow);
                }
                return btnCancel_Click;
            }
        }
        /// <summary>
        ///  This method is used to open print window for print invoice
        /// </summary>
        /// <param name="obj"></param>
        public void OpenPrintInvoiceContinue(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                int reprintIndValue = 0;
                int reptintTypeValue = 0;
                int dateTypeValue = 0;

                if (ReprintInd == true)
                    reprintIndValue = 1;
                else if (UnPrint == true)
                    reprintIndValue = 0;

                if (ByDate == true)
                    reptintTypeValue = 0;
                else if (ByNumber == true)
                    reptintTypeValue = 1;

                if (PrintedDate == true)
                    dateTypeValue = 0;
                else if (InvoiceDate == true)
                    dateTypeValue = 1;




                if (ReprintInd == true)
                {
                    if (ByDate == true)
                    {
                        if (InvoiceDateFrom == null)
                        {
                            MessageBox.Show(Resources.msgStratDateReq);
                            return;
                        }
                        if (InvoiceDateTo == null)
                        {
                            MessageBox.Show(Resources.msgEndDateReq);
                            return;
                        }
                        if (InvoiceDateFrom > InvoiceDateTo)
                        {
                            System.Windows.MessageBox.Show(Resources.msgDateCompair);
                            return;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace( InvoiceNumberFrom))
                        {
                            MessageBox.Show(Resources.msgStratInvoiceReq);
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(InvoiceNumberTo))
                        {
                            MessageBox.Show(Resources.msgEndInvoiceReq);
                            return;

                        }
                    }

                }
                if (BothCopiesInd)
                { CopiesToPrint = 2; }
                else if (DiversifiedCopyInd || CustomerCopyInd)
                { CopiesToPrint = 1; }

                if ((CopiesToPrint == 1) || (CopiesToPrint == 2))
                {
                    if (CopiesToPrint == 2)
                    {
                        var report2 = new PrintInvoiceRPT();
                        var data2 = _serviceInstance.GetInvoiceDataForInvoicePrint(reprintIndValue, reptintTypeValue, dateTypeValue, InvoiceDateFrom, InvoiceDateTo, InvoiceNumberFrom, InvoiceNumberTo).Select(d => new Appworks.Reports.PrintInvoiceReport
                        {
                            CustomerName = d.CustomerName,
                            AddressLine1 = d.AddressLine1,
                            AddressLine2 = d.AddressLine2,
                            City = d.City,
                            Country = d.Country,
                            CustomerCode = d.CustomerCode,
                            Zip = d.Zip,
                            FullAddress = (!string.IsNullOrEmpty(d.City) ? d.City + ", " : "") + d.State + " " + d.Zip,
                            InvoiceNumber = d.InvoiceNumber,
                            InvoiceDate = d.InvoiceDate,
                            InvoiceAmount = d.InvoiceAmount,
                            VIN = d.VIN,
                            DateIn = d.DateIn,
                            DateOut = d.DateOut,
                            EntryRate = d.EntryRate,
                            TotalCharge = d.TotalCharge,
                            StorageCharges = d.StorageCharges,
                            State = d.State,
                            CompanyName = CompanyName,
                            CompanyAddressLine1 = CompanyAddressLine1,
                            CompanyCity = CompanyCity,
                            CompanyPhone = CompanyPhone,
                            Model = d.Model,
                            StorageDays = d.StorageDays,
                            BilledDays = d.BilledDays,
                            DailyStorage = d.DailyStorage,
                            Header = d.Header


                        }).ToList();

                        PrintInvoiceList = new List<Appworks.Reports.PrintInvoiceReport>();

                        for (int i = 0; i < data2.ToList().Count; i++)
                        {
                            for (int j = 1; j <= 2; j++)
                            {
                                PrintInvoiceList.Add(data2[i]);
                            }
                        }

                        List<Appworks.Reports.PrintInvoiceReport> printInvoiceListTemp = new List<Appworks.Reports.PrintInvoiceReport>();
                        for (int i = 0; i < PrintInvoiceList.Count; i++)
                        {
                            Appworks.Reports.PrintInvoiceReport printInvoiceListCopy = new Appworks.Reports.PrintInvoiceReport();
                            printInvoiceListCopy.CustomerName = PrintInvoiceList[i].CustomerName;
                            printInvoiceListCopy.AddressLine1 = PrintInvoiceList[i].AddressLine1;
                            printInvoiceListCopy.AddressLine2 = PrintInvoiceList[i].AddressLine2;
                            printInvoiceListCopy.City = PrintInvoiceList[i].City;
                            printInvoiceListCopy.Country = PrintInvoiceList[i].Country;
                            printInvoiceListCopy.CustomerCode = PrintInvoiceList[i].CustomerCode;
                            printInvoiceListCopy.Zip = PrintInvoiceList[i].Zip;
                            printInvoiceListCopy.FullAddress = PrintInvoiceList[i].FullAddress;
                            printInvoiceListCopy.InvoiceNumber = PrintInvoiceList[i].InvoiceNumber;
                            printInvoiceListCopy.InvoiceDate = PrintInvoiceList[i].InvoiceDate;
                            printInvoiceListCopy.InvoiceAmount = PrintInvoiceList[i].InvoiceAmount;
                            printInvoiceListCopy.VIN = PrintInvoiceList[i].VIN;
                            printInvoiceListCopy.DateIn = PrintInvoiceList[i].DateIn;
                            printInvoiceListCopy.DateOut = PrintInvoiceList[i].DateOut;
                            printInvoiceListCopy.EntryRate = PrintInvoiceList[i].EntryRate;
                            printInvoiceListCopy.TotalCharge = PrintInvoiceList[i].TotalCharge;
                            printInvoiceListCopy.StorageCharges = PrintInvoiceList[i].StorageCharges;
                            printInvoiceListCopy.State = PrintInvoiceList[i].State;
                            printInvoiceListCopy.CompanyName = CompanyName;
                            printInvoiceListCopy.CompanyAddressLine1 = CompanyAddressLine1;
                            printInvoiceListCopy.CompanyCity = CompanyCity;
                            printInvoiceListCopy.CompanyPhone = CompanyPhone;
                            printInvoiceListCopy.Model = PrintInvoiceList[i].Model;
                            printInvoiceListCopy.StorageDays = PrintInvoiceList[i].StorageDays;
                            printInvoiceListCopy.BilledDays = PrintInvoiceList[i].BilledDays;
                            printInvoiceListCopy.DailyStorage = PrintInvoiceList[i].DailyStorage;
                            printInvoiceListCopy.Header = PrintInvoiceList[i].Header;
                            //if (i == 2)
                            //    printInvoiceListCopy.Header = "Dishank";
                            //else if (i == 3)
                            //    printInvoiceListCopy.Header = "Dishank Pundir";                            

                            if (i % 2 != 0)
                            {
                                printInvoiceListCopy.InvoiceNumber = printInvoiceListCopy.InvoiceNumber + " ";
                                printInvoiceListCopy.Header = "Customer Copy";
                            }
                            else
                            {
                                printInvoiceListCopy.Header = "Diversified Copy";
                            }

                            printInvoiceListTemp.Add(printInvoiceListCopy);
                        }

                        report2.DataSource = printInvoiceListTemp.OrderBy(x => x.Header).ThenBy(x => x.CustomerName);
                        MyReportSource = report2;
                    }
                    else
                    {
                        var report = new PrintInvoiceRPT();
                        var data = _serviceInstance.GetInvoiceDataForInvoicePrint(reprintIndValue, reptintTypeValue, dateTypeValue, InvoiceDateFrom, InvoiceDateTo, InvoiceNumberFrom, InvoiceNumberTo).Select(d => new Appworks.Reports.PrintInvoiceReport
                        {
                            CustomerName = d.CustomerName,
                            AddressLine1 = d.AddressLine1,
                            AddressLine2 = d.AddressLine2,
                            City = d.City,
                            Country = d.Country,
                            CustomerCode = d.CustomerCode,
                            Zip = d.Zip,
                            FullAddress = (!string.IsNullOrEmpty(d.City) ? d.City + ", " : "") + d.State + " " + d.Zip,
                            InvoiceNumber = d.InvoiceNumber,
                            InvoiceDate = d.InvoiceDate,
                            InvoiceAmount = d.InvoiceAmount,
                            VIN = d.VIN,
                            DateIn = d.DateIn,
                            DateOut = d.DateOut,
                            EntryRate = d.EntryRate,
                            TotalCharge = d.TotalCharge,
                            StorageCharges = d.StorageCharges,
                            State = d.State,
                            CompanyName = CompanyName,
                            CompanyAddressLine1 = CompanyAddressLine1,
                            CompanyCity = CompanyCity,
                            CompanyPhone = CompanyPhone,
                            Model = d.Model,
                            StorageDays = d.StorageDays,
                            BilledDays = d.BilledDays,
                            DailyStorage = d.DailyStorage,
                            Header = DiversifiedCopyInd == true ? "Diversified Copy" : "Customer Copy"
                        }).ToList();

                        PrintInvoiceList = data.ToList();
                        report.DataSource = PrintInvoiceList.OrderBy(c=>c.Header).ThenBy(x => x.CustomerName);
                        MyReportSource = report;
                    }
                    if (PrintInvoiceList.Count > 0)
                    {
                        //PrintWindow objPrintWindow = new PrintWindow(PrintInvoiceList);
                        //objPrintWindow.Show();
                        if (!reprintInd)
                        {
                            var summaryReport = new InvoiceSummaryRPT();
                            var list = PrintInvoiceList.OrderBy(x => x.InvoiceDate);
                            summaryReport.DataSource = GetAndDisplayPortStorageSummaryReport(list.FirstOrDefault().InvoiceDate,list.LastOrDefault().InvoiceDate);
                            MyReportSummarySource = summaryReport;
                            PrintInvoiceReportWindow objPrintSummary = new PrintInvoiceReportWindow(MyReportSummarySource);
                            objPrintSummary.ShowDialog();
                        }
                        PrintInvoiceReportWindow objPrintWindow = new PrintInvoiceReportWindow(MyReportSource);
                        objPrintWindow.Owner = Application.Current.MainWindow;
                        objPrintWindow.ShowDialog();
                    }
                    else
                    { MessageBox.Show(Resources.msgNoData); }
                }
                else
                { MessageBox.Show("Please select copies to print!"); }
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

        private List<InvoiceSummaryReport> GetAndDisplayPortStorageSummaryReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var data = _serviceInstance.GetDAIAddressName(userCode).Select(d => new InvoiceSummaryReport
                {
                    CompanyName = d.CompanyName,
                    AddressLine1 = d.AddressLine1,
                    City = d.City,
                    Phone = d.Phone
                }).FirstOrDefault();
                var CompanyName = data.CompanyName;
                var AddressLine1 = data.AddressLine1;
                var City = data.City;
                var Phone = data.Phone;

                // get data and create report here 
                

                var data1 = _serviceInstance.LoadInvoiceList(0, startDate, endDate).Select(d => new InvoiceSummaryReport
                {
                    InvoiceNumber = d.InvoiceNumber,
                    InvoiceAmount = d.InvoiceAmount,
                    CustomerNumber=d.CustomerNumber,
                    InvoiceDate = d.InvoiceDate,
                    Units = d.Units,
                    CustomerName = d.CustomerName,
                    CompanyName = CompanyName,
                    AddressLine1 = AddressLine1,
                    City = City,
                    Phone = Phone,
                    StartDate = startDate,
                    EndDate = endDate,
                }).ToList();

                //double totalInvoiceAmount = 0;

                //int totalUnits = 0;

                //if (data1 != null)
                //{
                //    data1.Add(new InvoiceSummaryReport
                //    {
                //        InvoiceNumber = null,
                //        InvoiceAmount = totalInvoiceAmount,
                //        InvoiceDate = null,
                //        Units = totalUnits,
                //        CompanyName = CompanyName,
                //        AddressLine1 = AddressLine1,
                //        City = City,
                //        Phone = Phone,
                //        StartDate = startDate,
                //        EndDate = endDate,
                //    });
                //}
                //report.DataSource = 
                //MyReportSource = report; 
                return data1;
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
                return null;
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to Cancle  Popup.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>June 07,2016</createdOn>
        private void CancelWindow(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                int countWindow = 0;
                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (countWindow == 1)
                    {
                        window.Close();
                    }
                    countWindow++;
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
        /// This method is used to get redion button selected status
        /// </summary>
        /// <param name="obj"></param>
        private void GetRadioButton(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (UnPrint == true)
                {
                    IsEnabledByDate = false;
                    IsEnabledByNumber = false;
                    IsEnabledFromDate = false;
                    IsEnabledFromNumber = false;
                    IsEnabledToDate = false;
                    IsEnabledToNumber = false;
                    IsEnabledInvoiceDate = false;
                    IsEnabledPrintedDate = false;
                }
                else
                {
                    IsEnabledByDate = true;
                    IsEnabledByNumber = true;
                    IsEnabledFromDate = true;
                    IsEnabledFromNumber = true;
                    IsEnabledToDate = true;
                    IsEnabledToNumber = true;
                    IsEnabledInvoiceDate = true;
                    IsEnabledPrintedDate = true;
                    ByDate = true;
                    PrintedDate = true;
                    IsEnabledToNumber = false;
                    IsEnabledFromNumber = false;

                }
                InvoiceDateFrom = null;
                InvoiceDateTo = null;
                InvoiceNumberFrom = null;
                InvoiceNumberTo = null;
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
        /// This method is used to get redion button selected status
        /// </summary>
        /// <param name="obj"></param>
        private void GetRadioButtonByValue(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (ByDate == true)
                {

                    IsEnabledFromDate = true;
                    IsEnabledToDate = true;
                    IsEnabledInvoiceDate = true;
                    IsEnabledPrintedDate = true;
                    IsEnabledToNumber = false;
                    IsEnabledFromNumber = false;
                    InvoiceNumberFrom = null;
                    InvoiceNumberTo = null;

                }
                else
                {
                    IsEnabledFromDate = false;
                    IsEnabledFromNumber = true;
                    IsEnabledToDate = false;
                    IsEnabledToNumber = true;
                    IsEnabledInvoiceDate = false;
                    IsEnabledPrintedDate = false;
                    InvoiceDateFrom = null;
                    InvoiceDateTo = null;


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
