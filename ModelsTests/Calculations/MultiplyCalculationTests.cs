using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;
using Marimo.WindowsCalculator.Models.Tokens;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class MultiplyCaluclationTests
{


    OperationCalculation tested
        = OperationCalculation.Create(
            new NumberCalculation(
                Calculation.NullObject,
                new NumberToken(2)),
            InputAction.Multiply,
                new NumberToken(3),
                true);

    [Fact]
    public void InputActionはMultiplyです()
    {
        tested.OperatorAction.Should().Be(InputAction.Multiply);
    }

    [Fact]
    public void 掛け算を行います()
    {
        tested.Result.Should().Be(6);
    }

    [Fact]
    public void 式表記で演算子は掛ける記号です()
    {
        tested.Expression.Should().Be("2　×");
    }
}
