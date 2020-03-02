using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class PortStorageInvoicesProp
    {
        public int CustomerId { get; set; }
        public int BillingAddressID { get; set; }
        public string CustomerCode { get; set; }
        public string CustName { get; set; }
        public int Unit { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class PortStoragePrintErrorProp
    {
        public int RunID { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string VIN { get; set; }
    }

    public class PSRatesInvoiceProp
    {
        public decimal? EntryFee { get; set; }
        public int? PerDiemGraceDays { get; set; }
    }

    public class LoadInfoList
    {
        public int OutsideCarrierID { get; set; }
        public int VehicleID { get; set; }
        public int LegsID { get; set; }
        public int LoadID { get; set; }
        public decimal? ChargeRate { get; set; }
        public decimal? OutsideCarrierBasePay { get; set; }
        public decimal? OutsideCarrierFuelSCPay { get; set; }
        public decimal? OutsideCarrierTotalPay { get; set; }
        public string OutsideCarrierName { get; set; }
        public int LoadNumber { get; set; }
        public int ExportedInd { get; set; }
        public int ElectronicBillOfLadingInd { get; set; }
    }

    public class VehicleLegCountProp
    {
        public int LegsCount1 { get; set; }
        public int LegsCount2 { get; set; }
    }

    public class PortStoragePrintInvoiceProp
    {
        public int BillingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int BillingAddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string InvoiceType { get; set; }
        public string LoadNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string PONumber { get; set; }
        public int OrdersID { get; set; }
        public string DriverNumber { get; set; }
        public string CarrierName { get; set; }
        public string VIN { get; set; }
        public string VehicleYear { get; set; }
        public string Model { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public decimal? OtherCharge1 { get; set; }
        public decimal? OtherCharge2 { get; set; }
        public decimal? OtherCharge3 { get; set; }
        public decimal? OtherCharge4 { get; set; }
        public decimal? EntryRate { get; set; }
        public decimal? TotalCharge { get; set; }
        public decimal? StorageCharges { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? DailyStorage { get; set; }
        public int? StorageDays { get; set; }
        public int? BilledDays { get; set; }
        public string Header { get; set; }
    }
}
