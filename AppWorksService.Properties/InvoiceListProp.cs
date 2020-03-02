using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public  class InvoiceListProp
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public double InvoiceAmount { get; set; }
        public decimal InvAmount { get; set; }
        public int? Units { get; set; }
        public int? BillingID { get; set; }

    }
}
