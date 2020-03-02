using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AppWorks.UI.ViewModel.Billing;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace AppWorks.UI.View.Billing
{
    /// <summary>
    /// Interaction logic for BillingPeriodFind.xaml
    /// </summary>
    public partial class BillingPeriodFind : Window
    {
        public BillingPeriodFind()
        {
            Closed += BillingPeriodFind_Closed;
            DataContextChanged += BillingPeriodFind_DataContextChanged;
            DataContext = new BillingPeriodFindVM();
            InitializeComponent();
            this.grdBilling.Loaded += this.OnLoaded;

        }

        private void BillingPeriodFind_Closed(object sender, EventArgs e)
        {
            UnsubscribeFromViewModelEvents(DataContext as BillingPeriodFindVM);
        }

        private void BillingPeriodFind_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UnsubscribeFromViewModelEvents(e.OldValue as BillingPeriodFindVM);
            SubscribeForViewModelEvents(e.NewValue as BillingPeriodFindVM);
        }

        private void SubscribeForViewModelEvents(BillingPeriodFindVM viewModel)
        {
            if (viewModel != null)
                viewModel.CloseWindowRequested += ViewModel_CloseWindowRequested;
        }

        private void UnsubscribeFromViewModelEvents(BillingPeriodFindVM viewModel)
        {
            if (viewModel != null)
                viewModel.CloseWindowRequested -= ViewModel_CloseWindowRequested;
        }
        private void ViewModel_CloseWindowRequested(object sender, EventArgs e)
        {
            if (sender == DataContext)
                Close();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.grdBilling.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
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
                AppWorks.UI.ViewModel.Vehicle.DelegateEventCustomer.SetValueMethodPageNumber(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
            }
        }
        private void RadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void txtCalendarYear_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
           
        }
    }
}
