using FluentAssertions;
using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.ViewModels;

namespace Marimo.WindowsCalculator.Tests.ViewModels;

public class CaluculatorViewModelTests
{
    CalculatorViewModel tested { get; } = new();

    [Fact]
    public void 初期状態で設定は取得できます()
    {
        tested.Settings.Should().NotBeNull();
    }

    [Fact]
    public void 入力した数値が表示されています()
    {
        tested.InputKeybord.Execute(InputAction.Five);

        tested.DisplaiedNumber.Should().Be("5");

    }

    [Fact]
    public void 入力した数値はカンマ区切りで表示されます()
    {
        tested.InputKeybord.Execute(InputAction.Five);
        tested.InputKeybord.Execute(InputAction.Five);
        tested.InputKeybord.Execute(InputAction.Five);
        tested.InputKeybord.Execute(InputAction.Five);
        tested.InputKeybord.Execute(InputAction.Five);
        tested.InputKeybord.Execute(InputAction.Five);
        tested.InputKeybord.Execute(InputAction.Five);

        tested.DisplaiedNumber.Should().Be("5,555,555");

    }

    [Fact]
    public void 計算式が表示されています()
    {
        tested.InputKeybord.Execute(InputAction.Five);
        tested.InputKeybord.Execute(InputAction.Add);
        tested.InputKeybord.Execute(InputAction.Three);
        tested.InputKeybord.Execute(InputAction.Equal);

        tested.Expression.Should().Be("5 + 3 =");
    }

    [Fact]
    public void 履歴が取得できます()
    {
        tested.InputKeybord.Execute(InputAction.Three);
        tested.InputKeybord.Execute(InputAction.Add);
        tested.InputKeybord.Execute(InputAction.Five);
        tested.InputKeybord.Execute(InputAction.Substract);
        tested.InputKeybord.Execute(InputAction.Seven);
        tested.InputKeybord.Execute(InputAction.Equal);

        var actual = tested.History.ToArray();

        actual.Should().HaveCount(2);
        actual[0].Should().Be(new CalculationHistoryItem("8　-　7 =", 1));
        actual[1].Should().Be(new CalculationHistoryItem("3　+　5 =", 8));
    }

    [Fact]
    public void 履歴が削除できます()
    {
        tested.InputKeybord.Execute(InputAction.Three);
        tested.InputKeybord.Execute(InputAction.Add);
        tested.InputKeybord.Execute(InputAction.Five);
        tested.InputKeybord.Execute(InputAction.Substract);
        tested.InputKeybord.Execute(InputAction.Seven);
        tested.InputKeybord.Execute(InputAction.Equal);

        tested.ClearHistoryCommand.Execute(null);

        tested.History.Should().BeEmpty();
    }

    [Fact]
    public void 履歴の削除は削除する履歴が残っているときに有効です()
    {
        tested.InputKeybord.Execute(InputAction.Three);
        tested.InputKeybord.Execute(InputAction.Add);
        tested.InputKeybord.Execute(InputAction.Five);
        tested.InputKeybord.Execute(InputAction.Substract);
        tested.InputKeybord.Execute(InputAction.Seven);
        tested.InputKeybord.Execute(InputAction.Equal);

        tested.ClearHistoryCommand.CanExecute(null).Should().BeTrue();

        tested.ClearHistoryCommand.Execute(null);

        tested.ClearHistoryCommand.CanExecute(null).Should().BeFalse();

        tested.History.Should().BeEmpty();
    }

    [Fact]
    public void InputOneCommandが反応します()
    {
        tested.InputOneCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("1");
    }

    [Fact]
    public void InputTwoCommandが反応します()
    {
        tested.InputTwoCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("2");
    }

    [Fact]
    public void InputThreeCommandが反応します()
    {
        tested.InputThreeCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("3");
    }

    [Fact]
    public void InputFourCommandが反応します()
    {
        tested.InputFourCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("4");
    }

    [Fact]
    public void InputFiveCommandが反応します()
    {
        tested.InputFiveCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("5");
    }

