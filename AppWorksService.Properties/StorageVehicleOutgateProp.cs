using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class StorageVehicleOutgateProp
    {
        public string Vin { get; set; }
        public Nullable<System.DateTime> DateOut { get; set; }
        public int? ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string Note { get; set; }
        public string User { get; set; }
    }
}
