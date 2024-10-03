using FluentAssertions;
using Marimo.MauiBlazor.Models;
using Marimo.MauiBlazor.Models.Tokens;

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
        tested.InputCharactor('1');
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
    }

    [Fact]
    public void 数字入力がトークンとして出力されます()
    {
        tested = new();
        tested.InputCharactor('1');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
        tested = new();
        tested.InputCharactor('2');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(2);
        tested = new();
        tested.InputCharactor('3');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(3);
        tested = new();
        tested.InputCharactor('4');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(4);        tested = new();
        tested = new();
        tested.InputCharactor('5');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(5);
        tested = new();
        tested.InputCharactor('6');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(6);
        tested = new();
        tested.InputCharactor('7');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(7);
        tested = new();
        tested.InputCharactor('8');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(8);
        tested = new();
        tested.InputCharactor('9');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(9);
        tested = new();
        tested.InputCharactor('0');
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(0);

    }


    [Fact]
    public void 数値や四則演算以外の値は受け付けません()
    {
        tested.InputCharactor('#');
        tested.ActiveToken.Should().BeNull();
    }
    [Fact]
    public void すでにトークンが入力された後についても数値や四則演算以外の値は受け付けません()
    {
        tested.InputCharactor('1');
        tested.InputCharactor('#');
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
    }

}
