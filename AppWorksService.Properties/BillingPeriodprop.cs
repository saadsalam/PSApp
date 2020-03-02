using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public partial class BillingPeriodprop
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
        public bool PeriodClosed { get; set; }
        public long TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int DefaultPageSize { get; set; }
    }

    public partial class BillingProp
    {
        public string CustomerAddress { get; set; }
        public int BillingID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> OutsideCarrierInvoiceInd { get; set; }
        public Nullable<int> OutsideCarrierID { get; set; }
        public Nullable<int> RunID { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceType { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<decimal> TransportCharges { get; set; }
        public Nullable<decimal> FuelSurchargeRate { get; set; }
        public Nullable<int> FuelSurchargeRateOverrideInd { get; set; }
        public Nullable<decimal> FuelSurcharge { get; set; }
        public Nullable<int> FuelSurchargeOverrideInd { get; set; }
        public Nullable<decimal> OtherCharge1 { get; set; }
        public string OtherCharge1Description { get; set; }
        public Nullable<decimal> OtherCharge2 { get; set; }
        public string OtherCharge2Description { get; set; }
        public Nullable<decimal> OtherCharge3 { get; set; }
        public string OtherCharge3Description { get; set; }
        public Nullable<decimal> OtherCharge4 { get; set; }
        public string OtherCharge4Description { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public Nullable<decimal> CreditsIssued { get; set; }
        public Nullable<decimal> BalanceDue { get; set; }
        public Nullable<decimal> DueToOutsideCarriers { get; set; }
        public Nullable<decimal> DueFromOutsideCarriers { get; set; }
        public Nullable<int> PaidInFullInd { get; set; }
        public Nullable<System.DateTime> PaidInFullDate { get; set; }
        public string Comments { get; set; }
        public string InvoiceStatus { get; set; }
        public Nullable<int> PrintedInd { get; set; }
        public Nullable<System.DateTime> DatePrinted { get; set; }
        public Nullable<int> CreditMemoInd { get; set; }
        public Nullable<int> CreditedOutInd { get; set; }
        public Nullable<int> CreditMemoID { get; set; }
        public Nullable<int> ExportedInd { get; set; }
        public Nullable<System.DateTime> DateExported { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string CreditedOutBy { get; set; }
        public Nullable<decimal> StorageCharges { get; set; }
        public Nullable<int> DATBillingID { get; set; }
        public Nullable<decimal> DATBillingPercentage { get; set; }



        // billing find ----------------------------------------------------------
        public string CarrierName { get; set; }
        public string LoadNumber { get; set; }

        // Search by properties
        public string CustomerName { get; set; }
        public string PONumber { get; set; }
        public string OrderNumber { get; set; }
        public string Vin { get; set; }
        public string CustomerNumber { get; set; }
        public string CustIndent { get; set; }
        public Nullable<DateTime> CreatedBetwDtFrm { get; set; }
        public Nullable<DateTime> CreatedBetwDtTo { get; set; }
        public string Driver { get; set; }
        public Nullable<DateTime> DtInvBetwDtFrm { get; set; }
        public Nullable<DateTime> DtInvBetwDtTo { get; set; }
        public string OutsideCarrier { get; set; }
        public Nullable<DateTime> DtPaidBetwDtFrm { get; set; }
        public Nullable<DateTime> DtPaidBetwDtTo { get; set; }
        public long TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int DefaultPageSize { get; set; }
        // billing find ----------------------------------------------------------
    }
}
