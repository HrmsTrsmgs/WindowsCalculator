namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 演算子と数値以外のトークンを表します。
/// </summary>
public enum OtherTokenKind
{
    /// <summary>
    /// =とEnterの操作。
    /// </summary>
    Equal,

    /// <summary>
    /// BackspaceとESCの操作。
    /// </summary>
    Backspace
}
