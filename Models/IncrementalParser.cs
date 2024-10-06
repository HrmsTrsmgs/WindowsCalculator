using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 一文字ずつの入力を処理します。
/// </summary>
public class IncrementalParser : ObservableObject
{

    Token? activeToken = null;
    /// <summary>
    /// 処理中のトークンを取得します。
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
        switch (key)
        {
            case >= Key.Zero and <= Key.Nine:
                ActiveToken = new NumberToken((int)key);
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
        }
    }
}
