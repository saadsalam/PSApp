using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AppWorks.UI.ViewModel.Vehicle;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Filtering.Editors;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using AppWorks.UI.Models;
using System.ComponentModel;

namespace AppWorks.UI.View.UserControls.Vehicle
{
    /// <summary>
    /// Interaction logic for VehicleLocator.xaml
    /// </summary>
    public partial class VehicleLocator : UserControl
    {
        bool result = false;
        public VehicleLocator()
        {
            InitializeComponent();
            this.grdUserList.Loaded += this.OnLoaded;
            Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.grdUserList.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += new System.Windows.Controls.ScrollChangedEventHandler(scrollViewer_ScrollChanged);
            }
        }

        void scrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            int lastOffset = Convert.ToInt32(Application.Current.Properties["FindUserGridLastScrollOffset"] ?? "0");
            if (Convert.ToInt32(e.VerticalOffset) > 0 && Convert.ToInt32(e.VerticalOffset) > lastOffset)
            {
                Application.Current.Properties["FindUserGridLastScrollOffset"] = e.VerticalOffset;
                if ((scrollViewer.VerticalOffset / scrollViewer.ScrollableHeight) * 100 > 80)
                {
                    Application.Current.Properties["FindUserGridCurrentPageIndex"] = Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0") + 1;
                    DelegateEventVehicle.SetValueMethodPageNumber(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
                }
            }
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

        private void rdPgPending_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {

            int NewPageSize = e.NewPageIndex;
            if (NewPageSize > 0)
            {
                result = true;
            }
            if (NewPageSize >= 0)
            {
                //if(result)
                //   DelegateEventVehicle.SetValueMethodPageNumber(NewPageSize);
                //DelegateEventVehicle.OnPageNumberClickEvt(NewPageSize);
            }

        }

        /// <summary>
        /// Double clicking on Grid would bind the current row in Port Storage Vehicle Detail tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdUserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement originalSender = e.OriginalSource as FrameworkElement;

            var items = grdUserList.Items;

            if (originalSender != null)
            {
                var row = originalSender.ParentOfType<GridViewRow>();

                if (row != null)
                {
                    row.IsSelected = true;
                    foreach (Window win in Application.Current.Windows)
                    {
                        if (win.Title == Properties.Resources.WinHomeTitle)
                        {
                            HomeWindow home = (HomeWindow)win;
                            foreach (RadTabControl tc in FindVisualChildren<RadTabControl>(win))
                            {
                                tc.SelectedIndex = (int)Enums.PortStorageTabs.VehicleDetail;
                                break;
                            }
                        }
                    }
                }
            }
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

        public SortingState SortState { get; set; }
        public Telerik.Windows.Controls.GridViewColumn SortedColumn { get; set; }

        private void grdUserList_Sorting(object sender, GridViewSortingEventArgs e)
        {
            e.Cancel = true;
            DelegateEventVehicle.SetGridSort(e.Column.Header.ToString(), string.Empty);
        }

        private void grdUserList_DataLoaded(object sender, EventArgs e)
        {
            //SortDescriptor sd = new SortDescriptor();
            //sd.SortDirection = ListSortDirection.Ascending;            
            //this.grdUserList.SortDescriptors.Add(sd);
        }
    }
}
