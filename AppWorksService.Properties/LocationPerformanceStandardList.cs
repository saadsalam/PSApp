using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class LocationPerformanceStandardList
    {
        public int LocationPerformanceStandardsID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> OriginID { get; set; }
        public Nullable<int> DestinationID { get; set; }
        public Nullable<int> StandardDays { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<int> StandardBasis { get; set; }
    }
}
