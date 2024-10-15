namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// 便利に使える機能を提供します。
/// </summary>
static class Utility
{
    /// <summary>
    /// decimalの自然数乗をdecimalで取得します。
    /// </summary>
    /// <param name="x">基数。</param>
    /// <param name="y">べき指数。0未満は保証されません。</param>
    /// <returns>xのy乗。</returns>
    public static decimal Pow(decimal x, int y)
        => Enumerable.Repeat(x, y).Aggregate(1m, (acc, val) => acc * val);

    /// <summary>
    /// 桁を丸めます。
    /// </summary>
    /// <param name="cecimalPlaces">小数点以下桁数。0未満は保証しません。</param>
    /// <param name="number">切り捨てられる数字。</param>
    /// <returns>切り捨てられた数字。</returns>
    public static decimal Truncate(int cecimalPlaces, decimal number)
    {
        decimal factor
            = Pow(
                10,
                cecimalPlaces);
        return  Decimal.Truncate(number * factor) / factor;
    }
}
