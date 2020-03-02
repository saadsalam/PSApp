using System.Windows;

namespace AppWorks.UI.Controls.Extensions
{
    public class Extension
    {
        protected DependencyObject extensionTarget;

        public static Extension GetAttachedExtensions(DependencyObject obj)
        {
            return (Extension)obj.GetValue(AttachedExtensionsProperty);
        }
        public static void SetAttachedExtensions(DependencyObject obj, Extension value)
        {
            obj.SetValue(AttachedExtensionsProperty, value);
        }
        public static readonly DependencyProperty AttachedExtensionsProperty =
            DependencyProperty.RegisterAttached("AttachedExtensions",
            typeof(Extension),
            typeof(Extension),
            new PropertyMetadata(null, OnExtensionChanged));

        static Extension()
        {
        }

        private static void OnExtensionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                Extension extension = e.OldValue as Extension;
                extension.Detach();
            }
            if (e.NewValue != null)
            {
                Extension extension = e.NewValue as Extension;
                extension.Attach(d);
            }
        }

        private void Attach(DependencyObject target)
        {
            this.extensionTarget = target;
            OnAttached();
        }

        protected void Detach()
        {
            OnDetaching();
            this.extensionTarget = null;
        }

        protected virtual void OnAttached()
        {
        }

        protected virtual void OnDetaching()
        {
        }
    }

    public class Extension<TTarget> : Extension where TTarget : FrameworkElement
    {
        public TTarget Target { get { return (TTarget)base.extensionTarget; } }

        public Extension()
        {
        }

        protected override void OnAttached()
        {
            if (Target.IsLoaded)
            {
                Target.Unloaded += OnTargetUnloaded;
            }
            else
            {
                Target.Loaded += OnTargetLoaded;
            }
        }

        private void OnTargetUnloaded(object sender, RoutedEventArgs e)
        {
            Target.Unloaded -= OnTargetUnloaded;
            Detach();
        }

        private void OnTargetLoaded(object sender, RoutedEventArgs e)
        {
            Target.Loaded -= OnTargetLoaded;
            Target.Unloaded += OnTargetUnloaded;
        }
    }
}
