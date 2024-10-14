using CommunityToolkit.Mvvm.ComponentModel;
using log4net;

namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// モデルの基底クラスとなり、ログ機能とINorifyPropertyChangedインターフェースの補助機能を提供します。
/// </summary>
public  class ModelBase : ObservableObject
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
