using AppWorks.UI.ViewModel.WebPortal;
using AppWorksService.Properties;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace AppWorks.UI.View.WebPortal
{
    /// <summary>
    /// Interaction logic for WebPortalUsersView.xaml
    /// </summary>
    public partial class WebPortalUsersView : UserControl
    {
        public WebPortalUsersView()
        {
            InitializeComponent();

            this.UserGridView.Loaded += this.OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.UserGridView.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += new ScrollChangedEventHandler(scrollViewer_ScrollChanged);
            }
        }

        void scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            int lastOffset = Convert.ToInt32(Application.Current.Properties["FindUserGridLastScrollOffset"] ?? "0");
            if (Convert.ToInt32(e.VerticalOffset) > 0 && Convert.ToInt32(e.VerticalOffset) > lastOffset)
            {
                Application.Current.Properties["FindUserGridLastScrollOffset"] = e.VerticalOffset;
                Application.Current.Properties["FindUserGridCurrentPageIndex"] = Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0") + 1;
                WebPortalDelegate.SetValueMethodPageNumber(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
            }
        }

        private void UserGridView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            var senderElement = e.OriginalSource as FrameworkElement;
            var row = senderElement.ParentOfType<GridViewRow>();

            if (row != null)
            {
                var rows = this.UserGridView.ChildrenOfType<GridViewRow>();

                foreach (var restrow in rows)
                {
                    restrow.IsSelected = false;
                } 

                row.IsSelected = true;

                FrameworkElement originalSender = e.OriginalSource as FrameworkElement;
                object oTest = sender.GetType();
                if (originalSender != null)
                {
                    WebPortalUserList RowItem = (WebPortalUserList)row.Item;

                    var cell = originalSender.ParentOfType<Button>();
                    if (cell != null)
                    {
                        if (cell.Content.ToString().ToLower() == "edit")
                        {
                            WebPortalDelegate.SetValueMethodEditCmd("Edit", RowItem);
                        }
                        else if (cell.Content.ToString().ToLower() == "delete")
                        {
                            WebPortalDelegate.SetValueMethodEditCmd("Delete", RowItem);
                        }
                    }
                }

            }
            
        }
    }
}
