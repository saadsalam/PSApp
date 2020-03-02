using AppWorks.UI.Common;
using AppWorks.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telerik.Reporting.Interfaces;
using AppWorks.UI.Common;

namespace AppWorks.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyUpEvent, new KeyEventHandler(RegisterkeyKeyboardEvent), true);
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyUpEvent, new KeyEventHandler(RegisterkeyKeyboardEvent), true);
            EventManager.RegisterClassHandler(typeof(Window), Mouse.MouseEnterEvent, new MouseEventHandler(RegisterMouseEvent), true);
            EventManager.RegisterClassHandler(typeof(Window), Mouse.MouseLeaveEvent, new MouseEventHandler(RegisterMouseEvent), true);
            EventManager.RegisterClassHandler(typeof(Window), Mouse.MouseDownEvent, new MouseButtonEventHandler(RegisterMouseEvent), true);
            EventManager.RegisterClassHandler(typeof(Window), Mouse.MouseUpEvent, new MouseButtonEventHandler(RegisterMouseEvent), true);
            EventManager.RegisterClassHandler(typeof(Window), Mouse.MouseWheelEvent, new MouseWheelEventHandler(RegisterMouseEvent), true);
            Application.Current.Dispatcher.UnhandledException += UnhandledExceptionHandler;
            
           
        }

        private void UnhandledExceptionHandler(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            LogHelper.LogErrorToDb(e.Exception);
            e.Handled = true;
        }

        private void RegisterMouseEvent(object sender, MouseEventArgs e)
        {
            ResetTimer();
        }

        private void RegisterkeyKeyboardEvent(object sender, KeyEventArgs e)
        {
            ResetTimer();
        }

        public void ResetTimer()
        {
            if (Application.Current == null ) return;

            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Title.Equals(WindowsTitles.LoginWindowTitle, StringComparison.OrdinalIgnoreCase)
                || w.Title.Equals(WindowsTitles.LogoutWindowTitle, StringComparison.OrdinalIgnoreCase));

            bool isLoginOrLogout = currentWindow != null;

            if (!isLoginOrLogout)
            {
                bool isSecurityRole = IsLoggedSecurityRole();
                if (!isSecurityRole)
                    AutoLogOffHelper.ResetLogoffTimer();
            }
        }

        private static bool IsLoggedSecurityRole()
        {
            char[] objRolesSplit = new char[] { ',' };
            var objRoles = Application.Current.Properties["LoggedInUserRole"].ToString().Split(objRolesSplit);

            bool isSecurityRole = objRoles.Contains(UserRoles.Security.GetUserRoleToCompare());

            return isSecurityRole;
        }

    }
}
