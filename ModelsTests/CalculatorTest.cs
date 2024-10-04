using FluentAssertions;
using Marimo.MauiBlazor.Models;

namespace Marimo.MauiBlazor.Tests.Models;

public class CalculatorTest
{
    Calculator tested = new();

    [Fact]
    public void 初期状態の計算は比演算子が0です()
    {
        tested.ActiveCaluculation.Operand.Should().Be(0);
    }

    [Fact]
    public void 初期状態の計算は演算子がnullです()
    {
        tested.ActiveCaluculation.Operator.Should().BeNull();
    }

    [Fact]
    public void 初期状態の表示は0です()
    {
        tested.Result.Should().Be(0);
    }

    [Fact]
    public void 入力されたトークンは結果となります()
    {
        tested.Input(new NumberToken { Number = 3 });

        tested.Result.Should().Be(3);
    }
}
