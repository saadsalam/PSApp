using System.Reflection;
using System.Globalization;
using System.Windows;
using System;
using AppWorks.UI.Common;
using AppWorksService.Properties;
using AppWorks.UI.View.Billing;
using AppWorks.UI.Properties;
using AppWorks.UI.Controls.Attributes;

namespace AppWorks.UI.ViewModel.Billing
{
    public class BillingPeriodAdminVM : ChangeTrackingViewModel, IDisposable
    {
        public BillingPeriodAdminVM()
        {
            PeriodNumber = 0;
            Text = Resources.btnSave;
            EnabledCancel = false;
            EnabledDelete = false;
            EnabledFind = true;
            EnabledModify = false;
            EnabledNew = true;
            EnabledSaveUpdate = false;
            DelegateEventBillingPeriod.OnSetBillingPeriodValueEvt += new DelegateEventBillingPeriod.SetBillingPeriodValue(GetBillingPeriodValue);
            DelegateEventBillingPeriod.OnSetBillingPeriodValueEvtCmdReferesh += new DelegateEventBillingPeriod.SetBillingPeriodValueCmdReferesh(GetBillingPeriodValue);
            AcceptChanges();
        }
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                this.text = value;
                NotifyPropertyChanged("Text");
            }
        }

        private bool enabledNew;
        public bool EnabledNew
        {
            get { return enabledNew; }
            set
            {
                this.enabledNew = value;
                NotifyPropertyChanged("EnabledNew");
            }
        }

        private bool enabledModify;
        public bool EnabledModify
        {
            get { return enabledModify; }
            set
            {
                this.enabledModify = value;
                NotifyPropertyChanged("EnabledModify");
            }
        }

        private bool enabledDelete;
        public bool EnabledDelete
        {
            get { return enabledDelete; }
            set
            {
                this.enabledDelete = value;
                NotifyPropertyChanged("EnabledDelete");
            }
        }

        private bool enabledFind;
        public bool EnabledFind
        {
            get { return enabledFind; }
            set
            {
                this.enabledFind = value;
                NotifyPropertyChanged("EnabledFind");
            }
        }

        private bool enabledSaveUpdate;
        public bool EnabledSaveUpdate
        {
            get { return enabledSaveUpdate; }
            set
            {
                this.enabledSaveUpdate = value;
                NotifyPropertyChanged("EnabledSaveUpdate");
            }
        }

        private bool enabledCancel;
        public bool EnabledCancel
        {
            get { return enabledCancel; }
            set
            {
                this.enabledCancel = value;
                NotifyPropertyChanged("EnabledCancel");
            }
        }


        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                this.isReadOnly = value;
                NotifyPropertyChanged("IsReadOnly");
            }
        }
        private bool isReadOnlyButton;
        public bool IsReadOnlyButton
        {
            get { return isReadOnlyButton; }
            set
            {
                this.isReadOnlyButton = value;
                NotifyPropertyChanged("IsReadOnlyButton");
            }
        }
        private bool isReadOnlyButtonSave;
        public bool IsReadOnlyButtonSave
        {
            get { return isReadOnlyButtonSave; }
            set
            {
                this.isReadOnlyButtonSave = value;
                NotifyPropertyChanged("IsReadOnlyButtonSave");
            }
        }
        private bool btnNew;
        public bool BtnNew
        {
            get { return this.btnNew; }
            set { this.btnNew = value; NotifyPropertyChanged("BtnNew"); }
        }

        private bool btnModify;
        public bool BtnModify
        {
            get { return this.btnModify; }
            set { this.btnModify = value; NotifyPropertyChanged("BtnModify"); }
        }

        private bool btnDelete;
        public bool BtnDelete
        {
            get { return this.btnDelete; }
            set { this.btnDelete = value; NotifyPropertyChanged("BtnDelete"); }
        }

        private bool btnFind;
        public bool BtnFind
        {
            get { return this.btnFind; }
            set { this.btnFind = value; NotifyPropertyChanged("BtnFind"); }
        }

        private bool btnPreview;
        public bool BtnPreview
        {
            get { return this.btnPreview; }
            set { this.btnPreview = value; NotifyPropertyChanged("BtnPreview"); }
        }

        private bool btnNext;
        public bool BtnNext
        {
            get { return this.btnNext; }
            set { this.btnNext = value; NotifyPropertyChanged("BtnNext"); }
        }

        private bool btnOK;
        public bool BtnOK
        {
            get { return this.btnOK; }
            set { this.btnOK = value; NotifyPropertyChanged("BtnOK"); }
        }

        private int billingPeriodID;
        private int BillingPeriodID
        {
            get { return this.billingPeriodID; }
            set { this.billingPeriodID = value; NotifyPropertyChanged("BillingPeriodID"); }
        }

        private Nullable<System.DateTime> updatedDate;
        [ChangeTracking]
        public Nullable<System.DateTime> UpdatedDate
        {
            get { return this.updatedDate; }
            set { this.updatedDate = value; NotifyPropertyChanged("UpdatedDate"); }
        }

        private string updatedBy;
        [ChangeTracking]
        public string UpdatedBy
        {
            get { return this.updatedBy; }
            set { this.updatedBy = value; NotifyPropertyChanged("UpdatedBy"); }
        }

        private Nullable<System.DateTime> periodEndDate;
        [ChangeTracking]
        public Nullable<System.DateTime> PeriodEndDate
        {
            get { return this.periodEndDate; }
            set { this.periodEndDate = value; NotifyPropertyChanged("PeriodEndDate"); }
        }

        private string createdBy;
        [ChangeTracking]
        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; NotifyPropertyChanged("CreatedBy"); }
        }

        private Nullable<System.DateTime> creationDate;
        [ChangeTracking]
        public Nullable<System.DateTime> CreationDate
        {
            get { return this.creationDate; }
            set { this.creationDate = value; NotifyPropertyChanged("CreationDate"); }
        }

        private Nullable<int> periodClosedInd;
        [ChangeTracking]
        public Nullable<int> PeriodClosedInd
        {
            get { return this.periodClosedInd; }
            set { this.periodClosedInd = value; NotifyPropertyChanged("PeriodClosedInd"); }
        }

        private string periodClosedBy;
        [ChangeTracking]
        public string PeriodClosedBy
        {
            get { return this.periodClosedBy; }
            set { this.periodClosedBy = value; NotifyPropertyChanged("PeriodClosedBy"); }
        }

        private Nullable<System.DateTime> periodClosedDate;
        [ChangeTracking]
        public Nullable<System.DateTime> PeriodClosedDate
        {
            get { return this.periodClosedDate; }
            set { this.periodClosedDate = value; NotifyPropertyChanged("PeriodClosedDate"); }
        }

        private Nullable<int> calendarYear;
        [ChangeTracking]
        public Nullable<int> CalendarYear
        {
            get { return this.calendarYear; }
            set { this.calendarYear = value; NotifyPropertyChanged("CalendarYear"); }
        }

        private Nullable<int> periodNumber;
        [ChangeTracking]
        public Nullable<int> PeriodNumber
        {
            get { return this.periodNumber; }
            set { this.periodNumber = value; NotifyPropertyChanged("PeriodNumber"); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.isSelected = value; NotifyPropertyChanged("IsSelected"); }
        }

        private AppWorxCommand checkBox_Checked;
        public AppWorxCommand CheckBox_Checked
        {
            get
            {
                if (checkBox_Checked == null)
                {
                    checkBox_Checked = new AppWorxCommand(this.ChangePeriodClosedIndProp);
                }
                return checkBox_Checked;
            }
        }

        private AppWorxCommand btnOk_Click;

        /// <summary>
        /// OK button event
        /// </summary>
        public AppWorxCommand BtnOk_Click
        {
            get
            {
                if (btnOk_Click == null)
                {
                    btnOk_Click = new AppWorxCommand(this.InsertBillingPeriod);
                }
                return btnOk_Click;
            }
        }

        /// <summary>
        /// This button click event for update details
        /// </summary>
        public AppWorxCommand btnModify_Click;
        public AppWorxCommand BtnModify_Click
        {
            get
            {
                if (btnModify_Click == null)
                {
                    btnModify_Click = new AppWorxCommand(this.ModifyBillingPeriodAdminRecord);
                }
                return btnModify_Click;
            }
        }

        /// <summary>
        /// To Delete The Existing Record.
        /// </summary>
        /// <param name="obj"></param>
        /// 
        public AppWorxCommand btnDelete_Click;
        public AppWorxCommand BtnDelete_Click
        {
            get
            {
                if (btnDelete_Click == null)
                {
                    btnDelete_Click = new AppWorxCommand(this.DeleteBillingPeriodAdminRecord);
                }
                return btnDelete_Click;
            }
        }
        private AppWorxCommand btnFind_Click;

        /// <summary>
        /// Find Button event
        /// </summary>
        public AppWorxCommand BtnFind_Click
        {
            get
            {
                if (btnFind_Click == null)
                {
                    btnFind_Click = new AppWorxCommand(this.OpenFindBillingPeriodPopup);
                }
                return btnFind_Click;
            }
        }

        private AppWorxCommand btnNew_Click;

        /// <summary>
        /// Find Button event
        /// </summary>
        public AppWorxCommand BtnNew_Click
        {
            get
            {
                if (btnNew_Click == null)
                {
                    btnNew_Click = new AppWorxCommand(this.NewRecorddetails);
                }
                return btnNew_Click;
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

                //NewRecorddetails(null);
                EnabledCancel = false;

                EnabledFind = true;

                EnabledNew = true;
                EnabledSaveUpdate = false;

                if (BillingPeriodID > 0)
                {
                    EnabledModify = true;
                    EnabledDelete = false;
                }
                else
                {
                    EnabledModify = false;
                    EnabledDelete = false;
                    ResetFindBillingPeriodWindow();
                }

                IsReadOnly = false;
                IsReadOnlyButtonSave = false;
                IsReadOnlyButton = false;

                AcceptChanges();
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
        /// Function  to Change Period ClosedInd Prop
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-21,2016</createdOn>
        public void ChangePeriodClosedIndProp(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (isSelected)
                    PeriodClosedInd = 1;
                else
                    PeriodClosedInd = 0;
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
        /// Function NewRecorddetails to show the details of new record
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-21,2016</createdOn>
        public void NewRecorddetails(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                IsReadOnly = true;
                IsReadOnlyButtonSave = true;
                IsReadOnlyButton = false;


                EnabledCancel = true;
                EnabledDelete = false;
                EnabledFind = false;
                EnabledModify = false;
                EnabledNew = false;
                EnabledSaveUpdate = true;
                IsReadOnly = true;
                IsReadOnlyButtonSave = true;
                IsReadOnlyButton = false;
                Text = Resources.btnSave;
                ResetFindBillingPeriodWindow();
                CreationDate = DateTime.Now;
                CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                BtnOK = true;
                BtnModify = false;
                BtnDelete = false;

                AcceptChanges();
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
        /// This function is used to clear all field on forms
        /// </summary>
        private void ResetFindBillingPeriodWindow()
        {
            Text = Resources.btnSave;
            BillingPeriodID = 0;
            PeriodClosedInd = 0;
            CalendarYear = null;
            PeriodNumber = 0;
            CreationDate = null;
            PeriodEndDate = null;
            CreatedBy = string.Empty;
            IsSelected = false;
            UpdatedBy = string.Empty;
            UpdatedDate = null;
            CreatedBy = string.Empty;
            PeriodClosedBy = string.Empty;
            PeriodClosedDate = null;

        }

        /// <summary>
        /// Function OpenFindBillingPeriodPopup to open the Find Billing Period window
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>NA</returns>
        /// <createdBy></createdBy>
        /// <createdOn>May-20,2016</createdOn>
        public void OpenFindBillingPeriodPopup(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //ResetFindBillingPeriodWindow();
                //display the Billing Period Find popup;
                BillingPeriodFind objBillingPeriodFind = new BillingPeriodFind();
                objBillingPeriodFind.Owner = Application.Current.MainWindow;
                objBillingPeriodFind.ShowDialog();

                AcceptChanges();
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
        /// This Method is used to Insert biilling detaills from form
        /// </summary>
        /// <param name="obj"></param>
        public void InsertBillingPeriod(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                BillingPeriodprop objBillingPeriodprop = new BillingPeriodprop();
                objBillingPeriodprop.BillingPeriodID = BillingPeriodID;
                objBillingPeriodprop.CalendarYear = CalendarYear;
                objBillingPeriodprop.PeriodNumber = PeriodNumber;
                objBillingPeriodprop.CreationDate = CreationDate;
                objBillingPeriodprop.PeriodEndDate = PeriodEndDate;
                objBillingPeriodprop.PeriodClosedInd = PeriodClosedInd;
                objBillingPeriodprop.CreatedBy = CreatedBy;
                if (BillingPeriodID > 0)
                {
                    objBillingPeriodprop.UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                    objBillingPeriodprop.UpdatedDate = DateTime.Now;
                    if (CalendarYear > 0)
                    {
                        if (PeriodNumber > 0)
                        {
                            bool result = _serviceInstance.UpdateBillingPeriodAdminDetails(objBillingPeriodprop);
                            if (result)
                            {
                                MessageBox.Show(Resources.msgUpdatedSuccessfully, Resources.msgTitleMessageBox);
                                DelegateEventBillingPeriod.SetBillingPeriodValueMethodCmdReferesh(objBillingPeriodprop);
                                EnabledCancel = false;
                                EnabledFind = true;
                                EnabledNew = true;
                                EnabledSaveUpdate = false;

                                if (BillingPeriodID > 0)
                                {
                                    EnabledModify = true;
                                    EnabledDelete = false;
                                }
                                else
                                {
                                    EnabledModify = false;
                                    EnabledDelete = false;
                                }
                                //ResetFindBillingPeriodWindow();
                                IsReadOnly = false;
                                IsReadOnlyButton = false;
                                isReadOnlyButtonSave = false;
                                Text = Resources.btnSave;

                                AcceptChanges();
                            }
                        }
                        else
                            MessageBox.Show(Resources.msgPeriodNumberReq);

                    }
                    else
                    {
                        MessageBox.Show(Resources.msgCalanderYearReq);
                    }

                }
                else
                {
                    if (CalendarYear > 0)
                    {
                        if (PeriodNumber > 0)
                        {
                            bool isValid = _serviceInstance.CheckDuplicateCalenderAndPeriod((int)CalendarYear, (int)PeriodNumber);
                            if (isValid)
                            {
                                var data = _serviceInstance.AddBillingPeriod(objBillingPeriodprop);
                                if (data > 0)
                                {
                                    BillingPeriodID = data;
                                    MessageBox.Show(Resources.msgInsertedSuccessfully);
                                    EnabledCancel = false;
                                    EnabledFind = true;
                                    if (BillingPeriodID > 0)
                                    {
                                        EnabledModify = true;
                                        EnabledDelete = false;
                                    }
                                    else
                                    {
                                        EnabledModify = false;
                                        EnabledDelete = false;
                                    }
                                    EnabledNew = true;
                                    IsReadOnly = false;
                                    IsReadOnlyButton = false;
                                    isReadOnlyButtonSave = false;
                                    Text = Resources.btnSave;

                                    AcceptChanges();
                                }
                                else
                                {
                                    MessageBox.Show(Resources.ErrorInserted);
                                    BtnOK = false;
                                }


                            }
                            else
                                MessageBox.Show(Resources.msgCalanderYearPerodNumberAlredy);
                        }
                        else
                            MessageBox.Show(Resources.msgPeriodNumberReq);


                    }
                    else
                    {
                        MessageBox.Show(Resources.msgCalanderYearReq);
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
        /// This Function is to bind all field of billing form for modify and delete functionality
        /// </summary>
        /// <param name="billingPeriodInfo"></param>
        public void GetBillingPeriodValue(BillingPeriodprop billingPeriodInfo)
        {

            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (billingPeriodInfo != null)
                {
                    Text = Resources.btnUpdate;
                    IsReadOnlyButton = false;
                    IsReadOnlyButtonSave = false;
                    IsReadOnly = false;
                    EnabledCancel = false;
                    EnabledDelete = false;
                    EnabledFind = true;
                    EnabledModify = true;
                    EnabledNew = true;
                    EnabledSaveUpdate = false;

                    BillingPeriodID = billingPeriodInfo.BillingPeriodID;
                    CalendarYear = billingPeriodInfo.CalendarYear;
                    PeriodNumber = billingPeriodInfo.PeriodNumber;
                    PeriodClosedInd = billingPeriodInfo.PeriodClosedInd;
                    PeriodEndDate = billingPeriodInfo.PeriodEndDate;
                    PeriodClosedBy = billingPeriodInfo.PeriodClosedBy;
                    PeriodClosedDate = billingPeriodInfo.PeriodClosedDate;
                    CreationDate = billingPeriodInfo.CreationDate;
                    CreatedBy = billingPeriodInfo.CreatedBy;
                    UpdatedBy = billingPeriodInfo.UpdatedBy;
                    UpdatedDate = billingPeriodInfo.UpdatedDate;
                    IsSelected = PeriodClosedInd == 0 ? false : true;
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
        /// This function is used to Modify Billing Period Admin Record
        /// </summary>
        /// <param name="obj"></param>
        public void ModifyBillingPeriodAdminRecord(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (BillingPeriodID > 0)
                {
                    IsReadOnly = true;
                    IsReadOnlyButtonSave = true;
                    IsReadOnlyButton = false;

                    EnabledCancel = true;
                    EnabledDelete = false;
                    EnabledFind = false;
                    EnabledModify = false;
                    EnabledNew = false;
                    EnabledSaveUpdate = true;
                    Text = Resources.btnUpdate;

                    AcceptChanges();
                }
                else
                    MessageBox.Show(Resources.MsgFindBillingPeriod);
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
        /// This function is used to Delete Billing Period Admin Record
        /// </summary>
        /// <param name="obj"></param>
        public void DeleteBillingPeriodAdminRecord(object obj)
        {
            bool result = false;

            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                MessageBoxResult messageBoxResult = MessageBox.Show(Resources.MsgDeleteConfirm, Resources.msgTitleMessageBoxDelete, MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    BillingPeriodprop objBillingPeriodprop = new BillingPeriodprop();

                    objBillingPeriodprop.BillingPeriodID = BillingPeriodID;

                    result = _serviceInstance.DeleteBillingPeriodAdminDetails(objBillingPeriodprop.BillingPeriodID);

                    if (result)
                    {
                        MessageBox.Show(Resources.msgDeleteSuccessfully, Resources.msgTitleMessageBox);
                        ResetFindBillingPeriodWindow();
                        PeriodNumber = 0;
                        Text = Resources.btnSave;
                        EnabledCancel = false;
                        EnabledDelete = false;
                        EnabledFind = true;
                        EnabledModify = false;
                        EnabledNew = true;
                        EnabledSaveUpdate = false;
                    }

                    AcceptChanges();
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
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DelegateEventBillingPeriod.OnSetBillingPeriodValueEvt -= new DelegateEventBillingPeriod.SetBillingPeriodValue(GetBillingPeriodValue);
                    DelegateEventBillingPeriod.OnSetBillingPeriodValueEvtCmdReferesh -= new DelegateEventBillingPeriod.SetBillingPeriodValueCmdReferesh(GetBillingPeriodValue);
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
}
