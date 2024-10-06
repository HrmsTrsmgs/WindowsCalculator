using CommunityToolkit.Mvvm.ComponentModel;
using Marimo.MauiBlazor.Models.Calculations;
using System.Net.Http.Headers;

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
                switch (ActiveCaluculation) 
                {
                    case NumberCalculation c:
                        c.Number = t.Number;
                        break;
                    case OperationCalculation c:
                        c.Operand = t.Number;
                        break;
                }
                
                break;
            case OperatorToken t:
                ActiveCaluculation = new OperationCalculation(t.Operator, null);
                break;
        }
        OnPropertyChanged(nameof(DisplaiedNumber));
    }

    /// <summary>
    /// 現在行っている計算です。
    /// </summary>
    Calculation activeCaluculation = new NumberCalculation();

    /// <summary>
    /// 現在行っている計算を取得、設定します。
    /// </summary>
    public Calculation ActiveCaluculation 
    {
        get => activeCaluculation;
        private set
        {
            value.Receiver = activeCaluculation;
            SetProperty(ref activeCaluculation, value);
        }
    }

    /// <summary>
    /// 計算結果を取得します。
    /// </summary>
    public int DisplaiedNumber =>
        ActiveCaluculation switch
        {
            NumberCalculation c => c.Number,
            OperationCalculation c
                => (c.Result != null ? c.Operand : c.Receiver.Result) ??
                    throw new InvalidOperationException(
                        "今の演算も前の演算も結果が出てないのはおかしいはず")
        };
}
