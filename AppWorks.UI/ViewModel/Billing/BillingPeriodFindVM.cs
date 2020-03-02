using System.Collections.ObjectModel;
using System.Reflection;
using System.Globalization;
using System.Windows;
using System;
using AppWorks.UI.Common;
using AppWorksService.Properties;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using AppWorks.UI.Properties;
using System.Configuration;
using AppWorks.UI.ViewModel.Vehicle;

namespace AppWorks.UI.ViewModel.Billing
{
    public class BillingPeriodFindVM : ViewModelBase, IDisposable
    {
        int _gridPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["GridPageSize"]);
        int _gridPageSizeOnScroll = Convert.ToInt32(ConfigurationManager.AppSettings["FindUserGridPageSizeOnScroll"]);

        public event EventHandler CloseWindowRequested;
        private void OnCloseWindowRequested()
        {
            EventHandler handler = CloseWindowRequested;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public BillingPeriodFindVM()
        {
            IsOpen = true;
            GetRadioButton(null);
            FindBillingPeriod(null);
            DelegateEventCustomer.OnSetValueEvtTotalCountPagerCmd += new DelegateEventCustomer.SetValueTotalCountPager(GetTotalPageCount);
            DelegateEventCustomer.OnSetValuePageNumberCmd += new DelegateEventCustomer.SetValuePageNumberClick(GetCurrentPageIndex);
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
            FindBillingPeriod("GridScroled");
        }


        private long totalPageCount;
        public long TotalPageCount
        {
            get { return totalPageCount; }
            set
            {
                this.totalPageCount = value;
                NotifyPropertyChanged("TotalPageCount");
            }
        }

        private int currentPageIndex;
        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                this.currentPageIndex = value;
            }
        }

        private bool isOpen;
        public bool IsOpen
        {
            get { return this.isOpen; }
            set { this.isOpen = value; NotifyPropertyChanged("IsOpen"); }
        }
        private bool isClosed;
        public bool IsClosed
        {
            get { return this.isClosed; }
            set { this.isClosed = value; NotifyPropertyChanged("IsClosed"); }
        }

        private Nullable<int> calendarYear;
        public Nullable<int> CalendarYear
        {
            get { return this.calendarYear; }
            private set { this.calendarYear = value; NotifyPropertyChanged("CalendarYear"); }
        }

        private int periodClosedInd;
        public int PeriodClosedInd
        {
            get { return this.periodClosedInd; }
            set { this.periodClosedInd = value; NotifyPropertyChanged("PeriodClosedInd"); }
        }

        private ObservableCollection<BillingPeriodprop> billingPeriodList;
        public ObservableCollection<BillingPeriodprop> BillingPeriodList
        {
            get { return billingPeriodList; }
            private set
            {
                this.billingPeriodList = value;
                NotifyPropertyChanged("BillingPeriodList");
            }
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

        private BillingPeriodprop selectedDisRecipientGridItem;
        public BillingPeriodprop SelectedDisRecipientGridItem
        {
            get { return selectedDisRecipientGridItem; }
            set { selectedDisRecipientGridItem = value; NotifyPropertyChanged("SelectedDisRecipientGridItem"); }
        }


        private AppWorxCommand radioButton_Checked;
        public AppWorxCommand RadioButton_Checked
        {
            get
            {
                if (radioButton_Checked == null)
                {
                    radioButton_Checked = new AppWorxCommand(this.GetRadioButton);
                }
                return radioButton_Checked;
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
                    btnFind_Click = new AppWorxCommand(this.FindBillingPeriod);
                }
                return btnFind_Click;
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


        private void GetRadioButton(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (IsOpen == true && isClosed == false)
                {
                    PeriodClosedInd = 0;
                }
                else if (IsOpen == false && isClosed == true)
                {
                    PeriodClosedInd = 1;
                }
                else if (IsOpen == false && isClosed == false)
                {
                    PeriodClosedInd = 2;
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
        /// <createdOn>May 21,2016</createdOn>
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
        /// Function to Search the Billing Period List.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 20,2016</createdOn>
        public void FindBillingPeriod(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (obj == null)
                {
                    CurrentPageIndex = 0;
                }
                if (CurrentPageIndex == 0)
                {
                    Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
                    Application.Current.Properties["FindUserGridCurrentPageIndex"] = 0;
                }

                BillingPeriodprop objBillingPeriodprop = new BillingPeriodprop();
                objBillingPeriodprop.CalendarYear = CalendarYear;
                objBillingPeriodprop.PeriodClosedInd = PeriodClosedInd;
                objBillingPeriodprop.CurrentPageIndex = CurrentPageIndex;
                objBillingPeriodprop.PageSize = CurrentPageIndex > 0 ? _gridPageSizeOnScroll : _gridPageSize; ;
                objBillingPeriodprop.DefaultPageSize = _gridPageSize;
                var list = new ObservableCollection<BillingPeriodprop>(_serviceInstance.FindBillingPeriod(objBillingPeriodprop));

                if (CurrentPageIndex == 0)
                {
                    BillingPeriodList = null;
                    BillingPeriodList = new ObservableCollection<BillingPeriodprop>(list);
                }
                else
                {
                    if (BillingPeriodList != null && BillingPeriodList.Count > 0)
                    {
                        foreach (BillingPeriodprop ud in new ObservableCollection<BillingPeriodprop>(list))
                        {
                            BillingPeriodList.Add(ud);
                        }
                    }
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

                DelegateEventBillingPeriod.SetBillingPeriodValueMethod(SelectedDisRecipientGridItem);
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
        /// Function to Cancle Find Period Popup & Fill selected to the buffer.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May 21,2016</createdOn>
        private void Continue(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (SelectedDisRecipientGridItem != null)
                {
                    DelegateEventBillingPeriod.SetBillingPeriodValueMethod(SelectedDisRecipientGridItem);
                    //DelegateEventVehicle.SetValueMethodCmd("Add");
                    OnCloseWindowRequested();
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DelegateEventCustomer.OnSetValueEvtTotalCountPagerCmd -= new DelegateEventCustomer.SetValueTotalCountPager(GetTotalPageCount);
                    DelegateEventCustomer.OnSetValuePageNumberCmd -= new DelegateEventCustomer.SetValuePageNumberClick(GetCurrentPageIndex);
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }

    public class BillingPeriodMultiSelect : Behavior<RadGridView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            ((BillingPeriodFindVM)this.AssociatedObject.DataContext).SelectedItems = this.AssociatedObject.SelectedItems;
        }
    }

}
