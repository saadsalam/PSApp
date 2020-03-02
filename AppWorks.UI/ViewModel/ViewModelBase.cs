using AppWorks.UI.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AppWorks.UI.ViewModel
{
    public class ViewModelBase : WcfSingleInstance, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual IEnumerable<IChangeTracking> GetChildViewModels()
        {
            return Enumerable.Empty<IChangeTracking>();
        }
    }
}
