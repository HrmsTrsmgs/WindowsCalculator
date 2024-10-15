using Marimo.WindowsCalculator.Models.Tokens;

namespace Marimo.WindowsCalculator.Models.Calculations;

/// <summary>
/// 割り算を表します。
/// </summary>
/// <param name="receiver">計算対象</param>
/// <param name="operand">計算される数値。</param>
/// <param name="isDisplayResult">結果が式の表示対象になるか。</param>
public class DivideCalculation(Calculation receiver, NumberToken? operand = null, bool isDisplayResult = false) : OperationCalculation(receiver, operand, isDisplayResult)
{
    /// <summary>
    /// 計算の内容を表す演算子を取得します。
    /// </summary>
    public override InputAction? OperatorAction => InputAction.Divide;

    /// <summary>
    /// 演算子を表す文字列を取得します。
    /// </summary>
    protected override string OperatorString => "÷";

    /// <summary>
    /// 結果の計算を行い、取得します。
    /// </summary>
    /// <returns>計算結果。</returns>
    protected override decimal? GetResult()
    {
        try
        {
            return Math.Round(Receiver.Result / Operand?.Number ?? 0, NumberToken.MaxDecimalPlaces, MidpointRounding.AwayFromZero);
        }
        catch(DivideByZeroException)
        {
            DisplayError = "0 で割ることはできません";
            return decimal.MaxValue;
        }
    }
}
