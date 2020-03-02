using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace AppWorks.UI.ViewModel.Location
{
    public class LocationMultiSelect : Behavior<RadGridView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            //((LocationVM)this.AssociatedObject.DataContext).SelectedItems = this.AssociatedObject.SelectedItems;
        }
    }
}
