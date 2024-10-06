using CommunityToolkit.Mvvm.ComponentModel;
using Marimo.MauiBlazor.Models.Calculations;

namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 電卓を表します。
/// </summary>
public class Calculator :ObservableObject
{
    /// <summary>
    /// 電卓にトークンを入力します。
    /// </summary>
    /// <param name="token">入力されたトークン。</param>
    public void Input(Token token)
    {
        switch (token)
        {
            case NumberToken t:
                InputNumberToken(t);
                break;
            case OperatorToken t:
                ActiveCaluculation 
                    = new OperationCalculation(ActiveCaluculation, t.Operator, null);
                break;
            case OtherToken t:
                ActiveCaluculation = GetCalculationOtherWhenTokenInputed(t);
                break;
        }
        OnPropertyChanged(nameof(DisplaiedNumber));
    }

    /// <summary>
    /// 数値と演算子以外の入力を行います。
    /// </summary>
    private Calculation GetCalculationOtherWhenTokenInputed(OtherToken otherToken)
        => otherToken.Kind switch
        {
            OtherTokenKind.Equal =>
                new EqualButtonCalculation(
                    ActiveCaluculation,
                    ActiveCaluculation switch
                    {
                        EqualButtonCalculation c => GetLastOperationCalculation(c),
                        _ => null
                    }),
            _ => throw new NotSupportedException()
        };

    /// <summary>
    /// 次の最後の演算を取得します。
    /// </summary>
    /// <param name="lastCaluculation">これまでの最後の計算</param>
    /// <returns>これまでの最後の演算。</returns>
    private OperationCalculation? GetLastOperationCalculation(
        EqualButtonCalculation lastCaluculation)
    {
        var before
            = lastCaluculation.LastOperationCalculation
            ?? lastCaluculation.Receiver as OperationCalculation
            ?? throw new InvalidOperationException();
        return new OperationCalculation(
                ActiveCaluculation, before.Operator, before.Operand);
    }

    /// <summary>
    /// 数値トークンが入力された処理を行います。
    /// </summary>
    /// <param name="token">数値トークン。</param>
    private void InputNumberToken(NumberToken token)
    {
        switch (ActiveCaluculation)
        {
            case NumberCalculation c:
                c.Number = token.Number;
                break;
            case OperationCalculation c:
                c.Operand = token.Number;
                break;
        }
    }

    /// <summary>
    /// 現在行っている計算です。
    /// </summary>
    Calculation activeCaluculation = new NumberCalculation(null);

    /// <summary>
    /// 現在、最新で行われている計算を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 現在、最新で行われている計算を取得、設定します。
    /// 設定された値のレシーバーは、直前の計算に設定されます。
    /// </remarks>
    public Calculation ActiveCaluculation 
    {
        get => activeCaluculation;
        private set
        {
            value.Receiver = activeCaluculation;
            SetProperty(ref activeCaluculation, value);
        }
    }

    /// <summary>
    /// 計算結果を取得します。
    /// </summary>
    public string DisplaiedNumber =>
        ActiveCaluculation switch
        {
            NumberCalculation c => c.Number.ToString(),
            OperationCalculation c
                => (c.Result != null ? c.Operand : c.Receiver?.Result)?.ToString() ??
                    throw new InvalidOperationException(
                        "今の演算も前の演算も結果が出てないのはおかしいはず"),
            EqualButtonCalculation c 
                => c.Result.ToString() ?? throw new InvalidOperationException()
           
        };
}
