namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 入力された数値の文字列を表します。
/// </summary>
/// <param name="number">
/// 文字列で表された数値。
/// </param>
public class NumberToken(decimal number) : Token
{
    /// <summary>
    /// 入力された数値を取得します。
    /// </summary>
    public decimal Number { get; set; } = number;
}
