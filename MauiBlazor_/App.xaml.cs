
namespace Marimo.WindowsCalculator.MauiBlazor
{
    
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage()
            {
                MinimumWidthRequest = 100
            };
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            window.MinimumWidth = 332;
            window.MinimumHeight = 504;
            window.Width = Preferences.Get("WindowWidth", window.MinimumWidth);
            window.Height = Preferences.Get("WindowHeight", window.MinimumHeight);
            window.Destroying += (s, e) =>
            {
                Preferences.Set("WindowWidth", window.Width);
                Preferences.Set("WindowHeight", window.Height);
            };

            return window;
        }
    }
}
