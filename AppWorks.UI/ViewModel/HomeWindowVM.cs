using AppWorks.UI.Common;
using AppWorks.UI.Infrastructure.Navigation;
using AppWorks.UI.Properties;
using AppWorks.UI.View;
using AppWorks.UI.View.Billing;
using AppWorks.UI.View.PortStorageImportExport;
using AppWorks.UI.View.PortStorageInvoices;
using AppWorks.UI.View.UserControls.CodeAdmin;
using AppWorks.UI.View.UserControls.UserAdmin;
using AppWorks.UI.View.UserControls.Vehicle;
using AppWorks.UI.View.WebPortal;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using AppWorks.UI.ViewModel.Vehicle;
using System.Configuration;
using AppWorks.Utilities;

namespace AppWorks.UI.ViewModel
{
    /// <summary>
    /// HomeViewModel Class To Contains Property for HomeView
    /// </summary>
    public class HomeWindowVM : ViewModelBase
    {
        public HomeWindowVM()
        {
            userName = Application.Current.Properties["LoggedInUserNameHome"].ToString();
            var version = ConfigurationManager.AppSettings["CurrentApplicationVersion"].ToString();
            AppVersion = string.Format("Diversified {0}", version);
            char[] objRolesSplit = new char[] { ',' };
            var objRoles = Application.Current.Properties["LoggedInUserRole"].ToString().Split(objRolesSplit);

            NavigationNode portStorageNode = null;

            if (objRoles.Contains(UserRoles.Administrator.GetUserRoleToCompare()))
            {

                NavigationNode adminNode = rootNavigationNode.AddChild("Admin");
                adminNode.Icon = "";
                adminNode.AddChild("Add New User", typeof(AddAdminUser)).Icon = "";
                adminNode.AddChild("Customer Admin", typeof(View.UserControls.CustomerAdmin.CustomerAdmin)).Icon = "";
                adminNode.AddChild("Code Admin", typeof(CodesTableAdmin)).Icon = "";
                adminNode.AddChild("Billing Period Admin", typeof(BillingPeriodAdmin)).Icon = "";
                adminNode.AddChild("System Settings Admin", typeof(SystemSettings)).Icon = "";
                adminNode.AddChild("Company Information", typeof(CompanyInformation)).Icon = "";
                //adminNode.AddChild("Add/Edit Billing Records", typeof(AddEditBillingRecords)).Icon = "";
            }
            if (objRoles.Contains(UserRoles.PortStorage.GetUserRoleToCompare()) || objRoles.Contains(UserRoles.Administrator.GetUserRoleToCompare()) || objRoles.Contains(UserRoles.Security.GetUserRoleToCompare()))
            {
                portStorageNode = rootNavigationNode.AddChild("Port Storage");
                portStorageNode.Icon = "";

                if (objRoles.Contains(UserRoles.PortStorage.GetUserRoleToCompare()) || objRoles.Contains(UserRoles.Administrator.GetUserRoleToCompare()))
                {
                    portStorageNode.AddChild("Add-Edit PS Records", typeof(PortStorageVehicalLocator)).Icon = "";
                    portStorageNode.AddChild("Request Processing", typeof(PostStorageRequestProcessing)).Icon = "";
                    portStorageNode.AddChild("Date Out Processing", typeof(PostStorageDateOutProcessing)).Icon = "";
                    portStorageNode.AddChild("Storage Vehicle Outgate", typeof(StorageVehicleOutgate)).Icon = "";
                }
                if (objRoles.Contains(UserRoles.Security.GetUserRoleToCompare()))
                {
                    var existingOutgateNode = portStorageNode.Children.Where(x => x.Name.Equals("Storage Vehicle Outgate", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (existingOutgateNode == null)
                    {
                        portStorageNode.AddChild("Storage Vehicle Outgate", typeof(StorageVehicleOutgate)).Icon = "";
                    }
                }
                if (objRoles.Contains(UserRoles.PortStorage.GetUserRoleToCompare()) || objRoles.Contains(UserRoles.Administrator.GetUserRoleToCompare()))
                {
                    portStorageNode.AddChild("Port Storage Reports", typeof(View.Reports.Report)).Icon = "";

                    //portStorageNode.AddChild("Import/Export", typeof(PortStorageImportExport)).Icon = "";
                    portStorageNode.AddChild("Vehicle Import YMS", typeof(PortStorageVehicleImportYMSWindow)).Icon = "";
                    portStorageNode.AddChild("Location Import YMS", typeof(AppWorks.UI.View.PortStorageImportExport.PortStorageLocationImportYMSWindow)).Icon = "";
                    portStorageNode.AddChild("Customer Web Portal", typeof(WebPortalAdministration)).Icon = "";
                    portStorageNode.AddChild("Port Storage Inventory", typeof(PortStorageVehicleInventoryDetails)).Icon = "";
                }
            }

            if (objRoles.Contains(UserRoles.Administrator.GetUserRoleToCompare()))
            {
                NavigationNode billingNode = rootNavigationNode.AddChild("Billing");
                billingNode.Icon = "";
                billingNode.AddChild("Generate Invoices", typeof(GeneratePortStorageInvoices)).Icon = "";
                billingNode.AddChild("Add-Edit Invoice Records", typeof(AddEditInvoiceRecords)).Icon = "";
                billingNode.AddChild("Billing Record Export", typeof(BillingRecordExport)).Icon = "";

            }
            Nodes = new ObservableCollection<NavigationNode>(rootNavigationNode.Children);
            //select default node

            var addEditRecords = portStorageNode.Children.SingleOrDefault(c => c.Name == "Add-Edit PS Records");
            if (addEditRecords == null)
            {
                addEditRecords = portStorageNode.Children.FirstOrDefault();
            }

            if (addEditRecords != null && portStorageNode != null)
            {
                portStorageNode.IsExpanded = true;
                addEditRecords.IsSelected = true;
                addEditRecords.IsFirstTimeLoaded = true;
            }
            if (!objRoles.Contains(UserRoles.Security.GetUserRoleToCompare()))
            {
                AutoLogOffHelper.MakeAutoLogOffEvent += new AutoLogOffHelper.MakeAutoLogOff(LoginoutUser);
                AutoLogOffHelper.StartAutoLogoffOption();
            }

            NavigateTo(addEditRecords);

        }


        private NavigationNode currentNode;
        public NavigationNode CurrentNode
        {
            get { return currentNode; }
            private set
            {
                currentNode = value;
                NotifyPropertyChanged("CurrentNode");
            }
        }

        private bool isAnyExpanded;
        public bool IsAnyExpanded
        {
            get { return this.isAnyExpanded; }
            set
            {
                if (this.isAnyExpanded == value) return;
                this.isAnyExpanded = value;
                NotifyPropertyChanged("IsAnyExpanded");
            }
        }

        private NavigationNode rootNavigationNode = new NavigationNode();
        public NavigationNode RootNavigationNode
        {
            get { return rootNavigationNode; }
            set
            {
                if (rootNavigationNode == value) return;
                rootNavigationNode = value;
                NotifyPropertyChanged("RootNavigationNode");
            }
        }

        private string navigationPath;
        public string NavigationPath
        {
            get { return this.navigationPath; }
            set
            {
                if (this.navigationPath == value) return;
                this.navigationPath = value;
                NotifyPropertyChanged("NavigationPath");
            }
        }

        private ObservableCollection<NavigationNode> Nndes;
        public ObservableCollection<NavigationNode> Nodes
        {
            get { return this.Nndes; }
            set
            {
                if (this.Nndes == value) return;
                this.Nndes = value;
                NotifyPropertyChanged("Nodes");
            }
        }

        private object content;
        public object Content
        {
            get { return this.content; }
            set
            {
                if (this.content == value) return;
                this.content = value;
                NotifyPropertyChanged("Content");
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                NotifyPropertyChanged("UserName");
                if (value != null)
                { this.userName = value; }
            }
        }

        private string _appVersion;
        public string AppVersion
        {
            get { return _appVersion; }
            set
            {
                NotifyPropertyChanged("AppVersion");
                if (value != null)
                { this._appVersion = value; }
            }
        }

        private ICommand _btnLogout_Click;

        /// <summary>
        /// Submit button event
        /// </summary>
        public ICommand btnLogout_Click
        {
            get
            {
                if (_btnLogout_Click == null)
                {
                    _btnLogout_Click = new AppWorxCommand(
                        param => this.LoginoutUser(),
                        param => CanCheck
                        );
                }
                return _btnLogout_Click;
            }
        }

        public static bool CanCheck
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Function to check the Login Credentials.
        /// </summary>
        /// <param name="objLoginProp"></param>
        /// <returns>void</returns>
        /// <createdBy></createdBy>
        /// <createdOn>Apr-13,2016</createdOn>
        public void LoginoutUser()
        {
            GridMultiSelect multiSelect = new GridMultiSelect();
            CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                /// remove the role details for the logged-in user before the logout
                Application.Current.Properties["LoggedInUserRole"] = string.Empty;
                /// redirect to the dashboard page and close the login page.
                //MainWindow objMainWindow = new MainWindow();

                char[] objRolesSplit = new char[] { ',' };
                var objRoles = Application.Current.Properties["LoggedInUserRole"].ToString().Split(objRolesSplit);
                if (!objRoles.Contains(UserRoles.Security.GetUserRoleToCompare()))
                {
                    AutoLogOffHelper.MakeAutoLogOffEvent -= new AutoLogOffHelper.MakeAutoLogOff(LoginoutUser);
                }


                LogoutWindow objLogoutWindow = new LogoutWindow();
                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.DataContext == this)
                    {
                        window.Close();
                    }
                }
                objLogoutWindow.Show();
                AutoLogOffHelper.StopTimer();
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

        internal void NavigateTo(NavigationNode selected)
        {
            if (selected.Type != null)
            {
                if (typeof(Window).IsAssignableFrom(selected.Type))
                {
                    Window window = (Window)Activator.CreateInstance(selected.Type);
                    window.Show();
                }
                else
                {
                    GridMultiSelect multiSelect = new GridMultiSelect();
                    object view = Activator.CreateInstance(selected.Type);
                    Content = view;
                    CurrentNode = selected;
                    NavigationPath = GetPath(selected);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string GetPath(NavigationNode node)
        {
            string result = null;
            if (node.Parent != null && !string.IsNullOrEmpty(node.Parent.Name))
            {
                result = GetPath(node.Parent);
            }
            result = string.Concat(result, node.Name, " / ");
            return result;
        }

    }
}
