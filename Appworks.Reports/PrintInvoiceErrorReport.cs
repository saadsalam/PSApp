using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appworks.Reports
{
    public class PrintInvoiceErrorReport
    {
        public int RunID { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string VIN { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddressLine1 { get; set; }
        public string CompanyAddressLine2 { get; set; }
        public string CompnayCity { get; set; }
        public string CompanyPhone { get; set; }
    }
}
