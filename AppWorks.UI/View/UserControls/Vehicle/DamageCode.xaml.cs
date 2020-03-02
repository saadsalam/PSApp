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
using Props = AppWorks.UI.Properties;
using AppWorks.UI.Common;
using System.Globalization;
using System.Reflection;

namespace AppWorks.UI.View.UserControls.Vehicle
{
    /// <summary>
    /// Interaction logic for DamageCode.xaml
    /// </summary>
    public partial class DamageCode : Window
    {
        public DamageCode(string VinNo, int VehicleId)
        {
            InitializeComponent();
            this.DataContext = new DamageCodeVM(VinNo, VehicleId);
        }

        /// <summary>
        /// This KeyDown event auto insert currunt date when press enter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatededBy></CreatededBy>
        /// <CreatedDate>06 July 2016</CreatedDate>
        //private void txtInspectionDate_KeyDown(object sender, KeyEventArgs e)
        //{
        //    CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgStart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        if (e.Key == Key.Enter || e.Key==Key.Tab)
        //        {
        //            string value = txtInspectionDate.Text;
        //            int index = txtInspectionDate.CaretIndex;
        //            if (!string.IsNullOrEmpty(value) && index > 0)
        //            {
        //                string GetDate = CommonSettings.GetDateValue(value, index);
        //                txtInspectionDate.Text = Convert.ToDateTime(GetDate).ToString("MM/dd/yyyy");
        //            }
                   
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        bool displayErrorOnUI = false;
        //        CommonSettings.logger.LogError(this.GetType(), ex);
        //        if (displayErrorOnUI)
        //        { throw; }
        //    }
        //    finally
        //    {
        //        CommonSettings.logger.LogInfo(typeof(string), string.Format(CultureInfo.InvariantCulture, Props.Resources.loggerMsgEnd, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    }


        //}

        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageCode1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageCode2_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageCode3_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageCode4_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageCode5_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageCode6_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageCode7_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageCode8_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageCode9_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// This key down event for input only number validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDamageCode10_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9))
            {
                e.Handled = true;
            }
        }

        private void MoveToNext(TextBox textbox)
        {
            if (textbox.Text.Length == 5)
                MoveCursor(textbox);
        }

        private void MoveCursor(TextBox textbox)
        {
            textbox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void txtDamageCode1_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                MoveCursor(textBox);
                return;
            }
            MoveToNext(textBox);
        }

        private void txtDamageCod2_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                MoveCursor(textBox);
                return;
            }
            MoveToNext(textBox);
        }

        private void txtDamageCod3_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                MoveCursor(textBox);
                return;
            }
            MoveToNext(textBox);
        }

        private void txtDamageCod4_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                MoveCursor(textBox);
                return;
            }
            MoveToNext(textBox);
        }

        private void txtDamageCod5_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                MoveCursor(textBox);
                return;
            }
            MoveToNext(textBox);
        }

        private void txtDamageCod6_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                MoveCursor(textBox);
                return;
            }
            MoveToNext(textBox);
        }

        private void txtDamageCod7_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                MoveCursor(textBox);
                return;
            }
            MoveToNext(textBox);
        }

        private void txtDamageCod8_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                MoveCursor(textBox);
                return;
            }
            MoveToNext(textBox);
        }

        private void txtDamageCod9_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                MoveCursor(textBox);
                return;
            }
            MoveToNext(textBox);
        }

        private void txtDamageCod10_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                MoveCursor(textBox);
                return;
            }
            MoveToNext(textBox);
        }


    }


}
