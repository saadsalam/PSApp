using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class PortStorageInventoryList
    {
        public int? BatchID { get; set; }
        public DateTime? CreattionDate { get; set; }
        public int? RecordsCount { get; set; }
        public string VIN { get; set; }
        public string Location { get; set; }
        public int? ImportedInd { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public DateTime? DateIn { get; set; }
        public string Baylocation { get; set; }
        public string Status { get; set; }
        public string LoadNumber { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerID { get; set; }

        public string DeliveryBayLocation { get; set; }

    }
}
