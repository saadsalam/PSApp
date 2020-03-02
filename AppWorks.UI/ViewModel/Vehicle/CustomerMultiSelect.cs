using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class CustomerMultiSelect : Behavior<RadGridView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            ((CustomerVM)this.AssociatedObject.DataContext).SelectedItems = this.AssociatedObject.SelectedItems;
        }
    }
}
