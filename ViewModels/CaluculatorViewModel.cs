using Marimo.MauiBlazor.Models;

namespace Marimo.MauiBlazor.ViewModels;

public class CaluculatorViewModel
{
    IncrementalParser parser { get; } = new();
    Calculator model { get; } = new();

    public CaluculatorViewModel()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    public void Input(char input)
    {
        parser.Input(input);
        if (parser.ActiveToken != null)
        {
            model.Input(parser.ActiveToken);
        }
    }

    public int Number => model.Result;
}
