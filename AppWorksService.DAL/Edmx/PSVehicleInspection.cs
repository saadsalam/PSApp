
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
    
public partial class PSVehicleInspection
{

    public int PSVehicleInspectionID { get; set; }

    public Nullable<int> PortStorageVehiclesID { get; set; }

    public Nullable<int> InspectionType { get; set; }

    public Nullable<System.DateTime> InspectionDate { get; set; }

    public string InspectedBy { get; set; }

    public Nullable<int> DamageCodeCount { get; set; }

    public Nullable<int> AttendedInd { get; set; }

    public Nullable<int> SubjectToInspectionInd { get; set; }

    public Nullable<int> CleanVehicleInd { get; set; }

    public string Notes { get; set; }

    public Nullable<System.DateTime> CreationDate { get; set; }

    public string CreatedBy { get; set; }

    public Nullable<System.DateTime> UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

}

}