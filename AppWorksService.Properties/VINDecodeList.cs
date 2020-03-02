using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class VINDecodeList
    {
        public int VINDecodeID { get; set; }
        public string VINSquish { get; set; }
        public string VehicleYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Bodystyle { get; set; }
        public string VehicleLength { get; set; }
        public string VehicleWidth { get; set; }
        public string VehicleHeight { get; set; }
        public string VehicleWeight { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public string DecodeError { get; set; }
        public string ErrorName { get; set; }


    }
}
