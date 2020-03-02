using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppWorks.UI.Models
{
    public class VehicleModel
    {
        static readonly DateTime DefaultDateTime = new DateTime(1900, 1, 1, 0, 0, 0);
        [DisplayName("Vehicles ID")]
        public int VehiclesID { get; set; }

        [DisplayName("CUstomer ID")]
        public int CustomerID { get; set; }

        [DisplayName("VIN")]
        public string Vin { get; set; }

        [DisplayName("YEAR")]
        public string Year { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Make Model")]
        public string MakeModel { get; set; }

        [DisplayName("Bay Location")]
        public string BayLocation { get; set; }

        private Nullable<System.DateTime> _dateIn;
        [DisplayName("Date In")]
        public Nullable<System.DateTime> DateIn
        {
            get
            {
                return GetVerifiedDate(_dateIn);
            }
            set { _dateIn = value; }
        }

        private Nullable<System.DateTime> _dateRequested;
        [DisplayName("Date Requested")]
        public Nullable<System.DateTime> DateRequested
        {
            get
            {
                return GetVerifiedDate(_dateRequested);
            }
            set { _dateRequested = value; }
        }

        private Nullable<System.DateTime> _dateOut;
        [DisplayName("Date Out")]
        public Nullable<System.DateTime> DateOut
        {
            get
            {
                return GetVerifiedDate(_dateOut);
            }
            set { _dateOut = value; }
        }

        private Nullable<System.DateTime> _dateInDateOut;
        [DisplayName("Date In Date Out")]
        public Nullable<System.DateTime> DateInDateOut
        {
            get
            {
                return GetVerifiedDate(_dateInDateOut);
            }
            set { _dateInDateOut = value; }
        }

        private Nullable<System.DateTime> _lastPhysicalDate;
        [DisplayName("Date In Date Out")]
        public Nullable<System.DateTime> LastPhysicalDate
        {
            get
            {
                return GetVerifiedDate(_lastPhysicalDate);
            }
            set { _lastPhysicalDate = value; }
        }

        private Nullable<System.DateTime> _creationDate;
        [DisplayName("CreationDate")]
        public Nullable<System.DateTime> CreationDate
        {
            get
            {
                return GetVerifiedDate(_creationDate);
            }
            set { _creationDate = value; }
        }

        private Nullable<System.DateTime> _updatedDate;
        [DisplayName("UpdatedDate")]
        public Nullable<System.DateTime> UpdatedDate
        {
            get
            {
                return GetVerifiedDate(_updatedDate);
            }
            set { _updatedDate = value; }
        }

        private Nullable<System.DateTime> _estimatedPickupDate;
        [DisplayName("EstimatedPickupDate")]
        public Nullable<System.DateTime> EstimatedPickupDate
        {
            get
            {
                return GetVerifiedDate(_estimatedPickupDate);
            }
            set { _estimatedPickupDate = value; }
        }

        [DisplayName("Vehicle Status")]
        public string VehicleStatus { get; set; }

        [DisplayName("Vecihle Val")]
        public int VecihleVal { get; set; }


        [DisplayName("Vecihle CreatedBy")]
        public string CreatedBy { get; set; }

        [DisplayName("Vecihle UpdatedBy")]
        public string UpdatedBy { get; set; }
        [DisplayName("Days")]
        public int? Days { get; set; }
        [DisplayName("TotalPageCount")]
        public long TotalPageCount { get; set; }
        [DisplayName("Color")]
        public string Color { get; set; }

        private Nullable<System.DateTime>_dealerPrintDate;
        [DisplayName("Dealer Print Date")]
        public Nullable<System.DateTime> DealerPrintDate
        {
            get
            {
                return GetVerifiedDate(_dealerPrintDate);
            }
            set { _dealerPrintDate = value; }
        }
        public decimal? EntryRate { get; set; }
        public int? PerDiemGraceDays { get; set; }
        public decimal? TotalCharge { get; set; }
        public decimal? DefaultEntryRate { get; set; }
        public decimal? DefaultPerDiem { get; set; }
        public int? DefaultPerDiemGraceDays { get; set; }
        public string AdddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Make { get; set; }
        public bool IsSelected { get; set; }
        public bool? IsProcessedRequestedOut { get; set; }
        
        private DateTime? GetVerifiedDate(DateTime? value)
        {
            var date = value == DefaultDateTime ? null : value;
            if (date.HasValue && date.Value.TimeOfDay == TimeSpan.Zero)
            {
                date = date.Value.Date;
            }

            return date;
        }
    }

    public class PSRatesInvoiceModel
    {
        public decimal? EntryFee { get; set; }
        public int? PerDiemGraceDays { get; set; }
    }
}
