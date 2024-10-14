using Marimo.WindowsCalculator.Models;
using Marimo.WindowsCalculator.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using System;

namespace Marimo.WindowsCalculator.WinUI3;
public sealed partial class MainWindow : Window
{
    public MainWindow(CalculatorViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);

        new KeyObserver(
            Root,
            TimeSpan.FromMilliseconds(500),
            TimeSpan.FromMilliseconds(100))
        .ObserveKeys()
        .Subscribe(action =>
        {
            try
            {
                switch (action)
                {
                    case InputAction.Zero:
                        viewModel.InputZeroCommand.Execute(null);
                        break;
                    case InputAction.One:
                        viewModel.InputOneCommand.Execute(null);
                        break;
                    case InputAction.Two:
                        viewModel.InputTwoCommand.Execute(null);
                        break;
                    case InputAction.Three:
                        viewModel.InputThreeCommand.Execute(null);
                        break;
                    case InputAction.Four:
                        viewModel.InputFourCommand.Execute(null);
                        break;
                    case InputAction.Five:
                        viewModel.InputFiveCommand.Execute(null);
                        break;
                    case InputAction.Six:
                        viewModel.InputSixCommand.Execute(null);
                        break;
                    case InputAction.Seven:
                        viewModel.InputSevenCommand.Execute(null);
                        break;
                    case InputAction.Eight:
                        viewModel.InputEightCommand.Execute(null);
                        break;
                    case InputAction.Nine:
                        viewModel.InputNineCommand.Execute(null);
                        break;
                    case InputAction.Dot:
                        viewModel.InputDotCommand.Execute(null);
                        break;
                    case InputAction.Add:
                        viewModel.InputAddCommand.Execute(null);
                        break;
                    case InputAction.Substract:
                        viewModel.InputSubstractCommand.Execute(null);
                        break;
                    case InputAction.Multiply:
                        viewModel.InputMultiplyCommand.Execute(null);
                        break;
                    case InputAction.Divide:
                        viewModel.InputDivideCommand.Execute(null);
                        break;
                    case InputAction.Equal:
                        viewModel.InputEqualCommand.Execute(null);
                        break;
                    case InputAction.Backspace:
                        viewModel.InputBackspaceCommand.Execute(null);
                        break;
                    case InputAction.C:
                        viewModel.InputCCommand.Execute(null);
                        break;
                    case InputAction.CE:
                        viewModel.InputCECommand.Execute(null);
                        break;
                    case InputAction.Undo:
                        viewModel.InputUndoCommand.Execute(null);
                        break;
                    case InputAction.Redo:
                        viewModel.InputRedoCommand.Execute(null);
                        break;
                }
            }
            catch { }
        });
    }

    public CalculatorViewModel ViewModel { get; }

    bool IsHistoryShowed { get; set; } = false;

    void HistoryClick(object _, TappedRoutedEventArgs e)
    {
        if (IsHistoryShowed) return;
        

        var showStoryboard = (Storyboard)Root.Resources["SlideInAnimation"];
        showStoryboard.Begin();

        showStoryboard.Completed += (_, _) =>
        {
            IsHistoryShowed = true;
        };
    }

    void MainClick(object sender, TappedRoutedEventArgs e)
    {
        if (!IsHistoryShowed) return;
        IsHistoryShowed = false;

        var hideStoryboard = (Storyboard)Root.Resources["SlideOutAnimation"];
        hideStoryboard.Begin();
    }

    void SettingsClick(object sender, TappedRoutedEventArgs e)
    {
        SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
    }
}
