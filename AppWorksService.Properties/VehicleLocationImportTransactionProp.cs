using System;

namespace AppWorksService.Properties
{
    public class VehicleLocationImportTransactionProp
    {
        public string VinNumberAndVinKey { get; set; }
        public string Row { get; set; }
        public string Bay { get; set; }
        public bool ByRowFlag { get; set; }
        public Nullable<System.DateTime> HandheldActionDate { get; set; }
        public string UserCode { get; set; }
        public string DealerCode { get; set; }
        public string ColorCode { get; set; }
        public string DamageCode { get; set; }

    }
}
