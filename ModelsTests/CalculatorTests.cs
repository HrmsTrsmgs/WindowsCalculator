using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;

namespace Marimo.WindowsCalculator.Tests.Models;

public class CalculatorTests
{
    private ILog _logger;
    private MemoryAppender _memoryAppender;

    Calculator tested = new();
    public CalculatorTests()
    {
        _memoryAppender = new MemoryAppender
        {
            Layout = new PatternLayout("%-5p %m%n")
        };
        _memoryAppender.ActivateOptions();

        BasicConfigurator.Configure(_memoryAppender);
        _logger = LogManager.GetLogger(typeof(CalculatorTests));
    }

    [Fact]
    public void TestLogOutput()
    {
        _logger.Info("テストメッセージ");

        var logEvents = _memoryAppender.GetEvents().Should().NotBeEmpty();
    }

    [Fact]
    public void 初期状態の計算はNullオブジェクトです()
    {
        tested.ActiveCaluculation.Should().BeOfType<NullCalculation>();
    }

    [Fact]
    public void 初期状態の計算は結果が0です()
    {
        tested.ActiveCaluculation.Should().BeOfType<NullCalculation>();
        var calculation = (NullCalculation)tested.ActiveCaluculation;
        calculation.Result.Should().Be(0);
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
    public void 数値が入力しなおされた場合に現在の計算は変わりません()
    {
        tested.Input(new NumberToken(1));
        var initial = tested.ActiveCaluculation;

        tested.Input(new NumberToken(3));

        tested.ActiveCaluculation.Should().BeSameAs(initial);
    }

    [Fact]
    public void 演算子が入力された場合に現在の計算が変わります()
    {
        var initial = tested.ActiveCaluculation;

        tested.Input(new OperatorToken(InputAction.Plus));

        tested.ActiveCaluculation.Should().NotBeSameAs(initial);
    }

    [Fact]
    public void 演算子が入力された場合に新しい計算の演算子は指定したものになります()
    {
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.ActiveCaluculation.Should().BeOfType<OperationCalculation>();
        var calculation = (OperationCalculation)tested.ActiveCaluculation;
        calculation.OperatorToken.Should().Be(InputAction.Plus);
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
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.DisplaiedNumber.Should().Be("1");
    }

    [Fact]
    public void 数字演算子数字と入力された状態で表示は二つ目の数字となっています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(3));
        tested.DisplaiedNumber.Should().Be("3");
    }

    [Fact]
    public void 数字演算子数字演算子と入力された状態で表示は計算結果となっています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.DisplaiedNumber.Should().Be("4");
    }

    [Fact]
    public void 足し算が実現されています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.DisplaiedNumber.Should().Be("4");
    }

    [Fact]
    public void 引き算が実現されています()
    {
        tested.Input(new NumberToken(5));
        tested.Input(new OperatorToken(InputAction.Minus));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.DisplaiedNumber.Should().Be("2");
    }

    [Fact]
    public void 掛け算が実現されています()
    {
        tested.Input(new NumberToken(5));
        tested.Input(new OperatorToken(InputAction.Multiply));
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.DisplaiedNumber.Should().Be("15");
    }

    [Fact]
    public void 掛け算後の小数点桁数は5桁までです()
    {
        tested.Input(new NumberToken(5.12345m));
        tested.Input(new OperatorToken(InputAction.Multiply));
        tested.Input(new NumberToken(0.1m));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.DisplaiedNumber.Should().Be("0.51235");
    }

    [Fact]
    public void 割り算が実現されています()
    {
        tested.Input(new NumberToken(8));
        tested.Input(new OperatorToken(InputAction.Divide));
        tested.Input(new NumberToken(2));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.DisplaiedNumber.Should().Be("4");
    }

    [Fact]
    public void 演算子の入れ替えで計算は入れ替わりません()
    {
        tested.Input(new NumberToken(8));
        var number = tested.ActiveCaluculation;
        tested.Input(new OperatorToken(InputAction.Divide));
        var first = tested.ActiveCaluculation; ;
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.ActiveCaluculation.Should().BeSameAs(first);
        tested.ActiveCaluculation.Receiver.Should().BeSameAs(number);


    }

    [Fact]
    public void 割り算のゼロ除算でエラーが表示されます()
    {
        tested.Input(new NumberToken(8));
        tested.Input(new OperatorToken(InputAction.Divide));
        tested.Input(new NumberToken(0));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.DisplaiedNumber.Should().Be("0 で割ることはできません");
    }

    [Fact]
    public void イコールボタンを押すと表示は計算結果となっています()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(3));
        tested.Input(OtherToken.Equal);
        tested.DisplaiedNumber.Should().Be("4");
    }
    [Fact]
    public void イコールボタンを二度押すと前の結果が繰り返されます()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(3));
        tested.Input(OtherToken.Equal);
        tested.Input(OtherToken.Equal);
        tested.DisplaiedNumber.Should().Be("7");
    }
    [Fact]
    public void イコールボタンを3度以上押しても前の結果が繰り返されます()
    {
        tested.Input(new NumberToken(1));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(3));
        tested.Input(OtherToken.Equal);
        tested.Input(OtherToken.Equal);
        tested.Input(OtherToken.Equal);
        tested.DisplaiedNumber.Should().Be("10");
    }

    [Fact]
    public void 小数点を入力した状態が表示できます()
    {
        tested.Input(new NumberToken(1) { DecimalPlaces = 0 });
        tested.DisplaiedNumber.Should().Be("1.");
    }

    [Fact]
    public void CEで計算結果が削除されます()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(OtherToken.CE);
        tested.DisplaiedNumber.Should().Be("0");
    }

    [Fact]
    public void Undoで直前の計算が無効になります()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(OtherToken.Equal);
        tested.Input(OtherToken.Undo);
        tested.DisplaiedNumber.Should().Be("8");
    }

    [Fact]
    public void Undoで入力中の直前の計算が無効になります()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(OtherToken.Undo);
        tested.DisplaiedNumber.Should().Be("3");
    }

    [Fact]
    public void Undoで数値入力前の直前の計算が無効になります()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(OtherToken.Undo);
        tested.ActiveCaluculation.Should().BeOfType<NumberCalculation>();
        tested.DisplaiedNumber.Should().Be("3");
    }

    [Fact]
    public void Undoで未入力の状態まで戻れます()
    {
        tested.Input(new NumberToken(3));
        tested.Input(OtherToken.Undo);
        tested.ActiveCaluculation.Should().BeSameAs(Calculation.NullObject);
        tested.DisplaiedNumber.Should().Be("0");
    }

    [Fact]
    public void Undoで戻った場合は演算結果が表示されます()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(7));
        tested.Input(OtherToken.Undo);
        tested.DisplaiedNumber.Should().Be("8");
    }

    [Fact]
    public void Undoで戻った後で数字が変更された場合は数字表示に戻ります()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(7));
        tested.Input(OtherToken.Undo);
        tested.Input(new NumberToken(9));
        tested.DisplaiedNumber.Should().Be("9");
    }

    [Fact]
    public void Undoをする前にRedoしても何も起きません()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        var initial = tested.ActiveCaluculation;
        tested.Input(OtherToken.Redo);
        tested.ActiveCaluculation.Should().BeSameAs(initial);
    }

    [Fact]
    public void RedoでUndoが元に戻ります()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(OtherToken.Equal);
        tested.Input(OtherToken.Undo);
        tested.Input(OtherToken.Redo);
        tested.ActiveCaluculation.Should().BeOfType<EqualButtonCalculation>();
        tested.DisplaiedNumber.Should().Be("8");
    }

    [Fact]
    public void Redoで複数回のUndoは一つずつ元に戻ります()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(OtherToken.Equal);
        tested.Input(OtherToken.Undo);
        tested.Input(OtherToken.Undo);
        tested.Input(OtherToken.Redo);
        tested.ActiveCaluculation.Should().BeOfType<OperationCalculation>();
        tested.DisplaiedNumber.Should().Be("8");
        tested.Input(OtherToken.Redo);
        tested.ActiveCaluculation.Should().BeOfType<EqualButtonCalculation>();
        tested.DisplaiedNumber.Should().Be("8");
    }

    [Fact]
    public void Undo後のRedoで入力中の直前の計算が復帰します()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(OtherToken.Undo);
        tested.Input(OtherToken.Redo);
        tested.DisplaiedNumber.Should().Be("5");
    }

    [Fact]
    public void Undo後のRedoで数値入力前の直前の計算が復帰します()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(OtherToken.Undo);
        tested.Input(OtherToken.Redo);
        tested.ActiveCaluculation.Should().BeOfType<OperationCalculation>();
        tested.DisplaiedNumber.Should().Be("3");
        ((OperationCalculation)tested.ActiveCaluculation)
            .OperatorToken.Should().Be(InputAction.Plus);
    }

    [Fact]
    public void Undo後のRedoでで未入力の状態が復帰します()
    {
        tested.Input(new NumberToken(3));
        tested.Input(OtherToken.Undo);
        tested.Input(OtherToken.Redo);
        tested.ActiveCaluculation.Should().BeOfType<NumberCalculation>();
        tested.DisplaiedNumber.Should().Be("3");
    }

    [Fact]
    public void 履歴が出力できます()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(new OperatorToken(InputAction.Minus));
        tested.Input(new NumberToken(7));
        tested.Input(OtherToken.Equal);

        var actual = tested.CalculationHistory.ToArray();

        actual.Should().HaveCount(2);
        actual[0].Should().Be(new CalculationHistoryItem("8 - 7 =", 1));
        actual[1].Should().Be(new CalculationHistoryItem("3 + 5 =", 8));
    }

    [Fact]
    public void 履歴は削除することができます()
    {
        tested.Input(new NumberToken(3));
        tested.Input(new OperatorToken(InputAction.Plus));
        tested.Input(new NumberToken(5));
        tested.Input(new OperatorToken(InputAction.Minus));
        tested.Input(new NumberToken(7));
        tested.Input(OtherToken.Equal);

        tested.ClearCalculationHistory();

        tested.CalculationHistory.Should().BeEmpty();
    }
}

