using AppWorks.UI.Common;
using AppWorks.UI.Controls.Attributes;
using AppWorks.UI.Properties;
using AppWorksService.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace AppWorks.UI.ViewModel
{
    class SystemSettingsAdminVM : ChangeTrackingViewModel, IDisposable
    {
        public SystemSettingsAdminVM(string mode)
        {
            if (mode.Equals("New", StringComparison.OrdinalIgnoreCase))
            {
                SaveMode();
            }
            if (mode.Equals("Find", StringComparison.OrdinalIgnoreCase))
            {
                BindGridDetails(null);
                FindMode();
            }
        }

        private ObservableCollection<AdminUserProp> systemSettingsList;
        public ObservableCollection<AdminUserProp> SystemSettingsList
        {
            get { return systemSettingsList; }
            set
            {
                if (value != null)
                {
                    this.systemSettingsList = value;
                    NotifyPropertyChanged("systemSettingsList");
                }
            }
        }

        private void BindGridDetails(object obj)
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                var list = new ObservableCollection<AdminUserProp>(_serviceInstance.GetSystemSettings());
                SystemSettingsList = null;
                SystemSettingsList = new ObservableCollection<AdminUserProp>(list);
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

        private string _valueKey;
        public string ValueKey
        {
            get
            {
                return _valueKey;
            }
            set
            {
                _valueKey = value;
                NotifyPropertyChanged("ValueKey");
            }
        }

        private string _valueDescription;
        public string ValueDescription
        {
            get
            {
                return _valueDescription;
            }
            set
            {
                _valueDescription = value;
                NotifyPropertyChanged("ValueDescription");
            }
        }

        private bool isSaveEnabled;
        public bool IsSaveEnabled
        {
            get
            {
                return isSaveEnabled;
            }
            set
            {
                isSaveEnabled = value;
                NotifyPropertyChanged("IsSaveEnabled");
            }
        }

        private bool isUpdateEnabled;
        public bool IsUpdateEnabled
        {
            get
            {
                return isUpdateEnabled;
            }
            set
            {
                isUpdateEnabled = value;
                NotifyPropertyChanged("IsUpdateEnabled");
            }
        }

        private bool isModifyEnabled;
        public bool IsModifyEnabled
        {
            get
            {
                return isModifyEnabled;
            }
            set
            {
                isModifyEnabled = value;
                NotifyPropertyChanged("IsModifyEnabled");
            }
        }

        private bool isDeleteEnabled;
        public bool IsDeleteEnabled
        {
            get
            {
                return isDeleteEnabled;
            }
            set
            {
                isDeleteEnabled = value;
                NotifyPropertyChanged("IsDeleteEnabled");
            }
        }

        private bool isFindEnabled;
        public bool IsFindEnabled
        {
            get
            {
                return isFindEnabled;
            }
            set
            {
                isFindEnabled = value;
                NotifyPropertyChanged("IsFindEnabled");
            }
        }

        private bool isKeyReadOnly;
        public bool IsKeyReadOnly
        {
            get
            {
                return isKeyReadOnly;
            }
            set
            {
                isKeyReadOnly = value;
                NotifyPropertyChanged("IsKeyReadOnly");
            }
        }

        private bool isDescriptionReadOnly;
        public bool IsDescriptionReadOnly
        {
            get
            {
                return isDescriptionReadOnly;
            }
            set
            {
                isDescriptionReadOnly = value;
                NotifyPropertyChanged("IsDescriptionReadOnly");
            }
        }

        private AdminUserProp selectedGridItem;
        public AdminUserProp SelectedGridItem
        {
            get
            {
                return selectedGridItem;
            }
            set
            {
                selectedGridItem = value; NotifyPropertyChanged("SelectedGridItem");
            }
        }

        private Window _currentWindow;
        public Window CurrentWindow
        {
            get
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.Title.Equals("New System Settings", StringComparison.OrdinalIgnoreCase) || window.Title.Equals("Find System Settings", StringComparison.OrdinalIgnoreCase))
                    {
                        _currentWindow = window;
                    }
                }
                return _currentWindow;
            }
        }

        private ICommand _btnCancel_Click;
        public ICommand BtnCancel_Click
        {
            get
            {
                if (_btnCancel_Click == null)
                {
                    _btnCancel_Click = new AppWorxCommand(
                        param => this.Cancel()
                        );
                }
                return _btnCancel_Click;
            }
        }

        private ICommand _btnFind_Click;
        public ICommand BtnFind_Click
        {
            get
            {
                if (_btnFind_Click == null)
                {
                    _btnFind_Click = new AppWorxCommand(
                        param => this.Find()
                        );
                }
                return _btnFind_Click;
            }
        }

        private ICommand _btnModify_Click;
        public ICommand BtnModify_Click
        {
            get
            {
                if (_btnModify_Click == null)
                {
                    _btnModify_Click = new AppWorxCommand(
                        param => this.Modify()
                        );
                }
                return _btnModify_Click;
            }
        }

        private ICommand _btnUpdate_Click;
        public ICommand BtnUpdate_Click
        {
            get
            {
                if (_btnUpdate_Click == null)
                {
                    _btnUpdate_Click = new AppWorxCommand(
                        param => this.Update()
                        );
                }
                return _btnUpdate_Click;
            }
        }

        private ICommand _btnSave_Click;
        public ICommand BtnSave_Click
        {
            get
            {
                if (_btnSave_Click == null)
                {
                    _btnSave_Click = new AppWorxCommand(
                        param => this.Save()
                        );
                }
                return _btnSave_Click;
            }
        }

        private ICommand _btnDelete_Click;
        public ICommand BtnDelete_Click
        {
            get
            {
                if (_btnDelete_Click == null)
                {
                    _btnDelete_Click = new AppWorxCommand(
                        param => this.Delete()
                        );
                }
                return _btnDelete_Click;
            }
        }

        private ICommand _btnFillData_Click;
        public ICommand BtnFillData_Click
        {
            get
            {
                if (_btnFillData_Click == null)
                {
                    _btnFillData_Click = new AppWorxCommand(
                        param => this.ItemSelected()
                        );
                }
                return _btnFillData_Click;
            }
        }

        private void ItemSelected()
        {
            ModifyMode();
            Clear();
        }

        private void FillData()
        {
            if (SelectedGridItem != null)
            {
                ValueKey = SelectedGridItem.ValueKey;
                ValueDescription = SelectedGridItem.ValueDescription;
            }
        }

        private void Cancel()
        {

            if (IsSaveEnabled || IsFindEnabled)
            {
                CurrentWindow.Close();
            }

            if (IsUpdateEnabled)
            {
                ModifyMode();
            }
            else if (IsModifyEnabled)
            {
                FindMode();

                Clear();
            }


        }

        private void Find()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //if (string.IsNullOrEmpty(ValueKey) && string.IsNullOrEmpty(ValueDescription))
                //{
                //    return;
                //}

                AdminUserProp settings = new AdminUserProp();
                settings.ValueKey = ValueKey;
                settings.ValueDescription = ValueDescription;
                SystemSettingsList = null;
                SystemSettingsList = new ObservableCollection<AdminUserProp>(_serviceInstance.FindSystemSettings(settings));
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

        private void Modify()
        {
            UpdateMode();
            FillData();
        }

        private void Update()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            bool isSucceed = false;
            try
            {
                if (string.IsNullOrEmpty(ValueKey))
                {
                    MessageBox.Show("Enter Value Key");
                    return;
                }

                if (string.IsNullOrEmpty(ValueDescription))
                {
                    MessageBox.Show("Enter Value Description");
                    return;
                }

                if (SelectedGridItem != null)
                {

                    var existingSettings = _serviceInstance.GetSystemSettings();
                    AdminUserProp setting = new AdminUserProp();
                    setting.ValueKey = SelectedGridItem.ValueKey;
                    setting.ValueDescription = ValueDescription;
                    _serviceInstance.UpdateSystemSettings(setting);
                    var item = SystemSettingsList.Where(x => x.ValueKey == setting.ValueKey).FirstOrDefault();
                    SystemSettingsList.Remove(item);
                    systemSettingsList.Add(setting);
                    isSucceed = true;
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
                if (isSucceed)
                {
                    MessageBox.Show(Resources.msgUpdatedSuccessfully);
                    ModifyMode();
                }
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        private void Delete()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));            

            bool isSucceed = false;
            try
            {
                if (SelectedGridItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are You Sure to Delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Cancel || result == MessageBoxResult.No)
                    {
                        return;
                    }
                    AdminUserProp setting = new AdminUserProp();
                    setting.ValueDescription = SelectedGridItem.ValueDescription;
                    setting.ValueKey = SelectedGridItem.ValueKey;
                    _serviceInstance.DeleteSystemSettings(setting);
                    SystemSettingsList.Remove(SelectedGridItem);
                    isSucceed = true;
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
                if (isSucceed)
                {
                    MessageBox.Show(Resources.msgDeleteSuccessfully);
                    ModifyMode();
                }
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }

        }

        private void Save()
        {
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            bool isSucceed = false;
            try
            {
                if (string.IsNullOrEmpty(ValueKey))
                {
                    MessageBox.Show("Enter Value Key");
                    return;
                }

                if (string.IsNullOrEmpty(ValueDescription))
                {
                    MessageBox.Show("Enter Value Description");
                    return;
                }

                var list = _serviceInstance.GetSystemSettings();
                var exists = list.Where(x => x.ValueKey.Equals(ValueKey, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (exists != null)
                {
                    MessageBox.Show("Value Key Already Exists");
                    return;
                }

                AdminUserProp setting = new AdminUserProp();
                setting.ValueDescription = ValueDescription;
                setting.ValueKey = ValueKey;
                _serviceInstance.InsertSystemSettings(setting);
                SystemSettingsList.Add(setting);
                isSucceed = true;
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
                if (isSucceed)
                {
                    MessageBox.Show(Resources.msgInsertedSuccessfully);
                    FindMode();
                }
                CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            }
        }

        private void Clear()
        {
            ValueKey = string.Empty;
            ValueDescription = string.Empty;

        }

        private void ModifyMode()
        {
            IsModifyEnabled = true;
            IsUpdateEnabled = false;
            IsSaveEnabled = false;
            IsFindEnabled = false;
            IsDeleteEnabled = true;
        }

        private void UpdateMode()
        {
            IsUpdateEnabled = true;
            IsModifyEnabled = false;
            IsSaveEnabled = false;
            IsFindEnabled = false;
            IsDeleteEnabled = false;
            IsKeyReadOnly = true;
            IsDescriptionReadOnly = false;
        }

        private void SaveMode()
        {
            IsSaveEnabled = true;
            IsModifyEnabled = false;
            IsDeleteEnabled = false;
            IsFindEnabled = false;
            IsDeleteEnabled = false;
        }

        private void FindMode()
        {
            IsFindEnabled = true;
            IsSaveEnabled = false;
            IsModifyEnabled = false;
            IsDeleteEnabled = false;
            IsUpdateEnabled = false;
            SelectedGridItem = null;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            //if (!disposedValue)
            //{
            //    if (disposing)
            //    {
            //        DelegateEventCodeAdmin.OnSetCodeAdminValueEvt -= new DelegateEventCodeAdmin.SetCodeAdminValue(GetCodeAdminValue);
            //        DelegateEventCodeAdmin.OnSetCodeAdminValueEvtCmdReferesh -= new DelegateEventCodeAdmin.SetCodeAdminValueCmdReferesh(GetCodeAdminValue);
            //        DelegateEventCodeAdmin.OnSetValuePageNumberCmd -= new DelegateEventCodeAdmin.SetValuePageNumberClick(GetCurrentPageIndex);
            //    }
            //    disposedValue = true;
            //}
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
