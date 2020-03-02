using System.Windows.Controls;
using System.Windows.Input;

namespace AppWorks.UI.View.WebPortal
{
    /// <summary>
    /// Interaction logic for WebPortalUserDetailsView.xaml
    /// </summary>
    public partial class WebPortalUserDetailsView : UserControl
    {
        public WebPortalUserDetailsView()
        {
            InitializeComponent();
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
    }
}
