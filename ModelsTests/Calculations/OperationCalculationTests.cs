using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class OperationCalculationTests
{
    OperationCalculation tested { get; set; }
        = new(
            new NumberCalculation(null) { NumberToken = new(10) },
            InputAction.Plus,
            new(20)
            );

    [Fact]
    public void 計算結果が出力されます()
    {
        tested.Result.Should().Be(30);

        tested.OperatorToken = InputAction.Minus;

        tested.Result.Should().Be(-10);

        tested.OperatorToken = InputAction.Multiply;

        tested.Result.Should().Be(200);

        tested.OperatorToken = InputAction.Divide;

        tested.Result.Should().Be(0.5m);
    }

    [Fact]
    public void 式は計算途中の式が表示されます()
    {
        tested.CurrentExpression.Should().Be("10 +");

        tested.OperatorToken = InputAction.Minus;

        tested.CurrentExpression.Should().Be("10 -");

        tested.OperatorToken = InputAction.Multiply;

        tested.CurrentExpression.Should().Be("10 ×");

        tested.OperatorToken = InputAction.Divide;

        tested.CurrentExpression.Should().Be("10 ÷");
    }
}
