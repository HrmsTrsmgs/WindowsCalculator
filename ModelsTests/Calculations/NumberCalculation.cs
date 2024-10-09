using FluentAssertions;
using Marimo.MauiBlazor.Models.Calculations;

namespace Marimo.MauiBlazor.Tests.Models.Calculations;

public class NumberCalculationTest
{
    NumberCalculation tested { get; } = new(null);

    [Fact]
    public void 計算結果は入力された数値そのものです()
    {
        tested.NumberToken = new(10);
        tested.Result.Should().Be(10);
    }

    [Fact]
    public void 数値入力の式は表示されません()
    {
        tested.NumberToken = new(10);
        tested.CurrentExpression.Should().BeEmpty();
    }
}
