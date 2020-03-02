using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class BillingInvoiceDetailsProp
    {
        public int? RunID { get; set; }
        public int? VehicleID { get; set; }
        public string VIN { get; set; }
        public string LoadNumber { get; set; }
        public int? PickUpLocationID { get; set; }
        public int? DropOffLocationID { get; set; }
        public int? OrderID { get; set; }
        public string PONumber { get; set; }
        public decimal? ChargeRate { get; set; }
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
        public string OrderNumber { get; set; }

        public int BillingDetailID { get; set; }
        public Nullable<int> BillingID { get; set; }
        public string ItemType { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<System.DateTime> ItemDate { get; set; }
        public Nullable<decimal> Units { get; set; }
        public Nullable<decimal> PricePerUnit { get; set; }
        public Nullable<decimal> ExtendedPrice { get; set; }
        public string GLAccount { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }


    }
}
