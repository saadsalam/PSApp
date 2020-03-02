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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppWorks.UI.ViewModel.Vehicle;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace AppWorks.UI.View.UserControls.Vehicle
{
    /// <summary>
    /// Interaction logic for CustomerLocator.xaml
    /// </summary>
    public partial class CustomerLocator : Window
    {
        public CustomerLocator()
        {   
            DataContextChanged += CustomerLocator_DataContextChanged;
            Closed += CustomerLocator_Closed;
            DataContext = new CustomerVM();
            InitializeComponent();
            grdCustomers.Loaded += this.OnLoaded;
            txtCustomerName.Focus();
        }

        private void CustomerLocator_Closed(object sender, EventArgs e)
        {
            UnsubsribeFromViewModelEvents(DataContext as CustomerVM);
        }

        private void CustomerLocator_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UnsubsribeFromViewModelEvents(e.OldValue as CustomerVM);
            SubsribeForViewModelEvents(e.NewValue as CustomerVM);
        }

        private void ViewModel_CloseWindowRequested(object sender, EventArgs e)
        {
            if (sender != DataContext)
                return;
            Close();
        }

        private void SubsribeForViewModelEvents(CustomerVM viewModel)
        {
            if (viewModel == null)
                return;

            viewModel.CloseWindowRequested += ViewModel_CloseWindowRequested;
        }

        private void UnsubsribeFromViewModelEvents(CustomerVM viewModel)
        {
            if (viewModel == null)
                return;

            viewModel.CloseWindowRequested -= ViewModel_CloseWindowRequested;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.grdCustomers.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
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
                DelegateEventCustomer.SetValueMethodPageNumber(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
            }
        }

        /// <summary>
        /// This is key down event for input onlu numeric value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtZIP_KeyDown(object sender, KeyEventArgs e)
        {


            if (((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)) && e.Key != Key.Enter)
            {
                e.Handled = true;
            }

        }
        /// <summary>
        /// This is page index chnaged event for gird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdPgPending_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            int NewPageSize = e.NewPageIndex;
            if (NewPageSize >= 0)
                DelegateEventCustomer.SetValueMethodPageNumber(NewPageSize);

        }

        private void RadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement originalSender = e.OriginalSource as FrameworkElement;

            if (originalSender != null)
            {
                var row = originalSender.ParentOfType<GridViewRow>();
                if (row != null)
                {
                    this.Close();
                }
            }
        }

    }
}
