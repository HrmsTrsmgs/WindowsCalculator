namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// 履歴の項目を表します。
/// </summary>
/// <param name="Expression">履歴に表示する式。</param>
/// <param name="Result">履歴に表示する答え。</param>
public record CalculationHistoryItem(string Expression, decimal Result);
