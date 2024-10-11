using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Marimo.WindowsCalculator.Models;

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
    public void Input(InputAction key)
    {
        Log.Info($"{key}が入力されました。");
        switch (key)
        {
            case >= InputAction.Zero and <= InputAction.Nine:
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
            case InputAction.Dot:
                switch (ActiveToken)
                {
                    case NumberToken t:
                        t.DecimalPlaces ??= 0;
                        break;
                }
                break;
            case InputAction.Plus or InputAction.Minus or InputAction.Multiply or InputAction.Divide:
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
            case InputAction.Equal:
                ActiveToken = OtherToken.Equal;
                break;
            case InputAction.C:
                ActiveToken = OtherToken.Delete;
                break;
            case InputAction.Backspace:
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
            case InputAction.CE:
                switch (ActiveToken)
                {
                    case NumberToken t:
                        t.Number = 0;
                        break;
                }
                break;
            case InputAction.Undo:
                ActiveToken = OtherToken.Undo;
                break;
        }
    }


}
