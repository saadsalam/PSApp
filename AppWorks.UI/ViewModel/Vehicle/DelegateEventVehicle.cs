using System;
using System.Collections.Generic;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public static class DelegateEventVehicle
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
        public delegate void SetValueList(List<VehicleLocatorVM> value);
        public static event SetValueList OnSetValueListEvt;
        public static void SetValueListMethod(List<VehicleLocatorVM> value)
        {           
            if (OnSetValueListEvt != null)
                OnSetValueListEvt(value);
        }


        /// <summary>
        /// ModelClosed function
        /// </summary>
        /// <param name="value"></param>
        public delegate void ModelClose(bool value);
        public static event ModelClose OnModelCloseEvt;
        public static void ModelClosed(bool value)
        {
            if (OnModelCloseEvt != null)
                OnModelCloseEvt(value);
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
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueTab(int value);
        public static event SetValueTab OnSetValueEvtCTabmd;
        public static void SetValueMethodTab(int value)
        {
            if (OnSetValueEvtCTabmd != null)
                OnSetValueEvtCTabmd(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueTabEnable(bool value);
        public static event SetValueTabEnable OnSetValueEvtCTabEnableCmd;
        public static void SetValueMethodTabEnable(bool value)
        {
            if (OnSetValueEvtCTabEnableCmd != null)
                OnSetValueEvtCTabEnableCmd(value);
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
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueDamageCode(string value);
        public static event SetValueDamageCode OnSetValueEvtDamageCodeCmd;
        public static void SetValueMethodDamageCode(string value)
        {
            if (OnSetValueEvtDamageCodeCmd != null)
                OnSetValueEvtDamageCodeCmd(value);
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
        public delegate void SetValueVINDecode(string value);
        public static event SetValueVINDecode OnSetValueEvtVINDecodeCmd;
        public static void SetValueMethodVINDecode(string value)
        {
            if (OnSetValueEvtVINDecodeCmd != null)
                OnSetValueEvtVINDecodeCmd(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueVINDecodeReq(string value);
        public static event SetValueVINDecodeReq OnSetValueEvtVINDecodeReqCmd;
        public static void SetValueMethodVINDecodeReq(string value)
        {
            if (OnSetValueEvtVINDecodeReqCmd != null)
                OnSetValueEvtVINDecodeReqCmd(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueEnableNew(string value);
        public static event SetValueEnableNew OnSetValueEvtEnableNewCmd;
        public static void SetValueMethodEnableNew(string value)
        {
            if (OnSetValueEvtEnableNewCmd != null)
                OnSetValueEvtEnableNewCmd(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValuePopup(string value);
        public static event SetValuePopup OnSetValueEvtPopupCmd;
        public static void SetValueMethodPopup(string value)
        {
            if (OnSetValueEvtPopupCmd != null)
                OnSetValueEvtPopupCmd(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueRefreshGrid(string value);
        public static event SetValueRefreshGrid OnSetValueEvtRefreshGridCmd;
        public static void SetValueMethodRefreshGrid(string value)
        {
            if (OnSetValueEvtRefreshGridCmd != null)
                OnSetValueEvtRefreshGridCmd(value);
        }


        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueUpCmd(string value);
        public static event SetValueUpCmd OnSetValueEvtUpCmd;
        public static void SetValueMethodUpCmd(string value)
        {
            if (OnSetValueEvtUpCmd != null)
                OnSetValueEvtUpCmd(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueFocusNext(bool value);
        public static event SetValueFocusNext OnSetValueEvtFocusNext;
        public static void SetValueMethodFocusNext(bool value)
        {
            if (OnSetValueEvtFocusNext != null)
                OnSetValueEvtFocusNext(value);
        }

        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetValueSearch(object obj, bool isCommand, DateTime? InvoiceDateFrom, DateTime? InvoiceDateTo, DateTime? DateInFrom, DateTime? DateInTo
                                            , DateTime? DateRequestFrom, DateTime? DateRequestTo, DateTime? DateOutFrom, DateTime? DateOutTo);
        public static event SetValueSearch OnSetValueEvtSearch;
        public static void SetValueMethodSearch(object obj, bool isCommand, DateTime? InvoiceDateFrom, DateTime? InvoiceDateTo, DateTime? DateInFrom, DateTime? DateInTo
                                            , DateTime? DateRequestFrom, DateTime? DateRequestTo, DateTime? DateOutFrom, DateTime? DateOutTo)
        {
            if (OnSetValueEvtSearch != null)
            {
                OnSetValueEvtSearch(obj, isCommand, InvoiceDateFrom, InvoiceDateTo, DateInFrom, DateInTo
                                            , DateRequestFrom, DateRequestTo, DateOutFrom, DateOutTo);
                return;
            }
        }

        public delegate void SetGridSorting(string sortColumn, string sortOrder);
        public static event SetGridSorting OnGridSorted;
        public static void SetGridSort(string sortColumn, string sortOrder)
        {
            if (OnGridSorted != null)
            {
                OnGridSorted(sortColumn, sortOrder);
                return;
            }
        }
    }
}
