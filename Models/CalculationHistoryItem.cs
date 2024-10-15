using Marimo.WindowsCalculator.Models.Calculations;
using System.ComponentModel;

namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// 履歴の項目を表します。
/// </summary>
public class CalculationHistoryItem : ModelBase
{
    /// <summary>
    /// 表示される計算を取得します。
    /// </summary>
    public OperationCalculation? Operation { get; }

    /// <summary>
    /// 履歴に表示する式。
    /// </summary>
    string expression;
    /// <summary>
    /// 履歴に表示する式を取得、設定します。
    /// </summary>
    public string Expression
    {
        get => expression;
        set => SetProperty(ref expression, value);
    }

    /// <summary>
    /// 履歴に表示する答え。
    /// </summary>
    decimal? result;
    /// <summary>
    /// 履歴に表示する答えを取得、設定します。
    /// </summary>
    public decimal? Result
    {
        get => result;
        set => SetProperty(ref result, value);
    }


    /// <summary>
    /// OperationCalculationを指定して、CalculationHistoryItemクラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="operation">表示される計算。</param>
    public CalculationHistoryItem(OperationCalculation operation)
    {
        Operation = operation;
        expression = $"{Operation!.Expression}　{Operation.Operand} =";
        result = Operation.Result;
        Operation.PropertyChanged += (_, args) =>
        {
            switch (args.PropertyName)
            {
                case nameof(Operation.Expression) or nameof(Operation.Operand):
                    Expression = $"{operation!.Expression}　{operation.Operand} =";
                    break;
                case nameof(Operation.Result):
                    Result = operation.Result;
                    break;
            }
        };
    }
}
