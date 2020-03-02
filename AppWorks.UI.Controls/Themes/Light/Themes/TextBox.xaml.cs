using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace AppWorks.UI.Controls.Themes.Light.Themes
{
    public partial class TextBox : ResourceDictionary
    {

        public TextBox()
        {
            InitializeComponent();
        }

        private void TextBoxUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BindingExpression expression = null;
                System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
                if (textBox == null || textBox.AcceptsReturn)
                {
                    return;
                }

                expression = textBox.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
                if (CanUpdateBindingExpression(expression))
                {
                    expression.UpdateSource();
                    if (expression.ParentBinding.Mode != BindingMode.OneWayToSource)
                        expression.UpdateTarget();
                }
            }
        }

        private bool CanUpdateBindingExpression(BindingExpression expression)
        {
            return expression != null
                && expression.ParentBinding != null
                && CanUpdateSource(expression.ParentBinding.Mode);
        }

        private bool CanUpdateSource(BindingMode mode)
        {
            return mode == BindingMode.Default
                || mode == BindingMode.TwoWay
                || mode == BindingMode.OneWayToSource;
        }

    }
}
