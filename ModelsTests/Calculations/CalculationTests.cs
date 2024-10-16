using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class CalculationTests
{
    Calculation error = OperationCalculation.Create(
            new NumberCalculation(
                Calculation.NullObject,
                new(3)),
            InputAction.Divide,
            new(0),
            true);

    OperationCalculation tested = OperationCalculation.Create(
        new NumberCalculation(
                Calculation.NullObject,
                new(3)),
        InputAction.Add,
        new(1));


    [Fact]
    public void エラーは計算するまでは出ていません()
    {
        error.DisplayError.Should().BeNull();
    }

    [Fact]
    public void ゼロ除算の計算をするとエラーが出ます()
    {
        error.Result.Should().BeNull();
        error.DisplayError.Should().NotBeNull();
        error.DisplayError.Should().Be("0 で割ることはできません");
    }


    [Fact]
    public void エラーの変更は通知されます()
    {
        var changedList = new List<string?>();
        error.PropertyChanged += (_, args) => changedList.Add(args.PropertyName);
        error.Result.Should().BeNull();

        changedList.Should().NotBeEmpty();
        changedList.Should().Contain(nameof(error.DisplayError));
    }


    [Fact]
    public void エラーの変更は計算先でも通知されます()
    {
        var tested = OperationCalculation.Create(
            error,
            InputAction.Add);
        error.DisplayError.Should().BeNull();
        tested.DisplayError.Should().BeNull();
        error.Result.Should().BeNull();
        error.DisplayError.Should().Be("0 で割ることはできません");
        tested.DisplayError.Should().Be("0 で割ることはできません");
    }

    [Fact]
    public void エラーの変更は計算先に伝播します()
    {
        var tested = OperationCalculation.Create(
            error,
            InputAction.Add);
        var changedList = new List<string?>();
        tested.PropertyChanged += (_, args) => changedList.Add(args.PropertyName);
        error.Result.Should().BeNull();

        changedList.Should().Contain(nameof(tested.DisplayError));

    }
    [Fact]
    public void 結果の計算は通知されます()
    {
        var changedList = new List<string?>();
        tested.PropertyChanged += (_, args) => changedList.Add(args.PropertyName);

        tested.IsDisplayResult = true;
        tested.Result.Should().Be(4);
        changedList.Should().Contain(nameof(tested.Result));
    }

    [Fact]
    public void 次の計算に読み込ま時に計算は行われません()
    {
        var changedList = new List<string?>();

        var next = OperationCalculation.Create(
            tested,
            InputAction.Multiply);

        tested.PropertyChanged += (_, args) => changedList.Add(args.PropertyName);

        changedList.Should().BeEmpty();
    }

    [Fact]
    public void 結果の変更の通知は次の計算も伝播されます()
    {
        var changedList = new List<string?>();

        var next = OperationCalculation.Create(
            tested,
            InputAction.Multiply);

        next.PropertyChanged += (_, args) => changedList.Add(args.PropertyName);
        tested.IsDisplayResult = true;
        changedList.Should().Contain(nameof(tested.Result));

    }

    [Fact]
    public void 結果の変更の通知は式にも伝播します()
    {
        var changedList = new List<string?>();
        tested.PropertyChanged += (_, args) => changedList.Add(args.PropertyName);

        tested.IsDisplayResult = true;
        tested.Result.Should().Be(4);
        changedList.Should().Contain(nameof(tested.Expression));
    }

    [Fact]
    public void 結果の変更は計算先にも通知されます()
    {
        var changedList = new List<string?>();
        tested.PropertyChanged += (_, args) => changedList.Add(args.PropertyName);

        tested.IsDisplayResult = true;
        tested.Result.Should().Be(4);
        changedList.Should().Contain(nameof(tested.Expression));
    }
}
