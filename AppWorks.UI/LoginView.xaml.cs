using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using AppWorks.UI.ViewModel;
using AppWorks.UI;

namespace AppWorks.UI
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            txtUserName.Focus();
            this.DataContext = new LoginViewModel();  
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin.Focus();
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            var passwrodBox = sender as PasswordBox;
            if (string.IsNullOrEmpty(passwrodBox.Password))
            {
                PasswordWaterMark.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordWaterMark.Visibility = Visibility.Collapsed;
            }
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordWaterMark.Visibility = Visibility.Collapsed;
        }

        private void ContentControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswordWaterMark.Visibility = Visibility.Collapsed;
            txtPassword.Focus();
        }
    }

}
