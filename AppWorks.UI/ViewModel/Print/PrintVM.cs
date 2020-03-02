using System;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using AppWorks.UI.Common;
using AppWorks.UI.View.Print;
using System.Reflection;
using AppWorks.UI.Model;
using AppWorks.UI.ViewModel.PortStorageInvoices;
using AppWorks.UI.Properties;
using AppWorks.UI.Common;

namespace AppWorks.UI.ViewModel.Print
{
    /// <summary>
    /// PrintVM Class To Contain all Property of print View
    /// </summary>
    public class PrintVM : ViewModelBase
    {
        /// <summary>
        /// Constructor to get the vehicle print details.
        /// </summary>
        /// <param name="objPortStorageVehiclePrintModel"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-07,2016</createdOn>
        public PrintVM(PortStorageVehiclePrintModel objPortStorageVehiclePrintModel)
        {
            if (objPortStorageVehiclePrintModel != null)
            {
                PrintModule = Enums.PrintEnum.portstorage.ToString();
                PrintPSVehicle = objPortStorageVehiclePrintModel;
            }
        }

        /// <summary>
        /// Constructor to get the vehicle print details.
        /// </summary>
        /// <param name="objPortStorageVehiclePrintModel"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-07,2016</createdOn>
        public PrintVM(List<RequestProcessingPrintModel> lstRequestProcessingPrintModel)
        {
            if (lstRequestProcessingPrintModel != null)
            {
                PrintModule = Enums.PrintEnum.requestprocessing.ToString();
                printPSRequest = lstRequestProcessingPrintModel;
            }
        }

        /// <summary>
        /// Constructor to get the vehicle print details.
        /// </summary>
        /// <param name="objPortStorageVehiclePrintModel"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-07,2016</createdOn>
        public PrintVM(DateoutProcessingModel objDateoutProcessingModel)
        {
            if (objDateoutProcessingModel != null)
            {
                //PrintModule = "dateout";
                PrintPSDateOut = objDateoutProcessingModel;
            }
        }

        public PrintVM(List<PrintInvoiceErrorModel> lstPrintInvoiceErrorModel)
        {
            if (lstPrintInvoiceErrorModel != null)
            {
                PrintModule = Enums.PrintEnum.printerrorinvoice.ToString();
                PrintErrorInvoice = lstPrintInvoiceErrorModel;
            }
        }
        public PrintVM(List<PrintInvoiceModel> lstPrintInvoiceModel)
        {
            if (lstPrintInvoiceModel != null)
            {
                PrintModule = Enums.PrintEnum.printinvoice.ToString();
                PrintInvoice = lstPrintInvoiceModel;
            }
        }

        #region "Page Properties"
        private PortStorageVehiclePrintModel printPSVehicle;
        public PortStorageVehiclePrintModel PrintPSVehicle
        {
            get { return printPSVehicle; }
            set
            {
                this.printPSVehicle = value;
            }
        }

        private List<RequestProcessingPrintModel> printPSRequest;
        public List<RequestProcessingPrintModel> PrintPSRequest
        {
            get { return printPSRequest; }
            set
            {
                this.printPSRequest = value;
            }
        }

        private DateoutProcessingModel printPSDateOut;
        public DateoutProcessingModel PrintPSDateOut
        {
            get { return printPSDateOut; }
            set
            {
                this.printPSDateOut = value;
            }
        }
        private List<PrintInvoiceErrorModel> printErrorInvoice;
        public List<PrintInvoiceErrorModel> PrintErrorInvoice
        {
            get { return printErrorInvoice; }
           private set
            {
                this.printErrorInvoice = value;
            }
        }
        private List<PrintInvoiceModel> printInvoice;
        public List<PrintInvoiceModel> PrintInvoice
        {
            get { return printInvoice; }
           private set
            {
                this.printInvoice = value;
            }
        }
        private string tempPrintType;
        private string printType;
        public string PrintType
        {
            get { return printType; }
            set
            {
                this.printType = value;
            }
        }

        private string printModule;
        public string PrintModule
        {
            get { return printModule; }
            set
            {
                if (value != null)
                {
                    this.printModule = value;
                }
            }
        }
        #endregion

        #region "Page Events"

        private ICommand btnPrinter_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnPrinter_Click
        {
            get
            {
                if (btnPrinter_Click == null)
                {
                    //tempPrintType = PrintHelper.FileType.openprinter.ToString();
                    //btnPrinter_Click = new AppWorxCommand(this.SetValue);
                    btnPrinter_Click = new AppWorxCommand(
                       param => this.SetValue(PrintHelper.FileType.openprinter.ToString()),
                       param => CanCheck
                       );
                }
                return btnPrinter_Click;
            }
        }

        private ICommand btnPriview_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnPriview_Click
        {
            get
            {
                if (btnPriview_Click == null)
                {
                    //tempPrintType = PrintHelper.FileType.preview.ToString();
                    //btnPriview_Click = new AppWorxCommand(this.SetValue);
                    //PrintType = PrintHelper.FileType.preview.ToString();
                    btnPriview_Click = new AppWorxCommand(
                       param => this.SetValue(PrintHelper.FileType.preview.ToString()),
                       param => CanCheck
                       );
                }
                return btnPriview_Click;
            }
        }


        private ICommand btnScreen_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnScreen_Click
        {
            get
            {
                if (btnScreen_Click == null)
                {
                  
                    btnScreen_Click = new AppWorxCommand(
                     param => this.SetValue(PrintHelper.FileType.screen.ToString()),
                     param => CanCheck
                     );
                }
                return btnScreen_Click;
            }
        }

