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
        tested.Input('1');
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
    }

    [Fact]
    public void 数字入力がトークンとして出力されます()
    {
        tested = new();
        tested.Input('1');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
        tested = new();
        tested.Input('2');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(2);
        tested = new();
        tested.Input('3');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(3);
        tested = new();
        tested.Input('4');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(4);        tested = new();
        tested = new();
        tested.Input('5');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(5);
        tested = new();
        tested.Input('6');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(6);
        tested = new();
        tested.Input('7');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(7);
        tested = new();
        tested.Input('8');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(8);
        tested = new();
        tested.Input('9');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(9);
        tested = new();
        tested.Input('0');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(0);

    }


    [Fact]
    public void 数値や四則演算以外の値は受け付けません()
    {
        tested.Input('#');
        tested.ActiveToken.Should().BeNull();
    }
    [Fact]
    public void すでにトークンが入力された後についても無効な値は受け付けません()
    {
        tested.Input('1');
        tested.Input('#');
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
    }

    [Fact]
    public void 演算子トークンが入力できます()
    {
        tested.Input('+');
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be('+');
    }

    [Fact]
    public void 四則演算の演算子トークンが入力できます()
    {
        tested.Input('+');
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be('+');
        tested.Input('-');
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be('-');
        tested.Input('*');
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be('*');
        tested.Input('/');
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be('/');
    }

    [Fact]
    public void 演算子の後に演算子が入力されてもトークンは同じです()
    {
        tested.Input('+');
        var initial = tested.ActiveToken;
        tested.Input('*');
        tested.ActiveToken.Should().BeSameAs(initial);
    }
    [Fact]
    public void 演算子の後に演算子が入力されると演算子は変わります()
    {
        tested.Input('+');
        var initial = tested.ActiveToken;
        tested.Input('*');
        tested.ActiveToken.Should().BeSameAs(initial);
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be('*');
    }
}
