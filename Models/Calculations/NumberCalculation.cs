using System.Net.NetworkInformation;

namespace Marimo.MauiBlazor.Models.Calculations;

public class NumberCalculation : Calculation
{
    public override Calculation? Receiver => null;

    public int Number { get; set; } = 0;
}
