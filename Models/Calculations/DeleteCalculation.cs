namespace Marimo.MauiBlazor.Models.Calculations;

/// <summary>
/// 削除を表す計算です。
/// </summary>
internal class DeleteCalculation(Calculation? receiver) : Calculation(receiver)
{
    public override decimal? Result => 0;
}
