using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
    public class PortStorageVehicleImportProp
    {
        public int PortStorageVehiclesLocationImportID { get; set; }
        public int PortStorageVehiclesImportID { get; set; }
        public int? BatchId { get; set; }
        public string Vin { get; set; }
        public string DateIn { get; set; }
        public string DealerCode { get; set; }
        public string ModelYear { get; set; }
        public string ModelName { get; set; }
        public string Color { get; set; }
        public string Location { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public int? BatchCount { get; set; }
        public string Message { get; set; }
        public int? ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public long TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int DefaultPageSize { get; set; }

    }
}
