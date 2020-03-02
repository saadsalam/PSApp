using AppWorksService.Properties;

namespace AppWorks.UI.ViewModel.CodeAdmin
{
    public static class DelegateEventCodeAdmin
    {
        /// <summary>
        /// SetValueMethod function
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetCodeAdminValue(CodeProp codeAdminInfo);
        public static event SetCodeAdminValue OnSetCodeAdminValueEvt;
        public static void SetCodeAdminValueMethod(CodeProp value)
        {
            if (OnSetCodeAdminValueEvt != null)
                OnSetCodeAdminValueEvt(value);
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

        /// <summary>
        /// SetValueMethod function for referesh all value
        /// </summary>
        /// <param name="value"></param>
        public delegate void SetCodeAdminValueCmdReferesh(CodeProp value);
        public static event SetCodeAdminValueCmdReferesh OnSetCodeAdminValueEvtCmdReferesh;
        public static void SetCodeAdminValueMethodCmdReferesh(CodeProp value)
        {
            if (OnSetCodeAdminValueEvtCmdReferesh != null)
                OnSetCodeAdminValueEvtCmdReferesh(value);
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
    }
}
