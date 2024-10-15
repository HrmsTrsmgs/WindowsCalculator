using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;
using System.Net.Http.Headers;

namespace Marimo.WindowsCalculator.Tests.Models;

public class CalculationHistoryItemTests
{
    readonly OperationCalculation operation = OperationCalculation.Create(
        new NumberCalculation(Calculation.NullObject, new(10)),
        InputAction.Add,
        new(20),
        true);

    readonly CalculationHistoryItem tested;

    public CalculationHistoryItemTests()
    {
        tested = new(operation);
    }

    [Fact]
    public void Expressionの初期値はWindows電卓と同じです()
    {
        tested.Expression.Should().Be("10　+　20 =");
    }

    [Fact]
    public void Resultの初期値はoperationのものです()
    {
        tested.Result.Should().Be(operation.Result);
    }

    [Fact]
    public void Expressionはoperationを変更することで追随します()
    {
        operation.Operand = new(30);
        tested.Expression.Should().Be("10　+　30 =");
    }

    [Fact]
    public void Resultはoperationを変更することで追随します()
    {
        operation.Operand = new(30);
        operation.IsDisplayResult = true;
        tested.Result.Should().Be(40);
    }
}
