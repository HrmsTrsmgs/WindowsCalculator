using Marimo.WindowsCalculator.Models.Calculations;
using Marimo.WindowsCalculator.Models.Tokens;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Marimo.WindowsCalculator.Models;

/// <summary>
/// 電卓を表します。
/// </summary>
public class Calculator : ModelBase
{
    /// <summary>
    /// 現在行っている計算です。
    /// </summary>
    Calculation activeCaluculation = Calculation.NullObject;

    /// <summary>
    /// 履歴作成のための計算です。
    /// </summary>
    readonly ObservableCollection<OperationCalculation> cumulativeCalculation = [];

    /// <summary>
    /// 現在、最新で行われている計算を取得、設定します。
    /// </summary>
    /// <remarks>
    /// 現在、最新で行われている計算を取得、設定します。
    /// 設定された値のレシーバーは、直前の計算に設定されます。
    /// </remarks>
    public Calculation ActiveCaluculation
    {
        get => activeCaluculation;
        private set
        {
            if (activeCaluculation == value) return;
            value.Receiver = activeCaluculation;
            SetProperty(ref activeCaluculation, value);
        }
    }

    /// <summary>
    /// Redo用に取ってある計算を取得、設定します。
    /// </summary>
    public Calculation? RedoCalculation { get; set; } = null;


    /// <summary>
    /// 履歴のもととなる計算のリストです。
    /// </summary>
    readonly ObservableCollection<CalculationHistoryItem> calculationHistory = [];

    /// <summary>
    /// 履歴用の計算結果一覧を取得します。
    /// </summary>
    public ReadOnlyObservableCollection<CalculationHistoryItem> CalculationHistory { get; }

    /// <summary>
    /// 計算結果を取得します。
    /// </summary>
    public string DisplayResult
        // :引継ぎ事項:
        // エラーが出たときに解除する方法の実装がWindows電卓通りにできていません。
        // Windows電卓では、数値とC,CE,バックスペース以外は受け付けなくなります。
        // 数値入力では計算式が消えて数字が表示され、他のものでは式が消えて0が表示されます。
        // ICommandのCanExecuteの設定も行ったほうがいいですね。
        // きちんとテストを実装してから実装を行ってください。
        => ActiveCaluculation.Receiver.DisplayError 
            ??ActiveCaluculation.DisplayNumber.ToString();


    /// <summary>
    /// 設定を取得します。
    /// </summary>
    public PropertySettings Settings { get; } = new();
    /// <summary>
    /// Calculatorクラスの新しいインスタンスを初期化します。
    /// </summary>
    public Calculator()
    {
        CalculationHistory = new(calculationHistory);
        cumulativeCalculation.CollectionChanged += CumulativeCalculationCollectionChanged;
        PropertyChanged += OnPropertyChanged;
    }

