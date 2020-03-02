using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appworks.Reports
{
   public class PrintInvoiceReport
    {
        
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int BillingAddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string InvoiceNumber { get; set; }
        public string VIN { get; set; }
        public string Model { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public decimal? DailyStorage { get; set; }
        public int? StorageDays { get; set; }
        public int? BilledDays { get; set; }
        public decimal? EntryRate { get; set; }
        public decimal? TotalCharge { get; set; }
        public decimal? StorageCharges { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddressLine1 { get; set; }
        public string CompanyAddressLine2 { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyPhone { get; set; }
        public string FullAddress { get; set; }
        public string Header { get; set; }
    }
}
