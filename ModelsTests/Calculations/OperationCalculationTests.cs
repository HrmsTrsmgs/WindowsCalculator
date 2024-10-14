using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class OperationCalculationTests
{
    OperationCalculation Tested { get; set; }
        = new(
            new NumberCalculation(Calculation.NullObject){ NumberToken = new(10) },
            InputAction.Add,
            new(20),
            true);

    [Fact]
    public void 計算結果が出力されます()
    {
        Tested.Result.Should().Be(30);

        Tested.OperatorToken = InputAction.Substract;

        Tested.Result.Should().Be(-10);

        Tested.OperatorToken = InputAction.Multiply;

        Tested.Result.Should().Be(200);

        Tested.OperatorToken = InputAction.Divide;

        Tested.Result.Should().Be(0.5m);
    }

    [Fact]
    public void 式は計算途中の式が表示されます()
    {
        Tested.Expression.Should().Be("10　+");

        Tested.OperatorToken = InputAction.Substract;

        Tested.Expression.Should().Be("10　-");

        Tested.OperatorToken = InputAction.Multiply;

        Tested.Expression.Should().Be("10　×");

        Tested.OperatorToken = InputAction.Divide;

        Tested.Expression.Should().Be("10　÷");
    }
}
