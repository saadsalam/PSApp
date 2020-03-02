using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class PortStorageDamageDetailsProp
    {
        public int PSVehicleDamageDetailID { get; set; }
        public Nullable<int> PSDamageClaimID { get; set; }
        public Nullable<int> PSVehicleInspectionID { get; set; }
        public string ClaimNumber { get; set; }
        public Nullable<int> PortStorageVehiclesID { get; set; }
        public string DamageCode { get; set; }
        public string DamageDescription { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public Nullable<System.DateTime> InsPDate { get; set; }
        public string InsPType { get; set; }
        public string InsPBy { get; set; }
        public int? Attened { get; set; }
        public string STI { get; set; }
        public int CleanVehicleId { get; set; }
        public int DamageDetailId { get; set; }
        public string BayLocationInfo { get; set; }
        public Nullable<DateTime> LocationDateInfo { get; set; }
        public string UpdatedbyInfo { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>
    public class PortStorageDamageDetailsStoredProcedureResult
    {
        public int? PSVehicleDamageDetailID { get; set; }

        public int PSVehicleInspectionID { get; set; }

        public DateTime? InspectionDate { get; set; }

        public string InspectedBy { get; set; }

        public string InspectionType { get; set; }

        public string SubjectToInspection { get; set; }

        public int? AttendedInd { get; set; }

        public string DamageCode { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }

}
