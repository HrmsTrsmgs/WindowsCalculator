using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Windows.Input;

namespace Marimo.WindowsCalculator.WinUI3.Controls;

/// <summary>
/// 計算機で使用されるボタンを表します。
/// </summary>
/// <remarks>
/// このコントロールは、コマンド、テキスト、背景色を指定することができます。
/// </remarks>
public sealed partial class CalculatorButton : UserControl
{
    /// <summary>
    /// <see cref="Command"/> 依存関係プロパティの定義。
    /// </summary>
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register("Command", typeof(ICommand), typeof(CalculatorButton),
        new PropertyMetadata(null));

    /// <summary>
    /// ボタンが実行するコマンドを取得または設定します。
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// <see cref="Text"/> 依存関係プロパティの定義。
    /// </summary>
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(CalculatorButton),
        new PropertyMetadata(""));

    /// <summary>
    /// ボタンに表示するテキストを取得または設定します。
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// <see cref="BackGround"/> 依存関係プロパティの定義。
    /// </summary>
    public static readonly DependencyProperty BackGroundProperty =
        DependencyProperty.Register("BackGround", typeof(Brush), typeof(CalculatorButton),
        new PropertyMetadata(new SolidColorBrush(Colors.White)));

    /// <summary>
    /// ボタンの背景色を取得または設定します。
    /// </summary>
    public Brush BackGround
    {
        get => (Brush)GetValue(BackGroundProperty);
        set => SetValue(BackGroundProperty, value);
    }

    /// <summary>
    /// <see cref="CalculatorButton"/> CalculatorButtonクラスの新しいインスタンスを初期化します。
    /// </summary>
    public CalculatorButton()
    {
        this.InitializeComponent();
    }
}
