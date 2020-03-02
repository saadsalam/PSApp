namespace AppWorks.UI.Controls
{
    using System;
    using System.Windows;
    using Themes.Dark;
    using Themes.Light;

    public enum Theme
    {
        None,
        Dark,
        Light
    }

    public static class ThemeManager
    {
        private static ResourceDictionary themeResources;

        private static readonly ResourceDictionary currentTheme = new ResourceDictionary();
        public static ResourceDictionary CurrentTheme
        {
            get
            {
                return currentTheme;
            }
        }

        static ThemeManager()
        {
#if DEBUG
            SwitchTo(Theme.Dark);
#endif
        }

        public static void SwitchTo(Theme theme)
        {
            currentTheme.MergedDictionaries.Remove(themeResources);
            switch (theme)
            {
                case Theme.Dark:
                    themeResources = new DarkTheme();
                    break;
                case Theme.Light:
                    themeResources = new LightTheme();
                    break;
                default:
                    throw new NotImplementedException(string.Format("{0} theme is not implemented", theme));
            }
            currentTheme.MergedDictionaries.Add(themeResources);
        }
    }
}
