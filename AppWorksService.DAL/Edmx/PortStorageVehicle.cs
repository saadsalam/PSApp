
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace AppWorksService.DAL.Edmx
{

using System;
    using System.Collections.Generic;
    
public partial class PortStorageVehicle
{

    public int PortStorageVehiclesID { get; set; }

    public Nullable<int> CustomerID { get; set; }

    public string VehicleYear { get; set; }

    public string Make { get; set; }

    public string Model { get; set; }

    public string Bodystyle { get; set; }

    public string VIN { get; set; }

    public string Color { get; set; }

    public string VehicleLength { get; set; }

    public string VehicleWidth { get; set; }

    public string VehicleHeight { get; set; }

    public string VehicleStatus { get; set; }

    public string CustomerIdentification { get; set; }

    public string SizeClass { get; set; }

    public string BayLocation { get; set; }

    public Nullable<decimal> EntryRate { get; set; }

    public Nullable<int> EntryRateOverrideInd { get; set; }

    public Nullable<int> PerDiemGraceDays { get; set; }

    public Nullable<int> PerDiemGraceDaysOverrideInd { get; set; }

    public Nullable<decimal> TotalCharge { get; set; }

    public Nullable<System.DateTime> DateIn { get; set; }

    public Nullable<System.DateTime> DateRequested { get; set; }

    public Nullable<System.DateTime> DateOut { get; set; }

    public Nullable<int> BilledInd { get; set; }

    public Nullable<int> BillingID { get; set; }

    public Nullable<System.DateTime> DateBilled { get; set; }

    public Nullable<int> VINDecodedInd { get; set; }

    public string Note { get; set; }

    public string RecordStatus { get; set; }

    public Nullable<System.DateTime> CreationDate { get; set; }

    public string CreatedBy { get; set; }

    public Nullable<System.DateTime> UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public Nullable<int> CreditHoldInd { get; set; }

    public string CreditHoldBy { get; set; }

    public Nullable<int> RequestPrintedInd { get; set; }

    public Nullable<System.DateTime> EstimatedPickupDate { get; set; }

    public Nullable<System.DateTime> DealerPrintDate { get; set; }

    public string DealerPrintBy { get; set; }

    public string RequestedBy { get; set; }

    public Nullable<int> RequestPrintedBatchID { get; set; }

    public Nullable<System.DateTime> DateRequestPrinted { get; set; }

    public Nullable<System.DateTime> LastPhysicalDate { get; set; }

}

}
