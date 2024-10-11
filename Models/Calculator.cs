using CommunityToolkit.Mvvm.ComponentModel;
using Marimo.WindowsCalculator.Models.Calculations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// 電卓を表します。
/// </summary>
public class Calculator : ModelBase
{
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
            if (activeCaluculation == value) return;
            value.Receiver = activeCaluculation;
            SetProperty(ref activeCaluculation, value);
        }
    }

    /// <summary>
    /// 履歴用の計算結果一覧を取得します。
    /// </summary>
    public IEnumerable<CalculationHistoryItem> CalculationHistory
    {
        get
        {
            var current = ActiveCaluculation;
            while(current != null)
            {
                if (current.Result != null && current is OperationCalculation)
                {
                    var operation = current as OperationCalculation;
                    yield return new(
                        $"{operation!.CurrentExpression} {operation.Operand} =",
                        operation.Result!.Value);
                }
                current = current.Receiver;
            }
        }
    }

    /// <summary>
    /// Redo用に取ってある計算を取得、設定します。
    /// </summary>
    public Calculation? RedoCalculation { get; set; } = null;



    /// <summary>
    /// 計算結果を取得します。
    /// </summary>
    public string DisplaiedNumber
    {
        get
        {
            try
            {
                return
                    ActiveCaluculation switch
                    {
                        NumberCalculation c => c.NumberToken.ToString(),
                        OperationCalculation c
                            => (
                                    c.IsDisplaiedResult && c.Result != null
                                        ? c.Result
                                    : c.Result != null
                                        ? c.Operand?.Number
                                        : c.Receiver?.Result
                                )?.ToString() ??
                                throw new InvalidOperationException(
                                    "今の演算も前の演算も結果が出てないのはおかしいはず"),
                        EqualButtonCalculation c
                            => c.Result.ToString() ?? throw new InvalidOperationException(),
                        DeleteCalculation c
                            => c.Result.ToString() ?? throw new InvalidOperationException(),
                        _ => "0"

                    };
            }
            catch (DivideByZeroException)
            {
                return "0 で割ることはできません";
            }
        }
    }

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
                switch(t.Kind)
                {
                    case OtherTokenKind.Undo:
                        if (RedoCalculation == null)
                        {
                            RedoCalculation = ActiveCaluculation;
                        }
                        SetProperty(ref activeCaluculation!, ActiveCaluculation.Receiver, nameof(ActiveCaluculation));
                        switch(ActiveCaluculation)
                        {
                            case OperationCalculation c:
                                c.IsDisplaiedResult = true; 
                                break;
                        }
                        break;
                    case OtherTokenKind.Redo:
                        if (RedoCalculation != null)
                        {
                            var nextCalculation = RedoCalculation;
                            while (nextCalculation!.Receiver != ActiveCaluculation)
                            {
                                nextCalculation = nextCalculation!.Receiver;
                            }
                            SetProperty(ref activeCaluculation, nextCalculation, nameof(ActiveCaluculation));
                        }
                        break;
                    default: 
                        ActiveCaluculation = GetCalculationOtherWhenTokenInputed(t);
                        break;
                };
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
            OtherTokenKind.Delete =>
                new DeleteCalculation(ActiveCaluculation),
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
                ActiveCaluculation, before.OperatorToken, before.Operand);
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
                c.NumberToken = token;
                break;
            case OperationCalculation c:
                c.Operand = token;
                break;
        }
    }

}
