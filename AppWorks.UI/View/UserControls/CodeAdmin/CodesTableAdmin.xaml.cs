using AppWorks.UI.ViewModel.CodeAdmin;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppWorks.UI.View.UserControls.CodeAdmin
{
    /// <summary>
    /// Interaction logic for CodesTableAdmin.xaml
    /// </summary>
    public partial class CodesTableAdmin : UserControl
    {
        public CodesTableAdmin()
        {
            InitializeComponent();
            DataContext = new CodesTableAdminVM();
        }
        /// <summary>
        /// This is key down event for input onlu numeric value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSortOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
            
        }
    }
}
