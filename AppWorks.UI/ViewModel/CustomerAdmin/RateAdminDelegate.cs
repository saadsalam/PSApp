using AppWorksService.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.ViewModel.CustomerAdmin
{
    class RateAdminDelegate
    {
        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetPortStorageRateAdminValue(PortStorageRateList value);
        public static event SetPortStorageRateAdminValue OnSetRateAdminValueEvt;
        public static void SetRateAdminValueMethod(PortStorageRateList value)
        {
            if (OnSetRateAdminValueEvt != null)
                OnSetRateAdminValueEvt(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetPortStorageRateModified (bool value);
        public static event SetPortStorageRateModified OnNotifyBindEvt;
        public static void SetRateAdminModifiedMethod(bool value)
        {
            if (OnNotifyBindEvt != null)
                OnNotifyBindEvt(value);
        }
    }
}
