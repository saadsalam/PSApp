using AppWorks.UI.Common;
using AppWorks.UI.Properties;
using AppWorksService.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using Appworks.Reports;
using AppWorks.UI.View.Billing;

namespace AppWorks.UI.ViewModel.Billing
{
    public class BillingExportVM : ViewModelBase
    {
        string UserCode = Application.Current.Properties["LoggedInUserName"].ToString();
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

        public BillingExportVM()
        {
            EXportTypeZero = true;
            var data = _serviceInstance.GetDAIAddressName(UserCode).FirstOrDefault().CompanyName;
            if (data != null)
            {
                CompanyName = data;
            }
        }

        #region :: All Property ::
        /// <summary>
        /// This proprty for File Name
        /// </summary>
        private string compName;
        public string CompanyName
        {
            get { return this.compName; }
            set { this.compName = value; }
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

        private bool eXportTypeZero;
        public bool EXportTypeZero
        {
            get { return this.eXportTypeZero; }
            set { this.eXportTypeZero = value; NotifyPropertyChanged("EXportTypeZero"); }
        }
        private bool eXportTypeOne;
        public bool EXportTypeOne
        {
            get { return this.eXportTypeOne; }
            set { this.eXportTypeOne = value; NotifyPropertyChanged("EXportTypeOne"); }
        }
        /// <summary>
        /// This property for BatchId
        /// </summary>
        private int exportType;
        public int ExportType
        {
            get { return this.exportType; }
            set { this.exportType = value; NotifyPropertyChanged("ExportType"); }
        }

        /// <summary>
        /// This property for BatchId
        /// </summary>
        private DateTime? exportDate;
        public DateTime? ExportDate
        {
            get { return this.exportDate; }
            set { this.exportDate = value; NotifyPropertyChanged("ExportDate"); }
        }

        /// <summary>
        /// This proprty for File Name
        /// </summary>
        private string filePath;
        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; NotifyPropertyChanged("FilePath"); }
        }

        /// <summary>
        /// This is list to bind data in grid view.
        /// </summary>
        private List<BillingLineItemsProp> lstBillingRecordExport;
        public List<BillingLineItemsProp> ListBillingRecordExport
        {
            get { return lstBillingRecordExport; }
            set { lstBillingRecordExport = value; NotifyPropertyChanged("ListBillingRecordExport"); }
        }

        private AppWorxCommand btnExport_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnExport_Click
        {
            get
            {
                if (btnExport_Click == null)
                {
                    btnExport_Click = new AppWorxCommand(this.BillingExport);
                }
                return btnExport_Click;
            }
        }
        #endregion

