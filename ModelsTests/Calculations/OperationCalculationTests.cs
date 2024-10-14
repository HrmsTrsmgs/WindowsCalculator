using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class OperationCalculationTests
{
    OperationCalculation tested { get; set; }
        = new(
            new NumberCalculation(null){ NumberToken = new(10) },
            InputAction.Add,
            new(20),
            true);

    [Fact]
    public void 計算結果が出力されます()
    {
        tested.Result.Should().Be(30);

        tested.OperatorToken = InputAction.Substract;

        tested.Result.Should().Be(-10);

        tested.OperatorToken = InputAction.Multiply;

        tested.Result.Should().Be(200);

        tested.OperatorToken = InputAction.Divide;

        tested.Result.Should().Be(0.5m);
    }

    [Fact]
    public void 式は計算途中の式が表示されます()
    {
        tested.Expression.Should().Be("10　+");

        tested.OperatorToken = InputAction.Substract;

        tested.Expression.Should().Be("10　-");

        tested.OperatorToken = InputAction.Multiply;

        tested.Expression.Should().Be("10　×");

        tested.OperatorToken = InputAction.Divide;

        tested.Expression.Should().Be("10　÷");
    }
}
