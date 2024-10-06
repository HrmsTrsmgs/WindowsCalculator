using FluentAssertions;
using Marimo.MauiBlazor.Models;

namespace Marimo.MauiBlazor.Tests.Models;

public class NumberTokenTest
{
    NumberToken tested = new(3);

    [Fact]
    public void Numberの初期値は指定したものです()
    {
        tested.Number.Should().Be(3);
    }

    [Fact]
    public void DecimalPlaceの初期値はnullです()
    {
        tested.DecimalPlaces.Should().BeNull();
    }
}
