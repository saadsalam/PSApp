using AppWorks.UI.ViewModel.Billing;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace AppWorks.UI.View.Billing
{
    /// <summary>
    /// Interaction logic for AddEditInvoiceRecords.xaml
    /// </summary>
    public partial class AddEditInvoiceRecords : UserControl
    {
        public AddEditInvoiceRecords()
        {
            InitializeComponent();
            this.DataContext = new AddEditBillingVM();
            this.grdInvoiceDetails.Loaded += this.OnLoaded;
            this.grdInvoiceLineItems.Loaded += this.OnLoaded_item;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.grdInvoiceDetails.ChildrenOfType<Telerik.Windows.Controls.GridView.GridViewScrollViewer>().FirstOrDefault();
            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += new System.Windows.Controls.ScrollChangedEventHandler(scrollViewer_ScrollChanged);
            }
        }

        void scrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            int lastOffset = Convert.ToInt32(Application.Current.Properties["FindUserGridLastScrollOffset"] ?? "0");
            if (Convert.ToInt32(e.VerticalOffset) > 0 && Convert.ToInt32(e.VerticalOffset) > lastOffset)
            {
                Application.Current.Properties["FindUserGridLastScrollOffset"] = e.VerticalOffset;
                Application.Current.Properties["FindUserGridCurrentPageIndex"] = Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0") + 1;
                DelegateEventBillingPeriod.SetValueMethodPageNumberInvoice(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
            }
        }

        private void OnLoaded_item(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.grdInvoiceLineItems.ChildrenOfType<Telerik.Windows.Controls.GridView.GridViewScrollViewer>().FirstOrDefault();
            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += new System.Windows.Controls.ScrollChangedEventHandler(scrollViewer_ScrollChanged_item);
            }
        }

        void scrollViewer_ScrollChanged_item(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            int lastOffset = Convert.ToInt32(Application.Current.Properties["FindUserGridLastScrollOffset"] ?? "0");
            if (Convert.ToInt32(e.VerticalOffset) > 0 && Convert.ToInt32(e.VerticalOffset) > lastOffset)
            {
                Application.Current.Properties["FindUserGridLastScrollOffset"] = e.VerticalOffset;
                Application.Current.Properties["FindUserGridCurrentPageIndex"] = Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0") + 1;
                DelegateEventBillingPeriod.SetValueMethodPageNumberLineItem(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
            }
        }
    }
}
