namespace Marimo.WindowsCalculator.Models.Calculations;

/// <summary>
/// 計算の最初にある、演算の対象となるのみの数字を表します。
/// </summary>
/// <param name="receiver"></param>
public class NumberCalculation(Calculation? receiver, NumberToken? numberToken = null) : Calculation(receiver)
{
    /// <summary>
    /// 計算する対象を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算する対象を取得します。
    /// 初期状態の計算しなくてもある数字を表しているので、なので必ずnullです。
    /// </remarks>
    public override Calculation? Receiver => receiver;

    /// <summary>
    /// 計算の最初の数値を取得、設定します。
    /// </summary>
    public NumberToken NumberToken { get; set; } = numberToken ?? new NumberToken(0);

    /// <summary>
    /// 計算した結果を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算した結果です。次の計算があった場合は、計算対象となります。
    /// nullになりません。
    /// </remarks>
    public override decimal? Result => NumberToken.Number;

    /// <summary>
    /// この計算がActiveCalculatorの場合に表示される式を取得します。
    /// </summary>
    public override string Expression => "";

    protected NumberCalculation(): this(null)
    {
    }
}
