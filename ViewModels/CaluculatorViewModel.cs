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
    /// キーボードを入力した事を表すコマンドを取得します。
    /// </summary>
    public ICommand InputKeybord { get; init; }


    /// <summary>
    /// 押された文字のパースを行います。
    /// </summary>
    IncrementalParser parser { get; } = new();

    /// <summary>
    /// 計算機を表します。
    /// </summary>
    Calculator model { get; } = new();

    /// <summary>
    /// 1を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputOneCommand { get; }

    /// <summary>
    /// 2を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputTwoCommand { get; }

    /// <summary>
    /// 3を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputThreeCommand { get; }

    /// <summary>
    /// 4を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputFourCommand { get; }

    /// <summary>
    /// 5を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputFiveCommand { get; }

    /// <summary>
    /// 6を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputSixCommand { get; }

    /// <summary>
    /// 7を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputSevenCommand { get; }

    /// <summary>
    /// 8を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputEightCommand { get; }

    /// <summary>
    /// 9を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputNineCommand { get; }

    /// <summary>
    /// 0を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputZeroCommand { get; }

    /// <summary>
    /// 小数点を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputDotCommand { get; }

    /// <summary>
    /// +を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputPlusCommand { get; }

    /// <summary>
    /// -を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputMinusCommand { get; }

    /// <summary>
    /// ×を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputMultiplyCommand { get; }

    /// <summary>
    /// ÷を入力するコマンドを取得します。
    /// </summary>
    public ICommand InputDivideCommand { get; }

    /// <summary>
    /// ＝ボタンを入力するコマンドを取得します。
    /// </summary>
    public ICommand InputEqualCommand { get; }

    /// <summary>
    /// Undoを入力するコマンドを取得します。
    /// </summary>
    public ICommand InputUndoCommand { get; }

    /// <summary>
    /// Redoを入力するコマンドを取得します。
    /// </summary>
    public ICommand InputRedoCommand { get; }

    /// <summary>
    /// Cボタンを入力するコマンドを取得します。
    /// </summary>
    public ICommand InputCCommand { get; }

    /// <summary>
    /// CEボタンを入力するコマンドを取得します。
    /// </summary>
    public ICommand InputCECommand { get; }

    /// <summary>
    /// Backspaceを入力するコマンドを取得します。
    /// </summary>
    public ICommand InputBackspaceCommand { get; }

    /// <summary>
    /// 履歴をクリアするコマンドを取得します。
    /// </summary>
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
    public string Expression => model.ActiveCaluculation.CurrentExpression;

    /// <summary>
    /// 設定を取得します。
    /// </summary>
    public PropertySettings Settings => model.Settings;

    /// <summary>
    /// CalculationViewModelクラスの新しいインスタンスを初期化します。
    /// </summary>
    public CalculatorViewModel()
    {
        InputKeybord = new RelayCommand<InputAction>(c => Input(c));

        ClearHistoryCommand = new RelayCommand(
            () => model.ClearCalculationHistory(),
            () => model.CalculationHistory.Any());
        InputOneCommand = new RelayCommand(() => Input(InputAction.One));
        InputTwoCommand = new RelayCommand(() => Input(InputAction.Two));
        InputThreeCommand = new RelayCommand(() => Input(InputAction.Three));
        InputFourCommand = new RelayCommand(() => Input(InputAction.Four));
        InputFiveCommand = new RelayCommand(() => Input(InputAction.Five));
        InputSixCommand = new RelayCommand(() => Input(InputAction.Six));
        InputSevenCommand = new RelayCommand(() => Input(InputAction.Seven));
        InputEightCommand = new RelayCommand(() => Input(InputAction.Eight));
        InputNineCommand = new RelayCommand(() => Input(InputAction.Nine));
        InputDotCommand = new RelayCommand(() => Input(InputAction.Dot));
        InputZeroCommand = new RelayCommand(() => Input(InputAction.Zero));
        InputPlusCommand = new RelayCommand(() => Input(InputAction.Plus));
        InputMinusCommand = new RelayCommand(() => Input(InputAction.Minus));
        InputMultiplyCommand = new RelayCommand(() => Input(InputAction.Multiply));
        InputDivideCommand = new RelayCommand(() => Input(InputAction.Divide));
        InputEqualCommand = new RelayCommand(() => Input(InputAction.Equal));
        InputUndoCommand = new RelayCommand(() => Input(InputAction.Undo));
        InputRedoCommand = new RelayCommand(() => Input(InputAction.Redo));
        InputCCommand = new RelayCommand(() => Input(InputAction.C));
        InputCECommand = new RelayCommand(() => Input(InputAction.CE));
        InputBackspaceCommand = new RelayCommand(() => Input(InputAction.Backspace));
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
