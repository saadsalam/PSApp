using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appworks.Reports
{
    public class InvoiceSummaryReport
    {
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string FaxNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneExtension { get; set; }
        public string UserCode { get; set; }

        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public double InvoiceAmount { get; set; }
        public int? Units { get; set; }
        public int? BillingID { get; set; }
        public string CompanyAddress { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
