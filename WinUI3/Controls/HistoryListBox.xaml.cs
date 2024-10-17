using Marimo.WindowsCalculator.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Marimo.WindowsCalculator.WinUI3.Controls;


/// <summary>
/// 履歴を表示します。
/// </summary>
public sealed partial class HistoryListBox : UserControl
{
    /// <summary>
    /// <see cref="ItemsSource"/> 依存関係プロパティの定義。
    /// </summary>
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<CalculationHistoryItem>), typeof(HistoryListBox),
        new PropertyMetadata(new ObservableCollection<CalculationHistoryItem>()));

    /// <summary>
    /// ボタンの背景色を取得または設定します。
    /// </summary>
    public IEnumerable<CalculationHistoryItem> ItemsSource
    {
        get => (IEnumerable<CalculationHistoryItem>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public HistoryListBox()
    {
        this.InitializeComponent();
    }
}