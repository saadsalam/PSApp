using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorksService.Properties;

namespace AppWorks.UI.ViewModel.PortStorageImportExport
{
    public static class DelegateEventVehicleImport
    {
        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetLoadBatchValue(int BatchId);
        public static event SetLoadBatchValue OnSetLoadBatchValueEvt;
        public static void SetLoadBatchValueMethod(int value)
        {
            if (OnSetLoadBatchValueEvt != null)
                OnSetLoadBatchValueEvt(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetFilterVinValue(string Vin);
        public static event SetFilterVinValue OnSetFilterVinValueEvt;
        public static void SetFilterVinValueMethod(string value)
        {
            if (OnSetFilterVinValueEvt != null)
                OnSetFilterVinValueEvt(value);
        }
    }
}
