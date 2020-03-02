using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class DelegateEventRequestProcessing
    {
        #region ForReport
        public delegate void SetSortingParams(string sortColumn);
        public static event SetSortingParams OnReprintGridSorted;
        public static void SetOnSortedMethod(string value)
        {
            if (OnReprintGridSorted != null)
                OnReprintGridSorted(value);
        }
        #endregion 
    }
}
