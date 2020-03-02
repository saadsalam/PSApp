using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.ViewModel.PortStorageInvoices
{
    public static class DelegateEventPortStorageInvoices
    {
        /// <summary>
        /// SetValueMethod function for genrateinvoice
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueCutOffDate(string value);
        public static event SetValueCutOffDate OnSetValueCutOffDate;
        public static void SetValueMethodCutOffDate(string value)
        {
            if (OnSetValueCutOffDate != null)
                OnSetValueCutOffDate(value);
        }
    }
}
