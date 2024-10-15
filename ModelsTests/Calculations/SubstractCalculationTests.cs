using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;
using Marimo.WindowsCalculator.Models.Tokens;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class SupstractCaluclationTests
{


    OperationCalculation tested
        = OperationCalculation.Create(
            new NumberCalculation(
                Calculation.NullObject,
                new NumberToken(1)),
            InputAction.Substract,
                new NumberToken(3),
                true);

    [Fact]
    public void InputActionはSubstractです()
    {
        tested.OperatorAction.Should().Be(InputAction.Substract);
    }

    [Fact]
    public void 引き算を行います()
    {
        tested.Result.Should().Be(-2);
    }

    [Fact]
    public void 式表記で演算子はマイナス記号です()
    {
        tested.Expression.Should().Be("1　-");
    }
}
