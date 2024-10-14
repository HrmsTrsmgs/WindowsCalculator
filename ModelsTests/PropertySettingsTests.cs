using Marimo.WindowsCalculator.Models;

namespace Marimo.WindowsCalculator.Tests.Models;

public class PropertySettingsTests
{
    PropertySettings tested = new();

    string? changedProperty = null;
    public PropertySettingsTests()
    {
        tested.PropertyChanged += (sender, e) => changedProperty = e.PropertyName;
    }

    [Fact]
    public void OneDisplayTextの初期値は1です()
    {
        tested.OneDisplayText.Should().Be("1");
    }

    [Fact]
    public void OneDisplayTextは変更されたことを通知します()
    {
        tested.OneDisplayText = "A";
        changedProperty.Should().Be("OneDisplayText");
    }

    [Fact]
    public void TwoDisplayTextの初期値は2です()
    {
        tested.TwoDisplayText.Should().Be("2");
    }

    [Fact]
    public void TwoDisplayTextは変更されたことを通知します()
    {
        tested.TwoDisplayText = "A";
        changedProperty.Should().Be("TwoDisplayText");
    }

    [Fact]
    public void ThreeDisplayTextの初期値は4です()
    {
        tested.ThreeDisplayText.Should().Be("3");
    }

    [Fact]
    public void ThreeDisplayTextは変更されたことを通知します()
    {
        tested.FiveDisplayText = "A";
        changedProperty.Should().Be("FiveDisplayText");
    }

    [Fact]
    public void FourDisplayTextの初期値は4です()
    {
        tested.FourDisplayText.Should().Be("4");
    }

    [Fact]
    public void FourDisplayTextは変更されたことを通知します()
    {
        tested.FiveDisplayText = "A";
        changedProperty.Should().Be("FiveDisplayText");
    }

    [Fact]
    public void FiveDisplayTextの初期値は5です()
    {
        tested.FiveDisplayText.Should().Be("5");
    }

    [Fact]
    public void FiveDisplayTextは変更されたことを通知します()
    {
        tested.FiveDisplayText = "A";
        changedProperty.Should().Be("FiveDisplayText");
    }

    [Fact]
    public void SixDisplayTextの初期値は6です()
    {
        tested.SixDisplayText.Should().Be("6");
    }

    [Fact]
    public void SixDisplayTextは変更されたことを通知します()
    {
        tested.SixDisplayText = "A";
        changedProperty.Should().Be("SixDisplayText");
    }

    [Fact]
    public void SevenDisplayTextの初期値は7です()
    {
        tested.SevenDisplayText.Should().Be("7");
    }

    [Fact]
    public void SevenDisplayTextは変更されたことを通知します()
    {
        tested.SevenDisplayText = "A";
        changedProperty.Should().Be("SevenDisplayText");
    }

    [Fact]
    public void EightDisplayTextの初期値は8です()
    {
        tested.EightDisplayText.Should().Be("8");
    }

    [Fact]
    public void EightDisplayTextは変更されたことを通知します()
    {
        tested.EightDisplayText = "A";
        changedProperty.Should().Be("EightDisplayText");
    }

    [Fact]
    public void NineDisplayTextの初期値は9です()
    {
        tested.NineDisplayText.Should().Be("9");
    }

    [Fact]
    public void NineDisplayTextは変更されたことを通知します()
    {
        tested.NineDisplayText = "A";
        changedProperty.Should().Be("NineDisplayText");
    }

    [Fact]
    public void ZeroDisplayTextの初期値は0です()
    {
        tested.ZeroDisplayText.Should().Be("0");
    }

    [Fact]
    public void ZeroDisplayTextは変更されたことを通知します()
    {
        tested.ZeroDisplayText = "A";
        changedProperty.Should().Be("ZeroDisplayText");
    }

    [Fact]
    public void DotDisplayTextの初期値は小数点です()
    {
        tested.DotDisplayText.Should().Be(".");
    }

    [Fact]
    public void DotDisplayTextは変更されたことを通知します()
    {
        tested.DotDisplayText = "A";
        changedProperty.Should().Be("DotDisplayText");
    }

    [Fact]
    public void PlusDisplayTextの初期値はプラスです()
    {
        tested.AddDisplayText.Should().Be("+");
    }

    [Fact]
    public void PlusDisplayTextは変更されたことを通知します()
    {
        tested.AddDisplayText = "A";
        changedProperty.Should().Be("PlusDisplayText");
    }

    [Fact]
    public void MinusDisplayTextの初期値はマイナスです()
    {
        tested.SubstractDisplayText.Should().Be("-");
    }

    [Fact]
    public void MinusDisplayTextは変更されたことを通知します()
    {
        tested.SubstractDisplayText = "A";
        changedProperty.Should().Be("MinusDisplayText");
    }

    [Fact]
    public void MultiplyDisplayTextの初期値は掛ける記号です()
    {
        tested.MultiplyDisplayText.Should().Be("×");
    }

    [Fact]
    public void MultiplyDisplayTextは変更されたことを通知します()
    {
        tested.MultiplyDisplayText = "A";
        changedProperty.Should().Be("MultiplyDisplayText");
    }

    [Fact]
    public void DivideDisplayTextの初期値は割る記号です()
    {
        tested.DivideDisplayText.Should().Be("÷");
    }

    [Fact]
    public void DivideDisplayTextは変更されたことを通知します()
    {
        tested.DivideDisplayText = "A";
        changedProperty.Should().Be("DivideDisplayText");
    }

    [Fact]
    public void EqualDisplayTextの初期値はイコール記号です()
    {
        tested.EqualDisplayText.Should().Be("=");
    }

    [Fact]
    public void EqualDisplayTextは変更されたことを通知します()
    {
        tested.EqualDisplayText = "A";
        changedProperty.Should().Be("EqualDisplayText");
    }

    [Fact]
    public void CDisplayTextの初期値はCです()
    {
        tested.CDisplayText.Should().Be("C");
    }

    [Fact]
    public void CDisplayTextは変更されたことを通知します()
    {
        tested.CDisplayText = "A";
        changedProperty.Should().Be("CDisplayText");
    }

    [Fact]
    public void CEDisplayTextの初期値は割る記号です()
    {
        tested.CEDisplayText.Should().Be("CE");
    }

    [Fact]
    public void CEDisplayTextは変更されたことを通知します()
    {
        tested.CEDisplayText = "A";
        changedProperty.Should().Be("CEDisplayText");
    }

    [Fact]
    public void BackspaceDisplayTextの初期値はバックスペース記号です()
    {
        tested.BackspaceDisplayText.Should().Be("⌫");
    }

    [Fact]
    public void BackspaceDisplayTextは変更されたことを通知します()
    {
        tested.BackspaceDisplayText = "A";
        changedProperty.Should().Be("BackspaceDisplayText");
    }

}
