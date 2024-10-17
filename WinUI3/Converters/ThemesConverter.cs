using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using ApplicationTheme = Marimo.WindowsCalculator.Models.ApplicationTheme;

namespace Marimo.WindowsCalculator.WinUI3.Converters
{
    public partial class ThemesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ApplicationTheme appTheme)
            {
                return appTheme switch
                {
                    ApplicationTheme.Light => ElementTheme.Light,
                    ApplicationTheme.Dark => ElementTheme.Dark,
                    ApplicationTheme.System => ElementTheme.Default,
                    _ => (object)ElementTheme.Default,
                };
            }
            return ElementTheme.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is ElementTheme elementTheme)
            {
                return elementTheme switch
                {
                    ElementTheme.Light => ApplicationTheme.Light,
                    ElementTheme.Dark => ApplicationTheme.Dark,
                    ElementTheme.Default => ApplicationTheme.System,
                    _ => (object)ApplicationTheme.System,
                };
            }
            return ApplicationTheme.System;
        }
    }
}
