using Marimo.WindowsCalculator.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
namespace Marimo.WindowsCalculator.WinUI3.Controls;


/// <summary>
/// 履歴を表示します。
/// </summary>
public sealed partial class HistoryListBox : UserControl, INotifyPropertyChanged
{
    /// <summary>
    /// <see cref="ItemsSource"/> 依存関係プロパティの定義。
    /// </summary>
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<CalculationHistoryItem>), typeof(HistoryListBox),
        new PropertyMetadata(new ObservableCollection<CalculationHistoryItem>()));

    /// <summary>
    /// 項目の一覧を取得または設定します。
    /// </summary>
    public ReadOnlyObservableCollection<CalculationHistoryItem> ItemsSource
    {
        get => (ReadOnlyObservableCollection<CalculationHistoryItem>)GetValue(ItemsSourceProperty);
        set
        {
            SetValue(ItemsSourceProperty, value);
            if (ItemsSource is INotifyCollectionChanged source)
            {
                source.CollectionChanged += OnItemsSourceChanged;
            }
        }
    } 

    /// <summary>
    /// ItemsSourceが空かどうかを取得します。
    /// </summary>
    public bool IsItemsSourceEmpty => !ItemsSource.Any();

    /// <summary>
    /// プロパティが変更されると起動します。
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// ItemsSourceが変更されると呼び出されます。
    /// </summary>
    /// <param name="sender">イベントが起きたオブジェクト。</param>
    /// <param name="e">イベント情報。</param>
    private void OnItemsSourceChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsItemsSourceEmpty)));
    }

    /// <summary>
    /// HistoryListBoxクラスの新しいインスタンスを初期化します。
    /// </summary>
    public HistoryListBox()
    {
        InitializeComponent();
    }
}