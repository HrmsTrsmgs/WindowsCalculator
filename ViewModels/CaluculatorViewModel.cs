using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Marimo.MauiBlazor.Models;
using System.Windows.Input;

namespace Marimo.MauiBlazor.ViewModels;

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

    /// <summary>
    /// 計算機に表示されている数を取得します。
    /// </summary>
    public int Number => model.DisplaiedNumber;

    public CalculatorViewModel()
    {
        PushKeybord = new RelayCommand<char>(c => Input(c));
        model.PropertyChanged += 
            (sender, e) =>
            {
                if (e.PropertyName == nameof(model.DisplaiedNumber))
                {
                    OnPropertyChanged(nameof(Number));
                }
            };
    }

    /// <summary>
    /// 文字を一文字入力します。
    /// </summary>
    /// <param name="input">一文字。</param>
    void Input(char input)
    {
        parser.Input(input);
        if (parser.ActiveToken != null)
        {
            model.Input(parser.ActiveToken);
        }
    }
}
