using FluentAssertions;
using Marimo.MauiBlazor.Models;
using Marimo.MauiBlazor.Models.Calculations;

namespace Marimo.MauiBlazor.Tests.Models;

public class CalculatorTest
{
    Calculator tested = new();

    [Fact]
    public void 初期状態の計算は数値演算子です()
    {
        tested.ActiveCaluculation.Should().BeOfType<NumberCalculation>();
        (tested.ActiveCaluculation as NumberCalculation)?.Number.Should().Be(0);
    }

    [Fact]
    public void 初期状態の計算は比演算子が0です()
    {
        tested.ActiveCaluculation.Should().BeOfType<NumberCalculation>();
        var calculation = (NumberCalculation)tested.ActiveCaluculation;
        calculation.Number.Should().Be(0);
    }

    [Fact]
    public void 初期状態の表示は0です()
    {
        tested.DisplaiedNumber.Should().Be(0);
    }

    [Fact]
    public void 入力された数値トークンの数値は結果となります()
    {
        tested.Input(new NumberToken(3));

        tested.DisplaiedNumber.Should().Be(3);
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
        tested.ActiveCaluculation.Should().BeOfType<OperationCalculation>();
        var calculation = (OperationCalculation)tested.ActiveCaluculation;
        calculation.Operator.Should().Be('+');
    }

    [Fact]
    public void 最初に数字が入力された場合に表示される数字はその数字になります()
    {
        tested.Input(new NumberToken(1));
        tested.DisplaiedNumber.Should().Be(1);
    }

    [Fact]
    public void 最初に数字が入力された後に演算子が入力されても表示される数字は変わりません()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken('+'));
        tested.DisplaiedNumber.Should().Be(1);
    }

    [Fact]
    public void 数字演算子数字と入力された状態で表示は二つ目の数字となっています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken('+'));
        tested.Input(new NumberToken(3));
        tested.DisplaiedNumber.Should().Be(3);
    }

    [Fact]
    public void 数字演算子数字演算子と入力された状態で表示は計算結果となっています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken('+'));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken('+'));
        tested.DisplaiedNumber.Should().Be(4);
    }

    [Fact]
    public void 足し算が実現されています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken('+'));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken('+'));
        tested.DisplaiedNumber.Should().Be(4);
    }

    [Fact]
    public void 引き算が実現されています()
    {
        tested.Input(new NumberToken(5));
        tested.Input(new OperatorToken('-'));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken('+'));
        tested.DisplaiedNumber.Should().Be(2);
    }

    [Fact]
    public void 掛け算が実現されています()
    {
        tested.Input(new NumberToken(5));
        tested.Input(new OperatorToken('*'));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken('+'));
        tested.DisplaiedNumber.Should().Be(15);
    }
}
