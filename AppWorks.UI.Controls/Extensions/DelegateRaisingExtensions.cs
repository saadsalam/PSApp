namespace AppWorks.UI.Controls.Extensions
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Threading;

    public static class DelegateRaisingExtensions
    {
        /// <summary>
        /// Raise EventHandler with empty Event Args
        /// </summary>
        public static void RaiseEventHandler(this EventHandler eventDelegate, object sender)
        {
            var temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
            if (temp != null) temp(sender, EventArgs.Empty);
        }

        public static void RaiseEventHandler<TEventArg>(this TEventArg e, object sender, ref EventHandler<TEventArg> eventDelegate) 
            where TEventArg : EventArgs        
        {
            var temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
            if (temp != null) temp(sender, e);
        }

        public static void RaisePropertyChangedEventHandler(this PropertyChangedEventArgs e, object sender, ref PropertyChangedEventHandler eventDelegate)
        {
            var temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
            if (temp != null) temp(sender, e);
        }

        internal static void RaiseNotifyCollectionChangedEventHandler(this NotifyCollectionChangedEventArgs e, object sender, ref NotifyCollectionChangedEventHandler eventDelegate)
        {
            var temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
            if (temp != null) temp(sender, e);
        }

        //internal static void RaiseNotifyCollectionChangingEventHandler(this NotifyCollectionChangingEventArgs e, object sender, ref NotifyCollectionChangingEventHandler eventDelegate)
        //{
        //    var temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
        //    if (temp != null) temp(sender, e);
        //}
    }
}
