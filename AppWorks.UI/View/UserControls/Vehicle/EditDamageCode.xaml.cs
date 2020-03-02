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
using AppWorks.UI.ViewModel.Vehicle;

namespace AppWorks.UI.View.UserControls.Vehicle
{
    /// <summary>
    /// Interaction logic for EditDamageCode.xaml
    /// </summary>
    public partial class EditDamageCode : Window
    {
        public EditDamageCode(int damageDetailId)
        {
            InitializeComponent();
            this.DataContext = new EditDamageCodeVM(damageDetailId);
        }

        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEditDamage_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
    }
}
