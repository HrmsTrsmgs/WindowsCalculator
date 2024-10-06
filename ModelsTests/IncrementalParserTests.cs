using FluentAssertions;
using Marimo.MauiBlazor.Models;

namespace Marimo.MauiBlazor.Tests.Models;

public class IncrementalParserTests
{
    public IncrementalParser tested = new();

    [Fact]
    public void ActiveTokenの初期値はnullです()
    {
        tested.ActiveToken.Should().BeNull();
    }

    [Fact]
    public void 最初の数字入力がトークンとして出力されます()
    {
        tested.Input(Key.One);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
    }

    [Fact]
    public void 数字入力がトークンとして出力されます()
    {
        tested = new();
        tested.Input(Key.One);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
        tested = new();
        tested.Input(Key.Two);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(2);
        tested = new();
        tested.Input(Key.Three);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(3);
        tested = new();
        tested.Input(Key.Four);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(4);        tested = new();
        tested = new();
        tested.Input(Key.Five);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(5);
        tested = new();
        tested.Input(Key.Six);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(6);
        tested = new();
        tested.Input(Key.Seven);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(7);
        tested = new();
        tested.Input(Key.Eight);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(8);
        tested = new();
        tested.Input(Key.Nine);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(9);
        tested = new();
        tested.Input(Key.Zero);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(0);

    }


    [Fact]
    public void 数値や四則演算以外の値は受け付けません()
    {
        tested.Input(Key.Undo);
        tested.ActiveToken.Should().BeNull();
    }
    [Fact]
    public void すでにトークンが入力された後についても無効な値は受け付けません()
    {
        tested.Input(Key.One);
        tested.Input(Key.Undo);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
    }

    [Fact]
    public void 演算子トークンが入力できます()
    {
        tested.Input(Key.Plus);
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(Key.Plus);
    }

    [Fact]
    public void 四則演算の演算子トークンが入力できます()
    {
        tested.Input(Key.Plus);
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(Key.Plus);
        tested.Input(Key.Minus);
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(Key.Minus);
        tested.Input(Key.Multiply);
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(Key.Multiply);
        tested.Input(Key.Divide );
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(Key.Divide  );
    }

    [Fact]
    public void 演算子の後に演算子が入力されてもトークンは同じです()
    {
        tested.Input(Key.Plus);
        var initial = tested.ActiveToken;
        tested.Input(Key.Multiply);
        tested.ActiveToken.Should().BeSameAs(initial);
    }
    [Fact]
    public void 演算子の後に演算子が入力されると演算子は変わります()
    {
        tested.Input(Key.Plus);
        var initial = tested.ActiveToken;
        tested.Input(Key.Multiply);
        tested.ActiveToken.Should().BeSameAs(initial);
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(Key.Multiply);
    }

    [Fact]
    public void 演算子の後に数字が入力されると数字トークンが付け足されます()
    {
        tested.Input(Key.Plus);
        tested.Input(Key.One);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
    }

    [Fact]
    public void イコールを受け付けその他トークンが出力されます()
    {
        tested.Input(Key.Equal);
        tested.ActiveToken.Should().BeSameAs(OtherToken.Equal);
    }

    [Fact]
    public void 二桁の数字が入力できます()
    {
        tested.Input(Key.One);
        tested.Input(Key.Zero);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(10);
    }
}
