using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class PortStorageRequestsReportProp
    {
        public int PortStorageVehiclesID { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? RequestPrintedBatchID { get; set; }
        public Nullable<System.DateTime> DateRequestPrinted { get; set; }
        public int PortStorageVehicleId { get; set; }
        public string VIN { get; set; }
        public string ShortVIN { get; set; }
        public Nullable<System.DateTime> DateRequested { get; set; }
        public string VehicleYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string AdddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string BayLocation { get; set; }
        public Nullable<System.DateTime> DateOut { get; set; }
        public Nullable<System.DateTime> EstimatedPickupDate { get; set; }
        public Nullable<System.DateTime> DealerPrintDate { get; set; }
        public int BatchCount { get; set; }
        
    }
}
