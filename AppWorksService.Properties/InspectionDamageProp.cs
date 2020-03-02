using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class InspectionDamageProp
    {
        public string PSVehicleInspectionID { get; set; }
        public int PSVehicleDamageDetailID { get; set; }
        public string InspectionType { get; set; }
        public string InspectedBy { get; set; }
        public string AttendedInd { get; set; }
        public Nullable<System.DateTime> InspectionDate { get; set; }
        public string SubjectToInspectionInd { get; set; }
        public string CleanVehicleInd { get; set; }
        public string DamageCode { get; set; }
        public string VehicleStatus { get; set; }
        public string CodeValue { get; set; }
        public string CodeDescription { get; set; }
    }

    public class InspectionTypeList
    {
        public int InspectionTypeID { get; set; }
        public string InspectionTypeName { get; set; }
    }
}
