using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.Model
{
    public class GenerateInvoiceModel
    {
    }

    public class LoadInfoListModel
    {
        public int OutsideCarrierID { get; set; }
        public int VehicleID { get; set; }
        public int LegsId { get; set; }
        public int LoadID { get; set; }
        public decimal? ChargeRate { get; set; }
        public decimal? OutsideCarrierBasePay { get; set; }
        public decimal? OutsideCarrierFuelSCPay { get; set; }
        public decimal? OutsideCarrierTotalPay { get; set; }
        public string OutsideCarrierName { get; set; }
        public int LoadNumber { get; set; }
        public int ExportedInd { get; set; }
        public int ElectronicBillOfLadingInd { get; set; }
    }

    public class VehicleLegCountModel
    {
        public int LegsCount1 { get; set; }
        public int LegsCount2 { get; set; }
    }
}
