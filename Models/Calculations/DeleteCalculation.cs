namespace Marimo.WindowsCalculator.Models.Calculations;

/// <summary>
/// 削除を表す計算です。
/// </summary>
public  class DeleteCalculation(Calculation? receiver) : Calculation(receiver)
{
    /// <summary>
    /// 計算した結果を取得します。クリアなのでゼロです。
    /// </summary>
    public override decimal? Result => 0;

    /// <summary>
    /// この計算がActiveCalculatorの場合に表示される式を取得します。
    /// </summary>
    public override string CurrentExpression => "";
}
