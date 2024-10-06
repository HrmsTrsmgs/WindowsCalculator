namespace Marimo.MauiBlazor.Models.Calculations;

/// <summary>
/// 電卓の＝ボタンが行っている演算を行います。
/// </summary>
public class EqualButtonCalculation : Calculation
{
    public override int? Result => Receiver.Result;
}
