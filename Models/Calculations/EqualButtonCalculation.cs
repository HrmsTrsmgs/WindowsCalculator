namespace Marimo.MauiBlazor.Models.Calculations;

/// <summary>
/// 電卓の＝ボタンが行っている演算を行います。
/// </summary>
public class EqualButtonCalculation : Calculation
{
    /// <summary>
    /// EqualButtonCalculationを初期化します。
    /// </summary>
    /// <param name="receiver">計算対象。</param>
    /// <param name="lastCalculation">イコールで演算を繰り返すとき、最後の演算となる対象。一度目のイコールではnullとなります。</param>
    public EqualButtonCalculation(Calculation receiver, Calculation? lastCalculation)
        : base(receiver)
    {
        LastOperationCalculation =
            lastCalculation switch
            {
                OperationCalculation c =>
                    new OperationCalculation(receiver, c.Operator, c.Operand),
                _ => null
            };
    }


    /// <summary>
    /// 計算結果を取得、設定します。
    /// </summary>
    public override int? Result =>
        LastOperationCalculation switch
        {
            OperationCalculation c => c.Result,
            null => Receiver?.Result,
        };

    /// <summary>
    /// イコールにより最後の演算を繰り返すとき、最後の演算を指します。
    /// </summary>
    public OperationCalculation? LastOperationCalculation { get; }
}
