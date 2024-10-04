using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Marimo.MauiBlazor.Models;
using System.Windows.Input;

namespace Marimo.MauiBlazor.ViewModels;

public class CalculatorViewModel : ObservableObject
{
    public ICommand PushKeybord { get; init; }


    IncrementalParser parser { get; } = new();
    Calculator model { get; } = new();

    public CalculatorViewModel()
    {
        PushKeybord = new RelayCommand<char>(c => Input(c));
        model.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Number));
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

    public int Number => model.Result;
}
