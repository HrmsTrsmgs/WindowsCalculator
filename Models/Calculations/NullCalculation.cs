namespace Marimo.WindowsCalculator.Models.Calculations;

public class NullCalculation : Calculation
{
    internal NullCalculation() : base(null!)
    {
    }

    public override Calculation Receiver => this;

    public override decimal? Result => 0;

    public override string Expression => "";
}
