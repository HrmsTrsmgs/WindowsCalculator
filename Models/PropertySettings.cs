namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// テンキーに表示する文字を管理します。
/// </summary>
public class PropertySettings : ModelBase
{
    

    ApplicationTheme theme = ApplicationTheme.System;

    public ApplicationTheme Theme
    {
        get => theme;
        set => SetProperty(ref theme, value);
    }


    /// <summary>
    /// 1のキーのテキストです。
    /// </summary>
    string oneDisplayText = "1";

    /// <summary>
    /// 1のキーのテキストを取得、設定します。
    /// </summary>
    public string OneDisplayText
    {
        get => oneDisplayText;
        set => SetProperty(ref oneDisplayText, value);
    }

    /// <summary>
    /// 2のキーのテキストです。
    /// </summary>
    string twoDisplayText = "2";

    /// <summary>
    /// 2のキーのテキストを取得、設定します。
    /// </summary>
    public string TwoDisplayText
    {
        get => twoDisplayText;
        set => SetProperty(ref twoDisplayText, value);
    }

    /// <summary>
    /// 3のキーのテキストです。
    /// </summary>
    string threeDisplayText = "3";

    /// <summary>
    /// 3のキーのテキストを取得、設定します。
    /// </summary>
    public string ThreeDisplayText
    {
        get => threeDisplayText;
        set => SetProperty(ref threeDisplayText, value);
    }

    /// <summary>
    /// 4のキーのテキストです。
    /// </summary>
    string fourDisplayText = "4";

    /// <summary>
    /// 4のキーのテキストを取得、設定します。
    /// </summary>
    public string FourDisplayText
    {
        get => fourDisplayText;
        set => SetProperty(ref fourDisplayText, value);
    }
    /// <summary>
    /// 5のキーのテキストです。
    /// </summary>
    string fiveDisplayText = "5";

    /// <summary>
    /// 5のキーのテキストを取得、設定します。
    /// </summary>
    public string FiveDisplayText
    {
        get => fiveDisplayText;
        set => SetProperty(ref fiveDisplayText, value);
    }

    /// <summary>
    /// 6のキーのテキストです。
    /// </summary>
    string sixDisplayText = "6";

    /// <summary>
    /// 6のキーのテキストを取得、設定します。
    /// </summary>
    public string SixDisplayText
    {
        get => sixDisplayText;
        set => SetProperty(ref sixDisplayText, value);
    }

    /// <summary>
    /// 7のキーのテキストです。
    /// </summary>
    string sevenDisplayText = "7";

    /// <summary>
    /// 7のキーのテキストを取得、設定します。
    /// </summary>
    public string SevenDisplayText
    {
        get => sevenDisplayText;
        set => SetProperty(ref sevenDisplayText, value);
    }
    /// <summary>
    /// 8のキーのテキストです。
    /// </summary>
    string eightDisplayText = "8";

    /// <summary>
    /// 8のキーのテキストを取得、設定します。
    /// </summary>
    public string EightDisplayText
    {
        get => eightDisplayText;
        set => SetProperty(ref eightDisplayText, value);
    }
    /// <summary>
    /// 9のキーのテキストです。
    /// </summary>
    string nineDisplayText = "9";

    /// <summary>
    /// 9のキーのテキストを取得、設定します。
    /// </summary>
    public string NineDisplayText
    {
        get => nineDisplayText;
        set => SetProperty(ref nineDisplayText, value);
    }
/// <summary>
/// 0のキーのテキストです。
/// </summary>
    string zeroDisplayText = "0";

    /// <summary>
    /// 0のキーのテキストを取得、設定します。
    /// </summary>
    public string ZeroDisplayText
    {
        get => zeroDisplayText;
        set => SetProperty(ref zeroDisplayText, value);
    }
    /// <summary>
    /// .のキーのテキストです。
    /// </summary>
    string dotDisplayText = ".";

    /// <summary>
    /// .のキーのテキストを取得、設定します。
    /// </summary>
    public string DotDisplayText
    {
        get => dotDisplayText;
        set => SetProperty(ref dotDisplayText, value);
    }
    /// <summary>
    /// +のキーのテキストです。
    /// </summary>
    string plusDisplayText = "+";

    /// <summary>
    /// +のキーのテキストを取得、設定します。
    /// </summary>
    public string AddDisplayText
    {
        get => plusDisplayText;
        set => SetProperty(ref plusDisplayText, value);
    }
    /// <summary>
    /// -のキーのテキストです。
    /// </summary>
    string minusDisplayText = "-";

    /// <summary>
    /// -のキーのテキストを取得、設定します。
    /// </summary>
    public string SubstractDisplayText
    {
        get => minusDisplayText;
        set => SetProperty(ref minusDisplayText, value);
    }
    /// <summary>
    /// ×のキーのテキストです。
    /// </summary>
    string multiplyDisplayText = "×";

    /// <summary>
    /// ×のキーのテキストを取得、設定します。
    /// </summary>
    public string MultiplyDisplayText
    {
        get => multiplyDisplayText;
        set => SetProperty(ref multiplyDisplayText, value);
    }
    /// <summary>
    /// ÷のキーのテキストです。
    /// </summary>
    string divideDisplayText = "÷";

    /// <summary>
    /// ÷のキーのテキストを取得、設定します。
    /// </summary>
    public string DivideDisplayText
    {
        get => divideDisplayText;
        set => SetProperty(ref divideDisplayText, value);
    }
    /// <summary>
    /// =のキーのテキストです。
    /// </summary>
    string equalDisplayText = "=";

    /// <summary>
    /// ＝のキーのテキストを取得、設定します。
    /// </summary>
    public string EqualDisplayText
    {
        get => equalDisplayText;
        set => SetProperty(ref equalDisplayText, value);
    }
    /// <summary>
    /// Cのキーのテキストです。
    /// </summary>
    string cDisplayText = "C";

    /// <summary>
    /// Cのキーのテキストを取得、設定します。
    /// </summary>
    public string CDisplayText
    {
        get => cDisplayText;
        set => SetProperty(ref cDisplayText, value);
    }
    /// <summary>
    /// CEのキーのテキストです。
    /// </summary>
    string ceDisplayText = "CE";

    /// <summary>
    /// ÷のキーのテキストを取得、設定します。
    /// </summary>
    public string CEDisplayText
    {
        get => ceDisplayText;
        set => SetProperty(ref ceDisplayText, value);
    }

    /// <summary>
    /// Undoキーのテキストです。
    /// </summary>
    string undoDisplayText = "Undo";

    /// <summary>
    /// ÷のキーのテキストを取得、設定します。
    /// </summary>
    public string UndoDisplayText
    {
        get => undoDisplayText;
        set => SetProperty(ref undoDisplayText, value);
    }
    /// <summary>
    /// バックスペースのキーのテキストです。
    /// </summary>
    string backspaceDisplayText = "⌫";

    /// <summary>
    /// バックスペースのキーのテキストを取得、設定します。
    /// </summary>
    public string BackspaceDisplayText
    {
        get => backspaceDisplayText;
        set => SetProperty(ref backspaceDisplayText, value);
    }
}
