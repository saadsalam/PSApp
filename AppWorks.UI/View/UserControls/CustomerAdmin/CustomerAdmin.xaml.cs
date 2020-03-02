using AppWorks.UI.ViewModel.CustomerAdmin;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;



namespace AppWorks.UI.View.UserControls.CustomerAdmin
{
    /// <summary>
    /// Interaction logic for CustomerAdmin.xaml
    /// </summary>
    public partial class CustomerAdmin : UserControl
    {
        bool result = false;
        public CustomerAdmin()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.CustomerAdmin.CustomerAdminVM();
            this.grdPortStorageRatesList.Loaded += grdPortStorageRatesList_Loaded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void grdPortStorageRatesList_Loaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.grdPortStorageRatesList.ChildrenOfType<ScrollViewer>().FirstOrDefault();
            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged -= new System.Windows.Controls.ScrollChangedEventHandler(scrollViewer_ScrollChanged);
                scrollViewer.ScrollChanged += new System.Windows.Controls.ScrollChangedEventHandler(scrollViewer_ScrollChanged);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void scrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            int lastOffset = Convert.ToInt32(Application.Current.Properties["FindUserGridLastScrollOffset"] ?? "0");
            if (Convert.ToInt32(e.VerticalOffset) > 0 && Convert.ToInt32(e.VerticalOffset) > lastOffset)
            {
                Application.Current.Properties["FindUserGridLastScrollOffset"] = e.VerticalOffset;
                Application.Current.Properties["FindUserGridCurrentPageIndex"] = Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0") + 1;
                CustomerAdminDeligate.SetValueMethodPageNumber(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
            }
        }

        private void rdPgPending_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {

            int NewPageSize = e.NewPageIndex;
            if (NewPageSize > 0)
            {
                result = true;
            }
            if (NewPageSize >= 0)
            {
                if (result)
                    CustomerAdminDeligate.SetValueMethodPageNumber(NewPageSize);
            }

        }
    }
}
