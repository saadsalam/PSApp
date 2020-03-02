using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.Model
{
    public class PrintInvoiceErrorModel
    {
        public int RunID { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string VIN { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}
