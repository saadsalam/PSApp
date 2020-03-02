using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class VehicleProp
    {
        public int VehiclesID { get; set; }
        public string Vin { get; set; }
        public string Name { get; set; }
        public string MakeModel { get; set; }
        public string BayLocation { get; set; }
        public Nullable<System.DateTime> DateIn { get; set; }
        public Nullable<System.DateTime> DateRequested { get; set; }
        public Nullable<System.DateTime> DateOut { get; set; }
        public Nullable<System.DateTime> DateInDateOut { get; set; }
        public string VehicleStatus { get; set; }
        public int VecihleVal { get; set; }

        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public Nullable<DateTime> InBetwDtFrm { get; set; }
        public Nullable<DateTime> InBetwDtTo { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public Nullable<DateTime> DtInBetwDtFrm { get; set; }
        public Nullable<DateTime> DtInBetwDtTo { get; set; }
        public Nullable<DateTime> DtRequestBetwDtFrm { get; set; }
        public string CustIndent { get; set; }
        public Nullable<DateTime> DtRequestBetwDtTo { get; set; }
        public Nullable<DateTime> DtOutBetwDtFrm { get; set; }
        public Nullable<DateTime> DtOutBetwDtTo { get; set; }

        /// Extra properties for insert vehicle details
        public int CustomerID { get; set; }
        public string Bodystyle { get; set; }
        public string Color { get; set; }
        public string VehicleLength { get; set; }
        public string VehicleWidth { get; set; }
        public string VehicleHeight { get; set; }
        public string CustomerIdentification { get; set; }
        public string SizeClass { get; set; }
        public decimal? EntryRate { get; set; }
        public int EntryRateOverrideInd { get; set; }
        public int? PerDiemGraceDays { get; set; }
        public int PerDiemGraceDaysOverrideInd { get; set; }
        public decimal? TotalCharge { get; set; }
        public int BilledInd { get; set; }
        public int BillingID { get; set; }
        public Nullable<DateTime> DateBilled { get; set; }
        public int VinDecodedInd { get; set; }
        public string Note { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int CreditHoldInd { get; set; }
        public string CreditHoldBy { get; set; }
        public int RequestPrintedInd { get; set; }
        public Nullable<DateTime> EstimatedPickupDate { get; set; }
        public Nullable<DateTime> DealerPrintDate { get; set; }
        public string DealerPrintBy { get; set; }
        public string RequestedBy { get; set; }
        public int RequestPrintedBatchID { get; set; }
        public Nullable<DateTime> DateRequestPrinted { get; set; }
        public Nullable<DateTime> LastPhysicalDate { get; set; }
        public int? DiffDays { get { return (DateOut == null ? (DateTime.Now - DateIn.GetValueOrDefault()).Days : (DateOut.GetValueOrDefault() - DateIn.GetValueOrDefault()).Days); } }
        public long TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int DefaultPageSize { get; set; }
        public decimal? DefaultEntryRate { get; set; }
        public decimal? DefaultPerDiem { get; set; }
        public int? DefaultPerDiemGraceDays { get; set; }
        public string AdddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string SortOrder { get; set; }
        public string SortColumn { get; set; }

        public bool ReturnToInventory
        {
            get
            {
                if (DateRequested == null)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
