using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appworks.Reports
{
   public class PortStorageInBoundReport
    {
      
        public string VIN { get; set; }
        public string Location { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public DateTime? DateIn { get; set; }
        public string Baylocation { get; set; }
        public string Status { get; set; }
        public string LoadNumber { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerID { get; set; }

        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public string DeliveryBayLocation { get; set; }
    }
}
