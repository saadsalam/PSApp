using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Windows;
using System;
using AppWorks.UI.Common;
using AppWorksService.Properties;
using AppWorks.UI.View.UserControls.CodeAdmin;
using AppWorks.UI.Properties;
using AppWorks.UI.Controls.Attributes;

namespace AppWorks.UI.ViewModel.CodeAdmin
{
    public class CodesTableAdminVM : ChangeTrackingViewModel, IDisposable
    {
        public CodesTableAdminVM()
        {
            UserStatusItem = 0;
            FillCodeTypeList();
            Text = Resources.btnSave;
            EnabledCancel = false;
            EnabledDelete = false;
            EnabledFind = true;
            EnabledModify = false;
            EnabledNew = true;
            EnabledSaveUpdate = false;
            DelegateEventCodeAdmin.OnSetCodeAdminValueEvt += new DelegateEventCodeAdmin.SetCodeAdminValue(GetCodeAdminValue);
            DelegateEventCodeAdmin.OnSetCodeAdminValueEvtCmdReferesh += new DelegateEventCodeAdmin.SetCodeAdminValueCmdReferesh(GetCodeAdminValue);
            AcceptChanges();
        }

        //private void GetEnableCommand(string value)
        //{
        //    if (value.Equals("EnableNew"))
        //    {
        //        EnabledCancel = false;
        //        EnabledDelete = true;
        //        EnabledFind = true;
        //        EnabledModify = true;
        //        EnabledNew = true;
        //        EnabledSaveUpdate = false;
        //        Text = Resources.btnUpdate;
        //    }
        //}

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

        private int codeID;
        [ChangeTracking]
        public int CodeID
        {
            get { return this.codeID; }
            set { this.codeID = value; NotifyPropertyChanged("CodeID"); }
        }
        private string codeType;
        [ChangeTracking]
        public string CodeType
        {
            get { return this.codeType; }
            set { this.codeType = value; NotifyPropertyChanged("CodeType"); }
        }
        private string code1;
        [ChangeTracking]
        public string Code1
        {
            get { return this.code1; }
            set { this.code1 = value; NotifyPropertyChanged("Code1"); }
        }
        private string codeDescription;
        [ChangeTracking]
        public string CodeDescription
        {
            get { return this.codeDescription; }
            set { this.codeDescription = value; NotifyPropertyChanged("CodeDescription"); }
        }
        private string value1;
        [ChangeTracking]
        public string Value1
        {
            get { return this.value1; }
            set { this.value1 = value; NotifyPropertyChanged("Value1"); }
        }
        private string value1Description;
        [ChangeTracking]
        public string Value1Description
        {
            get { return this.value1Description; }
            set { this.value1Description = value; NotifyPropertyChanged("Value1Description"); }
        }
        private string value2;
        [ChangeTracking]
        public string Value2
        {
            get { return this.value2; }
            set { this.value2 = value; NotifyPropertyChanged("Value2"); }
        }
        private string value2Description;
        [ChangeTracking]
        public string Value2Description
        {
            get { return this.value2Description; }
            set { this.value2Description = value; NotifyPropertyChanged("Value2Description"); }
        }
        private string recordStatus;
        [ChangeTracking]
        public string RecordStatus
        {
            get { return this.recordStatus; }
            set { this.recordStatus = value; NotifyPropertyChanged("RecordStatus"); }
        }
        private Nullable<int> sortOrder;
        [ChangeTracking]
        public Nullable<int> SortOrder
        {
            get { return this.sortOrder; }
            set { this.sortOrder = value; NotifyPropertyChanged("SortOrder"); }
        }
        private Nullable<System.DateTime> creationDate;
        [ChangeTracking]
        public Nullable<System.DateTime> CreationDate
        {
            get { return this.creationDate; }
            set { this.creationDate = value; NotifyPropertyChanged("CreationDate"); }
        }
        private string createdBy;
        [ChangeTracking]
        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; NotifyPropertyChanged("CreatedBy"); }
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
        private string codeName;
        public string CodeName
        {
            get { return this.codeName; }
            set { this.codeName = value; NotifyPropertyChanged("CodeName"); }
        }
        private string code;
        public string Code
        {
            get { return this.code; }
            set { this.code = value; NotifyPropertyChanged("Code"); }
        }
        private string description;
        public string Description
        {
            get { return this.description; }
            set { this.description = value; NotifyPropertyChanged("Description"); }
        }
        private List<string> codeTypeList;
        public List<string> CodeTypeList
        {
            get { return this.codeTypeList; }
            private set { this.codeTypeList = value; NotifyPropertyChanged("CodeTypeList"); }
        }
        private int userStatusItem;
        public int UserStatusItem
        {
            get { return userStatusItem; }
            set
            {
                if (value != null)
                { this.userStatusItem = value; }
                NotifyPropertyChanged("UserStatusItem");
            }
        }
        private AppWorxCommand btnNew_Click;

