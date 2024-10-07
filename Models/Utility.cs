namespace Marimo.MauiBlazor.Models;

/// <summary>
/// 便利に使える機能を提供します。
/// </summary>
static class Utility
{
    public static decimal Pow(decimal x, int y)
        => Enumerable.Repeat(x, y).Aggregate(1m, (acc, val) => acc * val);

    public static decimal Truncate(int? cecimalPlaces, decimal number)
    {
        decimal factor
            = Utility.Pow(
                10,
                cecimalPlaces ?? throw new InvalidOperationException());
        return  Decimal.Truncate(number * factor) / factor;
    }
}
