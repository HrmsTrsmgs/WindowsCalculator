using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Windows.Input;
namespace Marimo.WindowsCalculator.WinUI3.Controls;

/// <summary>
/// 計算機に使われているボタンを表します。
/// </summary>
public sealed partial class CalculatorButton : UserControl
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register("Command", typeof(ICommand), typeof(CalculatorButton),
        new PropertyMetadata(null));

    public ICommand Comand
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(CalculatorButton),
        new PropertyMetadata(""));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty BackGroundProperty =
        DependencyProperty.Register("BackGround", typeof(Brush), typeof(CalculatorButton),
        new PropertyMetadata(Colors.White));

    public Brush BackGround
    {
        get => (Brush)GetValue(BackGroundProperty);
        set => SetValue(BackGroundProperty, value);
    }
    public CalculatorButton()
    {
        this.InitializeComponent();
    }
}
