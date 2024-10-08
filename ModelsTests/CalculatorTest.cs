using FluentAssertions;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using Marimo.MauiBlazor.Models;
using Marimo.MauiBlazor.Models.Calculations;

namespace Marimo.MauiBlazor.Tests.Models;

public class CalculatorTest
{
    private ILog _logger;
    private MemoryAppender _memoryAppender;

    Calculator tested = new();
    public CalculatorTest()
    {
        _memoryAppender = new MemoryAppender
        {
            Layout = new PatternLayout("%-5p %m%n")
        };
        _memoryAppender.ActivateOptions();

        BasicConfigurator.Configure(_memoryAppender);
        _logger = LogManager.GetLogger(typeof(CalculatorTest));
    }

    [Fact]
    public void TestLogOutput()
    {
        _logger.Info("テストメッセージ");

        var logEvents = _memoryAppender.GetEvents().Should().NotBeEmpty();
    }

    [Fact]
    public void 初期状態の計算は数値演算子です()
    {
        tested.ActiveCaluculation.Should().BeOfType<NumberCalculation>();
        (tested.ActiveCaluculation as NumberCalculation)?.NumberToken.Number.Should().Be(0);
    }

    [Fact]
    public void 初期状態の計算は比演算子が0です()
    {
        tested.ActiveCaluculation.Should().BeOfType<NumberCalculation>();
        var calculation = (NumberCalculation)tested.ActiveCaluculation;
        calculation.NumberToken.Number.Should().Be(0);
    }

    [Fact]
    public void 初期状態の表示は0です()
    {
        tested.DisplaiedNumber.Should().Be("0");
    }

    [Fact]
    public void 入力された数値トークンの数値は結果となります()
    {
        tested.Input(new NumberToken(3));

        tested.DisplaiedNumber.Should().Be("3");
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

        tested.Input(new OperatorToken(Key.Plus));

        tested.ActiveCaluculation.Should().NotBeSameAs(initial);
    }

    [Fact]
    public void 演算子が入力された場合に新しい計算の演算子は指定したものになります()
    {
        tested.Input(new OperatorToken(Key.Plus));
        tested.ActiveCaluculation.Should().BeOfType<OperationCalculation>();
        var calculation = (OperationCalculation)tested.ActiveCaluculation;
        calculation.OperatorToken.Should().Be(Key.Plus);
    }

    [Fact]
    public void 最初に数字が入力された場合に表示される数字はその数字になります()
    {
        tested.Input(new NumberToken(1));
        tested.DisplaiedNumber.Should().Be("1");
    }

    [Fact]
    public void 最初に数字が入力された後に演算子が入力されても表示される数字は変わりません()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(Key.Plus));
        tested.DisplaiedNumber.Should().Be("1");
    }

    [Fact]
    public void 数字演算子数字と入力された状態で表示は二つ目の数字となっています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(Key.Plus));
        tested.Input(new NumberToken(3));
        tested.DisplaiedNumber.Should().Be("3");
    }

    [Fact]
    public void 数字演算子数字演算子と入力された状態で表示は計算結果となっています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(Key.Plus));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(Key.Plus));
        tested.DisplaiedNumber.Should().Be("4");
    }

    [Fact]
    public void 足し算が実現されています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(Key.Plus));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(Key.Plus));
        tested.DisplaiedNumber.Should().Be("4");
    }

    [Fact]
    public void 引き算が実現されています()
    {
        tested.Input(new NumberToken(5));
        tested.Input(new OperatorToken(Key.Minus));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(Key.Plus));
        tested.DisplaiedNumber.Should().Be("2");
    }

    [Fact]
    public void 掛け算が実現されています()
    {
        tested.Input(new NumberToken(5));
        tested.Input(new OperatorToken(Key.Multiply));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(Key.Plus));
        tested.DisplaiedNumber.Should().Be("15");
    }

    [Fact]
    public void 割り算が実現されています()
    {
        tested.Input(new NumberToken(8));
        tested.Input(new OperatorToken(Key.Divide));
        tested.Input(new NumberToken(2));
        tested.Input(new OperatorToken(Key.Plus));
        tested.DisplaiedNumber.Should().Be("4");
    }

    [Fact]
    public void 割り算のゼロ除算でエラーが表示されます()
    {
        tested.Input(new NumberToken(8));
        tested.Input(new OperatorToken(Key.Divide));
        tested.Input(new NumberToken(0));
        tested.Input(new OperatorToken(Key.Plus));
        tested.DisplaiedNumber.Should().Be("0 で割ることはできません");
    }

    [Fact]
    public void イコールボタンを押すと表示は計算結果となっています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(Key.Plus));
        tested.Input(new NumberToken(3));
        tested.Input(OtherToken.Equal);
        tested.DisplaiedNumber.Should().Be("4");
    }
    [Fact]
    public void イコールボタンを二度押すと前の結果が繰り返されます()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(Key.Plus));
        tested.Input(new NumberToken(3));
        tested.Input(OtherToken.Equal);
        tested.Input(OtherToken.Equal);
        tested.DisplaiedNumber.Should().Be("7");
    }
    [Fact]
    public void イコールボタンを3度以上押しても前の結果が繰り返されます()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(Key.Plus));
        tested.Input(new NumberToken(3));
        tested.Input(OtherToken.Equal);
        tested.Input(OtherToken.Equal);
        tested.Input(OtherToken.Equal);
        tested.DisplaiedNumber.Should().Be("10");
    }

    [Fact]
    public void 小数点を押した状態が表示できます()
    {
        tested.Input(new NumberToken(1) { DecimalPlaces = 0 });
        tested.DisplaiedNumber.Should().Be("1.");
    }

    [Fact]
    public void Deleteで計算結果が削除されます()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(Key.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(OtherToken.Delete);
        tested.DisplaiedNumber.Should().Be("0");
    }

    [Fact]
    public void Undoで直前の計算が無効になります()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(Key.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(OtherToken.Equal);
        tested.Input(OtherToken.Undo);
        tested.DisplaiedNumber.Should().Be("5");
    }

    [Fact]
    public void Undoで入力中の直前の計算が無効になります()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(Key.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(OtherToken.Undo);
        tested.DisplaiedNumber.Should().Be("3");
    }

    [Fact]
    public void Undoで数値入力前の直前の計算が無効になります()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(Key.Plus));
        tested.Input(OtherToken.Undo);
        tested.ActiveCaluculation.Should().BeOfType<NumberCalculation>();
        tested.DisplaiedNumber.Should().Be("3");
    }

    [Fact]
    public void Undoで未入力の状態まで戻れます()
    {
        tested.Input(new NumberToken(3));
        tested.Input(OtherToken.Undo);
        tested.ActiveCaluculation.Should().BeNull();
        tested.DisplaiedNumber.Should().Be("0");
    }
}

