using CommunityToolkit.Mvvm.ComponentModel;

namespace Marimo.WindowsCalculator.Models.Calculations;

/// <summary>
/// 計算を表します。
/// </summary>
/// <param name="receiver">計算する対象。</param>
public abstract class Calculation(Calculation receiver) : ModelBase
{

    /// <summary>
    /// Nullのように扱うCalculationを取得します。計算の初期値などにも使います。
    /// </summary>
    public static Calculation NullObject { get; } = new NullCalculation();

    /// <summary>
    /// 計算する対象を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算する対象です。
    /// ほぼnullになることはありませんが、初期表示の数字のみnullとなります。
    /// </remarks>
    public virtual Calculation Receiver { get; set; } = receiver;

    /// <summary>
    /// 計算した結果を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算した結果です。次の計算があった場合は、計算対象となります。
    /// 基本的にはnullになりません。
    /// 演算子が入力され、計算対象が入力されていない場合などにnullとなります。
    /// </remarks>
    public abstract decimal? Result { get; }


    /// <summary>
    /// この計算がActiveCalculatorの場合に表示される式を取得します。
    /// </summary>
    public abstract string CurrentExpression { get; }
}
