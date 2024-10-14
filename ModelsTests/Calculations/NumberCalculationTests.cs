using Marimo.WindowsCalculator.Models.Calculations;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class NumberCalculationTest
{
    NumberCalculation Tested { get; } = new(Calculation.NullObject);

    [Fact]
    public void 計算結果は入力された数値そのものです()
    {
        Tested.NumberToken = new(10);
        Tested.Result.Should().Be(10);
    }

    [Fact]
    public void 式は表示されません()
    {
        Tested.NumberToken = new(10);
        Tested.Expression.Should().BeEmpty();
    }
}
