namespace Marimo.WindowsCalculator.Models.Tokens;

/// <summary>
/// 演算を表すトークンです。
/// </summary>
/// <param name="operator">塩酸氏を表すキー。</param>
public class OperatorToken(InputAction @operator) : Token
{
    /// <summary>
    /// 演算子を表すキーを取得、設定します。
    /// </summary>
    public InputAction Operator { get; set; } = @operator;
}