    [Fact]
    public void InputSixCommandが反応します()
    {
        tested.InputSixCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("6");
    }

    [Fact]
    public void InputSevenCommandが反応します()
    {
        tested.InputSevenCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("7");
    }

    [Fact]
    public void InputEightCommandが反応します()
    {
        tested.InputEightCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("8");
    }

    [Fact]
    public void InputNineCommandが反応します()
    {
        tested.InputNineCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("9");
    }

    [Fact]
    public void InputZeroCommandが反応します()
    {
        tested.InputZeroCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("0");
    }

    [Fact]
    public void InputPlusCommandが反応します()
    {
        tested.InputAddCommand.Execute(null);

        tested.Expression.Should().Be("0　+");
    }

    [Fact]
    public void InputMinusCommandが反応します()
    {
        tested.InputSubstractCommand.Execute(null);

        tested.Expression.Should().Be("0　-");
    }

    [Fact]
    public void InputMultiplyCommandが反応します()
    {
        tested.InputMultiplyCommand.Execute(null);

        tested.Expression.Should().Be("0　×");
    }

    [Fact]
    public void InputDivideCommandが反応します()
    {
        tested.InputDivideCommand.Execute(null);

        tested.Expression.Should().Be("0　÷");
    }

    [Fact]
    public void InputEqualCommandが反応します()
    {
        tested.InputSubstractCommand.Execute(null);
        tested.InputThreeCommand.Execute(null);
        tested.InputEqualCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("-3");
    }

    [Fact]
    public void 演算子を入れ替えても結果がエラーにはなりません()
    {
        tested.InputThreeCommand.Execute(null);
        tested.InputMultiplyCommand.Execute(null);
        tested.InputSubstractCommand.Execute(null);

        var action = () => tested.DisplaiedNumber;
        action.Should().NotThrow();
    }

    [Fact]
    public void InputUndoCommandが反応します()
    {
        tested.InputThreeCommand.Execute(null);
        tested.InputMultiplyCommand.Execute(null);
        tested.InputThreeCommand.Execute(null);
        tested.InputSubstractCommand.Execute(null);
        tested.InputThreeCommand.Execute(null);
        tested.InputEqualCommand.Execute(null);
        tested.InputUndoCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("6");
    }

    [Fact]
    public void InputRedoCommandが反応します()
    {
        tested.InputThreeCommand.Execute(null);
        tested.InputMultiplyCommand.Execute(null);
        tested.InputThreeCommand.Execute(null);
        tested.InputSubstractCommand.Execute(null);
        tested.InputThreeCommand.Execute(null);
        tested.InputEqualCommand.Execute(null);
        tested.InputUndoCommand.Execute(null);
        tested.InputRedoCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("9");
    }

        [Fact]
    public void InputCCommandが反応します()
    {
        tested.InputThreeCommand.Execute(null);
        tested.InputMultiplyCommand.Execute(null);
        tested.InputThreeCommand.Execute(null);
        tested.InputSubstractCommand.Execute(null);
        tested.InputThreeCommand.Execute(null);
        tested.InputEqualCommand.Execute(null);
        tested.InputCCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("0");
        tested.History.Should().BeEmpty();
    }

    [Fact]
    public void InputCECommandが反応します()
    {
        tested.InputThreeCommand.Execute(null);
        tested.InputMultiplyCommand.Execute(null);
        tested.InputThreeCommand.Execute(null);
        tested.InputEqualCommand.Execute(null);
        tested.InputCECommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("0");
        tested.History.Should().NotBeEmpty();
        tested.History.First().Expression.Should().Be("3　×　3 =");
    }

    [Fact]
    public void InputBackspaceCommandが反応します()
    {
        tested.InputThreeCommand.Execute(null);
        tested.InputThreeCommand.Execute(null);
        tested.InputThreeCommand.Execute(null);
        tested.InputBackspaceCommand.Execute(null);

        tested.DisplaiedNumber.Should().Be("33");
    }
}
