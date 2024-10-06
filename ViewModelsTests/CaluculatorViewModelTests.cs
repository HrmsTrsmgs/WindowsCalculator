﻿using FluentAssertions;
using Marimo.MauiBlazor.Models;
using Marimo.MauiBlazor.ViewModels;

namespace Marimo.MauiBlazor.Tests.ViewModels;

public class CaluculatorViewModelTests
{
    CalculatorViewModel tested { get; } = new();

    [Fact]
    public void 入力した数値が表示されています()
    {
        tested.PushKeybord.Execute(Key.Five);

        tested.Number.Should().Be(5);

    }
}
