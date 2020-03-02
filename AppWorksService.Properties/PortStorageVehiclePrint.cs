using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
  public  class PortStorageVehiclePrint
    {
      public string Date { get; set; }
      public string VIN { get; set; }
      public string VINSort { get; set; }
      public DateTime? DateRequested { get; set; }
      public DateTime? EstPickupDate { get; set; }
      public DateTime? DateOutgated { get; set; }
      public string DateRequestDay { get; set; }
      public string Model { get; set; }
      public string ExtColor { get; set; }
      public string ShipTo { get; set; }
      public string Location { get; set; }
      public string ReturnToInventory { get; set; }

    }
}
