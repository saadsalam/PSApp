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

namespace AppWorks.UI
{
    /// <summary>
    /// Interaction logic for AutoLogOffPopup.xaml
    /// </summary>
    public partial class AutoLogOffPopup : Window
    {
        public AutoLogOffPopup(string Message)
        {
            InitializeComponent();
            lblMessage.Text = Message;
        }
    }
}
