using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.Model
{
    public class BillingModel
    {
        public int BillingID { get; set; }
        public string InvoiceNumber { get; set; }
        public string Address { get; set; }
        public string CustomerNumber { get; set; }
        public string PONumber { get; set; }
        public string VIN { get; set; }
        public string OrderNumber { get; set; }
        public string CustIndent { get; set; }
        public string Driver { get; set; }
        public string CustomerName { get; set; }
        public string CarrierName { get; set; }
        public string InvoiceType { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string LoadNumber { get; set; }
        public string OutsideCarrier { get; set; }
        public string InvoiceStatus { get; set; }
        public long TotalPageCount { get; set; }
        public int? OutsideCarrierID { get; set; }
    }
}
