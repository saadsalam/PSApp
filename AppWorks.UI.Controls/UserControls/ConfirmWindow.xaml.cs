using System.Windows;

namespace AppWorks.UI.Controls.UserControls
{
    public partial class ConfirmWindow
    {
        public ConfirmWindow()
        {
            InitializeComponent();
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
