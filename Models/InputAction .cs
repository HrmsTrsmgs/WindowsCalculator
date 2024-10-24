﻿namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// 入力されたキー入力の種類を表します。
/// </summary>
public enum InputAction
{
    /// <summary>
    /// 0キー。
    /// </summary>
    Zero = 0,

    /// <summary>
    /// 1キー。
    /// </summary>
    One = 1,

    /// <summary>
    /// 2キー。
    /// </summary>
    Two = 2,

    /// <summary>
    /// 3キー。
    /// </summary>
    Three = 3,

    /// <summary>
    /// 4キー。
    /// </summary>
    Four = 4,

    /// <summary>
    /// 5キー。
    /// </summary>
    Five = 5,

    /// <summary>
    /// 6キー。
    /// </summary>
    Six = 6,

    /// <summary>
    /// 7キー。
    /// </summary>
    Seven = 7,

    /// <summary>
    /// 8キー。
    /// </summary>
    Eight = 8,

    /// <summary>
    /// 9キー。
    /// </summary>
    Nine = 9,

    /// <summary>
    /// 小数点キー。
    /// </summary>
    Dot,

    /// <summary>
    /// +キー。
    /// </summary>
    Add,

    /// <summary>
    /// -キー。
    /// </summary>
    Substract,

    /// <summary>
    /// *キー。
    /// </summary>
    Multiply,

    /// <summary>
    /// /キー。
    /// </summary>
    Divide,

    /// <summary>
    /// イコールを表す、=キーもしくはEnterキー。
    /// </summary>
    Equal,

    /// <summary>
    /// Undoを表すCtrl+zキー。
    /// </summary>
    Undo,

    /// <summary>
    /// Redoを表すCrrl+yキー。
    /// </summary>
    Redo,

    /// <summary>
    /// 計算機のCキー。ESCキー。
    /// </summary>
    C,

    /// <summary>
    /// 計算機のCEキー。Deleteキー。
    /// </summary>
    CE,

    /// <summary>
    /// バックスペースキー。
    /// </summary>
    Backspace

}
