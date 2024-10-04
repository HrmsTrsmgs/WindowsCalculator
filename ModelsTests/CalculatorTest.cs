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
    public void 入力された数値トークンの数値は結果となります()
    {
        tested.Input(new NumberToken(3));

        tested.Result.Should().Be(3);
    }

    [Fact]
    public void 初期状態から数値が入力された場合に現在の計算は変わりません()
    {
        var initial = tested.ActiveCaluculation;

        tested.Input(new NumberToken(3));

        tested.ActiveCaluculation.Should().BeSameAs(initial);
    }

    [Fact]
    public void 演算子が入力された場合に現在の計算が変わります()
    {
        var initial = tested.ActiveCaluculation;

        tested.Input(new OperatorToken('+'));

        tested.ActiveCaluculation.Should().NotBeSameAs(initial);
    }

    [Fact]
    public void 演算子が入力された場合に新しい計算の演算子は指定したものになります()
    {
        tested.Input(new OperatorToken('+'));

        tested.ActiveCaluculation.Operator.Should().Be('+');
    }

}