        private ICommand btnDisk_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnDisk_Click
        {
            get
            {
                if (btnDisk_Click == null)
                {
                    //tempPrintType = PrintHelper.FileType.rep.ToString();
                    //btnDisk_Click = new AppWorxCommand(this.SetValue);
                   // PrintType = PrintHelper.FileType.rep.ToString();
                    btnDisk_Click = new AppWorxCommand(
                      param => this.SetValue(PrintHelper.FileType.rep.ToString()),
                      param => CanCheck
                      );
                }
                return btnDisk_Click;
            }
        }


        private AppWorxCommand btnClipboard_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public AppWorxCommand BtnClipboard_Click
        {
            get
            {
                if (btnClipboard_Click == null)
                {
                    //tempPrintType = PrintHelper.FileType.clipboard.ToString();
                    //btnClipboard_Click = new AppWorxCommand(this.SetValue);
                    //PrintType = PrintHelper.FileType.clipboard.ToString();
                    btnClipboard_Click = new AppWorxCommand(
                   param => this.SetValue(PrintHelper.FileType.clipboard.ToString()),
                   param => CanCheck
                   );
                }
                return btnClipboard_Click;
            }
        }

        private ICommand btnPort_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnPort_Click
        {
            get
            {
                if (btnPort_Click == null)
                {
                    //tempPrintType = PrintHelper.FileType.port.ToString();
                    //btnPort_Click = new AppWorxCommand(this.SetValue);
                   // PrintType = PrintHelper.FileType.port.ToString();
                    btnPort_Click = new AppWorxCommand(
                       param => this.SetValue(PrintHelper.FileType.port.ToString()),
                       param => CanCheck
                       );
                }
                return btnPort_Click;
            }
        }

        private ICommand btnFile_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnFile_Click
        {
            get
            {
                if (btnFile_Click == null)
                {
                    //tempPrintType = PrintHelper.FileType.prn.ToString();
                    //btnFile_Click = new AppWorxCommand(this.SetValue);
                    btnFile_Click = new AppWorxCommand(
                       param => this.SetValue(PrintHelper.FileType.prn.ToString()),
                       param => CanCheck
                       );

                    //PrintType = PrintHelper.FileType.prn.ToString();
                    //btnFile_Click = new AppWorxCommand(
                    //    param => this.PrintMethod(),
                    //    param => CanCheck
                    //    );
                }
                return btnFile_Click;
            }
        }

        private ICommand btnHTML_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnHTML_Click
        {
            get
            {
                if (btnHTML_Click == null)
                {
                    btnHTML_Click = new AppWorxCommand(
                        param => this.SetValue(PrintHelper.FileType.html.ToString()),
                        param => CanCheck
                        );
                }
                return btnHTML_Click;
            }
        }

        private ICommand btnPostscript_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnPostscript_Click
        {
            get
            {
                if (btnPostscript_Click == null)
                {
                    btnPostscript_Click = new AppWorxCommand(
                        param => this.SetValue(PrintHelper.FileType.ps.ToString()),
                        param => CanCheck
                        );
                }
                return btnPostscript_Click;
            }
        }

        private ICommand btnRTF_Click;

        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnRTF_Click
        {
            get
            {
                if (btnRTF_Click == null)
                {
                    btnRTF_Click = new AppWorxCommand(
                        param => this.SetValue(PrintHelper.FileType.rtf.ToString()),
                        param => CanCheck
                        );
                }
                return btnRTF_Click;
            }
        }

        private ICommand btnOK_Click;
        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand BtnOK_Click
        {
            get
            {
                if (btnOK_Click == null)
                {
                    btnOK_Click = new AppWorxCommand(
                        param => this.PrintMethod(),
                        param => CanCheck
                        );
                }
                return btnOK_Click;
            }
        }

        private ICommand btnCancel_Click;
        /// <summary>
        /// Cancel button event
        /// </summary>
        public ICommand BtnCancel_Click
        {
            get
            {
                if (btnCancel_Click == null)
                {
                    PrintType = string.Empty;
                    btnCancel_Click = new AppWorxCommand(
                        param => this.PrintClose(),
                        param => CanCheck
                        );
                }
                return btnCancel_Click;
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
        /// Function to check the Login Credentials.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        private void SetValue(object objCtrl)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (objCtrl != null)
                {
                    PrintType = objCtrl.ToString();
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
        /// Function to close the print popup.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-13,2016</createdOn>
        private void PrintClose()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.DataContext == this)
                    {
                        window.Close();
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
        /// Function to check the Login Credentials.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        private void PrintMethod()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                // creating the object of PrintHelper class
                PrintHelper objPrintHelper = new PrintHelper();
                // call the different print data functions on the basis of different modules
                if (!string.IsNullOrEmpty(PrintType))
                {
                    switch (PrintModule)
                    {
                        case "portstorage":
                            objPrintHelper.PrintData(PrintModule, PrintType, PrintPSVehicle, null, null, null);
                            break;
                        case "requestprocessing":
                           objPrintHelper.PrintData(PrintModule, PrintType, null, PrintPSRequest, null, null);
                            break;
                        case "printerrorinvoice":
                            objPrintHelper.PrintData(PrintModule, PrintType, null, null, PrintErrorInvoice, null);
                            break;
                        case "printinvoice":
                            objPrintHelper.PrintData(PrintModule, PrintType, null, null, null, PrintInvoice);
                            break;
                       
                    }
                    foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                    {
                        if (window.Title.ToLower().Equals("reportwindow"))
                        {
                            window.Close();
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

    }
}
