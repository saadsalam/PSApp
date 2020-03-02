using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using System.Windows.Interactivity;

namespace AppWorks.UI.ViewModel.AdminUser
{
    public class FindUserMultiSelect : Behavior<RadGridView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
           // ((FindUserViewModel)this.AssociatedObject.DataContext).SelectedItems = this.AssociatedObject.SelectedItems;
        }
    }
}
