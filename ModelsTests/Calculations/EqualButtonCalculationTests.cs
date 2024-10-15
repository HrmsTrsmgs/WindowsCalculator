using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class EqualButtonCalculationTests
{
    readonly EqualButtonCalculation tested 
        = new(
            OperationCalculation.Create(
                new NumberCalculation(Calculation.NullObject, numberToken: new(10)),
                InputAction.Add,
                new(20)
            ),null);

    [Fact]
    public void 計算結果は直前にした計算と同じです()
    {
        tested.Result.Should().Be(30);
    }

    [Fact]
    public void 計算結果は繰り返し演算がある場合は繰り返した結果を取得します()
    {
        EqualButtonCalculation tested
        = new(
            this.tested,
            OperationCalculation.Create(
                Calculation.NullObject,
                InputAction.Add,
                new(20)
            ));
        tested.Result.Should().Be(50);
    }

    [Fact]
    public void 式は直前の式にイコールを付けたものです()
    {
        tested.Expression.Should().Be("10 + 20 =");
    }


    [Fact]
    public void 式は繰り返し演算がある場合は繰り返した結果に対する式を取得します()
    {
        EqualButtonCalculation tested
        = new(
            this.tested,
            OperationCalculation.Create(
                Calculation.NullObject,
                InputAction.Add,
                new(20)
            ));　
        tested.Expression.Should().Be("30 + 20 =");
    }
}
