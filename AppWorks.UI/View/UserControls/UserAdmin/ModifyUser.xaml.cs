using System.Windows.Controls;
using AppWorks.UI.ViewModel;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System;
using System.Windows;

namespace AppWorks.UI.View.UserControls.UserAdmin
{
    /// <summary>
    /// Interaction logic for AddAdminUser.xaml
    /// </summary>
    public partial class ModifyUser : UserControl
    {
        public ModifyUser()
        {
            InitializeComponent();
            txtUserCode.Focus();
            this.DataContext=new AdminUserViewModel();
        }


        private void txtUserCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Tab)
            {
                txtFirstName.Focus();
            }
        }

        private void txtFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtLastName.Focus();
            }
        }

        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtPhone.Focus();
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
            if(e.Key==Key.Tab)
            {
                txtExtention.Focus();
            }
        }

        private void txtExtention_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab)
            {
                txtCellPhone.Focus();
            }
        }

        private void txtCellPhone_KeyDown(object sender,KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }

            if (e.Key == Key.Tab)
            {
                txtFaxNumber.Focus();
            }
        }

        private void txtFaxNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
           
            if (e.Key == Key.Tab)
            {
                txtEmail.Focus();
            }
        }

        private void txtEmail_keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtPin.Focus();
            }
        }

        private void txtPin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtXoffset.Focus();
            }
        }

        private void txtXoffset_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtYOffset.Focus();
            }
        }

        private void txtYOffset_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtEmployee.Focus();
            }
        }

        private void txtEmployee_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                cmbRecordStatus.Focus();
            }
        }

        private void cmbRecordStatus_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtPortPassId.Focus();
            }
        }

        private void txtPortPassId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                btnSave.Focus();
            }
        }


        private void txtXoffset_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void txtYOffset_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

    }
}
