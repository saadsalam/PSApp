using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Configuration;


namespace AppWorks.UI.Common
{
    public class AutoLogOffHelper 
    {

        /// <summary>
        /// Windows Forms Timer for execute event on exact time interval 
        /// </summary>
        static System.Windows.Forms.Timer _timer = null;

        /// <summary>
        /// Set automatic logout time
        /// </summary>
        public static int LogOffTime
        {
            get 
            {   
                return int.Parse(ConfigurationManager.AppSettings["SessionLogoutTime"]); 
            }
        }

        /// <summary>
        /// Delegate for logout event
        /// </summary>
        public delegate void MakeAutoLogOff();

        /// <summary>
        /// Logout Event handler
        /// </summary>
        static public event MakeAutoLogOff MakeAutoLogOffEvent;

        public AutoLogOffHelper()
        {

        }

        /// <summary>
        /// Start ThreadIdle queue to bind logout event if system is idle
        /// </summary>
        static public void StartAutoLogoffOption()
        {
            System.Windows.Interop.ComponentDispatcher.ThreadIdle += new EventHandler(LogoutTimeHandler);
        }

        /// <summary>
        /// Execute event at logout time interval and deregister idle queue EventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void _timer_Tick(object sender, EventArgs e)
        {
            if (_timer != null)
            {
                if (MakeAutoLogOffEvent != null)
                {
                    MakeAutoLogOffEvent();
                }
            }
        }

        /// <summary>
        /// Start or enable timer if system is idel for logout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void LogoutTimeHandler(object sender, EventArgs e)
        {
            if (_timer == null)
            {
                _timer = new System.Windows.Forms.Timer();
                _timer.Interval = LogOffTime * 60 * 1000;
                _timer.Tick += new EventHandler(_timer_Tick);
                _timer.Enabled = true;
            }
            else if (_timer.Enabled == false)
            {
                _timer.Enabled = true;
            }
        }

        /// <summary>
        /// Reset timer if system some activty
        /// </summary>
        static public void ResetLogoffTimer()
        {
            if (_timer != null)
            {
                _timer.Enabled = false;
                _timer.Enabled = true;
            }
        }

        /// <summary>
        /// Stop timer on logout
        /// </summary>
        static public void StopTimer()
        {
            if (_timer != null)
            {
                System.Windows.Interop.ComponentDispatcher.ThreadIdle -= new EventHandler(LogoutTimeHandler);
                _timer.Enabled = false;
                _timer.Stop();
                _timer = null;
            }
        }

    }
}