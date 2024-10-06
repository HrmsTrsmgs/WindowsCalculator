namespace Marimo.MauiBlazor.Models.Calculations;

/// <summary>
/// 計算の最初にある、演算の対象となるのみの数字を表します。
/// </summary>
/// <param name="receiver"></param>
public class NumberCalculation(Calculation? receiver) : Calculation(receiver)
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 計算する対象を取得します。
    /// 初期状態の計算しなくてもある数字を表しているので、なので必ずnullです。
    /// </remarks>
    public override Calculation? Receiver => null;

    /// <summary>
    /// 計算の最初の数値を取得、設定します。
    /// </summary>
    public int Number { get; set; } = 0;

    /// <summary>
    /// 計算した結果を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算した結果です。次の計算があった場合は、計算対象となります。
    /// nullになりません。
    /// </remarks>
    public override int? Result => Number;
}
