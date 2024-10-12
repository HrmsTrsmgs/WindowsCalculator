

namespace Marimo.WindowsCalculator.MauiBlazor;


/// <summary>
/// アプリケーションを表します。
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Appクラスの新しいインスタンスを初期化します。
    /// </summary>
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage()
        {
            MinimumWidthRequest = 320,
        };

    }

    /// <summary>
    /// Windowが作られる時の初期設定を行います。
    /// </summary>
    /// <param name="activationState">その時の状態。</param>
    /// <returns>作られたWindow</returns>
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        window.MinimumWidth = 332;
        window.MinimumHeight = 426;
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
