using AppWorks.UI.ViewModel;

namespace AppWorks.UI.Model
{
    public class SelectableModel<TModel> : ViewModelBase
    {
        private TModel instance;
        public TModel Instance
        {
            get { return this.instance; }
            set
            {
                if (object.ReferenceEquals(this.instance, value)) return;
                this.instance = value;
                NotifyPropertyChanged("Instance");
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (this.isSelected == value) return;
                this.isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }
    }
}
