using System;
using System.Collections.Generic;
using System.Text;
using AppWorks.UI.Model;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Globalization;
using AppWorks.UI.Properties;
using System.Reflection;

namespace AppWorks.UI.Common
{
    /// <summary>
    /// PrintHelper Class allows user to print the data in differernt formats
    /// </summary>
    public class PrintHelper
    {
        public string PrintModule { get; set; }
        public string PrintType { get; set; }
        public StringBuilder sb = new StringBuilder();
        public StringBuilder sbText = new StringBuilder();
        private string stringToPrint = string.Empty; // for accesss document to print
        Font Font = new Font("Arial", 10); // set font for print preview
        /// <summary>
        /// This enum for File Type
        /// </summary>
        public enum FileType
        {
            html,
            ps,
            rtf,
            prn,
            rep,
            port,
            clipboard,
            screen,
            preview,
            openprinter
        }

       /// <summary>
        /// This Method is used to print data for all moduel
       /// </summary>
       /// <param name="PrintModule"></param>
       /// <param name="PrintType"></param>
       /// <param name="portStorageVehiclePrint"></param>
       /// <param name="objRequestProcessingPrintModel"></param>
       /// <param name="lstPrintInvoiceErrorModel"></param>
       /// <param name="lstPrintInvoiceModel"></param>
        public void PrintData(string PrintModule, string PrintType, PortStorageVehiclePrintModel portStorageVehiclePrint = null, List<RequestProcessingPrintModel> lstRequestProcessingPrintModel = null, List<PrintInvoiceErrorModel> lstPrintInvoiceErrorModel = null, List<PrintInvoiceModel> lstPrintInvoiceModel = null)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (PrintModule.ToLower(CultureInfo.CurrentCulture).Equals(Enums.PrintEnum.portstorage.ToString()))
                {
                    if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.html.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.ps.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.rtf.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        using (SaveFileDialog dlg = new SaveFileDialog())
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                string fileName = dlg.FileName;
                                if (PrintType.Equals(FileType.rtf.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    SaveToFile(fileName, FileType.rtf, portStorageVehiclePrint);
                                }
                                else if (PrintType.Equals(FileType.html.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    SaveToFile(fileName, FileType.html, portStorageVehiclePrint);
                                }
                                else if (PrintType.Equals(FileType.ps.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    SaveToFile(fileName, FileType.ps, portStorageVehiclePrint);
                                }


                            }
                        }

                    }
                    else if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.prn.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.rep.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        using (SaveFileDialog dlg = new SaveFileDialog())
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                string fileName = dlg.FileName;
                                if (PrintType.Equals(FileType.prn.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    SaveToFile(fileName, FileType.prn, portStorageVehiclePrint);
                                }
                                else if (PrintType.Equals(FileType.rep.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    SaveToFile(fileName, FileType.rep, portStorageVehiclePrint);
                                }
                            }
                        }

                    }
                    else if (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.clipboard.ToString().ToLower(CultureInfo.CurrentCulture)))
                    {
                        SaveToClipBoard(portStorageVehiclePrint);
                    }
                    else if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.preview.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.screen.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        PrintPreview(portStorageVehiclePrint);
                    }
                    else if (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.openprinter.ToString().ToLower(CultureInfo.CurrentCulture)))
                    {
                        PrinterOpen(portStorageVehiclePrint);
                    }
                    else
                    {
                        GenerateHTML(portStorageVehiclePrint);
                    }
                }
                else if (PrintModule.ToLower(CultureInfo.CurrentCulture).Equals(Enums.PrintEnum.requestprocessing.ToString()))
                {
                    if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.html.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.ps.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.rtf.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        using (SaveFileDialog dlg = new SaveFileDialog())
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                string fileName = dlg.FileName;
                                if (PrintType.Equals(FileType.rtf.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    RequestProcessingSaveToFile(fileName, FileType.rtf, lstRequestProcessingPrintModel);
                                }
                                else if (PrintType.Equals(FileType.html.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    RequestProcessingSaveToFile(fileName, FileType.html, lstRequestProcessingPrintModel);
                                }
                                else if (PrintType.Equals(FileType.ps.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    RequestProcessingSaveToFile(fileName, FileType.ps, lstRequestProcessingPrintModel);
                                }


                            }
                        }

                    }
                    else if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.prn.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.rep.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        using (SaveFileDialog dlg = new SaveFileDialog())
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                string fileName = dlg.FileName;
                                if (PrintType.Equals(FileType.prn.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    RequestProcessingSaveToFile(fileName, FileType.prn, lstRequestProcessingPrintModel);
                                }
                                else if (PrintType.Equals(FileType.rep.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    RequestProcessingSaveToFile(fileName, FileType.rep, lstRequestProcessingPrintModel);
                                }
                            }
                        }

                    }
                    else if (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.clipboard.ToString().ToLower(CultureInfo.CurrentCulture)))
                    {
                        RequestProcessingSaveToClipBoard(lstRequestProcessingPrintModel);
                    }
                    else if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.preview.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.screen.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        RequestProcessingPrintPreview(lstRequestProcessingPrintModel);
                    }
                    else if (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.openprinter.ToString().ToLower(CultureInfo.CurrentCulture)))
                    {
                        RequestProcessingPrinterOpen(lstRequestProcessingPrintModel);
                    }
                    else
                    {
                        RequestProcessingGenerateHTML(lstRequestProcessingPrintModel);
                    }
                }
                else if (PrintModule.ToLower(CultureInfo.CurrentCulture).Equals(Enums.PrintEnum.printerrorinvoice.ToString()))
                {
                    if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.html.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.ps.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.rtf.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        using (SaveFileDialog dlg = new SaveFileDialog())
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                string fileName = dlg.FileName;
                                if (PrintType.Equals(FileType.rtf.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    PrintErrorInvoiceSaveToFile(fileName, FileType.rtf, lstPrintInvoiceErrorModel);
                                }
                                else if (PrintType.Equals(FileType.html.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    PrintErrorInvoiceSaveToFile(fileName, FileType.html, lstPrintInvoiceErrorModel);
                                }
                                else if (PrintType.Equals(FileType.ps.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    PrintErrorInvoiceSaveToFile(fileName, FileType.ps, lstPrintInvoiceErrorModel);
                                }


                            }
                        }

                    }
                    else if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.prn.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.rep.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        using (SaveFileDialog dlg = new SaveFileDialog())
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                string fileName = dlg.FileName;
                                if (PrintType.Equals(FileType.prn.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    PrintErrorInvoiceSaveToFile(fileName, FileType.prn, lstPrintInvoiceErrorModel);
                                }
                                else if (PrintType.Equals(FileType.rep.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    PrintErrorInvoiceSaveToFile(fileName, FileType.rep, lstPrintInvoiceErrorModel);
                                }
                            }
                        }

                    }
                    else if (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.clipboard.ToString().ToLower(CultureInfo.CurrentCulture)))
                    {
                        PrintErrorInvoiceSaveToClipBoard(lstPrintInvoiceErrorModel);
                    }
                    else if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.preview.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.screen.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        PrintErrorInvoicePrintPreview(lstPrintInvoiceErrorModel);
                    }
                    else if (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.openprinter.ToString().ToLower(CultureInfo.CurrentCulture)))
                    {
                        PrintErrorInvoicePrinterOpen(lstPrintInvoiceErrorModel);
                    }
                    else
                    {
                        PrintErrorInvoiceGenerateHTML(lstPrintInvoiceErrorModel);
                    }
                }
                else if (PrintModule.ToLower(CultureInfo.CurrentCulture).Equals(Enums.PrintEnum.printinvoice.ToString()))
                {
                    if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.html.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.ps.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.rtf.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        using (SaveFileDialog dlg = new SaveFileDialog())
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                string fileName = dlg.FileName;
                                if (PrintType.Equals(FileType.rtf.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    PrintInvoiceSaveToFile(fileName, FileType.rtf, lstPrintInvoiceModel);
                                }
                                else if (PrintType.Equals(FileType.html.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    PrintInvoiceSaveToFile(fileName, FileType.html, lstPrintInvoiceModel);
                                }
                                else if (PrintType.Equals(FileType.ps.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    PrintInvoiceSaveToFile(fileName, FileType.ps, lstPrintInvoiceModel);
                                }


                            }
                        }

                    }
                    else if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.prn.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.rep.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        using (SaveFileDialog dlg = new SaveFileDialog())
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                string fileName = dlg.FileName;
                                if (PrintType.Equals(FileType.prn.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    PrintInvoiceSaveToFile(fileName, FileType.prn, lstPrintInvoiceModel);
                                }
                                else if (PrintType.Equals(FileType.rep.ToString().ToLower(CultureInfo.CurrentCulture)))
                                {
                                    PrintInvoiceSaveToFile(fileName, FileType.rep, lstPrintInvoiceModel);
                                }
                            }
                        }

                    }
                    else if (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.clipboard.ToString().ToLower(CultureInfo.CurrentCulture)))
                    {
                        PrintInvoiceSaveToClipBoard(lstPrintInvoiceModel);
                    }
                    else if ((PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.preview.ToString().ToLower(CultureInfo.CurrentCulture))) || (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.screen.ToString().ToLower(CultureInfo.CurrentCulture))))
                    {
                        PrintInvoicePrintPreview(lstPrintInvoiceModel);
                    }
                    else if (PrintType.ToLower(CultureInfo.CurrentCulture).Equals(FileType.openprinter.ToString().ToLower(CultureInfo.CurrentCulture)))
                    {
                        PrintInvoicePrinterOpen(lstPrintInvoiceModel);
                    }
                    else
                    {
                        PrintInvoiceGenerateHTML(lstPrintInvoiceModel);
                    }
                }


            }
            catch (Exception ex)
            {
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

        #region :: PortStorageVehiclePrintModel ::
       /// <summary>
        ///  This method is used to save to file.
       /// </summary>
       /// <param name="fileName"></param>
       /// <param name="fileType"></param>
       /// <param name="objPortStorageVehiclePrint"></param>
        public void SaveToFile(string fileName, FileType fileType, PortStorageVehiclePrintModel objPortStorageVehiclePrint)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                System.IO.TextWriter w = null;
                switch (fileType)
                {
                    case FileType.html:
                        GenerateHTML(objPortStorageVehiclePrint);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.html.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;

                    case FileType.ps:
                        GenerateHTML(objPortStorageVehiclePrint);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.ps.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.rtf:
                        GenerateHTML(objPortStorageVehiclePrint);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.rtf.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.prn:
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.prn.ToString()))
                        {
                            w.Write(GenerateText(objPortStorageVehiclePrint));
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.rep:
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.rep.ToString()))
                        {
                            w.Write(GenerateText(objPortStorageVehiclePrint));
                            w.Flush();
                            w.Close();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
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
        /// This method is used to get Genrate HTML File format.
        /// </summary>
        /// <param name="portStorageVehiclePrint"></param>
        public void GenerateHTML(PortStorageVehiclePrintModel portStorageVehiclePrint)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                sb.Append("<html><BODY bgcolor='#FFFFFF' text='#000000' link='#808080' vlink='#808080' alink='#808080'>");
                sb.Append("<TABLE BORDER='0' CELLPADDING='0' CELLSPACING='0' WIDTH='737' HEIGHT='77'>");
                sb.Append("<TD VALIGN=TOP WIDTH=164></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=287>");
                sb.Append("<FONT FACE='Arial'>"+portStorageVehiclePrint.CompanyName+"<BR>"+portStorageVehiclePrint.CompanyAddressLine1+"<BR>"+portStorageVehiclePrint.CompanyCity+"<BR>"+portStorageVehiclePrint.CompanyPhone+"</FONT> </TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=150></TD>");
                sb.Append("<TD VALIGN=TOP ALIGN=RIGHT WIDTH=150>");
                sb.Append("<FONT FACE='Arial' SIZE='2'>Date: " + DateTime.Now.ToShortDateString() + "</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=3></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=20>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=18>");
                sb.Append("<TD VALIGN=TOP WIDTH=199></TD>");
                sb.Append("<TD VALIGN=TOP ALIGN=CENTER WIDTH=341>");
                sb.Append("<FONT FACE='Arial'><B>Port Storage Dealer Pickup/Delivery Request</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=197></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=12>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                sb.Append("<TD VALIGN=TOP WIDTH=737>");
                sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=737 NOSHADE></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=15>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=27>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>VIN:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=90></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=128>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + portStorageVehiclePrint.VIN + "</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=283></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=27>");
                sb.Append("<TD VALIGN=TOP WIDTH=325></TD>");
                //sb.Append("<TD VALIGN=TOP WIDTH=130 >");
                sb.Append("<TD> <span style=\"font-family:'3 of 9 Barcode';\">");
                sb.Append("<FONT SIZE='5' COLOR='#000011'>*" + portStorageVehiclePrint.VINSort + "*</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=282></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=105>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Date Requested:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=11></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=145>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + portStorageVehiclePrint.DateRequested + "</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=28></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=47>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + portStorageVehiclePrint.DateRequestDay + "</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=191></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=150>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Est. Pickup Date:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP></TD>");
                //sb.Append("<TD VALIGN=TOP WIDTH=173></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=150>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + portStorageVehiclePrint.EstPickupDate + "</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=238></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=0>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'></FONT></TD>");
                sb.Append("</TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD>");
                sb.Append("</TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=96>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Date Outgated:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=20></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=145>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + portStorageVehiclePrint.DateOutgated + "</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=266></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=43>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Model:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=73></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=71>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + portStorageVehiclePrint.Model + "</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=339></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=64>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Ext Color:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=53></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=10>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + portStorageVehiclePrint.ExtColor + "</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=401></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=53>");
                sb.Append(" <FONT FACE='Arial' SIZE='2'><B>Ship To:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=63></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=159>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>Herb Chambers BMW/Mini</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=252></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=326></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=156>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>1168 Commonwealth Ave.</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=255></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=326></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=107>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>Allston, MA 02134</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=303></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=17>");
                sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=60>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Location:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=56></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=27>");
                sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + portStorageVehiclePrint.Location + "</FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=384></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=12>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=90>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Pick Up By:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=437></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                sb.Append("<TD VALIGN=TOP WIDTH=328></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=297>");
                sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=297 NOSHADE></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=112></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=41>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Dated:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=486></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                sb.Append("<TD VALIGN=TOP WIDTH=328></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=297>");
                sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=297 NOSHADE></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=112></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=45>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=187></TD>");
                sb.Append("<TD VALIGN=TOP ALIGN=CENTER WIDTH=365>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>DIVERSIFIED AUTOMOTIVE WILL NOT BE RESPONSIBLE</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=185></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=213></TD>");
                sb.Append("<TD VALIGN=TOP ALIGN=CENTER WIDTH=314>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>FOR ANY DAMAGES NOT NOTED ON THIS FORM</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=211></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=28>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=11></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=50>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>NOTES:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=676></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                sb.Append("<TD VALIGN=TOP WIDTH=77></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=624>");
                sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=624 NOSHADE></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=36></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=29>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                sb.Append("<TD VALIGN=TOP WIDTH=77></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=624>");
                sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=624 NOSHADE></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=36></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=29>");
                sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                sb.Append("<TD VALIGN=TOP WIDTH=77></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=624>");
                sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=624 NOSHADE></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=36></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=29><TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                sb.Append("<TD VALIGN=TOP WIDTH=77></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=624>");
                sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=624 NOSHADE></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=36></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=29><TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                sb.Append("<TD VALIGN=TOP WIDTH=11></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=153>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Return To Inventory On:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=573></TD></TABLE>");
                sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=45>");
                sb.Append("<TD VALIGN=TOP WIDTH=79></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=544>");
                sb.Append("<FONT FACE='Arial' SIZE='7' COLOR='#000011'><B>" +String.Format("{0:dddd, MMMM d, yyyy}", portStorageVehiclePrint.Date + "</B></FONT></TD>"));
                sb.Append("<TD VALIGN=TOP WIDTH=113></TD></TABLE>");
                sb.Append("</BODY></html>");

            }
            catch (Exception ex)
            {
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
        /// This method is used to Genrate Text format.
        /// </summary>
        /// <param name="portStorageVehiclePrint"></param>
        /// <returns></returns>
        public string GenerateText(PortStorageVehiclePrintModel portStorageVehiclePrint)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                sbText.Append("" + portStorageVehiclePrint .CompanyName+ "").Append(' ', 45).Append("Date:  " + DateTime.Now.ToShortDateString() + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(""+portStorageVehiclePrint.CompanyAddressLine1+"");
                sbText.Append(Environment.NewLine);
                sbText.Append(""+portStorageVehiclePrint.CompanyCity+"");
                sbText.Append(Environment.NewLine);
                sbText.Append(""+portStorageVehiclePrint.CompanyPhone+"");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Port Storage Dealer Pickup/Delivery Request");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("VIN").Append(' ', 45).Append("" + portStorageVehiclePrint.VIN + "");
                sbText.Append(Environment.NewLine);
                sb.Append("<span style=\"font-family:'3 of 9 Barcode';\">");
                sb.Append("<FONT SIZE='5' COLOR='#000011'>*" + portStorageVehiclePrint.VINSort + "*</FONT></span>");
                sbText.Append(' ', 53).Append("*" + portStorageVehiclePrint.VINSort + "*");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Date Request:  " + portStorageVehiclePrint.DateRequested + "").Append(' ', 10).Append("" + portStorageVehiclePrint.DateRequestDay + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Est. Pickup Date:  " + portStorageVehiclePrint.EstPickupDate + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Date Outgate:  " + portStorageVehiclePrint.DateOutgated + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Model:  ").Append(' ', 30).Append("" + portStorageVehiclePrint.Model + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Ext Color:  " + portStorageVehiclePrint.ExtColor + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Ship To:   Herb Chambers BMW/Mini");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("              1168 Commonwealth Ave.");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("              Allston, MA 02134");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Location:   " + portStorageVehiclePrint.Location + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Pick Up By:").Append("________________________________________________");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Dated:").Append("_______________________________________________________"); 
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("DIVERSIFIED AUTOMOTIVE WILL NOT BE RESPONSIBLE");
                sbText.Append(Environment.NewLine);
                sbText.Append("FOR ANY DAMAGES NOT NOTED ON THIS FORM");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("NOTES:");
                sbText.Append(Environment.NewLine);
                sbText.Append(' ', 53).Append("_______________________________________________________________________________");
                sbText.Append(Environment.NewLine);
                sbText.Append(' ', 53).Append("_______________________________________________________________________________");
                sbText.Append(Environment.NewLine);
                sbText.Append(' ', 53).Append("_______________________________________________________________________________");
                sbText.Append(Environment.NewLine);
                sbText.Append(' ', 53).Append("_______________________________________________________________________________");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Return To Inventory On:");
                sbText.Append(Environment.NewLine);
                sbText.Append("" +String.Format("{0:dddd, MMMM d, yyyy}", portStorageVehiclePrint.Date + ""));
                return sbText.ToString();
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return sbText.ToString();
        }
        /// <summary>
        /// This is method is used to open printer window
        /// </summary>
        /// <param name="portStorageVehiclePrint"></param>
        public void PrinterOpen(PortStorageVehiclePrintModel portStorageVehiclePrint)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                // Create a FlowDocument dynamically.  
                using (System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog())
                {
                    // Allow the user to choose the page range he or she would
                    // like to print.
                    stringToPrint = GenerateText(portStorageVehiclePrint);
                    printDialog.AllowSomePages = true;

                    // Show the help button.
                    printDialog.ShowHelp = true;

                    // Set the Document property to the PrintDocument for 
                    // which the PrintPage Event has been handled. To display the
                    // dialog, either this property or the PrinterSettings property 
                    // must be set 
                    //ReadDocument();
                    using (System.Drawing.Printing.PrintDocument printDocument1 = new PrintDocument())
                    {
                       printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
                        printDialog.Document = printDocument1;

                        DialogResult result = printDialog.ShowDialog();

                        // If the result is OK then print the document.
                        if (result == DialogResult.OK)
                        {
                            printDocument1.Print();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
        /// This is method is used to open print preview  window
        /// </summary>
        /// <param name="portStorageVehiclePrint"></param>
        public void PrintPreview(PortStorageVehiclePrintModel portStorageVehiclePrint)// same for screen
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                stringToPrint = GenerateText(portStorageVehiclePrint);
                using (System.Windows.Forms.PrintPreviewDialog printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog())
                {
                    ((ToolStripButton)((ToolStrip)printPreviewDialog1.Controls[1]).Items[0]).Visible = false;
                    using (System.Drawing.Printing.PrintDocument print1 = new PrintDocument())
                    {
                       print1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
                        printPreviewDialog1.Document = print1;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
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
        /// This is method is used to Save to CLip Borad
        /// </summary>
        /// <param name="portStorageVehiclePrint"></param>
        public void SaveToClipBoard(PortStorageVehiclePrintModel portStorageVehiclePrint)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                System.Windows.Forms.Clipboard.SetText(GenerateText(portStorageVehiclePrint));
            }
            catch (Exception ex)
            {
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

        #region :: RequestProcessingPrintModel ::
        /// <summary>
        /// This method is used to Request Processing Generate HTML format.
        /// </summary>
        /// <param name="objRequestProcessingPrintModel"></param>
        public void RequestProcessingGenerateHTML(List<RequestProcessingPrintModel> lstRequestProcessingPrintModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                foreach (var item in lstRequestProcessingPrintModel)
                {


                    sb.Append("<html><BODY bgcolor='#FFFFFF' text='#000000' link='#808080' vlink='#808080' alink='#808080'>");
                    sb.Append("<TABLE BORDER='0' CELLPADDING='0' CELLSPACING='0' WIDTH='737' HEIGHT='77'>");
                    sb.Append("<TD VALIGN=TOP WIDTH=164></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=287>");
                    sb.Append("<FONT FACE='Arial'>" + item.CompanyName + "<BR>" + item.CompanyAddressLine1 + "<BR>" + item.CompanyCity + "<BR>" + item.Phone + "</FONT> </TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=181></TD>");
                    sb.Append("<TD VALIGN=TOP ALIGN=RIGHT WIDTH=102>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'>Date: " + DateTime.Now.ToShortDateString() + "</FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=3></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=20>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=18>");
                    sb.Append("<TD VALIGN=TOP WIDTH=199></TD>");
                    sb.Append("<TD VALIGN=TOP ALIGN=CENTER WIDTH=341>");
                    sb.Append("<FONT FACE='Arial'><B>Port Storage Dealer Pickup/Delivery Request</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=197></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=12>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737>");
                    sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=737 NOSHADE></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=15>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=27>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>VIN:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=90></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=128>");
                    sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + item.Vin + "</FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=283></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=27>");
                    sb.Append("<TD VALIGN=TOP WIDTH=325></TD>");
                    sb.Append("<TD> <span style=\"font-family:'3 of 9 Barcode';\">");
                    sb.Append("<FONT SIZE='5' COLOR='#000011'>*" + item.ShortVIN + "*</FONT></span></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=282></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=105>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>Date Requested:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=11></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=145>");
                    sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + item.DateRequested + "</FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=28></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=191></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=108>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>Est. Pickup Date:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=7></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=173></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=0>");
                    sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + item.PickeupDate + "</FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=238></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=0>");
                    sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'></FONT></TD>");
                    sb.Append("</TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD>");
                    sb.Append("</TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=43>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>Model:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=73></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=71>");
                    sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + item.MakeModel + "</FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=339></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=64>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>Ext Color:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=53></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=10>");
                    sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + item.Color + "</FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=401></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=53>");
                    sb.Append(" <FONT FACE='Arial' SIZE='2'><B>Ship To:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=63></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=159>");
                    sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>Herb Chambers BMW/Mini</FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=252></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=326></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=156>");
                    sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>1168 Commonwealth Ave.</FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=255></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=326></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=107>");
                    sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>Allston, MA 02134</FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=303></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=17>");
                    sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=60>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>Location:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=56></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=27>");
                    sb.Append("<FONT FACE='Arial' SIZE='2' COLOR='#000011'>" + item.BayLocation + "</FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=384></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=12>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=90>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>Pick Up By:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=437></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                    sb.Append("<TD VALIGN=TOP WIDTH=328></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=297>");
                    sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=297 NOSHADE></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=112></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=13>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=210></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=41>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>Dated:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=486></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                    sb.Append("<TD VALIGN=TOP WIDTH=328></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=297>");
                    sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=297 NOSHADE></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=112></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=45>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=187></TD>");
                    sb.Append("<TD VALIGN=TOP ALIGN=CENTER WIDTH=365>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>DIVERSIFIED AUTOMOTIVE WILL NOT BE RESPONSIBLE</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=185></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=213></TD>");
                    sb.Append("<TD VALIGN=TOP ALIGN=CENTER WIDTH=314>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>FOR ANY DAMAGES NOT NOTED ON THIS FORM</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=211></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=28>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=11></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=50>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>NOTES:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=676></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                    sb.Append("<TD VALIGN=TOP WIDTH=77></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=624>");
                    sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=624 NOSHADE></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=36></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=29>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                    sb.Append("<TD VALIGN=TOP WIDTH=77></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=624>");
                    sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=624 NOSHADE></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=36></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=29>");
                    sb.Append("<TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                    sb.Append("<TD VALIGN=TOP WIDTH=77></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=624>");
                    sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=624 NOSHADE></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=36></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=29><TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=0>");
                    sb.Append("<TD VALIGN=TOP WIDTH=77></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=624>");
                    sb.Append("<HR ALIGN=LEFT SIZE=1 WIDTH=624 NOSHADE></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=36></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=29><TD VALIGN=TOP WIDTH=737></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=16>");
                    sb.Append("<TD VALIGN=TOP WIDTH=11></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=153>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'><B>Return To Inventory On:</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=573></TD></TABLE>");
                    sb.Append("<TABLE BORDER=0 CELLPADDING=0 CELLSPACING=0 WIDTH=737 HEIGHT=45>");
                    sb.Append("<TD VALIGN=TOP WIDTH=79></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=544>");
                    sb.Append("<FONT FACE='Arial' SIZE='7' COLOR='#000011'><B>" + String.Format("{0:dddd, MMMM d, yyyy}", item.DealerPrintDate) + "</B></FONT></TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=113></TD></TABLE>");
                    sb.Append("</BODY></html>");
                }

            }
            catch (Exception ex)
            {
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
        /// This method is used to Request Processing Save To File
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        /// <param name="objRequestProcessingPrintModel"></param>
        public void RequestProcessingSaveToFile(string fileName, FileType fileType, List<RequestProcessingPrintModel> lstRequestProcessingPrintModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                System.IO.TextWriter w = null;
                switch (fileType)
                {
                    case FileType.html:
                        RequestProcessingGenerateHTML(lstRequestProcessingPrintModel);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.html.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.ps:
                        RequestProcessingGenerateHTML(lstRequestProcessingPrintModel);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.ps.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.rtf:
                        RequestProcessingGenerateHTML(lstRequestProcessingPrintModel);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.rtf.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.prn:
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.prn.ToString()))
                        {
                            w.Write(RequestProcessingGenerateText(lstRequestProcessingPrintModel));
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.rep:
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.rep.ToString()))
                        {
                            w.Write(RequestProcessingGenerateText(lstRequestProcessingPrintModel));
                            w.Flush();
                            w.Close();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
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
        /// This method is used to Request Processing Genrate Text format.
        /// </summary>
        /// <param name="portStorageVehiclePrint"></param>
        /// <returns></returns>
        public string RequestProcessingGenerateText(List<RequestProcessingPrintModel> lstRequestProcessingPrintModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                foreach (var item in lstRequestProcessingPrintModel)
                {

                   
                    sbText.AppendLine("" + item.CompanyName + "" + "                                                           Date: " + DateTime.Now.ToShortDateString() + "");
                    sbText.AppendLine("" + item.CompanyAddressLine1 + "");
                    sbText.AppendLine("" + item.CompanyCity + "");
                    sbText.AppendLine("" + item.Phone + "");
                    sbText.AppendLine("                                      Port Storage Dealer Pickup/Delivery Request");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("VIN:                                              " + item.Vin +"");
                    sbText.AppendLine("                                                     * " + item.ShortVIN + " *");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("Date Request:                                 " + item.DateRequested + "");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("Est. Pickup Date:                             " + item.PickeupDate + "");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("Model:                                            " + item.MakeModel + "");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("Ext Color:                                        " + item.Color + "");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("Ship To:                                          "+ "Herb Chambers BMW/Mini"+"");
                    sbText.AppendLine("                                                     1168 Commonwealth Ave.");
                    sbText.AppendLine("                                                     Allston, MA 02134");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("Location:                                          " + item.BayLocation + "");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("Pick Up By:                                ____________________________________________");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("Dated:                                           _____________________________________________");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("                                  DIVERSIFIED AUTOMOTIVE WILL NOT BE RESPONSIBLE");
                    sbText.AppendLine("                                       FOR ANY DAMAGES NOT NOTED ON THIS FORM");
                    sbText.AppendLine("NOTES:");
                    sbText.AppendLine("                 _______________________________________________________________________________");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("                 _______________________________________________________________________________");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("                 _______________________________________________________________________________");
                    sbText.AppendLine(Environment.NewLine);
                    sbText.AppendLine("                 _______________________________________________________________________________");
                    sbText.AppendLine("Return To Inventory On:");
                    sbText.Append(' ', 53).Append("" + String.Format("{0:dddd, MMMM d, yyyy}", item.DealerPrintDate) + "");
                }
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return sbText.ToString();
        }
        /// <summary>
        /// This is method for print by printer of request processing
        /// </summary>
        /// <param name="objRequestProcessingPrintModel"></param>
        public void RequestProcessingPrinterOpen(List<RequestProcessingPrintModel> objRequestProcessingPrintModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                // Create a FlowDocument dynamically.  
                using (System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog())
                {
                    // Allow the user to choose the page range he or she would
                    // like to print.
                    stringToPrint = RequestProcessingGenerateText(objRequestProcessingPrintModel);
                    printDialog.AllowSomePages = true;

                    // Show the help button.
                    printDialog.ShowHelp = true;

                    // Set the Document property to the PrintDocument for 
                    // which the PrintPage Event has been handled. To display the
                    // dialog, either this property or the PrinterSettings property 
                    // must be set 
                    //ReadDocument();
                    using (System.Drawing.Printing.PrintDocument printDocument1 = new PrintDocument())
                    {
                        printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
                        printDialog.Document = printDocument1;

                        DialogResult result = printDialog.ShowDialog();

                        // If the result is OK then print the document.
                        if (result == DialogResult.OK)
                        {
                            printDocument1.Print();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
        /// This is method is used to show print preview on window
        /// </summary>
        /// <param name="objRequestProcessingPrintModel"></param>
        public void RequestProcessingPrintPreview(List<RequestProcessingPrintModel> objRequestProcessingPrintModel)// same for screen
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                stringToPrint = RequestProcessingGenerateText(objRequestProcessingPrintModel);
                using (System.Windows.Forms.PrintPreviewDialog printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog())
                {
                    ((ToolStripButton)((ToolStrip)printPreviewDialog1.Controls[1]).Items[0]).Visible = false;
                    using (System.Drawing.Printing.PrintDocument print1 = new PrintDocument())
                    {
                        print1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
                        printPreviewDialog1.Document = print1;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
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
        /// This function is used to print Document1 Print Page functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                string documentContents = string.Empty;
                // e.Graphics.DrawImage(memoryImage, 0, 0);
                int charactersOnPage = 0;
                int linesPerPage = 0;

                // Sets the value of charactersOnPage to the number of characters 
                // of stringToPrint that will fit within the bounds of the page.
                e.Graphics.MeasureString(stringToPrint, Font,
                    e.MarginBounds.Size, StringFormat.GenericTypographic,
                    out charactersOnPage, out linesPerPage);

                // Draws the string within the bounds of the page.
                e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
                e.MarginBounds, StringFormat.GenericTypographic);

                // Remove the portion of the string that has been printed.
                if (stringToPrint != null)
                {
                    stringToPrint = stringToPrint.Substring(charactersOnPage);

                    // Check to see if more pages are to be printed.
                    e.HasMorePages = (stringToPrint.Length > 0);

                    // If there are no more pages, reset the string to be printed.
                    if (!e.HasMorePages)
                        stringToPrint = documentContents;
                }

            }
            catch (Exception ex)
            {
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
        /// This function is used to Request Processing Save To Clip Board
        /// </summary>
        /// <param name="objRequestProcessingPrintModel"></param>
        public void RequestProcessingSaveToClipBoard(List<RequestProcessingPrintModel> objRequestProcessingPrintModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                System.Windows.Forms.Clipboard.SetText(RequestProcessingGenerateText(objRequestProcessingPrintModel));
            }
            catch (Exception ex)
            {
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

        #region :: PrintErrorInvoicePrintModel ::
        /// <summary>
        /// This method is used to genrate html format for save to file.
        /// </summary>
        /// <param name="lstPrintInvoiceErrorModel"></param>
        public void PrintErrorInvoiceGenerateHTML(List<PrintInvoiceErrorModel> lstPrintInvoiceErrorModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                sb.Append("<html><BODY bgcolor='#FFFFFF' text='#000000' link='#808080' vlink='#808080' alink='#808080'>");
                sb.Append("<TABLE BORDER='0' WIDTH='100%'>");
                sb.Append("<TD VALIGN=TOP></TD>");
                sb.Append("<TD VALIGN=TOP >");
                sb.Append("<FONT FACE='Arial'>Diversified Automotive<BR>100 Terminal Street<BR>Charlestown,  MA   02129<BR>(800) 666-9007</FONT> </TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=150></TD>");
                sb.Append("<TD VALIGN=TOP ALIGN=RIGHT WIDTH=150>");
                sb.Append("<FONT FACE='Arial' SIZE='2'>Date: " + DateTime.Now.ToShortDateString() + "</FONT></TD>");
                sb.Append("<TR><TD colspan='4' style='border-top-style: solid; border-top-width: 1px; border-top-color: #000000;'>");
                sb.Append("</TD>");
                sb.Append("</TR>");
                sb.Append("<TD VALIGN=TOP WIDTH=3></TD></TABLE>");
                sb.Append("<TABLE BORDER='0' CELLPADDING='0' CELLSPACING='0' WIDTH='100%' HEIGHT='77'>");
                sb.Append("<TR >");
                sb.Append("<TD>");
                sb.Append("</TD>");
                sb.Append("<TD style='text-align:center; font-size:x-large'><B>Billing Exceptions</B>");
                sb.Append("</TD>");
                sb.Append("<TD>Date");
                sb.Append("</TD>");
                sb.Append("</TR>");
                sb.Append("<TR>");
                sb.Append("<TD>Exception");
                sb.Append("</TD>");
                sb.Append("<TD>");
                sb.Append("</TD>");
                sb.Append("<TD>Page");
                sb.Append("</TD>");
                sb.Append("</TR>");
                sb.Append("<TR>");
                if (lstPrintInvoiceErrorModel != null)
                {
                    foreach (var item in lstPrintInvoiceErrorModel)
                    {
                        sb.Append("<TD>" + item.OrderNumber);
                        sb.Append("</TD>");
                    }
                }
                sb.Append("</TR>");
                sb.Append("</TABLE>");

                sb.Append("</BODY></html>");


            }
            catch (Exception ex)
            {
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
        /// This method is used to print invoice for save to file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        /// <param name="lstPrintInvoiceErrorModel"></param>
        public void PrintErrorInvoiceSaveToFile(string fileName, FileType fileType, List<PrintInvoiceErrorModel> lstPrintInvoiceErrorModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                System.IO.TextWriter w = null;
                switch (fileType)
                {
                    case FileType.html:
                        PrintErrorInvoiceGenerateHTML(lstPrintInvoiceErrorModel);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.html.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.ps:
                        PrintErrorInvoiceGenerateHTML(lstPrintInvoiceErrorModel);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.ps.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.rtf:
                        PrintErrorInvoiceGenerateHTML(lstPrintInvoiceErrorModel);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.rtf.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.prn:
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.prn.ToString()))
                        {
                            w.Write(PrintErrorInvoiceGenerateText(lstPrintInvoiceErrorModel));
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.rep:
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.rep.ToString()))
                        {
                            w.Write(PrintErrorInvoiceGenerateText(lstPrintInvoiceErrorModel));
                            w.Flush();
                            w.Close();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
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
        /// This method is used to Genrate Text format.
        /// </summary>
        /// <param name="portStorageVehiclePrint"></param>
        /// <returns></returns>
        public string PrintErrorInvoiceGenerateText(List<PrintInvoiceErrorModel> lstPrintInvoiceErrorModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                sbText.Append("Diversified Automotive").Append(' ', 80).Append("Date:  " + DateTime.Now.ToShortDateString() + "");
                sbText.Append(Environment.NewLine);
                sbText.Append("100 Terminal Street");
                sbText.Append(Environment.NewLine);
                sbText.Append("Charlestown,  MA   02129");
                sbText.Append(Environment.NewLine);
                sbText.Append("(800) 666-9007");
                sbText.Append(Environment.NewLine);
                sbText.Append("____________________________________________________________________________");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(' ', 60).Append("Billing Exceptions");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(' ', 90).Append("Date: ");
                sbText.Append(Environment.NewLine);
                sbText.Append(' ', 90).Append("Page: ");
                sbText.Append(Environment.NewLine);
                sbText.Append("Exceptions");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                if (lstPrintInvoiceErrorModel != null)
                {
                    foreach (var item in lstPrintInvoiceErrorModel)
                    {
                        sbText.Append(item.OrderNumber);
                    }
                }
                return sbText.ToString();

            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return sbText.ToString();
        }
        /// <summary>
        /// This is method for print by printer of request processing
        /// </summary>
        /// <param name="objRequestProcessingPrintModel"></param>
        public void PrintErrorInvoicePrinterOpen(List<PrintInvoiceErrorModel> lstPrintInvoiceErrorModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                // Create a FlowDocument dynamically.  
                using (System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog())
                {
                    // Allow the user to choose the page range he or she would
                    // like to print.
                    stringToPrint = PrintErrorInvoiceGenerateText(lstPrintInvoiceErrorModel);
                    printDialog.AllowSomePages = true;

                    // Show the help button.
                    printDialog.ShowHelp = true;

                    // Set the Document property to the PrintDocument for 
                    // which the PrintPage Event has been handled. To display the
                    // dialog, either this property or the PrinterSettings property 
                    // must be set 
                    //ReadDocument();
                    using (System.Drawing.Printing.PrintDocument printDocument1 = new PrintDocument())
                    {
                        printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintErrorInvoiceprintDocument1_PrintPage);
                        printDialog.Document = printDocument1;

                        DialogResult result = printDialog.ShowDialog();

                        // If the result is OK then print the document.
                        if (result == DialogResult.OK)
                        {
                            printDocument1.Print();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
        /// This is method is used to show print preview on window
        /// </summary>
        /// <param name="objRequestProcessingPrintModel"></param>
        public void PrintErrorInvoicePrintPreview(List<PrintInvoiceErrorModel> lstPrintInvoiceErrorModel)// same for screen
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                stringToPrint = PrintErrorInvoiceGenerateText(lstPrintInvoiceErrorModel);
                using (System.Windows.Forms.PrintPreviewDialog printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog())
                {
                    ((ToolStripButton)((ToolStrip)printPreviewDialog1.Controls[1]).Items[0]).Visible = false;
                    using (System.Drawing.Printing.PrintDocument print1 = new PrintDocument())
                    {
                        print1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintErrorInvoiceprintDocument1_PrintPage);
                        printPreviewDialog1.Document = print1;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
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
        /// This function is used to Print Error Invoice print Document1 PrintPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PrintErrorInvoiceprintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                string documentContents = string.Empty;
                // e.Graphics.DrawImage(memoryImage, 0, 0);
                int charactersOnPage = 0;
                int linesPerPage = 0;

                // Sets the value of charactersOnPage to the number of characters 
                // of stringToPrint that will fit within the bounds of the page.
                e.Graphics.MeasureString(stringToPrint, Font,
                    e.MarginBounds.Size, StringFormat.GenericTypographic,
                    out charactersOnPage, out linesPerPage);

                // Draws the string within the bounds of the page.
                e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
                e.MarginBounds, StringFormat.GenericTypographic);

                // Remove the portion of the string that has been printed.
                if (stringToPrint != null)
                {
                    stringToPrint = stringToPrint.Substring(charactersOnPage);

                    // Check to see if more pages are to be printed.
                    e.HasMorePages = (stringToPrint.Length > 0);

                    // If there are no more pages, reset the string to be printed.
                    if (!e.HasMorePages)
                        stringToPrint = documentContents;
                }
            }
            catch (Exception ex)
            {
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
        /// This method is used to Print Error Invoice Save To ClipBoard
        /// </summary>
        /// <param name="lstPrintInvoiceErrorModel"></param>
        public void PrintErrorInvoiceSaveToClipBoard(List<PrintInvoiceErrorModel> lstPrintInvoiceErrorModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                System.Windows.Forms.Clipboard.SetText(PrintErrorInvoiceGenerateText(lstPrintInvoiceErrorModel));
            }
            catch (Exception ex)
            {
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

        #region :: PrintInvoicePrintModel ::
        /// <summary>
        /// This method is used to Print Invoice for Generate HTML format
        /// </summary>
        /// <param name="lstPrintInvoiceModel"></param>
        public void PrintInvoiceGenerateHTML(List<PrintInvoiceModel> lstPrintInvoiceModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                decimal? InvoiceTotal;
                foreach (var item in lstPrintInvoiceModel)
                {
                    sb.Append("<html><BODY bgcolor='#FFFFFF' text='#000000' link='#808080' vlink='#808080' alink='#808080'>");
                    sb.Append("<TABLE BORDER='0' WIDTH='100%'>");
                    sb.Append("<TD VALIGN=TOP></TD>");
                    sb.Append("<TD VALIGN=TOP >");
                    sb.Append("<FONT FACE='Arial'>"+item.CompanyName+"<BR>"+item.CompanyAddressLine1+"<BR>"+item.CompanyCity+"<BR>"+item.CompanyPhone+"</FONT> </TD>");
                    sb.Append("<TD VALIGN=TOP WIDTH=150></TD>");
                    sb.Append("<TD VALIGN=TOP ALIGN=RIGHT WIDTH=150>");
                    sb.Append("<FONT FACE='Arial' SIZE='2'>Date: " + DateTime.Now.ToShortDateString() + "</FONT></TD>");
                    sb.Append("<TR><TD colspan='4' style='border-top-style: solid; border-top-width: 1px; border-top-color: #000000;'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TD VALIGN=TOP WIDTH=3></TD></TABLE>");
                    sb.Append("<TABLE BORDER='0' WIDTH='100%' HEIGHT='77'>");
                    sb.Append("<TR style='text-align:center; font-size:x-large'>");
                    sb.Append("<TD colspan='3' ><B>Invoice List</B>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    //sb.Append("<TD>______________________________________________________________________________");
                    //sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD><B>Dealer Number</B>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD style='text-align:right'><B>Invoice Number &nbsp;&nbsp;</B>" + item.InvoiceNumber);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD style='text-align:right'><B>Invoice Number &nbsp;&nbsp;</B>" + item.InvoiceDate);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD colspan='3'>" + item.CustomerName);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD colspan='3'>" + item.AddressLine1);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD colspan='3'>" + item.AddressLine2);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD colspan='3'>" + item.BillingId);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD colspan='3'>" + item.Country);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3' style='border-top-style: solid; border-top-width: 1px; border-top-color: #000000;'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD colspan='3'>");
                    sb.Append("<TABLE style='width:100%'>");
                    sb.Append("<TR>");
                    sb.Append("<TD> <B>Vehicle</B>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD><B>Date In</B>");
                    sb.Append("</TD>");
                    sb.Append("<TD><B>Date Out</B>");
                    sb.Append("</TD>");
                    sb.Append("<TD><B>Storage Days</B>");
                    sb.Append("</TD>");
                    sb.Append("<TD><B>Billed Days</B>");
                    sb.Append("</TD>");
                    sb.Append("<TD><B>Entry Rate</B>");
                    sb.Append("</TD>");
                    sb.Append("<TD><B>Daily Storage</B>");
                    sb.Append("</TD>");
                    sb.Append("<TD><B>Rate</B>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD>" + item.VIN);
                    sb.Append("</TD>");
                    sb.Append("<TD>" + item.Model);
                    sb.Append("</TD>");
                    sb.Append("<TD>" + item.DateIn);
                    sb.Append("</TD>");
                    sb.Append("<TD>" + item.DateOut);
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD>" + item.EntryRate);
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("</TABLE >");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3' style='border-top-style: solid; border-top-width: 1px; border-top-color: #000000;'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD style='text-align:right'><B>Port Storage Charges</B>  " + item.StorageCharges);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD style='text-align:right'><B>Other Charges1</B>  " + item.OtherCharge1);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD style='text-align:right'><B>Other Charges2</B>  " + item.OtherCharge2);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD style='text-align:right'><B>Other Charges3</B>  " + item.OtherCharge3);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD style='text-align:right'><B>Other Charges4</B>  " + item.OtherCharge4);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    InvoiceTotal = item.StorageCharges + item.OtherCharge1 + item.OtherCharge2 + item.OtherCharge3 + item.OtherCharge4;
                    sb.Append("<TR>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("<TD style='text-align:right'><B>Invoice Total</B>  " + InvoiceTotal);
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR>");
                    sb.Append("<TD>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR style='text-align:center'>");
                    sb.Append("<TD><B>PAYMENT IN FULL DUE NET 30 DAYS</B>");
                    sb.Append("</TD>");

                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3' style='border-top-style: solid; border-top-width: 1px; border-top-color: #000000;'>");
                    sb.Append("<TR  style='text-align:center'>");
                    sb.Append("<TD><B>SERVICE IS OUR BUSINESS</B>");
                    sb.Append("</TD>");

                    sb.Append("</TR>");
                    sb.Append("<TR  style='text-align:center'>");
                    sb.Append("<TD><B>DIRECT INQUIRIES TO 617-242-2300 X1125 LINDSAY GOLDSTEIN</B>");
                    sb.Append("</TD>");

                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("<TR><TD colspan='3'>");
                    sb.Append("</TD>");
                    sb.Append("</TR>");
                    sb.Append("</TABLE>");
                    sb.Append("</BODY></html>");
                }


            }
            catch (Exception ex)
            {
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
        /// This method is used to Print Invoice Save To File
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        /// <param name="lstPrintInvoiceModel"></param>
        public void PrintInvoiceSaveToFile(string fileName, FileType fileType, List<PrintInvoiceModel> lstPrintInvoiceModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                System.IO.TextWriter w = null;
                switch (fileType)
                {
                    case FileType.html:
                        PrintInvoiceGenerateHTML(lstPrintInvoiceModel);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.html.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.ps:
                        PrintInvoiceGenerateHTML(lstPrintInvoiceModel);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.ps.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.rtf:
                        PrintInvoiceGenerateHTML(lstPrintInvoiceModel);
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.rtf.ToString()))
                        {
                            w.Write(sb.ToString());
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.prn:
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.prn.ToString()))
                        {
                            w.Write(PrintInvoiceGenerateText(lstPrintInvoiceModel));
                            w.Flush();
                            w.Close();
                        }
                        break;
                    case FileType.rep:
                        using (w = new System.IO.StreamWriter(fileName + "." + FileType.rep.ToString()))
                        {
                            w.Write(PrintInvoiceGenerateText(lstPrintInvoiceModel));
                            w.Flush();
                            w.Close();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
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
        /// This method is used to Print Invoice Genrate Text format.
        /// </summary>
        /// <param name="portStorageVehiclePrint"></param>
        /// <returns></returns>
        public string PrintInvoiceGenerateText(List<PrintInvoiceModel> lstPrintInvoiceModel)
        {
            StringBuilder sbTextx = new StringBuilder();
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
               
                    decimal? InvoiceTotal;
                    foreach (var item in lstPrintInvoiceModel)
                    {
                        sbTextx.AppendLine("" + item.CompanyName + "" + "                                                     Date:  " + DateTime.Now.ToShortDateString() + "");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("" + item.CompanyAddressLine1 + "");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("" + item.CompanyCity + "");
                        //sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("" + item.CompanyPhone + "");
                        sbTextx.AppendLine("___________________________________________________________________________");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("                                                      Invoice List");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("Dealer Number");
                        sbTextx.AppendLine("                                                           Invoice Number  " + item.InvoiceNumber);
                        sbTextx.AppendLine("                                                           Invoice Date  " + item.InvoiceDate);
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("" + item.CustomerName + "");
                        sbTextx.AppendLine("" + item.AddressLine1 + "");
                        sbTextx.AppendLine("" + item.AddressLine2 + "");
                        sbTextx.AppendLine("" + item.BillingId + "");
                        //sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("" + item.Country + "");
                        sbTextx.AppendLine("___________________________________________________________________________");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("Vehicle" + "    " + "Date In" + "      " + "Date Out" + " " + "Storage Days" + "Billed Days" + "Entry Rate" + "Daily Stiarge" + "Rate");
                        //sbTextx.AppendLine(Environment.NewLine); 
                        sbTextx.AppendLine("" + item.VIN + ""+"" + item.Model + ""+"" + item.DateIn + ""+"" + item.DateOut + ""+"" + item.EntryRate + "");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("                            Port Storage Charges "+"" + item.StorageCharges + "");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("                            Other Charges1  "+"" + item.OtherCharge1 + "");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("                            Other Charges2  "+"" + item.OtherCharge2 + "");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("                            Other Charges3  "+"" + item.OtherCharge3 + "");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("                            Other Charges4  "+"" + item.OtherCharge4 + "");
                        InvoiceTotal = item.StorageCharges + item.OtherCharge1 + item.OtherCharge2 + item.OtherCharge3 + item.OtherCharge4;
                        sbTextx.AppendLine("                            Invoice Total  "+"" + InvoiceTotal + "");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("               PAYMENT IN FULL DUE NET 30 DAYS");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("                SERVICE IS OUR BUSINESS");
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine(Environment.NewLine);
                        sbTextx.AppendLine("   DIRECT INQUIRIES TO 617-242-2300 X1125 LINDSAY GOLDSTEIN");
                     
                    }
                    //printPreviewDialog1.ShowDialog();
                
               
            }
            catch (Exception ex)
            {
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
            return sbTextx.ToString();

        }
        /// <summary>
        /// This is method for print by printer of request processing
        /// </summary>
        /// <param name="objRequestProcessingPrintModel"></param>
        public void PrintInvoicePrinterOpen(List<PrintInvoiceModel> lstPrintInvoiceModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                // Create a FlowDocument dynamically.  
                using (System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog())
                {
                    // Allow the user to choose the page range he or she would
                    // like to print.
                    stringToPrint = PrintInvoiceGenerateText(lstPrintInvoiceModel);
                    printDialog.AllowSomePages = true;

                    // Show the help button.
                    printDialog.ShowHelp = true;

                    // Set the Document property to the PrintDocument for 
                    // which the PrintPage Event has been handled. To display the
                    // dialog, either this property or the PrinterSettings property 
                    // must be set 
                    //ReadDocument();
                    using (System.Drawing.Printing.PrintDocument printDocument1 = new PrintDocument())
                    {
                        printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintInvoiceprintDocument1_PrintPage);
                        printDialog.Document = printDocument1;

                        DialogResult result = printDialog.ShowDialog();

                        // If the result is OK then print the document.
                        if (result == DialogResult.OK)
                        {
                            printDocument1.Print();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
        /// This is method is used to show print preview on window
        /// </summary>
        /// <param name="objRequestProcessingPrintModel"></param>
        public void PrintInvoicePrintPreview(List<PrintInvoiceModel> lstPrintInvoiceModel)// same for screen
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
               // PrintInvoiceGenerateText(lstPrintInvoiceModel);
                stringToPrint = PrintInvoiceGenerateText(lstPrintInvoiceModel);
                using (System.Windows.Forms.PrintPreviewDialog printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog())
                {
                    ((ToolStripButton)((ToolStrip)printPreviewDialog1.Controls[1]).Items[0]).Visible = false;
                    using (System.Drawing.Printing.PrintDocument print1 = new PrintDocument())
                    {

                        print1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintInvoiceprintDocument1_PrintPage);

                        printPreviewDialog1.Document = print1;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
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
        public void PrintInvoiceprintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                string documentContents = string.Empty;
                // e.Graphics.DrawImage(memoryImage, 0, 0);
                int charactersOnPage = 0;
                int linesPerPage = 0;

                // Sets the value of charactersOnPage to the number of characters 
                // of stringToPrint that will fit within the bounds of the page.
                e.Graphics.MeasureString(stringToPrint, Font,
                    e.MarginBounds.Size, StringFormat.GenericTypographic,
                    out charactersOnPage, out linesPerPage);

                // Draws the string within the bounds of the page.
                e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
                e.MarginBounds, StringFormat.GenericTypographic);
               stringToPrint = stringToPrint.Substring(charactersOnPage);


                // Remove the portion of the string that has been printed.
                if (stringToPrint != null)
                {
                   
                    // Check to see if more pages are to be printed.
                    e.HasMorePages = (stringToPrint.Length > 0);

                    // If there are no more pages, reset the string to be printed.
                    if (!e.HasMorePages)
                        stringToPrint = documentContents;
                }
             
            }
            catch (Exception ex)
            {
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
        /// This method is used to PrintI nvoice Save To ClipBoard
        /// </summary>
        /// <param name="lstPrintInvoiceModel"></param>
        public void PrintInvoiceSaveToClipBoard(List<PrintInvoiceModel> lstPrintInvoiceModel)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                System.Windows.Forms.Clipboard.SetText(PrintInvoiceGenerateText(lstPrintInvoiceModel));
            }
            catch (Exception ex)
            {
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

    }
}
