using Marimo.WindowsCalculator.ViewModels;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Marimo.WindowsCalculator.WinUI3;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow(CalculatorViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
    }

    public CalculatorViewModel ViewModel { get; }
}
