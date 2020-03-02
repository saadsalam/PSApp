using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class DamageCodeProp
    {
        public Nullable<int> PortStorageVehiclesID { get; set; }
        public Nullable<int> PSVehicleInspectionID { get; set; }
        public string DamageCode { get; set; }
        public string DamageDescription { get; set; }
        public Nullable<System.DateTime> InspectionDate { get; set; }
        public string InspectedBy { get; set; }
        public string SelectedInspectionType { get; set; }
    }
}
