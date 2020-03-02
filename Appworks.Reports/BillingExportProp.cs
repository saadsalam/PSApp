using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appworks.Reports
{
    public class BillingExportProp
    {
          public string CompanyName { get; set; }
          public DateTime? ExportDate { get; set; }
          public int BatchID { get; set; }
          public int CustomerID { get; set; }
          public string Invoice { get; set; }  
          public DateTime? InvoiceDate { get; set; }
          public string DebitAcc { get; set; }
          public string DebitProfit { get; set; }
          public string DebitCost { get; set; }

          public string CreditAcc { get; set; }
          public string CreditProfit { get; set; }
          public string CreditCost { get; set; }

          public decimal? TransAmount { get; set; }
          public string Description { get; set; }
          public decimal? TotalCharge { get; set; }

    }
}
