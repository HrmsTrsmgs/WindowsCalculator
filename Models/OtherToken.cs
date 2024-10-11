namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// 演算子と数値以外のトークンを表します。
/// </summary>
/// <param name="kind">トークンの種類。</param>
public class OtherToken : Token
{
    OtherToken(OtherTokenKind kind) => Kind = kind;
    /// <summary>
    /// イコールトークンを取得します。
    /// </summary>
    public static OtherToken Equal { get; } = new OtherToken(OtherTokenKind.Equal);

    /// <summary>
    /// バックスペーストークンを取得します。
    /// </summary>
    public static OtherToken Backspace { get; } = new OtherToken(OtherTokenKind.Backspace);

    /// <summary>
    /// デリートトークンを取得します。
    /// </summary>
    public static OtherToken Delete { get; } = new OtherToken(OtherTokenKind.Delete);

    /// <summary>
    /// アンドゥトークンを取得します。
    /// </summary>
    public static OtherToken Undo { get; } = new OtherToken(OtherTokenKind.Undo);

    /// <summary>
    /// リドゥトークンを取得します。
    /// </summary>
    public static OtherToken Redo { get; } = new OtherToken(OtherTokenKind.Redo);

    /// <summary>
    /// トークンの種類を取得します。
    /// </summary>
    public OtherTokenKind Kind { get; }
}
