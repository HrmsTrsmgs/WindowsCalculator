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
}
