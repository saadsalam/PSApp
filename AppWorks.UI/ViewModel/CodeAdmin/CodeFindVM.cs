using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Windows;
using System;
using AppWorks.UI.Common;
using AppWorksService.Properties;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using System.Configuration;
using AppWorks.UI.Properties;
using AppWorks.UI.Controls.Extensions;

namespace AppWorks.UI.ViewModel.CodeAdmin
{
    class CodeFindVM : ViewModelBase
    {
        int _GridPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]);
        int _GridPageSizeOnScroll = Convert.ToInt32(ConfigurationManager.AppSettings["FindUserGridPageSizeOnScroll"]);

        /// <summary>
        /// for Total Records on end of grid
        /// </summary>
        /// 
        private long count;
        public long Count
        {
            get { return count; }
            set
            {
                this.count = value;
                NotifyPropertyChanged("Count");
            }
        }

        public event EventHandler CloseWindowRequested;
        private void OnCloseWindowRequested()
        {
            EventHandler handler = CloseWindowRequested;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public CodeFindVM()
        {
            if (!DesignTimeExtensions.IsDesignTime)
            {
                FillCodeTypeList();
                FindCode(null);
            }
            CodeType = Resources.MsgAll;
            DelegateEventCodeAdmin.OnSetValueEvtTotalCountPagerCmd += new DelegateEventCodeAdmin.SetValueTotalCountPager(GetTotalPageCount);
            DelegateEventCodeAdmin.OnSetValuePageNumberCmd += new DelegateEventCodeAdmin.SetValuePageNumberClick(GetCurrentPageIndex);
        }

        private List<string> codeTypeList;
        public List<string> CodeTypeList
        {
            get { return this.codeTypeList; }
            set { this.codeTypeList = value; NotifyPropertyChanged("CodeTypeList"); }
        }

        private string codeType;
        public string CodeType
        {
            get { return this.codeType; }
            set { this.codeType = value; NotifyPropertyChanged("CodeType"); }
        }

        private ObservableCollection<CodeProp> codeList;
        public ObservableCollection<CodeProp> CodeList
        {
            get { return codeList; }
            set
            {
                this.codeList = value;
                NotifyPropertyChanged("CodeList");
            }
        }

        private CodeProp selectedDisRecipientGridItem;
        public CodeProp SelectedDisRecipientGridItem
        {
            get { return selectedDisRecipientGridItem; }
            set { selectedDisRecipientGridItem = value; NotifyPropertyChanged("SelectedDisRecipientGridItem"); }
        }

        private ObservableCollection<object> selectedItems;
        public ObservableCollection<object> SelectedItems
        {
            get { return selectedItems; }
            set
            {
                selectedItems = value;
                NotifyPropertyChanged("SelectedItems");
            }
        }
        private long totalPageCount;
        public long TotalPageCount
        {
            get { return totalPageCount; }
            set
            {
                if (value != null)
                {
                    this.totalPageCount = value;
                    NotifyPropertyChanged("TotalPageCount");
                }
            }
        }

        private int currentPageIndex;
        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                if (value != null)
                {
                    this.currentPageIndex = value;
                }
            }
        }
        private AppWorxCommand btnCancel_Click;
        /// <summary>
        /// Cancel button event
        /// </summary>
        public AppWorxCommand BtnCancel_Click
        {
            get
            {
                if (btnCancel_Click == null)
                {
                    btnCancel_Click = new AppWorxCommand(this.CancelWindow);
                }
                return btnCancel_Click;
            }
        }

        private AppWorxCommand btnFind_Click;
        /// <summary>
        /// Find button event
        /// </summary>
        public AppWorxCommand BtnFind_Click
        {
            get
            {
                if (btnFind_Click == null)
                {
                    btnFind_Click = new AppWorxCommand(this.FindCode);
                }
                return btnFind_Click;
            }
        }


        private AppWorxCommand btnFillData_Click;
        public AppWorxCommand BtnFillData_Click
        {
            get
            {
                if (btnFillData_Click == null)
                {
                    btnFillData_Click = new AppWorxCommand(this.FillData);
                }
                return btnFillData_Click;
            }
        }

        private AppWorxCommand btnContinue_Click;
        /// <summary>
        /// Continue button event
        /// </summary>
        public AppWorxCommand BtnContinue_Click
        {
            get
            {
                if (btnContinue_Click == null)
                {
                    btnContinue_Click = new AppWorxCommand(this.Continue);
                }
                return btnContinue_Click;
            }
        }
        /// <summary>
        /// Get Total page count for grid
        /// </summary>
        /// 
        public void GetTotalPageCount(long totalPageCount)
        {
            TotalPageCount = totalPageCount;
        }

        public void GetCurrentPageIndex(int currentPageIndx)
        {
            CurrentPageIndex = currentPageIndx;
            FindCode("GridScroled");
        }
        /// <summary>
        /// Function to Search the Billing Period List.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 20,2016</createdOn>
        public void FindCode(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                Count = 0;
                if (obj == null)
                {
                    CurrentPageIndex = 0;
                }
                if (CurrentPageIndex == 0)
                {
                    Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
                    Application.Current.Properties["FindUserGridCurrentPageIndex"] = 0;
                }

                CodeProp objCodeProp = new CodeProp();
                objCodeProp.CurrentPageIndex = CurrentPageIndex;
                objCodeProp.PageSize = CurrentPageIndex > 0 ? _GridPageSizeOnScroll : _GridPageSize; ;
                objCodeProp.DefaultPageSize = _GridPageSize;
                objCodeProp.CodeType = CodeType;
                var list = new ObservableCollection<CodeProp>(_serviceInstance.FindCode(objCodeProp));
                if (CurrentPageIndex == 0)
                {
                    CodeList = null;
                    CodeList = new ObservableCollection<CodeProp>(list);
                }
                else
                {
                    if (CodeList != null && CodeList.Count > 0)
                    {
                        foreach (CodeProp ud in new ObservableCollection<CodeProp>(list))
                        {
                            CodeList.Add(ud);
                        }
                    }
                }
                Count = CodeList.ToList().Where(x => x.TotalPageCount > 0).FirstOrDefault().TotalPageCount;
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to FIll Customer Data on row selection.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 10,2016</createdOn>
        private void FillData(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                DelegateEventCodeAdmin.SetCodeAdminValueMethod(SelectedDisRecipientGridItem);
                DelegateEventCodeAdmin.SetValueMethodEnableNew("EnableNew");
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }

            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to Continue Find Period Popup & Fill selected to the buffer.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 24,2016</createdOn>
        private void Continue(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (SelectedDisRecipientGridItem != null)
                {
                    DelegateEventCodeAdmin.SetCodeAdminValueMethod(SelectedDisRecipientGridItem);
                    //DelegateEventVehicle.SetValueMethodCmd("Add");
                    OnCloseWindowRequested();
                    //DelegateEventCodeAdmin.SetValueMethodEnableNew("EnableNew");
                }
                else
                {
                    MessageBox.Show(Resources.MsgSelectUser);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }

            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// Function to Cancle Find Period Popup.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 23,2016</createdOn>
        private void CancelWindow(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                OnCloseWindowRequested();
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorToDb(ex);
                bool displayErrorOnUI = false;
                CommonSettings.logger.LogError(this.GetType(), ex);
                if (displayErrorOnUI)
                { throw; }
            }
            finally
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>
        /// This method is used to get all code types
        /// </summary>
        private void FillCodeTypeList()
        {
            List<string> List = new List<string>();
            List.Add(Resources.MsgAll);
            List.AddRange(_serviceInstance.CodeTypeList().Where(x => x != null).ToList());
            CodeTypeList = List;
        }
    }

    public class CodeMultiSelect : Behavior<RadGridView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            ((CodeFindVM)this.AssociatedObject.DataContext).SelectedItems = this.AssociatedObject.SelectedItems;
        }
    }
}
