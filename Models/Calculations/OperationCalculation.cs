﻿namespace Marimo.MauiBlazor.Models.Calculations;

/// <summary>
/// 計算、もしくは最初に入力された数値を表します。
/// </summary>
/// <param name="Operand">計算される、もしくは最初に入力された数値。</param>
public class OperationCalculation(Calculation receiver, Key? operatorToken = null, NumberToken? operand = null) : Calculation(receiver)
{
    public bool IsDisplaiedResult { get; set; } = false;

    /// <summary>
    /// 計算の内容を表す演算子を取得、設定します。
    /// </summary>
    public Key? OperatorToken 
    {
        get => operatorToken;
        set
        {
            SetProperty(ref operatorToken, value);
        }
    }

    /// <summary>
    /// 演算子で計算される比演算子を指します。
    /// </summary>
    /// <remarks>
    /// 演算子で計算される比演算子を指します。
    /// Receiverに対してこの数値で演算を行います。
    /// 入力されるまではnullとなります。
    /// </remarks>
    public NumberToken? Operand 
    {
        get => operand;
        set
        {
            IsDisplaiedResult = false;
            SetProperty(ref operand, value);
        }
    }

    /// <summary>
    /// 計算する対象を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算する対象です。
    /// ここではnullになりません。
    /// </remarks>
    public override Calculation? Receiver 
    {
        get => base.Receiver;
        set => base.Receiver = value;
    }

    /// <summary>
    /// 計算した結果を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算した結果です。次の計算があった場合は、計算対象となります。
    /// 基本的にはnullになりません。
    /// 演算子が入力され、計算対象が入力されていない場合などにnullとなります。
    /// </remarks>
    public override decimal? Result =>
        Receiver == null 
        ? null 
        : OperatorToken switch
        { 
            Key.Plus => Receiver.Result + Operand?.Number,
            Key.Minus => Receiver.Result - Operand?.Number,
            Key.Multiply => Receiver.Result * Operand?.Number,
            Key.Divide => Receiver.Result / Operand?.Number,
            _ => throw new NotImplementedException()
        };

    public override string CurrentExpression => throw new NotImplementedException();
}
