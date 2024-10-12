namespace Marimo.WindowsCalculator.Models.Calculations;

/// <summary>
/// 電卓の＝ボタンが行っている演算を行います。
/// </summary>
public class EqualButtonCalculation : Calculation
{
    /// <summary>
    /// EqualButtonCalculationを初期化します。
    /// </summary>
    /// <param name="receiver">計算対象。</param>
    /// <param name="lastOperationCalculation">イコールで演算を繰り返すとき、最後の演算となる対象。一度目のイコールではnullとなります。</param>
    public EqualButtonCalculation(Calculation receiver, Calculation? lastOperationCalculation)
        : base(receiver)
    {
        switch (receiver)
        {
            case OperationCalculation c:
                c.IsDisplaiedResult = true;
                break;
        }

        LastOperationCalculation =
            lastOperationCalculation switch
            {
                OperationCalculation c =>
                    new OperationCalculation(receiver, c.OperatorToken, c.Operand, true),
                _ => null
            };
    }


    /// <summary>
    /// 計算結果を取得、設定します。
    /// </summary>
    public override decimal? Result =>
        LastOperationCalculation switch
        {
            OperationCalculation c => c.Result,
            null => Receiver?.Result,
        };

    /// <summary>
    /// イコールにより最後の演算を繰り返すとき、最後の演算を指します。
    /// </summary>
    public OperationCalculation? LastOperationCalculation { get; }

    /// <summary>
    /// この計算がActiveCalculatorの場合に表示される式を取得します。
    /// </summary>
    public override string CurrentExpression
    {
        get
        {
            OperationCalculation? calculator = (LastOperationCalculation ?? Receiver) as OperationCalculation;
            return $"{calculator!.CurrentExpression} {calculator!.Operand?.Number} =";
        }
    }
}
