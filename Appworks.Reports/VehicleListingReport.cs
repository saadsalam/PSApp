using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appworks.Reports
{
   public class VehicleListingReport
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerName1 { get; set; }
        public string VIN { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateRequested { get; set; }
        public DateTime? DateOut { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string BayLocation { get; set; }
        public int UnitCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public string HeadingText { get; set; }
        public bool IsGrouped { get; set; }
        public int TempIndex { get; set; }
    }
}
