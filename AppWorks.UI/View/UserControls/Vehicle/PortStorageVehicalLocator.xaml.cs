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

namespace AppWorks.UI.View.UserControls.Vehicle
{
    /// <summary>
    /// Interaction logic for PortStorageVehicalLocator.xaml
    /// </summary>
    public partial class PortStorageVehicalLocator : UserControl
    {
        public PortStorageVehicalLocator()
        {
            InitializeComponent();
            this.DataContext = new VehicleLocatorVM();
        }

        private void VehicleLoc_SelectionChanged(object sender, Telerik.Windows.Controls.RadSelectionChangedEventArgs e)
        {
            if (PortStorageVehicalDetails == null || e.AddedItems.Count != 1)
                return;
            if (e.AddedItems[0] == detailsTab)
            {
                PortStorageVehicalDetails.FocusListOfItemsIfExist();
            }
        }
    }
}
