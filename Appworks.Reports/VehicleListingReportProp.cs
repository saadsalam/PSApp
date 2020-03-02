using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appworks.Reports
{
    public class VehicleListingReportProp
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string VIN { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateRequested { get; set; }
        public DateTime? DateOut { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string BayLocation { get; set; }
        public int Count { get; set; }

    }
}
