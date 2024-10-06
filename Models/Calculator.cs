﻿using CommunityToolkit.Mvvm.ComponentModel;
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
                ActiveCaluculation = new OperationCalculation(ActiveCaluculation, t.Operator, null);
                break;
            case OtherToken t:
                OperationCalculation? lastCalculation = null;
                if (ActiveCaluculation is EqualButtonCalculation)
                {
                    var before = ((EqualButtonCalculation)ActiveCaluculation).LastOperationCalculation;
                    if(before == null)
                    {
                        before = ((EqualButtonCalculation)ActiveCaluculation).Receiver as OperationCalculation;
                    }
                    lastCalculation = new OperationCalculation(ActiveCaluculation, before.Operator, before.Operand);
                }
                ActiveCaluculation =
                new EqualButtonCalculation(ActiveCaluculation, lastCalculation);
                break;
        }
        OnPropertyChanged(nameof(DisplaiedNumber));
    }

    /// <summary>
    /// 現在行っている計算です。
    /// </summary>
    Calculation activeCaluculation = new NumberCalculation(null);

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
                        "今の演算も前の演算も結果が出てないのはおかしいはず"),
            EqualButtonCalculation c => c.Result ?? 0
           
        };
}
