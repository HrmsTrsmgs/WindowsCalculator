using CommunityToolkit.Mvvm.ComponentModel;

namespace Marimo.MauiBlazor.Models.Calculations;

/// <summary>
/// 計算、もしくは最初に入力された数値を表します。
/// </summary>
/// <param name="Operand">計算される、もしくは最初に入力された数値。</param>
public class OperationCalculation(Key? @operator = null, int? operand = null) : Calculation
{
    public Key? Operator { get; set; } = @operator;
    public int? Operand { get; set; } = operand;

    public override Calculation Receiver 
    {
        get => base.Receiver ?? throw new InvalidOperationException();
        set => base.Receiver = value; 
    }

    public override int? Result =>
        Operator switch
        { 
            Key.Plus => Receiver.Result + Operand,
            Key.Minus => Receiver.Result - Operand,
            Key.Multiply => Receiver.Result * Operand,
            Key.Divide => Receiver.Result / Operand
        };
}
