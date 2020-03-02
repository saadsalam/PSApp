namespace AppWorks.UI.ViewModel.Vehicle
{
    public static class DelegateEventCustomer
    {

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetCustomerValue(CustomerInfo customerinfo);
        public static event SetCustomerValue OnSetCustomerValueEvt;
        public static void SetCustomerValueMethod(CustomerInfo value)
        {
            if (OnSetCustomerValueEvt != null)
                OnSetCustomerValueEvt(value);
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

    }
}
