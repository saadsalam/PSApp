using System.Windows.Controls;
using AppWorks.UI.ViewModel.AdminUser;
using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace AppWorks.UI.View.UserControls.ManageUser
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FindUser
    {
        public FindUser()
        {
            Closed += FindUser_Closed;
            DataContextChanged += FindUser_DataContextChanged;
            InitializeComponent();
            this.DataContext = new FindUserViewModel();
            this.UserGridView.Loaded += this.OnLoaded;
            Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
        }

        private void FindUser_Closed(object sender, EventArgs e)
        {
            UnsubscribeFromViewModelEvents(DataContext as FindUserViewModel);
        }

        private void FindUser_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UnsubscribeFromViewModelEvents(e.OldValue as FindUserViewModel);
            SubscribeForViewModelEvents(e.NewValue as FindUserViewModel);
        }

        private void SubscribeForViewModelEvents(FindUserViewModel findUserViewModel)
        {
           if (findUserViewModel != null)
                findUserViewModel.CloseWindowRequested += FindUserViewModel_CloseWindowRequested;
        }

        private void UnsubscribeFromViewModelEvents(FindUserViewModel findUserViewModel)
        {
            if (findUserViewModel != null)
                findUserViewModel.CloseWindowRequested -= FindUserViewModel_CloseWindowRequested;
        }
        private void FindUserViewModel_CloseWindowRequested(object sender, EventArgs e)
        {
            if (sender == DataContext)
                Close();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.UserGridView.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
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
                FindUserDeligate.SetValueMethodPageNumber(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
            }
        }
        private void rdPgPending_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            int NewPageSize = e.NewPageIndex;
            if (NewPageSize >= 0)
                FindUserDeligate.SetValueMethodPageNumber(NewPageSize);

        }

        private void UserGridView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FrameworkElement originalSender = e.OriginalSource as FrameworkElement;

            if (originalSender != null)
            {
                //var cell = originalSender.ParentOfType<GridViewCell>();
                //if (cell != null)
                //{
                //    MessageBox.Show("The double-clicked cell is " + cell.Value);
                //}
                var row = originalSender.ParentOfType<GridViewRow>();
                if (row != null)
                {
                    this.Close();
                }
            }

            
        }
    }
}