﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appworks.Reports
{
   public class PrintRequestReport
    {
        public string VehiclesID { get; set; }
        public string Vin { get; set; }
        public string Name { get; set; }
        public string MakeModel { get; set; }
        public string BayLocation { get; set; }
        public string Color { get; set; }
        public string ShortVIN { get; set; }
        public Nullable<System.DateTime> DateRequested { get; set; }
        public Nullable<System.DateTime> DateOut { get; set; }
        public Nullable<System.DateTime> PickeupDate { get; set; }
        public Nullable<System.DateTime> DealerPrintDate { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CompanyCity { get; set; }
        public string Phone { get; set; }
        public string AdddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public Nullable<System.DateTime> ReturnToInventory { get; set; }
        public string DayName { get; set; }
        public string DayNamePickUp { get; set; }
        public string FullAddress { get; set; }
    }
}
