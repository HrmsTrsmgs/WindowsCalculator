using FluentAssertions;
using Marimo.MauiBlazor.Models;

namespace Marimo.MauiBlazor.Tests.Models;

public class NumberTokenTests
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

    [Fact]
    public void 桁が16桁を超える数値は指数表記になります()
    {
        tested.Number = 9999999999999999;
        tested.ToString().Should().Be("9999999999999999");
        tested.Number = 10000000000000000;
        tested.ToString().Should().Be("1E+16");
    }

    [Fact]
    public void 指数表記の数値部分の最大桁も16桁です()
    {
        tested.Number = 99999999999999990;
        tested.ToString().Should().Be("9.999999999999999E+16");
    }
}
