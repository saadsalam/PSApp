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
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppWorks.UI.ViewModel.Vehicle;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Filtering.Editors;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using System.Text;
using System.IO;
using AppWorks.UI.ViewModel.Vehicle;
using Props = AppWorks.UI.Properties;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;
using AppWorks.UI.ViewModel.Billing;
using System.Diagnostics;
using System.Threading;

namespace AppWorks.UI.View.Billing
{
    /// <summary>
    /// Interaction logic for InvoiceRecordFind.xaml
    /// </summary>
    public partial class InvoiceRecordFind : Window
    {
        bool result = false;

        public InvoiceRecordFind()
        {
            DataContextChanged += InvoiceRecordFind_DataContextChanged;
            Closed += InvoiceRecordFind_Closed;
            InitializeComponent();
            DataContext = InvoiceRecordFindDockPanel.DataContext;
            this.grdBillingList.Loaded += this.OnLoaded;
        }

        private void InvoiceRecordFind_Closed(object sender, EventArgs e)
        {
            var billingFindVM = InvoiceRecordFindDockPanel.DataContext as BillingFindVM;
            //dispose the delegates before closing the window
            billingFindVM.Dispose();
            UnsubscribeFromViewModelEvents(DataContext as InvoiceRecordFindViewModel);
        }

        private void InvoiceRecordFind_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UnsubscribeFromViewModelEvents(e.OldValue as InvoiceRecordFindViewModel);
            SubscribeForViewModelEvents(e.NewValue as InvoiceRecordFindViewModel);
        }

        private void SubscribeForViewModelEvents(InvoiceRecordFindViewModel viewModel)
        {
            if (viewModel != null)
                viewModel.CloseWindowRequested += ViewModel_CloseWindowRequested;
        }

        private void UnsubscribeFromViewModelEvents(InvoiceRecordFindViewModel viewModel)
        {
            if (viewModel != null)
                viewModel.CloseWindowRequested -= ViewModel_CloseWindowRequested;
        }
        private void ViewModel_CloseWindowRequested(object sender, EventArgs e)
        {
            if (sender == DataContext)
            { Close(); }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.grdBillingList.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += new System.Windows.Controls.ScrollChangedEventHandler(scrollViewer_ScrollChanged);
            }
        }

        void scrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {           
            int lastOffset = Convert.ToInt32(Application.Current.Properties["FindUserGridLastScrollOffset"] ?? "0");
            var scrollViewer = this.grdBillingList.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
            if (scrollViewer != null)
            {
                // Below  decides when to get the records
                // Implemented the logic that when scrollbar crosses more that half of the total available height
                var whenToScroll = scrollViewer.ScrollableHeight / 1.3;
                if (Convert.ToInt32(e.VerticalOffset) > 0 && Convert.ToInt32(e.VerticalOffset) > lastOffset && e.VerticalOffset > whenToScroll)
                {
                    Application.Current.Properties["FindUserGridLastScrollOffset"] = e.VerticalOffset;
                    Application.Current.Properties["FindUserGridCurrentPageIndex"] = Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0") + 1;
                    DelegateEventBillingPeriod.SetValueMethodPageNumber(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
                }
            }
        }

        private void GridViewRow_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        /// <summary>
        /// This is Field Filter Editor Created event for gird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadGridView_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e)
        {
            var stringFilterEditor = e.Editor as StringFilterEditor;

            if (stringFilterEditor != null)
            {
                e.Editor.Loaded += (s1, e1) =>
                {
                    var textBox = e.Editor.ChildrenOfType<TextBox>().Single();
                    textBox.TextChanged += (s2, e2) =>
                    {
                        textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    };
                };
            }
        }

        private void RadGridView_FilterOperatorsLoading(object sender, Telerik.Windows.Controls.GridView.FilterOperatorsLoadingEventArgs e)
        {
            if (e.AvailableOperators.Contains(FilterOperator.Contains))
            {
                e.DefaultOperator1 = FilterOperator.Contains;
            }
        }


        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var s = e.OriginalSource as FrameworkElement;
            var parentRow = s.ParentOfType<GridViewRow>();
            if (parentRow != null)
            {
                if (parentRow.DetailsProvider.Visibility == System.Windows.Visibility.Visible)
                {
                    parentRow.IsSelected = false;
                }
            }
        }
        private void ExportToCsv()
        {

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
                    DelegateEventVehicle.SetValueMethodPageNumber(NewPageSize);
                //DelegateEventVehicle.OnPageNumberClickEvt(NewPageSize);
            }

        }

        private void grdBillingList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //foreach (Window win in Application.Current.Windows)
            //{
            //    if (win.Title != AppWorks.UI.Properties.Resources.WinHomeTitle)
            //    {
            this.Close();
            //    }
            //}
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
