using System.ComponentModel;

namespace Marimo.WindowsCalculator.Models.Calculations;

/// <summary>
/// 計算を表します。
/// </summary>
public abstract class Calculation : ModelBase
{

    /// <summary>
    /// エラーが出たときに表示されるエラーを取得、設定します。
    /// </summary>
    public virtual string? DisplayError => Receiver.DisplayError;

    /// <summary>
    /// Nullのように扱うCalculationを取得します。計算の初期値などにも使います。
    /// </summary>
    public static Calculation NullObject { get; } = new NullObjectCalculation();

    /// <summary>
    /// 計算する対象。
    /// </summary>
    Calculation receiver;

    /// <summary>
    /// 計算する対象を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 計算する対象です。
    /// 自身を返す場合があるので無限ループに気を付けましょう。。
    /// </remarks>
    public virtual Calculation Receiver 
    {
        get => receiver;
        internal set
        {
            if(receiver != null)
            {
                receiver.PropertyChanged -= ReceiverPropertyChanged;
            }
            SetProperty(ref receiver, value);
            receiver.PropertyChanged += ReceiverPropertyChanged;

        }
    }

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
    public abstract string Expression { get; }

    /// <summary>
    /// Calculationクラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="receiver">計算する対象。</param>
    public Calculation(Calculation receiver)
    {
        this.receiver = receiver;
        if (receiver != null)
        {
            receiver.PropertyChanged += ReceiverPropertyChanged;
        }
        PropertyChanged += (_, e) =>
        {
            if (e.PropertyName is nameof(Result))
            {
                OnPropertyChanged(nameof(Expression));
            }
        };
    }

    /// <summary>
    /// ReceiverのPropertyChangedが起きたときに呼び出されます。
    /// </summary>
    /// <param name="sender">イベントが発生したオブジェクト。</param>
    /// <param name="e">イベント情報。</param>
    internal void ReceiverPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Result))
        {
            OnPropertyChanged(nameof(Result));
        }
        else if (e.PropertyName == nameof(DisplayError))
        {
            OnPropertyChanged(nameof(DisplayError));
        }
    }
}
