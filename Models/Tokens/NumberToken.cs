using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Marimo.WindowsCalculator.Models.Tokens;

/// <summary>
/// 入出力に使う数値の文字列を表します。
/// </summary>
/// <param name="number">
/// 文字列で表された数値。
/// </param>
public class NumberToken(decimal? number) : Token
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
    public decimal Number { get; set; } = number ?? 0;

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
    bool IsInteger => Number == decimal.Truncate(Number);

    /// <summary>
    /// NumberTokenに関係がある種類かどうかを取得します。関係あればtrue,そうでなければfalseです。
    /// </summary>
    /// <param name="input">確認するキー入力。</param>
    /// <returns>関係があるかどうか。</returns>
    public static bool CanRead(InputAction input)
        => input
            is >= InputAction.Zero and <= InputAction.Nine
                or InputAction.Dot
                or InputAction.Backspace
                or InputAction.CE;

    /// <summary>
    /// inputを読み込ませます。NumberTokenに関係あるinputだったらtrueを、そうでないならfalseを返します。
    /// </summary>
    /// <param name="input">入力されたキー。</param>
    /// <returns>NumberTokenに関係あるinputか。</returns>
    public bool Input(InputAction input)
    {
        if (!CanRead(input)) return false;
        switch(input)
        {
            case InputAction.Dot:
                DecimalPlaces ??= 0;
                break;
            case InputAction.Backspace:
                switch (DecimalPlaces)
                {
                    case null:
                        Number = Decimal.Truncate(Number / 10);
                        break;
                    case 0:
                        DecimalPlaces = null;
                        break;
                    case >= 1:
                        DecimalPlaces--;
                        Number = Utility.Truncate(DecimalPlaces.Value, Number);
                        break;
                }
                break;
            case InputAction.CE:
                Number = 0;
                break;
            default:
                if (DecimalPlaces == null
                && Number < Utility.Pow(10m, NumberToken.MaxDigits - 1))
                {
                    Number = Number * 10 + (int)input;
                }
                else if (DecimalPlaces < NumberToken.MaxDecimalPlaces)
                {
                    DecimalPlaces++;
                    Number
                        += (int)input
                            * Utility.Pow(0.1m, DecimalPlaces.Value);
                }
                break;
        }
        return true;
    }

    /// <summary>
    /// 現在のオブジェクトを表す文字列を返します。
    /// </summary>
    /// <returns>現在のオブジェクトを表す文字列。</returns>
    public override string ToString()
        => Number < Utility.Pow(10, MaxDigits)
        ? Number.ToString($"#,##0.{new string('#', MaxDecimalPlaces)}") + (IsInteger && decimalPlaces != null ? "." : "")
        : Number.ToString($"0.{new string('#', MaxDigits - 1)}e+0");
}
