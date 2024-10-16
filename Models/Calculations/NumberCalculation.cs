using Marimo.WindowsCalculator.Models.Tokens;

namespace Marimo.WindowsCalculator.Models.Calculations;

/// <summary>
/// 計算の最初にある、演算の対象となるのみの数字を表します。
/// </summary>
/// <remarks>
/// NumberCalculationクラスの新しいインスタンスを初期化します。
/// </remarks>
/// <param name="receiver">計算する対象。</param>
/// <param name="numberToken">計算される数値</param>
public class NumberCalculation(Calculation receiver, NumberToken numberToken) : Calculation(receiver)
{
    /// <summary>
    /// 表示する計算結果を取得します。
    /// </summary>
    public override NumberToken DisplayNumber => NumberToken;

    /// <summary>
    /// NumberCalculationクラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="receiver">計算する対象。</param>
    public NumberCalculation(Calculation receiver) 
        : this(receiver, new NumberToken(0))
    { }

    /// <summary>
    /// 計算する対象を取得、設定します。
    /// </summary>
    public override Calculation Receiver { get; internal set; } = receiver;

    /// <summary>
    /// 計算される数値を取得、設定します。
    /// </summary>
    public NumberToken NumberToken { get; set; } = numberToken;

    /// <summary>
    /// 計算した結果を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算した結果です。次の計算があった場合は、計算対象となります。
    /// </remarks>
    public override decimal? Result => NumberToken.Number;

    /// <summary>
    /// この計算がActiveCalculatorの場合に表示される式を取得します。
    /// </summary>
    public override string Expression => "";
}
