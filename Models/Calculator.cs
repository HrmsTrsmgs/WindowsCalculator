namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 電卓を表します。
/// </summary>
public class Calculator
{
    /// <summary>
    /// 電卓にトークンを入力します。
    /// </summary>
    /// <param name="token">入力されたトークン。</param>
    public void Input(Token token) 
    {
        var numberToken = token as NumberToken;
        if(numberToken == null) return;
        ActiveCaluculation = new Caluculation(numberToken.Number);
    }

    /// <summary>
    /// 現在行っている計算を取得、設定します。
    /// </summary>
    Caluculation ActiveCaluculation { get; set; }

    /// <summary>
    /// 計算結果を取得します。
    /// </summary>
    public int Result => ActiveCaluculation.Result;
}
