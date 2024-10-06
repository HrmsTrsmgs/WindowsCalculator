using CommunityToolkit.Mvvm.ComponentModel;

namespace Marimo.MauiBlazor.Models.Calculations;

/// <summary>
/// 計算、もしくは最初に入力された数値を表します。
/// </summary>
/// <param name="Operand">計算される、もしくは最初に入力された数値。</param>
public class OperationCalculation(char? @operator = null, int? operand = null) : Calculation
{
    public char? Operator { get; set; } = @operator;
    public int? Operand { get; set; } = operand;

    public int Result => Operand ?? throw new Exception("とりあえず");
}
