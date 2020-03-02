using AppWorks.UI.ViewModel.WebPortal;
using AppWorksService.Properties;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace AppWorks.UI.View.WebPortal
{
    /// <summary>
    /// Interaction logic for PortStorageVehicleInventoryDetails.xaml
    /// </summary>
    public partial class PortStorageVehicleInventoryDetails : UserControl
    {
        public PortStorageVehicleInventoryDetails()
        {
            InitializeComponent();
            this.DataContext = new VehicleInventoryVM();
            //double width = System.Windows.SystemParameters.PrimaryScreenWidth;
            //double height = System.Windows.SystemParameters.PrimaryScreenHeight;
            //VehicleInventoryGrid.Height = (height - 350) ;
            this.VehicleInventoryGrid.Loaded += this.OnLoaded;
            Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = this.VehicleInventoryGrid.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
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
        private void rdPgPending_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {   
            int NewPageSize = e.NewPageIndex;
            if (NewPageSize >= 0)
                WebPortalDelegate.SetValueMethodPageNumber(NewPageSize);

        }

        private void VehicleInventoryGrid_RowLoaded(object sender, RowLoadedEventArgs e)
        {
            if (e.Row.DataContext is PortStorageVehicleProp)
            {
                System.Windows.Media.Color colReq = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#F7D7D5"); 
                System.Windows.Media.Color colOut = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#D5D7F7");
                e.Row.Background = (e.Row.DataContext as PortStorageVehicleProp).VehicleStatus == "OutGated" ? new System.Windows.Media.SolidColorBrush(colOut) : (e.Row.DataContext as PortStorageVehicleProp).VehicleStatus == "Requested" ? new System.Windows.Media.SolidColorBrush(colReq) : null;
            }
        }
    }
}