    /// <summary>
    /// CumulativeCalculationが変更された時に、履歴に反映させます。
    /// </summary>
    /// <param name="sender">イベントが起きたオブジェクト。</param>
    /// <param name="args">イベント情報。</param>
    void CumulativeCalculationCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        switch (args.Action)
        {
            case NotifyCollectionChangedAction.Add:
                var newItems = (args.NewItems?.OfType<OperationCalculation>()) ?? throw new NotSupportedException();
                if (!newItems.Any()) return;
                var newItem = new CalculationHistoryItem(newItems.Single());
                newItem.PropertyChanged += (_, args) =>
                {
                    if (args.PropertyName != nameof(newItem.Result)) return;

                    if (newItem.Result == null)
                    {
                        calculationHistory.Remove(newItem);
                    }
                    else
                    {
                        calculationHistory.Insert(0, newItem);
                    }
                };
                if (newItem.Result != null)
                {
                    calculationHistory.Insert(0, newItem);
                }
                break;
            case NotifyCollectionChangedAction.Remove:
                var oldItems
                = (args.OldItems?.OfType<OperationCalculation>()) ?? throw new NotSupportedException();
                if (!oldItems.Any()) return;
                var removed = calculationHistory
                .Where(it => it.Operation == oldItems.First())
                .SingleOrDefault();
                if (removed != null)
                {
                    calculationHistory.Remove(removed);
                }
                break;
            case NotifyCollectionChangedAction.Reset:
                calculationHistory.Clear();
                break;
        }
    }

    /// <summary>
    /// PropertyChangedイベントが起きたときの処理です。
    /// </summary>
    /// <param name="sender">イベントが起きたオブジェクト。</param>
    /// <param name="e">イベント情報。</param>
    void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(ActiveCaluculation)) return;

        if (ActiveCaluculation is not OperationCalculation calcuraton) return;
        if (!cumulativeCalculation.Contains(calcuraton))
        {
            cumulativeCalculation.Add(calcuraton);
        }
        else
        {
            while (cumulativeCalculation?.LastOrDefault() != calcuraton)
            {
                cumulativeCalculation?.Remove(cumulativeCalculation.Last());
            }
            if (cumulativeCalculation.Any())
            {
                cumulativeCalculation?.Remove(cumulativeCalculation.Last());
            }
        }
    }

    /// <summary>
    /// 履歴を削除します。
    /// </summary>
    public void ClearCalculationHistory()
    {
        cumulativeCalculation.Clear();
    }

    /// <summary>
    /// 電卓にトークンを入力します。
    /// </summary>
    /// <param name="token">入力されたトークン。</param>
    public void Input(Token token)
    {
        switch (token)
        {
            case NumberToken t:
                InputNumberToken(t);
                break;
            case OperatorToken t:
                InputOperatorToken(t);
                break;
            case OtherToken t:
                InputOtherToken(t);
                break;
        }
        OnPropertyChanged(nameof(DisplayResult));
    }



    /// <summary>
    /// 数値と演算子以外の入力を行います。
    /// </summary>
    Calculation GetCalculationOtherWhenTokenInputed(OtherToken otherToken)
        => otherToken.Kind switch
        {
            OtherTokenKind.Equal =>
                new EqualButtonCalculation(
                    ActiveCaluculation,
                    ActiveCaluculation switch
                    {
                        EqualButtonCalculation c => GetLastOperationCalculation(c),
                        _ => null
                    }),
            OtherTokenKind.C =>
                new DeleteCalculation(ActiveCaluculation),
            _ => throw new NotSupportedException()
        };

    /// <summary>
    /// イコールで繰り返すために、最後の計算を取得します。
    /// </summary>
    /// <param name="lastCaluculation">これまでの最後の計算</param>
    /// <returns>これまでの最後の計算。</returns>
    private OperationCalculation? GetLastOperationCalculation(
        EqualButtonCalculation lastCaluculation)
    {
        var before
            = lastCaluculation.LastOperationCalculation
            ?? lastCaluculation.Receiver as OperationCalculation
            ?? throw new InvalidOperationException();
        return OperationCalculation.Create(ActiveCaluculation, before.OperatorAction, before.Operand, true);
    }

    /// <summary>
    /// 数値トークンが入力された処理を行います。
    /// </summary>
    /// <param name="token">数値トークン。</param>
    void InputNumberToken(NumberToken token)
    {
        switch (ActiveCaluculation)
        {
            case NumberCalculation c:
                c.NumberToken = token;
                break;
            case OperationCalculation c:
                c.Operand = token;
                break;
            case NullObjectCalculation c:
                ActiveCaluculation = new NumberCalculation(c, token);
                break;
        }
    }

    /// <summary>
    /// 演算トークンが入力された処理を行います。
    /// </summary>
    /// <param name="token">演算トークン</param>
    void InputOperatorToken(OperatorToken token)
    {
        switch (ActiveCaluculation)
        {
            case OperationCalculation c when c.Operand == null:
                ActiveCaluculation = OperationCalculation.Create(
                    c.Receiver,
                    token.Operator,
                    c.Operand,
                    c.IsDisplayResult);
                break;
            case OperationCalculation c:
                c.IsDisplayResult = true;
                goto default;
            default:
                ActiveCaluculation
                    = token.Operator switch
                    {
                        InputAction.Equal => new EqualButtonCalculation(ActiveCaluculation, null),
                        _ => OperationCalculation.Create(ActiveCaluculation, token.Operator, null, true)
                    };
                break;
        }
    }

    /// <summary>
    /// その他のトークンが入力された処理を行います。
    /// </summary>
    /// <param name="token">その他のトークン。</param>
    void InputOtherToken(OtherToken token)
    {
        switch (token.Kind)
        {
            case OtherTokenKind.Undo:
                InvokeUndo();
                break;
            case OtherTokenKind.Redo:
                InvokeRedu();
                break;
            case OtherTokenKind.Equal:
                InvokeEqual();
                goto default;
            case OtherTokenKind.C:
                InvokeC();
                break;
            case OtherTokenKind.CE:
                InvokeCE();
                break;
            default:
                ActiveCaluculation = GetCalculationOtherWhenTokenInputed(token);
                break;
        };
    }

    /// <summary>
    /// Undoキーの処理を行います。
    /// </summary>
    void InvokeUndo()
    {
        RedoCalculation ??= ActiveCaluculation;
        switch (ActiveCaluculation)
        {
            case OperationCalculation c:
                c.IsDisplayResult = false;
                break;
            case EqualButtonCalculation:
                switch (ActiveCaluculation.Receiver)
                {
                    case OperationCalculation cc:
                        cc.IsDisplayResult = false;
                        break;
                }
                break;
        }
        SetProperty(ref activeCaluculation!, ActiveCaluculation.Receiver, nameof(ActiveCaluculation));
    }

    /// <summary>
    /// Redoの処理を行います。
    /// </summary>
    void InvokeRedu()
    {
        if (RedoCalculation != null)
        {
            var nextCalculation = RedoCalculation;
            while (nextCalculation!.Receiver != ActiveCaluculation)
            {
                nextCalculation = nextCalculation!.Receiver;
            }
            switch (nextCalculation)
            {
                case EqualButtonCalculation:
                    switch (activeCaluculation)
                    {
                        case OperationCalculation cc:
                            cc.IsDisplayResult = true;
                            break;
                    }
                    break;
            }
            SetProperty(ref activeCaluculation, nextCalculation, nameof(ActiveCaluculation));
        }
    }

    /// <summary>
    /// イコールキーの処理を行います。
    /// </summary>
    void InvokeEqual()
    {
        switch (ActiveCaluculation)
        {
            case OperationCalculation c:
                c.IsDisplayResult = true;
                break;
            case EqualButtonCalculation c:
                switch (c.LastOperationCalculation)
                {
                    case OperationCalculation cc:
                        cc.IsDisplayResult = true;
                        break;
                }
                break;
        }
    }

    /// <summary>
    /// Cキーの処理を行います。
    /// </summary>
    void InvokeC()
    {
        ActiveCaluculation = Calculation.NullObject;
        ClearCalculationHistory();
    }

    /// <summary>
    /// CEキーの処理を行いまうs。。
    /// </summary>
    void InvokeCE()
    {
        switch (ActiveCaluculation)
        {
            case NumberCalculation c:
                c.NumberToken = new(0);
                break;
            default:
                ActiveCaluculation = new NumberCalculation(ActiveCaluculation);
                break;
        }
    }
}
