using Marimo.WindowsCalculator.Models.Tokens;

namespace Marimo.WindowsCalculator.Models.Calculations;


/// <summary>
/// CalculationのNullオブジェクトです。
/// </summary>
/// <remarks>
/// CalculationのNullオブジェクトです。Calculationの連結リストの番兵の役割もします。
/// </remarks>
internal class NullObjectCalculation : Calculation
{
    /// <summary>
    /// nullを取得します。
    /// </summary>
    public override string? DisplayError => null;

    /// <summary>
    /// Receiverはnullにできないので自身を取得します
    /// </summary>
    /// <remarks>
    /// Receiverはnullにできないので自身を取得します。
    /// 無限ループにならないように気を付けて使いましょう。
    /// </remarks>
    public override Calculation Receiver => this;

    /// <summary>
    ///  nullを取得します。
    /// </summary>
    public override decimal? Result => 0;

    /// <summary>
    /// 自身を表す式です。空文字列を取得します。
    /// </summary>
    public override string Expression => "";

    /// <summary>
    /// NullObjectクラスの新しいインスタンスを初期化します。
    /// </summary>
    internal NullObjectCalculation() : base(null!)
    { }
}
