using CommunityToolkit.Mvvm.ComponentModel;
using log4net;

namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// モデルの基底クラスとなる。
/// ログ機能とINorifyPropertyChangedインターフェースの補助機能を提供します。
/// </summary>
/// <remarks>
/// モデルの基底クラスとなる。
/// ログ機能とINorifyPropertyChangedインターフェースの補助機能を提供します。
/// INorifyPropertyChangedは表示に必要なものについてのみ実装します。
/// なのでこのクラスを実装していないモデルもあります。
/// </remarks>
public class ModelBase : ObservableObject
{
    /// <summary>
    /// ログを行うロガーを取得します。
    /// </summary>
    protected ILog Log { get; }

    /// <summary>
    /// ModelBaseクラスの新しいインスタンスを初期化します。
    /// </summary>
    public ModelBase()
    {
        Log = LogManager.GetLogger(GetType());
    }
}
