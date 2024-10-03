using Marimo.MauiBlazor.Models.Tokens;

namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 一文字ずつの入力を処理します。
/// </summary>
public class IncrementalParser
{
    /// <summary>
    /// 処理中のトークンを取得します。
    /// </summary>
    public Token? ActiveToken { get; private set; } = null;

    /// <summary>
    /// 一文字ずつの入力を処理します。
    /// </summary>
    /// <param name="charactor">入力された一文字。</param>
    public void InputCharactor(char charactor)
    {
        switch (charactor)
        {
            case >= '0' and <= '9':
                ActiveToken = new NumberToken() { Number = int.Parse(charactor.ToString()) };
                break;
        }
    }
    
}
