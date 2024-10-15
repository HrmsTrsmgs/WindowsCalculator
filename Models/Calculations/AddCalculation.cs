namespace Marimo.WindowsCalculator.Models.Calculations;

/// <summary>
///\\を表します。
/// </summary>
/// <param name="receiver">計算対象</param>
/// <param name="operand">計算される数値。</param>
/// <param name="isDisplaiedResult">結果が式の表示対象になるか。</param>
public class AddCalculation(Calculation receiver, NumberToken? operand = null, bool isDisplaiedResult = false) : OperationCalculation(receiver, operand, isDisplaiedResult)
{
    /// <summary>
    /// 計算の内容を表す演算子を取得します。
    /// </summary>
    public override InputAction? OperatorToken => InputAction.Add;

    /// <summary>
    /// 演算子を表す文字列を取得します。
    /// </summary>
    protected override string OperatorString => "+";

    /// <summary>
    /// 結果の計算を行い、取得します。
    /// </summary>
    /// <returns>計算結果。</returns>
    protected override decimal? GetResult()
        => Receiver.Result + Operand?.Number;
}
