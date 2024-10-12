namespace Marimo.WindowsCalculator.Models;

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
    /// Backspaceの操作。
    /// </summary>
    Backspace,

    /// <summary>
    /// Cの操作。
    /// </summary>
    C,

    /// <summary>
    /// Undo操作。
    /// </summary>
    Undo,

    /// <summary>
    /// Redo操作。
    /// </summary>
    Redo,

    /// <summary>
    /// 現在の入力された数値を0にします。
    /// </summary>
    CE
}
