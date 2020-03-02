using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class AutoExportRateList
    {
        public int AutoportExportRatesID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<decimal> EntryFee { get; set; }
        public Nullable<decimal> PerDiem { get; set; }
        public Nullable<int> PerDiemGraceDays { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string RateType { get; set; }
    }
}
