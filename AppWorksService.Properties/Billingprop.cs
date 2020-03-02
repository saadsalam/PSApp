using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public partial class Billingprop
    {
        public int BillingPeriodID { get; set; }
        public Nullable<int> PeriodNumber { get; set; }
        public Nullable<int> CalendarYear { get; set; }
        public Nullable<System.DateTime> PeriodEndDate { get; set; }
        public Nullable<int> PeriodClosedInd { get; set; }
        public string PeriodClosedBy { get; set; }
        public Nullable<System.DateTime> PeriodClosedDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
