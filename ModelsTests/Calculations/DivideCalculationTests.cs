using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;
using FluentAssertions.Events;
using Marimo.WindowsCalculator.Models.Tokens;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class DivideCaluclationTests
{


    OperationCalculation tested
        = OperationCalculation.Create(
            new NumberCalculation(
                Calculation.NullObject,
                new NumberToken(3)),
            InputAction.Divide,
                new NumberToken(2),
                true);

    [Fact]
    public void InputActionはDivideです()
    {
        tested.OperatorAction.Should().Be(InputAction.Divide);
    }

    [Fact]
    public void 割り算を行います()
    {
        tested.Result.Should().Be(1.5m);
    }

    [Fact]
    public void 式表記で演算子は割る記号です()
    {
        tested.Expression.Should().Be("3　÷");
    }

    [Fact]
    public void エラーの初期値はnullです()
    {
        tested.DisplayError.Should().BeNull();
    }

    [Fact]
    public void Zeroで割り算を行うとエラーが設定されます()
    {
        tested.Expression.Should().Be("3　÷");

        tested
            = OperationCalculation.Create(
                new NumberCalculation(
                    Calculation.NullObject,
                    new NumberToken(3)),
                InputAction.Divide,
                    new NumberToken(0),
                    true);

        tested.DisplayError.Should().BeNull();
        tested.Result.Should().Be(decimal.MaxValue);
        tested.DisplayError.Should().Be("0 で割ることはできません");
    }

    [Fact]
    public void 被演算子は変更できます()
    {
        var expected = new NumberToken(1);
        tested.Operand = expected;

        tested.Operand.Should().Be(expected);
    }

    [Fact]
    public void 被演算子の変更は監視されます()
    {
        string? propertyName = null;
        tested.PropertyChanged += (_, e) => propertyName = e.PropertyName;

        var expected = new NumberToken(1);
        tested.Operand = expected;

        propertyName.Should().NotBeNull();
        propertyName.Should().Be(nameof(tested.Operand));
    }

    [Fact]
    public void 被演算子を変更することによりIsDisplayResultはfalseになります()
    {
        var expected = new NumberToken(1);
        tested.Operand = expected;

        tested.IsDisplayResult.Should().BeFalse();
    }
}
