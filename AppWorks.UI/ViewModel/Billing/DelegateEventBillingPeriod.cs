using System.Collections.Generic;
using AppWorksService.Properties;

namespace AppWorks.UI.ViewModel.Billing
{
    public static class DelegateEventBillingPeriod
    {
        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetBillingPeriodValue(BillingPeriodprop billingPeriodAdminInfo);
        public static event SetBillingPeriodValue OnSetBillingPeriodValueEvt;
        public static void SetBillingPeriodValueMethod(BillingPeriodprop value)
        {
            if (OnSetBillingPeriodValueEvt != null)
                OnSetBillingPeriodValueEvt(value);
        }

        /// <summary>
        /// SetValueMethod function for referesh all value
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetBillingPeriodValueCmdReferesh(BillingPeriodprop value);
        public static event SetBillingPeriodValueCmdReferesh OnSetBillingPeriodValueEvtCmdReferesh;
        public static void SetBillingPeriodValueMethodCmdReferesh(BillingPeriodprop value)
        {
            if (OnSetBillingPeriodValueEvtCmdReferesh != null)
                OnSetBillingPeriodValueEvtCmdReferesh(value);
        }

        #region "billing find"

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

        public delegate void SetValuePageNumberInvoiceClick(int pageNumber);
        public static event SetValuePageNumberInvoiceClick OnSetValuePageNumberInvoiceCmd;
        public static void SetValueMethodPageNumberInvoice(int val)
        {
            if (OnSetValuePageNumberInvoiceCmd != null)
                OnSetValuePageNumberInvoiceCmd(val);
        }

        public delegate void SetValuePageNumberLineItemClick(int pageNumber);
        public static event SetValuePageNumberLineItemClick OnSetValuePageNumberLineItemCmd;
        public static void SetValueMethodPageNumberLineItem(int val)
        {
            if (OnSetValuePageNumberLineItemCmd != null)
                OnSetValuePageNumberLineItemCmd(val);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueRefreshCmd(string value);
        public static event SetValueRefreshCmd OnSetValueEvtRefreshCmd;
        public static void SetValueMethodRefreshCmd(string value)
        {
            if (OnSetValueEvtRefreshCmd != null)
                OnSetValueEvtRefreshCmd(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueList(List<BillingFindVM> value);
        public static event SetValueList OnSetValueListEvt;
        public static void SetValueListMethod(List<BillingFindVM> value)
        {
            if (OnSetValueListEvt != null)
                OnSetValueListEvt(value);
        }
        #endregion
    }
}
