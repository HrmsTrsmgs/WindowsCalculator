using log4net.Appender;
using Marimo.WindowsCalculator.Models;

namespace Marimo.WindowsCalculator.Tests.Models;

public class IncrementalParserTest : TestBase
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
        tested.Input(InputAction.One);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
    }

    [Fact]
    public void 入力時にログが出力されます()
    {
        tested.Input(InputAction.One);

        MemoryAppender.GetEvents().Should().HaveCount(1);
        MemoryAppender.GetEvents().First()
            .RenderedMessage.Should().Be("Oneが入力されました。");

    }

    [Fact]
    public void 数字入力がトークンとして出力されます()
    {
        tested = new();
        tested.Input(InputAction.One);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
        tested = new();
        tested.Input(InputAction.Two);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(2);
        tested = new();
        tested.Input(InputAction.Three);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(3);
        tested = new();
        tested.Input(InputAction.Four);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(4);        tested = new();
        tested = new();
        tested.Input(InputAction.Five);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(5);
        tested = new();
        tested.Input(InputAction.Six);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(6);
        tested = new();
        tested.Input(InputAction.Seven);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(7);
        tested = new();
        tested.Input(InputAction.Eight);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(8);
        tested = new();
        tested.Input(InputAction.Nine);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(9);
        tested = new();
        tested.Input(InputAction.Zero);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(0);

    }

    [Fact]
    public void 演算子トークンが入力できます()
    {
        tested.Input(InputAction.Add);
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(InputAction.Add);
    }

    [Fact]
    public void 四則演算の演算子トークンが入力できます()
    {
        tested.Input(InputAction.Add);
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(InputAction.Add);
        tested.Input(InputAction.Substract);
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(InputAction.Substract);
        tested.Input(InputAction.Multiply);
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(InputAction.Multiply);
        tested.Input(InputAction.Divide );
        tested.ActiveToken.Should().BeAssignableTo<OperatorToken>();
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(InputAction.Divide  );
    }

    [Fact]
    public void 演算子の後に演算子が入力されてもトークンは同じです()
    {
        tested.Input(InputAction.Add);
        var initial = tested.ActiveToken;
        tested.Input(InputAction.Multiply);
        tested.ActiveToken.Should().BeSameAs(initial);
    }
    [Fact]
    public void 演算子の後に演算子が入力されると演算子は変わります()
    {
        tested.Input(InputAction.Add);
        var initial = tested.ActiveToken;
        tested.Input(InputAction.Multiply);
        tested.ActiveToken.Should().BeSameAs(initial);
        (tested.ActiveToken as OperatorToken)?.Operator.Should().Be(InputAction.Multiply);
    }

    [Fact]
    public void 演算子の後に数字が入力されると数字トークンが付け足されます()
    {
        tested.Input(InputAction.Add);
        tested.Input(InputAction.One);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
    }

    [Fact]
    public void イコールを受け付けその他トークンが出力されます()
    {
        tested.Input(InputAction.Equal);
        tested.ActiveToken.Should().BeSameAs(OtherToken.Equal);
    }

    [Fact]
    public void 二桁の数字が入力できます()
    {
        tested.Input(InputAction.One);
        tested.Input(InputAction.Zero);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(10);
    }

    [Fact]
    public void 小数点の入力をするまではトークンの小数点以下桁数はnullです()
    {
        tested.Input(InputAction.One);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
        (tested.ActiveToken as NumberToken)?.DecimalPlaces.Should().BeNull();
    }
    [Fact]
    public void 小数点が入力できます()
    {
        tested.Input(InputAction.One);
        tested.Input(InputAction.Dot);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
        (tested.ActiveToken as NumberToken)?.DecimalPlaces.Should().Be(0);
    }

    [Fact]
    public void 小数点を何回入力しても特に変わりはありません()
    {
        tested.Input(InputAction.One);
        tested.Input(InputAction.Dot);
        tested.Input(InputAction.Dot);
        tested.Input(InputAction.Dot);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1);
        (tested.ActiveToken as NumberToken)?.DecimalPlaces.Should().Be(0);
    }

    [Fact]
    public void 小数点に続いて小数を入力することができます()
    {
        tested.Input(InputAction.One);
        tested.Input(InputAction.Dot);
        tested.Input(InputAction.Two);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1.2m);
    }

    [Fact]
    public void 小数点に続いて小数を5桁入力することができます()
    {
        tested.Input(InputAction.One);
        tested.Input(InputAction.Dot);
        tested.Input(InputAction.Two);
        tested.Input(InputAction.Three);
        tested.Input(InputAction.Four);
        tested.Input(InputAction.Five);
        tested.Input(InputAction.Six);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1.23456m);
    }

    [Fact]
    public void 小数は5桁までしか入力することはできません()
    {
        tested.Input(InputAction.One);
        tested.Input(InputAction.Dot);
        tested.Input(InputAction.Two);
        tested.Input(InputAction.Three);
        tested.Input(InputAction.Four);
        tested.Input(InputAction.Five);
        tested.Input(InputAction.Six);
        tested.Input(InputAction.Seven);
        tested.Input(InputAction.Eight);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1.23456m);
    }

    [Fact]
    public void 整数は16桁入力することができます()
    {
        tested.Input(InputAction.One);
        tested.Input(InputAction.Two);
        tested.Input(InputAction.Three);
        tested.Input(InputAction.Four);
        tested.Input(InputAction.Five);
        tested.Input(InputAction.Six);
        tested.Input(InputAction.Seven);
        tested.Input(InputAction.Eight);
        tested.Input(InputAction.Nine);
        tested.Input(InputAction.Zero);
        tested.Input(InputAction.One);
        tested.Input(InputAction.Two);
        tested.Input(InputAction.Three);
        tested.Input(InputAction.Four);
        tested.Input(InputAction.Five);
        tested.Input(InputAction.Six);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1234567890123456m);
    }

    [Fact]
    public void 整数は16桁までしか入力することはできません()
    {

        tested.Input(InputAction.One);
        tested.Input(InputAction.Two);
        tested.Input(InputAction.Three);
        tested.Input(InputAction.Four);
        tested.Input(InputAction.Five);
        tested.Input(InputAction.Six);
        tested.Input(InputAction.Seven);
        tested.Input(InputAction.Eight);
        tested.Input(InputAction.Nine);
        tested.Input(InputAction.Zero);
        tested.Input(InputAction.One);
        tested.Input(InputAction.Two);
        tested.Input(InputAction.Three);
        tested.Input(InputAction.Four);
        tested.Input(InputAction.Five);
        tested.Input(InputAction.Six);
        tested.Input(InputAction.Seven);
        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1234567890123456m);
    }

    [Fact]
    public void 二文字以上書いた分をDeleteで削除できます()
    {

        tested.Input(InputAction.One);
        tested.Input(InputAction.Six);
        tested.Input(InputAction.Seven);
        tested.Input(InputAction.CE);

        tested.ActiveToken.Should().BeAssignableTo<OtherToken>();
        (tested.ActiveToken as OtherToken).Should().Be(OtherToken.CE);
    }

    [Fact]
    public void 一文字書いた分をBackspaceで削除できます()
    {

        tested.Input(InputAction.One);
        tested.Input(InputAction.Backspace);

        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(0);
    }

    [Fact]
    public void 二文字以上書いた分をBackspaceで削除できます()
    {

        tested.Input(InputAction.One);
        tested.Input(InputAction.Six);
        tested.Input(InputAction.Seven);
        tested.Input(InputAction.Backspace);

        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(16);
    }

    [Fact]
    public void 小数点のみをBackspaceで削除できます()
    {

        tested.Input(InputAction.One);
        tested.Input(InputAction.Dot);
        tested.Input(InputAction.Backspace);

        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.DecimalPlaces.Should().BeNull();
            ;
    }

    [Fact]
    public void 小数点以下の数字をBackspaceで削除できます()
    {

        tested.Input(InputAction.One);
        tested.Input(InputAction.Dot);
        tested.Input(InputAction.Two);
        tested.Input(InputAction.Three);
        tested.Input(InputAction.Backspace);

        tested.ActiveToken.Should().BeAssignableTo<NumberToken>();
        (tested.ActiveToken as NumberToken)?.DecimalPlaces.Should().Be(1);
        (tested.ActiveToken as NumberToken)?.Number.Should().Be(1.2m);
        ;
    }

    [Fact]
    public void Cを受け付けその他トークンが出力されます()
    {
        tested.Input(InputAction.C);
        tested.ActiveToken.Should().BeSameAs(OtherToken.C);
    }

    [Fact]
    public void Undoを受け付けその他トークンが出力されます()
    {
        tested.Input(InputAction.Undo);
        tested.ActiveToken.Should().BeSameAs(OtherToken.Undo);
    }
}