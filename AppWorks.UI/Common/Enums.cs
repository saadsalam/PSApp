using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI
{
    public class Enums
    {
        /// <summary>
        /// 
        /// </summary>
        public enum PortStorageTabs
        {
            VehicleDetail = 0,
            VehicleLocator = 1
        }

        /// <summary>
        /// Depicts vehicle status as in Port Storage Vehicle table
        /// </summary>
        public enum VehicleStatus
        {
            InInventory = 0,
            OutGated = 1,
            Requested = 2
        }

        /// <summary>
        /// 
        /// </summary>
        public enum RecordStatus
        {
            Active = 0,
            Inactive = 1
        }

        /// <summary>
        /// Depicts the Invoice status in Billing table
        /// </summary>
        public enum InvoiceStatus
        {
            Pending = 0,
            Printed = 1
        }

        /// <summary>
        /// Depicts the Invoice status in Billing table
        /// </summary>
        public enum InvoiceType
        {
            /// <summary>
            /// For Port Storage only
            /// </summary>
            StorageCharge = 0,
            ExportCharge = 1,
            TransportCharge = 3
        }

        /// <summary>
        /// This enum creted for code type
        /// </summary>
        public enum CodeType
        {
            CustomerType,//Customer Type
            TruckingCompany,//Parent Company
            PaymentMethod,//Payment Method
            RecordStatus,//Record Status
            LocationType,//LocationType
            LocationSubType,//LocationSubType
            UserRole,//User Role
            InvoiceType//For Invoice Type
        }
        /// <summary>
        /// This enum creted for printmodule
        /// </summary>
        public enum PrintEnum
        {
            portstorage,
            requestprocessing,
            printerrorinvoice,
            printinvoice
        }


        public enum VehicleStatusType
        {
            OutGated,// OutGated
            InInventory,// In Inventory
            Requested//Requested
        }
    }
}
