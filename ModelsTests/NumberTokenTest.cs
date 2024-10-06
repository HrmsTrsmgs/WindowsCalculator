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

    [Fact]
    public void DecimalPlaceに値を設定することができます()
    {
        tested.DecimalPlaces = 5;
        tested.DecimalPlaces.Should().Be(5);
    }

    [Fact]
    public void DecimalPlaceには5までの値しか設定することができません()
    {
        tested.DecimalPlaces = 6;
        tested.DecimalPlaces.Should().Be(5);
        tested.DecimalPlaces = 100;
        tested.DecimalPlaces.Should().Be(5);
    }
}
