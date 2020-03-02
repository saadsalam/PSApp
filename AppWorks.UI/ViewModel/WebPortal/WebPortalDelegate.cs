namespace AppWorks.UI.ViewModel.WebPortal
{
    public class WebPortalDelegate
    {
        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValue(string value);
        public static event SetValue OnSetValueEvt;
        public static void SetValueMethod(string value)
        {
            if (OnSetValueEvt != null)
                OnSetValueEvt(value);
        }

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
        /// SetValueMethod function for referesh grid
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueCmdReferesh(string value);
        public static event SetValueCmdReferesh OnSetValueEvtCmdReferesh;
        public static void SetValueMethodCmdReferesh(string value)
        {
            if (OnSetValueEvtCmdReferesh != null)
                OnSetValueEvtCmdReferesh(value);
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

        public delegate void SetValuePageNumberClick(int pageNumber);
        public static event SetValuePageNumberClick OnSetValuePageNumberCmd;
        public static void SetValueMethodPageNumber(int val)
        {
            if (OnSetValuePageNumberCmd != null)
                OnSetValuePageNumberCmd(val);
        }

        /// <summary>
        /// SetValueMethod function for total count
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueEditCmd(string value,object obj);
        public static event SetValueEditCmd  OnSetValueEvtEditCmd;
        public static void SetValueMethodEditCmd(string value,object obj)
        {
            if (OnSetValueEvtEditCmd != null)
                OnSetValueEvtEditCmd(value,obj);
        }
    }
}