        /// <summary>
        /// OK button event
        /// </summary>
        public AppWorxCommand BtnNew_Click
        {
            get
            {
                if (btnNew_Click == null)
                {
                    btnNew_Click = new AppWorxCommand(this.NewRecordDetails);
                }
                return btnNew_Click;
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
                    btnOk_Click = new AppWorxCommand(this.InsertCode);
                }
                return btnOk_Click;
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
        /// This button click event for update details
        /// </summary>
        public AppWorxCommand btnModify_Click;
        public AppWorxCommand BtnModify_Click
        {
            get
            {
                if (btnModify_Click == null)
                {
                    btnModify_Click = new AppWorxCommand(this.ModifyCodeAdminRecord);
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
                    btnDelete_Click = new AppWorxCommand(this.DeleteCodeAdminRecord);
                }
                return btnDelete_Click;
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
                //NewRecordDetails(null);
                IsReadOnly = false;
                IsReadOnlyButtonSave = false;
                IsReadOnlyButton = false;
                EnabledCancel = false;

                EnabledFind = true;
                if (CodeID > 0)
                {
                    EnabledDelete = true;
                    EnabledModify = true;
                }
                else
                {
                    EnabledDelete = false;
                    EnabledModify = false;
                }
                EnabledNew = true;
                EnabledSaveUpdate = false;
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
        /// This function used to dispaly new record data
        /// </summary>
        private void NewRecordDetails(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

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
                ResetCodeAdminWindow();
                CodeID = 0;
                SortOrder = 0;
                CreationDate = DateTime.Now;
                CreatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                BtnOK = true;
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
                    btnFind_Click = new AppWorxCommand(this.ShowFindCodePopup);
                }
                return btnFind_Click;
            }
        }


        /// <summary>
        /// This function used to Show Find Code Popup
        /// </summary>
        private void ShowFindCodePopup(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                CodeFind objCodeFind = new CodeFind();
                objCodeFind.Owner = Application.Current.MainWindow;

                objCodeFind.ShowDialog();
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
        /// This function is used to insert code admin details in database
        /// </summary>
        /// <param name="obj"></param>
        public void InsertCode(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                CodeProp objCodeProp = new CodeProp();
                objCodeProp.CodeID = CodeID;
                objCodeProp.RecordStatus = RecordStatus;
                objCodeProp.CodeType = CodeType;
                objCodeProp.Code1 = Code1;
                objCodeProp.Value1 = Value1;
                objCodeProp.Value2 = Value2;
                objCodeProp.CodeDescription = CodeDescription;
                objCodeProp.Value1Description = Value1Description;
                objCodeProp.Value2Description = Value2Description;
                objCodeProp.SortOrder = SortOrder;
                objCodeProp.CreationDate = CreationDate;
                objCodeProp.CreatedBy = CreatedBy;

                if (CodeID > 0)
                {
                    objCodeProp.UpdatedBy = Application.Current.Properties["LoggedInUserName"].ToString();
                    objCodeProp.UpdatedDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(CodeType))
                    {
                        if (!string.IsNullOrEmpty(Code1))
                        {
                            bool result = _serviceInstance.ModifyCodeAdminRecord(objCodeProp);

                            if (result)
                            {
                                DelegateEventCodeAdmin.SetCodeAdminValueMethodCmdReferesh(objCodeProp);
                                MessageBox.Show(Resources.msgUpdatedSuccessfully);
                                //ResetCodeAdminWindow();
                                IsReadOnly = false;
                                IsReadOnlyButton = false;
                                isReadOnlyButtonSave = false;
                                EnabledCancel = false;
                                EnabledFind = true;
                                if (CodeID > 0)
                                {
                                    EnabledDelete = true;
                                    EnabledModify = true;
                                }
                                else
                                {
                                    EnabledDelete = false;
                                    EnabledModify = false;
                                }
                                EnabledNew = true;
                                EnabledSaveUpdate = false;

                                Text = Resources.btnSave;

                                AcceptChanges();
                            }
                            else
                                MessageBox.Show(Resources.ErrorToUpdate);
                        }
                        else
                            MessageBox.Show(Resources.ErrorEnterCode);

                    }
                    else
                        MessageBox.Show(Resources.MsgSelectCodeType);

                }
                else
                {
                    if (!string.IsNullOrEmpty(CodeType))
                    {
                        if (!string.IsNullOrEmpty(Code1))
                        {
                            bool isValidRecord = false;

                            isValidRecord = _serviceInstance.CheckDuplicateCodeAndType(Code1, CodeType);

                            if (isValidRecord)
                            {
                                int data = _serviceInstance.AddCodeAdmin(objCodeProp);

                                if (data > 0)
                                {
                                    MessageBox.Show(Resources.msgInsertedSuccessfully);
                                    CodeID = data;
                                    IsReadOnly = false;
                                    IsReadOnlyButton = false;
                                    isReadOnlyButtonSave = false;
                                    EnabledCancel = false;
                                    EnabledFind = true;
                                    if (CodeID > 0)
                                    {
                                        EnabledDelete = true;
                                        EnabledModify = true;
                                    }
                                    else
                                    {
                                        EnabledDelete = false;
                                        EnabledModify = false;
                                    }
                                    EnabledNew = true;
                                    EnabledSaveUpdate = false;
                                    Text = Resources.btnSave;
                                    AcceptChanges();
                                }

                                else
                                {
                                    MessageBox.Show(Resources.ErrorInserted);
                                }
                            }
                            else
                                MessageBox.Show(Resources.ErrorDuplicateCode);
                        }
                        else
                            MessageBox.Show(Resources.ErrorEnterCode);

                    }
                    else
                        MessageBox.Show(Resources.MsgSelectCodeType);

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
        /// this method is used to get all code type for bind dropdown
        /// </summary>
        private void FillCodeTypeList()
        {
            try
            {
                List<string> List = new List<string>();
                List = _serviceInstance.CodeTypeList().Where(x => x != null).ToList();
                CodeTypeList = List;
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
        /// This method is used to get all code admin details for update and delete functionality and fill form
        /// </summary>
        /// <param name="codePropInfo"></param>
        public void GetCodeAdminValue(CodeProp codePropInfo)
        {
            if (codePropInfo != null)
            {
                EnabledCancel = false;
                EnabledDelete = true;
                EnabledFind = true;
                EnabledModify = true;
                EnabledNew = true;
                EnabledSaveUpdate = false;
                Text = Resources.btnUpdate;

                IsReadOnlyButton = false;
                IsReadOnlyButtonSave = false;
                IsReadOnly = false;
                CodeID = codePropInfo.CodeID;
                RecordStatus = codePropInfo.RecordStatus;
                CodeType = codePropInfo.CodeType;
                CodeName = codePropInfo.CodeName;
                Code1 = codePropInfo.Code1;
                CodeDescription = codePropInfo.CodeDescription;
                Value1 = codePropInfo.Value1;
                Value1Description = codePropInfo.Value1Description;
                Value2 = codePropInfo.Value2;
                Value2Description = codePropInfo.Value2Description;
                SortOrder = codePropInfo.SortOrder;
                CreationDate = codePropInfo.CreationDate;
                CreatedBy = codePropInfo.CreatedBy;
                UpdatedBy = codePropInfo.UpdatedBy;
                UpdatedDate = codePropInfo.UpdatedDate;
            }
        }

        /// <summary>
        /// This function is used to clear all field on forms
        /// </summary>
        private void ResetCodeAdminWindow()
        {
            Text = Resources.btnSave; ;
            CodeID = 0;
            CodeDescription = string.Empty;
            Code1 = string.Empty;
            CodeType = string.Empty;
            Value1 = string.Empty;
            Value1Description = string.Empty;
            Value2 = string.Empty;
            Value2Description = string.Empty;
            SortOrder = 0;
            CreationDate = null;
            CreatedBy = string.Empty;
            UpdatedBy = string.Empty;
            UpdatedDate = null;
            UserStatusItem = 0;

        }
        /// <summary>
        /// This function is used to Modify Code Admin Record
        /// </summary>
        /// <param name="obj"></param>
        public void ModifyCodeAdminRecord(object obj)
        {
            try
            {
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (CodeID > 0)
                {
                    IsReadOnly = true;
                    IsReadOnlyButton = true;
                    IsReadOnlyButtonSave = true;
                    EnabledCancel = true;
                    EnabledDelete = false;
                    EnabledFind = false;
                    EnabledModify = false;
                    EnabledNew = false;
                    EnabledSaveUpdate = true;
                    Text = Resources.btnUpdate; ;
                    AcceptChanges();
                }
                else
                { MessageBox.Show(Resources.MsgFindCustomer); }
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
        /// This function is used to Delete Code Admin Record
        /// </summary>
        /// <param name="obj"></param>
        public void DeleteCodeAdminRecord(object obj)
        {

            bool result = false;
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(Resources.MsgDeleteConfirm, Resources.msgTitleMessageBoxDelete, System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    CodeProp objCodeProp = new CodeProp();

                    objCodeProp.CodeID = CodeID;

                    result = _serviceInstance.DeleteCodeAdminDetails(objCodeProp.CodeID);

                    if (result)
                    {
                        MessageBox.Show(Resources.msgDeleteSuccessfully, Resources.msgTitleMessageBox);
                        ResetCodeAdminWindow();
                        IsReadOnly = false;
                        IsReadOnlyButton = false;
                        IsReadOnlyButtonSave = false;
                        EnabledCancel = false;
                        EnabledDelete = false;
                        EnabledFind = true;
                        EnabledModify = false;
                        EnabledNew = true;
                        EnabledSaveUpdate = false;
                        Text = Resources.btnSave; ;
                        AcceptChanges();
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

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DelegateEventCodeAdmin.OnSetCodeAdminValueEvt -= new DelegateEventCodeAdmin.SetCodeAdminValue(GetCodeAdminValue);
                    DelegateEventCodeAdmin.OnSetCodeAdminValueEvtCmdReferesh -= new DelegateEventCodeAdmin.SetCodeAdminValueCmdReferesh(GetCodeAdminValue);
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
