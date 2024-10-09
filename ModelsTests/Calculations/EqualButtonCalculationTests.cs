﻿using Marimo.MauiBlazor.Models;
using Marimo.MauiBlazor.Models.Calculations;

namespace Marimo.MauiBlazor.Tests.Models.Calculations;

public class EqualButtonCalculationTests
{
    EqualButtonCalculation tested 
        = new(
            new OperationCalculation(
                new NumberCalculation(null) { NumberToken = new(10) },
                InputAction.Plus,
                new(20)
            ),null);

    [Fact]
    public void 計算結果は直前にした計算と同じです()
    {
        tested.Result.Should().Be(30);
    }

    [Fact]
    public void 計算結果は繰り返し演算がある場合は繰り返した結果を取得します()
    {
        EqualButtonCalculation tested
        = new(
            this.tested,
            new OperationCalculation(
                new NumberCalculation(null),
                InputAction.Plus,
                new(20)
            ));
        tested.Result.Should().Be(50);
    }

    [Fact]
    public void 式は直前の式にイコールを付けたものです()
    {
        tested.CurrentExpression.Should().Be("10 + 20 =");
    }


    [Fact]
    public void 式は繰り返し演算がある場合は繰り返した結果に対する式を取得します()
    {
        EqualButtonCalculation tested
        = new(
            this.tested,
            new OperationCalculation(
                new NumberCalculation(null),
                InputAction.Plus,
                new(20)
            ));
        tested.CurrentExpression.Should().Be("30 + 20 =");
    }
}