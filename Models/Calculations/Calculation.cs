using CommunityToolkit.Mvvm.ComponentModel;

namespace Marimo.MauiBlazor.Models.Calculations;

public abstract class Calculation(Calculation? receiver) : ObservableObject
{
    public virtual Calculation? Receiver { get; set; } = receiver;

    public abstract int? Result { get; }
}
