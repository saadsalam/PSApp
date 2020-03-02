namespace AppWorks.UI.Controls.Extensions
{
    using System.ComponentModel;
    using System.Windows;

    public static class DesignTimeExtensions
    {
        private static readonly bool isDesignTime = DesignerProperties.GetIsInDesignMode(new DependencyObject());
        public static bool IsDesignTime { get { return isDesignTime; } }
    }
}
