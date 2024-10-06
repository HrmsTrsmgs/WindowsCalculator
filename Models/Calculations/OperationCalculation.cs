namespace Marimo.MauiBlazor.Models.Calculations;

/// <summary>
/// 計算、もしくは最初に入力された数値を表します。
/// </summary>
/// <param name="Operand">計算される、もしくは最初に入力された数値。</param>
public class OperationCalculation(Calculation receiver, Key? @operator = null, int? operand = null) : Calculation(receiver)
{
    /// <summary>
    /// 計算の内容を表す演算子を取得、設定します。
    /// </summary>
    public Key? Operator { get; set; } = @operator;

    /// <summary>
    /// 演算子で計算される比演算子を指します。
    /// </summary>
    /// <remarks>
    /// 演算子で計算される比演算子を指します。
    /// Receiverに対してこの数値で演算を行います。
    /// 入力されるまではnullとなります。
    /// </remarks>
    public int? Operand { get; set; } = operand;


    /// <summary>
    /// 計算する対象を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算する対象です。
    /// ここではnullになりません。
    /// </remarks>
    public override Calculation? Receiver 
    {
        get => base.Receiver;
        set => base.Receiver = value;
    }

    /// <summary>
    /// 計算した結果を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算した結果です。次の計算があった場合は、計算対象となります。
    /// 基本的にはnullになりません。
    /// 演算子が入力され、計算対象が入力されていない場合などにnullとなります。
    /// </remarks>
    public override int? Result =>
        Receiver == null 
        ? null 
        : Operator switch
        { 
            Key.Plus => Receiver.Result + Operand,
            Key.Minus => Receiver.Result - Operand,
            Key.Multiply => Receiver.Result * Operand,
            Key.Divide => Receiver.Result / Operand,
            _ => throw new NotImplementedException()
        };
}
