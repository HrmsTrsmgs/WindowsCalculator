using FluentAssertions;
using Marimo.MauiBlazor.Models;

namespace Marimo.MauiBlazor.Tests.Models;

public class CalculatorTest
{
    Calculator tested = new();

    [Fact]
    public void 入力されたトークンは結果となります()
    {
        tested.Input(new NumberToken { Number = 3 });

        tested.Result.Should().Be(3);
    }
}
