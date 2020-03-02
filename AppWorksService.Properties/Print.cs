using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorksService.Properties;

namespace WindowsFormsApplication1
{
    public class Print
    {
        public string PrintModule { get; set; }
        public string PrintType { get; set; }
        StringBuilder sb = new StringBuilder();
        StringBuilder sbText = new StringBuilder();
        PortStorageVehiclePrint objPortStorageVehiclePrint = new PortStorageVehiclePrint();
        /// <summary>
        /// This Method is used to print data
        /// </summary>
        /// <param name="PrintModule"></param>
        /// <param name="PrintType"></param>
        /// <param name="portStorageVehiclePrint"></param>
        public void PrintData(string PrintModule, string PrintType, PortStorageVehiclePrint portStorageVehiclePrint)
        {
            try
            {
                if (PrintModule.ToLower().Equals("portstorage"))
                {
                    if (PrintType.ToLower().Equals("html"))
                    {
                        GenerateHTML(portStorageVehiclePrint);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// This method is used to genrate HTML format
        /// </summary>
        /// <param name="portStorageVehiclePrint"></param>
        public void GenerateHTML(PortStorageVehiclePrint portStorageVehiclePrint)
        {
            try
            {
                sb.Append("<html><BODY bgcolor='#FFFFFF' text='#000000' link='#808080' vlink='#808080' alink='#808080'>");
                sb.Append("<TABLE BORDER='0' CELLPADDING='0' CELLSPACING='0' WIDTH='737' HEIGHT='77'>");
                sb.Append("<TD VALIGN=TOP WIDTH=164></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=287>");
                sb.Append("<FONT FACE='Arial'>Diversified Automotive<BR>100 Terminal Street<BR>Charlestown,  MA   02129<BR>(800) 666-9007</FONT> </TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=181></TD>");
                sb.Append("<TD VALIGN=TOP ALIGN=RIGHT WIDTH=102>");
                sb.Append("<FONT FACE='Arial' SIZE='2'>Date: " + portStorageVehiclePrint.Date + "</FONT></TD>");
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
                sb.Append("<TD VALIGN=TOP WIDTH=130>");
                sb.Append("<FONT FACE='3 of 9 Barcode' SIZE='5' COLOR='#000011'>*" + portStorageVehiclePrint.VINSort + "*</FONT></TD>");
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
                sb.Append("<TD VALIGN=TOP WIDTH=108>");
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Est. Pickup Date:</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=7></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=173></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=0>");
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
                sb.Append("<FONT FACE='Arial' SIZE='2'><B>Pickup Up By:</B></FONT></TD>");
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
                sb.Append("<FONT FACE='Arial' SIZE='7' COLOR='#000011'><B>" + portStorageVehiclePrint.ReturnToInventory + "</B></FONT></TD>");
                sb.Append("<TD VALIGN=TOP WIDTH=113></TD></TABLE>");
                sb.Append("</BODY></html>");

            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// This method is used to save to file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        public void SaveToFile(string fileName, FileType fileType)
        {
            try
            {

                System.IO.TextWriter w = null;
                switch (fileType)
                {
                    case FileType.html:
                        GenerateHTML(objPortStorageVehiclePrint);
                        w = new System.IO.StreamWriter(fileName + "." + FileType.html.ToString());
                        w.Write(sb.ToString());
                        w.Flush();
                        w.Close();
                        break;
                    case FileType.ps:
                        GenerateHTML(objPortStorageVehiclePrint);
                        w = new System.IO.StreamWriter(fileName + "." + FileType.ps.ToString());
                        w.Write(sb.ToString());
                        w.Flush();
                        w.Close();
                        break;
                    case FileType.rtf:
                        GenerateHTML(objPortStorageVehiclePrint);
                        w = new System.IO.StreamWriter(fileName + "." + FileType.rtf.ToString());
                        w.Write(sb.ToString());
                        w.Flush();
                        w.Close();
                        break;
                    case FileType.prn:
                        w = new System.IO.StreamWriter(fileName + "." + FileType.prn.ToString());
                        w.Write(GenerateText(objPortStorageVehiclePrint));
                        w.Flush();
                        w.Close();
                        break;
                    case FileType.rep:
                        w = new System.IO.StreamWriter(fileName + "." + FileType.rep.ToString());
                        w.Write(GenerateText(objPortStorageVehiclePrint));
                        w.Flush();
                        w.Close();
                        break;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// This method is used to Genrate Text format.
        /// </summary>
        /// <param name="portStorageVehiclePrint"></param>
        /// <returns></returns>
        public string GenerateText(PortStorageVehiclePrint portStorageVehiclePrint)
        {
            try
            {
                sbText.Append("Diversified Automotive").Append(' ', 45).Append("Date: " + portStorageVehiclePrint.Date + "");
                sbText.Append(Environment.NewLine);
                sbText.Append("100 Terminal Street");
                sbText.Append(Environment.NewLine);
                sbText.Append("Charlestown,  MA   02129");
                sbText.Append(Environment.NewLine);
                sbText.Append("(800) 666-9007");
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
                sbText.Append(' ', 53).Append("*" + portStorageVehiclePrint.VINSort + "*");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Date Request" + portStorageVehiclePrint.DateRequested + "" + portStorageVehiclePrint.DateRequestDay + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Est. Pickup Date:");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Date Outgate" + portStorageVehiclePrint.DateOutgated + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Model:").Append(' ', 45).Append("" + portStorageVehiclePrint.Model + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Ext Color:  " + portStorageVehiclePrint.ExtColor + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Ship To:    Herb Chambers BMW/Mini");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("1168 Commonwealth Ave.");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Allston, MA 02134");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Location:   " + portStorageVehiclePrint.Location + "");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Pickup Up By:");
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Dated:");
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
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append(Environment.NewLine);
                sbText.Append("Return To Inventory On:");
                sbText.Append(Environment.NewLine);
                sbText.Append("" + portStorageVehiclePrint.ReturnToInventory + "");
                return sbText.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
