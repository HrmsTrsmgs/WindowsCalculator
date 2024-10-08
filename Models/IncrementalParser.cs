using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 一文字ずつの入力を処理します。
/// </summary>
public class IncrementalParser : ModelBase
{

    /// <summary>
    /// 現在入力されているトークンです。
    /// </summary>
    Token? activeToken = null;

    /// <summary>
    /// 現在入力されているトークンを取得します。
    /// </summary>
    public Token? ActiveToken 
    {
        get => activeToken;
        set => SetProperty(ref activeToken, value);
    }

    /// <summary>
    /// 一文字ずつの入力を処理します。
    /// </summary>
    /// <param name="key">入力された一文字。</param>
    public void Input(Key key)
    {
        Log.Info($"{key}が入力されました。");
        switch (key)
        {
            case >= Key.Zero and <= Key.Nine:
                switch (ActiveToken)
                {
                    case NumberToken t:
                        if (t.DecimalPlaces == null
                            && t.Number < Utility.Pow(10m, NumberToken.MaxDigits - 1))                       {
                            t.Number = t.Number * 10 + (int)key;
                        }
                        else if(t.DecimalPlaces < NumberToken.MaxDecimalPlaces)
                        {
                            t.DecimalPlaces++;
                            t.Number
                                += (int)key
                                    * Utility.Pow(0.1m, t.DecimalPlaces.Value);
                        }
                        break;
                    default:
                        ActiveToken = new NumberToken((int)key);
                        break;
                }
                break;
            case Key.Dot:
                switch (ActiveToken)
                {
                    case NumberToken t:
                        t.DecimalPlaces ??= 0;
                        break;
                }
                break;
            case Key.Plus or Key.Minus or Key.Multiply or Key.Divide:
                switch (ActiveToken)
                {
                    case OperatorToken t:
                        t.Operator = key;
                        break;
                    default:
                        ActiveToken = new OperatorToken(key);
                        break;
                }
                break;
            case Key.Equal:
                ActiveToken = OtherToken.Equal;
                break;
            case Key.Delete:
                ActiveToken = OtherToken.Delete;
                break;
            case Key.Backspace:
                switch (ActiveToken)
                {
                    case NumberToken t:
                        switch (t.DecimalPlaces)
                        {
                            case null:
                                t.Number = Decimal.Truncate(t.Number / 10);
                                break;
                            case 0:
                                t.DecimalPlaces = null;
                                break;
                            case >= 1:
                                t.DecimalPlaces--;
                                t.Number = Utility.Truncate(t.DecimalPlaces, t.Number);

                                break;

                        }
                        
                        break;
                }
                break;
        }
    }


}
