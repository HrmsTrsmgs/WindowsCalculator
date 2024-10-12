using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Marimo.WindowsCalculator.Models;
using System.Windows.Input;

namespace Marimo.WindowsCalculator.ViewModels;

/// <summary>
/// 計算機のViewModelです。
/// </summary>
public class CalculatorViewModel : ObservableObject
{
    /// <summary>
    /// キーボードを押した事を表すコマンドを取得します。
    /// </summary>
    public ICommand PushKeybord { get; init; }


    /// <summary>
    /// 押された文字のパースを行います。
    /// </summary>
    IncrementalParser parser { get; } = new();

    /// <summary>
    /// 計算機を表します。
    /// </summary>
    Calculator model { get; } = new();

    public ICommand ClearHistoryCommand { get; }

    ///// <summary>
    ///// 計算機に表示されている数を取得します。
    ///// </summary>
    public string DisplaiedNumber => model.DisplaiedNumber;

    /// <summary>
    /// 履歴を取得します。
    /// </summary>
    public IEnumerable<CalculationHistoryItem> History => model.CalculationHistory;

    /// <summary>
    /// 計算式を取得します。
    /// </summary>
    public object Expression => model.ActiveCaluculation.CurrentExpression;

    /// <summary>
    /// CalculationViewModelクラスの新しいインスタンスを初期化します。
    /// </summary>
    public CalculatorViewModel()
    {
        PushKeybord = new RelayCommand<InputAction>(c => Input(c));

        ClearHistoryCommand = new RelayCommand(() => model.ClearCalculationHistory());
        model.PropertyChanged += 
            (sender, e) =>
            {
                if (e.PropertyName == nameof(model.DisplaiedNumber))
                {
                    OnPropertyChanged(nameof(DisplaiedNumber));
                }
            };


    }

    /// <summary>
    /// 文字を一文字入力します。
    /// </summary>
    /// <param name="input">一文字。</param>
    void Input(InputAction input)
    {
        parser.Input(input);
        if (parser.ActiveToken != null)
        {
            model.Input(parser.ActiveToken);
        }
    }
}
