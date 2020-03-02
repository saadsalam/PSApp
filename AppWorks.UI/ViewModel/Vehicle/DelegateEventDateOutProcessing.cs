using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public static class DelegateEventDateAndVehicleProcessing
    {
        /// <summary>
        /// SetValueMethod function for reusing exisitng commands
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValue(string value);
        public static event SetValue OnSetValueEvt;
        public static void SetValueMethod(string value)
        {
            if (OnSetValueEvt != null)
                OnSetValueEvt(value);
        }

    }
}
