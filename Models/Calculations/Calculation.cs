using CommunityToolkit.Mvvm.ComponentModel;

namespace Marimo.MauiBlazor.Models.Calculations;

public abstract class Calculation : ObservableObject
{
    public virtual Calculation? Receiver { get; set; } = null;
}
