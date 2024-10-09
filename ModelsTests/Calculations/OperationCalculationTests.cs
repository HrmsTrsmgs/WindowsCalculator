using Marimo.MauiBlazor.Models;
using Marimo.MauiBlazor.Models.Calculations;

namespace Marimo.MauiBlazor.Tests.Models.Calculations;

public class OperationCalculationTests
{
    OperationCalculation tested { get; set; }
        = new(
            new NumberCalculation(null) { NumberToken = new(10) },
            Key.Plus,
            new(20)
            );

    [Fact]
    public void 計算結果が出力されます()
    {
        tested.Result.Should().Be(30);

        tested.OperatorToken = Key.Minus;

        tested.Result.Should().Be(-10);

        tested.OperatorToken = Key.Multiply;

        tested.Result.Should().Be(200);

        tested.OperatorToken = Key.Divide;

        tested.Result.Should().Be(0.5m);
    }

    [Fact]
    public void 式は計算途中の式が表示されます()
    {
        tested.CurrentExpression.Should().Be("10 +");

        tested.OperatorToken = Key.Minus;

        tested.CurrentExpression.Should().Be("10 -");

        tested.OperatorToken = Key.Multiply;

        tested.CurrentExpression.Should().Be("10 ×");

        tested.OperatorToken = Key.Divide;

        tested.CurrentExpression.Should().Be("10 ÷");
    }
}
