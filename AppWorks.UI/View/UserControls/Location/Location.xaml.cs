using AppWorks.UI.ViewModel.Location;
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

namespace AppWorks.UI.View.UserControls.Location
{
    /// <summary>
    /// Interaction logic for Location.xaml
    /// </summary>
    public partial class Location : UserControl
    {
        public Location()
        {
            InitializeComponent();
            //this.DataContext = new LocationVM();
            this.grdLocationList.Loaded += this.OnLoaded;
        }
        /// <summary>
        /// This is page index chnaged event for gird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLocationListPage_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            int NewPageSize = e.NewPageIndex;
            if (NewPageSize >= 0)
                LocationDeligate.SetValueMethodPageNumber(NewPageSize);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.grdLocationList.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
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
                Application.Current.Properties["tempScrollCount"] = 1;
                LocationDeligate.SetValueMethodPageNumber(Convert.ToInt32(Application.Current.Properties["FindUserGridCurrentPageIndex"] ?? "0"));
                Application.Current.Properties["tempScrollCount"] = 1;
            }
        }

    }
}
