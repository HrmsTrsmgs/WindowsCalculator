namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 入力された数値の文字列を表します。
/// </summary>
public class NumberToken(int number) : Token
{
    /// <summary>
    /// 入力された数値です。
    /// </summary>
    public int Number { get; } = number;
}
