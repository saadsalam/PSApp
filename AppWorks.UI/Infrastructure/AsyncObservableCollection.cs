using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AppWorks.UI.Infrastructure
{
    /// <summary>
    /// This observable collection is used to bind the collection and change the collection asynchronously from UI thread
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AsyncObservableCollection<T> : ObservableCollection<T>
    {
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var eh = CollectionChanged;
            if (eh != null)
            {
                Dispatcher dispatcher = (from NotifyCollectionChangedEventHandler nh in eh.GetInvocationList()
                                         let dpo = nh.Target as DispatcherObject
                                         where dpo != null
                                         select dpo.Dispatcher).FirstOrDefault();

                if (dispatcher != null && dispatcher.CheckAccess() == false)
                {
                    dispatcher.Invoke(DispatcherPriority.DataBind, (Action)(() => this.OnCollectionChanged(e)));
                }
                else
                {
                    NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                  
                    foreach (NotifyCollectionChangedEventHandler nh in eh.GetInvocationList())
                    {
                        if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Add)
                        {
                            nh.Invoke(this, e);
                        }
                        else { nh.Invoke(this, notifyCollectionChangedEventArgs); }
                    }
                }
            }
        }
    }
}