        public void BillingExport(object obj)
        {

            try
            {
                PortStorageVehicleImportProp portStorageVehicleImportProp = new PortStorageVehicleImportProp();
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Export Billing Records?", "Are you sure you wish to export the Billing Records?", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (EXportTypeZero == true)
                        ExportType = 0;
                    else if (EXportTypeOne == true)
                        ExportType = 1;

                    if (ExportType == 1)
                    {
                        if (ExportDate == null)
                        {
                            MessageBox.Show("You must select the date that the billing records were previously exported.", "Enter Export Date");
                            return;
                        }

                    }

                    ListBillingRecordExport = _serviceInstance.GetBillingRecordExport(ExportType, ExportDate).ToList();


                    if (ListBillingRecordExport.Count < 1)
                    {
                        MessageBox.Show("There are no billing records to export using the current export type.", "No Billing Records To Export");
                        return;
                    }
                    if (ExportType == 0)
                    {
                        BatchId = _serviceInstance.GetBillingExportBatchId();
                        if (BatchId > 0)
                        {
                            bool value = _serviceInstance.SetBillingExportNextBatchId(BatchId);
                            if (!value)
                            {
                                MessageBox.Show("The system was unable to set the next billing export batch id!");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("The system was unable to get the next billing export batch id!", "Unable To Get Batch ID");
                            return;
                        }
                    }

                    FilePath = _serviceInstance.GetBillingExportFilePath();
                    if (FilePath != null)
                    {
                        if (Directory.Exists(FilePath))
                        {
                            string fileName = string.Empty;
                            StringBuilder dataWriter = new StringBuilder();
                            string prevYear = string.Empty;
                            string prevMonth = string.Empty;
                            string prevCustOf = string.Empty;
                            int fileExported = 0;

                            foreach (var item in ListBillingRecordExport)
                            {

                                fileName = "I" + item.CustomerOf + Convert.ToDateTime(item.InvoiceDate).ToString("MMM") + item.TheYear.ToString();

                                string invoiceDate = Convert.ToDateTime(item.InvoiceDate).ToString("MMddyyyy");
                                string customerNumber = item.CustomerNumber;
                                string invoiceNumber = item.InvoiceNumber;
                                string debitAccountNumber = item.DebitAccountNumber;
                                string debitProfitCenterNumber = item.DebitProfitCenterNumber;
                                string debitCostCenterNumber = item.DebitCostCenterNumber;
                                string creditAccountNumber = item.CreditAccountNumber;
                                string creditProfitCenterNumber = item.CreditProfitCenterNumber;
                                string creditCostCenterNumber = item.CreditCostCenterNumber;
                                string aRTransactionAmount = item.ARTransactionAmount == null ? string.Empty : item.ARTransactionAmount.ToString();
                                string creditMemoInd = item.CreditMemoInd == null ? string.Empty : item.CreditMemoInd.ToString();
                                string description = item.Description;

                                var row = string.Format("{0,-6}{1,-10}{2}{3,-5}{4,-2}{5,-3}{6,-5}{7,-2}{8,-3}{9,-8}{10,-1}{11,-30}{12,-1}", customerNumber, invoiceNumber, invoiceDate, debitAccountNumber, debitProfitCenterNumber, debitCostCenterNumber, creditAccountNumber, creditProfitCenterNumber, creditCostCenterNumber, aRTransactionAmount, creditMemoInd, description, "Z");

                                dataWriter = dataWriter.Append(row);
                                dataWriter = dataWriter.Append(Environment.NewLine);
                                if (!File.Exists(FilePath + "\\" + fileName + ".txt"))
                                {
                                    File.Create(FilePath + "\\" + fileName + ".txt").Close();
                                    dataWriter = new StringBuilder();
                                    string rowUpdated = string.Format("{0,-6}{1,-10}{2}{3,-5}{4,-2}{5,-3}{6,-5}{7,-2}{8,-3}{9,-8}{10,-1}{11,-30}{12,-1}", customerNumber, invoiceNumber, invoiceDate, debitAccountNumber, debitProfitCenterNumber, debitCostCenterNumber, creditAccountNumber, creditProfitCenterNumber, creditCostCenterNumber, aRTransactionAmount, creditMemoInd, description, "Z");
                                    dataWriter = dataWriter.Append(rowUpdated);
                                    dataWriter = dataWriter.Append(Environment.NewLine);
                                }
                                using (StreamWriter writer = new StreamWriter(FilePath + "\\" + fileName + ".txt", false))
                                {
                                    writer.WriteLine(dataWriter);
                                    fileExported++;
                                }

                                prevMonth = Convert.ToString(item.TheMonth);
                                prevYear = Convert.ToString(item.TheYear);
                                UserCode = Application.Current.Properties["LoggedInUserName"].ToString();
                                _serviceInstance.UpdateBillingLineItem(BatchId, item.BillingLineItemsID, UserCode);
                            }
                            if (ListBillingRecordExport.Count > 0)
                            {
                                var objReportData = ListBillingRecordExport.Select(d => new Appworks.Reports.BillingExportProp
                                {
                                    BatchID = BatchId,
                                    CompanyName = CompanyName,
                                    CreditAcc = d.CreditAccountNumber,
                                    CreditCost = d.CreditCostCenterNumber,
                                    CreditProfit = d.CreditProfitCenterNumber,
                                    CustomerID = Convert.ToInt32(d.CustomerNumber),
                                    DebitAcc = d.DebitAccountNumber,
                                    DebitCost = d.DebitCostCenterNumber,
                                    DebitProfit = d.DebitProfitCenterNumber,
                                    TransAmount = d.ARTransactionAmount,
                                    Description = d.Description,
                                    ExportDate = d.ExportedDate == null ? DateTime.Now : d.ExportedDate,
                                    Invoice = d.InvoiceNumber,
                                    InvoiceDate = d.InvoiceDate
                                }).ToList();

                                ///billing Invoice report that displays batchid
                                var report = new BillingInvoiceRPT();
                                report.DataSource = objReportData.ToList();
                                MyReportSource = report;

                                BillingInvoiceReportWindow objPrintWindow = new BillingInvoiceReportWindow(MyReportSource);
                                objPrintWindow.ShowDialog();
                            }
                            int countWindow = 0;
                            if (fileExported > 0)
                            {
                                MessageBox.Show("Data exported succesfully.");
                                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                                {
                                    if (countWindow == 1)
                                    {
                                        window.Close();
                                    }
                                    countWindow++;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Unable to export data.");
                                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                                {
                                    if (countWindow == 1)
                                    {
                                        window.Close();
                                    }
                                    countWindow++;
                                }
                            }
                        }
                        else
                        {
                            //if it doesn't, create it
                            // Directory.CreateDirectory(directoryPath);
                            MessageBox.Show("The Billing Line Items Export file export path does not exist!", "Export Path Does Not Exist");
                        }
                    }
                    else
                    {
                        MessageBox.Show("There was an error loading the data.", "Error Loading Data");
                        return;
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


    }
}
