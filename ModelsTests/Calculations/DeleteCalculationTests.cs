using Marimo.WindowsCalculator.Models.Calculations;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class DeleteCalculationTest
{
    DeleteCalculation tested { get; } = new(new NumberCalculation(null));

    [Fact]
    public void 計算結果はゼロです()
    {
        tested.Result.Should().Be(0);
    }

    [Fact]
    public void 式は表示されません()
    {
        tested.CurrentExpression.Should().BeEmpty();
    }
}
