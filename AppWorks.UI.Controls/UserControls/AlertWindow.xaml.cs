using System.Windows;

namespace AppWorks.UI.Controls.UserControls
{
    public partial class AlertWindow
    {
        public AlertWindow()
        {
            InitializeComponent();
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
