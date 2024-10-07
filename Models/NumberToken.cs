using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    /// 小数点以下の最大桁数です。
    /// </summary>
    public const int MaxDecimalPlaces = 5;

    /// <summary>
    /// 整数の最大桁数です。
    /// </summary>
    public const int MaxDigits = 16;

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

    /// <summary>
    /// 表す数が整数であることを表します。
    /// </summary>
    bool IsInteger => Number == Decimal.Truncate(Number);


    public override string ToString()
        => Number < 
            Enumerable.Repeat(10m, NumberToken.MaxDigits)
            .Aggregate(1m, (acc, val) => acc * val)
        ? Number.ToString() + (IsInteger && decimalPlaces != null ? "." : "")
        : string.Format($"{{0:0.{new string('#', NumberToken.MaxDigits - 1)}E+0}}", Number);
}
