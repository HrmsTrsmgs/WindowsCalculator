namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 入力された数値の文字列を表します。
/// </summary>
public class NumberToken : Token
{
    /// <summary>
    /// 入力された数値です。
    /// </summary>
    public int Number { get; init; }
}
