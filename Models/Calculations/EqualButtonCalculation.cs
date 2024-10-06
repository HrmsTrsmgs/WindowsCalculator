namespace Marimo.MauiBlazor.Models.Calculations;

/// <summary>
/// 電卓の＝ボタンが行っている演算を行います。
/// </summary>
public class EqualButtonCalculation : Calculation
{
    public EqualButtonCalculation(Calculation receiver, OperationCalculation? lastCalculation)
        : base(receiver)
    {
        if (lastCalculation != null)
        {
            LastCalculation =
                new OperationCalculation(Receiver?.Receiver, lastCalculation.Operator, lastCalculation.Operand)
                {
                };

        }
    }


    /// <summary>
    /// 計算結果を取得、設定します。
    /// </summary>
    public override int? Result =>
        LastCalculation switch
        {
            OperationCalculation c => c.Result,
            null => Receiver?.Result,
        };

    /// <summary>
    /// イコールにより最後の演算を繰り返すとき、最後の演算を指します。
    /// </summary>
    public Calculation? LastCalculation { get; }
}
