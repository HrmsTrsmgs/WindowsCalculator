using Marimo.WindowsCalculator.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System;
using System.Reactive.Linq;
using Windows.System;

namespace Marimo.WindowsCalculator.WinUI3;

internal class KeyObserver
{
    UIElement Element { get; }
    TimeSpan InitialDelay { get; }
    TimeSpan RepeatInterval { get; }
    internal KeyObserver(UIElement element, TimeSpan initialDelay, TimeSpan repeatInterval)
    {
        Element = element;
        InitialDelay = initialDelay;
        RepeatInterval = repeatInterval;
    }

    public IObservable<InputAction> ObserveKeys()
    {
        //:引継ぎ事項:
        //
        var keyDowns = Observable.FromEventPattern<KeyEventHandler, KeyRoutedEventArgs>(
            h => Element.KeyDown += h,
            h => Element.KeyDown -= h);

        var keyUps = Observable.FromEventPattern<KeyEventHandler, KeyRoutedEventArgs>(
            h => Element.KeyUp += h,
            h => Element.KeyUp -= h);

        var keyObservable =
            from e in keyDowns
            select new KeyPress(e.EventArgs.Key, false);

        return
            keyObservable
            .SelectMany(key => Observable.Return(0L)
                .Concat(Observable.Timer(InitialDelay))
                .Concat(Observable.Interval(RepeatInterval))
                .TakeUntil(keyUps).Select(_ => key))
            .Select<KeyPress, InputAction?>(key => key switch
            {
                (VirtualKey.Number0, _) or (VirtualKey.NumberPad0, _) => InputAction.Zero,
                (VirtualKey.Number1, _) or (VirtualKey.NumberPad1, _) => InputAction.One,
                (VirtualKey.Number2, _) or (VirtualKey.NumberPad2, _) => InputAction.Two,
                (VirtualKey.Number3, _) or (VirtualKey.NumberPad3, _) => InputAction.Three,
                (VirtualKey.Number4, _) or (VirtualKey.NumberPad4, _) => InputAction.Four,
                (VirtualKey.Number5, _) or (VirtualKey.NumberPad5, _) => InputAction.Five,
                (VirtualKey.Number6, _) or (VirtualKey.NumberPad6, _) => InputAction.Six,
                (VirtualKey.Number7, _) or (VirtualKey.NumberPad7, _) => InputAction.Seven,
                (VirtualKey.Number8, _) or (VirtualKey.NumberPad8, _) => InputAction.Eight,
                (VirtualKey.Number9, _) or (VirtualKey.NumberPad9, _) => InputAction.Nine,
                (VirtualKey.Decimal, _) => InputAction.Dot,
                (VirtualKey.Add, _) => InputAction.Add,
                (VirtualKey.Subtract, _) => InputAction.Substract,
                (VirtualKey.Multiply, _) => InputAction.Multiply,
                (VirtualKey.Divide, _) => InputAction.Divide,
                (VirtualKey.Enter, _) => InputAction.Equal,
                (VirtualKey.Back, _) => InputAction.Backspace,
                (VirtualKey.Escape, _) => InputAction.C,
                (VirtualKey.Delete, _) => InputAction.CE,
                (VirtualKey.Z, true) => InputAction.Undo,
                (VirtualKey.Y, true) => InputAction.Redo,
                _ => null
            })
            .Where(action => action != null)
            .Select(action => action.Value);
    }
}
