using AppWorks.UI.ViewModel.CodeAdmin;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace AppWorks.UI.View.UserControls.CodeAdmin
{
    /// <summary>
    /// Interaction logic for CodeFind.xaml
    /// </summary>
    public partial class CodeFind : Window
    {
        public CodeFind()
        {
            Closed += CodeFind_Closed;
            DataContextChanged += CodeFind_DataContextChanged;
            DataContext = new CodeFindVM();
            InitializeComponent();
            this.grdCode.Loaded += this.OnLoaded;
        }

        private void CodeFind_Closed(object sender, EventArgs e)
        {
            UnsubrscribeFromViewModelEvents(DataContext as CodeFindVM);
        }

        private void CodeFind_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UnsubrscribeFromViewModelEvents(e.OldValue as CodeFindVM);
            SubscribeForViewModelEvents(e.NewValue as CodeFindVM);
        }

        private void SubscribeForViewModelEvents(CodeFindVM codeFindVM)
        {
            if (codeFindVM == null)
                return;
            codeFindVM.CloseWindowRequested += CodeFindVM_CloseWindowRequested;
        }

        private void UnsubrscribeFromViewModelEvents(CodeFindVM codeFindVM)
        {
            if (codeFindVM == null)
                return;
            codeFindVM.CloseWindowRequested -= CodeFindVM_CloseWindowRequested;
        }

        private void CodeFindVM_CloseWindowRequested(object sender, EventArgs e)
        {
            if (sender == DataContext)
                Close();
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
                DelegateEventCodeAdmin.SetValueMethodPageNumber(NewPageSize);

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

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.grdCode.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
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
                DelegateEventCodeAdmin.SetValueMethodPageNumber(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
            }
        }
    }
}
