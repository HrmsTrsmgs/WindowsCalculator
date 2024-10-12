using FluentAssertions;
using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.ViewModels;

namespace Marimo.WindowsCalculator.Tests.ViewModels;

public class CaluculatorViewModelTests
{
    CalculatorViewModel tested { get; } = new();

    [Fact]
    public void 入力した数値が表示されています()
    {
        tested.PushKeybord.Execute(InputAction.Five);

        tested.DisplaiedNumber.Should().Be("5");

    }

    [Fact]
    public void 計算式が表示されています()
    {
        tested.PushKeybord.Execute(InputAction.Five);
        tested.PushKeybord.Execute(InputAction.Plus);
        tested.PushKeybord.Execute(InputAction.Three);
        tested.PushKeybord.Execute(InputAction.Equal);

        tested.Expression.Should().Be("5 + 3 =");
    }

    [Fact]
    public void 履歴が取得できます()
    {
        tested.PushKeybord.Execute(InputAction.Three);
        tested.PushKeybord.Execute(InputAction.Plus);
        tested.PushKeybord.Execute(InputAction.Five);
        tested.PushKeybord.Execute(InputAction.Minus);
        tested.PushKeybord.Execute(InputAction.Seven);
        tested.PushKeybord.Execute(InputAction.Equal);

        var actual = tested.History.ToArray();

        actual.Should().HaveCount(2);
        actual[0].Should().Be(new CalculationHistoryItem("8 - 7 =", 1));
        actual[1].Should().Be(new CalculationHistoryItem("3 + 5 =", 8));
    }

    [Fact]
    public void 履歴が削除できます()
    {
        tested.PushKeybord.Execute(InputAction.Three);
        tested.PushKeybord.Execute(InputAction.Plus);
        tested.PushKeybord.Execute(InputAction.Five);
        tested.PushKeybord.Execute(InputAction.Minus);
        tested.PushKeybord.Execute(InputAction.Seven);
        tested.PushKeybord.Execute(InputAction.Equal);

        tested.ClearHistoryCommand.Execute(null);

        tested.History.Should().BeEmpty();
    }
}
