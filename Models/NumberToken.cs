namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 入力された数値の文字列を表します。
/// </summary>
/// <param name="number">
/// 文字列で表された数値。
/// </param>
public class NumberToken(decimal number) : Token
{
    const int MaxDecimalPlaces = 5;

    /// <summary>
    /// 入力された数値を取得します。
    /// </summary>
    public decimal Number { get; set; } = number;

    int? decimalPlaces = null;
    public int? DecimalPlaces 
    {
        get => decimalPlaces;
        set => decimalPlaces 
            = value == null 
            ? null 
            : Math.Min(value.Value, MaxDecimalPlaces);
    }
}
