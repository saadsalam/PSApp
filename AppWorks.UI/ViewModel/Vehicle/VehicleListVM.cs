using System;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using AppWorks.UI.Common;
using AppWorks.UI.View.UserControls;
using AppWorks.UI.Properties;
using System.Reflection;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class VehicleListVM : Behavior<RadGridView>
    {

        protected override void OnAttached()
        {
            base.OnAttached();
           // ((VehicleLocatorVM)this.AssociatedObject.DataContext).SelectedItems = this.AssociatedObject.SelectedItems;
        }



    }
}
