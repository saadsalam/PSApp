using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class CustomerSearchProp
    {
        public string CustomerType { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerActualName { get; set; }
        public string CustomerCode { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string DBAName { get; set; }
        public string ShortName { get; set; }
        public string CustomerOf { get; set; }
        public int? BillingAddressID { get; set; }
        public int? MainAddressID { get; set; }

        public long TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int DefaultPageSize { get; set; }

        public decimal? EntryRate { get; set; }
        public int? PerDiemGraceDays { get; set; }
        
        /// Other Information Properties

        public string VendorNumber { get; set; }
        public string DefaultBillingMethod { get; set; }
        public string RecordStatus { get; set; }
        public int? SortOrder { get; set; }
        public int? EligibleForAutoLoad { get; set; }
        public int? ALwaysVVIPInd { get; set; }
        public int? CollectionIssueInd { get; set; }
        public int? AssignedToExternalCollectionsInd { get; set; }
        public int? BulkBillingInd { get; set; }
        public int? DoNotPrintInvoiceInd { get; set; }
        public int? DoNotExportInvoiceInd { get; set; }
        public int? HideNewFreightFromBrokerInd { get; set; }
        public int? PortStorageCustomerInd { get; set; }
        public int? AutoPortExportCustomerInd { get; set; }
        public int? RequiresPONumberInd { get; set; }
        public int? SendEmailConfirmationsInd { get; set; }
        public int? STIFollowupRequiredInd { get; set; }
        public int? DamagePhotoRequiredInd { get; set; }
        public int? ApplyFuelSurchargeInd { get; set; }
        public float? FuelSurchargePercent { get; set; }
        public string LoadNumberPrefix { get; set; }
        public int? LoadNumberLength { get; set; }
        public int? NextLoadNUmber { get; set; }
        public string BookingNumberPrefix { get; set; }
        public string HandHeldScannerCustomerCode { get; set; }
        public string InternalComment { get; set; }
        public string DriverComment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
