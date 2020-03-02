using System;
using System.Windows.Markup;

namespace AppWorks.UI.Controls.Extensions
{
    public class ThemeResourceExtension : MarkupExtension
    {
        private object resourceKey;

        [ConstructorArgument("resourceKey")]
        public object ResourceKey
        {
            get { return this.resourceKey; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.resourceKey = value;
            }
        }

        public ThemeResourceExtension() { }

        public ThemeResourceExtension(object resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object value = ThemeManager.CurrentTheme[ResourceKey];

            if (value == null)
                throw new Exception(string.Format("Cannot find resource named '{0}'. Resources names are case sensitive.", ResourceKey.ToString()));

            return value;
        }
    }
}
