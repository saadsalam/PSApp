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
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Filtering.Editors;
using Telerik.Windows.Data;
using AppWorks.UI.ViewModel.Vehicle;

namespace AppWorks.UI.View.UserControls.Vehicle
{
    /// <summary>
    /// Interaction logic for MultiVIN.xaml
    /// </summary>
    public partial class MultiVIN : Window
    {
        public MultiVIN(string VIN)
        {
            InitializeComponent();
            this.DataContext = new MultiVinVM(VIN);
            txtMultiVin.Focus();
        }

        public void CloseFunction(bool value)
        {
            if (value == true)
            {
                this.Close();
            }
        }
    }
}
