using CommunityToolkit.Mvvm.ComponentModel;

namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 電卓を表します。
/// </summary>
public class Calculator :ObservableObject
{
    /// <summary>
    /// 電卓にトークンを入力します。
    /// </summary>
    /// <param name="token">入力されたトークン。</param>
    public void Input(Token token)
    {
        switch (token) 
        {
            case NumberToken t:
                ActiveCaluculation.Operand = t.Number;
                break;
            case OperatorToken t:
                activeCaluculation = new Caluculation(t.Operator, null);
                break;
        }
        OnPropertyChanged(nameof(Result));
    }

    /// <summary>
    /// 現在行っている計算です。
    /// </summary>
    Caluculation activeCaluculation = new(null, 0);

    /// <summary>
    /// 現在行っている計算を取得、設定します。
    /// </summary>
    public Caluculation ActiveCaluculation 
    {
        get => activeCaluculation;
        private set => SetProperty(ref activeCaluculation, value); 
    }

    /// <summary>
    /// 計算結果を取得します。
    /// </summary>
    public int Result => ActiveCaluculation.Result;
}
