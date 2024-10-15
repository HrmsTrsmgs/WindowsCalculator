using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;
using Marimo.WindowsCalculator.Models.Tokens;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class AddCaluclationTests
{


    OperationCalculation tested
        = OperationCalculation.Create(
            new NumberCalculation(
                Calculation.NullObject,
                new NumberToken(1)),
            InputAction.Add,
                new NumberToken(3),
                true);

    [Fact]
    public void InputActionはAddです()
    {
        tested.OperatorAction.Should().Be(InputAction.Add);
    }

    [Fact]
    public void 足し算を行います()
    {
        tested.Result.Should().Be(4);
    }

    [Fact]
    public void 式表記で演算子はプラス記号です()
    {
        tested.Expression.Should().Be("1　+");
    }
}
