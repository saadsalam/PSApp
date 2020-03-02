using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorksService.Properties
{
   public class LocationEmailContactList
    {
        public int LocationEmailContactsID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string GreetingName { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<int> HTMLEmailSupportedInd { get; set; }
        public Nullable<int> PickupNoticeInd { get; set; }
        public Nullable<int> STIDeliveryNoticeInd { get; set; }
        public Nullable<int> BillOfLadingInd { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<int> BookingRecordInd { get; set; }
    }
}
