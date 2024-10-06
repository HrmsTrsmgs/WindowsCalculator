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

    /// <summary>
    /// 入力中の小数点の桁数です。小数の入力中でなければnullです。
    /// </summary>
    int? decimalPlaces = null;

    /// <summary>
    /// 入力中の小数点の桁数を取得、設定します。小数の入力中でなければnullです。
    /// </summary>
    public int? DecimalPlaces 
    {
        get => decimalPlaces;
        set => decimalPlaces 
            = value == null 
            ? null 
            : Math.Min(value.Value, MaxDecimalPlaces);
    }
}
