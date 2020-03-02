using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWorks.UI.Model;
using System.Collections.ObjectModel;

namespace AppWorks.UI.ViewModel.Location
{
    public class LocationDeligate
    {

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetLocationValue(LocationModel value);
        public static event SetLocationValue OnSetLocationValueEvt;
        public static void SetValueLocationMethod(LocationModel value)
        {
            if (OnSetLocationValueEvt != null)
                OnSetLocationValueEvt(value);
        }

        ///// <summary>
        ///// SetValueListMethod function
        ///// </summary>
        ///// <param name="value"></param>
        //public delegate void SetLocationListValue(ObservableCollection<LocationModel> value);
        //public static event SetLocationListValue OnSetLocationListValueEvt;
        //public static void SetValueLocationListMethod(ObservableCollection<LocationModel> value)
        //{
        //    if (OnSetLocationListValueEvt != null)
        //        OnSetLocationListValueEvt(value);
        //}


        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueCmd(string value);
        public static event SetValueCmd OnSetValueEvtCmd;
        public static void SetValueMethodCmd(string value)
        {
            if (OnSetValueEvtCmd != null)
                OnSetValueEvtCmd(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueUpdateCmd(string value);
        public static event SetValueUpdateCmd OnSetValueEvtUpdateCmd;
        public static void SetValueMethodUpdateCmd(string value)
        {
            if (OnSetValueEvtUpdateCmd != null)
                OnSetValueEvtUpdateCmd(value);
        }

        /// <summary>
        /// SetValueMethod function for total count
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueTotalCountPager(long value);
        public static event SetValueTotalCountPager OnSetValueEvtTotalCountPagerCmd;
        public static void SetValueMethodTotalCountPager(long value)
        {
            if (OnSetValueEvtTotalCountPagerCmd != null)
                OnSetValueEvtTotalCountPagerCmd(value);
        }

        /// <summary>
        /// this delegate is used to get pagenumber
        /// </summary>
        /// <param name="pageNumber"></param>
        public delegate void SetValuePageNumberClick(int pageNumber);
        public static event SetValuePageNumberClick OnSetValuePageNumberCmd;
        public static void SetValueMethodPageNumber(int val)
        {
            if (OnSetValuePageNumberCmd != null)
                OnSetValuePageNumberCmd(val);
        }


    }
}
