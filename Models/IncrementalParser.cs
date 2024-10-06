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
    /// <param name="charactor">入力された一文字。</param>
    public void Input(char charactor)
    {
        switch (charactor)
        {
            case >= '0' and <= '9':
                ActiveToken = new NumberToken(charactor - '0');
                break;
            case '+' or '-' or '*' or '/':

                switch (ActiveToken)
                {
                    case OperatorToken t:
                        t.Operator = charactor;
                        break;
                    default:
                        ActiveToken = new OperatorToken(charactor);
                        break;
                }
                

                break;
        }
    }
}
