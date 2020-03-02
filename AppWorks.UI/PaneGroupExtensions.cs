using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using System.Windows.Controls;
using System.Windows;


namespace AppWorks.UI
{
    public class PaneGroupExtensions
    {
        private RadPaneGroup Group { get; set; }

        private static PaneGroupExtensions GetPaneGroupExtension(DependencyObject obj)
        {
            return (PaneGroupExtensions)obj.GetValue(PaneGroupExtensionProperty);
        }

        private static void SetPaneGroupExtension(DependencyObject obj, PaneGroupExtensions value)
        {
            obj.SetValue(PaneGroupExtensionProperty, value);
        }

        private static readonly DependencyProperty PaneGroupExtensionProperty =
            DependencyProperty.RegisterAttached("PaneGroupExtension", typeof(PaneGroupExtensions), typeof(PaneGroupExtensions), null);

        public static IEnumerable<RadPane> GetItemsSource(DependencyObject obj)
        {
            return (IEnumerable<RadPane>)obj.GetValue(ItemsSourceProperty);
        }

        public static void SetItemsSource(DependencyObject obj, IEnumerable<RadPane> value)
        {
            obj.SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.RegisterAttached("ItemsSource", typeof(IEnumerable<RadPane>), typeof(PaneGroupExtensions), new PropertyMetadata(null, OnItemsSourceChanged));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var group = d as RadPaneGroup;
            var oldValue = e.OldValue as IEnumerable<RadPane>;
            var newValue = e.NewValue as IEnumerable<RadPane>;
            var oldValueObservableCollection = e.OldValue as INotifyCollectionChanged;
            var newValueObservableCollection = e.NewValue as INotifyCollectionChanged;

            if (group != null)
            {
                var extension = GetPaneGroupExtension(group);
                if (extension == null)
                {
                    extension = new PaneGroupExtensions { Group = group };
                    SetPaneGroupExtension(group, extension);
                }

                if (oldValue != null)
                {
                    foreach (var pane in oldValue)
                    {
                        pane.RemoveFromParent();
                    }

                    if (oldValueObservableCollection != null)
                    {
                        oldValueObservableCollection.CollectionChanged -= extension.OnItemsSourceCollectionChanged;
                    }
                }

                if (newValue != null)
                {
                    foreach (var pane in newValue)
                    {
                        group.Items.Add(pane);
                    }

                    if (newValueObservableCollection != null)
                    {
                        newValueObservableCollection.CollectionChanged += extension.OnItemsSourceCollectionChanged;
                    }
                }
            }
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // TODO: Make better implementation of this.
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.Group.Items.Clear();
                foreach (var pane in GetItemsSource(this.Group))
                {
                    this.Group.Items.Add(pane);
                }
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (var pane in e.OldItems.OfType<RadPane>())
                    {
                        pane.RemoveFromParent();
                    }
                }

                if (e.NewItems != null)
                {
                    int i = e.NewStartingIndex;
                    foreach (var pane in e.NewItems.OfType<RadPane>())
                    {
                        try
                        {
                            this.Group.Items.Add(pane);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                }
            }
        }
    }
}
