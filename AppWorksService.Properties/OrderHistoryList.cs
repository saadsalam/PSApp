using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
   public class OrderHistoryList
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public string DropOffLocationName { get; set; }
        public int? DropOffLocationID { get; set; }
        public string PickUpLocationName { get; set; }
        public int? PickUpLocationID { get; set; }
        public int? Units { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
