using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.Models.Calculations;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class OperationCalculationTests
{
    OperationCalculation Tested { get; set; }
        = OperationCalculation.Create(
            new NumberCalculation(Calculation.NullObject, new(10) ),
            InputAction.Add,
            new(20),
            true);


    [Fact]
    public void 式は計算途中の式が表示されます()
    {
        Tested.Expression.Should().Be("10　+");
    }
}
