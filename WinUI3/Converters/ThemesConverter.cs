using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using ApplicationTheme = Marimo.WindowsCalculator.Models.ApplicationTheme;

namespace Marimo.WindowsCalculator.WinUI3.Converters
{
    public class ThemesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ApplicationTheme appTheme)
            {
                switch (appTheme)
                {
                    case ApplicationTheme.Light:
                        return ElementTheme.Light;
                    case ApplicationTheme.Dark:
                        return ElementTheme.Dark;
                    case ApplicationTheme.System:
                        return ElementTheme.Default;
                    default:
                        return ElementTheme.Default;
                }
            }
            return ElementTheme.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is ElementTheme elementTheme)
            {
                switch (elementTheme)
                {
                    case ElementTheme.Light:
                        return ApplicationTheme.Light;
                    case ElementTheme.Dark:
                        return ApplicationTheme.Dark;
                    case ElementTheme.Default:
                        return ApplicationTheme.System;
                    default:
                        return ApplicationTheme.System;
                }
            }
            return ApplicationTheme.System;
        }
    }
}
