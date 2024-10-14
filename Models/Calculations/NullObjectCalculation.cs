namespace Marimo.WindowsCalculator.Models.Calculations;


/// <summary>
/// CalculationのNullオブジェクトです。
/// </summary>
internal class NullObjectCalculation : Calculation
{
    /// <summary>
    /// NullObjectクラスの新しいインスタンスを初期化します。
    /// </summary>
    internal NullObjectCalculation() : base(null!)
    { }

    /// <summary>
    /// Receiverはnullにできないので自身を取得します
    /// </summary>
    /// <remarks>
    /// Receiverはnullにできないので自身を取得します。
    /// 無限ループにならないように気を付けて使いましょう。
    /// </remarks>
    public override Calculation Receiver => this;

    /// <summary>
    ///  0を取得します。
    /// </summary>
    public override decimal? Result => 0;

    /// <summary>
    /// 自身を表す式です。空文字列を取得します。
    /// </summary>
    public override string Expression => "";
}
