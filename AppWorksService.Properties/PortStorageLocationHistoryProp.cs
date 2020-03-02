using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class PortStorageLocationHistoryProp
    {
        public int PortStorageVehiclesLocationHistoryID { get; set; }
        public int PortStorageVehicalsID { get; set; }
        public string BayLocation { get; set; }
        public Nullable<DateTime> LocationDate { get; set; }
        public Nullable<DateTime> CreationDate { get; set; }
        public string Createdby { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public string Updatedby { get; set; }
    }
}
