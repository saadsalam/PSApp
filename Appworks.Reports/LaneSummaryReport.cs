using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appworks.Reports
{
   public class LaneSummaryReport
    {
        public string CustomerName { get; set; }
        public string Baylocation { get; set; }
        public int? RecordsCount { get; set; }

        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}
