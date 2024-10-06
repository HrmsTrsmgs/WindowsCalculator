using System.Net.NetworkInformation;

namespace Marimo.MauiBlazor.Models.Calculations;

public class NumberCalculation : Calculation
{
    public NumberCalculation(Calculation? receiver) : base(receiver)
    {

    }
    public override Calculation? Receiver => null;

    public int Number { get; set; } = 0;

    public override int? Result => Number;
}
