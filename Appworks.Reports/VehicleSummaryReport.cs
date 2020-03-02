using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appworks.Reports
{
   public class VehicleSummaryReport
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int UnitCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string HeadingText { get; set; }
      
    }
}
