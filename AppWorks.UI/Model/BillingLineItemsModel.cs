﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.Model
{
    public class BillingLineItemsModel
    {
        public int BillingLineItemsID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> BillingID { get; set; }
        public string CustomerNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string DebitAccountNumber { get; set; }
        public string DebitProfitCenterNumber { get; set; }
        public string DebitCostCenterNumber { get; set; }
        public string CreditAccountNumber { get; set; }
        public string CreditProfitCenterNumber { get; set; }
        public string CreditCostCenterNumber { get; set; }
        public Nullable<decimal> ARTransactionAmount { get; set; }
        public Nullable<int> CreditMemoInd { get; set; }
        public string Description { get; set; }
        public Nullable<int> ExportedInd { get; set; }
        public Nullable<int> ExportBatchID { get; set; }
        public Nullable<System.DateTime> ExportedDate { get; set; }
        public string ExportedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
