using Marimo.WindowsCalculator.Models.Calculations;

namespace Marimo.WindowsCalculator.Tests.Models.Calculations;

public class NullCalculationTests
{
    Calculation tested = Calculation.NullObject;

    [Fact]
    public void Receiverは自身を返します()
    {
        tested.Receiver.Should().BeSameAs(tested);
    }

    [Fact]
    public void 結果は0です()
    {
        tested.Result.Should().Be(0);
    }
    [Fact]
    public void 式は空文字列です()
    {
        tested.Expression.Should().BeEmpty();
    }
        
}
