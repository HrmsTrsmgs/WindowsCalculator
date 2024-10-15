using System.Runtime.InteropServices;
using Marimo.WindowsCalculator.Models.Tokens;

namespace Marimo.WindowsCalculator.Models.Calculations;

/// <summary>
/// 計算、もしくは最初に入力された数値を表します。
/// </summary>
/// <param name="receiver">計算対象</param>
/// <param name="operand">計算される数値。</param>
/// <param name="isDisplayResult">結果が式の表示対象になるか。</param>
public abstract class OperationCalculation(Calculation receiver, NumberToken? operand = null, bool isDisplayResult = false) : Calculation(receiver)
{

    /// <summary>
    /// エラーが出たときに表示されるエラーを取得、設定します。
    /// </summary>
    public string? DisplayError { get; set; } = null;
    /// <summary>
    /// 結果が式の表示対象になるかを取得、設定します。
    /// </summary>
    public bool IsDisplayResult
    {
        get => isDisplayResult;
        set
        {
            SetProperty(ref isDisplayResult, value);
            OnPropertyChanged(nameof(Result));
        }
    }

    /// <summary>
    /// 計算の内容を表す演算子を取得します。
    /// </summary>
    public abstract InputAction? OperatorAction { get; }

    /// <summary>
    /// 演算子で計算される比演算子を指します。
    /// </summary>
    /// <remarks>
    /// 演算子で計算される比演算子を指します。
    /// Receiverに対してこの数値で演算を行います。
    /// 入力されるまではnullとなります。
    /// </remarks>
    public NumberToken? Operand
    {
        get => operand;
        set
        {
            IsDisplayResult = false;
            SetProperty(ref operand, value);
        }
    }

    /// <summary>
    /// 計算した結果を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算した結果です。次の計算があった場合は、計算対象となります。
    /// 基本的にはnullになりません。
    /// 演算子が入力され、計算対象が入力されていない場合などにnullとなります。
    /// </remarks>
    public override decimal? Result
        => !IsDisplayResult
            ? null
            : GetResult();

    /// <summary>
    /// 結果の計算を行い、取得します。
    /// </summary>
    /// <returns>計算結果。</returns>
    protected abstract decimal? GetResult();

    /// <summary>
    /// この計算がActiveCalculatorの場合に表示される式を取得します。
    /// </summary>
    public override string Expression => $"{Receiver?.Result}　{OperatorString}";

    /// <summary>
    /// 演算子を表す文字列を取得します。
    /// </summary>
    protected abstract string OperatorString { get; }

    /// <summary>
    /// OperationCalculationクラスを継承する新しいインスタンスを生成します。
    /// </summary>
    /// <param name="receiver">計算対象</param>
    /// <param name="operatorAction">入力された演算子を表すトークン。</param>
    /// <param name="operand">計算される数値。</param>
    /// <param name="isDisplayResult">結果が式の表示対象になるか。</param>
    public static OperationCalculation Create(Calculation receiver, InputAction? operatorAction = null, NumberToken? operand = null, bool isDisplayResult = false)
         => operatorAction switch
         {
             InputAction.Add => new AddCalculation(receiver, operand, isDisplayResult),
             InputAction.Substract => new SubstractCalculation(receiver, operand, isDisplayResult),
             InputAction.Multiply => new MultiplyCalculation(receiver, operand, isDisplayResult),
             InputAction.Divide => new DivideCalculation(receiver, operand, isDisplayResult),
             _ => throw new InvalidOperationException()
         };
}


