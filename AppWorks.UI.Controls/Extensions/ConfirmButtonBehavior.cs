using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using AppWorks.UI.Controls.CustomControls;

namespace AppWorks.UI.Controls.Extensions
{
    public sealed class ConfirmButtonBehavior : Extension<ButtonBase>
    {
        private static Window GetWindow(DependencyObject element)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            if (parent is Window)
            {
                return parent as Window;
            }
            else if (parent == null)
            {
                return null;
            }
            return GetWindow(parent);
        }

        private static INotifyDataErrorInfo FindValidation(ButtonBase button)
        {
            if (button.DataContext is FrameworkElement)
            {
                FrameworkElement content = button.DataContext as FrameworkElement;
                if (content.DataContext is INotifyDataErrorInfo)
                {
                    return content.DataContext as INotifyDataErrorInfo;
                }
            }
            return null;
        }

        private void OnConfirm(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            ButtonBase button = sender as ButtonBase;
            Window dialogWindow = GetWindow(button);
            if (!Validator.IsValid((UserControl)dialogWindow.Content))
            {
                VSWindow.Alert("Validation", "Invalid Data", dialogWindow);
                return;
            }

            if (ValidateBeforeConfirm(sender)) return;
        
            dialogWindow.DialogResult = true;
            dialogWindow.Close();
        }

        private bool ValidateBeforeConfirm(object sender)
        {
            IConfirmValidate validate = ((sender as FrameworkElement).DataContext as FrameworkElement).DataContext as IConfirmValidate;
            if (validate != null)
            {
                validate.ValidateOnConfirm();
                if ((validate is INotifyDataErrorInfo))
                {
                    bool hasErrors = (validate as INotifyDataErrorInfo).HasErrors;
                    Target.IsEnabled = !hasErrors;
                    return hasErrors;
                }
            }
            return false;
        }

        private void OnValidationErrorsChanges(object sender, DataErrorsChangedEventArgs e)
        {
            INotifyDataErrorInfo validation = sender as INotifyDataErrorInfo;
            Target.IsEnabled = !validation.HasErrors;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            INotifyDataErrorInfo validation = FindValidation(Target);
            if (validation != null)
            {
                Target.IsEnabled = !validation.HasErrors;
                validation.ErrorsChanged += OnValidationErrorsChanges;
            }
            Target.PreviewMouseLeftButtonUp += OnConfirm;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            INotifyDataErrorInfo validation = FindValidation(Target);
            if (validation != null)
            {
                validation.ErrorsChanged -= OnValidationErrorsChanges;
            }
            Target.MouseLeftButtonUp -= OnConfirm;
        }
    }

    public interface IConfirmValidate
    {
        void ValidateOnConfirm();
    }
}
