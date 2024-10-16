using Marimo.WindowsCalculator.Models.Tokens;

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
    /// <param name="input">入力された一文字。</param>
    public void Input(InputAction input)
    {
        Log.Info($"{input}が入力されました。");
        bool isCompleted = false;
        switch (ActiveToken)
        {
            case NumberToken t:
                isCompleted = t.Input(input);
                break;
            default:
                if (!(input is InputAction.Backspace or InputAction.CE)
                    && NumberToken.CanRead(input))
                {
                    ActiveToken = new NumberToken((int)input);
                    isCompleted = true;
                }
                else if (input is InputAction.CE)
                {
                    ActiveToken = OtherToken.CE;
                    isCompleted = true;
                }
                break;
        }
        if (isCompleted) return;
        switch(input)
        { 
            case InputAction.Add or InputAction.Substract or InputAction.Multiply or InputAction.Divide:
                switch (ActiveToken)
                {
                    case OperatorToken t:
                        t.Operator = input;
                        break;
                    default:
                        ActiveToken = new OperatorToken(input);
                        break;
                }
                break;
            case InputAction.Equal:
                ActiveToken = OtherToken.Equal;
                break;
            case InputAction.C:
                ActiveToken = OtherToken.C;
                break;
            case InputAction.Undo:
                ActiveToken = OtherToken.Undo;
                break;
        }
    }
}
